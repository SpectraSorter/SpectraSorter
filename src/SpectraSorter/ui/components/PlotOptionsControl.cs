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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using static spectra.state.SettingsManager;
using spectra.state;
using spectra.processing;
using System.Globalization;

namespace spectra.ui.components
{
    public partial class PlotOptionsControl : UserControl
    {
        public PlotOptionsControl()
        {
            InitializeComponent();

            SetUp();

            RegisterEventHandlers();
        }

        public void SetUp()
        {
            comboBoxPlotType.SelectedIndex = (int)SettingsManager.CurrentPlotType;
            textBoxNumberOfTimePointsToBuffer.Text = SettingsManager.NumberOfTimePointsToStore.ToString(CultureInfo.InvariantCulture);
            textBoxNumberOfTriggerPointsToBuffer.Text = SettingsManager.NumberOfTriggerPoints.ToString(CultureInfo.InvariantCulture);
            textBoxNumberOfTimePointsToPlot.Text = SettingsManager.NumberOfTimePointsToPlot.ToString(CultureInfo.InvariantCulture);
            checkBoxShowThresholds.Checked = SettingsManager.PlotThresholds;
            checkBoxShowTriggerPoints.Checked = SettingsManager.PlotTriggerPoints;
            trackBarNumberOfPointsToPlot.Minimum = 2; // With less than two points, noting gets plotted
            trackBarNumberOfPointsToPlot.Maximum = (int)SettingsManager.NumberOfTimePointsToStore;
            trackBarNumberOfPointsToPlot.Value = (int)SettingsManager.NumberOfTimePointsToPlot;
        }

        private void UpdateUIElementsOnYAxisAutoScaleChange(bool enabled)
        {
            if (enabled == true)
            {
                textBoxYAxisRangeMin.Enabled = false;
                textBoxYAxisRangeMax.Enabled = false;
                MainWindow.mainPlotter.YAxisResetBothBoundsAndAllowedBounds();
                MainWindow.mainPlotter.YAxisAutoRanges = true;
                buttonYAxisReset.Enabled = false;
            }
            else
            {
                textBoxYAxisRangeMin.Enabled = true;
                textBoxYAxisRangeMax.Enabled = true;
                MainWindow.mainPlotter.YAxisAutoRanges = false;
                MainWindow.mainPlotter.YAxisResetBothBoundsAndAllowedBounds();
                textBoxYAxisRangeMin.Text = "";
                textBoxYAxisRangeMax.Text = "";
                buttonYAxisReset.Enabled = true;
            }

            // Update
            MainWindow.mainPlotter.Plot();
        }

        private void PlotOptionsControl_Load(object sender, EventArgs e)
        {
        }

        private void checkBoxYAxisAutoScale_CheckedChanged(object sender, EventArgs e)
        {
            // Raise the event
            PlotOptionsControl.OnToggleYAxisAutoScale(null,
                new SingleBooleanEventArgs
                {
                    Enabled = this.checkBoxYAxisAutoScale.Checked
                });

            UpdateUIElementsOnYAxisAutoScaleChange(checkBoxYAxisAutoScale.Checked);
        }

        private void textBoxYAxisRangeMin_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxYAxisRangeMin.Text.Length == 0)
            {
                textBoxYAxisRangeMin.BackColor = Color.White;
                e.Cancel = false;

                return;
            }

            if (Double.TryParse(textBoxYAxisRangeMin.Text, out double value))
            {
                // Is the range acceptable?
                (_, _, _, double maxY) = MainWindow.mainPlotter.GetCurrentDataRange();

                if (value > maxY)
                {
                    MainWindow.mainPlotter.YAxisResetBothBoundsAndAllowedBounds();
                    textBoxYAxisRangeMin.BackColor = Color.Red;
                    e.Cancel = true;
                    return;
                }

                textBoxYAxisRangeMin.BackColor = Color.White;
                e.Cancel = false;
            }
            else
            {
                textBoxYAxisRangeMin.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxYAxisRangeMin_Validated(object sender, EventArgs e)
        {
            // Set the value
            MainWindow.mainPlotter.SetYAxisLowerBoundFromString(textBoxYAxisRangeMin.Text);

            // Update both text fields
            textBoxYAxisRangeMin.Text = MainWindow.mainPlotter.GetYAxisLowerBoundAsString();
        }

        private void textBoxYAxisRangeMax_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxYAxisRangeMax.Text.Length == 0)
            {
                textBoxYAxisRangeMax.BackColor = Color.White;
                e.Cancel = false;

                return;
            }

