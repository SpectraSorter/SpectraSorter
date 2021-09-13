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

using spectra.state;
using spectra.utils;
using OBP_Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace spectra.processing
{
    public class SpectrumProcessor
    {
        #region members

        // Instance
        private static SpectrumProcessor mInstance = null;

        #endregion members

        #region methods

        #region public

        /// <summary>
        /// Compute the dark-corrected reference spectrum if both dark and reference
        /// spectrum are present.
        /// </summary>
        public void ComputeReferenceCorrectedSpectrumIfPossible()
        {
            if (this.DarkSpectrum != null && this.ReferenceSpectrum != null)
            {
                // Compute the dark corrected reference spectrum
                this.ReferenceCorrectedSpectrum = new float[this.DarkSpectrum.Length];
                int iLen = this.DarkSpectrum.Length < this.ReferenceSpectrum.Length ? this.DarkSpectrum.Length : this.ReferenceSpectrum.Length;
                for (int i = 0; i < iLen; i++)
                {
                    this.ReferenceCorrectedSpectrum[i] = this.ReferenceSpectrum[i] - this.DarkSpectrum[i];
                }
            }
        }

        /// <summary>
        /// Retrieve the raw spectrum
        /// </summary>
        /// <param name="spectrum"></param>
        /// <param name="pLen"></param>
        /// <param name="intResult"></param>
        public void GetRawSpectrum(RawSpectrumWithMetadataBuffer spectrum, int pLen, ref int[] intResult)
        {
            for (int p = 0; p < pLen; p++)
            {
                intResult[p] = spectrum.GetPixelU16(p);
            }
        }

        /// <summary>
        /// Calculate the absorbance and store it in the referenced float array.
        /// </summary>
        /// Please notice: the calculation of absorbance requires that the dark and reference spectrum
        /// have been acquired (and stored):
        ///   * if the dark spectrum is not present, it will be ignored.
        ///   * if the reference spectrum is not present, the absorbance will be set to 0.
        public void CalculateAbsorbance(int[] input, ref float[] output)
        {
            double ratioVal;

            for (int p = 0; p < input.Length; p++)
            {
                float refValue = this.ReferenceCorrectedSpectrum != null && p < this.ReferenceCorrectedSpectrum.Length ? this.ReferenceCorrectedSpectrum[p] : 0.0f;

                // Avoid a divide by zero
                if (refValue == 0)
                {
                    output[p] = 0.0f;  // Could be set to MAX_ABSORBANCE or flag an error, etc.
                }
                else
                {
                    float darkValue = this.DarkSpectrum != null && p < this.DarkSpectrum.Length ? this.DarkSpectrum[p] : 0.0f;

                    ratioVal = (input[p] - darkValue) / refValue;

                    // Avoid NaN log10...
                    if (ratioVal <= 0)
                    {
                        output[p] = 0.0f;  // Could be set to MAX_ABSORBANCE or flag an error, etc.
                    }
                    else
                    {
                        output[p] = (float) -Math.Log10(ratioVal);
                    }
                }
            }
        }

        /// <summary>
        /// Calculate the transmission and store it in the referenced float array.
        /// 
        /// Please notice: the calculation of transmission requires that the dark and reference spectrum
        /// have been acquired (and stored):
        ///   * if the dark spectrum is not present, it will be ignored.
        ///   * if the reference spectrum is not present, the absorbance will be set to 0.
        /// </summary>
        public void CalculateTransmission(int[] input, ref float[] output)
        {
            for (int p = 0; p < input.Length; p++)
            {
                float refValue = this.ReferenceCorrectedSpectrum != null && p < this.ReferenceCorrectedSpectrum.Length ? this.ReferenceCorrectedSpectrum[p] : 0.0f;

                // Avoid a divide by zero
                if (refValue == 0)
                {
                    output[p] = 100.0f;  // Could be set to zero or flag an error, etc.
                }
                else
                {
                    float darkValue = this.DarkSpectrum != null && p < this.DarkSpectrum.Length ? this.DarkSpectrum[p] : 0.0f;

                    output[p] = 100.0f * ((input[p] - darkValue) / refValue);
                }
            }
        }

        /// <summary>
        /// Calculate the dark-corrected spectrum and store it in the referenced int array.
        /// </summary>
        ///
        /// Please notice: the calculation of dark correction requires that the dark spectrum
        /// has been acquired (and stored):
        ///   * if the dark spectrum is not present, it will be ignored.
        public void CalculateDarkCorrection(int[] input, ref float[] output)
        {
            for (int p = 0; p < input.Length; p++)
            {
                ushort darkValue = this.DarkSpectrum != null && p < this.DarkSpectrum.Length ? this.DarkSpectrum[p] : (ushort)0;
                output[p] = (float)input[p] - (float) darkValue;
            }
        }

        /// <summary>
        /// Enqueue the (float) values of the last acquired spectrum for the requested wavelengths.
        /// </summary>
        /// <param name="spectrum">Spectrum</param>
        public void UpdateFloatTimeSeriesForRequestedWavengths(ref float[] spectrum)
        {
            // Cache the query
            List<Wavelength> wavelengthsToPlot = WavelengthManager.Instance.WavelengthsForPlotting;

            // Process all requested wavelengths
            foreach (Wavelength wavelength in wavelengthsToPlot)
            {
                this.FloatTimeSeries[wavelength.ID].Enqueue(spectrum[wavelength.Index]);
            }
        }

        /// <summary>
        /// Enqueue the (integer) values of the last acquired spectrum for the requested wavelengths.
        /// </summary>
        /// <param name="spectrum">Spectrum</param>
        public void UpdateIntTimeSeriesForRequestedWavengths(ref float[] spectrum)
        {
            // Cache the query
            List<Wavelength> wavelengthsToPlot = WavelengthManager.Instance.WavelengthsForPlotting;

            // Process all requested wavelengths
            foreach (Wavelength wavelength in wavelengthsToPlot)
            {
                this.IntTimeSeries[wavelength.ID].Enqueue((int)spectrum[wavelength.Index]);
            }
        }

        /// <summary>
        /// Calculate the static accumulated spectrum.
        /// </summary>
        /// <returns></returns>
        private float[] CalculateStaticAccumulatedSpectra()
        {
            // Check that spectra were accumulated already
            if (this.StaticAccumulatedSpectraForReferenceEstimation == null)
            {
                return new float[0];
            }

            if (this.StaticAccumulatedSpectraStartIndex == -1)
            {
                this.StaticAccumulatedSpectraStartIndex = 0;
            }

            if (this.StaticAccumulatedSpectraEndIndex == -1 ||
                this.StaticAccumulatedSpectraEndIndex > (this.StaticAccumulatedSpectraForReferenceEstimation.Count - 1))
            {
                this.StaticAccumulatedSpectraEndIndex = this.StaticAccumulatedSpectraForReferenceEstimation.Count - 1;
            }

            if (this.StaticAccumulatedSpectraStartIndex < 0)
            {
                this.StaticAccumulatedSpectraStartIndex = 0;
            }

            if (this.StaticAccumulatedSpectraStartIndex >= this.StaticAccumulatedSpectraEndIndex)
            {
                this.StaticAccumulatedSpectraStartIndex = 0;
                this.StaticAccumulatedSpectraEndIndex = this.StaticAccumulatedSpectraForReferenceEstimation.Count - 1;
            }

            // Constants
            int nWavelengths = this.StaticAccumulatedSpectraForReferenceEstimation[0].Length;
            int nSpectra = this.StaticAccumulatedSpectraEndIndex - this.StaticAccumulatedSpectraStartIndex + 1;

            // Output spectrum
            float[] output = new float[nWavelengths];

            // Calculate median of all accumulated spectra
            for (int i = 0; i < nWavelengths; i++)
            {
                // Temporary array
                float[] tmp = new float[nSpectra];

                int spectrumNum = 0;
                for (int j = this.StaticAccumulatedSpectraStartIndex; j < this.StaticAccumulatedSpectraEndIndex; j++)
                {
                    tmp[spectrumNum++] = this.StaticAccumulatedSpectraForReferenceEstimation[j][i];
                }

                // Store the median
                output[i] = Utils.Median(tmp);
            }

            return output;
        }

        /// <summary>
        /// Build a reference spectrum from the accumulated spectra.
        /// </summary>
        /// 
        /// The dark spectrum is subtracted from the reference.
        /// <returns>True if the reference spectrum could be built correctly,
        /// false otherwise.</returns>
        public bool BuildStaticReferenceSpectrum()
        {
            ushort[] accumulated = Array.ConvertAll(this.CalculateStaticAccumulatedSpectra(), x => (ushort)x);
            if (accumulated.Length == 0)
            {
                this.ReferenceSpectrum = null;
                return false;
            }

            this.ReferenceSpectrum = accumulated;

            // If needed, allocate space for the result
            if (this.ReferenceCorrectedSpectrum == null)
            {
                this.ReferenceCorrectedSpectrum = new float[this.ReferenceSpectrum.Length];
            }

            for (int i = 0; i < this.ReferenceSpectrum.Length; i++)
            {
                // Update current reference spectrum
                this.ReferenceCorrectedSpectrum[i] = this.ReferenceSpectrum[i] - this.DarkSpectrum[i];
            }

            return true;
        }

        /// <summary>
        /// Calculate the dynamic accumulated spectrum.
        /// </summary>
        /// <returns></returns>
        private float[] CalculateDynamicAccumulatedSpectra()
        {
            // Check that spectra were accumulated already
            if (this.DynamicAccumulatedSpectraForReferenceEstimation == null)
            {
                return new float[0];
            }

            // Assertion
            Debug.Assert(this.DynamicAccumulatedSpectraForReferenceEstimation.Count <= SettingsManager.NumberOfSpectraForDynamicAccumulation);

            // Constants
            int nWavelengths = this.DynamicAccumulatedSpectraForReferenceEstimation[0].Length;
            
            // Output spectrum
            float[] output = new float[nWavelengths];

            // Calculate median of all accumulated spectra
            for (int i = 0; i < nWavelengths; i++)
            {
                // Please note: since DynamicAccumulatedSpectraForReferenceEstimation is a circular array,
                // Count will grow up to SettingsManager.NumberOfSpectraForDynamicAccumulation and then
                // stay there.

                // Temporary array
                float[] tmp = new float[this.DynamicAccumulatedSpectraForReferenceEstimation.Count];

                int spectrumNum = 0;
                for (int j = 0; j < this.DynamicAccumulatedSpectraForReferenceEstimation.Count; j++)
                {
                    tmp[spectrumNum++] = this.DynamicAccumulatedSpectraForReferenceEstimation[j][i];
                }

                // Store the median
                output[i] = Utils.Median(tmp);
            }

            return output;
        }

        /// <summary>
        /// Build a reference spectrum from the accumulated spectra (in a dynamic experiment).
        /// </summary>
        /// 
        /// The dark spectrum is subtracted from the reference.
        /// <returns>True if the reference spectrum could be built correctly,
        /// false otherwise.</returns>
        public bool BuildDynamicReferenceSpectrum()
        {
            ushort[] accumulated = Array.ConvertAll(this.CalculateDynamicAccumulatedSpectra(), x => (ushort)x);
            if (accumulated.Length == 0)
            {
                this.ReferenceSpectrum = null;
                return false;
            }

            this.ReferenceSpectrum = accumulated;

            // If needed, allocate space for the result
            if (this.ReferenceCorrectedSpectrum == null)
            {
                this.ReferenceCorrectedSpectrum = new float[this.ReferenceSpectrum.Length];
            }

            for (int i = 0; i < this.ReferenceSpectrum.Length; i++)
            {
                // Update current reference spectrum
                this.ReferenceCorrectedSpectrum[i] = this.ReferenceSpectrum[i] - this.DarkSpectrum[i];
            }

            return true;
        }

        /// <summary>
        /// Build running time series arrays from the accumulated spectra.
        /// 
        /// This uses SettingsManager.NumberOfTimePointsToStore as in the normal time series plots.
        /// 
        /// @return
        /// </summary>
        public (float[], Dictionary<string, CircularBuffer<float>>) buildRunningTimeSeriesFromAccumulatingData()
        {
            List<Wavelength> wavelengths = WavelengthManager.Instance.WavelengthsForPlotting;
            Dictionary<string, CircularBuffer<float>> timeSeries = new Dictionary<string, CircularBuffer<float>>();

            // Number of time points to use
            int currentNumTimePoints = SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count;
            int nTimePoints = currentNumTimePoints <= (int)SettingsManager.NumberOfTimePointsToStore ?
                currentNumTimePoints : (int)SettingsManager.NumberOfTimePointsToStore;

            // Build the time series dictionary
            foreach (Wavelength wavelength in wavelengths)
            {
                CircularBuffer<float> values = new CircularBuffer<float>(capacity: nTimePoints);

                int first = currentNumTimePoints - nTimePoints;
                int last = currentNumTimePoints;
                for (int p = 0, i = first; i < last; i++, p++)
                {
                    values.Enqueue((float)SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation[i][wavelength.Index]);
                }
                timeSeries[wavelength.ID] = values;
            }

            // Prepare the timepoints array
            float[] timePoints = new float[nTimePoints];
            for (int i = 0; i < nTimePoints; i++)
            {
                timePoints[i] = (float)i;
            }

            return (timePoints, timeSeries);
        }

        /// <summary>
        /// Get values for requested wavelength index from accumulated spectra.
        /// </summary>
        /// <param name="wavelengthIndex"></param>
        /// <returns></returns>
        public float[] GetStaticAccumulatedWavelength(int wavelengthIndex)
        {
            if (SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation == null ||
                SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count == 0)
            {
                return new float[1] { 0.0f };
            }

            if (wavelengthIndex > SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation[0].Length)
            {
                return new float[1] { 0.0f };
            }

            float[] values = new float[SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count];

            for (int i = 0; i < SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count; i++)
            {
                try 
                {
                    values[i] = SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation[i][wavelengthIndex];
                }
                catch 
                {
                    // Try again later
                }
            }

            return values;
        }

        #endregion public

        /// <summary>
        /// Reset all circular buffers to their capacity.
        /// </summary>
        /// <returns></returns>
        public void ResetAllBuffers()
        {
            if (SpectrumProcessor.Instance.FloatTimeSeries == null)
            {
                // Initialize time series
                SpectrumProcessor.Instance.FloatTimeSeries = new Dictionary<string, CircularBuffer<float>>();
            }

            if (SpectrumProcessor.Instance.IntTimeSeries == null)
            {
                // Initialize time series
                SpectrumProcessor.Instance.IntTimeSeries = new Dictionary<string, CircularBuffer<int>>();
            }

            // Set up buffer for time stamps differences
            if (SpectrumProcessor.Instance.TimeSeriesTimeValues == null ||
                SpectrumProcessor.Instance.TimeSeriesTimeValues.Capacity != SettingsManager.NumberOfTimePointsToStore)
            {
                // Initialize time stamp differences
                SpectrumProcessor.Instance.TimeSeriesTimeValues = new CircularBuffer<float>(capacity: (int)SettingsManager.NumberOfTimePointsToStore);
            }

            // Set up buffer for time hit values
            if (SpectrumProcessor.Instance.TimeSeriesHits == null ||
                SpectrumProcessor.Instance.TimeSeriesHits.Capacity != SettingsManager.NumberOfTimePointsToStore)
            {
                // Initialize time stamp differences
                SpectrumProcessor.Instance.TimeSeriesHits = new CircularBuffer<float>(capacity: (int)SettingsManager.NumberOfTriggerPoints);
            }

            // Clear circular buffers
            SpectrumProcessor.Instance.FloatTimeSeries.Clear();
            SpectrumProcessor.Instance.IntTimeSeries.Clear();
            SpectrumProcessor.Instance.TimeSeriesTimeValues.Clear();
            SpectrumProcessor.Instance.TimeSeriesHits.Clear();

            // Initialize them (allocate memory for all wavelengths, whether they will be plotted or not)
            for (int i = 0; i < WavelengthManager.Instance.Wavelengths.Count; i++)
            {
                CircularBuffer<int> intBuffer = new CircularBuffer<int>(capacity: (int)SettingsManager.NumberOfTimePointsToStore);
                for (int j = 0; j < SettingsManager.NumberOfTimePointsToStore; j++)
                {
                    intBuffer.Enqueue(0);
                }
                SpectrumProcessor.Instance.IntTimeSeries[WavelengthManager.Instance.Wavelengths[i].ID] = intBuffer;

                CircularBuffer<float> floatBuffer = new CircularBuffer<float>(capacity: (int)SettingsManager.NumberOfTimePointsToStore);
                for (int j = 0; j < SettingsManager.NumberOfTimePointsToStore; j++)
                {
                    floatBuffer.Enqueue(0.0f);
                }
                SpectrumProcessor.Instance.FloatTimeSeries[WavelengthManager.Instance.Wavelengths[i].ID] = floatBuffer;
            }
            for (int j = 0; j < SettingsManager.NumberOfTimePointsToStore; j++)
            {
                SpectrumProcessor.Instance.TimeSeriesTimeValues.Enqueue(0.0f);
            }
            for (int j = 0; j < SettingsManager.NumberOfTriggerPoints; j++)
            {
                SpectrumProcessor.Instance.TimeSeriesHits.Enqueue(0.0f);
            }

        }

        #region private

        private SpectrumProcessor()
        {
            this.Pixels = new int[0];
            this.Wavelengths = new double[0];
        }

        #endregion private

        #endregion methods

        #region properties

        /// <summary>
        /// SpectrumManager (singleton) instance.
        /// </summary>
        public static SpectrumProcessor Instance
        {
            get
            {
                // If the Form has not been created yet,
                // instantiate it now.
                if (mInstance == null)
                {
                    mInstance = new SpectrumProcessor();
                }

                // Return a reference
                return mInstance;
            }
        }

        // Wavelengths
        public double[] Wavelengths { get; set; }

        // Pixels
        public int[] Pixels { get; set; }

        // Dark spectrum
        public ushort[] DarkSpectrum { get; set; }

        public DateTime DarkSpectrumTime { get; set; }

        // Reference spectrum
        public ushort[] ReferenceSpectrum { get; set; }

        // Date and time of the reference spectrum
        public DateTime ReferenceSpectrumTime { get; set; }

        // Reference corrected spectrum
        // Note: this is a float[] to force absorbance and transmission division result to be float
        public float[] ReferenceCorrectedSpectrum { get; set; }

        // Result spectrum (absorbance, transmission, etc.)
        public float[] ResultSpectrum { get; set; }

        // Accumulated spectra for static reference estimation
        public CircularBuffer<float[]> StaticAccumulatedSpectraForReferenceEstimation { get; set; }

        // Accumulated spectra for dynamic reference estimation
        public CircularBuffer<float[]> DynamicAccumulatedSpectraForReferenceEstimation { get; set; }

        // Accumulated spectra X values
        public double[] StaticAccumulatedSpectraXValues { get; set; }

        // Selected accumulated spectrum number (to plot; default is 0)
        public int SelectedStaticAccumulatedSpectrumNumber { get; set; } = 0;

        // Map of accumulated integer time series.
        // The key is the unique ID of the wavelength.
        public Dictionary<string, CircularBuffer<int>> IntTimeSeries { get; set; }

        // Map of accumulated float time series
        // The key is the unique ID of the wavelength.
        public Dictionary<string, CircularBuffer<float>> FloatTimeSeries { get; set; }

        // Time series time stamps differences
        public CircularBuffer<float> TimeSeriesTimeValues { get; set; }

        // Time series hit positions
        public CircularBuffer<float> TimeSeriesHits { get; set; }

        // Time points for accumulated spectra
        public float[] StaticAccumulatedSpectraTimepoints { get; set; }

        // Start index for accumulated spectra
        public int StaticAccumulatedSpectraStartIndex { get; set; }

        // End index for accumulated spectra
        public int StaticAccumulatedSpectraEndIndex { get; set; }

        // Processing start index
        public int StartIndex
        {
            get => SettingsManager.SaveStartPixel;

            set
            {
                if (this.Pixels == null)
                {
                    SettingsManager.SaveStartPixel = 1;
                }
                else
                {
                    if (value >= 0 && value < this.EndIndex && value < this.Pixels.Length)
                    {
                        SettingsManager.SaveStartPixel = value;
                    }
                    else
                    {
                        SettingsManager.SaveStartPixel = 0;
                    }
                }
            }
        }

        // Processing end index
        public int EndIndex
        {
            get => SettingsManager.SaveEndPixel;

            set
            {
                if (this.Pixels == null)
                {
                    SettingsManager.SaveEndPixel = -1;
                }
                else
                {
                    if (value >= 0 && value > this.StartIndex && value < this.Pixels.Length)
                    {
                        SettingsManager.SaveEndPixel = value;
                    }
                    else
                    {
                        SettingsManager.SaveEndPixel = this.Pixels.Length - 1;
                    }
                }
            }
        }

        // Processing start index
        public int StartWavelength
        {
            get => SettingsManager.SaveStartPixel;

            set
            {
                if (this.Pixels == null)
                {
                    SettingsManager.SaveStartPixel = 1;
                }
                else
                {
                    if (value >= 0 && value < this.EndIndex && value < this.Pixels.Length)
                    {
                        SettingsManager.SaveStartPixel = value;
                    }
                    else
                    {
                        SettingsManager.SaveStartPixel = 0;
                    }
                }
            }
        }

        // Processing end index
        public int EndWavelength
        {
            get => SettingsManager.SaveEndPixel;

            set
            {
                if (this.Pixels == null)
                {
                    SettingsManager.SaveEndPixel = -1;
                }
                else
                {
                    if (value >= 0 && value > this.StartIndex && value < this.Pixels.Length)
                    {
                        SettingsManager.SaveEndPixel = value;
                    }
                    else
                    {
                        SettingsManager.SaveEndPixel = this.Pixels.Length - 1;
                    }
                }
            }
        }

        #endregion properties
    }
}
