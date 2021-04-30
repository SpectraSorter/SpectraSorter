using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using OceanUtil;
using MadWizard.WinUSBNet;
using OBP_Library;

//
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
// Three threads of execution are utilized during high speed acquisition:
//
//  1. Main UI Thread
//     A timer, UIUpdateTimer, triggers every 100ms to update the chart and
//     IO statistics.  This timer is always active throughout the lifetime
//     of the app regardless of whether an acquisition is in progress.
//
//  2. An IO thread, doHighSpeedAcquisition, ingests spectrum IO buffers from
//     mSupplyQueue, requests a spectrum from the spectrometer, reads results
//     into the spectrum's Response buffer, and adds the spectrum to
//     mSaveQueue for processing and persistence.
//
//     The OceanFX spectrometer can accept more than one request for spectrum
//     at a time.  The IO thread utilizes an overlapped command technique
//     where it initially sends two requests for spectrum before reading out
//     a spectrum.  The idea is to keep one request for spectrum active on the
//     spectrometer at all times so the spectrometer is busy filling the next
//     request while the app is processing the previous response.
//
//  3. A compute and save thread, doComputeSaveSpectrum, ingests spectrum
//     from the mSaveQueue, computes a result (absorbance, transmission, etc.),
//     optionally persists the result to disk, and returns the spectrum
//     to the mSupplyQueue for reuse.
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

namespace FXStreamer
{
    public partial class MainForm : Form
    {

        #region data_members
        //========================= data_members =========================

        public bool mUsbScanning = true;
        public bool mIPScanning = true;

        // The USb PID of the OceanFX
        private const int OCEANFX_PID = 0x2001;

        // The list of supported device GUIDs
        protected static string[] DEVICE_GUIDS = {
            "DBBAD306-1786-4f2e-A8AB-340D45F0653F",
        };

        // The available USB device descriptors
        protected Dictionary<string, USBDeviceInfo> mUsbDeviceDescriptors = new Dictionary<string, USBDeviceInfo>();

        // The IO objects
        protected ISendReceive mActiveIO = null;
        protected TcpIpIO mTCPIO = null;
        protected USBIO mUSBIO = null;

        // Default IO timeouts
        protected int mSendTimeoutMS = 1000;
        protected int mReceiveTimeoutMS = 2000;

        // The basic IO buffers
        protected OBPBuffer mRequest = new OBPBuffer(1024);
        protected OBPBuffer mResponse = new OBPBuffer(1024);

        // The spectrum IO buffers and queues
        protected const int NUM_RESPONSES_TO_ALLOCATE = 10;  // number of spectrum response buffers to allocate
        protected OBPBuffer mSpectrumRequest = new OBPBuffer(64);  // a single spectrum request buffer should suffice for this application
        protected Queue<OBPGetRawSpectrumWithMetadata> mSaveQueue = new Queue<OBPGetRawSpectrumWithMetadata>();
        protected Queue<OBPGetRawSpectrumWithMetadata> mSupplyQueue = new Queue<OBPGetRawSpectrumWithMetadata>();

        // File save
        protected string mSaveDir = "";

        // The version strings
        protected string mFWVersion = "";
        protected string mFWSubversion = "";
        protected string mFPGAVersion = "";
        protected string mSerialNum = "";

        // Spectrometer Settings
        protected ushort mNumPixels;
        protected int mRawMetadataResponseBufferSize;
        protected const int MAX_SPECTRA_PER_REQUEST = 15;
        protected int[] mPixels = new int[0];
        protected double[] mWavecalCoefficients;
        protected double[] mWavelengths = new double[0];
        protected double[] mNonlinearityCoefficients;
        protected uint mSaturationLevel = 0xffff;

        // Continuous Strobe Settings
        protected byte mContinuousStrobeEnabled = 0;
        protected uint mContinuousStrobePeriod = 0;
        protected uint mContinuousStrobeWidth = 0;

        // Single Strobe Settings
        protected byte mSingleStrobeEnabled = 0;
        protected uint mSingleStrobePulseDelay = 0;
        protected uint mSingleStrobePulseWidth = 0;

        // Acquisition Settings
        protected uint mIntegrationTime = 10;
        protected uint mScansToAverage = 1;
        protected uint mBackToBackPerTrigger = 1;
        protected uint mNumSpectraPerRequest = 1;
        protected uint mAcquisitionDelay = 0;
        protected byte mTriggerMode = 0;
        protected byte mLampEnable = 0;

        protected bool mSingleSWTrigger = false;
        protected double mAcquisitionDuration = 0.0;
        protected string mAcquisitionDurationUnits = "seconds";  // seconds, minutes, hours, spectra
        protected bool mClearBufferBeforeTest = true;

        // File Save Settings
        protected bool mSaveToFile = true;
        protected string mSaveRangeUnits = "pixels";  // pixels, wavelengths
        protected string mSaveSpectrumType = "spectrum";  // spectrum, darkcorrected, absorbance, transmission
        protected string mSaveFilename = "fxstreamer_spectrum.csv";
        protected int mSaveStartPixel = 0;
        protected int mSaveEndPixel = 0;

        // Buffer Settings
        protected uint mBufferSize = 50000;
        protected uint mNumSpectraInBuffer = 10;
        protected byte mBufferEnabled = 0;

        // Dark and Reference Spectra
        protected ushort[] mDarkSpectrum = null;
        protected ushort[] mDarkCharted = null;
        protected DateTime mDarkSpectrumTime;

        protected ushort[] mReferenceSpectrum = null;
        protected ushort[] mReferenceCharted = null;
        protected DateTime mReferenceSpectrumTime;

        // Dark corrected spectrum
        protected ushort[] mSampleCorrectedSpectrum = null;
        protected float[] mReferenceCorrectedSpectrum = null;  // Note: this is a float[] to force absorbance and transmission division result to be float

        // Result spectrum (absorbance, transmission, etc.)
        protected float[] mResultSpectrum = null;
        protected float[] mResultCharted = null;

        protected const double MAX_ABSORBANCE = 4.0;

        // UI Control
        protected bool mIsInit = true;
        protected System.Windows.Forms.Timer mUITimer;
        protected bool mTimerBusy = false;
        protected int mUIUpdateRateMS = 100;  // Rate at which the UI is updated in milliseconds
        protected bool mIsClosing = false;

        // Acquisition State & Control
        protected volatile bool mAcquiringSpectra = false;
        protected volatile bool mStopAcquiring = false;
        protected volatile bool mIsComputeAndSave = false;

        // Acquisition Statistics
        protected long mTotalRequests;          // Total requests for spectra sent
        protected long mTotalSpectraReceived;   // Total spectra received
        protected long mTotalAcquireTime;       // Total milliseconds the acquisition has run (or ran)
        protected long mTotalSaveTime;          // Total time the compute and save thread ran (note: there's a minimum 100ms wait after acquisition completes)
        protected long mCurSpectraPerSec;       // The current acquisition rate (over the past 1 second)
        protected int mMinSupplyCount;          // The minimum number of spectrum detected in the supply queue (can be used to optimize how many to allocate)
        private long mReceiveFailures = 0;      // Number of receive failures
        private long mBytesFlushed = 0;         // Number of bytes flushed after the acquisition completes (should be zero)
        private long mSpinCounter = 0;          // Spin wait counter (waiting for IO buffers in the supply queue)
        private long mTotalBytes = 0;           // Total bytes

        #endregion


        #region initialization_and_cleanup
        //========================= initialization_and_cleanup =========================

        public MainForm()
        {
            InitializeComponent();

            // App title... include the version
            string appName = Assembly.GetEntryAssembly().GetName().Name;
            string appVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();
            appVersion = appVersion.Substring(0, appVersion.LastIndexOf('.'));
            this.Text = String.Format("{0}  {1}", appName, appVersion);

            comboBoxSaveRangeUnits.SelectedIndex = 0;
            comboBoxAcquireUnits.SelectedIndex = 0;
            comboBoxSaveType.SelectedIndex = 0;
        }

        // UI initialization
        private void MainForm_Load(object sender, EventArgs e)
        {
            buttonStartAcquisition.BackColor = Color.Gray;
            buttonAbortAcquisition.BackColor = Color.Gray;

            // Find all USB and network devices
            findDevices();

            //  UI update timer
            mUITimer = new System.Windows.Forms.Timer();
            mUITimer.Tick += new EventHandler(UIUpdateTimer);
            mUITimer.Interval = mUIUpdateRateMS;
            mUITimer.Enabled = true;
            mUITimer.Start();
        }


        // Cleanup
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mIsClosing = true;

            // Request the acquisition to stop
            mStopAcquiring = true;

            //
            // Wait for the acquisition to complete
            //
            bool bTimeout = false;
            DateTime startTime = DateTime.Now;
            while (mAcquiringSpectra && !bTimeout)
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
            mUITimer = null;

            // Close the open device
            closeDeviceConnection(false);
        }

        #endregion


        #region device_discovery
        //========================= device_discovery =========================

        private void findDevices()
        {
            dataGridViewUSBDeviceList.Rows.Clear();
            dataGridViewIPDevices.Rows.Clear();

            // Close the current device, if any
            closeDeviceConnection();

            // Find the USB devices
            Thread usbDevicesThread = new Thread(doFindUSBDevices);
            usbDevicesThread.Start();

            // Find the Network devices
            Thread ipDevicesThread = new Thread(doFindIPDevices);
            ipDevicesThread.Start();
        }