            if (Double.TryParse(textBoxYAxisRangeMax.Text, out double value))
            {
                // Is the range acceptable?
                (_, _, double minY, _) = MainWindow.mainPlotter.GetCurrentDataRange();

                if (value < minY)
                {
                    MainWindow.mainPlotter.YAxisResetBothBoundsAndAllowedBounds();
                    textBoxYAxisRangeMax.BackColor = Color.Red;
                    e.Cancel = true;
                    return;
                }
                textBoxYAxisRangeMax.BackColor = Color.White;
                e.Cancel = false;
            }
            else
            {
                textBoxYAxisRangeMax.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxYAxisRangeMax_Validated(object sender, EventArgs e)
        {
            // Set the value
            MainWindow.mainPlotter.SetYAxisUpperBoundFromString(textBoxYAxisRangeMax.Text);

            // Update both text fields
            textBoxYAxisRangeMax.Text = MainWindow.mainPlotter.GetYAxisUpperBoundAsString();
        }

        private void textBoxXAxisRangeMin_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxXAxisRangeMin.Text.Length == 0)
            {
                textBoxXAxisRangeMin.BackColor = Color.White;
                e.Cancel = false;

                return;
            }

            if (Double.TryParse(textBoxXAxisRangeMin.Text, out double value))
            {
                // Is the range acceptable?
                (_, double maxX, _, _) = MainWindow.mainPlotter.GetCurrentDataRange();

                if (value > maxX)
                {
                    MainWindow.mainPlotter.XAxisResetBothBoundsAndAllowedBounds();
                    textBoxXAxisRangeMin.BackColor = Color.Red;
                    e.Cancel = true;
                    return;
                }

                textBoxXAxisRangeMin.BackColor = Color.White;
                e.Cancel = false;
            }
            else
            {
                textBoxXAxisRangeMin.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxXAxisRangeMin_Validated(object sender, EventArgs e)
        {
            // Set the value
            MainWindow.mainPlotter.SetXAxisLowerBoundFromString(textBoxXAxisRangeMin.Text);

            // Update both text fields
            textBoxXAxisRangeMin.Text = MainWindow.mainPlotter.GetXAxisLowerBoundAsString();
        }

        private void textBoxXAxisRangeMax_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxXAxisRangeMax.Text.Length == 0)
            {
                textBoxXAxisRangeMax.BackColor = Color.White;
                e.Cancel = false;

                return;
            }

