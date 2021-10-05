/*

    Copyright © 2018-2021, ETH Zurich, D-BSSE, Aaron Ponti & Todd Duncombe
    All rights reserved. This program and the accompanying materials
    are made available under the terms of the Apache-2.0 license
    which accompanies this distribution, and is available at
    https://www.apache.org/licenses/LICENSE-2.0

    SpectraSorter is based on FXStreamer by Oliver Lischtschenko (Ocean Optics):
    Lischtschenko, O.; private communication on OBP protocol, 2018. 
    The original code is added to the repository.

*/

using spectra.devices;
using spectra.plotting;
using spectra.processing;
using spectra.state;
using spectra.utils;
using MadWizard.WinUSBNet;
using OBP_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static System.Windows.Forms.TabControl;
using System.Configuration;
using static spectra.state.SettingsManager;
using System.Globalization;

// Spectra Sorter is based on FXStreamer by Oliver Lischtschenko (Ocean Optics)
// 
// What follows is the original description of FXStreamer.

//*****************************************************************************
//*****************************************************************************
//
// This app is intended to demonstrate how spectra can be readout from an
// OceanFX spectrometer at high data rates (up to 4500 spectra/sec).
//
// This app sends OBP messages to the spectrometer using "OBP_Library" which
// uses "WinUSBNet" and/or Sockets to send OBP messages to the spectrometer
// over USB and TCP-IP respectively.
//
// Note: This app does NOT use SeaBreeze or OmniDriver.
//
// Up to four threads of execution are utilized during high speed acquisition:
//
//  1. Main UI Thread
//     A timer, UIUpdateTimer, triggers every 100ms to update the chart and
//     IO statistics.  This timer is always active throughout the lifetime
//     of the app regardless of whether an acquisition is in progress.
//
//  2. An IO thread, doHighSpeedAcquisition, ingests spectrum IO buffers from
//     mSupplyQueue, requests a spectrum from the spectrometer, reads results
//     into the spectrum's Response buffer, and adds the spectrum to
//     mComputeQueue for processing.
//
//     The OceanFX spectrometer can accept more than one request for spectrum
//     at a time.  The IO thread utilizes an overlapped command technique
//     where it initially sends two requests for spectrum before reading out
//     a spectrum.  The idea is to keep one request for spectrum active on the
//     spectrometer at all times so the spectrometer is busy filling the next
//     request while the app is processing the previous response.
//
//  3. A compute thread, doComputeSpectrum, ingests spectrum from the mComputeQueue,
//     computes a result (absorbance, transmission, etc.), optionally enqueues the 
//     result to mSaveQueue, and returns the spectrum to the mSupplyQueue for reuse.
//
//  3. A save thread, doSaveSpectrum, ingests spectrum from the mSaveQueue,
//     and persists it to disk.
//
// This app should be able to retrieve up to 4500 spectra/sec from an OceanFX
// spectrometer.  However, this app may NOT be able to persist
// 4500 spectra/sec to disk with the default save as CSV text file.  Saving
// floating point results such as transmission and absorbance is even
// slower due to the conversion of 2136 float values to string.  To optimize
// saving floating point results this app should be modified to save to a
// binary file format such as SPC or a proprietary binary format.
//
//*****************************************************************************
//*****************************************************************************
//

namespace spectra.ui
{
    public partial class MainWindow : Form
    {
        #region data_members

        /// <summary>
        /// Private form instance.
        /// </summary>
        private static MainWindow mInstance = null;

        ////========================= data_members =========================

        // About box
        private AboutDialog aboutDialog = null;

        // Shortcuts box
        private ShortcutsDialog shortcutsDialog = null;

        // Queues
        private Queue<OBPGetRawSpectrumWithMetadata> mComputeQueue = new Queue<OBPGetRawSpectrumWithMetadata>();

        private Queue<OBPGetRawSpectrumWithMetadata> mSupplyQueue = new Queue<OBPGetRawSpectrumWithMetadata>();

        private Queue<SpectrumForSavingQueueObject> mSaveQueue = new Queue<SpectrumForSavingQueueObject>();

        private bool mClearBufferBeforeTest = true;

        // UI Control
        private System.Windows.Forms.Timer mUITimer;

        private bool mTimerBusy = false;
        private int mUIUpdateRateMS = 100; // Rate at which the UI is updated in milliseconds

        // Acquisition Statistics
        private long mTotalRequests; // Total requests for spectra sent

        private long mTotalSpectraReceived; // Total spectra received
        private long mTotalSpectraComputed; // Total spectra computed
        private long mTotalSpectraSaved; // Total spectra saved
        private long mTotalAcquireTime; // Total milliseconds the acquisition has run (or ran)

        private long mTotalSaveTime; // Total time the compute and save thread ran (note: there's a minimum 100ms wait after acquisition completes)

        private long mCurSpectraPerSec; // The current acquisition rate (over the past 1 second)

        private int mMinSupplyCount; // The minimum number of spectrum detected in the supply queue (can be used to optimize how many to allocate)

        private long mReceiveFailures = 0; // Number of receive failures
        private long mBytesFlushed = 0; // Number of bytes flushed after the acquisition completes (should be zero)
        private long mSpinCounter = 0; // Spin wait counter (waiting for IO buffers in the supply queue)
        private long mTotalReceivedBytes = 0; // Total bytes received from OceanFX
        private long mTotalSavedBytes = 0; // Total bytes saved to disk

        // New MainPlotter
        private MainPlotter mMainPlotter = null;

        // Arduino board
        private Arduino mArduino = null;

        // OceanFX board
        private OceanFX mOceanFX = null;

        #endregion data_members

        #region initialization_and_cleanup

        //========================= initialization_and_cleanup =========================

        private MainWindow()
        {
            // Slightly increase the minimum number of Threads in the application ThreadPool
            ThreadPool.GetMinThreads(out int minW, out int minP);
            ThreadPool.SetMinThreads(2 * minW, 2 * minP);

            // Setup window
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            InitializeComponent();

            // Register event handlers
            RegisterEventHandlers();

            // App title... include the version
            string appName = Assembly.GetEntryAssembly().GetName().Name;
            string appVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
            appVersion = appVersion.Substring(0, appVersion.LastIndexOf('.'));
            this.Text = $"{appName}  {appVersion}";

            // Initialize the main plotter with the reference to the chart
            this.mMainPlotter = new MainPlotter(this.mainChart);
        }

        // Get read-only access to the MainPlotter
        public static MainPlotter mainPlotter => MainWindow.Instance.mMainPlotter;

        /// <summary>
        /// MainWindow (singleton) instance.
        /// </summary>
        public static MainWindow Instance
        {
            get
            {
                // If the Form has not been created yet,
                // instantiate it now.
                if (mInstance == null)
                {
                    mInstance = new MainWindow();
                }

                // Return a reference
                return mInstance;
            }
        }

        // UI initialization
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initialize state
            State.Instance.IsPerformingStandardAcquisition = false;
            State.Instance.IsStopAcquiringRequested = false;

            // Initialize the output to raw spectrum
            SettingsManager.ResultSpectrumType = Options.ResultSpectrumType.RAW_SPECTRUM;

            // Initialize the reference type to NONE at start
            SettingsManager.ReferenceType = Options.ReferenceType.NONE;

            // Initialize the plot type to OUTPUT at start
            SettingsManager.CurrentPlotType = Options.PlotType.OUTPUT;

            // Update menus
            this.ApplySettingsToUI();

            // Initialize filtering
            SpectrumFilterer.Instance.InitializeFilterFromCurrentSettings();

            buttonStartAcquisition.BackColor = Color.Gray;
            buttonAbortAcquisition.BackColor = Color.Gray;

            // Find all USB, network, and COM devices
            FindUSBDevices();
            FindIPDevices();
            FindCOMDevices();

            //  UI update timer
            mUITimer = new System.Windows.Forms.Timer();
            mUITimer.Tick += new EventHandler(UIUpdateTimer);
            mUITimer.Interval = mUIUpdateRateMS;
            mUITimer.Enabled = true;
            mUITimer.Start();

            // Initialize the Arduino board
            mArduino = new Arduino();
            mArduino.TriggerDelayInMicros = SettingsManager.ArduinoTriggerDelay;

            // Inform on timer accuracy
            toolStripStatusSystemTimeAccuracy.Text = $"System timer accuracy: {HighResStopWatch.TimerAccuracyInNanoseconds()} ns.";

        }

        // Cleanup
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Exit application?",
                "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Cleanly shutdown everything
                    State.Instance.IsClosing = true;

                    // Request the acquisition to stop
                    State.Instance.IsStopAcquiringRequested = true;

                    //
                    // Wait for the acquisition to complete
                    //
                    bool bTimeout = false;
                    DateTime startTime = DateTime.Now;
                    while (State.Instance.IsPerformingStandardAcquisition && !bTimeout)
                    {
                        Thread.Sleep(1);
                        TimeSpan deltaTime = DateTime.Now - startTime;
                        if (deltaTime.Milliseconds > 500)
                        {
                            bTimeout = true;
                        }
                    }

                    // Stop the UI timer
                    mUITimer.Stop();
                    mUITimer.Enabled = false;
                    mUITimer.Dispose();

                    // Close the open device
                    if (mOceanFX != null)
                    {
                        closeDeviceConnection(false);
                    }

                    // Disconnect the Arduino board
                    mArduino.Disconnect();

                    // Persist the settings
                    // SettingsManager.Save();

                    // Dispose the various dialogs
                    if (this.aboutDialog != null)
                    {
                        this.aboutDialog.Dispose();
                    }

                    // Shortcuts box
                    if (this.shortcutsDialog != null)
                    {
                        this.shortcutsDialog.Dispose();
                    }