        private void doFindUSBDevices()
        {
            mUsbScanning = true;

            // Remove any previously found devices
            mUsbDeviceDescriptors.Clear();

            foreach (string devGuid in DEVICE_GUIDS)
            {
                // Find all devices with the Ocean GUID
                USBDeviceInfo[] usbDeviceDescriptors = USBDevice.GetDevices(devGuid.ToUpper());

                // If one or more devices were found with the specified GUID...
                if (usbDeviceDescriptors.Length > 0)
                {
                    foreach (USBDeviceInfo devInfo in usbDeviceDescriptors)
                    {
                        if (devInfo.PID == OCEANFX_PID)
                        {
                            mUsbDeviceDescriptors.Add(devInfo.DeviceDescription, devInfo);
                        }
                    }
                }
            }

            Invoke((MethodInvoker)delegate
            {
                onFindUSBComplete();  // Invoke on UI thread
            });

        }

        private void doFindIPDevices()
        {
            mIPScanning = true;

            // Get the IP addresses for this system
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

            // Build the set of local IPs
            HashSet<string> localIPs = new HashSet<string>();
            foreach (IPAddress localIPAddr in ipHostInfo.AddressList)
            {
                localIPs.Add(localIPAddr.ToString());
            }

            // Used to prevent duplicate entries
            HashSet<string> devicesFound = new HashSet<string>();

            IPAddress multicastIP = new IPAddress(new byte[] { 239, 239, 239, 239 });
            int multicastPort = 57357;
            int multicastTTL = 3;

            //
            // Bind to each local IPv4 and multicast
            //
            // Note: Duplicate entries can occur if a system is bound to the same network on multiple local IPs
            //       such as from a Wifi network and a wired Ethernet network.  In this case the entries
            //       really aren't duplicate entries since you should be able to connect through either
            //       local interface.  However, at this time FXStreamer only connects via the primary (default)
            //       local interface so duplicates are removed.
            //
            //       Ultimately something like “Device IP Address via Local IP Address” should probably be
            //       supported when discovering and connecting to devices.
            //
            foreach (IPAddress localIPAddr in ipHostInfo.AddressList)
            {
                // If IPv4...
                if (localIPAddr.AddressFamily == AddressFamily.InterNetwork)
                {
                    using (UdpClient udpclient = new UdpClient(AddressFamily.InterNetwork))
                    {
                        udpclient.JoinMulticastGroup(multicastIP, multicastTTL);

                        IPEndPoint localEP = new IPEndPoint(localIPAddr, multicastPort);
                        udpclient.Client.Bind(localEP);

                        IPEndPoint multicastEP = new IPEndPoint(multicastIP, multicastPort);
                        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, multicastPort);

                        // Initialize the OBP request serial number message
                        OBPBuffer sendBuffer = new OBPBuffer(64);
                        sendBuffer.IOBuffer[0] = 0xC1;
                        sendBuffer.IOBuffer[1] = 0xC0;
                        sendBuffer.IOBuffer[3] = 0x00;
                        sendBuffer.IOBuffer[9] = 0x01;
                        sendBuffer.IOBuffer[40] = 0x14;
                        sendBuffer.IOBuffer[60] = 0xC5;
                        sendBuffer.IOBuffer[61] = 0xC4;
                        sendBuffer.IOBuffer[62] = 0xC3;
                        sendBuffer.IOBuffer[63] = 0xC2;

                        // Initialize the send/receive timeouts
                        udpclient.Client.ReceiveTimeout = 200;
                        udpclient.Client.SendTimeout = 200;

                        try
                        {
                            // Send the multicast request for serial number
                            udpclient.Send(sendBuffer.IOBuffer, 64, multicastEP);
                        }
                        catch (SocketException)
                        {
                            // nothing to do
                        }

                        bool bReceivedData = false;
                        do
                        {
                            bReceivedData = false;
                            byte[] responseData = null;
                            try
                            {
                                responseData = udpclient.Receive(ref remoteEP);
                            }
                            catch (SocketException)
                            {
                                // nothing to do
                            }

                            if (responseData != null && responseData.Length > 0 && remoteEP != null)
                            {
                                bReceivedData = true;

                                char[] splitChar = { ':' };
                                string[] ipPort = remoteEP.ToString().Split(splitChar);
                                string ipAddr = ipPort.Length > 0 ? ipPort[0] : "unknown";
                                string portNum = ipPort.Length > 1 ? ipPort[1] : "unknown";
                                string serialNum = "unknown";
                                if (responseData.Length > 63)
                                {
                                    OBPBuffer responseBuffer = new OBPBuffer(responseData);
                                    OBPGetSerialNumber serialMessage = new OBPGetSerialNumber(sendBuffer, responseBuffer, false);
                                    serialMessage.initFromResponse();
                                    serialNum = serialMessage.SerialNum;
                                }

                                // Build a unique ID to prevent duplicate entries
                                string uniqueId = ipAddr + "-" + portNum + "-" + serialNum;

                                if (!localIPs.Contains(ipAddr) && !devicesFound.Contains(uniqueId))
                                {
                                    devicesFound.Add(uniqueId);

                                    Invoke((MethodInvoker)delegate
                                    {
                                        addIPDevice(ipAddr, portNum, serialNum); // invoke on UI thread
                                    });
                                }
                            }
                        } while (bReceivedData);
                    }
                }

            }

