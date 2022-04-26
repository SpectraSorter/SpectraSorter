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
using spectra.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spectra
{
    public static class Experiment
    {
        public static void SetupStandardAcquisition()
        {
            // Check that the current plot type is compatible
            if (!(
                SettingsManager.CurrentPlotType == Options.PlotType.OUTPUT ||
                (SettingsManager.CurrentPlotType == Options.PlotType.TIMESERIES &&
                WavelengthManager.Instance.WavelengthsForPlotting.Count > 0)
                ))
            {
                SettingsManager.CurrentPlotType = Options.PlotType.OUTPUT;
            }

            // Build list of frequencies to save
            WavelengthManager.Instance.MapRequestedWavelengthsToSaveToIndices();

            // Build list of frequencies for thresholding
            WavelengthManager.Instance.MapThresholdingWavelengthsToIndices();

            // Build list of requested frequencies to plot
            WavelengthManager.Instance.MapRequestedWavelengthsToPlotToIndices();

            // Reset all buffers
            SpectrumProcessor.Instance.ResetAllBuffers();

            // This is not an accumulation experiment
            State.Instance.IsPerformingAccumulationAcquisition = false;

            // Reset the hit rate
            State.Instance.TotalHitNumber = 0;
        }

        /// <summary>
        /// Set up a normal acquisition with dynamic reference.
        /// </summary>
        public static void SetupAcquisitionWithDynamicReference()
        {
            // Set up a standard acquisition first
            Experiment.SetupStandardAcquisition();

            // Add the initialization of the dynamic reference
            if (SettingsManager.NumberOfSpectraForDynamicAccumulation > 0)
            {
                SpectrumProcessor.Instance.DynamicAccumulatedSpectraForReferenceEstimation =
                    new CircularBuffer<float[]>(capacity: SettingsManager.NumberOfSpectraForDynamicAccumulation);
            }
            else
            {
                SpectrumProcessor.Instance.DynamicAccumulatedSpectraForReferenceEstimation =
                    new CircularBuffer<float[]>(capacity: 1);
            }

            // This is not an accumulation experiment
            State.Instance.IsPerformingAccumulationAcquisition = false;

            // Reset the hit rate
            State.Instance.TotalHitNumber = 0;
        }

        /// <summary>
        /// Set up an experiment to accumulate reference spectra.
        /// </summary>
        public static void SetupStaticReferenceEstimationExperiment()
        {
            // Set up a standard acquisition first
            SetupStandardAcquisition();

            // Allocate space to accumulate spectra
            SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation = new CircularBuffer<float[]>(capacity: (int)65535);

            SpectrumProcessor.Instance.StaticAccumulatedSpectraXValues = new double[SpectrumProcessor.Instance.Wavelengths.Length];
            Array.Copy(SpectrumProcessor.Instance.Wavelengths, 
                SpectrumProcessor.Instance.StaticAccumulatedSpectraXValues, 
                SpectrumProcessor.Instance.Wavelengths.Length);

            // This is an accumulation experiment
            State.Instance.IsPerformingAccumulationAcquisition = true;

            // Reset the hit rate
            State.Instance.TotalHitNumber = 0;
        }
    }
}
