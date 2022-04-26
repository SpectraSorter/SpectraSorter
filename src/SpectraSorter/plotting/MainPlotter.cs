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

using spectra.processing;
using spectra.state;
using spectra.ui.components;
using spectra.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace spectra.plotting
{
    public class MainPlotter
    {
        #region members

        // Main chart reference
        private MainChart mMainChart;

        // Chart ranges
        private AxisLimits YAxisLimits;

        private AxisLimits XAxisLimits;

        #endregion members

        #region methods

        #region public

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="plotType">Plot type: one of the Options.PlotType plots.</param>
        /// <param name="autoYAxis">True to have the Y axis limits to be automatically rescaled.</param>
        public MainPlotter(MainChart chart)
        {
            // Store the reference to the chart
            this.mMainChart = chart;

            // Reset defaults
            this.Reset();
        }

        /// <summary>
        /// Clear all Series from the plot.
        ///
        /// The Series are preserved, but their Points are dropped.
        /// </summary>
        public void Clear()
        {
            // Clear the series
            this.mMainChart.ClearAllSeries();
        }

        public (double, double, double, double) GetCurrentDataRange()
        {
            return this.mMainChart.GetCurrentDataRange();
        }

        /// <summary>
        /// Resets axis limits, plot type and auto scale
        /// </summary>
        public void Reset()
        {
            // Set auto Y axis to false
            this.YAxisAutoRanges = false;

            // Initialize axis limits
            this.YAxisLimits = new AxisLimits();
            this.XAxisLimits = new AxisLimits();

            // Set the format of the ticks
            this.mMainChart.AxisX.LabelStyle.Format = "0.00";
            this.mMainChart.AxisY.LabelStyle.Format = "0.00";
        }

        /// <summary>
        /// Refresh the plot
        /// </summary>
        public void Refresh()
        {
            this.mMainChart.Refresh();
        }

        /// <summary>
        /// Plot current data.
        /// </summary>
        public void Plot()
        {
            switch (SettingsManager.CurrentPlotType)
            {
                case Options.PlotType.OUTPUT:

                    // Plot the spectrum
                    this.PlotOutputSpectrum();

                    break;

                case Options.PlotType.TIMESERIES:

                    // Plot the time series
                    this.PlotTimeSeries();

                    break;

                case Options.PlotType.DARK_SPECTRUM:

                    // Plot the dark spectrum
                    this.PlotDarkSpectrum();
                    break;

                case Options.PlotType.REFERENCE_SPECTRUM:

                    // Plot the (raw) reference spectrum
                    this.PlotReferenceSpectrum();

                    break;

                case Options.PlotType.CORRECTED_REFERENCE_SPECTRUM:

                    // Plot the (corrected) reference spectrum
                    this.PlotCorrectedReferenceSpectrum();

                    break;

                case Options.PlotType.ACCUMULATING_SPECTRUM:

                    // Plot the accumulating spectrum
                    this.PlotAccumulatingSpectrum();

                    break;

                case Options.PlotType.ACCUMULATED_SPECTRA:

                    // Plot the accumulated spectra
                    this.PlotAccumulatedSpectra();

                    break;

                case Options.PlotType.ACCUMULATING_TIMESERIES:

                    // Plot the accumulated time series
                    this.PlotAccumulatingTimeSeries();

                    break;

                case Options.PlotType.ACCUMULATED_TIMESERIES:

                    // Plot the accumulated time series
                    this.PlotAccumulatedTimeSeries();

                    break;

                default:

                    // Plot the spectrum
                    this.PlotOutputSpectrum();

                    break;
            }

            // Add thresholds
            this.PlotThresholds();
        }

        #region XAxis

        /// <summary>
        /// Sets the lower bound on the X axis to the specified value.
        /// </summary>
        /// <param name="value">Value of the lower bound.</param
        public void SetXAxisLowerBoundFromString(string value)
        {
            this.XAxisLimits.LowerBoundFromString(value);
        }

        /// <summary>
        /// Returns the lower bound on the X axis as String.
        /// </summary>
        public string GetXAxisLowerBoundAsString()
        {
            return this.XAxisLimits.LowerBoundToString();
        }

        /// <summary>
        /// Sets the upper bound on the X axis to the specified value.
        /// </summary>
        /// <param name="value">Value of the upper bound.</param
        public void SetXAxisUpperBoundFromString(string value)
        {
            this.XAxisLimits.UpperBoundFromString(value);
        }

        /// <summary>
        /// Returns the upper bound on the X axis as String.
        /// </summary>
        public string GetXAxisUpperBoundAsString()
        {
            return this.XAxisLimits.UpperBoundToString();
        }

        /// <summary>
        /// Sets the lower and upper bound on the X axis using the X data array.
        /// </summary>
        /// <param name="XData">X data array.</param>
        public void XAxisSetBoundsFromData(float[] XData)
        {
            this.XAxisLimits.SetMinMaxAllowedValuesForBoundsFromArray(XData);
            this.XAxisLimits.LowerBound = this.XAxisLimits.MinAllowedValueForUpperBound;
            this.XAxisLimits.UpperBound = this.XAxisLimits.MaxAllowedValueForLowerBound;
        }

        /// <summary>
        /// Sets the lower and upper bound on the X axis using the X data array.
        /// </summary>
        /// <param name="XData">X data array.</param>
        public void XAxisSetBoundsFromData(double[] XData)
        {
            this.XAxisLimits.SetMinMaxAllowedValuesForBoundsFromArray(XData);
            this.XAxisLimits.LowerBound = this.XAxisLimits.MinAllowedValueForUpperBound;
            this.XAxisLimits.UpperBound = this.XAxisLimits.MaxAllowedValueForLowerBound;
        }

        /// <summary>
        /// Reset both lower and upper X axis bounds.
        /// </summary>
        public void XAxisResetBothBounds()
        {
            this.XAxisLimits.ResetLowerBound();
            this.XAxisLimits.ResetUpperBound();
        }

        /// <summary>
        /// Reset the lower and upper bounds of the X axis as long as their minimum
        /// and maximum allowed values.
        /// </summary>
        public void XAxisResetBothBoundsAndAllowedBounds()
        {
            this.XAxisLimits.ResetMaxAllowedValueForLowerBound();
            this.XAxisLimits.ResetMinAllowedValueForUpperBound();
            this.XAxisLimits.ResetLowerBound();
            this.XAxisLimits.ResetUpperBound();
        }

        /// <summary>
        /// Reset lower X axis bound.
        /// </summary>
        public void XAxisResetLowerBound()
        {
            this.XAxisLimits.ResetLowerBound();
        }

        /// <summary>
        /// Reset upper X axis bound.
        /// </summary>
        public void XAxisResetUpperBound()
        {
            this.XAxisLimits.ResetUpperBound();
        }

        #endregion XAxis

        #region YAxis

        /// <summary>
        /// Sets the lower bound on the Y axis to the specified value.
        /// </summary>
        /// <param name="value">Value of the lower bound.</param>
        public void SetYAxisLowerBoundFromString(string value)
        {
            this.YAxisLimits.LowerBoundFromString(value);
        }

        /// <summary>
        /// Returns the lower bound on the Y axis as String.
        /// </summary>
        public string GetYAxisLowerBoundAsString()
        {
            return this.YAxisLimits.LowerBoundToString();
        }

        /// <summary>
        /// Sets the upper bound on the Y axis to the specified value.
        /// </summary>
        /// <param name="value">Value of the upper bound.</param>
        public void SetYAxisUpperBoundFromString(string value)
        {
            this.YAxisLimits.UpperBoundFromString(value);
        }

        /// <summary>
        /// Returns the upper bound on the Y axis as String.
        /// </summary>
        public string GetYAxisUpperBoundAsString()
        {
            return this.YAxisLimits.UpperBoundToString();
        }

        /// <summary>
        /// Sets the lower and upper bound on the Y axis using the Y data array.
        /// It does not change the minimum and maximum allowed values.
        /// </summary>
        /// <param name="YData">Y data array (circular buffer).</param>
        public void YAxisInitFromData(CircularBuffer<float[]> YData)
        {
            this.YAxisLimits.LowerBound = Utils.FindGlobalMin(YData);
            this.YAxisLimits.UpperBound = Utils.FindGlobalMax(YData);
        }

        /// <summary>
        /// Sets the Y axis bounds to match the minimum and maximum values allowed by the data.
        /// </summary>
        public void YAxisSetBoundsAsAllowedFromData(Dictionary<string, CircularBuffer<float>> MapYData)
        {
            this.YAxisLimits.MinAllowedValueForUpperBound = Utils.FindGlobalMin(MapYData);
            this.YAxisLimits.MaxAllowedValueForLowerBound = Utils.FindGlobalMax(MapYData);
            this.YAxisLimits.LowerBound = this.YAxisLimits.MinAllowedValueForUpperBound;
            this.YAxisLimits.UpperBound = this.YAxisLimits.MaxAllowedValueForLowerBound;
        }

        /// <summary>
        /// Sets the Y axis bounds to match the minimum and maximum values allowed by the data.
        /// </summary>
        public void YAxisSetBoundsAsAllowedFromData(Dictionary<string, CircularBuffer<int>> MapYData)
        {
            this.YAxisLimits.MinAllowedValueForUpperBound = Utils.FindGlobalMin(MapYData);
            this.YAxisLimits.MaxAllowedValueForLowerBound = Utils.FindGlobalMax(MapYData);
            this.YAxisLimits.LowerBound = this.YAxisLimits.MinAllowedValueForUpperBound;
            this.YAxisLimits.UpperBound = this.YAxisLimits.MaxAllowedValueForLowerBound;
        }

        /// <summary>
        /// Reset the lower and upper bounds of the Y axis but does not reset
        /// their minimum and maximum allowed values.
        /// </summary>
        public void YAxisResetBothBounds()
        {
            this.YAxisLimits.ResetLowerBound();
            this.YAxisLimits.ResetUpperBound();
        }

        /// <summary>
        /// Reset the lower and upper bounds of the Y axis as long as their minimum
        /// and maximum allowed values.
        /// </summary>
        public void YAxisResetBothBoundsAndAllowedBounds()
        {
            this.YAxisLimits.ResetMaxAllowedValueForLowerBound();
            this.YAxisLimits.ResetMinAllowedValueForUpperBound();
            this.YAxisLimits.ResetLowerBound();
            this.YAxisLimits.ResetUpperBound();
        }

        /// <summary>
        /// Reset lower Y axis bound.
        /// </summary>
        public void YAxisResetLowerBound()
        {
            this.YAxisLimits.ResetLowerBound();
        }

        /// <summary>
        /// Reset upper Y axis bound.
        /// </summary>
        public void YAxisResetUpperBound()
        {
            this.YAxisLimits.ResetUpperBound();
        }

        /// <summary>
        /// Sets the lower and upper bound on the Y axis using the currently set X range and the X and Y data arrays.
        /// </summary>
        /// <param name="XData">X data array (used to map the lower bound to an index).</param>
        /// <param name="YData">Y data array</param>
        public void YAxisSetBoundsConstrainedByCurrentXAxisRange(float[] XData, float[] YData)
        {
            int firstIndex = Utils.FindIndexOfClosestValueInSortedArray(XData, (float)this.XAxisLowerBound);
            int lastIndex = Utils.FindIndexOfClosestValueInSortedArray(XData, (float)this.XAxisUpperBound);
            this.YAxisLimits.SetMinMaxAllowedValuesForBoundsFromArrayAndRange(YData, firstIndex, lastIndex);
            this.YAxisLimits.LowerBound = this.YAxisLimits.MinAllowedValueForUpperBound;
            this.YAxisLimits.UpperBound = this.YAxisLimits.MaxAllowedValueForLowerBound;
        }

        /// <summary>
        /// Sets the lower and upper bound on the Y axis using the currently set X range and the X and Y data arrays.
        /// </summary>
        /// <param name="XData">X data array (used to map the lower bound to an index).</param>
        /// <param name="YData">Y data array</param>
        public void YAxisSetBoundsConstrainedByCurrentXAxisRange(double[] XData, float[] YData)
        {
            int firstIndex = Utils.FindIndexOfClosestValueInSortedArray(XData, (float)this.XAxisLowerBound);
            int lastIndex = Utils.FindIndexOfClosestValueInSortedArray(XData, (float)this.XAxisUpperBound);
            this.YAxisLimits.SetMinMaxAllowedValuesForBoundsFromArrayAndRange(YData, firstIndex, lastIndex);
            this.YAxisLimits.LowerBound = this.YAxisLimits.MinAllowedValueForUpperBound;
            this.YAxisLimits.UpperBound = this.YAxisLimits.MaxAllowedValueForLowerBound;
        }

        /// <summary>
        /// Sets the lower and upper bound on the Y axis using the currently set X range and the X and Y data arrays.
        /// </summary>
        /// <param name="XData">X data array (used to map the lower bound to an index).</param>
        /// <param name="YData">Y data array</param>
        public void YAxisSetBoundsConstrainedByCurrentXAxisRange(double[] XData, ushort[] YData)
        {
            int firstIndex = Utils.FindIndexOfClosestValueInSortedArray(XData, (float)this.XAxisLowerBound);
            int lastIndex = Utils.FindIndexOfClosestValueInSortedArray(XData, (float)this.XAxisUpperBound);
            this.YAxisLimits.SetMinMaxAllowedValuesForBoundsFromArrayAndRange(YData, firstIndex, lastIndex);
            this.YAxisLimits.LowerBound = this.YAxisLimits.MinAllowedValueForUpperBound;
            this.YAxisLimits.UpperBound = this.YAxisLimits.MaxAllowedValueForLowerBound;
        }

        #endregion YAxis

        #endregion public

        #region private

        /// <summary>
        /// Plot the processed spectrum.
        /// </summary>
        private void PlotOutputSpectrum()
        {
            MainSeries spectrumSeries = this.mMainChart.UseSeriesByName(PlottingConstants.OUTPUT_SERIES_NAME);
            spectrumSeries.SetAsStandardSeries();

            if (SpectrumProcessor.Instance.ResultSpectrum != null)
            {
                // If a change in the result spectrum...
                if (SpectrumProcessor.Instance.ResultSpectrum != this.ResultCharted)
                {
                    this.ResultCharted = SpectrumProcessor.Instance.ResultSpectrum;

                    if (this.ResultCharted == null)
                    {
                        spectrumSeries.Enabled = false;
                        spectrumSeries.IsVisibleInLegend = false;
                    }
                    else
                    {
                        spectrumSeries.Enabled = true;
                        spectrumSeries.IsVisibleInLegend = true;

                        // Plot the data
                        spectrumSeries.Points.DataBindXY(SpectrumProcessor.Instance.Wavelengths, this.ResultCharted);
                    }
                }

                // Set the X axis title
                this.mMainChart.AxisX.Title = "Wavelength [nm]";
                this.mMainChart.AxisX.LabelStyle.Format = "N2";
            }
            else
            {
                // Disable
                spectrumSeries.Enabled = false;
                spectrumSeries.IsVisibleInLegend = false;
                this.mMainChart.AxisX.Title = "";
            }

            // Update X and Y axis ranges
            this.SetXAxisRangeForAcquisition();
            this.SetYAxisRangeForAcquisition();
        }

        /// <summary>
        /// Plot time series
        /// </summary>
        private void PlotTimeSeries()
        {
            if (SpectrumProcessor.Instance.IntTimeSeries == null || SpectrumProcessor.Instance.TimeSeriesTimeValues == null)
            {
                this.mMainChart.AxisX.Title = "";
                return;
            }

            // Cache the queries
            List<Wavelength> wavelengths = WavelengthManager.Instance.Wavelengths;
            List<Wavelength> wavelengthsToPlot = WavelengthManager.Instance.WavelengthsForPlotting;

            // What we plot depends on the values of SettingsManager.NumberOfTimePointsToStore 
            // and SettingsManager.NumberOfTimePointsToPlot
            int startTimePoint = (int)(SettingsManager.NumberOfTimePointsToStore - SettingsManager.NumberOfTimePointsToPlot);

            if (SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.ABSORBANCE ||
                SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.TRANSMISSION)
            {
                foreach (Wavelength wavelength in wavelengths)
                {
                    if (! WavelengthManager.Contains(wavelengthsToPlot, wavelength.ID))
                    {
                        // If the series is plotted, hide it
                        MainSeries seriesToCheck = this.mMainChart.GetSeriesByID(wavelength.SeriesName);
                        if (seriesToCheck != null)
                        {
                            seriesToCheck.Enabled = false;
                            seriesToCheck.IsVisibleInLegend = false;
                            continue;
                        }
                    }

                    if (!SpectrumProcessor.Instance.FloatTimeSeries.ContainsKey(wavelength.ID))
                    {
                        // In case the wavelength was added after the acquisition started
                        continue;
                    }
                    string seriesName = wavelength.SeriesName;
                    string seriesID = wavelength.ID;
                    MainSeries s = this.mMainChart.UseSeriesByID(seriesID, seriesName);
                    s.SetAsTimeSeries();
                    s.Color = Color.FromArgb(wavelength.SeriesColor);
                    s.Enabled = true;
                    s.IsVisibleInLegend = true;

                    // What we plot depends on the values of SettingsManager.NumberOfTimePointsToStore 
                    // and SettingsManager.NumberOfTimePointsToPlot
                    s.Points.DataBindXY(
                        new ArraySegment<float>(
                            SpectrumProcessor.Instance.TimeSeriesTimeValues.ToArray(), 
                            startTimePoint, 
                            (int)SettingsManager.NumberOfTimePointsToPlot),
                        new ArraySegment<float>(
                            SpectrumProcessor.Instance.FloatTimeSeries[seriesID].ToArray(),
                            startTimePoint,
                            (int)SettingsManager.NumberOfTimePointsToPlot));
                }
            }
            else
            {
                foreach (Wavelength wavelength in wavelengths)
                {
                    if (!WavelengthManager.Contains(wavelengthsToPlot, wavelength.ID))
                    {
                        // If the series is plotted, hide it
                        MainSeries seriesToCheck = this.mMainChart.GetSeriesByID(wavelength.SeriesName);
                        if (seriesToCheck != null)
                        {
                            seriesToCheck.Enabled = false;
                            seriesToCheck.IsVisibleInLegend = false;
                            continue;
                        }
                    }

                    if (!SpectrumProcessor.Instance.IntTimeSeries.ContainsKey(wavelength.ID))
                    {
                        // In case the wavelength was added after the acquisition started
                        continue;
                    }
                    string seriesName = wavelength.SeriesName;
                    string seriesID = wavelength.ID;
                    MainSeries s = this.mMainChart.UseSeriesByID(seriesID, seriesName);
                    s.SetAsTimeSeries();
                    s.Color = Color.FromArgb(wavelength.SeriesColor);
                    s.Enabled = true;
                    s.IsVisibleInLegend = true;

                    s.Points.DataBindXY(
                        new ArraySegment<float>(
                            SpectrumProcessor.Instance.TimeSeriesTimeValues.ToArray(),
                            startTimePoint,
                            (int)SettingsManager.NumberOfTimePointsToPlot),
                        new ArraySegment<int>(
                            SpectrumProcessor.Instance.IntTimeSeries[seriesID].ToArray(),
                            startTimePoint,
                            (int)SettingsManager.NumberOfTimePointsToPlot));
                }
            }

            // In the case of time series, we always reset the x axis limits
            this.XAxisResetBothBoundsAndAllowedBounds();

            // If the X Axis Limits have not been set yet, initialize them to reasonable values
            if (!this.XAxisIsLowerBoundSet || !this.XAxisIsUpperBoundSet)
            {
                this.XAxisLowerBound = SpectrumProcessor.Instance.TimeSeriesTimeValues[startTimePoint];
                this.XAxisUpperBound = SpectrumProcessor.Instance.TimeSeriesTimeValues.Last<float>();
            }

            // Update the X axis range
            this.SetXAxisRangeForAcquisition();

            // Update the Y range
            if (this.YAxisAutoRanges)
            {
                if (SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.ABSORBANCE ||
                    SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.TRANSMISSION)
                {
                    this.YAxisSetBoundsAsAllowedFromData(SpectrumProcessor.Instance.FloatTimeSeries);
                }
                else
                {
                    this.YAxisSetBoundsAsAllowedFromData(SpectrumProcessor.Instance.IntTimeSeries);
                }
            }
            this.SetYAxisRangeForAcquisition();

            // Plot the hits
            PlotTriggerPoints();

            this.mMainChart.AxisX.Title = "Acquisition time [ms] (last " + SettingsManager.NumberOfTimePointsToStore.ToString(CultureInfo.InvariantCulture) + " points)";
            this.mMainChart.AxisX.LabelStyle.Format = "N2";
        }

        /// <summary>
        /// Plot trigger points on time series.
        /// </summary>
        private void PlotTriggerPoints()
        {
            if (SettingsManager.PlotTriggerPoints == false)
            {
                return;
            }

            if (SettingsManager.CurrentPlotType != Options.PlotType.TIMESERIES)
            {
                return;
            }

            float firstDelta = SpectrumProcessor.Instance.TimeSeriesTimeValues[0];
            for (int i = 0; i < SpectrumProcessor.Instance.TimeSeriesHits.Count; i++)
            {
                float currentDelta = SpectrumProcessor.Instance.TimeSeriesHits[i];
                if (currentDelta < firstDelta)
                {
                    // This is too much in the past
                    continue;
                }

                // Build the line and add it
                MainSeries triggerSeries = this.mMainChart.UseSeriesByName("Trigger" + i);
                triggerSeries.SetAsTriggerSeries();
                triggerSeries.Enabled = true;
                triggerSeries.IsVisibleInLegend = false;
                triggerSeries.Points.DataBindXY(
                    new float[2] { currentDelta, currentDelta },
                    new float[2] { (float)this.mMainChart.AxisY.Minimum, (float)this.mMainChart.AxisY.Maximum });

            }
        }

        /// <summary>
        /// Plot accumulating spectrum.
        /// </summary>
        private void PlotAccumulatingSpectrum()
        {
            // Also, an accumulation experiment should be running
            if (State.Instance.IsPerformingAccumulationAcquisition == false)
            {
                // Change back to OUTPUT
                SettingsManager.CurrentPlotType = Options.PlotType.OUTPUT;
                return;
            }

            // Check that we have accumulated something already
            if (SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation == null)
            {
                this.mMainChart.AxisX.Title = "";
                return;
            }

            MainSeries accumulatingSeries = this.mMainChart.UseSeriesByName(PlottingConstants.ACCUMULATING_SPECTRUM_SERIES_NAME);
            accumulatingSeries.SetAsStandardSeries();

            int index;
            float[] accumulatingSpectrum;

            try
            {
                index = SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count - 1;
                accumulatingSpectrum = SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation[index];
            }
            catch (Exception)
            {
                return;
            }

            if (accumulatingSpectrum != null)
            {
                // If a change in the result spectrum...
                if (accumulatingSpectrum != this.AccumulatingCharted)
                {
                    this.AccumulatingCharted = accumulatingSpectrum;

                    if (this.AccumulatingCharted == null)
                    {
                        accumulatingSeries.Enabled = false;
                        accumulatingSeries.IsVisibleInLegend = false;
                    }
                    else
                    {
                        accumulatingSeries.Enabled = true;
                        accumulatingSeries.IsVisibleInLegend = true;

                        // Plot the data
                        accumulatingSeries.Points.DataBindXY(SpectrumProcessor.Instance.Wavelengths, this.AccumulatingCharted);

                        // Update the X range
                        this.SetXAxisRangeForAcquisition();

                        // Update the Y range
                        if (this.YAxisAutoRanges)
                        {
                            // Update the bounds
                            this.YAxisSetBoundsConstrainedByCurrentXAxisRange(SpectrumProcessor.Instance.Wavelengths, this.AccumulatingCharted);
                        }

                        this.SetYAxisRangeForAcquisition();
                    }

                    // Set the X axis title
                    this.mMainChart.AxisX.Title = "Wavelength [nm]";
                    this.mMainChart.AxisX.LabelStyle.Format = "N2";
                }
             }
            else
            {
                // Disable
                this.mMainChart.AxisX.Title = "";
                accumulatingSeries.Enabled = false;
                accumulatingSeries.IsVisibleInLegend = false;
            }

        }

        /// <summary>
        /// Plot accumulated spectra
        /// </summary>
        private void PlotAccumulatedSpectra()
        {
            // Do we have some spectra to plot?
            if (SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation == null ||
                SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count == 0)
            {
                this.mMainChart.AxisX.Title = "";
                return;
            }

            // Get the index from the slider
            int spectrumNumber = SpectrumProcessor.Instance.SelectedStaticAccumulatedSpectrumNumber;

            if (spectrumNumber < 0)
            {
                spectrumNumber = 0;
            }

            if (spectrumNumber > SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count - 1)
            {
                spectrumNumber = SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count - 1;
            }

            // Retrieve the spectrum
            float[] spectrum = SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation[spectrumNumber];

            // Plot
            MainSeries s = this.mMainChart.UseSeriesByName(PlottingConstants.ACCUMULATED_SPECTRA_SERIES_NAME);
            s.SetAsStandardSeries();

            // Plot data
            s.Points.DataBindXY(SpectrumProcessor.Instance.StaticAccumulatedSpectraXValues, spectrum);

            // Update the X axis range
            this.SetXAxisRangeForAcquisition();

            // Update the Y axis range
            if (this.YAxisAutoRanges)
            {
                // Update the bounds
                this.YAxisSetBoundsConstrainedByCurrentXAxisRange(SpectrumProcessor.Instance.StaticAccumulatedSpectraXValues, spectrum);
            }
            this.SetYAxisRangeForAcquisition();

            // Set the X axis title
            this.mMainChart.AxisX.Title = "Wavelength [nm]";
            this.mMainChart.AxisX.LabelStyle.Format = "N2";
        }

        /// <summary>
        /// Plot the dark spectrum.
        /// </summary>
        private void PlotDarkSpectrum()
        {
            MainSeries darkSeries = this.mMainChart.UseSeriesByName(PlottingConstants.DARK_SERIES_NAME);
            darkSeries.SetAsStandardSeries();

            // Anything to plot?
            if (SpectrumProcessor.Instance.DarkSpectrum == null)
            {
                this.mMainChart.AxisX.Title = "";
                darkSeries.Enabled = false;
                darkSeries.IsVisibleInLegend = false;
            }
            else if (SpectrumProcessor.Instance.DarkSpectrum != this.DarkCharted || darkSeries.Points.Count == 0)
            {
                // Is the spectrum already plotted? Otherwise, replace the data and update the reference.

                // Make the series visible
                darkSeries.Enabled = true;
                darkSeries.IsVisibleInLegend = true;

                // Replace the data
                darkSeries.Points.Clear();
                darkSeries.Points.DataBindXY(
                    SpectrumProcessor.Instance.Wavelengths,
                    SpectrumProcessor.Instance.DarkSpectrum);

                // Update the reference
                this.DarkCharted = SpectrumProcessor.Instance.DarkSpectrum;

                // Set the X axis title
                this.mMainChart.AxisX.Title = "Wavelength [nm]";
                this.mMainChart.AxisX.LabelStyle.Format = "N2";
            }

            // Set the axis
            this.SetXAxisRangeForAcquisition();
            this.SetYAxisRangeForAcquisition();
        }

        /// <summary>
        /// Plot raw reference spectrum.
        /// </summary>
        private void PlotReferenceSpectrum()
        {
            MainSeries referenceSeries = this.mMainChart.UseSeriesByName(PlottingConstants.REFERENCE_SERIES_NAME);
            referenceSeries.SetAsStandardSeries();

            // Anything to plot?
            if (SpectrumProcessor.Instance.ReferenceSpectrum == null)
            {
                this.mMainChart.AxisX.Title = "";
                referenceSeries.Enabled = false;
                referenceSeries.IsVisibleInLegend = false;
            } 
            else if (SpectrumProcessor.Instance.ReferenceSpectrum != this.ReferenceCharted || referenceSeries.Points.Count == 0)
            {
                // Is the spectrum already plotted? Otherwise, replace the data and update the reference

                // Make the series visible
                referenceSeries.Enabled = true;
                referenceSeries.IsVisibleInLegend = true;

                // Replace the data
                referenceSeries.Points.Clear();
                referenceSeries.Points.DataBindXY(
                    SpectrumProcessor.Instance.Wavelengths,
                    SpectrumProcessor.Instance.ReferenceSpectrum);

                // Update the reference
                this.ReferenceCharted = SpectrumProcessor.Instance.ReferenceSpectrum;

                // Set the X axis title
                this.mMainChart.AxisX.Title = "Wavelength [nm]";
                this.mMainChart.AxisX.LabelStyle.Format = "N2";
            }

            // Update the axis ranges
            this.SetXAxisRangeForAcquisition();
            this.SetYAxisRangeForAcquisition();
        }

        /// <summary>
        /// Plot the dark-corrected reference spectrum.
        /// </summary>
        private void PlotCorrectedReferenceSpectrum()
        {
            MainSeries corrReferenceSeries = this.mMainChart.UseSeriesByName(PlottingConstants.CORR_REFERENCE_SERIES_NAME);
            corrReferenceSeries.SetAsStandardSeries();

            // Anything to plot?
            if (SpectrumProcessor.Instance.ReferenceCorrectedSpectrum == null)
            {
                this.mMainChart.AxisX.Title = "";
                corrReferenceSeries.Enabled = false;
                corrReferenceSeries.IsVisibleInLegend = false;

            }
            else if (SpectrumProcessor.Instance.ReferenceCorrectedSpectrum != this.ReferenceCorrectedCharted || corrReferenceSeries.Points.Count == 0)
            {
                // Is the spectrum already plotted? Otherwise, replace the data and update the reference

                // Make the series visible
                corrReferenceSeries.Enabled = true;
                corrReferenceSeries.IsVisibleInLegend = true;

                // Replace the data
                corrReferenceSeries.Points.Clear();
                corrReferenceSeries.Points.DataBindXY(
                    SpectrumProcessor.Instance.Wavelengths,
                    SpectrumProcessor.Instance.ReferenceCorrectedSpectrum);

                // Update the reference
                this.ReferenceCorrectedCharted = SpectrumProcessor.Instance.ReferenceCorrectedSpectrum;

                // Set the X axis title
                this.mMainChart.AxisX.Title = "Wavelength [nm]";
                this.mMainChart.AxisX.LabelStyle.Format = "N2";
            }

            // Set the axis
            this.SetXAxisRangeForAcquisition();
            this.SetYAxisRangeForAcquisition();
        }

        /// <summary>
        /// Plot accumulated time series.
        /// </summary>
        private void PlotAccumulatedTimeSeries()
        {
            // The accumulation is finished
            if (WavelengthManager.Instance.WavelengthsForPlotting == null ||
                WavelengthManager.Instance.WavelengthsForPlotting.Count == 0)
            {
                return;
            }

            if (SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation == null ||
                SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count == 0)
            {
                return;
            }

            if (SpectrumProcessor.Instance.StaticAccumulatedSpectraTimepoints == null ||
                SpectrumProcessor.Instance.StaticAccumulatedSpectraTimepoints.Length != 
                SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count)
            {
                SpectrumProcessor.Instance.StaticAccumulatedSpectraTimepoints = 
                    SpectrumProcessor.Instance.StaticAccumulatedSpectraTimepoints = Utils.FloatRange(0,
                    SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count - 1, 1.0f);
            }

            /// Cache the query
            List<Wavelength> wavelengthsToPlot = WavelengthManager.Instance.WavelengthsForPlotting;

            if (wavelengthsToPlot.Count != 0)
            {
                WavelengthManager.Instance.MapRequestedWavelengthsToPlotToIndices();
            }

            for (int i = 0; i < wavelengthsToPlot.Count; i++)
            {
                string seriesName = wavelengthsToPlot[i].SeriesName;
                MainSeries s = this.mMainChart.UseSeriesByID(wavelengthsToPlot[i].ID, seriesName);
                s.SetAsStandardSeries();
                s.Enabled = true;
                s.Color = Color.FromArgb(wavelengthsToPlot[i].SeriesColor);
                s.Points.DataBindXY(
                    SpectrumProcessor.Instance.StaticAccumulatedSpectraTimepoints, 
                    SpectrumProcessor.Instance.GetStaticAccumulatedWavelength(wavelengthsToPlot[i].Index));
            }

            this.SetXAxisRangeForAcquisition();

            // Update the Y range
            if (this.YAxisAutoRanges)
            {
                this.YAxisInitFromData(SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation);
            }
            this.SetYAxisRangeForAcquisition();

            // Set the X axis title
            this.mMainChart.AxisX.Title = "Spectrum index";
            this.mMainChart.AxisX.LabelStyle.Format = "D";
        }

        /// <summary>
        /// Plot accumulating time series.
        /// </summary>
        private void PlotAccumulatingTimeSeries()
        {
            // Also, an accumulation experiment should be running
            if (State.Instance.IsPerformingAccumulationAcquisition == false)
            {
                // Change back to OUTPUT
                SettingsManager.CurrentPlotType = Options.PlotType.OUTPUT;
                return;
            }

            // If the accumulating spectrum curve is displayed, hide it
            MainSeries acc = this.mMainChart.FindSeriesByName("Accumulating");
            if (acc != null)
            {
                acc.Enabled = false;
                acc.IsVisibleInLegend = false;
            }

            // Extract the time series to plot from the running accumulation
            (float[] timePoints, Dictionary<string, CircularBuffer<float>> timeSeries) =
                SpectrumProcessor.Instance.buildRunningTimeSeriesFromAccumulatingData();

            foreach (KeyValuePair<string, CircularBuffer<float>> entry in timeSeries)
            {
                // Get the wavelength
                Wavelength wavelength = WavelengthManager.Instance.GetWavelengthByID(entry.Key);
                if (wavelength == null)
                {
                    continue;
                }

                // Plot the values for current wavelength
                string seriesName = wavelength.SeriesName;
                string seriesID = wavelength.ID;
                MainSeries s = this.mMainChart.UseSeriesByID(seriesID, seriesName);
                s.SetAsTimeSeries();
                s.Color = Color.FromArgb(wavelength.SeriesColor);
                s.Enabled = true;
                s.IsVisibleInLegend = true;
                s.Points.DataBindXY(timePoints, entry.Value.ToArray<float>());
            }

            // Update X and Y axis ranges
            this.SetXAxisRangeForAccumulatingAcquisition(timePoints);
            this.SetYAxisRangeForAccumulatingAcquisition(timeSeries);

            // Set the X axis title
            this.mMainChart.AxisX.Title = "Last " + timePoints.Length.ToString(CultureInfo.InvariantCulture) + " acquired spectra";
            this.mMainChart.AxisX.LabelStyle.Format = "D";
        }

        /// <summary>
        /// Plot current threshold values.
        /// </summary>
        private void PlotThresholds()
        {
            // Do not plot if there is nothing else on the chart
            if (this.mMainChart.GetNumberOfVisibleSeries() == 0)
            {
                return;
            }

            // Cache the query
            List<Wavelength> wavelengthsForThresholding = WavelengthManager.Instance.WavelengthsForThresholding;
            
            // Are there thresholds series to hide?
            foreach(MainSeries s in this.mMainChart.Series)
            {
                if (!s.IsThresholdSeries())
                {
                    continue;
                }

                bool found = false;
                foreach(Wavelength wavelength in wavelengthsForThresholding)
                {
                    if (wavelength.ID.Equals(s.ID, StringComparison.InvariantCultureIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    s.Enabled = false;
                    s.IsVisibleInLegend = false;
                }               
            }

            if (wavelengthsForThresholding.Count == 0)
            {
                return;
            }

            if (SpectrumProcessor.Instance.Wavelengths.Length == 0)
            {
                return;
            }

            // Get or prepare series for the thresholds.
            // Important: current X axis limits are preserved!
            for (int i = 0; i < wavelengthsForThresholding.Count; i++)
            {
                double intensity = wavelengthsForThresholding[i].ThresholdValue;

                string seriesName = wavelengthsForThresholding[i].ThresholdSeriesName;
                MainSeries s = this.mMainChart.UseSeriesByID(wavelengthsForThresholding[i].ThresholdID, seriesName);
                s.SetAsThresholdSeries();
                s.Color = Color.FromArgb(wavelengthsForThresholding[i].SeriesColor);


                double[] X = new double[2]
                {
                    this.mMainChart.AxisX.Minimum,
                    this.mMainChart.AxisX.Maximum
                };
                double[] Y = new double[2]
                {
                    intensity,
                    intensity
                };

                // Display only if requested
                s.Enabled = SettingsManager.PlotThresholds;
                s.IsVisibleInLegend = SettingsManager.PlotThresholds;

                // Plot
                s.Points.DataBindXY(X, Y);
            }
        }

        /// <summary>
        /// Set the X axis range.
        /// </summary>
        private void SetXAxisRangeForAcquisition()
        {
            if (SpectrumProcessor.Instance.Wavelengths == null ||
                SpectrumProcessor.Instance.Wavelengths.Length == 0)
            {
                return;
            }

            // If the X Axis Limits have not been set yet, initialize them to reasonable values
            if (!this.XAxisIsLowerBoundSet || !this.XAxisIsUpperBoundSet)
            {
                if (SettingsManager.CurrentPlotType == Options.PlotType.ACCUMULATED_TIMESERIES)
                {
                    if (SpectrumProcessor.Instance.StaticAccumulatedSpectraTimepoints == null ||
                        SpectrumProcessor.Instance.StaticAccumulatedSpectraTimepoints.Length == 0)
                    {
                        return;
                    }
                    this.XAxisSetBoundsFromData(SpectrumProcessor.Instance.StaticAccumulatedSpectraTimepoints);

                } else if (SettingsManager.CurrentPlotType == Options.PlotType.ACCUMULATED_SPECTRA)
                {
                    if (SpectrumProcessor.Instance.StaticAccumulatedSpectraXValues == null ||
                        SpectrumProcessor.Instance.StaticAccumulatedSpectraXValues.Length == 0)
                    {
                        return;
                    }
                    this.XAxisSetBoundsFromData(SpectrumProcessor.Instance.StaticAccumulatedSpectraXValues);
                }
                else
                {
                    this.XAxisSetBoundsFromData(SpectrumProcessor.Instance.Wavelengths);
                }
            }

            // Set the X axis range in the chart
            if (this.XAxisIsLowerBoundSet)
            {
                this.mMainChart.AxisX.Minimum = this.XAxisLowerBound;
            }
            else
            {
                this.mMainChart.AxisX.Minimum = SpectrumProcessor.Instance.Wavelengths[0];
            }

            if (this.XAxisIsUpperBoundSet)
            {
                this.mMainChart.AxisX.Maximum = this.XAxisUpperBound;
            }
            else
            {
                this.mMainChart.AxisX.Maximum = SpectrumProcessor.Instance.Wavelengths[SpectrumProcessor.Instance.Wavelengths.Length - 1];
            }
        }

        /// <summary>
        /// Set the X axis range.
        /// </summary>
        private void SetXAxisRangeForAccumulatingAcquisition(float[] timePoints)
        {
            // In the case of time series, we always reset the x axis limits
            this.XAxisResetBothBoundsAndAllowedBounds();

            // If the X Axis Limits have not been set yet, initialize them to reasonable values
            if (!this.XAxisIsLowerBoundSet || !this.XAxisIsUpperBoundSet)
            {
                this.XAxisSetBoundsFromData(timePoints);
            }

            // Set the X axis range in the chart
            if (this.XAxisIsLowerBoundSet)
            {
                this.mMainChart.AxisX.Minimum = this.XAxisLowerBound;
            }
            else
            {
                this.mMainChart.AxisX.Minimum = timePoints[0];
            }

            if (this.XAxisIsUpperBoundSet)
            {
                this.mMainChart.AxisX.Maximum = this.XAxisUpperBound;
            }
            else
            {
                this.mMainChart.AxisX.Maximum = timePoints[timePoints.Length - 1];
            }
        }

        /// <summary>
        /// Set the Y axis range (with or without autoscale).
        /// </summary>
        /// <param name="dynamic">If yes, the X axis range is updated every time from the data.</param>
        private void SetYAxisRangeForAcquisition()
        {
            // Update the Y range
            if (this.YAxisAutoRanges)
            {
                // Update the bounds
                if (SpectrumProcessor.Instance.Wavelengths != null &&
                    SpectrumProcessor.Instance.Wavelengths.Length > 0)
                {
                    if (SettingsManager.CurrentPlotType == Options.PlotType.OUTPUT &&
                        this.ResultCharted != null)
                    {
                        this.YAxisSetBoundsConstrainedByCurrentXAxisRange(SpectrumProcessor.Instance.Wavelengths,
                            this.ResultCharted);
                    }
                    else if (SettingsManager.CurrentPlotType == Options.PlotType.DARK_SPECTRUM &&
                        this.DarkCharted != null)
                    {
                        this.YAxisSetBoundsConstrainedByCurrentXAxisRange(SpectrumProcessor.Instance.Wavelengths,
                            this.DarkCharted);

                    }
                    else if (SettingsManager.CurrentPlotType == Options.PlotType.REFERENCE_SPECTRUM &&
                      this.ReferenceCharted != null)
                    {
                        this.YAxisSetBoundsConstrainedByCurrentXAxisRange(SpectrumProcessor.Instance.Wavelengths,
                            this.ReferenceCharted);
                    }
                    else if (SettingsManager.CurrentPlotType == Options.PlotType.CORRECTED_REFERENCE_SPECTRUM &&
                        this.ReferenceCorrectedCharted != null)
                    {
                        this.YAxisSetBoundsConstrainedByCurrentXAxisRange(SpectrumProcessor.Instance.Wavelengths,
                            this.ReferenceCorrectedCharted);
                    }
                }
            }

            // Set the minimum Y value
            if (this.YAxisIsLowerBoundSet)
            {
                this.mMainChart.AxisY.Minimum = this.YAxisLowerBound;
            }
            else
            {
                // Set the minimum Y value based on the spectrum type
                if (SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.RAW_SPECTRUM)
                {
                    this.mMainChart.AxisY.Maximum = 0;
                }
                else if (SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.DARK_CORRECTED)
                {
                    this.mMainChart.AxisY.Maximum = -0.05 * SpectrumConstants.SATURATION_LEVEL;
                }
                else if (SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.ABSORBANCE)
                {
                    this.mMainChart.AxisY.Maximum = -1 * SpectrumConstants.MAX_ABSORBANCE;
                }
                else
                {
                    this.mMainChart.AxisY.Maximum = -1 * SpectrumConstants.MAX_TRANSMISSION;
                }
            }

            // Set the maximum Y value
            if (this.YAxisIsUpperBoundSet)
            {
                this.mMainChart.AxisY.Maximum = this.YAxisUpperBound;
            }
            else
            {
                // Set the maximum Y value based on the spectrum type
                if (SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.RAW_SPECTRUM ||
                    SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.DARK_CORRECTED)
                {
                    this.mMainChart.AxisY.Maximum = SpectrumConstants.SATURATION_LEVEL;
                }
                else if (SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.ABSORBANCE)
                {
                    this.mMainChart.AxisY.Maximum = SpectrumConstants.MAX_ABSORBANCE;
                }
                else
                {
                    this.mMainChart.AxisY.Maximum = SpectrumConstants.MAX_TRANSMISSION;
                }
            }
        }

        /// <summary>
        /// Set the Y axis range (with or without autoscale).
        /// </summary>
        /// <param name="dynamic">If yes, the X axis range is updated every time from the data.</param>
        private void SetYAxisRangeForAccumulatingAcquisition(Dictionary<string, CircularBuffer<float>> timeSeries)
        {
            // Update the Y range
            if (this.YAxisAutoRanges)
            {
                if (SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.ABSORBANCE ||
                    SettingsManager.ResultSpectrumType == Options.ResultSpectrumType.TRANSMISSION)
                {
                    this.YAxisSetBoundsAsAllowedFromData(timeSeries);
                }
                else
                {
                    this.YAxisSetBoundsAsAllowedFromData(timeSeries);
                }
            }

            if (this.YAxisIsLowerBoundSet)
            {
                this.mMainChart.AxisY.Minimum = this.YAxisLowerBound;
            }
            else
            {
                this.mMainChart.AxisY.Minimum = 0;
            }

            if (this.YAxisIsUpperBoundSet)
            {
                this.mMainChart.AxisY.Maximum = this.YAxisUpperBound;
            }
        }

        #endregion private

        #endregion methods

        #region properties

        #region YAxis

        public bool YAxisAutoRanges { get; set; }

        #region LowerBound

        public double YAxisLowerBound
        {
            get => this.YAxisLimits.LowerBound;
            set => this.YAxisLimits.LowerBound = value;
        }

        public bool YAxisIsLowerBoundSet
        {
            get => this.YAxisLimits.IsLowerBoundSet();
        }

        #endregion LowerBound

        #region HigherBound

        public double YAxisUpperBound
        {
            get => this.YAxisLimits.UpperBound;
            set => this.YAxisLimits.UpperBound = value;
        }

        public bool YAxisIsUpperBoundSet
        {
            get => this.YAxisLimits.IsUpperBoundSet();
        }

        #endregion HigherBound

        #endregion YAxis

        #region XAxis

        #region LowerBound

        public double XAxisLowerBound
        {
            get => this.XAxisLimits.LowerBound;
            set => this.XAxisLimits.LowerBound = value;
        }

        public bool XAxisIsLowerBoundSet
        {
            get => this.XAxisLimits.IsLowerBoundSet();
        }

        #endregion LowerBound

        #region HigherBound

        public double XAxisUpperBound
        {
            get => this.XAxisLimits.UpperBound;
            set => this.XAxisLimits.UpperBound = value;
        }

        public bool XAxisIsUpperBoundSet
        {
            get => this.XAxisLimits.IsUpperBoundSet();
        }

        #endregion HigherBound

        #endregion XAxis

        #region charted

        // Plotted dark spectrum
        public ushort[] DarkCharted { get; set; }

        // Plotted reference spectrum
        public ushort[] ReferenceCharted { get; set; }

        // Reference corrected charted
        public float[] ReferenceCorrectedCharted { get; set; }

        // Plotted accumulating spectrum
        public float[] AccumulatingCharted { get; set; }

        // Plotted result spectrum
        public float[] ResultCharted { get; set; }

        #endregion charted

        #endregion properties
    }
}