            if (!mIsClosing)
            {
                Invoke((MethodInvoker)delegate
                {
                    onFindIPDevicesComplete();  // Invoke on UI thread
                });
            }
        }

        private void onFindUSBComplete()
        {
            mUsbScanning = false;

            if (!mIPScanning)
            {
                buttonRescan.Enabled = true;
            }

            // If one or more devices were found...
            if (mUsbDeviceDescriptors.Count() > 0)
            {
                dataGridViewUSBDeviceList.SelectionChanged -= dataGridViewUSBDeviceList_SelectionChanged;

                foreach (USBDeviceInfo di in mUsbDeviceDescriptors.Values)
                {
                    int iRow = dataGridViewUSBDeviceList.Rows.Add(di.DeviceDescription);
                    dataGridViewUSBDeviceList.Rows[iRow].Tag = di;
                }

                dataGridViewUSBDeviceList.ClearSelection();

                dataGridViewUSBDeviceList.SelectionChanged += dataGridViewUSBDeviceList_SelectionChanged;
            }

        }

        private void onFindIPDevicesComplete()
        {
            mIPScanning = false;

            if (!mUsbScanning)
            {
                buttonRescan.Enabled = true;
            }
        }

        private void buttonRescan_Click(object sender, EventArgs e)
        {
            buttonRescan.Enabled = false;

            findDevices();
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

        protected void addIPDevice(string ipAddress, string portNum, string serialNum)
        {
            dataGridViewIPDevices.SelectionChanged -= dataGridViewIPDevices_SelectionChanged;
            dataGridViewIPDevices.Rows.Add(ipAddress, portNum, serialNum);
            dataGridViewIPDevices.ClearSelection();
            dataGridViewIPDevices.SelectionChanged += dataGridViewIPDevices_SelectionChanged;
        }

        #endregion


        #region device_connection
        //========================= device_connection =========================

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            connectToSelectedDevice();
        }

        protected void connectToSelectedDevice()
        {
            closeDeviceConnection();

            if (dataGridViewIPDevices.SelectedRows.Count > 0)
            {
                int portNum = 57357;
                IPAddress ipAddr = null;

                try
                {
                    ipAddr = IPAddress.Parse(dataGridViewIPDevices.SelectedRows[0].Cells[0].Value.ToString());
                    portNum = int.Parse(dataGridViewIPDevices.SelectedRows[0].Cells[1].Value.ToString());
                }
                catch (Exception)
                {
                    // nothing to do
                }

                if (ipAddr == null)
                {
                    SetConnectError("Invalid IP");
                }
                else
                {
                    Thread tcpConnectThread = new Thread(() => doTCPConnect(ipAddr, portNum));
                    tcpConnectThread.Start();
                }
            }
            else if (dataGridViewUSBDeviceList.SelectedRows.Count > 0)
            {
                USBDeviceInfo devInfo = (USBDeviceInfo)(dataGridViewUSBDeviceList.SelectedRows[0].Tag);

                if (devInfo == null)
                {
                    SetConnectError("Failed to Open");
                }
                else
                {
                    Thread usbConnectThread = new Thread(() => doUSBConnect(devInfo));
                    usbConnectThread.Start();
                }

            }
            else
            {
                SetConnectStatus("No Selection");
            }

        }

        protected void closeDeviceConnection(bool bUpdateUIControls=true) {
            try
            {
                if (mTCPIO != null)
                {
                    mTCPIO.MySocket.Close(500);
                    mTCPIO = null;
                }

            }
            catch (Exception)
            {
                // nothing to do
            }

            try
            {
                if (mUSBIO != null)
                {
                    mUSBIO.Dispose();
                    mUSBIO = null;
                }

            }
            catch (Exception)
            {
                // nothing to do
            }

            mActiveIO = null;

            mFWVersion = "";
            mFWSubversion = "";
            mFPGAVersion = "";
            mSerialNum = "";

            if (bUpdateUIControls)
            {
                labelConnectStatus.Visible = false;

                UpdateUIControls();

                clearDarkSpectrum();
                clearReferenceSpectrum();

                updateDefaultSaveType();
            }
        }

        protected void doTCPConnect(IPAddress ipAddr, int portNum) {
            try {
                mTCPIO = new TcpIpIO(ipAddr, portNum);
                if (mTCPIO != null) {
                    if (mTCPIO.MySocket == null)
                    {
                        mTCPIO = null;
                    }
                    else if (!mTCPIO.MySocket.Connected) {
                            mTCPIO.Connect();
                    }

                    if (!mTCPIO.MySocket.Connected)
                    {
                        mTCPIO = null;
                    }
                    else
                    {
                        mActiveIO = mTCPIO;

                        doDeviceInitialization();
                    }
                }

            }
            catch(Exception) {
                Invoke((MethodInvoker)delegate
                {
                    closeDeviceConnection();
                });
            }

            if (mTCPIO == null)
            {
                Invoke((MethodInvoker)delegate
                {
                    onConnectFailed();  // Invoke on UI thread
                });
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    onConnected();  // Invoke on UI thread
                });
            }

        }

        protected void doUSBConnect(USBDeviceInfo devInfo)
        {
            try
            {
                // Instantiate the new USB device
                mUSBIO = new USBIO(devInfo);

                mActiveIO = mUSBIO;

                doDeviceInitialization();
            }
            catch (Exception)
            {
                Invoke((MethodInvoker)delegate
                {
                    closeDeviceConnection();
                });
            }

            if (mUSBIO == null)
            {
                Invoke((MethodInvoker)delegate
                {
                    onConnectFailed();  // Invoke on UI thread
                });
            }
            else
            {
                Invoke((MethodInvoker)delegate
                {
                    onConnected();  // Invoke on UI thread
                });
            }

        }

        protected void onConnectFailed()
        {
            SetConnectError("Failed to Connect");
            updateDefaultSaveType();
        }

        protected void onConnected()
        {
            SetConnectStatus("Connected");
            updateDefaultSaveType();
        }

        protected void SetConnectError(string statusStr)
        {
            labelConnectStatus.Visible = true;
            labelConnectStatus.ForeColor = Color.Crimson;
            labelConnectStatus.Text = statusStr;
        }

        protected void SetConnectStatus(string statusStr)
        {
            labelConnectStatus.Visible = true;
            labelConnectStatus.ForeColor = Color.ForestGreen;
            labelConnectStatus.Text = statusStr;
        }

        protected void doDeviceInitialization()
        {
            doFlush();

            OBPGetFWRevision fwRevision = new OBPGetFWRevision(mActiveIO, mRequest, mResponse);
            mFWVersion = (fwRevision.Send()) ? fwRevision.FwRevision.ToString("X4") : "";

            OBPGetFWSubRevision fwSubRevision = new OBPGetFWSubRevision(mActiveIO, mRequest, mResponse);
            mFWSubversion = (fwSubRevision.Send()) ? fwSubRevision.FwSubRevision.ToString("X4") : "";

            OBPGetFPGARevision fpgaRevision = new OBPGetFPGARevision(mActiveIO, mRequest, mResponse);
            mFPGAVersion = (fpgaRevision.Send()) ? fpgaRevision.FPGARevision.ToString("X4") : "";

            OBPGetSerialNumber serialNum = new OBPGetSerialNumber(mActiveIO, mRequest, mResponse);
            mSerialNum = (serialNum.Send()) ? serialNum.SerialNum : "";

            OBPGetNumPixels numPixels = new OBPGetNumPixels(mActiveIO, mRequest, mResponse);
            mNumPixels = (numPixels.Send()) ? numPixels.NumPixels : (ushort)0;

            // Returns zero so commented out
            //OBPGetSaturationLevel saturationLevel = new OBPGetSaturationLevel(mActiveIO, mRequest, mResponse);
            //mSaturationLevel = (saturationLevel.Send()) ? saturationLevel.SaturationLevel : (uint)0xffff;

            // Response buffer size required to receive 15 spectra per response
            mRawMetadataResponseBufferSize = 64 + MAX_SPECTRA_PER_REQUEST * (68 + mNumPixels * 2);
            
            mPixels = new int[mNumPixels];
            mWavelengths = new double[mNumPixels];

            OBPGetNumWavecalCoefficients numWavecal = new OBPGetNumWavecalCoefficients(mActiveIO, mRequest, mResponse);
            mWavecalCoefficients = (numWavecal.Send()) ? new double[numWavecal.NumWavecalCoeffs] : new double[0];

            OBPGetWavecalCoefficient wavecalCoeff = new OBPGetWavecalCoefficient(mActiveIO, mRequest, mResponse);
            for (int i = 0; i < mWavecalCoefficients.Length; i++)
            {
                wavecalCoeff.WavecalIndex = (byte)i;
                mWavecalCoefficients[i] = (wavecalCoeff.Send()) ? wavecalCoeff.WavecalCoeff : 0.0;
            }

            for (int i = 0; i < mNumPixels; i++)
            {
                mPixels[i] = i;
                mWavelengths[i] = applyCoefficients(mWavecalCoefficients, i);
            }

            OBPGetNumNonLinearityCoefficients numNLC = new OBPGetNumNonLinearityCoefficients(mActiveIO, mRequest, mResponse);
            mNonlinearityCoefficients = (numNLC.Send()) ? new double[numNLC.NumNonLinearityCoeffs] : new double[0];

            OBPGetNonLinearityCoefficient nlcCoeff = new OBPGetNonLinearityCoefficient(mActiveIO, mRequest, mResponse);
            for (int i = 0; i < mNonlinearityCoefficients.Length; i++)
            {
                nlcCoeff.NonLinearityIndex = (byte)i;
                mNonlinearityCoefficients[i] = (nlcCoeff.Send()) ? nlcCoeff.NonLinearityCoeff : 0.0;
            }

            OBPGetContinuousStrobeEnable csEnable = new OBPGetContinuousStrobeEnable(mActiveIO, mRequest, mResponse);
            mContinuousStrobeEnabled = (csEnable.Send()) ? csEnable.Enabled: (byte)0;

            OBPGetContinuousStrobePeriod csPeriod = new OBPGetContinuousStrobePeriod(mActiveIO, mRequest, mResponse);
            mContinuousStrobePeriod = (csPeriod.Send()) ? csPeriod.Period : (uint)0;

            OBPGetContinuousStrobeWidth csWidth = new OBPGetContinuousStrobeWidth(mActiveIO, mRequest, mResponse);
            mContinuousStrobeWidth = (csWidth.Send()) ? csWidth.Width : (uint)0;

            OBPGetSingleStrobeEnable ssEnable = new OBPGetSingleStrobeEnable(mActiveIO, mRequest, mResponse);
            mSingleStrobeEnabled = (ssEnable.Send()) ? ssEnable.Enabled : (byte)0;

            OBPGetSingleStrobePulseDelay ssPulseDelay = new OBPGetSingleStrobePulseDelay(mActiveIO, mRequest, mResponse);
            mSingleStrobePulseDelay = (ssPulseDelay.Send()) ? ssPulseDelay.PulseDelay : (uint)0;

            OBPGetSingleStrobePulseWidth ssPulseWidth = new OBPGetSingleStrobePulseWidth(mActiveIO, mRequest, mResponse);
            mSingleStrobePulseWidth = (ssPulseWidth.Send()) ? ssPulseWidth.PulseWidth : (uint)0;

            OBPGetIntegrationTime integrationTime = new OBPGetIntegrationTime(mActiveIO, mRequest, mResponse);
            mIntegrationTime = (integrationTime.Send()) ? integrationTime.IntegrationTime : (uint)0;
            mIntegrationTime = 10;  // Override the actual setting with a default of 10 microseconds

            OBPGetScansToAvg scansToAvg = new OBPGetScansToAvg(mActiveIO, mRequest, mResponse);
            mScansToAverage = (scansToAvg.Send()) ? scansToAvg.ScansToAvg : (uint)0;

            OBPGetNumSpectraPerTrigger spectraPerTrigger = new OBPGetNumSpectraPerTrigger(mActiveIO, mRequest, mResponse);
            mBackToBackPerTrigger = (spectraPerTrigger.Send()) ? spectraPerTrigger.NumSpectra : (uint)0;
            mBackToBackPerTrigger = 50000;  // Override the actual setting with a default of 50000

            mNumSpectraPerRequest = 15;

            OBPGetAcquisitionDelay acquisitionDelay = new OBPGetAcquisitionDelay(mActiveIO, mRequest, mResponse);
            mAcquisitionDelay = (acquisitionDelay.Send()) ? acquisitionDelay.AcquisitionDelay : (uint)0;

            OBPGetTriggerMode triggerMode = new OBPGetTriggerMode(mActiveIO, mRequest, mResponse);
            mTriggerMode = (triggerMode.Send()) ? triggerMode.TriggerMode : (byte)0;

            OBPGetLampEnable lampEnable = new OBPGetLampEnable(mActiveIO, mRequest, mResponse);
            mLampEnable = (lampEnable.Send()) ? lampEnable.LampEnable : (byte)0;

            OBPGetBufferingEnabled bufferEnable = new OBPGetBufferingEnabled(mActiveIO, mRequest, mResponse);
            mBufferEnabled = (bufferEnable.Send()) ? bufferEnable.BufferingEnabled : (byte)0;
            mNumSpectraPerRequest = 15;
            mBufferEnabled = 1;  // Override the actual setting with enabled

            OBPGetBufferSize bufferSize = new OBPGetBufferSize(mActiveIO, mRequest, mResponse);
            mBufferSize = (bufferSize.Send()) ? bufferSize.BufferSize : (byte)0;

            OBPGetNumSpectraInBuffer numInBuffer = new OBPGetNumSpectraInBuffer(mActiveIO, mRequest, mResponse);
            mNumSpectraInBuffer = (numInBuffer.Send()) ? numInBuffer.NumSpectraInBuffer : (byte)0;

            Invoke((MethodInvoker)delegate
            {
                OnDeviceInitComplete();  // Invoke on UI thread
                mIsInit = false;
            });
        }

        protected void allocateIOBuffers()
        {
            mSupplyQueue.Clear();
            mSaveQueue.Clear();
            for (int i = 0; i < NUM_RESPONSES_TO_ALLOCATE; i++)
            {
                mSupplyQueue.Enqueue(
                    new OBPGetRawSpectrumWithMetadata(mActiveIO, new OBPBuffer(64), new OBPBuffer(mRawMetadataResponseBufferSize))
                );
            }
        }

        protected void OnDeviceInitComplete()
        {
            UpdateUIControls();

            textBoxSaveEndRange.Text = (mPixels.Length - 1).ToString();
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

        // Attempts to flush the receive buffer
        protected int doFlush()
        {
            byte[] data = new byte[1];
            int bytesFlushed = 0;
            int MAX_TO_FLUSH = 1024 * 1024;

            if (mActiveIO != null)
            {
                SendReceiveStatus ioStatus;
                mActiveIO.setReceiveTimeout(50);

                do
                {
                    ioStatus = mActiveIO.receiveBytes(data, 0, data.Length);
                    if (ioStatus == SendReceiveStatus.IO_SUCCESS)
                    {
                        bytesFlushed++;
                    }
                } while (ioStatus == SendReceiveStatus.IO_SUCCESS && bytesFlushed < MAX_TO_FLUSH);

                mActiveIO.setReceiveTimeout(mReceiveTimeoutMS);
            }

            return bytesFlushed;

        }

        #endregion


        #region helper_methods
        //========================= helper_methods =========================

        protected int int_ParseHexOrDec(String numStr)
        {
            int rtnVal = 0;
            try
            {
                rtnVal = (numStr.StartsWith("0x")) ?
                    int.Parse(numStr.Substring(2), System.Globalization.NumberStyles.HexNumber) :
                    int.Parse(numStr);
            }
            finally
            {
            }
            return rtnVal;
        }

        protected uint uint_ParseHexOrDec(String numStr)
        {
            uint rtnVal = 0;
            try
            {
                rtnVal = (numStr.StartsWith("0x")) ?
                    uint.Parse(numStr.Substring(2), System.Globalization.NumberStyles.HexNumber) :
                    uint.Parse(numStr);
            }
            finally
            {
            }
            return rtnVal;
        }

        protected long long_ParseHexOrDec(String numStr)
        {
            long rtnVal = 0;
            try
            {
                rtnVal = (numStr.StartsWith("0x")) ?
                    long.Parse(numStr.Substring(2), System.Globalization.NumberStyles.HexNumber) :
                    long.Parse(numStr);
            }
            finally
            {
            }
            return rtnVal;
        }

        protected short short_ParseHexOrDec(String numStr)
        {
            short rtnVal = 0;
            try
            {
                rtnVal = (numStr.StartsWith("0x")) ?
                    short.Parse(numStr.Substring(2), System.Globalization.NumberStyles.HexNumber) :
                    short.Parse(numStr);
            }
            finally
            {
            }
            return rtnVal;
        }

        protected ushort ushort_ParseHexOrDec(String numStr)
        {
            ushort rtnVal = 0;
            try
            {
                rtnVal = (numStr.StartsWith("0x")) ?
                    ushort.Parse(numStr.Substring(2), System.Globalization.NumberStyles.HexNumber) :
                    ushort.Parse(numStr);
            }
            finally
            {
            }
            return rtnVal;
        }

        protected byte byte_ParseHexOrDec(String numStr)
        {
            byte rtnVal = 0;
            try
            {
                rtnVal = (numStr.StartsWith("0x")) ?
                    byte.Parse(numStr.Substring(2), System.Globalization.NumberStyles.HexNumber) :
                    byte.Parse(numStr);
            }
            finally
            {
            }
            return rtnVal;
        }

        protected double double_ParseHexOrDec(String numStr)
        {
            double rtnVal = 0;
            try
            {
                rtnVal = (numStr.StartsWith("0x")) ?
                    double.Parse(numStr.Substring(2), System.Globalization.NumberStyles.HexNumber) :
                    double.Parse(numStr);
            }
            finally
            {
            }
            return rtnVal;
        }

        #endregion


        #region ui_timer_and_updates
        //========================= ui_timer_and_updates =========================

        protected void UpdateUIControls()
        {
            textBoxFWVersion.Text = mFWVersion;
            textBoxFWSubversion.Text = mFWSubversion;
            textBoxFPGAVersion.Text = mFPGAVersion;
            textBoxSerialNum.Text = mSerialNum;

            UpdateFileSaveUnits();

            checkBoxContinuousStrobeEnabled.Checked = mContinuousStrobeEnabled == 0 ? false : true;
            textBoxContinuousStrobePeriod.Text = mContinuousStrobePeriod.ToString();
            textBoxContinuousStrobeWidth.Text = mContinuousStrobeWidth.ToString();

            checkBoxSingleStrobeEnabled.Checked = mSingleStrobeEnabled == 0 ? false : true;
            textBoxSingleStrobePulseDelay.Text = mSingleStrobePulseDelay.ToString();
            textBoxSingleStrobePulseWidth.Text = mSingleStrobePulseWidth.ToString();

            textBoxIntegrationTime.Text = mIntegrationTime.ToString();
            textBoxScansToAverage.Text = mScansToAverage.ToString();
            textBoxBackToBack.Text = mBackToBackPerTrigger.ToString();
            textBoxSpectraPerRequest.Text = mNumSpectraPerRequest.ToString();
            textBoxAcquisitionDelay.Text = mAcquisitionDelay.ToString();
            comboBoxTriggerMode.SelectedIndex = (mTriggerMode < 6) ? mTriggerMode : 0xFF;
            checkBoxLampEnable.Checked = (mLampEnable == 0) ? false : true;

            checkBoxBufferingEnabled.Checked = (mBufferEnabled == 0) ? false : true;
            textBoxNumInBuffer.Text = mNumSpectraInBuffer.ToString();

            bool bActiveIO = (mActiveIO != null);

            buttonStartAcquisition.Enabled = bActiveIO;
            buttonStartAcquisition.BackColor = bActiveIO ? Color.ForestGreen : Color.Gray;
            buttonUpdateSpectraInBuffer.Enabled = bActiveIO;
            buttonClearBuffer.Enabled = bActiveIO;
            buttonTakeReference.Enabled = bActiveIO;
            buttonClearReference.Enabled = bActiveIO;
            buttonTakeDark.Enabled = bActiveIO;
            buttonClearDark.Enabled = bActiveIO;
            buttonSaveDir.Enabled = bActiveIO;
            buttonUpdateFileTime.Enabled = bActiveIO;
            textBoxSaveFilename.Enabled = bActiveIO;
            textBoxSaveStartRange.Enabled = bActiveIO;
            textBoxSaveEndRange.Enabled = bActiveIO;

        }

        private void UpdateFileSaveUnits()
        {
            if (comboBoxSaveRangeUnits.SelectedIndex == 0 && mPixels.Length > 0)
            {
                mSaveRangeUnits = "pixels";
                labelSaveStart.Text = mPixels[0].ToString();
                labelSaveEnd.Text = mPixels[mPixels.Length - 1].ToString();
            }
            else if (comboBoxSaveRangeUnits.SelectedIndex == 1 && mWavelengths.Length > 0)
            {
                mSaveRangeUnits = "wavelengths";
                labelSaveStart.Text = mWavelengths[0].ToString("0.#");
                labelSaveEnd.Text = mWavelengths[mWavelengths.Length - 1].ToString("0.#");
            }
        }

        private void updateFileSaveRange()
        {
            mSaveStartPixel = 0;
            mSaveEndPixel = 0;

            if (comboBoxSaveRangeUnits.SelectedIndex == 0 && mPixels.Length > 0)
            {
                mSaveStartPixel = int_ParseHexOrDec(textBoxSaveStartRange.Text);
                if (mSaveStartPixel < 0)
                {
                    mSaveStartPixel = 0;
                }
                else if (mSaveStartPixel >= mPixels.Length)
                {
                    mSaveStartPixel = mPixels.Length - 1;
                }

                mSaveEndPixel = int_ParseHexOrDec(textBoxSaveEndRange.Text);
                if (mSaveEndPixel < mSaveStartPixel)
                {
                    mSaveEndPixel = mSaveStartPixel;
                }
                else if (mSaveEndPixel >= mPixels.Length)
                {
                    mSaveEndPixel = mPixels.Length - 1;
                }
            }
            else if (comboBoxSaveRangeUnits.SelectedIndex == 1 && mWavelengths.Length > 0)
            {
                double startWavelength = mWavelengths[0];
                double endWavelength = mWavelengths[mWavelengths.Length - 1];

                try
                {
                    startWavelength = double.Parse(textBoxSaveStartRange.Text);
                }
                catch(Exception)
                {
                    // nothing to do
                }

                try
                {
                    endWavelength = double.Parse(textBoxSaveEndRange.Text);
                }
                catch (Exception)
                {
                    // nothing to do
                }

                mSaveStartPixel = 0;
                mSaveEndPixel = 0;
                for (int i=0; i < mWavelengths.Length; i++)
                {
                    if (startWavelength >= mWavelengths[i])
                    {
                        mSaveStartPixel = i;
                    }
                    if (endWavelength >= mWavelengths[i])
                    {
                        mSaveEndPixel = i;
                    }
                }
            }
        }

        //
        // Periodic timer-driven UI updates
        //
        private void UIUpdateTimer(Object myObject, EventArgs myEventArgs)
        {
            if (mTimerBusy) return;

            try
            {
                mTimerBusy = true;
                DateTime curTime = DateTime.Now;

                if (mDarkSpectrum != null)
                {
                    labelDarkStatus.Text = getAgeString(curTime - mDarkSpectrumTime);
                }

                if (mReferenceSpectrum != null)
                {
                    labelReferenceStatus.Text = getAgeString(curTime - mReferenceSpectrumTime);
                }

                labelCurSpectraPerSec.Text = mCurSpectraPerSec + " spectra/sec";
                labelTotalBytes.Text = toByteString(mTotalBytes);
                labelTotalSpectra.Text = mTotalSpectraReceived.ToString("N0");
                labelTotalRequests.Text = mTotalRequests.ToString("N0");
                labelTotalTime.Text = getAgeString(new TimeSpan(mTotalAcquireTime * TimeSpan.TicksPerMillisecond));
                double totalAcquireSeconds = mTotalAcquireTime / 1000.0;
                labelSpectraPerSec.Text = totalAcquireSeconds == 0 ? "0" : (mTotalSpectraReceived / totalAcquireSeconds).ToString("0.0");
                labelSpectraPerRequest.Text = mTotalRequests == 0 ? "0" : (((double)mTotalSpectraReceived) / mTotalRequests).ToString("0.0");

                if (mResultSpectrum != null)
                {
                    // If a change in the result spectrum...
                    if (mResultSpectrum != mResultCharted)
                    {
                        mResultCharted = mResultSpectrum;

                        Series s = chartSpectrum.Series[0];

                        if (mResultCharted == null)
                        {
                            s.Enabled = false;
                        }
                        else
                        {
                            s.Enabled = true;

                            s.Points.DataBindXY(mWavelengths, mResultCharted);

                            chartSpectrum.ChartAreas[0].AxisY.IsStartedFromZero = true;
                            chartSpectrum.ChartAreas[0].AxisY.Minimum = 0;
                        }

                        // Set the maximum Y value based on the spectrum type
                        if (mSaveSpectrumType.StartsWith("spe") || mSaveSpectrumType.StartsWith("dar"))
                        {
                            chartSpectrum.ChartAreas[0].AxisY.Maximum = mSaturationLevel;
                        }
                        else if (mSaveSpectrumType.StartsWith("abs"))
                        {
                            chartSpectrum.ChartAreas[0].AxisY.Maximum = MAX_ABSORBANCE;
                        }
                        else 
                        {
                            chartSpectrum.ChartAreas[0].AxisY.Maximum = 100.0;
                        }

                    }

                }
                else
                {
                    // If a change in the dark spectrum...
                    if (mDarkSpectrum != mDarkCharted)
                    {
                        mDarkCharted = mDarkSpectrum;

                        Series s = chartSpectrum.Series[0];

                        if (mDarkCharted == null)
                        {
                            s.Enabled = false;
                        }
                        else
                        {
                            s.Enabled = true;

                            s.Points.DataBindXY(mWavelengths, mDarkCharted);

                            chartSpectrum.ChartAreas[0].AxisY.IsStartedFromZero = true;
                            chartSpectrum.ChartAreas[0].AxisY.Maximum = mSaturationLevel;
                        }
                    }

                    // If a change in the reference spectrum...
                    if (mReferenceSpectrum != mReferenceCharted)
                    {
                        mReferenceCharted = mReferenceSpectrum;

                        Series s = chartSpectrum.Series[1];

                        if (mReferenceCharted == null)
                        {
                            s.Enabled = false;
                        }
                        else
                        {
                            s.Enabled = true;

                            s.Points.DataBindXY(mWavelengths, mReferenceCharted);

                            chartSpectrum.ChartAreas[0].AxisY.IsStartedFromZero = true;
                            chartSpectrum.ChartAreas[0].AxisY.Maximum = mSaturationLevel;
                        }
                    }

                }

            }
            finally
            {
                mTimerBusy = false;
            }
        }

        protected string toByteString(double numBytes)
        {
            string byteStr = " bytes";
            if (numBytes >= 1024)
            {
                numBytes /= 1024;
                byteStr = " KB";
            }

            if (numBytes >= 1024)
            {
                numBytes /= 1024;
                byteStr = " MB";
            }

            if (numBytes >= 1024)
            {
                numBytes /= 1024;
                byteStr = " GB";
            }

            if (numBytes >= 1024)
            {
                numBytes /= 1024;
                byteStr = " PB";
            }

            if (numBytes >= 1024)
            {
                numBytes /= 1024;
                byteStr = " EB";
            }

            return numBytes.ToString("0.0") + byteStr;
        }

        // Return an age string from a TimeSpan
        protected string getAgeString(TimeSpan ts)
        {
            string ageStr = ts.ToString(@"hh\:mm\:ss");

            if (ts.Days == 1)
            {
                ageStr = ts.ToString("%d") + " day " + ageStr;
            }
            else if (ts.Days >= 1)
            {
                ageStr = ts.ToString("%d") + " days " + ageStr;
            }

            return ageStr;

        }

        #endregion

        #region ui_control_handlers
        //========================= ui_control_handlers =========================

        private void comboBoxSaveRangeUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFileSaveUnits();
        }

        // ----- Update the number of spectra in the buffer -----

        private void buttonUpdateSpectraInBuffer_Click(object sender, EventArgs e)
        {
            buttonUpdateSpectraInBuffer.Enabled = false;

            Thread ioThread = new Thread(() => doUpdateSpectraInBuffer());
            ioThread.Start();

        }

        private void doUpdateSpectraInBuffer()
        {
            OBPGetNumSpectraInBuffer numInBuffer = new OBPGetNumSpectraInBuffer(mActiveIO, mRequest, mResponse);
            mNumSpectraInBuffer = (numInBuffer.Send()) ? numInBuffer.NumSpectraInBuffer : (byte)0;

            Invoke((MethodInvoker)delegate
            {
                onUpdateSpectraInBuffer(); // invoke on UI thread
            });
        }

        private void onUpdateSpectraInBuffer()
        {
            textBoxNumInBuffer.Text = mNumSpectraInBuffer.ToString();

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
            OBPClearBufferedSpectra clearBuffer = new OBPClearBufferedSpectra(mActiveIO, mRequest, mResponse);
            clearBuffer.Send();

            doUpdateSpectraInBuffer();
        }

        private void checkBoxSingleSWTrigger_CheckedChanged(object sender, EventArgs e)
        {
            mSingleSWTrigger = checkBoxSingleSWTrigger.Checked;
        }

        private void checkBoxSaveToFile_CheckedChanged(object sender, EventArgs e)
        {
            mSaveToFile = checkBoxSaveToFile.Checked;
        }

        private void comboBoxAcquireUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxAcquireUnits.SelectedIndex)
            {
                case 0:
                    mAcquisitionDurationUnits = "seconds";
                    break;
                case 1:
                    mAcquisitionDurationUnits = "minutes";
                    break;
                case 2:
                    mAcquisitionDurationUnits = "hours";
                    break;
                case 3:
                    mAcquisitionDurationUnits = "spectra";
                    break;

            }
        }

        private void checkBoxSingleStrobeEnabled_CheckedChanged(object sender, EventArgs e)
        {
            mSingleStrobeEnabled = checkBoxSingleStrobeEnabled.Checked ? (byte)1 : (byte)0;
        }

        private void checkBoxContinuousStrobeEnabled_CheckedChanged(object sender, EventArgs e)
        {
            mContinuousStrobeEnabled = checkBoxContinuousStrobeEnabled.Checked ? (byte)1 : (byte)0;
        }

        private void checkBoxClearBufferBeforeAcquisition_CheckedChanged(object sender, EventArgs e)
        {
            mClearBufferBeforeTest = checkBoxClearBufferBeforeAcquisition.Checked;
        }

        #endregion

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

            mContinuousStrobeEnabled = checkBoxContinuousStrobeEnabled.Checked ? (byte)1 : (byte)0;
            mContinuousStrobePeriod = uint_ParseHexOrDec(textBoxContinuousStrobePeriod.Text);
            mContinuousStrobeWidth = uint_ParseHexOrDec(textBoxContinuousStrobeWidth.Text);

            mSingleStrobeEnabled = checkBoxSingleStrobeEnabled.Checked ? (byte)1 : (byte)0;
            mSingleStrobePulseDelay = uint_ParseHexOrDec(textBoxSingleStrobePulseDelay.Text);
            mSingleStrobePulseWidth = uint_ParseHexOrDec(textBoxSingleStrobePulseWidth.Text);

            mIntegrationTime = uint_ParseHexOrDec(textBoxIntegrationTime.Text);
            mScansToAverage = uint_ParseHexOrDec(textBoxScansToAverage.Text);
            mBackToBackPerTrigger = uint_ParseHexOrDec(textBoxBackToBack.Text);
            mNumSpectraPerRequest = uint_ParseHexOrDec(textBoxSpectraPerRequest.Text);
            mAcquisitionDelay = uint_ParseHexOrDec(textBoxAcquisitionDelay.Text);

            mTriggerMode = (byte)comboBoxTriggerMode.SelectedIndex;
            if (mTriggerMode > 5)
            {
                mTriggerMode = 0xFF;
            }

            mBufferEnabled = checkBoxBufferingEnabled.Checked ? (byte)1 : (byte)0;

            mSaveFilename = textBoxSaveFilename.Text;

            mAcquisitionDuration = double_ParseHexOrDec(textBoxAcquireDuration.Text);

        }

        private void doSyncSpectrometerSettings(bool bSingleSpectrum=false)
        {
            OBPSetContinuousStrobeEnable csEnable = new OBPSetContinuousStrobeEnable(mActiveIO, mRequest, mResponse);
            csEnable.Enabled = mContinuousStrobeEnabled;
            csEnable.Response.AckRequest = true;
            csEnable.Send();

            OBPSetContinuousStrobePeriod csPeriod = new OBPSetContinuousStrobePeriod(mActiveIO, mRequest, mResponse);
            csPeriod.Period = mContinuousStrobePeriod;
            csPeriod.Response.AckRequest = true;
            csPeriod.Send();

            OBPSetContinuousStrobeWidth csWidth = new OBPSetContinuousStrobeWidth(mActiveIO, mRequest, mResponse);
            csWidth.Width = mContinuousStrobeWidth;
            csWidth.Response.AckRequest = true;
            csWidth.Send();

            OBPSetSingleStrobeEnable ssEnable = new OBPSetSingleStrobeEnable(mActiveIO, mRequest, mResponse);
            ssEnable.Enabled = mSingleStrobeEnabled;
            ssEnable.Response.AckRequest = true;
            ssEnable.Send();

            OBPSetSingleStrobePulseDelay ssPulseDelay = new OBPSetSingleStrobePulseDelay(mActiveIO, mRequest, mResponse);
            ssPulseDelay.PulseDelay = mSingleStrobePulseDelay;
            ssPulseDelay.Response.AckRequest = true;
            ssPulseDelay.Send();

            OBPSetSingleStrobePulseWidth ssPulseWidth = new OBPSetSingleStrobePulseWidth(mActiveIO, mRequest, mResponse);
            ssPulseWidth.PulseWidth = mSingleStrobePulseWidth;
            ssPulseWidth.Response.AckRequest = true;
            ssPulseWidth.Send();

            OBPSetIntegrationTime integrationTime = new OBPSetIntegrationTime(mActiveIO, mRequest, mResponse);
            integrationTime.IntegrationTime = mIntegrationTime;
            integrationTime.Response.AckRequest = true;
            integrationTime.Send();

            OBPSetScansToAvg scansToAvg = new OBPSetScansToAvg(mActiveIO, mRequest, mResponse);
            scansToAvg.ScansToAvg = mScansToAverage;
            scansToAvg.Response.AckRequest = true;
            scansToAvg.Send();

            OBPSetNumSpectraPerTrigger spectraPerTrigger = new OBPSetNumSpectraPerTrigger(mActiveIO, mRequest, mResponse);
            spectraPerTrigger.NumSpectra = bSingleSpectrum ? 1 : mBackToBackPerTrigger;
            spectraPerTrigger.Response.AckRequest = true;
            spectraPerTrigger.Send();

            OBPSetAcquisitionDelay acquisitionDelay = new OBPSetAcquisitionDelay(mActiveIO, mRequest, mResponse);
            acquisitionDelay.AcquisitionDelay = mAcquisitionDelay;
            acquisitionDelay.Response.AckRequest = true;
            acquisitionDelay.Send();

            OBPSetTriggerMode triggerMode = new OBPSetTriggerMode(mActiveIO, mRequest, mResponse);
            triggerMode.TriggerMode = bSingleSpectrum ? (byte)0 : mTriggerMode;  // Force SW trigger when getting a single spectrum
            triggerMode.Response.AckRequest = true;
            triggerMode.Send();

            // Note: Lamp enable takes effect immediately upon checkbox change and doesn't need to be set here
            //OBPSetLampEnable lampEnable = new OBPSetLampEnable(mActiveIO, mRequest, mResponse);
            //lampEnable.LampEnable = mLampEnable;
            //lampEnable.Send();

            OBPSetBufferingEnabled bufferEnable = new OBPSetBufferingEnabled(mActiveIO, mRequest, mResponse);
            bufferEnable.BufferingEnabled = mBufferEnabled;
            bufferEnable.Response.AckRequest = true;
            bufferEnable.Send();

            //OBPSetBufferSize bufferSize = new OBPSetBufferSize(mActiveIO, mRequest, mResponse);
            //bufferSize.BufferSize = mBufferSize;
            //bufferSize.Response.AckRequest = true;
            //bufferSize.Send();

            if (!mIsClosing)
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

            takeSingleSpectrum("dark");
        }

        private void buttonClearDark_Click(object sender, EventArgs e)
        {
            clearDarkSpectrum();
        }

        private void clearDarkSpectrum()
        {
            labelDarkStatus.Visible = false;
            mDarkSpectrum = null;

            updateDefaultSaveType();
        }

        private void buttonTakeReference_Click(object sender, EventArgs e)
        {
            buttonTakeReference.Enabled = false;

            takeSingleSpectrum("reference");
        }

        private void buttonClearReference_Click(object sender, EventArgs e)
        {
            clearReferenceSpectrum();
        }

        private void clearReferenceSpectrum()
        {
            labelReferenceStatus.Visible = false;
            mReferenceSpectrum = null;

            updateDefaultSaveType();
        }

        private void takeSingleSpectrum(string spectrumType)
        {
            Thread ioThread = new Thread(() => doTakeSingleSpectrum(spectrumType));
            ioThread.Start();
        }

        private void doTakeSingleSpectrum(string spectrumType)
        {
            doSyncSpectrometerSettings(true);

            // Allocate a large enough response buffer
            OBPBuffer spectrumResponse = new OBPBuffer(mNumPixels * 2 + 128);

            // Note: With the FX the "corrected" spectrum is just the raw spectrum without the metadata
            OBPGetCorrectedSpectrum correctedSpectrum = new OBPGetCorrectedSpectrum(mActiveIO, mRequest, spectrumResponse);
            correctedSpectrum.Send();
            if (correctedSpectrum.IsSuccess) {
                if (spectrumType == "dark") {
                    mDarkSpectrum = correctedSpectrum.CorrectedSpectrum;
                }
                else if (spectrumType == "reference") {
                    mReferenceSpectrum = correctedSpectrum.CorrectedSpectrum;
                }

                // If both a dark and reference are available...
                if (mDarkSpectrum != null && mReferenceSpectrum != null)
                {
                    // Compute the dark corrected reference spectrum
                    mReferenceCorrectedSpectrum = new float[mDarkSpectrum.Length];
                    int iLen = mDarkSpectrum.Length < mReferenceSpectrum.Length ? mDarkSpectrum.Length : mReferenceSpectrum.Length;
                    for (int i=0; i < iLen; i++)
                    {
                        mReferenceCorrectedSpectrum[i] = mReferenceSpectrum[i] - mDarkSpectrum[i];
                    }
                }
            }

            Invoke((MethodInvoker)delegate
            {
                OnSingleSpectrumComplete(spectrumType);  // Invoke on UI thread
            });
        }

        private void OnSingleSpectrumComplete(string spectrumType)
        {

            if (spectrumType == "dark")
            {
                buttonTakeDark.Enabled = true;

                mDarkSpectrumTime = DateTime.Now;

                labelDarkStatus.Text = String.Format("00:00:00");
                labelDarkStatus.Visible = true;

                updateDefaultSaveType();

            }
            else if (spectrumType == "reference")
            {
                buttonTakeReference.Enabled = true;

                mReferenceSpectrumTime = DateTime.Now;

                labelReferenceStatus.Text = String.Format("00:00:00");
                labelReferenceStatus.Visible = true;

                updateDefaultSaveType();

            }
        }

        //
        // Invoked upon taking or clearing a dark or reference spectrum
        //
        protected void updateDefaultSaveType()
        {
            int newIndex = comboBoxSaveType.SelectedIndex;

            if (mDarkSpectrum == null) {
                newIndex = 0;
            }
            else if (mReferenceSpectrum == null) {
                newIndex = 1;
            }
            else if (newIndex < 2) {
                newIndex = 2;
            }

            if (newIndex != comboBoxSaveType.SelectedIndex)
            {
                comboBoxSaveType.SelectedIndex = newIndex;

                updateSaveFilename();
            }
            else
            {
                updateSaveTypeComment();
            }

            updateSaveFilename();
        }

        protected void updateSaveTypeComment()
        {
            labelSaveTypeStatus.Visible = false;
            if (comboBoxSaveType.SelectedIndex > 0)
            {
                if (mDarkSpectrum == null)
                {
                    labelSaveTypeStatus.Text = "no dark available";
                    labelSaveTypeStatus.Visible = true;
                }
                else if (mReferenceSpectrum == null && comboBoxSaveType.SelectedIndex > 1)
                {
                    labelSaveTypeStatus.Text = "no reference available";
                    labelSaveTypeStatus.Visible = true;
                }
            }
        }

        //
        // Update the save filename in response to changes in the data type being saved
        //
        protected void updateSaveFilename()
        {
            int saveTypeIndex = comboBoxSaveType.SelectedIndex;
            mSaveSpectrumType = "spectrum";
            if (mDarkSpectrum != null && saveTypeIndex > 0)
            {
                mSaveSpectrumType = "darkcorrected";
                if (mReferenceSpectrum != null && saveTypeIndex > 1)
                {
                    mSaveSpectrumType = (saveTypeIndex == 2) ? "absorbance" : "transmission";
                }
            }

            string baseName = (mSerialNum == null || mSerialNum.Length == 0) ? "fxstreamer" : mSerialNum;
            textBoxSaveFilename.Text = mSaveDir + baseName + "_" + mSaveSpectrumType + DateTime.Now.ToString("_ddMMMyyyy_HHmmss") + ".csv";
        }

        private void comboBoxSaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateSaveTypeComment();
            updateSaveFilename();
        }

        private void checkBoxLampEnable_CheckedChanged(object sender, EventArgs e)
        {
            mLampEnable = checkBoxLampEnable.Checked ? (byte)1 : (byte)0;

            Thread lampThread = new Thread(() => doLampEnable());
            lampThread.Start();
        }

        //
        // Update the lamp enable state immediately (don't wait for an acquisition to start)
        //
        private void doLampEnable()
        {
            OBPSetLampEnable lampEnable = new OBPSetLampEnable(mActiveIO, mRequest, mResponse);
            lampEnable.LampEnable = mLampEnable;
            lampEnable.Send();
        }

        private void buttonSaveDir_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (mSaveDir != null && mSaveDir.Length > 0)
            {
                folderBrowserDialog.SelectedPath = mSaveDir;
            }

            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                mSaveDir = folderBrowserDialog.SelectedPath;
                if (!mSaveDir.EndsWith("\\"))
                {
                    mSaveDir += "\\";
                }

                string saveFilename = textBoxSaveFilename.Text;
                int fileIndex = saveFilename.LastIndexOf('\\');
                if (fileIndex > 0)
                {
                    saveFilename = saveFilename.Substring(fileIndex + 1);
                }

                textBoxSaveFilename.Text = mSaveDir + saveFilename;
            }
        }

        //
        // Update the save directory in response to updates (manual or automated)
        //
        private void textBoxSaveFilename_TextChanged(object sender, EventArgs e)
        {
            string saveFilename = textBoxSaveFilename.Text;
            int fileIndex = saveFilename.LastIndexOf('\\');
            if (fileIndex > 0)
            {
                mSaveDir = saveFilename.Substring(0, fileIndex + 1);
            }
            else
            {
                mSaveDir = "";
            }

        }

        private void updateAcquisitionButtonState()
        {
            bool bActiveIO = (mActiveIO != null);

            buttonStartAcquisition.Enabled = bActiveIO && !mAcquiringSpectra;
            buttonStartAcquisition.BackColor = buttonStartAcquisition.Enabled ? Color.ForestGreen : Color.Gray;

            buttonAbortAcquisition.Enabled = bActiveIO && mAcquiringSpectra;
            buttonAbortAcquisition.BackColor = buttonAbortAcquisition.Enabled ? Color.Maroon : Color.Gray;

            buttonRescan.Enabled = !mAcquiringSpectra;
            buttonConnect.Enabled = !mAcquiringSpectra;
            buttonTakeDark.Enabled = !mAcquiringSpectra;
            buttonClearDark.Enabled = !mAcquiringSpectra;
            buttonTakeReference.Enabled = !mAcquiringSpectra;
            buttonClearReference.Enabled = !mAcquiringSpectra;
            buttonUpdateSpectraInBuffer.Enabled = !mAcquiringSpectra;
            buttonUpdateSpectraInBuffer.Enabled = !mAcquiringSpectra;
            buttonClearBuffer.Enabled = !mAcquiringSpectra;
            buttonSaveDir.Enabled = !mAcquiringSpectra;

            labelCurSpectraPerSec.Visible = true;
            if (mAcquiringSpectra)
            {
                labelCurSpectraPerSec.Text = "0 spectra/sec";
                labelFileSaveError.Visible = false;
            }
        }

        #endregion

        #region high_speed_acquisition
        //========================= high_speed_acquisition =========================

        private void buttonStartAcquisition_Click(object sender, EventArgs e)
        {
            mAcquiringSpectra = true;
            mStopAcquiring = false;

            updateAcquisitionButtonState();

            // Update the pixel range to be saved to file
            updateFileSaveRange();

            // Ensure all the setting variables are sync'd to the latest UI state
            // (the UI controls can't be acccessed directly from a non-UI thread)
            syncUISettings();

            // Perform save spectrum in a new thread
            Thread saveThread = new Thread(() => doComputeSaveSpectrum());
            saveThread.Start();

            // Perform the acquisition in a new thread
            Thread acquisitionThread = new Thread(() => doHighSpeedAcquisition());
            acquisitionThread.Start();

        }

        private void buttonAbortAcquisition_Click(object sender, EventArgs e)
        {
            mStopAcquiring = true;
        }

        private void onAcquisitionComplete()
        {
            mAcquiringSpectra = false;

            updateAcquisitionButtonState();
        }

        private void doHighSpeedAcquisition()
        {
            try
            {
                mReceiveFailures = 0;

                // Sync the spectrometer settings
                doSyncSpectrometerSettings(false);

                // Optionally clear the buffer
                if (mClearBufferBeforeTest)
                {
                    OBPClearBufferedSpectra clearBufferedSpectra = new OBPClearBufferedSpectra(mActiveIO, mRequest, mResponse);
                    clearBufferedSpectra.Response.AckRequest = true;
                    clearBufferedSpectra.Send();
                }

                //
                // Allocate the IO buffers
                //
                allocateIOBuffers();

                // Initialize the statistic counters
                mTotalRequests = 0;
                mTotalSpectraReceived = 0; ;
                mTotalAcquireTime = 0;
                mCurSpectraPerSec = 0;
                mMinSupplyCount = int.MaxValue;
                mTotalBytes = 0;

                // Initialize the acqusition end condition
                long acquisitionDurationMS = 0;
                if (mAcquisitionDurationUnits.Equals("seconds"))
                {
                    acquisitionDurationMS = (long)(mAcquisitionDuration * 1000);
                }
                else if (mAcquisitionDurationUnits.Equals("minutes"))
                {
                    acquisitionDurationMS = (long)(mAcquisitionDuration * 60 * 1000);
                }
                else if (mAcquisitionDurationUnits.Equals("hours"))
                {
                    acquisitionDurationMS = (long)(mAcquisitionDuration * 60 * 60 * 1000);
                }

                long acquisitionDurationSpectra = 0;
                if (mAcquisitionDurationUnits.Equals("spectra"))
                {
                    acquisitionDurationSpectra = (long)mAcquisitionDuration;
                }

                // Allocate and start the timer
                System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
                stopWatch.Start();

                // Prime the pump with the first spectrum request
                OBPGetRawSpectrumWithMetadata spectrumOut = mSupplyQueue.Dequeue();
                spectrumOut.NumSpectraPerRequest = mNumSpectraPerRequest;
                spectrumOut.SendOnly();
                mTotalRequests++;

                OBPGetRawSpectrumWithMetadata spectrumIn = spectrumOut;

                //
                // The acquisition loop
                //
                while (!mStopAcquiring)
                {
                    // Spin lock (waiting) for the next available spectrum
                    while (mSupplyQueue.Count == 0 && !mStopAcquiring)
                    {
                        mSpinCounter++;  // Ideally we're never spinning but it could happen if the compute and save thread is too slow
                    }

                    if (mStopAcquiring)
                    {
                        break;
                    }

                    lock (mSupplyQueue)
                    {
                        spectrumOut = mSupplyQueue.Dequeue();
                    }

                    if (mSupplyQueue.Count < mMinSupplyCount)
                    {
                        mMinSupplyCount = mSupplyQueue.Count;  // Track the minimum supply queue count
                    }

                    // Send the next spectrum request
                    spectrumOut.NumSpectraPerRequest = mNumSpectraPerRequest;
                    spectrumOut.SendOnly();
                    mTotalRequests++;

                    mTotalAcquireTime = stopWatch.ElapsedMilliseconds;

                    // Receive the next spectrum response
                    receiveNextSpectrum(spectrumIn);

                    spectrumIn = spectrumOut;

                    // Check the end condition
                    if (acquisitionDurationMS > 0 && mTotalAcquireTime >= acquisitionDurationMS)
                    {
                        mStopAcquiring = true;
                    }
                    else if (acquisitionDurationSpectra > 0 && mTotalSpectraReceived >= acquisitionDurationSpectra)
                    {
                        mStopAcquiring = true;
                    }
                }

                // Receive the last spectrum response
                receiveNextSpectrum(spectrumIn);

                // This is the end of acquisition
                mTotalAcquireTime = stopWatch.ElapsedMilliseconds;

                // Wait for the compute and save thread to complete
                long waitForSave = 0;
                while (mIsComputeAndSave && waitForSave < 500)
                {
                    waitForSave = stopWatch.ElapsedMilliseconds - mTotalAcquireTime;
                    Thread.Sleep(0);  // context switch
                }

                stopWatch.Stop();

                // Record the total compute and save time
                // Note: There's a minimum 100ms wait after acquisition completes
                mTotalSaveTime = stopWatch.ElapsedMilliseconds;

                // Flush any extraneous bytes (should be zero)
                mBytesFlushed = doFlush();

                // Release the IO buffers
                mSupplyQueue.Clear();
                mSaveQueue.Clear();

                // Encourage a memory cleanup
                System.GC.Collect();

            }
            finally {

                if (!mIsClosing)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        onAcquisitionComplete();  // Invoke on UI thread
                    });
                }
            }

        }

        private void receiveNextSpectrum(OBPGetRawSpectrumWithMetadata spectrumIn)
        {
            // Receive the next spectrum response
            spectrumIn.ReceiveOnly();
            if (!spectrumIn.IsSuccess)
            {
                mReceiveFailures++; // Note failures for info purposes... some are to be expected based on load

                mTotalBytes += 64;  // Assume the failed response was received (this seems to be the case with OBP error code 13)

                // Send immediately back to the supply queue
                lock (mSupplyQueue)
                {
                    mSupplyQueue.Enqueue(spectrumIn);
                }
            }
            else
            {
                // Track the total bytes received
                mTotalBytes += spectrumIn.Response.BytesRemaining + 44;

                // Queue for the compute and save thread
                lock (mSaveQueue)
                {
                    mSaveQueue.Enqueue(spectrumIn);
                }
            }


        }

        private void doComputeSaveSpectrum()
        {
            mIsComputeAndSave = true;

            StringBuilder sb = new StringBuilder(mPixels.Length * 10 + 128);
            StreamWriter outFile = null;

            bool bSaveToFile = mSaveToFile;
            bool bFileExists = false;

            try
            {
                if (bSaveToFile)
                {
                    try
                    {
                        bFileExists = File.Exists(mSaveFilename);
                        bool bAppend = true;

                        outFile = new System.IO.StreamWriter(mSaveFilename, bAppend);
                        if (outFile == null)
                        {
                            bSaveToFile = false;
                        }
                    }
                    catch (Exception)
                    {
                        bSaveToFile = false;

                        // Indicate an error occurred
                        Invoke((MethodInvoker)delegate
                        {
                            // Invoke on UI thread
                            onAcquisitionError("File save error.");
                        });
                    }
                }

                if (bSaveToFile && !bFileExists)
                {
                    // Write the CSV header line
                    sb.Append("\"Counter\",\"MicroSec\",\"IntegTime\",\"ScansToAvg\"");
                    for (int i = mSaveStartPixel; i <= mSaveEndPixel; i++)
                    {
                        // Output wavelengths on header line
                        sb.Append(",\"").Append(mWavelengths[i]).Append("\"");
                    }
                    outFile.WriteLine(sb.ToString());
                    sb.Clear();
                }

                OBPGetRawSpectrumWithMetadata saveSpectrum;

                // The computed result (raw spectrum, dark corrected, transmission, or absorbance)
                float[] curResult = new float[mNumPixels];
                int[] intResult = new int[mNumPixels];
                float[] chartSpectrum = null;

                bool bFlushing = true;
                long flushStart = 0;

                // Allocate and start the timer
                System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
                stopWatch.Start();

                // Timer vars to allow processing every 1/10 and 1 second
                long oneTenthStart = stopWatch.ElapsedMilliseconds;
                long oneSecondStart = oneTenthStart;
                long oneSecondNumSpectra = 0;

                //
                // While acquiring or spectrum to process...
                //
                while (!mStopAcquiring || bFlushing)
                {
                    if (mSaveQueue.Count > 0)
                    {
                        // Get the next spectrum response
                        lock (mSaveQueue)
                        {
                            saveSpectrum = mSaveQueue.Dequeue();
                        }

                        RawSpectrumWithMetadataBuffer[] spectrumA = saveSpectrum.Spectrum;

                        int numSpectraReceived = spectrumA.Length;

                        mTotalSpectraReceived += numSpectraReceived;
                        oneSecondNumSpectra += numSpectraReceived;

                        // If absorbance...
                        if (mSaveSpectrumType.StartsWith("abs") && mDarkSpectrum != null && mReferenceCorrectedSpectrum != null)
                        {
                            for (int s = 0; s < numSpectraReceived; s++)
                            {
                                RawSpectrumWithMetadataBuffer spectrum = spectrumA[s];

                                int pLen = spectrum.NumPixels < mNumPixels ? spectrum.NumPixels : mNumPixels;
                                double ratioVal;
                                for (int p = 0; p < pLen; p++)
                                {
                                    // Avoid a divide by zero
                                    if (mReferenceCorrectedSpectrum[p] == 0)
                                    {
                                        curResult[p] = 0.0f;  // Could be set to MAX_ABSORBANCE or flag an error, etc.
                                    }
                                    else
                                    {
                                        ratioVal = (spectrum.GetPixelU16(p) - mDarkSpectrum[p]) / mReferenceCorrectedSpectrum[p];
                                        // Avoid NaN log10...
                                        if (ratioVal <= 0)
                                        {
                                            curResult[p] = 0.0f;  // Could be set to MAX_ABSORBANCE or flag an error, etc.
                                        }
                                        else
                                        {
                                            curResult[p] = (float)-Math.Log10(ratioVal);
                                        }
                                    }

                                    if (chartSpectrum != null)
                                    {
                                        chartSpectrum[p] = curResult[p];
                                    }
                                }

                                if (bSaveToFile)
                                {
                                    doSaveSpectrum(sb, spectrum, curResult, outFile);
                                }
                            }
                        }
                        // If transmission...
                        else if (mSaveSpectrumType.StartsWith("tra") && mDarkSpectrum != null && mReferenceCorrectedSpectrum != null)
                        {
                            for (int s = 0; s < numSpectraReceived; s++)
                            {
                                RawSpectrumWithMetadataBuffer spectrum = spectrumA[s];

                                int pLen = spectrum.NumPixels < mNumPixels ? spectrum.NumPixels : mNumPixels;
                                for (int p = 0; p < pLen; p++)
                                {
                                    // Avoid a divide by zero
                                    if (mReferenceCorrectedSpectrum[p] == 0)
                                    {
                                        curResult[p] = 100.0f;  // Could be set to zero or flag an error, etc.
                                    }
                                    else
                                    {
                                        curResult[p] = 100.0f * ((spectrum.GetPixelU16(p) - mDarkSpectrum[p]) / mReferenceCorrectedSpectrum[p]);
                                    }

                                    if (chartSpectrum != null)
                                    {
                                        chartSpectrum[p] = curResult[p];
                                    }
                                }

                                if (bSaveToFile)
                                {
                                    doSaveSpectrum(sb, spectrum, curResult, outFile);
                                }
                            }
                        }
                        // If dark corrected...
                        else if (mSaveSpectrumType.StartsWith("dar") && mDarkSpectrum != null)
                        {
                            for (int s = 0; s < numSpectraReceived; s++)
                            {
                                RawSpectrumWithMetadataBuffer spectrum = spectrumA[s];

                                int pLen = spectrum.NumPixels < mNumPixels ? spectrum.NumPixels : mNumPixels;
                                for (int p = 0; p < pLen; p++)
                                {
                                    intResult[p] = spectrum.GetPixelU16(p) - mDarkSpectrum[p];
                                    if (chartSpectrum != null)
                                    {
                                        chartSpectrum[p] = intResult[p];
                                    }
                                }

                                if (bSaveToFile)
                                {
                                    doSaveSpectrum(sb, spectrum, intResult, outFile);
                                }
                            }

                        }
                        // If raw spectrum...
                        else 
                        {
                            for (int s = 0; s < numSpectraReceived; s++)
                            {
                                RawSpectrumWithMetadataBuffer spectrum = spectrumA[s];

                                int pLen = spectrum.NumPixels < mNumPixels ? spectrum.NumPixels : mNumPixels;
                                for (int p = 0; p < pLen; p++)
                                {
                                    intResult[p] = spectrum.GetPixelU16(p);
                                    if (chartSpectrum != null)
                                    {
                                        chartSpectrum[p] = intResult[p];
                                    }
                                }

                                if (bSaveToFile)
                                {
                                    doSaveSpectrum(sb, spectrum, intResult, outFile);
                                }
                            }

                        }

                        if (chartSpectrum != null)
                        {
                            mResultSpectrum = chartSpectrum;
                            chartSpectrum = null;
                        }

                            // Return the spectrum to the supply queue
                            lock (mSupplyQueue)
                        {
                            mSupplyQueue.Enqueue(saveSpectrum);

                        }
                    }

                    long curTime = stopWatch.ElapsedMilliseconds;

                    long oneSecondDeltaTime = curTime - oneSecondStart;
                    if (oneSecondDeltaTime >= 1000)
                    {
                        //
                        // --- Every 1 second ----
                        //

                        mCurSpectraPerSec = oneSecondNumSpectra * 1000 / oneSecondDeltaTime;

                        // Reset the one second data
                        oneSecondNumSpectra = 0;
                        oneSecondStart = curTime;
                    }

                    if ((curTime - oneTenthStart) >= 100)
                    {
                        //
                        // --- Every 1/10 second ---
                        //

                        // Allocate a new spectrum to be charted
                        chartSpectrum = new float[curResult.Length];

                        oneTenthStart = curTime;
                    }

                    // Allow some time to flush pending acquisitions and queued saved spectra
                    // after acquisition has stopped
                    if (mStopAcquiring || flushStart > 0)
                    {
                        if (flushStart == 0)
                        {
                            flushStart = curTime;
                        }
                        else
                        {
                            long flushDeltaTime = curTime - flushStart;

                            // If after 100ms there are no queued spectra...
                            if (flushDeltaTime >= 100 && mSaveQueue.Count == 0)
                            {
                                bFlushing = false;
                            }
                            // Not matter what after 500ms... 
                            else if (flushDeltaTime >= 500)
                            {
                                bFlushing = false;
                            }
                        }
                    }

                }

            }
            catch (Exception)
            {
                mStopAcquiring = true;

                // Indicate an error occurred
                Invoke((MethodInvoker)delegate
                {
                    // Invoke on UI thread
                    onAcquisitionError("Unexpectation exception in compute and save thread.");
                });
            }
            finally
            {
                if (outFile != null)
                {
                    outFile.Close();
                }
            }

            mIsComputeAndSave = false;

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
        protected void doSaveSpectrum(StringBuilder sb, RawSpectrumWithMetadataBuffer spectrum, float[] curResult, StreamWriter outFile)
        {
            // Write the metadata
            sb.Append(spectrum.SpectrumCounter).Append(",");
            sb.Append(spectrum.MicrosecondCounter).Append(",");
            sb.Append(spectrum.IntegrationTime).Append(",");
            sb.Append(spectrum.ScansAveraged == 0 ? 1 : spectrum.ScansAveraged);

            // Write the data
            for (int i = mSaveStartPixel; i <= mSaveEndPixel; i++)
            {
                sb.Append(",").Append(curResult[i].ToString());
            }

            outFile.WriteLine(sb.ToString());
            sb.Clear();
        }

        //
        // Append a result to the save file
        //
        protected void doSaveSpectrum(StringBuilder sb, RawSpectrumWithMetadataBuffer spectrum, int[] curResult, StreamWriter outFile)
        {
            // Write the metadata
            sb.Append(spectrum.SpectrumCounter).Append(",");
            sb.Append(spectrum.MicrosecondCounter).Append(",");
            sb.Append(spectrum.IntegrationTime).Append(",");
            sb.Append(spectrum.ScansAveraged == 0 ? 1 : spectrum.ScansAveraged);

            // Write the data
            for (int i = mSaveStartPixel; i <= mSaveEndPixel; i++)
            {
                sb.Append(",").Append(curResult[i]);
            }

            outFile.WriteLine(sb.ToString());
            sb.Clear();
        }

        private void onAcquisitionError(string errorMessage)
        {
            labelFileSaveError.Visible = true;
            labelFileSaveError.Text = errorMessage;
        }

        private void buttonUpdateFileTime_Click(object sender, EventArgs e)
        {
            updateSaveFilename();
        }

        #endregion

    }
}
