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

using spectra.plotting;
using spectra.state;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static spectra.state.SettingsManager;
using static spectra.ui.MainWindow;

namespace spectra.ui.components
{
    public partial class PlotOptionsControl
    {
        #region event_handlers

        private void RegisterEventHandlers()
        {
            MainWindow.ToggleYAxisAutoScale += ToggleYAxisAutoScaleHandler;
            MainWindow.AcquisitionStarted += ToggleElementsOnAcquisitionStarted;
            MainWindow.AcquisitionCompleted += ToggleElementsOnAcquisitionCompleted;
            SettingsManager.ChangePlotType += ChangePlotTypeHandler;
            SettingsManager.TogglePlotThresholds += TogglePlotThresholdsCheckbox;
            SettingsManager.TogglePlotTriggerPoints += TogglePlotTriggerPointsCheckbox;
        }

        // Event handlers
        void ToggleYAxisAutoScaleHandler(object sender, EventArgs e)
        {
            SingleBooleanEventArgs te = (SingleBooleanEventArgs)e;

            checkBoxYAxisAutoScale.Checked = te.Enabled;

            this.UpdateUIElementsOnYAxisAutoScaleChange(te.Enabled);

        }

        void ToggleElementsOnAcquisitionStarted(object sender, EventArgs e)
        {
            textBoxNumberOfTimePointsToBuffer.Enabled = false;
        }

        void ToggleElementsOnAcquisitionCompleted(object sender, EventArgs e)
        {
            textBoxNumberOfTimePointsToBuffer.Enabled = true;
        }

        void TogglePlotThresholdsCheckbox(object sender, EventArgs e)
        {
            SingleBooleanEventArgs te = (SingleBooleanEventArgs)e;
            checkBoxShowThresholds.Checked = te.Enabled;
        }
        void TogglePlotTriggerPointsCheckbox(object sender, EventArgs e)
        {
            SingleBooleanEventArgs te = (SingleBooleanEventArgs)e;
            checkBoxShowTriggerPoints.Checked = te.Enabled;
        }

        void ChangePlotTypeHandler(object sender, EventArgs e)
        {
            PlotTypeEventArgs te = (PlotTypeEventArgs)e;

            if (SettingsManager.CurrentPlotType != te.Type)
            {
                throw new Exception("Mismatch of type between current settings and event!");
            }

            // Update the combo box (only with the user-selectable entries)
            if ((int)SettingsManager.CurrentPlotType < 7)
            {
                comboBoxPlotType.SelectedIndex = (int)SettingsManager.CurrentPlotType;
            }

            // Reset the plotter axis
            MainWindow.mainPlotter.YAxisResetBothBoundsAndAllowedBounds();
            MainWindow.mainPlotter.XAxisResetBothBoundsAndAllowedBounds();

            switch (te.Type)
            {
                case Options.PlotType.OUTPUT:

                    // Enable the X axis limits
                    groupBoxXAxis.Enabled = true;

                    // Disable the time series options 
                    groupBoxTimeSeriesOptions.Enabled = false;

                    // Disable the Accumulated Spectra options groupbox
                    groupBoxAccumulatedSpectraOptions.Enabled = false;

                    break;

                case Options.PlotType.TIMESERIES:

                    // Disable the X axis limits
                    groupBoxXAxis.Enabled = false;

                    // Enable the time series options 
                    groupBoxTimeSeriesOptions.Enabled = true;

                    // Disable the Accumulated Spectra options groupbox
                    groupBoxAccumulatedSpectraOptions.Enabled = false;

                    break;

                case Options.PlotType.DARK_SPECTRUM:

                    // Enable the X axis limits
                    groupBoxXAxis.Enabled = true;

                    // Disable the time series options 
                    groupBoxTimeSeriesOptions.Enabled = false;

                    // Disable the Accumulated Spectra options groupbox
                    groupBoxAccumulatedSpectraOptions.Enabled = false;

                    break;

                case Options.PlotType.REFERENCE_SPECTRUM:

                    // Enable the X axis limits
                    groupBoxXAxis.Enabled = true;

                    // Disable the time series options 
                    groupBoxTimeSeriesOptions.Enabled = false;

                    // Disable the Accumulated Spectra options groupbox
                    groupBoxAccumulatedSpectraOptions.Enabled = false;

                    break;

                case Options.PlotType.CORRECTED_REFERENCE_SPECTRUM:

                    // Enable the X axis limits
                    groupBoxXAxis.Enabled = true;

                    // Disable the time series options 
                    groupBoxTimeSeriesOptions.Enabled = false;

                    break;

                case Options.PlotType.ACCUMULATING_SPECTRUM:

                    // Enable the X axis limits
                    groupBoxXAxis.Enabled = true;

                    // Disable the time series options 
                    groupBoxTimeSeriesOptions.Enabled = false;

                    // Disable the Accumulated Spectra options groupbox
                    groupBoxAccumulatedSpectraOptions.Enabled = false;

                    break;

                case Options.PlotType.ACCUMULATED_SPECTRA:

                    // Enable the X axis limits
                    groupBoxXAxis.Enabled = true;

                    // Disable the time series options 
                    groupBoxTimeSeriesOptions.Enabled = false;

                    // Enable the Accumulated Spectra options groupbox
                    groupBoxAccumulatedSpectraOptions.Enabled = true;
                    
                    break;

                case Options.PlotType.ACCUMULATING_TIMESERIES:

                    // Enable the X axis limits
                    groupBoxXAxis.Enabled = true;

                    // Disable the time series options 
                    groupBoxTimeSeriesOptions.Enabled = false;

                    // Disable the Accumulated Spectra options groupbox
                    groupBoxAccumulatedSpectraOptions.Enabled = false;

                    break;

                case Options.PlotType.ACCUMULATED_TIMESERIES:

                    // Enable the X axis limits
                    groupBoxXAxis.Enabled = true;

                    // Disable the time series options 
                    groupBoxTimeSeriesOptions.Enabled = false;

                    // Disable the Accumulated Spectra options groupbox
                    groupBoxAccumulatedSpectraOptions.Enabled = false;

                    break;

                default:

                    break;
            }
        }

        #endregion event_handlers
    }
}
