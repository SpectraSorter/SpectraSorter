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

using OBP_Library;
using spectra.devices;
using spectra.processing;
using spectra.state;
using spectra.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spectra.ui
{
    public partial class MainWindow : Form
    {
        //
        // Periodic timer-driven UI updates
        //
        private void UIUpdateTimer(Object myObject, EventArgs myEventArgs)
        {
            if (mTimerBusy == true)
            {
                return;
            }

            try
            {
                mTimerBusy = true;

                // Update info on data acquired
                this.labelCurSpectraPerSec.Text = $"{mCurSpectraPerSec} spectra/sec";
                labelTotalBytes.Text = Utils.ToByteString(mTotalReceivedBytes);
                labelSavedBytes.Text = Utils.ToByteString(mTotalSavedBytes);
                labelSavedSpectra.Text = mTotalSpectraSaved.ToString("N0", CultureInfo.InvariantCulture);
                labelTotalSpectra.Text = mTotalSpectraReceived.ToString("N0", CultureInfo.InvariantCulture);
                labelComputedSpectra.Text = mTotalSpectraComputed.ToString("N0", CultureInfo.InvariantCulture);
                labelTotalRequests.Text = mTotalRequests.ToString("N0", CultureInfo.InvariantCulture);
                labelTotalTime.Text = Utils.GetAgeString(new TimeSpan(mTotalAcquireTime * TimeSpan.TicksPerMillisecond));
                double totalAcquireSeconds = mTotalAcquireTime / 1000.0;
                labelSpectraPerSec.Text = totalAcquireSeconds == 0 ? "0" : (mTotalSpectraReceived / totalAcquireSeconds).ToString("0.0", CultureInfo.InvariantCulture);
                labelSpectraPerRequest.Text = mTotalRequests == 0 ? "0" : (((double)mTotalSpectraReceived) / mTotalRequests).ToString("0.0", CultureInfo.InvariantCulture);

                // Update info on hits
                this.labelTotalHits.Text = $"{State.Instance.TotalHitNumber} hits";

                DateTime curTime = DateTime.Now;

                if (SpectrumProcessor.Instance.DarkSpectrum != null)
                {
                    labelDarkStatus.Text = Utils.GetAgeString(curTime - SpectrumProcessor.Instance.DarkSpectrumTime);
                }

                if (SpectrumProcessor.Instance.ReferenceSpectrum != null)
                {
                    labelReferenceStatus.Text = Utils.GetAgeString(curTime - SpectrumProcessor.Instance.ReferenceSpectrumTime);
                }

                // Plot
                this.mMainPlotter.Plot();
            }
            finally
            {
                mTimerBusy = false;
            }
        }

        //
        // High-speed acquisition thread
        //
        private void doHighSpeedAcquisition()
        {
            // Experiment type
            bool currentExperimentIsAccumulation = State.Instance.IsPerformingAccumulationAcquisition;

            // Initialize the acquisition end condition
            long acquisitionDurationMS = 0;
            long acquisitionDurationSpectra = 0;

            // Which acquisition time should we use?
            if (State.Instance.IsPerformingAccumulationAcquisition == true)
            {
                // Use the accumulation time for accumulation experiments to stop the acquisition.
                // There is no limit in the number of spectra
                acquisitionDurationMS = (long)(SettingsManager.StaticReferenceAccumulateTime * 1000);
                acquisitionDurationSpectra = 0;
            }
            else
            {
                // Use the standard acquisition duration.

                // Initialize the acquisition end condition
                acquisitionDurationMS = 0;
                if ((Options.AcquisitionDurationUnits)SettingsManager.AcquisitionDurationIndex == Options.AcquisitionDurationUnits.SECONDS)
                {
                    acquisitionDurationMS = (long)(SettingsManager.AcquisitionDuration * 1000);
                }
                else if ((Options.AcquisitionDurationUnits)SettingsManager.AcquisitionDurationIndex == Options.AcquisitionDurationUnits.MINUTES)
                {
                    acquisitionDurationMS = (long)(SettingsManager.AcquisitionDuration * 60 * 1000);
                }
                else if ((Options.AcquisitionDurationUnits)SettingsManager.AcquisitionDurationIndex == Options.AcquisitionDurationUnits.HOURS)
                {
                    acquisitionDurationMS = (long)(SettingsManager.AcquisitionDuration * 60 * 60 * 1000);
                }

                if ((Options.AcquisitionDurationUnits)SettingsManager.AcquisitionDurationIndex == Options.AcquisitionDurationUnits.SPECTRA)
                {
                    acquisitionDurationSpectra = (long)SettingsManager.AcquisitionDuration;
                }
            }

            if (SettingsManager.SpectrumThresholdingEnabled && mArduino.IsConnected())
            {
                // Inform Arduino that a new acquisition is starting
                mArduino.SendCommand(Arduino.COMMANDS.START);
            }

            try
            {
                mReceiveFailures = 0;

                // Sync the spectrometer settings
                doSyncSpectrometerSettings(false);

                // Optionally clear the buffer
                if (mClearBufferBeforeTest)
                {
                    mOceanFX.ClearBufferOnDevice(withAckResponse: true);
                }

                //
                // Allocate the IO buffers
                //
                allocateIOBuffers();

                // Initialize the statistic counters
                mTotalRequests = 0;
                mTotalSpectraReceived = 0;
                mTotalAcquireTime = 0;
                mCurSpectraPerSec = 0;
                mMinSupplyCount = int.MaxValue;
                mTotalReceivedBytes = 0;

                // Allocate and start the timer
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                // Prime the pump with the first spectrum request
                OBPGetRawSpectrumWithMetadata spectrumOut = mSupplyQueue.Dequeue();
                spectrumOut.NumSpectraPerRequest = mOceanFX.NumSpectraPerRequest;
                spectrumOut.SendOnly();
                mTotalRequests++;

                OBPGetRawSpectrumWithMetadata spectrumIn = spectrumOut;

                //
                // The acquisition loop
                //
                while (!State.Instance.IsStopAcquiringRequested)
                {
                    // Spin lock (waiting) for the next available spectrum
                    while (mSupplyQueue.Count == 0 && !State.Instance.IsStopAcquiringRequested)
                    {
                        mSpinCounter++;  // Ideally we're never spinning but it could happen if the compute and save thread is too slow
                    }
                    mSpinCounter = 0;

                    if (State.Instance.IsStopAcquiringRequested)
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
                    spectrumOut.NumSpectraPerRequest = mOceanFX.NumSpectraPerRequest;
                    spectrumOut.SendOnly();
                    mTotalRequests++;

                    mTotalAcquireTime = stopWatch.ElapsedMilliseconds;

                    // Receive the next spectrum response
                    receiveNextSpectrum(spectrumIn);

                    spectrumIn = spectrumOut;

                    // Check the end condition
                    if (acquisitionDurationMS > 0 && mTotalAcquireTime >= acquisitionDurationMS)
                    {
                        State.Instance.IsStopAcquiringRequested = true;
                    }
                    else if (acquisitionDurationSpectra > 0 && mTotalSpectraReceived >= acquisitionDurationSpectra)
                    {
                        State.Instance.IsStopAcquiringRequested = true;
                    }
                }

                // Receive the last spectrum response
                receiveNextSpectrum(spectrumIn);

                // This is the end of acquisition
                mTotalAcquireTime = stopWatch.ElapsedMilliseconds;

                // Conclude acquisition
                if (State.Instance.IsPerformingAccumulationAcquisition == true)
                {
                    State.Instance.IsPerformingAccumulationAcquisition = false;
                }
                if (State.Instance.IsPerformingStandardAcquisition == true)
                {
                    State.Instance.IsPerformingStandardAcquisition = false;
                }

                // Stop acquisition timer
                stopWatch.Stop();

                // Record the total compute and save time
                // Note: There's a minimum 100ms wait after acquisition completes
                mTotalSaveTime = stopWatch.ElapsedMilliseconds;

                // Flush any extraneous bytes (should be zero)
                mBytesFlushed = mOceanFX.Flush();

                // Wait for the compute and save thread to complete
                long waitForSave = 0;
                while (
                    ((State.Instance.IsPerformingCompute || mComputeQueue.Count > 0) ||
                    (State.Instance.IsPerformingSave || mSaveQueue.Count > 0)) ||
                    (State.Instance.IsPerformingAccumulationAcquisition &&
                    waitForSave < 500)
                    )
                {
                    waitForSave = stopWatch.ElapsedMilliseconds - mTotalAcquireTime;
                    Thread.Sleep(0);  // context switch
                }

                // Release the IO buffers
                mSupplyQueue.Clear();
                mComputeQueue.Clear();
                mSaveQueue.Clear();

                // Encourage a memory cleanup
                GC.Collect();
            }
            finally
            {
                if (!State.Instance.IsClosing)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        if (currentExperimentIsAccumulation == true)
                        {
                            // Complete acquisition when running for 
                            // reference spectrum accumulation
                            onAcquisitionForAccumulationComplete();   // Invoke on UI thread
                        }
                        else
                        {
                            // Complete standard acquisition
                            onAcquisitionComplete();  // Invoke on UI thread
                        }

                        arduinoParametersControl.UpdateTriggerCounter();
                    });
                }
            }
        }

        //
        // Accumulation experiment thread
        //
        private void doAccumulateSpectra()
        {
            if (mOceanFX == null)
            {
                return;
            }

            State.Instance.IsPerformingStandardAcquisition = false;
            State.Instance.IsPerformingAccumulationAcquisition = true;

            try
            {
                OBPGetRawSpectrumWithMetadata saveSpectrum;

                // Retrieve last spectrum
                int[] intResult = new int[mOceanFX.NumPixels];

                // Allocate and start the timer
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                // Timer vars to allow processing every 1/10 and 1 second
                long oneTenthStart = stopWatch.ElapsedMilliseconds;
                long oneSecondStart = oneTenthStart;
                long oneSecondNumSpectra = 0;

                //
                // While acquiring or spectrum to process...
                //

                long waitForAccumulate = 0;
                int processedSpectra = 0;
                while (!State.Instance.IsStopAcquiringRequested || waitForAccumulate < 500 ||
                    (mComputeQueue.Count > 0 || processedSpectra < mTotalSpectraReceived))
                {
                    if (mComputeQueue.Count > 0)
                    {
                        // Get the next spectrum response
                        lock (mComputeQueue)
                        {
                            saveSpectrum = mComputeQueue.Dequeue();
                        }

                        RawSpectrumWithMetadataBuffer[] spectrumA = saveSpectrum.Spectrum;

                        int numSpectraReceived = spectrumA.Length;

                        mTotalSpectraReceived += numSpectraReceived;
                        oneSecondNumSpectra += numSpectraReceived;

                        for (int s = 0; s < numSpectraReceived; s++)
                        {
                            RawSpectrumWithMetadataBuffer spectrum = spectrumA[s];

                            int pLen = spectrum.NumPixels < mOceanFX.NumPixels ? spectrum.NumPixels : mOceanFX.NumPixels;
                            for (int p = 0; p < pLen; p++)
                            {
                                intResult[p] = spectrum.GetPixelU16(p);
                            }

                            // Filter?
                            if (SettingsManager.SpectrumFilteringEnabled && !SpectrumFilterer.Instance.IsUnit)
                            {
                                intResult = SpectrumFilterer.Instance.Convolve(intResult, full: false, symmetric: true);
                            }

                            // Enqueue
                            SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Enqueue(Array.ConvertAll(intResult, x => (float)x));

                            // Update count
                            processedSpectra++;
                        }

                        // Return the spectrum to the supply queue
                        lock (mSupplyQueue)
                        {
                            mSupplyQueue.Enqueue(saveSpectrum);
                        }
                    }

                    // Keep the time
                    waitForAccumulate = stopWatch.ElapsedMilliseconds - mTotalAcquireTime;

                    // Context switch
                    Thread.Sleep(0);
                }
            }
            catch (Exception)
            {
                State.Instance.IsStopAcquiringRequested = true;

                // Indicate an error occurred
                Invoke((MethodInvoker)delegate
                {
                    // Invoke on UI thread
                    onAcquisitionError("Unexpected exception in accumulate spectra thread.");
                });
            }
            finally
            {
            }
        }

        //
        // Save spectrum thread
        //
        private void doSaveSpectrum()
        {
            // Set performing save flag to true
            State.Instance.IsPerformingSave = true;

            // Fix this so that it can't be changed during an acquisition
            int savingWavelengthStep = SettingsManager.SaveStepPixel;

            // Prepare the file
            StringBuilder sb = new StringBuilder(SpectrumProcessor.Instance.Pixels.Length * 10 + 128);
            StreamWriter outFile = null;
            bool bFileExists = false;

            // Keep track of all spectra extracted from the queue (whether saved or not)
            long processedSpectra = 0;

            // Cache the file name. During the whole saving process we will work with this 
            // copy, so that changes to settings will be ignored.
            string saveFileNameCache = SettingsManager.SaveFileName;

            try
            {
                // Cache a copy of the wavelength array to save. During the whole saving process 
                // we will work with this copy, so that changes to settings will be ignored.
                double[] wavelengthsCache = new double[SpectrumProcessor.Instance.Wavelengths.Length];
                Array.Copy(SpectrumProcessor.Instance.Wavelengths, 0, wavelengthsCache, 0, SpectrumProcessor.Instance.Wavelengths.Length);
                int saveStartPixelCache = SettingsManager.SaveStartPixel;
                int saveEndPixelCache = SettingsManager.SaveEndPixel;

                // Cache a copy of the wavelength indices to save (it can be an empty list).
                // During the whole saving process we will work with this copy, so that changes
                // to settings will be ignored.
                List<Wavelength> wavelengthsToSaveCache = WavelengthManager.Clone(WavelengthManager.Instance.WavelengthsForSaving);

                // Cache the result spectrum type. During the whole saving process we will work with 
                // this copy, so that changes to settings will be ignored.
                Options.ResultSpectrumType resultSpectrumTypeCache = SettingsManager.ResultSpectrumType;

                // Do we save the whole spectrum or just a few selected wavelengths? Also perform 
                // consistency check.
                bool bSaveOnlySelectedWavelengths = SettingsManager.SaveWavelengthRange == false;
                if (bSaveOnlySelectedWavelengths == true && wavelengthsToSaveCache.Count == 0)
                {
                    SettingsManager.SaveToFile = false;
                }

                // Reset the number of saved bytes and spectra
                mTotalSavedBytes = 0;
                mTotalSpectraSaved = 0;

                // Whether saving is currently on or not, we open the file; since 
                // saving could be enabled at any time during the acquisition.

                // If the file exists append
                bFileExists = File.Exists(saveFileNameCache);
                bool bAppend = true;

                // Open stream for writing
                outFile = new StreamWriter(saveFileNameCache, bAppend);

                if (outFile == null)
                {
                    // Indicate an error occurred
                    Invoke((MethodInvoker)delegate
                    {
                        // Invoke on UI thread
                        onAcquisitionError("File save error.");
                    });

                    // Do not try to save
                    SettingsManager.SaveToFile = false;
                }
                else
                {
                    //
                    // Write the file header
                    //
                    if (bSaveOnlySelectedWavelengths == true)
                    {
                        // Write the CSV header line
                        sb.Append("Counter;MicroSec;IntegTime;ScansToAvg;Trigger");

                        // Output only threshold wavelengths on header line
                        for (int i = 0; i < wavelengthsToSaveCache.Count; i++)
                        {
                            sb.Append(";").Append(wavelengthsCache[wavelengthsToSaveCache[i].Index]);
                        }
                        outFile.WriteLine(sb.ToString());
                        sb.Clear();
                    }
                    else
                    {
                        // Write the CSV header line
                        sb.Append("Counter;MicroSec;IntegTime;ScansToAvg;Trigger");
                        for (int i = saveStartPixelCache; i <= saveEndPixelCache; i+=savingWavelengthStep)
                        {
                            // Output all wavelengths on header line
                            sb.Append(";").Append(wavelengthsCache[i]);
                        }
                        outFile.WriteLine(sb.ToString());
                        sb.Clear();
                    }
                }

                // Declare our spectrum queue for saving
                SpectrumForSavingQueueObject saveSpectrumQueue;

                //
                // Process the queue
                //
                while (State.Instance.IsPerformingStandardAcquisition == true ||
                    State.Instance.IsPerformingCompute == true ||
                    mSaveQueue.Count > 0 ||
                    processedSpectra < mTotalSpectraReceived)
                {
                    if (mSaveQueue.Count > 0)
                    {
                        // Get the next spectrum response
                        lock (mSaveQueue)
                        {
                            saveSpectrumQueue = mSaveQueue.Dequeue();
                        }

                        int spectraInThisQueue = saveSpectrumQueue.Count;

                        while (saveSpectrumQueue.Count > 0)
                        {
                            SpectrumForSaving saveSpectrum = saveSpectrumQueue.Dequeue();

                            if (SettingsManager.SaveToFile)
                            {
                                // Since saving floats to disk is very slow, we avoid doing it if possible
                                if (resultSpectrumTypeCache == Options.ResultSpectrumType.RAW_SPECTRUM ||
                                    resultSpectrumTypeCache == Options.ResultSpectrumType.DARK_CORRECTED)
                                {
                                    // Convert back to int
                                    int[] convertedOutputArray = Utils.ToIntArray(saveSpectrum.computedSpectrum);

                                    if (bSaveOnlySelectedWavelengths == true)
                                    {
                                        SaveSpectrumSelectedWavelengthsOnly(sb, saveSpectrum.rawSpectrum, convertedOutputArray, outFile, saveSpectrum.triggered);

                                        // Update the number of saved bytes
                                        mTotalSavedBytes += (long)(sizeof(UInt16) * wavelengthsToSaveCache.Count);

                                    }
                                    else
                                    {
                                        SaveSpectrum(sb, saveSpectrum.rawSpectrum, convertedOutputArray, outFile, saveSpectrum.triggered, savingWavelengthStep);

                                        // Update the number of saved bytes
                                        mTotalSavedBytes += (long)(sizeof(UInt16)) * (saveEndPixelCache - saveStartPixelCache + 1) / savingWavelengthStep;
                                    }

                                    // Update the number of spectra saved
                                    mTotalSpectraSaved += 1;
                                }
                                else
                                {
                                    if (bSaveOnlySelectedWavelengths == true)
                                    {
                                        SaveSpectrumSelectedWavelengthsOnly(sb, saveSpectrum.rawSpectrum, saveSpectrum.computedSpectrum, outFile, saveSpectrum.triggered);

                                        // Update the number of saved bytes
                                        mTotalSavedBytes += (long)(sizeof(float) * wavelengthsToSaveCache.Count);

                                    }
                                    else
                                    {
                                        SaveSpectrum(sb, saveSpectrum.rawSpectrum, saveSpectrum.computedSpectrum, outFile, saveSpectrum.triggered, savingWavelengthStep);

                                        // Update the number of saved bytes
                                        mTotalSavedBytes += (long)(sizeof(float)) * (saveEndPixelCache - saveStartPixelCache + 1) / savingWavelengthStep;

                                    }

                                    // Update the number of spectra saved
                                    mTotalSpectraSaved += 1;
                                }
                            }
                        }

                        // Return the spectrum to the supply queue
                        lock (mSupplyQueue)
                        {
                            mSupplyQueue.Enqueue(saveSpectrumQueue.originalSpectrum);
                        }

                        // Update number of processed spectra
                        processedSpectra += spectraInThisQueue;

                        // Context switch
                        Thread.Sleep(0);
                    }
                }
            }
            catch (Exception)
            {
                // Indicate an error occurred
                Invoke((MethodInvoker)delegate
                {
                    // Invoke on UI thread
                    onAcquisitionError("Unexpected error in save thread.");
                });
            }
            finally
            {
                // Close the file
                if (outFile != null)
                {
                    outFile.Close();

                    // If nothing has been written to the file, we delete it
                    if (mTotalSavedBytes == 0)
                    {
                        try
                        {
                            File.Delete(saveFileNameCache);
                        }
                        catch (Exception)
                        {
                            // We silenty ignore this.
                        }
                    }
                }
            }

            // Reset the performing save flag
            State.Instance.IsPerformingSave = false;
        }

        //
        // Compute spectrum thread
        //
        private void doComputeSpectrum()
        {
            State.Instance.IsPerformingCompute = true;

            // Reset counter of computed spectra
            mTotalSpectraComputed = 0;

            // Keep track of each processed spectrum
            UInt64 currentSpectrumNumber = 0;

            try
            {
                OBPGetRawSpectrumWithMetadata computeSpectrum;

                // The computed result (raw spectrum, dark corrected, transmission, or absorbance)
                float[] computedOutputSpectrum = new float[mOceanFX.NumPixels];
                int[] intRawSpectrum = new int[mOceanFX.NumPixels];
                float[] chartedSpectrum = null;

                // Allocate and start the timer
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                // Timer vars to allow processing every 1/10 and 1 second
                long oneTenthStart = stopWatch.ElapsedMilliseconds;
                long oneSecondStart = oneTenthStart;
                long oneSecondNumSpectra = 0;

                //
                // While acquiring a spectrum to process...
                //
                UInt64 zeroMicroSecondCounter = 0;
                bool isZerosSet = false;
                UInt64 NumberOfLastSpectrumForDynamicReferenceRegeneration = 0;
                while (State.Instance.IsPerformingStandardAcquisition == true ||
                    mComputeQueue.Count > 0 || mTotalSpectraComputed < mTotalSpectraReceived)
                {
                    if (mComputeQueue.Count > 0)
                    {
                        // Get the next spectrum response
                        lock (mComputeQueue)
                        {
                            computeSpectrum = mComputeQueue.Dequeue();
                        }

                        // Create a new save spectrum queue object
                        SpectrumForSavingQueueObject spectrumQueue = new SpectrumForSavingQueueObject(computeSpectrum);

                        // Get the spectrum with metadata
                        RawSpectrumWithMetadataBuffer[] spectrumA = computeSpectrum.Spectrum;

                        int numSpectraReceived = spectrumA.Length;
                        this.mTotalSpectraReceived += numSpectraReceived;
                        oneSecondNumSpectra += numSpectraReceived;

                        // Process all spectra received in current batch
                        for (int s = 0; s < numSpectraReceived; s++)
                        {
                            // Number of the spectrum being processed since beginning
                            currentSpectrumNumber++;

                            // First get current spectrum and the corresponding data
                            RawSpectrumWithMetadataBuffer spectrum = spectrumA[s];

                            // Append the time stamp difference
                            float deltaCounter = 0;
                            if (!isZerosSet)
                            {
                                zeroMicroSecondCounter = spectrum.MicrosecondCounter;
                                isZerosSet = true;
                            }
                            // Store time in milliseconds
                            deltaCounter = (spectrum.MicrosecondCounter - zeroMicroSecondCounter) / 1000.0f;
                            SpectrumProcessor.Instance.TimeSeriesTimeValues.Enqueue(deltaCounter);

                            // Number of pixels to process
                            int pLen = spectrum.NumPixels < mOceanFX.NumPixels ? spectrum.NumPixels : mOceanFX.NumPixels;

                            // Retrieve the raw spectrum
                            SpectrumProcessor.Instance.GetRawSpectrum(spectrum, pLen, ref intRawSpectrum);

                            // Filter?
                            if (SettingsManager.SpectrumFilteringEnabled && !SpectrumFilterer.Instance.IsUnit)
                            {
                                intRawSpectrum = SpectrumFilterer.Instance.Convolve(intRawSpectrum,
                                    full: false, symmetric: true);
                            }

                            // Dynamic reference requested?
                            if (
                                (
                                SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.ABSORBANCE ||
                                SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.TRANSMISSION
                                ) &&
                                SettingsManager.ReferenceType == Options.ReferenceType.DYNAMIC)
                            {
                                // Enqueue current spectrum
                                SpectrumProcessor.Instance.DynamicAccumulatedSpectraForReferenceEstimation.Enqueue(
                                    Array.ConvertAll(intRawSpectrum, x => (float)x));

                                // Build current reference from the dynamically accumulated
                                // spectra. The corrected reference spectrum is automatically
                                // updated for the following calculations. Notice that this is
                                // is run on a user-defined interval (and after the first acquisition).
                                UInt64 SpectrumNumberSinceLastDynamicReferenceRegeneration =
                                    currentSpectrumNumber - NumberOfLastSpectrumForDynamicReferenceRegeneration;
                                if (currentSpectrumNumber == 1 ||
                                    SpectrumNumberSinceLastDynamicReferenceRegeneration >= SettingsManager.IntervalForDynamicAccumulation)
                                {
                                    // Recreate the reference
                                    SpectrumProcessor.Instance.BuildDynamicReferenceSpectrum();

                                    // Reset the NumberOfLastSpectrumForDynamicReferenceRegeneration
                                    NumberOfLastSpectrumForDynamicReferenceRegeneration = currentSpectrumNumber;
                                }
                            }

                            // Calculate desired output
                            switch (SettingsManager.ResultSpectrumType)
                            {
                                case Options.ResultSpectrumType.RAW_SPECTRUM:

                                    // Cast to float[] for subsequent operations
                                    Utils.ToFloatArray(intRawSpectrum, ref computedOutputSpectrum);
                                    break;

                                case Options.ResultSpectrumType.ABSORBANCE:

                                    // Please notice: the calculation of absorbance requires a dark
                                    // and reference spectrum; if the dark spectrum is not present,
                                    // it will be ignored. if the reference spectrum is not present,
                                    // the abosrbance will be set to 0.
                                    SpectrumProcessor.Instance.CalculateAbsorbance(intRawSpectrum,
                                        ref computedOutputSpectrum);
                                    break;

                                case Options.ResultSpectrumType.TRANSMISSION:

                                    // Please notice: the calculation of transmission requires a dark
                                    // and reference spectrum; if the dark spectrum is not present,
                                    // it will be ignored. if the reference spectrum is not present,
                                    // the abosrbance will be set to 0.
                                    SpectrumProcessor.Instance.CalculateTransmission(intRawSpectrum,
                                        ref computedOutputSpectrum);
                                    break;

                                case Options.ResultSpectrumType.DARK_CORRECTED:

                                    // Please notice: the calculation of dark correction
                                    // requires that the dark spectrum has been acquired (and stored):
                                    // * if the dark spectrum is not present, it will be ignored.
                                    SpectrumProcessor.Instance.CalculateDarkCorrection(intRawSpectrum,
                                        ref computedOutputSpectrum);
                                    break;

                                default:

                                    break;
                            }

                            // Store the values for the time-series plot?
                            if (SettingsManager.CurrentPlotType == Options.PlotType.TIMESERIES)
                            {
                                if (SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.ABSORBANCE ||
                                    SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.TRANSMISSION)
                                {
                                    SpectrumProcessor.Instance.UpdateFloatTimeSeriesForRequestedWavengths(ref computedOutputSpectrum);
                                }
                                else
                                {
                                    SpectrumProcessor.Instance.UpdateIntTimeSeriesForRequestedWavengths(ref computedOutputSpectrum);
                                }
                            }

                            // Copy the result to the charted array
                            if (SettingsManager.CurrentPlotType != Options.PlotType.TIMESERIES)
                            {
                                if (chartedSpectrum == null)
                                {
                                    chartedSpectrum = new float[computedOutputSpectrum.Length];
                                }
                                Array.Copy(computedOutputSpectrum, chartedSpectrum, computedOutputSpectrum.Length);
                            }

                            // Threshold spectrum?
                            bool IsThresholdConditionSatisfied = false;
                            if (SettingsManager.SpectrumThresholdingEnabled == true && mArduino.IsConnected() == true)
                            {
                                IsThresholdConditionSatisfied =
                                    SpectrumThresholder.IsSpectrumSatisfyingThresholding(
                                    computedOutputSpectrum);
                            }

                            // Trigger Arduino board?
                            if (IsThresholdConditionSatisfied)
                            {
                                // Count the hit
                                State.Instance.TotalHitNumber++;

                                Task.Run(() =>
                                {
                                    triggerArduino();
                                });

                                // Store the hit
                                SpectrumProcessor.Instance.TimeSeriesHits.Enqueue(deltaCounter);
                            }

                            // Update counter of computed spectra
                            mTotalSpectraComputed++;

                            // No matter whether saving is on or not, we enqueue the spectrum.
                            // The save thread will take care of cleaning the queue, whether
                            // the spectra will be saved to disk or not.
                            spectrumQueue.Enqueue(new SpectrumForSaving()
                            {
                                rawSpectrum = spectrum,
                                computedSpectrum = computedOutputSpectrum,
                                triggered = IsThresholdConditionSatisfied
                            });
                        }

                        lock (mSaveQueue)
                        {
                            mSaveQueue.Enqueue(spectrumQueue);
                        }

                        if (chartedSpectrum != null)
                        {
                            SpectrumProcessor.Instance.ResultSpectrum = chartedSpectrum;
                            chartedSpectrum = null;
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
                        chartedSpectrum = new float[computedOutputSpectrum.Length];

                        oneTenthStart = curTime;
                    }
                }
            }
            catch (Exception)
            {
                State.Instance.IsStopAcquiringRequested = true;

                // Indicate an error occurred
                Invoke((MethodInvoker)delegate
                {
                    // Invoke on UI thread
                    onAcquisitionError("Unexpected error in compute thread.");
                });
            }
            finally
            {
            }

            State.Instance.IsPerformingCompute = false;
        }
    }
}
