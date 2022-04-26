/*

    Copyright © 2018-2022, ETH Zurich, D-BSSE, Aaron Ponti & Todd Duncombe
    All rights reserved. This program and the accompanying materials
    are made available under the terms of the Apache-2.0 license
    which accompanies this distribution, and is available at
    https://www.apache.org/licenses/LICENSE-2.0

    SpectraSorter is based on FXStreamer by Oliver Lischtschenko (Ocean Optics):
    Lischtschenko, O.; private communication on OBP protocol, 2018. 
    The original code is added to the repository.

*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OBP_Library;

namespace spectra.devices
{
    public abstract class OceanFX
    {

        #region members
        /// <summary>
        /// The device.
        /// </summary>
        protected ISendReceive mActiveIO = null;

        // The USb PID of the OceanFX
        private const int mPID = 0x2001;

        // The list of supported device GUIDs
        private readonly static string[] mDEVICE_GUIDS =
        {
            "DBBAD306-1786-4f2e-A8AB-340D45F0653F",
        };

        private string mFWVersion;
        private string mFWSubversion;
        private string mSerialNum;
        private string mFPGAVersion;

        protected const int mReceiveTimeoutMS = 2000;

        // The basic IO buffers
        protected OBPBuffer mRequest = new OBPBuffer(1024);
        protected OBPBuffer mResponse = new OBPBuffer(1024);

        // Spectrometer settings
        private int mRawMetadataResponseBufferSize;
        private ushort mNumPixels;
        protected const int MAX_SPECTRA_PER_REQUEST = 15;
        private double[] mWavecalCoefficients;
        protected double[] mNonlinearityCoefficients;

        // Continuous Strobe Settings
        private byte mContinuousStrobeEnabled = 0;
        private uint mContinuousStrobePeriod = 0;
        private uint mContinuousStrobeWidth = 0;

        // Single Strobe Settings
        private byte mSingleStrobeEnabled = 0;
        private uint mSingleStrobePulseDelay = 0;
        private uint mSingleStrobePulseWidth = 0;

        // Acquisition Settings
        private uint mIntegrationTime;
        private uint mScansToAverage;
        private uint mBackToBackPerTrigger;
        private uint mNumSpectraPerRequest;
        private uint mAcquisitionDelay;
        private byte mTriggerMode;
        private byte mLampEnable;

        // Buffer Settings
        protected uint mBufferSize = 50000;
        private uint mNumSpectraInBuffer = 10;
        private byte mBufferEnabled = 0;

        #endregion members

        /// <summary>
        /// Connect.
        /// </summary>
        public abstract void Connect();

        /// <summary>
        /// Disconnect.
        /// </summary>
        public abstract void Disconnect();

        /// <summary>
        /// Flush the device.
        /// </summary>
        public int Flush()
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

                try
                {
                    mActiveIO.setReceiveTimeout(OceanFX.mReceiveTimeoutMS);
                }
                catch (Exception)
                {
                    // This happens if the application is closed too abruptly.
                }
            }

            return bytesFlushed;

        }

        /// <summary>
        /// Initialize device.
        /// </summary>
        public void Initialize()
        {
            Flush();

            OBPGetFWRevision fwRevision = new OBPGetFWRevision(mActiveIO, mRequest, mResponse);
            mFWVersion = (fwRevision.Send()) ? fwRevision.FwRevision.ToString("X4", CultureInfo.InvariantCulture) : "";

            OBPGetFWSubRevision fwSubRevision = new OBPGetFWSubRevision(mActiveIO, mRequest, mResponse);
            mFWSubversion = (fwSubRevision.Send()) ? fwSubRevision.FwSubRevision.ToString("X4", CultureInfo.InvariantCulture) : "";

            OBPGetFPGARevision fpgaRevision = new OBPGetFPGARevision(mActiveIO, mRequest, mResponse);
            mFPGAVersion = (fpgaRevision.Send()) ? fpgaRevision.FPGARevision.ToString("X4", CultureInfo.InvariantCulture) : "";

            OBPGetSerialNumber serialNum = new OBPGetSerialNumber(mActiveIO, mRequest, mResponse);
            mSerialNum = (serialNum.Send()) ? serialNum.SerialNum : "";

            OBPGetNumPixels numPixels = new OBPGetNumPixels(mActiveIO, mRequest, mResponse);
            mNumPixels = (numPixels.Send()) ? numPixels.NumPixels : (ushort)0;

            // Returns zero so commented out
            //OBPGetSaturationLevel saturationLevel = new OBPGetSaturationLevel(mActiveIO, mRequest, mResponse);
            //mSaturationLevel = (saturationLevel.Send()) ? saturationLevel.SaturationLevel : (uint)0xffff;

            // Response buffer size required to receive 15 spectra per response
            mRawMetadataResponseBufferSize = 64 + MAX_SPECTRA_PER_REQUEST * (68 + mNumPixels * 2);

            OBPGetNumWavecalCoefficients numWavecal = new OBPGetNumWavecalCoefficients(mActiveIO, mRequest, mResponse);
            mWavecalCoefficients = (numWavecal.Send()) ? new double[numWavecal.NumWavecalCoeffs] : new double[0];

            OBPGetWavecalCoefficient wavecalCoeff = new OBPGetWavecalCoefficient(mActiveIO, mRequest, mResponse);
            for (int i = 0; i < mWavecalCoefficients.Length; i++)
            {
                wavecalCoeff.WavecalIndex = (byte)i;
                mWavecalCoefficients[i] = (wavecalCoeff.Send()) ? wavecalCoeff.WavecalCoeff : 0.0;
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
            mContinuousStrobeEnabled = (csEnable.Send()) ? csEnable.Enabled : (byte)0;

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

            OBPGetScansToAvg scansToAvg = new OBPGetScansToAvg(mActiveIO, mRequest, mResponse);
            mScansToAverage = (scansToAvg.Send()) ? scansToAvg.ScansToAvg : (uint)0;

            OBPGetNumSpectraPerTrigger spectraPerTrigger = new OBPGetNumSpectraPerTrigger(mActiveIO, mRequest, mResponse);
            mBackToBackPerTrigger = (spectraPerTrigger.Send()) ? spectraPerTrigger.NumSpectra : (uint)0;

            OBPGetAcquisitionDelay acquisitionDelay = new OBPGetAcquisitionDelay(mActiveIO, mRequest, mResponse);
            mAcquisitionDelay = (acquisitionDelay.Send()) ? acquisitionDelay.AcquisitionDelay : (uint)0;

            OBPGetTriggerMode triggerMode = new OBPGetTriggerMode(mActiveIO, mRequest, mResponse);
            mTriggerMode = (triggerMode.Send()) ? triggerMode.TriggerMode : (byte)0;

            OBPGetLampEnable lampEnable = new OBPGetLampEnable(mActiveIO, mRequest, mResponse);
            mLampEnable = (lampEnable.Send()) ? lampEnable.LampEnable : (byte)0;

            OBPGetBufferingEnabled bufferEnable = new OBPGetBufferingEnabled(mActiveIO, mRequest, mResponse);
            mBufferEnabled = (bufferEnable.Send()) ? bufferEnable.BufferingEnabled : (byte)0;

            OBPGetBufferSize bufferSize = new OBPGetBufferSize(mActiveIO, mRequest, mResponse);
            mBufferSize = (bufferSize.Send()) ? bufferSize.BufferSize : (byte)0;

            OBPGetNumSpectraInBuffer numInBuffer = new OBPGetNumSpectraInBuffer(mActiveIO, mRequest, mResponse);
            mNumSpectraInBuffer = (numInBuffer.Send()) ? numInBuffer.NumSpectraInBuffer : (byte)0;
        }

        /// <summary>
        /// Sync the local values with the OceanFX spectrometer.
        /// </summary>
        /// <param name="singleSpectrum">Set to true in case of a single-spectrum acquisition.</param>
        public void Sync(bool singleSpectrum = false)
        {
            OBPSetContinuousStrobeEnable csEnable = new OBPSetContinuousStrobeEnable(mActiveIO, mRequest, mResponse);
            csEnable.Enabled = this.mContinuousStrobeEnabled;
            csEnable.Response.AckRequest = true;
            csEnable.Send();

            OBPSetContinuousStrobePeriod csPeriod = new OBPSetContinuousStrobePeriod(mActiveIO, mRequest, mResponse);
            csPeriod.Period = this.mContinuousStrobePeriod;
            csPeriod.Response.AckRequest = true;
            csPeriod.Send();

            OBPSetContinuousStrobeWidth csWidth = new OBPSetContinuousStrobeWidth(mActiveIO, mRequest, mResponse);
            csWidth.Width = this.mContinuousStrobeWidth;
            csWidth.Response.AckRequest = true;
            csWidth.Send();

            OBPSetSingleStrobeEnable ssEnable = new OBPSetSingleStrobeEnable(mActiveIO, mRequest, mResponse);
            ssEnable.Enabled = this.mSingleStrobeEnabled;
            ssEnable.Response.AckRequest = true;
            ssEnable.Send();

            OBPSetSingleStrobePulseDelay ssPulseDelay = new OBPSetSingleStrobePulseDelay(mActiveIO, mRequest, mResponse);
            ssPulseDelay.PulseDelay = this.mSingleStrobePulseDelay;
            ssPulseDelay.Response.AckRequest = true;
            ssPulseDelay.Send();

            OBPSetSingleStrobePulseWidth ssPulseWidth = new OBPSetSingleStrobePulseWidth(mActiveIO, mRequest, mResponse);
            ssPulseWidth.PulseWidth = this.mSingleStrobePulseWidth;
            ssPulseWidth.Response.AckRequest = true;
            ssPulseWidth.Send();

            OBPSetIntegrationTime integrationTime = new OBPSetIntegrationTime(mActiveIO, mRequest, mResponse);
            integrationTime.IntegrationTime = this.mIntegrationTime;
            integrationTime.Response.AckRequest = true;
            integrationTime.Send();

            OBPSetScansToAvg scansToAvg = new OBPSetScansToAvg(mActiveIO, mRequest, mResponse);
            scansToAvg.ScansToAvg = this.mScansToAverage;
            scansToAvg.Response.AckRequest = true;
            scansToAvg.Send();

            OBPSetNumSpectraPerTrigger spectraPerTrigger = new OBPSetNumSpectraPerTrigger(mActiveIO, mRequest, mResponse);
            spectraPerTrigger.NumSpectra = singleSpectrum ? 1 : this.mBackToBackPerTrigger;
            spectraPerTrigger.Response.AckRequest = true;
            spectraPerTrigger.Send();

            OBPSetAcquisitionDelay acquisitionDelay = new OBPSetAcquisitionDelay(mActiveIO, mRequest, mResponse);
            acquisitionDelay.AcquisitionDelay = this.mAcquisitionDelay;
            acquisitionDelay.Response.AckRequest = true;
            acquisitionDelay.Send();

            OBPSetTriggerMode triggerMode = new OBPSetTriggerMode(mActiveIO, mRequest, mResponse);
            triggerMode.TriggerMode = singleSpectrum ? (byte)0 : this.mTriggerMode;  // Force SW trigger when getting a single spectrum
            triggerMode.Response.AckRequest = true;
            triggerMode.Send();

            // Note: Lamp enable takes effect immediately upon checkbox change and doesn't need to be set here
            //OBPSetLampEnable lampEnable = new OBPSetLampEnable(mActiveIO, mRequest, mResponse);
            //lampEnable.LampEnable = this.mLampEnable;
            //lampEnable.Send();

            OBPSetBufferingEnabled bufferEnable = new OBPSetBufferingEnabled(mActiveIO, mRequest, mResponse);
            bufferEnable.BufferingEnabled = this.mBufferEnabled;
            bufferEnable.Response.AckRequest = true;
            bufferEnable.Send();

            //OBPSetBufferSize bufferSize = new OBPSetBufferSize(mActiveIO, mRequest, mResponse);
            //bufferSize.BufferSize = this.mBufferSize;
            //bufferSize.Response.AckRequest = true;
            //bufferSize.Send();
        }

        /// <summary>
        /// Get the corrected spectrum
        /// </summary>
        /// <returns>The corrected spectrum.</returns>
        public OBPGetCorrectedSpectrum GetCorrectedSpectrum()
        {
            // Allocate a large enough response buffer
            OBPBuffer spectrumResponse = new OBPBuffer(this.mNumPixels * 2 + 128);

            // Note: With the FX the "corrected" spectrum is just the raw spectrum without the metadata
            OBPGetCorrectedSpectrum correctedSpectrum = new OBPGetCorrectedSpectrum(mActiveIO, mRequest, spectrumResponse);
            correctedSpectrum.Send();

            return correctedSpectrum;
        }

        /// <summary>
        /// Get the raw spectrum with metadata.
        /// </summary>
        /// <returns>The raw spectrum with metadata.</returns>
        public OBPGetRawSpectrumWithMetadata GetRawSpectrumWithMetadata()
        {
            return new OBPGetRawSpectrumWithMetadata(mActiveIO, new OBPBuffer(64), new OBPBuffer(this.mRawMetadataResponseBufferSize));
        }

        /// <summary>
        /// Enable the lamp.
        /// 
        /// This will use the current value of this.mLambEnable to either turn on or
        /// turn off the lamp. 
        /// @TODO Make sure the behavio
        /// </summary>
        public void EnableLamp()
        {
            //this.mLampEnable = (byte)1;

            OBPSetLampEnable lampEnable = new OBPSetLampEnable(mActiveIO, mRequest, mResponse);
            lampEnable.LampEnable = this.mLampEnable;
            lampEnable.Send();
        }

        /// <summary>
        /// Clear buffer on device.
        /// </summary>
        public void ClearBufferOnDevice(bool withAckResponse = false)
        {
            OBPClearBufferedSpectra clearBuffer = new OBPClearBufferedSpectra(mActiveIO, mRequest, mResponse);
            if (withAckResponse == true)
            {
                clearBuffer.Response.AckRequest = true;
            }
            clearBuffer.Send();
        }

        /// <summary>
        /// Get number of spectra in device buffer (and chaches it).
        /// </summary>
        /// <returns></returns>
        public uint GetNumberOfSpectraInDeviceBuffer()
        {
            OBPGetNumSpectraInBuffer numInBuffer = new OBPGetNumSpectraInBuffer(mActiveIO, mRequest, mResponse);
            this.mNumSpectraInBuffer = (numInBuffer.Send()) ? numInBuffer.NumSpectraInBuffer : (byte)0;

            return this.mNumSpectraInBuffer;
        }

        #region properties

        /// <summary>
        /// Check if there is an open connection.
        /// </summary>
        public abstract bool IsConnected { get; }

        public static int PID => mPID;

        public static string[] DEVICE_GUIDS => mDEVICE_GUIDS;

        public string FWVersion { get => this.mFWVersion; }
        public string FWSubversion { get => this.mFWSubversion; }
        public string SerialNum { get => this.mSerialNum; }
        public string FPGAVersion { get => this.mFPGAVersion; }
        public ushort NumPixels { get => this.mNumPixels; set => this.mNumPixels = value; }
        public double[] WavecalCoefficients { get => this.mWavecalCoefficients; set => this.mWavecalCoefficients = value; }
        public uint IntegrationTime { get => this.mIntegrationTime; set => this.mIntegrationTime = value; }
        public uint BackToBackPerTrigger { get => this.mBackToBackPerTrigger; set => this.mBackToBackPerTrigger = value; }
        public uint NumSpectraPerRequest { get => this.mNumSpectraPerRequest; set => this.mNumSpectraPerRequest = value; }
        public byte BufferEnabled { get => this.mBufferEnabled; set => this.mBufferEnabled = value; }
        public uint SingleStrobePulseDelay { get => this.mSingleStrobePulseDelay; set => this.mSingleStrobePulseDelay = value; }
        public byte SingleStrobeEnabled { get => this.mSingleStrobeEnabled; set => this.mSingleStrobeEnabled = value; }
        public byte ContinuousStrobeEnabled { get => this.mContinuousStrobeEnabled; set => this.mContinuousStrobeEnabled = value; }
        public uint ContinuousStrobePeriod { get => this.mContinuousStrobePeriod; set => this.mContinuousStrobePeriod = value; }
        public uint SingleStrobePulseWidth { get => this.mSingleStrobePulseWidth; set => this.mSingleStrobePulseWidth = value; }
        public uint ContinuousStrobeWidth { get => this.mContinuousStrobeWidth; set => this.mContinuousStrobeWidth = value; }
        public uint ScansToAverage { get => this.mScansToAverage; set => this.mScansToAverage = value; }
        public uint AcquisitionDelay { get => this.mAcquisitionDelay; set => this.mAcquisitionDelay = value; }
        public byte TriggerMode { get => this.mTriggerMode; set => this.mTriggerMode = value; }
        public byte LampEnable { get => this.mLampEnable; set => this.mLampEnable = value; }
        public uint NumSpectraInBuffer { get => this.mNumSpectraInBuffer; set => this.mNumSpectraInBuffer = value; }
        public int RawMetadataResponseBufferSize { get => this.mRawMetadataResponseBufferSize; set => this.mRawMetadataResponseBufferSize = value; }

        #endregion properties

    }
}