            if (Double.TryParse(textBoxXAxisRangeMax.Text, out double value))
            {
                // Is the range acceptable?
                (double minX, _, _, _) = MainWindow.mainPlotter.GetCurrentDataRange();

                if (value < minX)
                {
                    MainWindow.mainPlotter.XAxisResetBothBoundsAndAllowedBounds();
                    textBoxXAxisRangeMax.BackColor = Color.Red;
                    e.Cancel = true;
                    return;
                }

                textBoxXAxisRangeMax.BackColor = Color.White;
                e.Cancel = false;
            }
            else
            {
                textBoxXAxisRangeMax.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxXAxisRangeMax_Validated(object sender, EventArgs e)
        {
            MainWindow.mainPlotter.SetXAxisUpperBoundFromString(textBoxXAxisRangeMax.Text);

            // Update both text fields
            textBoxXAxisRangeMax.Text = MainWindow.mainPlotter.GetXAxisUpperBoundAsString();
        }

        private void textBoxNumberOfTimePointsToBuffer_Validating(object sender, CancelEventArgs e)
        {
            if (UInt32.TryParse(textBoxNumberOfTimePointsToBuffer.Text, out uint value))
            {
                // We need at least two samples otherwise the time series plotting won't work
                if (value < 2)
                {
                    textBoxNumberOfTimePointsToBuffer.BackColor = Color.Red;
                    e.Cancel = true;
                }
                else
                {
                    textBoxNumberOfTimePointsToBuffer.BackColor = Color.White;
                    e.Cancel = false;

                }
            }
            else
            {
                textBoxNumberOfTimePointsToBuffer.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxNumberOfTimePointsToBuffer_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxNumberOfTimePointsToBuffer.Text, out uint value))
            {
                if (SettingsManager.NumberOfTimePointsToStore != value)
                {
                    // Update the number of time points to store
                    SettingsManager.NumberOfTimePointsToStore = value;

                    // If needed, also correct the number of time points to plot
                    if (SettingsManager.NumberOfTimePointsToPlot > SettingsManager.NumberOfTimePointsToStore)
                    {
                        // Set the number of points to plot to be equal to the number of points to store
                        SettingsManager.NumberOfTimePointsToPlot = SettingsManager.NumberOfTimePointsToStore;

                        // Update corresponding UI elements
                        textBoxNumberOfTimePointsToPlot.Text = "" + SettingsManager.NumberOfTimePointsToPlot;
                    }

                    // The maximum number of time points to plot always corresponds to the number
                    // of points to store.
                    trackBarNumberOfPointsToPlot.Maximum = (int)SettingsManager.NumberOfTimePointsToStore;

                    // Reset all buffers
                    SpectrumProcessor.Instance.ResetAllBuffers();
                }
            }
        }

        private void textBoxNumberOfTimePointsToBuffer_TextChanged(object sender, EventArgs e)
        {
            this.Validate();
        }

        private void textBoxNumberOfTriggerPointsToBuffer_TextChanged(object sender, EventArgs e)
        {
            this.Validate();
        }

