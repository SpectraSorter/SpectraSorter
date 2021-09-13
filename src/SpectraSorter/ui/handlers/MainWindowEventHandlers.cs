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

using spectra.processing;
using spectra.state;
using spectra.ui.components;
using System;
using System.Globalization;
using System.Windows.Forms;
using static spectra.state.SettingsManager;

namespace spectra.ui
{
    public partial class MainWindow
    {
        #region event_handlers

        private void RegisterEventHandlers()
        {
            SettingsManager.ToggleFiltering += ToggleFilteringHandler;
            SettingsManager.ToggleThresholding += ToggleThresholdingHandler;
            SettingsManager.ToggleSaving += ToggleSavingHandler;
            SettingsManager.ChangeReferenceType += ChangeReferenceTypeHandler;
            SettingsManager.ChangePlotType += ChangePlotTypeHandler;
            SettingsManager.ChangeResultSpectrumType += ChangeResultSpectrumTypeHandler;
            SettingsManager.ChangeSpectrumThresholdingWavelengths += ChangeSpectrumThresholdingWavelengthsHandler;
            SettingsManager.ChangeNumberOfSpectraForDynamicAccumulation += ChangeNumberOfSpectraForDynamicAccumulationEventHandler;
            PlotOptionsControl.ToggleYAxisAutoScale += ToggleYAxisAutoScaleHandler;

        }

        // Event handlers
        void ToggleFilteringHandler(object sender, EventArgs e)
        {
            SingleBooleanEventArgs te = (SingleBooleanEventArgs)e;
            if (te.Enabled)
            {
                // Update the menu bars
                enabledFilteringToolStripMenuItem.Checked = true;

                // Update the status bar
                toolStripStatusFilteringLabel.Text = "Filtering: On";

                // Update the filter
                SpectrumFilterer.Instance.InitializeFilterFromCurrentSettings();
            }
            else
            {
                // Update the menu bars
                enabledFilteringToolStripMenuItem.Checked = false;

                // Update the status bar
                toolStripStatusFilteringLabel.Text = "Filtering: Off";
            }
        }

        void ToggleThresholdingHandler(object sender, EventArgs e)
        {
            SingleBooleanEventArgs te = (SingleBooleanEventArgs)e;
            if (te.Enabled)
            {
                // Update the menu bars
                enabledTriggeringToolStripMenuItem.Checked = true;

                // Update the status bar
                toolStripStatusThresholdingLabel.Text = "Triggering: On";
            }
            else
            {
                // Update the menu bars
                enabledTriggeringToolStripMenuItem.Checked = false;

                // Update the status bar
                toolStripStatusThresholdingLabel.Text = "Triggering: Off";
            }
        }

        void ToggleSavingHandler(object sender, EventArgs e)
        {
            SingleBooleanEventArgs te = (SingleBooleanEventArgs)e;
            if (te.Enabled)
            {
                // Update the menu bars
                enableSaveToFileToolStripMenuItem.Checked = true;

                // Update the status bar
                if (State.Instance.IsPerformingStandardAcquisition) 
                {
                    toolStripStatusSavingLabel.Text = "Pause saving";
                }
                else
                {
                    toolStripStatusSavingLabel.Text = "Saving to file: On";
                }
            }
            else
            {
                // Update the menu bars
                enableSaveToFileToolStripMenuItem.Checked = false;

                // Update the status bar
                if (State.Instance.IsPerformingStandardAcquisition)
                {
                    toolStripStatusSavingLabel.Text = "Resume saving";
                }
                else
                {
                    toolStripStatusSavingLabel.Text = "Saving to file: Off";
                }
            }
        }

        /// <summary>
        /// Perform needed operations when the reference type changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ChangeReferenceTypeHandler(object sender, EventArgs e)
        {
            ReferenceTypeEventArgs te = (ReferenceTypeEventArgs)e;

            doNotUseReferenceToolStripMenuItem.Checked = false;
            staticToolStripMenuItem.Checked = false;
            dynamicReferenceToolStripMenuItem.Checked = false;

            switch (te.Type)
            {
                case Options.ReferenceType.NONE:

                    // Clear reference (but do not touch the dark spectrum)
                    clearReferenceSpectrum();

                    doNotUseReferenceToolStripMenuItem.Checked = true;
                    labelCorrectedReferenceSpectrumStatus.Text = "Not available.";
                    break;

                case Options.ReferenceType.STATIC:

                    staticToolStripMenuItem.Checked = true;

                    // Do not modify the labels!
                    break;

                case Options.ReferenceType.STATIC_SINGLE:

                    staticToolStripMenuItem.Checked = true;
                    toolStripStatusReferenceLabel.Text = "Reference: single";
                    labelCorrectedReferenceSpectrumStatus.Text = "Single static corrected spectrum available.";
                    break;

                case Options.ReferenceType.STATIC_ACCUMULATED:

                    staticToolStripMenuItem.Checked = true;
                    toolStripStatusReferenceLabel.Text = "Reference: accumulated";
                    labelCorrectedReferenceSpectrumStatus.Text = "Averaged static corrected spectrum available.";

                    break;

                case Options.ReferenceType.DYNAMIC:

                    dynamicReferenceToolStripMenuItem.Checked = true;
                    toolStripStatusReferenceLabel.Text = "Reference: dynamic (" + 
                        SettingsManager.NumberOfSpectraForDynamicAccumulation.ToString(CultureInfo.InvariantCulture) + ")";
                    labelCorrectedReferenceSpectrumStatus.Text = "Averaged dynamic corrected spectrum will be used.";

                    break;

                default:

                    throw new Exception("Bad value for ReferenceTypeEventArgs.");
            }
        }

