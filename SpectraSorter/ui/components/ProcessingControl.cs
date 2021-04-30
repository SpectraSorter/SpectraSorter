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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using spectra.state;
using spectra.processing;
using System.Globalization;

namespace spectra.ui.components
{
    public partial class ProcessingControl : UserControl
    {
        public ProcessingControl()
        {
            InitializeComponent();

            this.Refresh();
        }

        override public void Refresh()
        {
            // Make sure the combobox is not editable
            comboBoxFilteringKernelType.DropDownStyle = ComboBoxStyle.DropDownList;

            // Set the filtering values
            UpdateFilteringUIElementsFromSettings();
        }

        private void buttonStartWavelengthHub_Click(object sender, EventArgs e)
        {
            WavelengthHub.Instance.Show();
            WavelengthHub.Instance.BringToFront();
        }

        // Update the UI elements related to Filtering
        private void UpdateFilteringUIElementsFromSettings()
        {
            this.checkBoxFilteringEnable.Checked = SettingsManager.SpectrumFilteringEnabled;

            int support = SettingsManager.SpectrumFilteringSupport;
            if (SettingsManager.SpectrumFilteringAverage)
            {
                SpectrumFilterer.Instance.UseAverageFilter(support);
            }
            else
            {
                SpectrumFilterer.Instance.UseGaussianFilter(support);
            }

            if (SpectrumFilterer.Instance.IsAverage)
            {
                comboBoxFilteringKernelType.SelectedIndex = 0;
            }
            else
            {
                comboBoxFilteringKernelType.SelectedIndex = 1;
            }
            if (SettingsManager.SpectrumFilteringAverage)
            {
                textBoxSpectrumFilterWidth.Text = SpectrumFilterer.Instance.Support.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                textBoxSpectrumFilterWidth.Text = SpectrumFilterer.Instance.Sigma.ToString(CultureInfo.InvariantCulture);
            }

            // Initialize filtering
            SpectrumFilterer.Instance.InitializeFilterFromCurrentSettings();

            this.UpdateFilterSupportText();

            this.DisplayFilterParameters();

            // Register event handlers
            RegisterEventHandlers();

            // Enable/disable UI elements
            ToggleUIElements(SettingsManager.SpectrumFilteringEnabled);
        }

        private void UpdateFilterSupportText()
        {
            if (comboBoxFilteringKernelType.SelectedIndex == 0)
            {
                labelSpectrumFilterWidth.Text = "Kernel support (width)";
            }
            else
            {
                labelSpectrumFilterWidth.Text = "Sigma";
            }
        }

        private void DisplayFilterParameters()
        {
            if (comboBoxFilteringKernelType.SelectedIndex == 0)
            {
                labelFilteringSummary.Text = "Kernel = Average(width = " +
                    SpectrumFilterer.Instance.Support.ToString(CultureInfo.InvariantCulture) + 
                    "; σ = 0.0)";
            }
            else
            {
                labelFilteringSummary.Text = "Kernel = Gaussian(width = " +
                    SpectrumFilterer.Instance.Support.ToString(CultureInfo.InvariantCulture) +
                    "; σ = " + SpectrumFilterer.Instance.Sigma.ToString("0.###", CultureInfo.InvariantCulture) +
                    ")";
            }
        }

        private void comboBoxFilteringKernelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update the settings
            if (comboBoxFilteringKernelType.SelectedIndex == 0)
            {
                SettingsManager.SpectrumFilteringAverage = true;
                SettingsManager.SpectrumFilteringGaussian = false;
            }
            else
            {
                SettingsManager.SpectrumFilteringAverage = false;
                SettingsManager.SpectrumFilteringGaussian = true;
            }

            // Update the sigma/support field
            if (SettingsManager.SpectrumFilteringAverage)
            {
                textBoxSpectrumFilterWidth.Text = SpectrumFilterer.Instance.Support.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                textBoxSpectrumFilterWidth.Text = SpectrumFilterer.Instance.Sigma.ToString(CultureInfo.InvariantCulture);
            }

            // Update the text
            this.UpdateFilterSupportText();

            // Update the filter
            SpectrumFilterer.Instance.InitializeFilterFromCurrentSettings();
            this.DisplayFilterParameters();
        }

        private void textBoxSpectrumFilterWidth_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxSpectrumFilterWidth.Text.Length == 0)
            {
                textBoxSpectrumFilterWidth.BackColor = Color.White;
                e.Cancel = false;

                return;
            }

            int maxSupport = 4271;
            if (SpectrumProcessor.Instance.Wavelengths != null && SpectrumProcessor.Instance.Wavelengths.Length > 0)
            {
                maxSupport = SpectrumProcessor.Instance.Wavelengths.Length * 2 - 1;
            }

            // Get the value from textBoxSpectrumFilterWidth
            if (Double.TryParse(textBoxSpectrumFilterWidth.Text, out double value))
            {
                if (SettingsManager.SpectrumFilteringAverage)
                {
                    // Hard-code max kernel support for known signal length,
                    if (value < 1.0 || value > maxSupport)
                    {
                        textBoxSpectrumFilterWidth.BackColor = Color.Red;
                        e.Cancel = true;
                        return;
                    }

                    textBoxSpectrumFilterWidth.BackColor = Color.White;
                    e.Cancel = false;
                }
                else
                {
                    if (value < 1.0)
                    {
                        textBoxSpectrumFilterWidth.BackColor = Color.Red;
                        e.Cancel = true;
                        return;
                    }

                    int support = SpectrumFilterer.Instance.SupportFromSigma(value);
                    
                    // Hard-code max kernel support for known signal length,
                    if (support < 1 || support > maxSupport)
                    {
                        textBoxSpectrumFilterWidth.BackColor = Color.Red;
                        e.Cancel = true;
                        return;
                    }

                    textBoxSpectrumFilterWidth.BackColor = Color.White;
                    e.Cancel = false;
                }
            }
            else
            {
                textBoxSpectrumFilterWidth.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxSpectrumFilterWidth_Validated(object sender, EventArgs e)
        {
            // Get the value from textBoxSpectrumFilterWidth
            if (Double.TryParse(textBoxSpectrumFilterWidth.Text, out double value))
            {
                if (SettingsManager.SpectrumFilteringAverage)
                {
                    SpectrumFilterer.Instance.UseAverageFilter((int)value);
                    textBoxSpectrumFilterWidth.Text = "" + SpectrumFilterer.Instance.Support;
                }
                else
                {
                    SpectrumFilterer.Instance.UseGaussianFilter(value);
                    textBoxSpectrumFilterWidth.Text = "" + SpectrumFilterer.Instance.Sigma;
                }

                // Store the settings
                SettingsManager.SpectrumFilteringSupport = SpectrumFilterer.Instance.Support;

                // Reset the filter
                SpectrumFilterer.Instance.InitializeFilterFromCurrentSettings();

                // Update the UI
                this.DisplayFilterParameters();
            }
        }

        private void textBoxSpectrumFilterWidth_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void checkBoxFilteringEnable_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.SpectrumFilteringEnabled = checkBoxFilteringEnable.Checked;
        }
    }
}