                    // Now we can close the application
                    this.Dispose(true);
                }
                catch (ObjectDisposedException)
                {
                    // Ignore.
                }

                // Accept the closing event
                e.Cancel = false;

                // Now exit (no matter what)
                Application.Exit();
            }
            else
            {
                // Ceject the closing event
                e.Cancel = true;
            }
        }

        #endregion initialization_and_cleanup

        #region device_discovery

        //========================= device_discovery =========================

        private void FindUSBDevices()
        {
            dataGridViewUSBDeviceList.Rows.Clear();

            // Close the current device, if any
            if (mOceanFX != null)
            {
                closeDeviceConnection();
            }

            // Find the USB devices
            Thread usbDevicesThread = new Thread(doFindUSBDevices);
            usbDevicesThread.Start();
        }

        private void FindIPDevices()
        {
            dataGridViewIPDevices.Rows.Clear();

            // Close the current device, if any
            if (mOceanFX != null)
            {
                closeDeviceConnection();
            }

            // Find the Network devices
            Thread ipDevicesThread = new Thread(doFindIPDevices);
            ipDevicesThread.Start();
        }

        private void FindCOMDevices()
        {
            dataGridViewArduinoDevices.Rows.Clear();

            // Close the current device, if any
            disconnectFromCOMDevice();

            // Find the COM devices
            Thread comDevicesThread = new Thread(doFindCOMDevices);
            comDevicesThread.Start();
        }

        /// <summary>
        /// Scan for COM devices (Arduino)
        /// </summary>
        private void doFindCOMDevices()
        {
            State.Instance.IsCOMScanning = true;

            // Clear the data grid
            dataGridViewArduinoDevices.Rows.Clear();

            COMScanner.Instance.Scan();

            Invoke((MethodInvoker)delegate
            {
                onFindCOMComplete();  // Invoke on UI thread
            });
        }

        /// <summary>
        /// Scan for USB devices (OceanFX)
        /// </summary>
        private void doFindUSBDevices()
        {
            State.Instance.IsUSBScanning = true;

            // Scan
            USBScanner.Instance.Scan();

            Invoke((MethodInvoker)delegate
            {
                onFindUSBComplete();  // Invoke on UI thread
            });
        }

        /// <summary>
        /// Scan for IP devices (OceanFX)
        /// </summary>
        private void doFindIPDevices()
        {
            State.Instance.IsIPScanning = true;

            IPScanner.Instance.Scan();

            if (!State.Instance.IsClosing)
            {
                Invoke((MethodInvoker)delegate
                {
                    onFindIPDevicesComplete();  // Invoke on UI thread
                });
            }
        }

        private void onFindCOMComplete()
        {
            State.Instance.IsCOMScanning = false;

            buttonRescanArduino.Enabled = true;

            // If one or more devices were found...
            if (COMScanner.Instance.DeviceDescriptors.Count > 0)
            {
                dataGridViewArduinoDevices.SelectionChanged -= dataGridViewArduinoDevices_SelectionChanged;

                foreach (SerialPortWrapper wr in COMScanner.Instance.DeviceDescriptors.Values)
                {
                    int iRow = dataGridViewArduinoDevices.Rows.Add(wr.Name);
                    dataGridViewArduinoDevices.Rows[iRow].Tag = wr;
                }

                dataGridViewArduinoDevices.ClearSelection();

                dataGridViewArduinoDevices.SelectionChanged += dataGridViewArduinoDevices_SelectionChanged;
            }
        }

        private void onFindUSBComplete()
        {
            State.Instance.IsUSBScanning = false;

            if (!State.Instance.IsIPScanning)
            {
                buttonUSBRescan.Enabled = true;
            }

            // If one or more devices were found...
            if (USBScanner.Instance.DeviceDescriptors.Count > 0)
            {
                dataGridViewUSBDeviceList.SelectionChanged -= dataGridViewUSBDeviceList_SelectionChanged;

                foreach (USBDeviceInfo di in USBScanner.Instance.DeviceDescriptors.Values)
                {
                    int iRow = dataGridViewUSBDeviceList.Rows.Add(di.DeviceDescription);
                    dataGridViewUSBDeviceList.Rows[iRow].Tag = di;
                }

                dataGridViewUSBDeviceList.ClearSelection();

                dataGridViewUSBDeviceList.SelectionChanged += dataGridViewUSBDeviceList_SelectionChanged;

                buttonUSBRescan.Enabled = true;
                buttonUSBConnect.Enabled = true;
                buttonUSBDisconnect.Enabled = false;
                toolStripOceanFXUSBStatus.Visible = true;
            }
            else
            {
                buttonUSBRescan.Enabled = true;
                buttonUSBConnect.Enabled = false;
                buttonUSBDisconnect.Enabled = false;
                toolStripOceanFXUSBStatus.Visible = false;
            }
        }

        private void onFindIPDevicesComplete()
        {
            State.Instance.IsIPScanning = false;

            if (!State.Instance.IsUSBScanning)
            {
                buttonUSBRescan.Enabled = true;
            }

            // If one or more devices were found...
            if (IPScanner.Instance.DeviceDescriptors.Count > 0)
            {
                dataGridViewIPDevices.SelectionChanged -= dataGridViewIPDevices_SelectionChanged;

                //foreach (USBDeviceInfo di in USBScanner.Instance.DeviceDescriptors.Values)
                foreach (KeyValuePair<string, IPScanner.IPDevice> entry in IPScanner.Instance.DeviceDescriptors)
                {
                    dataGridViewIPDevices.Rows.Add(entry.Value.ipAddr, entry.Value.portNum, entry.Value.serialNum);
                }
                dataGridViewIPDevices.ClearSelection();
                dataGridViewIPDevices.SelectionChanged += dataGridViewIPDevices_SelectionChanged;

                buttonIPRescan.Enabled = true;
                buttonIPConnect.Enabled = true;
                buttonIPDisconnect.Enabled = false;
                toolStripOceanFXIPStatus.Visible = true;
            }
            else
            {
                buttonIPRescan.Enabled = true;
                buttonIPConnect.Enabled = false;
                buttonIPDisconnect.Enabled = false;
                toolStripOceanFXIPStatus.Visible = false;
            }
        }

        // Rescan USB devices
        private void buttonUSBRescan_Click(object sender, EventArgs e)
        {
            buttonUSBRescan.Enabled = false;

            FindUSBDevices();
        }

        private void dataGridViewUSBDeviceList_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewUSBDeviceList.SelectedRows.Count > 0)
            {
                dataGridViewIPDevices.ClearSelection();
            }
        }

        private void dataGridViewIPDevices_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewIPDevices.SelectedRows.Count > 0)
            {
                dataGridViewUSBDeviceList.ClearSelection();
            }
        }

        private void dataGridViewArduinoDevices_SelectionChanged(object sender, EventArgs e)
        {
            // Nothing
        }

        #endregion device_discovery

        #region device_connection

        //========================= device_connection =========================

        private void buttonUSBConnect_Click(object sender, EventArgs e)
        {
            connectToSelectedUSBDevice();
        }

        protected void connectToSelectedUSBDevice()
        {
            if (mOceanFX != null)
            {
                closeDeviceConnection();
            }

            if (dataGridViewUSBDeviceList.SelectedRows.Count > 0)
            {
                USBDeviceInfo devInfo = (USBDeviceInfo)(dataGridViewUSBDeviceList.SelectedRows[0].Tag);

                if (devInfo == null)
                {
                    SetUSBConnectError("Failed to Open");
                }
                else
                {
                    Thread usbConnectThread = new Thread(() => doUSBConnect(devInfo));
                    usbConnectThread.Start();
                }
            }
            else
            {
                SetUSBConnectStatus("No Selection");
            }
        }

        protected void connectToSelectedIPDevice()
        {
            if (mOceanFX != null)
            {
                closeDeviceConnection();
            }

            if (dataGridViewIPDevices.SelectedRows.Count > 0)
            {
                int portNum = 57357;
                IPAddress ipAddr = null;

                try
                {
                    ipAddr = IPAddress.Parse(dataGridViewIPDevices.SelectedRows[0].Cells[0].Value.ToString());
                    portNum = int.Parse(dataGridViewIPDevices.SelectedRows[0].Cells[1].Value.ToString(), CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    // nothing to do
                }

                if (ipAddr == null)
                {
                    SetIPConnectError("Invalid IP");
                }
                else
                {
                    Thread tcpConnectThread = new Thread(() => doIPConnect(ipAddr, portNum));
                    tcpConnectThread.Start();
                }
            }
            else
            {
                SetIPConnectStatus("No Selection");
            }
        }

        protected void closeDeviceConnection(bool bUpdateUIControls = true)
        {
            if (mOceanFX != null)
            {
                mOceanFX.Disconnect();
            }

            if (bUpdateUIControls)
            {
                //labelConnectStatus.Visible = false;

                UpdateUIControls();

                clearDarkSpectrum();
                clearReferenceSpectrum();

                //updateDefaultSaveType();
            }
        }

        protected void doIPConnect(IPAddress ipAddr, int portNum)
        {
            try
            {
                // Instantiate the new USB device
                if (mOceanFX.IsConnected)
                {
                    mOceanFX.Disconnect();
                }

                // Initialize new connection
                mOceanFX = new IPOceanFX(ipAddr, portNum);

                doDeviceInitialization();
            }
            catch (Exception)
            {
                Invoke((MethodInvoker)delegate
                {
                    if (mOceanFX != null)
                    {
                        closeDeviceConnection();
                    }
                });
            }

            if (!mOceanFX.IsConnected)
            {
                Invoke((MethodInvoker)delegate
                {
                    onIPConnectFailed();  // Invoke on UI thread
                });
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    onIPConnected();  // Invoke on UI thread
                });
            }
        }

        protected void doUSBConnect(USBDeviceInfo devInfo)
        {
            try
            {
                // Instantiate the new USB device
                if (mOceanFX != null && mOceanFX.IsConnected)
                {
                    mOceanFX.Disconnect();
                }

                // Initialize new connection
                mOceanFX = new USBOceanFX(devInfo);

                doDeviceInitialization();
            }
            catch (Exception)
            {
                Invoke((MethodInvoker)delegate
                {
                    if (mOceanFX != null)
                    {
                        closeDeviceConnection();
                    }
                });
            }

            if (!mOceanFX.IsConnected)
            {
                Invoke((MethodInvoker)delegate
                {
                    onUSBConnectFailed();  // Invoke on UI thread
                });
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    onUSBConnected();  // Invoke on UI thread
                });
            }
        }

        protected void doCOMConnect(SerialPortWrapper device)
        {
            // Connect
            String port = device.Port;

            // Make sure to Disconnect if connected
            mArduino.Disconnect();

            // Set the new port
            mArduino.PortName = port;

            // Reconnect
            bool success = mArduino.Connect();

            Invoke((MethodInvoker)delegate
            {
                onCOMConnected(success);  // Invoke on UI thread
            });
        }

        protected void onUSBConnectFailed()
        {
            buttonUSBConnect.Enabled = true;
            buttonUSBDisconnect.Enabled = false;
            SetUSBConnectError("Failed to Connect");

            // Disable the settings panel
            TabPageCollection pages = tabControlAcquisition.TabPages;
            Debug.Assert(pages[4].Name.Equals("tabPageArduino", StringComparison.Ordinal));
            ((Control)pages[4]).Enabled = false;
        }

        protected void onUSBConnected()
        {
            buttonUSBConnect.Enabled = false;
            buttonUSBDisconnect.Enabled = true;

            SetUSBConnectStatus("Connected");

            // Update the ranges
            WavelengthRangeOptions.Instance.UpdateFileSaveUnits();
        }

        protected void onIPConnectFailed()
        {
            buttonUSBConnect.Enabled = true;
            buttonUSBDisconnect.Enabled = false;
            SetUSBConnectError("Failed to Connect");
        }

        protected void onIPConnected()
        {
            buttonIPConnect.Enabled = false;
            buttonIPDisconnect.Enabled = true;

            SetIPConnectStatus("Connected");

            // Update the ranges
            WavelengthRangeOptions.Instance.UpdateFileSaveUnits();
        }

        protected void onCOMConnected(bool success)
        {
            if (success)
            {
                // Set status
                SetCOMConnectStatus("Connected");

                // Update the Arduino parameter form
                arduinoParametersControl.SetArduino(mArduino);

                buttonConnectToArduino.Enabled = false;
                buttonDisconnectFromArduino.Enabled = true;

                // Enable the settings panel
                TabPageCollection pages = tabControlAcquisition.TabPages;
                Debug.Assert(pages[4].Name.Equals("tabPageArduino", StringComparison.Ordinal));
                ((Control)pages[4]).Enabled = true;
            }
            else
            {
                SetCOMConnectError("Could not connect!");

                buttonConnectToArduino.Enabled = true;
                buttonDisconnectFromArduino.Enabled = false;

                // Disable the settings panel
                TabPageCollection pages = tabControlAcquisition.TabPages;
                Debug.Assert(pages[4].Name.Equals("tabPageArduino", StringComparison.Ordinal));
                ((Control)pages[4]).Enabled = false;
            }
        }

        protected void SetUSBConnectError(string statusStr)
        {
            toolStripOceanFXUSBStatus.Visible = true;
            toolStripOceanFXUSBStatus.ForeColor = Color.Crimson;
            toolStripOceanFXUSBStatus.Text = "OceanFX (USB): " + statusStr;
        }

        protected void SetIPConnectError(string statusStr)
        {
            toolStripOceanFXIPStatus.Visible = true;
            toolStripOceanFXIPStatus.ForeColor = Color.Crimson;
            toolStripOceanFXIPStatus.Text = "OceanFX (IP): " + statusStr;
        }

        protected void SetUSBConnectStatus(string statusStr)
        {
            toolStripOceanFXUSBStatus.Visible = true;
            toolStripOceanFXUSBStatus.ForeColor = Color.Black;
            toolStripOceanFXUSBStatus.Text = "OceanFX (USB): " + statusStr;
        }

        protected void SetIPConnectStatus(string statusStr)
        {
            toolStripOceanFXIPStatus.Visible = true;
            toolStripOceanFXIPStatus.ForeColor = Color.Black;
            toolStripOceanFXIPStatus.Text = "OceanFX (IP): " + statusStr;
        }

        protected void SetCOMConnectError(string statusStr)
        {
            toolStripArduinoStatus.Visible = true;
            toolStripArduinoStatus.ForeColor = Color.Crimson;
            toolStripArduinoStatus.Text = "Arduino: " + statusStr;
        }

        protected void SetCOMConnectStatus(string statusStr)
        {
            toolStripArduinoStatus.Visible = true;
            toolStripArduinoStatus.ForeColor = Color.Black;
            toolStripArduinoStatus.Text = "Arduino: " + statusStr;
        }

        protected void doDeviceInitialization()
        {
            // Initialize the device
            mOceanFX.Initialize();

            SpectrumProcessor.Instance.Pixels = new int[mOceanFX.NumPixels];
            SpectrumProcessor.Instance.Wavelengths = new double[mOceanFX.NumPixels];

            for (int i = 0; i < mOceanFX.NumPixels; i++)
            {
                SpectrumProcessor.Instance.Pixels[i] = i;
                SpectrumProcessor.Instance.Wavelengths[i] = applyCoefficients(mOceanFX.WavecalCoefficients, i);
            }

            // Override some of the settings with the stored values
            mOceanFX.IntegrationTime = SettingsManager.IntegrationTime;
            mOceanFX.BackToBackPerTrigger = SettingsManager.BackToBackPerTrigger;
            mOceanFX.NumSpectraPerRequest = SettingsManager.NumSpectraPerRequest;
            mOceanFX.BufferEnabled = SettingsManager.BufferEnabled;

            // Make sure the save ranges make sense
            if (SettingsManager.SaveEndPixel == 0)
            {
                SettingsManager.SaveEndPixel = SpectrumProcessor.Instance.Wavelengths.Length - 1;
                SettingsManager.SaveEndWavelength = SpectrumProcessor.Instance.Wavelengths[SettingsManager.SaveEndPixel];
            }

            Invoke((MethodInvoker)delegate
            {
                OnDeviceInitComplete();  // Invoke on UI thread
                State.Instance.IsInit = false;
            });
        }

        protected void allocateIOBuffers()
        {
            mSupplyQueue.Clear();
            mComputeQueue.Clear();
            mSaveQueue.Clear();
            for (int i = 0; i < SpectrumConstants.NUM_RESPONSES_TO_ALLOCATE; i++)
            {
                mSupplyQueue.Enqueue(mOceanFX.GetRawSpectrumWithMetadata());
            }
        }

        protected void OnDeviceInitComplete()
        {
            UpdateUIControls();

            // Update the range fields on SavingParameters
            WavelengthRangeOptions.Instance.UpdateFileSaveUnits();
        }

        // Applies the specified coefficients to the specified value
        protected double applyCoefficients(double[] mCoeffs, int val)
        {
            double result = 0.0;
            int iMult = 1;
            for (int i = 0; i < mCoeffs.Length; i++)
            {
                result += mCoeffs[i] * iMult;
                iMult *= val;
            }

            return result;
        }

        #endregion device_connection

        #region ui_timer_and_updates

        //========================= ui_timer_and_updates =========================

        protected void UpdateMenusAndToolbarFromSettings()
        {
            // Update the menus
            enabledFilteringToolStripMenuItem.Checked = SettingsManager.SpectrumFilteringEnabled;

            enabledTriggeringToolStripMenuItem.Checked = SettingsManager.SpectrumThresholdingEnabled;

            enableSaveToFileToolStripMenuItem.Checked = SettingsManager.SaveToFile;

            // Update the status bar
            if (SettingsManager.SpectrumFilteringEnabled == true)
            {
                toolStripStatusFilteringLabel.Text = "Filtering: On";
            }
            else
            {
                toolStripStatusFilteringLabel.Text = "Filtering: Off";
            }

            if (SettingsManager.SpectrumThresholdingEnabled == true)
            {
                toolStripStatusThresholdingLabel.Text = "Triggering: On";
            }
            else
            {
                toolStripStatusThresholdingLabel.Text = "Triggering: Off";
            }

            if (SettingsManager.SaveToFile == true)
            {
                toolStripStatusSavingLabel.Text = "Saving to file: On";
            }
            else
            {
                toolStripStatusSavingLabel.Text = "Saving to file: Off";
            }

            // Plot thresholds?
            showThresholdsToolStripMenuItem.Checked = SettingsManager.PlotThresholds;

            // Plot trigger points?
            showTriggerPointsToolStripMenuItem.Checked = SettingsManager.PlotTriggerPoints;
        }

        protected void UpdateUIControls()
        {
            string fwv = mOceanFX == null ? "" : mOceanFX.FWVersion;
            string fws = mOceanFX == null ? "" : mOceanFX.FWSubversion;
            string fpga = mOceanFX == null ? "" : mOceanFX.FPGAVersion;
            string sn = mOceanFX == null ? "" : mOceanFX.SerialNum;

            // Update the hardware info
            acquisitionParametersControl.SetHardwareInfoFields(fwv, fws, fpga, sn);

            WavelengthRangeOptions.Instance.UpdateFileSaveUnits();

            if (mOceanFX != null && mOceanFX.ContinuousStrobeEnabled != 0)
            {
                SettingsManager.ContinuousStrobePulseEnabled = true;
            }
            else
            {
                SettingsManager.ContinuousStrobePulseEnabled = false;
            }
            if (mOceanFX != null && mOceanFX.SingleStrobeEnabled != 0)
            {
                SettingsManager.SingleStrobePulseEnabled = true;
            }
            else
            {
                SettingsManager.SingleStrobePulseEnabled = false;
            }

            if (mOceanFX != null && mOceanFX.LampEnable != 0)
            {
                checkBoxLampEnableDark.Checked = true;
            }
            else
            {
                checkBoxLampEnableDark.Checked = false;
            }

            textBoxNumInBuffer.Text = mOceanFX == null ? "" : mOceanFX.NumSpectraInBuffer.ToString(CultureInfo.InvariantCulture);

            textBoxAcquireFor.Text = SettingsManager.StaticReferenceAccumulateTime.ToString(CultureInfo.InvariantCulture);

            if (SettingsManager.NumberOfSpectraForDynamicAccumulation <= 1)
            {
                SettingsManager.NumberOfSpectraForDynamicAccumulation = 1;
            }
            if (SettingsManager.IntervalForDynamicAccumulation <= 1)
            {
                SettingsManager.IntervalForDynamicAccumulation = 1;
            }
            if (SettingsManager.IntervalForDynamicAccumulation > SettingsManager.NumberOfSpectraForDynamicAccumulation)
            {
                SettingsManager.IntervalForDynamicAccumulation = (uint)SettingsManager.NumberOfSpectraForDynamicAccumulation;
            }
            textBoxNumberOfSpectramToAccumulate.Text = SettingsManager.NumberOfSpectraForDynamicAccumulation.ToString(CultureInfo.InvariantCulture);
            textBoxSpectrumIntervalBetweenGeneration.Text = SettingsManager.IntervalForDynamicAccumulation.ToString(CultureInfo.InvariantCulture);

            enabledTriggeringToolStripMenuItem.Checked = SettingsManager.SpectrumThresholdingEnabled;

            bool bActiveIO = (mOceanFX != null && mOceanFX.IsConnected);

            buttonStartAcquisition.Enabled = bActiveIO;
            buttonStartAcquisition.BackColor = bActiveIO ? Color.ForestGreen : Color.Gray;
            buttonUpdateSpectraInBuffer.Enabled = bActiveIO;
            buttonClearBuffer.Enabled = bActiveIO;
            buttonTakeReference.Enabled = bActiveIO;
            buttonClearReference.Enabled = bActiveIO;
            buttonTakeDark.Enabled = bActiveIO;
            buttonClearDark.Enabled = bActiveIO;
            buttonAccumulateSpectra.Enabled = bActiveIO;
            buttonAbortAccumulateSpectra.Enabled = !bActiveIO;

            startToolStripMenuItem.Enabled = bActiveIO;

            processToolStripMenuItem.Enabled = bActiveIO;
            plotToolStripMenuItem.Enabled = bActiveIO;
            statusStripToolbar.Enabled = bActiveIO;
            wavelengthsToolStripMenuItem.Enabled = bActiveIO;

            acquisitionParametersControl.Enabled = bActiveIO;
            tabControlAcquisition.Enabled = bActiveIO;

            WavelengthRangeOptions.Instance.ToggleEditing(bActiveIO);
        }

        #endregion ui_timer_and_updates

        #region ui_control_handlers

        //========================= ui_control_handlers =========================

        // ----- Update the number of spectra in the buffer -----

        private void buttonUpdateSpectraInBuffer_Click(object sender, EventArgs e)
        {
            buttonUpdateSpectraInBuffer.Enabled = false;

            Thread ioThread = new Thread(() => doUpdateSpectraInBuffer());
            ioThread.Start();
        }

        private void doUpdateSpectraInBuffer()
        {
            if (mOceanFX == null || !mOceanFX.IsConnected)
            {
                return;
            }

            mOceanFX.GetNumberOfSpectraInDeviceBuffer();

            Invoke((MethodInvoker)delegate
            {
                onUpdateSpectraInBuffer(); // invoke on UI thread
            });
        }

        private void onUpdateSpectraInBuffer()
        {
            if (mOceanFX == null || !mOceanFX.IsConnected)
            {
                textBoxNumInBuffer.Text = "0";
                buttonUpdateSpectraInBuffer.Enabled = false;
                buttonClearBuffer.Enabled = false;
                return;
            }

            textBoxNumInBuffer.Text = mOceanFX.NumSpectraInBuffer.ToString(CultureInfo.InvariantCulture);
            buttonUpdateSpectraInBuffer.Enabled = true;
            buttonClearBuffer.Enabled = true;
        }

        // ----- Clear the buffer -----

        private void buttonClearBuffer_Click(object sender, EventArgs e)
        {
            buttonClearBuffer.Enabled = false;

            Thread ioThread = new Thread(() => doClearBuffer());
            ioThread.Start();
        }

        private void doClearBuffer()
        {
            if (mOceanFX == null || !mOceanFX.IsConnected)
            {
                return;
            }

            mOceanFX.ClearBufferOnDevice();

            doUpdateSpectraInBuffer();
        }

        private void checkBoxClearBufferBeforeAcquisition_CheckedChanged(object sender, EventArgs e)
        {
            mClearBufferBeforeTest = checkBoxClearBufferBeforeAcquisition.Checked;
        }

        #endregion ui_control_handlers

        #region spectrometer_io

        //========================= spectrometer_io =========================

        //
        // Update setting variables from the current UI settings
        //
        // Note: Some variables associated with checkboxes and drop down lists
        //       are updated by event handlers and don't need to be updated here.
        //
        protected void syncUISettings(bool bSingleSpectrum = false)
        {
            //
            // Sync settings from the UI controls
            //

            if (mOceanFX == null || !mOceanFX.IsConnected)
            {
                return;
            }

            mOceanFX.ContinuousStrobeEnabled = SettingsManager.ContinuousStrobePulseEnabled ? (byte)1 : (byte)0;
            mOceanFX.ContinuousStrobePeriod = SettingsManager.ContinuousStrobePulsePeriod;
            mOceanFX.ContinuousStrobeWidth = SettingsManager.ContinuousStrobePulseWidth;

            mOceanFX.SingleStrobeEnabled = SettingsManager.SingleStrobePulseEnabled ? (byte)1 : (byte)0;
            mOceanFX.SingleStrobePulseDelay = SettingsManager.SingleStrobePulseDelay;
            mOceanFX.SingleStrobePulseWidth = SettingsManager.SingleStrobePulseWidth;

            mOceanFX.IntegrationTime = SettingsManager.IntegrationTime;
            mOceanFX.ScansToAverage = SettingsManager.ScansToAverage;
            mOceanFX.BackToBackPerTrigger = SettingsManager.BackToBackPerTrigger;
            mOceanFX.NumSpectraPerRequest = SettingsManager.NumSpectraPerRequest;
            mOceanFX.AcquisitionDelay = SettingsManager.AcquisitionDelay;

            if (SettingsManager.TriggerMode > 5)
            {
                mOceanFX.TriggerMode = 0xFF;
            }
            else
            {
                mOceanFX.TriggerMode = SettingsManager.TriggerMode;
            }

            mOceanFX.BufferEnabled = SettingsManager.BufferEnabled;
        }

        private void doSyncSpectrometerSettings(bool bSingleSpectrum = false)
        {
            if (mOceanFX == null || !mOceanFX.IsConnected)
            {
                return;
            }

            mOceanFX.Sync(bSingleSpectrum);

            if (!State.Instance.IsClosing)
            {
                Invoke((MethodInvoker)delegate
                {
                    OnSettingsSync();  // Invoke on UI thread
                });
            }
        }

        private void OnSettingsSync()
        {
            // nothing to do for now
        }

        private void buttonTakeDark_Click(object sender, EventArgs e)
        {
            buttonTakeDark.Enabled = false;

            takeSingleSpectrum(Options.InputSpectrumType.DARK);
        }

        private void buttonClearDark_Click(object sender, EventArgs e)
        {
            clearDarkSpectrum();
        }

        private void clearDarkSpectrum()
        {
            labelDarkStatus.Visible = false;
            SpectrumProcessor.Instance.DarkSpectrum = null;
            SpectrumProcessor.Instance.ReferenceCorrectedSpectrum = null;

            // This will clear the plot
            this.mMainPlotter.Plot();

            // Update corrected reference spectrum field
            labelCorrectedReferenceSpectrumStatus.Text = "Not available.";
        }

        private void buttonTakeReference_Click(object sender, EventArgs e)
        {
            buttonTakeReference.Enabled = false;

            takeSingleSpectrum(Options.InputSpectrumType.REFERENCE);
        }

        private void buttonClearReference_Click(object sender, EventArgs e)
        {
            clearReferenceSpectrum();
        }

        private void clearReferenceSpectrum()
        {
            labelReferenceStatus.Visible = false;
            SpectrumProcessor.Instance.ReferenceSpectrum = null;
            SpectrumProcessor.Instance.ReferenceCorrectedSpectrum = null;

            // This will clear the plot
            this.mMainPlotter.Plot();

            // Update corrected reference spectrum field
            labelCorrectedReferenceSpectrumStatus.Text = "Not available.";
        }

        private void takeSingleSpectrum(Options.InputSpectrumType spectrumType)
        {
            Thread ioThread = new Thread(() => doTakeSingleSpectrum(spectrumType));
            ioThread.Start();
        }

        private void doTakeSingleSpectrum(Options.InputSpectrumType spectrumType)
        {
            if (mOceanFX == null)
            {
                return;
            }

            doSyncSpectrometerSettings(true);

            OBPGetCorrectedSpectrum correctedSpectrum = mOceanFX.GetCorrectedSpectrum();
            if (correctedSpectrum.IsSuccess)
            {

                // Filter?
                if (SettingsManager.SpectrumFilteringEnabled && !SpectrumFilterer.Instance.IsUnit)
                {
                    ushort[] spectrum = correctedSpectrum.CorrectedSpectrum;
                    spectrum = SpectrumFilterer.Instance.Convolve(spectrum, full: false, symmetric: true);
                    for (int i = 0; i < correctedSpectrum.CorrectedSpectrum.Length; i++)
                    {
                        correctedSpectrum.CorrectedSpectrum[i] = spectrum[i];
                    }
                }

                // Clear the latest result spectrum
                SpectrumProcessor.Instance.ResultSpectrum = null;

                if (spectrumType == Options.InputSpectrumType.DARK)
                {
                    SpectrumProcessor.Instance.DarkSpectrum = correctedSpectrum.CorrectedSpectrum;
                }
                else if (spectrumType == Options.InputSpectrumType.REFERENCE)
                {
                    SpectrumProcessor.Instance.ReferenceSpectrum = correctedSpectrum.CorrectedSpectrum;
                }

                // If both a dark and reference are available, the corrected reference spectrum can be calculated
                SpectrumProcessor.Instance.ComputeReferenceCorrectedSpectrumIfPossible();
            }

            Invoke((MethodInvoker)delegate
            {
                OnSingleSpectrumComplete(spectrumType);  // Invoke on UI thread
            });
        }

        private void OnSingleSpectrumComplete(Options.InputSpectrumType spectrumType)
        {
            if (spectrumType == Options.InputSpectrumType.DARK)
            {
                buttonTakeDark.Enabled = true;

                SpectrumProcessor.Instance.DarkSpectrumTime = DateTime.Now;

                labelDarkStatus.Text = String.Format(CultureInfo.InvariantCulture, "00:00:00");
                labelDarkStatus.Visible = true;

                // Set current plot (this forces a redraw)
                SettingsManager.CurrentPlotType = Options.PlotType.DARK_SPECTRUM;
            }
            else if (spectrumType == Options.InputSpectrumType.REFERENCE)
            {
                buttonTakeReference.Enabled = true;

                SpectrumProcessor.Instance.ReferenceSpectrumTime = DateTime.Now;

                labelReferenceStatus.Text = String.Format(CultureInfo.InvariantCulture, "00:00:00");
                labelReferenceStatus.Visible = true;

                // Set current plot (this forces a redraw)
                SettingsManager.CurrentPlotType = Options.PlotType.REFERENCE_SPECTRUM;

                if (SpectrumProcessor.Instance.ReferenceCorrectedSpectrum != null)
                {
                    // Set current plot (this forces a redraw)
                    SettingsManager.CurrentPlotType = Options.PlotType.CORRECTED_REFERENCE_SPECTRUM;
                }
            }

            // Update corrected reference spectrum field
            if (SpectrumProcessor.Instance.ReferenceCorrectedSpectrum != null)
            {
                labelCorrectedReferenceSpectrumStatus.Text = "Available.";

                SettingsManager.ReferenceType = Options.ReferenceType.STATIC_SINGLE;
            }
            else
            {
                labelCorrectedReferenceSpectrumStatus.Text = "Not available.";
            }
        }

        private void comboBoxSaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //updateSaveTypeComment();
            acquisitionParametersControl.UpdateSaveFilename();
        }

        private void checkBoxLampEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (mOceanFX == null)
            {
                return;
            }

            mOceanFX.LampEnable = checkBoxLampEnableDark.Checked ? (byte)1 : (byte)0;

            SettingsManager.LampEnable = mOceanFX.LampEnable;

            Thread lampThread = new Thread(() => doLampEnable());
            lampThread.Start();
        }

        //
        // Update the lamp enable state immediately (don't wait for an acquisition to start)
        //
        private void doLampEnable()
        {
            if (mOceanFX == null)
            {
                return;
            }

            mOceanFX.EnableLamp();
        }

        private void updateAcquisitionButtonAndMenuState()
        {
            bool bActiveIO = (mOceanFX != null && mOceanFX.IsConnected);

            buttonStartAcquisition.Enabled = bActiveIO && !State.Instance.IsPerformingStandardAcquisition;
            buttonStartAcquisition.BackColor = buttonStartAcquisition.Enabled ? Color.ForestGreen : Color.Gray;

            startToolStripMenuItem.Enabled = buttonStartAcquisition.Enabled;

            buttonAbortAcquisition.Enabled = bActiveIO && State.Instance.IsPerformingStandardAcquisition;
            buttonAbortAcquisition.BackColor = buttonAbortAcquisition.Enabled ? Color.Maroon : Color.Gray;

            abortToolStripMenuItem.Enabled = buttonAbortAcquisition.Enabled;

            buttonTakeDark.Enabled = !State.Instance.IsPerformingStandardAcquisition;
            buttonClearDark.Enabled = !State.Instance.IsPerformingStandardAcquisition;
            buttonTakeReference.Enabled = !State.Instance.IsPerformingStandardAcquisition;
            buttonClearReference.Enabled = !State.Instance.IsPerformingStandardAcquisition;
            buttonUpdateSpectraInBuffer.Enabled = !State.Instance.IsPerformingStandardAcquisition;
            buttonUpdateSpectraInBuffer.Enabled = !State.Instance.IsPerformingStandardAcquisition;
            buttonClearBuffer.Enabled = !State.Instance.IsPerformingStandardAcquisition;
            buttonAccumulateSpectra.Enabled = !State.Instance.IsPerformingStandardAcquisition;
            buttonAbortAccumulateSpectra.Enabled = State.Instance.IsPerformingStandardAcquisition;

            toolStripStatusOutput.Enabled = !State.Instance.IsPerformingStandardAcquisition;
            comboBoxAcquisitionOutput.Enabled = !State.Instance.IsPerformingStandardAcquisition;
            outputToolStripMenuItem.Enabled = !State.Instance.IsPerformingStandardAcquisition;

            // Update Save menu and toolstrip
            if (State.Instance.IsPerformingStandardAcquisition)
            {
                if (SettingsManager.SaveToFile)
                {
                    toolStripStatusSavingLabel.Text = "Pause saving";
                }
                else
                {
                    toolStripStatusSavingLabel.Text = "Saving to file: Off";
                }
            }
            else
            {
                if (SettingsManager.SaveToFile)
                {
                    toolStripStatusSavingLabel.Text = "Saving to file: On";
                }
                else
                {
                    toolStripStatusSavingLabel.Text = "Saving to file: Off";
                }
            }
              
            WavelengthRangeOptions.Instance.ToggleEditing(!State.Instance.IsPerformingStandardAcquisition);
            acquisitionParametersControl.Enabled = !State.Instance.IsPerformingStandardAcquisition;

            if (State.Instance.IsPerformingStandardAcquisition)
            {
                this.labelCurSpectraPerSec.Text = "0 spectra/sec";
                labelFileSaveError.Visible = false;
            }

            // Hardware scan and connection buttons
            this.ToggleHardwareConnectionButtons(!State.Instance.IsPerformingStandardAcquisition);

        }

        #endregion spectrometer_io

        #region high_speed_acquisition

        //========================= high_speed_acquisition =========================

        private void buttonStartAcquisition_Click(object sender, EventArgs e)
        {
            StartAcquisition();
        }

        private void StartAcquisition()
        {
            if (SettingsManager.SpectrumThresholdingEnabled &&
                !mArduino.IsConnected())
            {
                MessageBox.Show(
                    "Triggering requires a connection to Arduino!",
                    "Info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            if (SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.DARK_CORRECTED &&
                SpectrumProcessor.Instance.DarkSpectrum == null)
            {
                MessageBox.Show(
                    "Please acquire a dark spectrum!",
                    "Info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if ((
                SettingsManager.ReferenceType == Options.ReferenceType.STATIC ||
                SettingsManager.ReferenceType == Options.ReferenceType.STATIC_SINGLE ||
                SettingsManager.ReferenceType == Options.ReferenceType.STATIC_ACCUMULATED) &&
                SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.ABSORBANCE &&
                SpectrumProcessor.Instance.ReferenceCorrectedSpectrum == null)
            {
                MessageBox.Show(
                    "Absorbance measurement requires a corrected reference spectrum!",
                    "Info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if ((
                SettingsManager.ReferenceType == Options.ReferenceType.STATIC ||
                SettingsManager.ReferenceType == Options.ReferenceType.STATIC_SINGLE ||
                SettingsManager.ReferenceType == Options.ReferenceType.STATIC_ACCUMULATED) &&
                SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.TRANSMISSION &&
                SpectrumProcessor.Instance.ReferenceCorrectedSpectrum == null)
            {
                MessageBox.Show(
                    "Transmission measurement requires a corrected reference spectrum!",
                    "Info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (SettingsManager.ReferenceType == Options.ReferenceType.DYNAMIC &&
                (
                SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.ABSORBANCE ||
                SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.TRANSMISSION
                ) &&
                SpectrumProcessor.Instance.DarkSpectrum == null)
            {
                MessageBox.Show(
                    "Dynamic reference requires a dark spectrum!",
                    "Info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Clear the plots
            this.mMainPlotter.Clear();

            // Set state flags
            State.Instance.IsPerformingStandardAcquisition = true;
            State.Instance.IsStopAcquiringRequested = false;

            // If needed, update the file name
            if (SettingsManager.SaveFileNameAutoUpdate == true)
            {
                this.acquisitionParametersControl.UpdateSaveFilename();
            }

            // Raise the event
            MainWindow.OnAcquisitionStarted(null, new EventArgs());

            // Update state of buttons and menus
            updateAcquisitionButtonAndMenuState();

            // Update the pixel range to be saved to file
            WavelengthRangeOptions.Instance.UpdateFileSaveUnits();

            // Ensure all the setting variables are sync'd to the latest UI state
            // (the UI controls can't be accessed directly from a non-UI thread)
            syncUISettings();

            // Set up acquisition
            if (SettingsManager.ReferenceType == Options.ReferenceType.NONE ||
                SettingsManager.ReferenceType == Options.ReferenceType.STATIC ||
                SettingsManager.ReferenceType == Options.ReferenceType.STATIC_SINGLE ||
                SettingsManager.ReferenceType == Options.ReferenceType.STATIC_ACCUMULATED)
            {
                Experiment.SetupStandardAcquisition();
            }
            else if (SettingsManager.ReferenceType == Options.ReferenceType.DYNAMIC)
            {
                Experiment.SetupAcquisitionWithDynamicReference();
            }
            else
            {
                throw new Exception("Unexpected reference type for acquisition!");
            }

            // Clear the plot
            this.mMainPlotter.Clear();

            // Reset counters
            mTotalSpectraReceived = 0;
            mTotalSpectraComputed = 0;
            mTotalSpectraSaved = 0;
            mTotalSavedBytes = 0;

            // Perform the save spectrum in a new thread
            Thread saveThread = new Thread(() => doSaveSpectrum());
            saveThread.Start();

            // Perform compute and save spectrum in a new thread
            Thread computeThread = new Thread(() => doComputeSpectrum());
            computeThread.Start();

            // Perform the acquisition in a new thread
            Thread acquisitionThread = new Thread(() => doHighSpeedAcquisition());
            acquisitionThread.Start();
        }

        private void ToggleHardwareConnectionButtons(bool enabled)
        {
            buttonUSBRescan.Enabled = enabled;
            buttonUSBConnect.Enabled = enabled;
            buttonUSBDisconnect.Enabled = enabled;
            buttonIPRescan.Enabled = enabled;
            buttonIPConnect.Enabled = enabled;
            buttonIPDisconnect.Enabled = enabled;
            buttonRescanArduino.Enabled = enabled;
            buttonConnectToArduino.Enabled = enabled;
            buttonDisconnectFromArduino.Enabled = enabled;
        }

        private void AbortAcquisition()
        {
            State.Instance.IsStopAcquiringRequested = true;
        }

        private void buttonAbortAcquisition_Click(object sender, EventArgs e)
        {
            AbortAcquisition();
        }

        private void onAcquisitionComplete()
        {
            State.Instance.IsPerformingStandardAcquisition = false;

            // Raise the event
            MainWindow.OnAcquisitionCompleted(null, new EventArgs());

            // Persist the acquisition settings
            //SettingsManager.Save();

            // Store the acquisition settings
            string filename = Path.Combine(SettingsManager.SaveDirectory, SettingsManager.SaveFileName.Replace(".csv", "_settings.xml"));
            SettingsWriter.Save(filename);

            updateAcquisitionButtonAndMenuState();
        }

        /// <summary>
        /// Complete the acquisition of accumulated spectra for static reference 
        /// generation.
        /// </summary>
        private void onAcquisitionForAccumulationComplete()
        {
            State.Instance.IsPerformingStandardAcquisition = false;
            State.Instance.IsPerformingAccumulationAcquisition = false;

            // Raise the event
            MainWindow.OnAcquisitionCompleted(null, new EventArgs());

            // Update the min and max fields
            SpectrumProcessor.Instance.StaticAccumulatedSpectraStartIndex = 0;
            SpectrumProcessor.Instance.StaticAccumulatedSpectraEndIndex = SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count - 1;
            updateAcquisitionButtonAndMenuState();

            // Update the slider max value
            trackBarAccumulateSpectraSlider.Minimum = 0;
            trackBarAccumulateSpectraSlider.Maximum = SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count - 1;

            // Enable the accumulated spectra slider view
            trackBarAccumulateSpectraSlider.Enabled = true;
            labelAccumulateSpectraSlider.Visible = true;

            // Enable the 'Generate' groupbox
            groupBoxGenerateStaticReferenceSpectrum.Enabled = true;

            // Set the min and max values into the generate spectrum range
            textBoxEstimateReferenceSpectrumMin.Text = SpectrumProcessor.Instance.StaticAccumulatedSpectraStartIndex.ToString(CultureInfo.InvariantCulture);
            textBoxEstimateReferenceSpectrumMax.Text = SpectrumProcessor.Instance.StaticAccumulatedSpectraEndIndex.ToString(CultureInfo.InvariantCulture);

            // Enable the generation groupbox
            groupBoxGenerateStaticReferenceSpectrum.Enabled = true;

            // Switch to the ACCUMULATED_SPECTRA plot type and plot
            SettingsManager.CurrentPlotType = Options.PlotType.ACCUMULATED_SPECTRA;
            mMainPlotter.Plot();

            // Restore the enabled state of the plotting options
            spectrumToolStripMenuItem.Enabled = true;
            timeSeriesToolStripMenuItem.Enabled = true;
            darkSpectrumToolStripMenuItem.Enabled = true;
            referenceSpectrumToolStripMenuItem.Enabled = true;
            correctedReferenceSpectrumToolStripMenuItem.Enabled = true;
            accumulatedSpectraToolStripMenuItem.Enabled = true;
            accumulatedTimeSeriesToolStripMenuItem.Enabled = true;
        }


        private void receiveNextSpectrum(OBPGetRawSpectrumWithMetadata spectrumIn)
        {
            // Receive the next spectrum response
            spectrumIn.ReceiveOnly();
            if (!spectrumIn.IsSuccess)
            {
                mReceiveFailures++; // Note failures for info purposes... some are to be expected based on load

                mTotalReceivedBytes += 64;  // Assume the failed response was received (this seems to be the case with OBP error code 13)

                // Send immediately back to the supply queue
                lock (mSupplyQueue)
                {
                    mSupplyQueue.Enqueue(spectrumIn);
                }
            }
            else
            {
                // Track the total bytes received
                mTotalReceivedBytes += spectrumIn.Response.BytesRemaining + 44;

                // Queue for the compute and save thread
                lock (mComputeQueue)
                {
                    mComputeQueue.Enqueue(spectrumIn);
                }
            }
        }


        //
        // Append a result to the save file
        //
        // Notes:
        // 1. This method seems to be a bottleneck.  The float.ToString() conversions are MUCH slower than int.ToString().
        //    (which is why there are both a float and int overload of this method)
        // 2. An async version of this method was tried with outFile.WriteLineAsync() and .Net 4.5.
        //    The async implementation didn't seem to make a very big difference.
        // 3. It seems like the best way to improve performance would be to save as binary data.
        //    (or scale up the float data and save as int)
        // 4. Release builds perform better than Debug builds
        //
        protected void SaveSpectrum(StringBuilder sb, RawSpectrumWithMetadataBuffer spectrum, float[] curResult, StreamWriter outFile, bool triggered, int wavelengthStep)
        {
            // Write the metadata
            sb.Append(spectrum.SpectrumCounter).Append(";");
            sb.Append(spectrum.MicrosecondCounter).Append(";");
            sb.Append(spectrum.IntegrationTime).Append(";");
            sb.Append(spectrum.ScansAveraged == 0 ? 1 : spectrum.ScansAveraged).Append(";");
            sb.Append(triggered);

            // Write the data
            for (int i = SettingsManager.SaveStartPixel; i <= SettingsManager.SaveEndPixel; i+=wavelengthStep)
            {
                sb.Append(";").Append(curResult[i].ToString(CultureInfo.InvariantCulture));
            }

            outFile.WriteLine(sb.ToString());
            sb.Clear();
        }

        protected void SaveSpectrumSelectedWavelengthsOnly(StringBuilder sb, RawSpectrumWithMetadataBuffer spectrum, float[] curResult, StreamWriter outFile, bool triggered)
        {
            // Write the metadata
            sb.Append(spectrum.SpectrumCounter).Append(";");
            sb.Append(spectrum.MicrosecondCounter).Append(";");
            sb.Append(spectrum.IntegrationTime).Append(";");
            sb.Append(spectrum.ScansAveraged == 0 ? 1 : spectrum.ScansAveraged).Append(";");
            sb.Append(triggered);

            // Cache the query
            List<Wavelength> wavelengthsToSave = WavelengthManager.Instance.Wavelengths;

            // Write only data for threshold wavelengths
            for (int i = 0; i < wavelengthsToSave.Count; i++)
            {
                if (wavelengthsToSave[i].IsToBeSaved)
                {

                    int index = wavelengthsToSave[i].Index;
                    sb.Append(";").Append(curResult[index].ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    sb.Append(";").Append("NaN");
                }
            }

            outFile.WriteLine(sb.ToString());
            sb.Clear();
        }

        //
        // Append a result to the save file
        //
        protected void SaveSpectrum(StringBuilder sb, RawSpectrumWithMetadataBuffer spectrum, int[] curResult, StreamWriter outFile, bool triggered, int wavelengthStep)
        {
            // Write the metadata
            sb.Append(spectrum.SpectrumCounter).Append(";");
            sb.Append(spectrum.MicrosecondCounter).Append(";");
            sb.Append(spectrum.IntegrationTime).Append(";");
            sb.Append(spectrum.ScansAveraged == 0 ? 1 : spectrum.ScansAveraged).Append(";");
            sb.Append(triggered);

            // Write the data
            for (int i = SettingsManager.SaveStartPixel; i <= SettingsManager.SaveEndPixel; i+=wavelengthStep)
            {
                sb.Append(";").Append(curResult[i].ToString(CultureInfo.InvariantCulture));
            }

            outFile.WriteLine(sb.ToString());
            sb.Clear();
        }

        //
        // Append a result to the save file
        //
        protected void SaveSpectrumSelectedWavelengthsOnly(StringBuilder sb, RawSpectrumWithMetadataBuffer spectrum, int[] curResult, StreamWriter outFile, bool triggered)
        {
            // Write the metadata
            sb.Append(spectrum.SpectrumCounter).Append(";");
            sb.Append(spectrum.MicrosecondCounter).Append(";");
            sb.Append(spectrum.IntegrationTime).Append(";");
            sb.Append(spectrum.ScansAveraged == 0 ? 1 : spectrum.ScansAveraged).Append(";");
            sb.Append(triggered);

            // Cache the query
            List<Wavelength> wavelengthsToSave = WavelengthManager.Instance.Wavelengths;

            // Write only data for wavelengths to be flagged for saving
            for (int i = 0; i < wavelengthsToSave.Count; i++)
            {
                if (wavelengthsToSave[i].IsToBeSaved)
                {
                    int index = wavelengthsToSave[i].Index;
                    sb.Append(";").Append(curResult[index]);
                }
                else
                {
                    sb.Append(";").Append("NaN");
                }
            }

            outFile.WriteLine(sb.ToString());
            sb.Clear();
        }

        private void onAcquisitionError(string errorMessage)
        {
            labelFileSaveError.Visible = true;
            labelFileSaveError.Text = errorMessage;
        }

        #endregion high_speed_acquisition

        /// <summary>
        /// Trigger Arduino. The delay is applied by the SendCommand() call.
        /// </summary>
        private void triggerArduino()
        {
            mArduino.SendCommand(Arduino.COMMANDS.TRIGGER);
        }

        private void buttonRescanArduino_Click(object sender, EventArgs e)
        {
            buttonRescanArduino.Enabled = false;

            doFindCOMDevices();
        }

        private void buttonConnectToArduino_Click(object sender, EventArgs e)
        {
            connectToSelectedCOMDevice();
        }

        private void connectToSelectedCOMDevice()
        {
            if (dataGridViewArduinoDevices.SelectedRows.Count > 0)
            {
                SerialPortWrapper device = (SerialPortWrapper)(dataGridViewArduinoDevices.SelectedRows[0].Tag);

                if (device == null)
                {
                    SetCOMConnectError("Failed to connect!");
                }
                else
                {
                    Thread usbConnectThread = new Thread(() => doCOMConnect(device));
                    usbConnectThread.Start();
                }
            }
        }

        private void disconnectFromCOMDevice()
        {
            if (mArduino != null)
            {
                mArduino.Disconnect();

                // Disable the settings panel
                TabPageCollection pages = tabControlAcquisition.TabPages;
                Debug.Assert(pages[4].Name.Equals("tabPageArduino", StringComparison.Ordinal));
                ((Control)pages[4]).Enabled = false;
            }
        }

        private void buttonDisconnectFromArduino_Click(object sender, EventArgs e)
        {
            // Disconnect
            disconnectFromCOMDevice();

            // Clear label
            SetCOMConnectStatus("Disconnected");

            // Reset selection
            dataGridViewArduinoDevices.ClearSelection();

            // Toggle buttons
            buttonConnectToArduino.Enabled = true;
            buttonDisconnectFromArduino.Enabled = false;
        }

        // Handle the TrackBar.ValueChanged event by calculating a value for
        // TextBox1 based on the TrackBar value.
        private void trackBarAccumulateSpectraSlider_ValueChanged(object sender, System.EventArgs e)
        {
            labelAccumulateSpectraSlider.Text = trackBarAccumulateSpectraSlider.Value.ToString(CultureInfo.InvariantCulture);

            SpectrumProcessor.Instance.SelectedStaticAccumulatedSpectrumNumber = trackBarAccumulateSpectraSlider.Value;

            // Make sure the correct plot type is drawn
            if (SettingsManager.CurrentPlotType != Options.PlotType.ACCUMULATED_SPECTRA)
            {
                SettingsManager.CurrentPlotType = Options.PlotType.ACCUMULATED_SPECTRA;
            }
            this.mMainPlotter.Plot();
        }

        private void ButtonUSBDisconnect_Click(object sender, EventArgs e)
        {
            if (mOceanFX != null)
            {
                closeDeviceConnection(true);
            }

            buttonUSBConnect.Enabled = true;
            buttonUSBDisconnect.Enabled = false;

            SetUSBConnectStatus("Disconnected");
            //updateDefaultSaveType();
        }

        #region menu_callbacks

        /// <summary>
        /// Sets the plot type to Options.PlotType.OUTPUT.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpectrumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the new plot type
            SettingsManager.CurrentPlotType = Options.PlotType.OUTPUT;
        }

        /// <summary>
        /// Sets the plot type to Options.PlotType.TIMESERIES.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the new plot type
            SettingsManager.CurrentPlotType = Options.PlotType.TIMESERIES;
        }

        /// <summary>
        /// Sets the plot type to Options.PlotType.ACCUMULATED_SPECTRA.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccumulatedSpectraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the new plot type
            SettingsManager.CurrentPlotType = Options.PlotType.ACCUMULATED_SPECTRA;
        }

        /// <summary>
        /// Sets the plot type to Options.PlotType.REFERENCE_SPECTRUM.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReferenceSpectrumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the new plot type
            SettingsManager.CurrentPlotType = Options.PlotType.REFERENCE_SPECTRUM;
        }

        /// <summary>
        /// Sets the plot type to Options.PlotType.ACCUMULATED_TIMESERIES.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccumulatedTimeSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the new plot type
            SettingsManager.CurrentPlotType = Options.PlotType.ACCUMULATED_TIMESERIES;
        }

        /// <summary>
        /// Sets the plot type to Options.PlotType.ACCUMULATING_TIMESERIES.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accumulatingTimeSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the new plot type
            SettingsManager.CurrentPlotType = Options.PlotType.ACCUMULATING_TIMESERIES;
        }

        /// <summary>
        /// Sets the plot type to Options.PlotType.ACCUMULATING_SPECTRUM.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void accumulatingSpectraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the new plot type
            SettingsManager.CurrentPlotType = Options.PlotType.ACCUMULATING_SPECTRUM;
        }

        private void YAutoScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Toggle
            this.autoScaleYAxisToolStripMenuItem.Checked = !this.autoScaleYAxisToolStripMenuItem.Checked;

            // Raise the event
            MainWindow.OnToggleYAxisAutoScale(null,
                new SingleBooleanEventArgs
                {
                    Enabled = this.autoScaleYAxisToolStripMenuItem.Checked
                });

            // Force redraw
            switch (SettingsManager.CurrentPlotType)
            {
                case (Options.PlotType.OUTPUT):
                    this.mMainPlotter.ResultCharted = null;
                    break;

                case (Options.PlotType.DARK_SPECTRUM):
                    this.mMainPlotter.DarkCharted = null;
                    break;

                case Options.PlotType.REFERENCE_SPECTRUM:
                    this.mMainPlotter.ReferenceCharted = null;
                    break;

                case Options.PlotType.CORRECTED_REFERENCE_SPECTRUM:
                    this.mMainPlotter.ReferenceCorrectedCharted = null;
                    break;

                default:
                    break;
            }

            // Refresh plot
            //mMainPlotter.Refresh();
            mMainPlotter.Plot();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Close the window to trigger the FormClosing event
            this.Close();
        }

        #endregion menu_callbacks

        // Rescan IP devices
        private void ButtonIPRescan_Click(object sender, EventArgs e)
        {
            buttonIPRescan.Enabled = false;
            FindIPDevices();
        }

        // Connect to selected IP device
        private void ButtonIPConnect_Click(object sender, EventArgs e)
        {
            connectToSelectedIPDevice();
        }

        // Disconnect from current IP device
        private void ButtonIPDisconnect_Click(object sender, EventArgs e)
        {
            if (mOceanFX != null)
            {
                closeDeviceConnection(true);
            }

            buttonIPConnect.Enabled = true;
            buttonIPDisconnect.Enabled = false;

            SetIPConnectStatus("Disconnected");
            //updateDefaultSaveType();
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WavelengthHub.Instance.Show();
            WavelengthHub.Instance.BringToFront();
        }

        private void SettingsThresholdingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WavelengthHub.Instance.Show();
            WavelengthHub.Instance.BringToFront();
        }

        private void SettingsToolStripMenuItem_Click_3(object sender, EventArgs e)
        {
            WavelengthHub.Instance.Show();
            WavelengthHub.Instance.BringToFront();
        }

        private void EstimateLiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Not implemented yet!");
        }

        private void AccumulateReferenceButton_Click(object sender, EventArgs e)
        {
            if (SpectrumProcessor.Instance.DarkSpectrum == null)
            {
                MessageBox.Show(
                    "Please acquire a dark spectrum first!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Make sure that the accumulation time is larger than 0 seconds
            if (SettingsManager.StaticReferenceAccumulateTime == 0)
            {
                MessageBox.Show(
                    "Please specify an accumulation time larger than 0.",
                    "Info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            // Set up accumulation experiment
            Experiment.SetupStaticReferenceEstimationExperiment();

            // Switch the output to accumulating spectrum
            SettingsManager.CurrentPlotType = Options.PlotType.ACCUMULATING_SPECTRUM;

            State.Instance.IsPerformingStandardAcquisition = false;
            State.Instance.IsStopAcquiringRequested = false;

            // Raise the event
            MainWindow.OnAcquisitionCompleted(null, new EventArgs());

            updateAcquisitionButtonAndMenuState();

            // Make sure to disable the 'Generate' groupbox
            groupBoxGenerateStaticReferenceSpectrum.Enabled = false;

            // Disable some of the plotting options
            spectrumToolStripMenuItem.Enabled = false;
            timeSeriesToolStripMenuItem.Enabled = false;
            darkSpectrumToolStripMenuItem.Enabled = false;
            referenceSpectrumToolStripMenuItem.Enabled = false;
            correctedReferenceSpectrumToolStripMenuItem.Enabled = false;
            accumulatedSpectraToolStripMenuItem.Enabled = false;
            accumulatedTimeSeriesToolStripMenuItem.Enabled = false;

            // Ensure all the setting variables are sync'd to the latest UI state
            // (the UI controls can't be accessed directly from a non-UI thread)
            syncUISettings();

            // Reset counters
            mTotalSpectraReceived = 0;
            mTotalSpectraComputed = 0;
            mTotalSpectraSaved = 0;
            mTotalSavedBytes = 0;

            // Disable the accumulate button
            buttonAccumulateSpectra.Enabled = false;
            buttonAbortAccumulateSpectra.Enabled = true;

            // Perform accumulate spectra in a new thread
            Thread accumulateThread = new Thread(() => doAccumulateSpectra());
            accumulateThread.Start();

            // Perform the acquisition in a new thread
            Thread acquisitionThread = new Thread(() => doHighSpeedAcquisition());
            acquisitionThread.Start();
        }

        private void EnableToolStripMenuItem_Click_3(object sender, EventArgs e)
        {
            SettingsManager.SaveToFile = !((ToolStripMenuItem)sender).Checked;
        }

        private void SettingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            WavelengthRangeOptions.Instance.Show();
            WavelengthRangeOptions.Instance.BringToFront();
        }

        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartAcquisition();
        }

        private void AbortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbortAcquisition();
        }

        private void FilteringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.SpectrumFilteringEnabled = !((ToolStripMenuItem)sender).Checked;
        }

        /// <summary>
        /// Do not use a reference spectrum.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoNotUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the new reference type. The event handler will set all relevant changes.
            SettingsManager.ReferenceType = Options.ReferenceType.NONE;
        }

        private void StaticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.ReferenceType = Options.ReferenceType.STATIC;
        }

        /// <summary>
        /// Use a dynamic, accumulated reference spectrum.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DynamicReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the new reference type. The event handler will set all relevant changes.
            SettingsManager.ReferenceType = Options.ReferenceType.DYNAMIC;
        }

        private void RawSpectrumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.ResultSpectrumType = Options.ResultSpectrumType.RAW_SPECTRUM;
        }

        private void DarkCorrectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.ResultSpectrumType = Options.ResultSpectrumType.DARK_CORRECTED;
        }

        private void AbsorbanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.ResultSpectrumType = Options.ResultSpectrumType.ABSORBANCE;
        }

        private void TransmissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.ResultSpectrumType = Options.ResultSpectrumType.TRANSMISSION;
        }

        private void DarkSpectrumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the new plot type
            SettingsManager.CurrentPlotType = Options.PlotType.DARK_SPECTRUM;
        }

        private void CorrectedReferenceSpectrumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Set the new plot type
            SettingsManager.CurrentPlotType = Options.PlotType.CORRECTED_REFERENCE_SPECTRUM;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.aboutDialog == null)
            {
                this.aboutDialog = new AboutDialog();
            }

            this.aboutDialog.Show();
            this.aboutDialog.BringToFront();
        }

        private void ShortcutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.shortcutsDialog == null)
            {
                this.shortcutsDialog = new ShortcutsDialog(
                    wavelengthHubToolStripMenuItem.ShortcutKeys,
                    enabledFilteringToolStripMenuItem.ShortcutKeys,
                    enabledTriggeringToolStripMenuItem.ShortcutKeys,
                    enableSaveToFileToolStripMenuItem.ShortcutKeys,
                    startToolStripMenuItem.ShortcutKeys,
                    abortToolStripMenuItem.ShortcutKeys,
                    spectrumToolStripMenuItem.ShortcutKeys,
                    timeSeriesToolStripMenuItem.ShortcutKeys,
                    darkSpectrumToolStripMenuItem.ShortcutKeys,
                    referenceSpectrumToolStripMenuItem.ShortcutKeys,
                    correctedReferenceSpectrumToolStripMenuItem.ShortcutKeys,
                    accumulatedSpectraToolStripMenuItem.ShortcutKeys,
                    accumulatedTimeSeriesToolStripMenuItem.ShortcutKeys,
                    showThresholdsToolStripMenuItem.ShortcutKeys,
                    showTriggerPointsToolStripMenuItem.ShortcutKeys,
                    autoScaleYAxisToolStripMenuItem.ShortcutKeys
                );
            }

            this.shortcutsDialog.Show();
            this.shortcutsDialog.BringToFront();
        }

        /// <summary>
        /// Toggle plot thresholds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowThresholdsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showThresholdsToolStripMenuItem.Checked = !showThresholdsToolStripMenuItem.Checked;
            SettingsManager.PlotThresholds = showThresholdsToolStripMenuItem.Checked;
        }

        private void OnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.SpectrumFilteringEnabled = true;
        }

        private void OffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.SpectrumFilteringEnabled = false;
        }

        private void OnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SettingsManager.SpectrumThresholdingEnabled = true;
        }

        private void OffToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SettingsManager.SpectrumThresholdingEnabled = false;
        }

        private void RawSpectrumToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SettingsManager.ResultSpectrumType = Options.ResultSpectrumType.RAW_SPECTRUM;
        }

        private void DarkCorrectedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SettingsManager.ResultSpectrumType = Options.ResultSpectrumType.DARK_CORRECTED;
        }

        private void AbsorbanceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SettingsManager.ResultSpectrumType = Options.ResultSpectrumType.ABSORBANCE;
        }

        private void TransmissionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SettingsManager.ResultSpectrumType = Options.ResultSpectrumType.TRANSMISSION;
        }

        private void OnToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SettingsManager.SaveToFile = true;
        }

        private void OffToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SettingsManager.SaveToFile = false;
        }

        private void NoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.ReferenceType = Options.ReferenceType.NONE;
        }

        private void StaticToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SettingsManager.ReferenceType = Options.ReferenceType.STATIC;
        }

        private void DynamicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.ReferenceType = Options.ReferenceType.DYNAMIC;
        }

        private void SettingsToolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            WavelengthHub.Instance.Show();
            WavelengthHub.Instance.BringToFront();
        }

        private void SettingsToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            WavelengthRangeOptions.Instance.Show();
            WavelengthRangeOptions.Instance.BringToFront();
        }

        private void managerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // If the Hub is opened to early, it may fail
                WavelengthHub.Instance.Show();
                WavelengthHub.Instance.BringToFront();
            }
            catch (Exception)
            {
                // We will let the user retry
            }
        }

        private void buttonAbortAccumulateSpectra_Click(object sender, EventArgs e)
        {
            AbortAcquisition();
        }

        /// <summary>
        /// Toggle plot trigger points.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showTriggerPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showTriggerPointsToolStripMenuItem.Checked = !showTriggerPointsToolStripMenuItem.Checked;
            SettingsManager.PlotTriggerPoints = showTriggerPointsToolStripMenuItem.Checked;
        }

        private void textBoxNumInBuffer_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAcquireFor_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void textBoxAcquireFor_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxAcquireFor.Text.Length == 0)
            {
                textBoxAcquireFor.BackColor = Color.White;
                e.Cancel = false;

                return;
            }

            if (UInt32.TryParse(textBoxAcquireFor.Text, out UInt32 value))
            {
                textBoxAcquireFor.BackColor = Color.White;
                e.Cancel = false;
            }
            else
            {
                textBoxAcquireFor.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxAcquireFor_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxAcquireFor.Text, out UInt32 value))
            {
                SettingsManager.StaticReferenceAccumulateTime = value;
            }
        }

        private void buttonGenerateReferenceFromAccumulatedSpectra_Click(object sender, EventArgs e)
        {
            // Build the reference spectrum
            bool success = SpectrumProcessor.Instance.BuildStaticReferenceSpectrum();

            if (success)
            {
                SettingsManager.ReferenceType = Options.ReferenceType.STATIC_ACCUMULATED;

                // Display the result
                SettingsManager.CurrentPlotType = Options.PlotType.CORRECTED_REFERENCE_SPECTRUM;
            }
            else
            {
                MessageBox.Show(
                    "Something went wrong in the generation of the reference spectrum!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void textBoxEstimateReferenceSpectrumMin_TextChanged(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxEstimateReferenceSpectrumMin.Text, out UInt32 value))
            {
                if (SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation == null ||
                    SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count == 0)
                {
                    SpectrumProcessor.Instance.StaticAccumulatedSpectraStartIndex = 0;
                    textBoxEstimateReferenceSpectrumMin.Text = "0";
                }
                else
                {
                    SpectrumProcessor.Instance.StaticAccumulatedSpectraStartIndex = (int)value;
                }
            }
            else
            {
                SpectrumProcessor.Instance.StaticAccumulatedSpectraStartIndex = 0;
                textBoxEstimateReferenceSpectrumMin.Text = "0";
            }
        }

        private void textBoxEstimateReferenceSpectrumMax_TextChanged(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxEstimateReferenceSpectrumMax.Text, out UInt32 value))
            {
                if (SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation == null ||
                    SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count == 0)
                {
                    SpectrumProcessor.Instance.StaticAccumulatedSpectraEndIndex = 0;
                    textBoxEstimateReferenceSpectrumMax.Text = "0";

                    return;
                }

                if (value >= SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count)
                {
                    SpectrumProcessor.Instance.StaticAccumulatedSpectraEndIndex = SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count - 1;
                    textBoxEstimateReferenceSpectrumMax.Text = "" +
                        (SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count - 1);
                }
                else
                {
                    SpectrumProcessor.Instance.StaticAccumulatedSpectraEndIndex = (int)value;
                }
            }
            else
            {
                SpectrumProcessor.Instance.StaticAccumulatedSpectraEndIndex = 0;
                textBoxEstimateReferenceSpectrumMax.Text = "0";
            }
        }

        private void buttonClearReferenceFromStatic_Click(object sender, EventArgs e)
        {
            clearReferenceSpectrum();
        }

        private void textBoxNumberOfSpectramToAccumulate_TextChanged(object sender, EventArgs e)
        {
            // Force validation on text change
            this.ValidateChildren();
        }

        private void textBoxNumberOfSpectramToAccumulate_Validating(object sender, CancelEventArgs e)
        {
            if (Int32.TryParse(textBoxNumberOfSpectramToAccumulate.Text, out int value))
            {
                if (value < 1 || value > 65535)
                {
                    textBoxNumberOfSpectramToAccumulate.BackColor = Color.Red;
                    e.Cancel = true;
                }
                else
                {
                    textBoxNumberOfSpectramToAccumulate.BackColor = Color.White;
                    e.Cancel = false;
                }
            }
            else
            {
                textBoxNumberOfSpectramToAccumulate.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxNumberOfSpectramToAccumulate_Validated(object sender, EventArgs e)
        {
            // Set the value
            SettingsManager.NumberOfSpectraForDynamicAccumulation = Int32.Parse(textBoxNumberOfSpectramToAccumulate.Text, CultureInfo.InvariantCulture);
        }

        private void textBoxSpectrumIntervalBetweenGeneration_TextChanged(object sender, EventArgs e)
        {
            // Force validation on text change
            this.ValidateChildren();
        }

        private void textBoxSpectrumIntervalBetweenGeneration_Validating(object sender, CancelEventArgs e)
        {
            if (UInt32.TryParse(textBoxSpectrumIntervalBetweenGeneration.Text, out uint value))
            {
                if (value < 1 || value > SettingsManager.NumberOfSpectraForDynamicAccumulation)
                {
                    textBoxSpectrumIntervalBetweenGeneration.BackColor = Color.Red;
                    e.Cancel = true;
                }
                else
                {
                    textBoxSpectrumIntervalBetweenGeneration.BackColor = Color.White;
                    e.Cancel = false;
                }
            }
            else
            {
                textBoxSpectrumIntervalBetweenGeneration.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxSpectrumIntervalBetweenGeneration_Validated(object sender, EventArgs e)
        {
            // Set the value
            SettingsManager.IntervalForDynamicAccumulation = UInt32.Parse(textBoxSpectrumIntervalBetweenGeneration.Text, CultureInfo.InvariantCulture);
        }

        private void enabledFilteringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.SpectrumFilteringEnabled = !(enabledFilteringToolStripMenuItem.Checked);
        }

        private void enabledTriggeringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.SpectrumThresholdingEnabled = !(enabledTriggeringToolStripMenuItem.Checked);
        }

        private void comboBoxAcquisitionOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxAcquisitionOutput.SelectedIndex)
            {
                case 0:
                    SettingsManager.ResultSpectrumType = Options.ResultSpectrumType.RAW_SPECTRUM;
                    break;
                case 1:
                    SettingsManager.ResultSpectrumType = Options.ResultSpectrumType.DARK_CORRECTED;
                    break;
                case 2:
                    SettingsManager.ResultSpectrumType = Options.ResultSpectrumType.ABSORBANCE;
                    break;
                case 3:
                    SettingsManager.ResultSpectrumType = Options.ResultSpectrumType.TRANSMISSION;
                    break;
                default:
                    throw new Exception("Unknown acquitistion output!");
            }
        }

        /// <summary>
        /// Export application settings to external configuration file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog exportSettingsDialog = new SaveFileDialog();

            exportSettingsDialog.Filter = "Settings files (*.xml)|*.xml|All files (*.*)|*.*";
            exportSettingsDialog.FilterIndex = 1;
            exportSettingsDialog.RestoreDirectory = true;

            if (exportSettingsDialog.ShowDialog() == DialogResult.OK)
            {
                SettingsWriter.Save(exportSettingsDialog.FileName);
            }
        }

        /// <summary>
        /// Import application settings from external configuration file and applies them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog importSettingsDialog = new OpenFileDialog();
            importSettingsDialog.Filter = "Settings files (*.xml)|*.xml|All files (*.*)|*.*";
            importSettingsDialog.FilterIndex = 1;
            importSettingsDialog.RestoreDirectory = true;

            if (importSettingsDialog.ShowDialog() == DialogResult.OK)
            {
                if (! SettingsReader.Load(importSettingsDialog.FileName))
                {
                    MessageBox.Show("The file " + 
                        importSettingsDialog.FileName +
                        " is not valid, and the settings could " +
                        "not be imported.",
                        "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                ApplySettingsToUI();
            }
        }

        /// <summary>
        /// Persists current application settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;

            // Persist the settings
            SettingsManager.Save();
        }

        private void ApplySettingsToUI()
        {
            // Update menus and tool bars
            UpdateMenusAndToolbarFromSettings();

            // Main window controls
            this.UpdateUIControls();

            // Refresh  the Acquisition parameters UI
            this.acquisitionParametersControl.Refresh();

            // Refresh the Processing settings
            this.processingControl.Refresh();

            // Refresh the Wavelength Hub data table
            WavelengthHub.Instance.Refresh();

            // Refresh the Saving parameters ui
            WavelengthRangeOptions.Instance.Refresh();

            // Refresh the Arduino settings
            this.arduinoParametersControl.Refresh();
        }

        private void revertSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to revert to factory settings?",
                "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SettingsManager.RestoreDefaults();

                ApplySettingsToUI();
            }
        }

        private void enableSaveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsManager.SaveToFile = !(enableSaveToFileToolStripMenuItem.Checked);
        }

        private void reloadDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to discard current changes and reload defaults settings?",
                "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                SettingsManager.RestoreLastSavedValues();

                ApplySettingsToUI();
            }
        }
    }

    // Structure to store all data required for the save thread
    public struct SpectrumForSaving
    {
        public RawSpectrumWithMetadataBuffer rawSpectrum;
        public float[] computedSpectrum;
        public bool triggered;
    }

    // Queue for SpectrumForSaving objects, to make sure that in acquisitions
    // with number of requested spectra larger than 1, only one
    // OBPGetRawSpectrumWithMetadata spectrum is added back to the supply queue
    // at the end of processing.
    public class SpectrumForSavingQueueObject
    {
        // Lock object
        private static readonly object LockObject = new object();

        public OBPGetRawSpectrumWithMetadata originalSpectrum;
        public Queue<SpectrumForSaving> mQueue;

        public SpectrumForSavingQueueObject(OBPGetRawSpectrumWithMetadata spectrum)
        {
            originalSpectrum = spectrum;
            mQueue = new Queue<SpectrumForSaving>();
        }

        public void Enqueue(SpectrumForSaving s)
        {
            lock (LockObject)
            {
                mQueue.Enqueue(s);
            }
        }

        public SpectrumForSaving Dequeue()
        {
            SpectrumForSaving s;
            lock (LockObject)
            {
                s = mQueue.Dequeue();
            }
            return s;
        }

        public int Count => mQueue.Count;
    }
}