        void ChangePlotTypeHandler(object sender, EventArgs e)
        {
            PlotTypeEventArgs te = (PlotTypeEventArgs)e;

            if (SettingsManager.CurrentPlotType != te.Type)
            {
                throw new Exception("Mismatch of type between current settings and event!");
            }

            // Update the menus
            spectrumToolStripMenuItem.Checked = false;
            timeSeriesToolStripMenuItem.Checked = false;
            // ---
            darkSpectrumToolStripMenuItem.Checked = false;
            referenceSpectrumToolStripMenuItem.Checked = false;
            correctedReferenceSpectrumToolStripMenuItem.Checked = false;
            // ---
            accumulatedSpectraToolStripMenuItem.Checked = false;
            accumulatedTimeSeriesToolStripMenuItem.Checked = false;

            // Reset the plotter axis
            this.mMainPlotter.YAxisResetBothBoundsAndAllowedBounds();
            this.mMainPlotter.XAxisResetBothBoundsAndAllowedBounds();

            switch (te.Type)
            {
                case Options.PlotType.OUTPUT:

                    // Enable the corresponding menu
                    spectrumToolStripMenuItem.Checked = true;

                    // Hide all Series in the graph
                    this.mainChart.HideAllSeries();

                    // Disable the accumulate spectra
                    trackBarAccumulateSpectraSlider.Enabled = false;
                    labelAccumulateSpectraSlider.Visible = false;

                    // Update the plot type
                    labelPlotType.Text = "[Live] Spectrum";

                    // Plot
                    mMainPlotter.Plot();

                    break;

                case Options.PlotType.TIMESERIES:

                    // Enable the corresponding menu
                    timeSeriesToolStripMenuItem.Checked = true;

                    // Hide all Series in the graph
                    this.mainChart.HideAllSeries();

                    // Disable the accumulate spectra
                    trackBarAccumulateSpectraSlider.Enabled = false;
                    labelAccumulateSpectraSlider.Visible = false;

                    // Plot
                    mMainPlotter.Plot();

                    // Update the plot type
                    labelPlotType.Text = "[Live] Time series";

                    break;

                case Options.PlotType.DARK_SPECTRUM:

                    // Enable the corresponding menu
                    darkSpectrumToolStripMenuItem.Checked = true;

                    // Hide all Series in the graph
                    this.mainChart.HideAllSeries();

                    // Disable the accumulate spectra
                    trackBarAccumulateSpectraSlider.Enabled = false;
                    labelAccumulateSpectraSlider.Visible = false;

                    // Plot
                    mMainPlotter.Plot();

                    // Update the plot type
                    labelPlotType.Text = "Dark spectrum";

                    break;

                case Options.PlotType.REFERENCE_SPECTRUM:

                    // Enable the corresponding menu
                    referenceSpectrumToolStripMenuItem.Checked = true;

                    // Hide all Series in the graph
                    this.mainChart.HideAllSeries();

                    // Disable the accumulate spectra
                    trackBarAccumulateSpectraSlider.Enabled = false;
                    labelAccumulateSpectraSlider.Visible = false;

                    // Plot
                    mMainPlotter.Plot();

                    // Update the plot type
                    labelPlotType.Text = "Reference spectrum";

                    break;

                case Options.PlotType.CORRECTED_REFERENCE_SPECTRUM:

                    // Enable the corresponding menu
                    correctedReferenceSpectrumToolStripMenuItem.Checked = true;

                    // Hide all Series in the graph
                    this.mainChart.HideAllSeries();

                    // Disable the accumulate spectra
                    trackBarAccumulateSpectraSlider.Enabled = false;
                    labelAccumulateSpectraSlider.Visible = false;

                    // Plot
                    mMainPlotter.Plot();

                    // Update the plot type
                    labelPlotType.Text = "Dark-corrected reference spectrum";

                    break;

                case Options.PlotType.ACCUMULATING_SPECTRUM:

                    // Hide all Series in the graph
                    this.mainChart.HideAllSeries();

                    // Disable the accumulate spectra
                    trackBarAccumulateSpectraSlider.Enabled = false;
                    labelAccumulateSpectraSlider.Visible = false;

                    // Plot
                    mMainPlotter.Plot();

                    // Update the plot type
                    labelPlotType.Text = "Spectrum (accumulating)";

                    break;

                case Options.PlotType.ACCUMULATED_SPECTRA:

                    // Enable the corresponding menu
                    accumulatedSpectraToolStripMenuItem.Checked = true;

                    // Hide all Series in the graph
                    this.mainChart.HideAllSeries();

                    // Set the min and max values
                    if (SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation == null ||
                        SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count == 0)
                    {
                        trackBarAccumulateSpectraSlider.Minimum = 0;
                        trackBarAccumulateSpectraSlider.Maximum = 0;
                    }
                    else
                    {
                        trackBarAccumulateSpectraSlider.Minimum = 0;
                        trackBarAccumulateSpectraSlider.Maximum = SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count - 1;
                    }
                    labelAccumulateSpectraSlider.Text = trackBarAccumulateSpectraSlider.Value.ToString(CultureInfo.InvariantCulture);
                    trackBarAccumulateSpectraSlider.Visible = true;
                    labelAccumulateSpectraSlider.Visible = true;

                    // Enable the accumulate spectra
                    trackBarAccumulateSpectraSlider.Enabled = true;
                    labelAccumulateSpectraSlider.Visible = true;

                    // Plot
                    mMainPlotter.Plot();

                    // Update the plot type
                    labelPlotType.Text = "Acccumulated spectra";

                    break;

                case Options.PlotType.ACCUMULATING_TIMESERIES:

                    // There is no corresponding menu; this plot type
                    // is managed internally by the application and cannot
                    // be chosen by the user.

                    // All plotting options are taken care of.

                    // Update the plot type
                    labelPlotType.Text = "Acccumulating time series";

                    break;

                case Options.PlotType.ACCUMULATED_TIMESERIES:

                    // Enable the corresponding menu
                    accumulatedTimeSeriesToolStripMenuItem.Checked = true;

                    /// Hide all Series in the graph
                    this.mainChart.HideAllSeries();

                    // Disable the accumulate spectra
                    trackBarAccumulateSpectraSlider.Enabled = false;
                    labelAccumulateSpectraSlider.Visible = false;

                    // Plot
                    mMainPlotter.Plot();

                    // Update the plot type
                    labelPlotType.Text = "Accumulated time series";

                    break;

                default:

                    break;
            }
        }