        private void textBoxNumberOfTriggerPointsToBuffer_Validating(object sender, CancelEventArgs e)
        {
            if (UInt32.TryParse(textBoxNumberOfTriggerPointsToBuffer.Text, out uint value))
            {
                textBoxNumberOfTriggerPointsToBuffer.BackColor = Color.White;
                e.Cancel = false;
            }
            else
            {
                textBoxNumberOfTriggerPointsToBuffer.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxNumberOfTriggerPointsToBuffer_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxNumberOfTriggerPointsToBuffer.Text, out uint value))
            {
                SettingsManager.NumberOfTriggerPoints = value;
            }
        }

        private void textBoxNumberOfTimePointsToPlot_TextChanged(object sender, EventArgs e)
        {
            this.Validate();
        }

        private void textBoxNumberOfTimePointsToPlot_Validating(object sender, CancelEventArgs e)
        {
            if (UInt32.TryParse(textBoxNumberOfTimePointsToPlot.Text, out uint value))
            {
                // The number of time points to plot cannot be larger than the number
                // of time points to store. Also, no line will be drawn with less than
                // two points.
                if (value >= 2 && value <= SettingsManager.NumberOfTimePointsToStore)
                {
                    textBoxNumberOfTimePointsToPlot.BackColor = Color.White;
                    e.Cancel = false;
                }
                else
                {
                    textBoxNumberOfTimePointsToPlot.BackColor = Color.Red;
                    e.Cancel = true;
                }
            }
            else
            {
                textBoxNumberOfTimePointsToPlot.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxNumberOfTimePointsToPlot_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxNumberOfTimePointsToPlot.Text, out uint value))
            {
                SettingsManager.NumberOfTimePointsToPlot = value;
                trackBarNumberOfPointsToPlot.Value = (int)value;
            }
        }

        private void checkBoxShowThresholds_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.PlotThresholds = checkBoxShowThresholds.Checked;
        }

        private void checkBoxShowTriggerPoints_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.PlotTriggerPoints = checkBoxShowTriggerPoints.Checked;
        }

        private void trackBarNumberOfPointsToPlot_Scroll(object sender, EventArgs e)
        {
            // Set the value to textBoxNumberOfTimePointsToPlot.Text and trigger validation
            textBoxNumberOfTimePointsToPlot.Text = trackBarNumberOfPointsToPlot.Value.ToString(CultureInfo.InvariantCulture);
            this.ValidateChildren();
        }

        private void comboBoxPlotType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If an accumulation is running, do not allow to change to ACCUMULATED_SPECTRA

            if (State.Instance.IsPerformingAccumulationAcquisition == true &&
                (Options.PlotType)comboBoxPlotType.SelectedIndex == Options.PlotType.ACCUMULATED_SPECTRA)
            {
                SettingsManager.CurrentPlotType = Options.PlotType.ACCUMULATING_SPECTRUM;
                return;
            }

            // Update the plot type in the settings
            SettingsManager.CurrentPlotType = (Options.PlotType)comboBoxPlotType.SelectedIndex;

            if (SettingsManager.CurrentPlotType == Options.PlotType.ACCUMULATED_SPECTRA)
            {
                // Do we have some spectra to plot?
                if (SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation != null &&
                    SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count > 0)
                {
                    trackBarAccumulateSpectraSlider.Minimum = 0;
                    trackBarAccumulateSpectraSlider.Maximum = SpectrumProcessor.Instance.StaticAccumulatedSpectraForReferenceEstimation.Count - 1;

                    int value = SpectrumProcessor.Instance.SelectedStaticAccumulatedSpectrumNumber;
                    if (value >= 0 && value <= trackBarAccumulateSpectraSlider.Maximum)
                    {
                        trackBarAccumulateSpectraSlider.Value = value;
                    }
                    else
                    {
                        trackBarAccumulateSpectraSlider.Value = 0;
                    }

                    labelAccumulateSpectraSlider.Text = (trackBarAccumulateSpectraSlider.Value).ToString(CultureInfo.InvariantCulture);

                    SpectrumProcessor.Instance.SelectedStaticAccumulatedSpectrumNumber = trackBarAccumulateSpectraSlider.Value;

                    groupBoxAccumulatedSpectraOptions.Enabled = true;

                    // Make sure the correct plot type is drawn
                    if (SettingsManager.CurrentPlotType != Options.PlotType.ACCUMULATED_SPECTRA)
                    {
                        SettingsManager.CurrentPlotType = Options.PlotType.ACCUMULATED_SPECTRA;
                    }
                    MainWindow.mainPlotter.Plot();
                }
                else
                {
                    groupBoxAccumulatedSpectraOptions.Enabled = false;
                }
            }
        }

        private void trackBarAccumulateSpectraSlider_ValueChanged(object sender, EventArgs e)
        {
            labelAccumulateSpectraSlider.Text = (trackBarAccumulateSpectraSlider.Value).ToString(CultureInfo.InvariantCulture);

            SpectrumProcessor.Instance.SelectedStaticAccumulatedSpectrumNumber = trackBarAccumulateSpectraSlider.Value;

            // Make sure the correct plot type is drawn
            if (SettingsManager.CurrentPlotType != Options.PlotType.ACCUMULATED_SPECTRA)
            {
                SettingsManager.CurrentPlotType = Options.PlotType.ACCUMULATED_SPECTRA;
            }
            MainWindow.mainPlotter.Plot();
        }

        private void buttonXAxisReset_Click(object sender, EventArgs e)
        {
            MainWindow.mainPlotter.XAxisResetBothBoundsAndAllowedBounds();
            textBoxXAxisRangeMin.Text = "";
            textBoxXAxisRangeMax.Text = "";
            MainWindow.mainPlotter.Plot();
        }

        private void buttonYAxisReset_Click(object sender, EventArgs e)
        {
            MainWindow.mainPlotter.YAxisResetBothBoundsAndAllowedBounds();
            textBoxYAxisRangeMin.Text = "";
            textBoxYAxisRangeMax.Text = "";
            MainWindow.mainPlotter.Plot();
        }
    }
}