        void ChangeResultSpectrumTypeHandler(object sender, EventArgs e)
        {
            ResultSpectrumTypeEventArgs te = (ResultSpectrumTypeEventArgs)e;

            // Update the menus and the status bar
            rawSpectrumToolStripMenuItem.Checked = false;
            darkCorrectedToolStripMenuItem.Checked = false;
            absorbanceToolStripMenuItem.Checked = false;
            transmissionToolStripMenuItem.Checked = false;

            switch (te.Type)
            {
                case Options.ResultSpectrumType.RAW_SPECTRUM:

                    rawSpectrumToolStripMenuItem.Checked = true;
                    toolStripStatusOutput.Text = "Output: Raw spectrum";
                    comboBoxAcquisitionOutput.SelectedIndex = 0;
                    break;

                case Options.ResultSpectrumType.DARK_CORRECTED:

                    darkCorrectedToolStripMenuItem.Checked = true;
                    toolStripStatusOutput.Text = "Output: Dark corrected";
                    comboBoxAcquisitionOutput.SelectedIndex = 1;
                    break;

                case Options.ResultSpectrumType.ABSORBANCE:

                    absorbanceToolStripMenuItem.Checked = true;
                    toolStripStatusOutput.Text = "Output: Absorbance";
                    comboBoxAcquisitionOutput.SelectedIndex = 2;
                    break;

                case Options.ResultSpectrumType.TRANSMISSION:

                    transmissionToolStripMenuItem.Checked = true;
                    toolStripStatusOutput.Text = "Output: Transmission";
                    comboBoxAcquisitionOutput.SelectedIndex = 3;
                    break;

                default:

                    throw new Exception("Bad value for ResultSpectrumTypeEventArgs!");
            }

            acquisitionParametersControl.UpdateSaveFilename();
        }

        void ChangeSpectrumThresholdingWavelengthsHandler(object sender, EventArgs e)
        {
            SpectrumThresholdingWavelengthsEventArgs te = (SpectrumThresholdingWavelengthsEventArgs)e;

            // TODO: Implement me!

        }

        void ChangeNumberOfSpectraForDynamicAccumulationEventHandler(object sender, EventArgs e)
        {
            // If the reference type is already DYNAMIC, we just need to update the 
            // number of spectra that will be used.
            if (SettingsManager.ReferenceType == Options.ReferenceType.DYNAMIC)
            {
                dynamicReferenceToolStripMenuItem.Checked = true;
                toolStripStatusReferenceLabel.Text = "Reference: dynamic (" +
                    SettingsManager.NumberOfSpectraForDynamicAccumulation.ToString(CultureInfo.InvariantCulture) + ")";
                labelCorrectedReferenceSpectrumStatus.Text = "Averaged dynamic corrected spectrum will be used.";
            }
        }

        void ToggleYAxisAutoScaleHandler(object sender, EventArgs e)
        {
            SingleBooleanEventArgs se = (SingleBooleanEventArgs)e;
            autoScaleYAxisToolStripMenuItem.Checked = se.Enabled;
        }

        #endregion event_handlers
    }
}
