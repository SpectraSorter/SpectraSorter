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
using spectra.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace spectra.ui
{
    public partial class WavelengthRangeOptions : Form
    {
        #region members

        /// <summary>
        /// Reference to the singleton SavingParameters instance.
        /// </summary>
        private static WavelengthRangeOptions mInstance = null;

        private double mSpectrumWavelengthResolution = -1.0;

        #endregion members

        #region methods

        #region private

        private WavelengthRangeOptions()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            InitializeComponent();

            this.Refresh();
        }

        #endregion private

        #region public

         public void UpdateFileSaveUnits()
        {
            // Total range (info)
            int nW = SpectrumProcessor.Instance.Wavelengths.Length;
            if (nW > 0)
            {
                nW--;
                labelTotalPixelRange.Text = "Complete spectrum ranges from " +
                    SpectrumProcessor.Instance.Wavelengths[0].ToString("F1", CultureInfo.InvariantCulture) + 
                    " to " +
                    SpectrumProcessor.Instance.Wavelengths[nW].ToString("F1", CultureInfo.InvariantCulture) +
                    " nm (indices 0 to " +
                    nW.ToString(CultureInfo.InvariantCulture) +
                    ").";
            }
            else
            {
                labelTotalPixelRange.Text = "Spectrum information not yet available.";
            }

            if (SettingsManager.SaveRangeUnits == Options.RangeUnits.Pixels)
            {
                textBoxSaveStartRange.Text = SettingsManager.SaveStartPixel.ToString(CultureInfo.InvariantCulture);
                textBoxSaveEndRange.Text = SettingsManager.SaveEndPixel.ToString(CultureInfo.InvariantCulture);
                labelConvertedStartRange.Text = SettingsManager.SaveStartWavelength.ToString("F1", CultureInfo.InvariantCulture);
                labelConvertedEndRange.Text = SettingsManager.SaveEndWavelength.ToString("F1", CultureInfo.InvariantCulture);
                labelUnit.Text = "pixels";
                labelConvertedUnit.Text = "nm";
                if (SettingsManager.SaveStepPixel == 1)
                {
                    labelWavelengthSavingStepExplanation.Text = "pixel";
                }
                else
                {
                    labelWavelengthSavingStepExplanation.Text = "pixels";
                }
            }

            if (SettingsManager.SaveRangeUnits == Options.RangeUnits.Wavelengths)
            {
                textBoxSaveStartRange.Text = SettingsManager.SaveStartWavelength.ToString("F1", CultureInfo.InvariantCulture);
                textBoxSaveEndRange.Text = SettingsManager.SaveEndWavelength.ToString("F1", CultureInfo.InvariantCulture);
                labelConvertedStartRange.Text = SettingsManager.SaveStartPixel.ToString("F0", CultureInfo.InvariantCulture);
                labelConvertedEndRange.Text = SettingsManager.SaveEndPixel.ToString("F0", CultureInfo.InvariantCulture);
                labelUnit.Text = "nm";
                labelConvertedUnit.Text = "pixels";
                labelWavelengthSavingStepExplanation.Text = "nm";
            }
        }

        private void UpdateSavingStep()
        {
            if (SettingsManager.SaveRangeUnits == Options.RangeUnits.Pixels)
            {
                // Convert the step in nm to step in pixels
                int pixelStep = PixelStepFromWavelength(Double.Parse(this.textBoxWavelengthSavingStep.Text, CultureInfo.InvariantCulture));

                // Update the text
                this.textBoxWavelengthSavingStep.Text = pixelStep.ToString("F0", CultureInfo.InvariantCulture);

            }
            else
            {
                // Convert the step in pixels to step in nm
                double wavelengthStep = WavelengthStepFromPixel(Int32.Parse(this.textBoxWavelengthSavingStep.Text, CultureInfo.InvariantCulture));

                // Update the text
                this.textBoxWavelengthSavingStep.Text = wavelengthStep.ToString("F3", CultureInfo.InvariantCulture);
            }
        }

        private int PixelStepFromWavelength(double wavelengthStep)
        {
            if (this.mSpectrumWavelengthResolution == -1)
            {
                // Wavelength resolution
                int pixelRange = SettingsManager.SaveEndPixel - SettingsManager.SaveStartPixel;
                double wavelengthRange = SettingsManager.SaveEndWavelength - SettingsManager.SaveStartWavelength;
                this.mSpectrumWavelengthResolution = wavelengthRange / pixelRange;
            }

            // Calculate the step in pixels
            int numPixels = (int)Math.Round(wavelengthStep / this.mSpectrumWavelengthResolution);

            if (numPixels < 1)
            {
                numPixels = 1;
            }

            if (numPixels > SettingsManager.SaveEndPixel)
            {
                numPixels = SettingsManager.SaveEndPixel;
            }

            // Return
            return numPixels;
        }

        private double WavelengthStepFromPixel(int pixelStep)
        {
            if (this.mSpectrumWavelengthResolution == -1)
            {
                // Wavelength resolution
                int pixelRange = SettingsManager.SaveEndPixel - SettingsManager.SaveStartPixel;
                double wavelengthRange = SettingsManager.SaveEndWavelength - SettingsManager.SaveStartWavelength;
                this.mSpectrumWavelengthResolution = wavelengthRange / pixelRange;
            }

            // Calculate the step in nm
            double nm = (double)(pixelStep * this.mSpectrumWavelengthResolution);

            // Return
            return nm;
        }

        public void ToggleEditing(bool enabled)
        {
            groupBoxWavelengthSubset.Enabled = enabled;
        }

        override
        public void Refresh()
        {
            comboBoxSaveRangeUnits.SelectedIndex = (int)SettingsManager.SaveRangeUnits;
            if (SettingsManager.SaveRangeUnits == Options.RangeUnits.Pixels)
            {
                textBoxSaveStartRange.Text = SettingsManager.SaveStartPixel.ToString("F0", CultureInfo.InvariantCulture);
                labelConvertedStartRange.Text = SettingsManager.SaveStartWavelength.ToString("F1", CultureInfo.InvariantCulture);
            }
            else
            {
                textBoxSaveStartRange.Text = SettingsManager.SaveStartWavelength.ToString("F1", CultureInfo.InvariantCulture);
                labelConvertedStartRange.Text = SettingsManager.SaveStartPixel.ToString("F0", CultureInfo.InvariantCulture);
            }

            textBoxWavelengthSavingStep.Text = SettingsManager.SaveStepPixel.ToString("F0", CultureInfo.InvariantCulture);
        }

        #endregion public

        #endregion methods

        #region properties

        /// <summary>
        /// SavingParameters (singleton) instance.
        /// </summary>
        public static WavelengthRangeOptions Instance
        {
            get
            {
                // If the Form has not been created yet,
                // instantiate it now.
                if (mInstance == null)
                {
                    mInstance = new WavelengthRangeOptions();
                }

                // Return a reference
                return mInstance;
            }
        }

        #endregion properties

        private void ComboBoxSaveRangeUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Only react on change
            if (SettingsManager.SaveRangeUnits == (Options.RangeUnits)comboBoxSaveRangeUnits.SelectedIndex)
            {
                return;
            }

            // Update the units
            SettingsManager.SaveRangeUnits = (Options.RangeUnits)comboBoxSaveRangeUnits.SelectedIndex;

            // Update the step
            this.UpdateSavingStep();

            // Update the UI
            this.UpdateFileSaveUnits();
        }

        private void TextBoxSaveStartRange_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool compare = false;
            if (Double.TryParse(textBoxSaveEndRange.Text, out double endValue))
            {
                compare = true;
            }

            if (textBoxSaveStartRange.Text.Length == 0)
            {
                textBoxSaveStartRange.BackColor = Color.Red;
                e.Cancel = true;
                return;
            }

            if (Double.TryParse(textBoxSaveStartRange.Text, out double value))
            {
                if (SettingsManager.SaveRangeUnits == Options.RangeUnits.Pixels) 
                {
                    if (value < 0)
                    {
                        textBoxSaveStartRange.BackColor = Color.Red;
                        e.Cancel = true;
                        return;
                    }
                } else
                {
                    if (value < Math.Round(SpectrumProcessor.Instance.Wavelengths[0], 0))
                    {
                        textBoxSaveStartRange.BackColor = Color.Red;
                        e.Cancel = true;
                        return;
                    }
                }

                if (compare)
                {
                    if (value >= endValue)
                    {
                        textBoxSaveStartRange.BackColor = Color.Red;
                        e.Cancel = true;
                        return;
                    }
                }

                e.Cancel = false;
            }
            else
            {
                textBoxSaveStartRange.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void TextBoxSaveStartRange_Validated(object sender, EventArgs e)
        {
            if (SettingsManager.SaveRangeUnits == Options.RangeUnits.Pixels)
            {
                if (Double.TryParse(textBoxSaveStartRange.Text, out double value))
                {
                    WavelengthMapper.SyncStartPixelToWavelengthAndStore(
                        (int)value, SpectrumProcessor.Instance.Wavelengths
                    );

                    textBoxSaveStartRange.Text = SettingsManager.SaveStartPixel.ToString("F0", CultureInfo.InvariantCulture);
                    labelConvertedStartRange.Text = SettingsManager.SaveStartWavelength.ToString("F1", CultureInfo.InvariantCulture);
                    textBoxSaveStartRange.BackColor = Color.White;
                }  
            }
            else
            {
                if (Double.TryParse(textBoxSaveStartRange.Text, out double value))
                {
                    WavelengthMapper.SyncStartWavelengthToPixelAndStore(
                        value, SpectrumProcessor.Instance.Wavelengths
                    );

                    textBoxSaveStartRange.Text = SettingsManager.SaveStartWavelength.ToString("F1", CultureInfo.InvariantCulture);
                    labelConvertedStartRange.Text = SettingsManager.SaveStartPixel.ToString("F0", CultureInfo.InvariantCulture);
                    textBoxSaveStartRange.BackColor = Color.White;
                }
            }
        }

        private void SavingParameters_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Do not close; just hide
            e.Cancel = true;
            this.Hide();
        }

        private void TextBoxSaveEndRange_Validated(object sender, EventArgs e)
        {
            if (SettingsManager.SaveRangeUnits == Options.RangeUnits.Pixels)
            {
                if (Double.TryParse(textBoxSaveEndRange.Text, out double value))
                {
                    WavelengthMapper.SyncEndPixelToWavelengthAndStore(
                        (int)value, SpectrumProcessor.Instance.Wavelengths
                    );

                    textBoxSaveEndRange.Text = SettingsManager.SaveEndPixel.ToString("F0", CultureInfo.InvariantCulture);
                    labelConvertedEndRange.Text = SettingsManager.SaveEndWavelength.ToString("F1", CultureInfo.InvariantCulture);
                    textBoxSaveEndRange.BackColor = Color.White;
                }
            }
            else
            {
                if (Double.TryParse(textBoxSaveEndRange.Text, out double value))
                {
                    WavelengthMapper.SyncEndPixelToWavelengthAndStore(
                        value, SpectrumProcessor.Instance.Wavelengths
                    );

                    textBoxSaveEndRange.Text = SettingsManager.SaveEndWavelength.ToString("F1", CultureInfo.InvariantCulture);
                    labelConvertedEndRange.Text = SettingsManager.SaveEndPixel.ToString("F0", CultureInfo.InvariantCulture);
                    textBoxSaveEndRange.BackColor = Color.White;
                }
            }
        }

        private void TextBoxSaveEndRange_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool compare = false;
            if (Double.TryParse(textBoxSaveStartRange.Text, out double startValue))
            {
                compare = true;
            }

            if (textBoxSaveEndRange.Text.Length == 0)
            {
                textBoxSaveEndRange.BackColor = Color.Red;
                e.Cancel = true;
                return; 
            }

            if (Double.TryParse(textBoxSaveEndRange.Text, out double value))
            {
                if (compare)
                {
                    if (value <= startValue)
                    {
                        textBoxSaveEndRange.BackColor = Color.Red;
                        e.Cancel = true;
                        return;
                    }
                }
                e.Cancel = false;
            }
            else
            {
                textBoxSaveEndRange.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void buttonResetRange_Click(object sender, EventArgs e)
        {
            int nW = SpectrumProcessor.Instance.Wavelengths.Length;
            if (nW == 0)
            {
                return;
            }

            SettingsManager.SaveStartPixel = 0;
            SettingsManager.SaveEndPixel = nW - 1;
            SettingsManager.SaveStartWavelength = SpectrumProcessor.Instance.Wavelengths[0];
            SettingsManager.SaveEndWavelength = SpectrumProcessor.Instance.Wavelengths[nW - 1];

            UpdateFileSaveUnits();
        }

        private void textBoxWavelengthSavingStep_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (textBoxWavelengthSavingStep.Text.Length == 0)
            {
                textBoxWavelengthSavingStep.BackColor = Color.Red;
                e.Cancel = true;

                return;
            }

            if (SettingsManager.SaveRangeUnits == Options.RangeUnits.Pixels)
            {
                if (Int32.TryParse(textBoxWavelengthSavingStep.Text, out Int32 value))
                {
                    if (value > 0 && value < (SettingsManager.SaveEndPixel - SettingsManager.SaveStartPixel + 1))
                    {
                        textBoxWavelengthSavingStep.BackColor = Color.White;
                        e.Cancel = false;
                    }
                    else
                    {
                        textBoxWavelengthSavingStep.BackColor = Color.Red;
                        e.Cancel = true;
                    }
                }
                else
                {
                    textBoxWavelengthSavingStep.BackColor = Color.Red;
                    e.Cancel = true;
                }
            }
            else
            {
                if (Double.TryParse(textBoxWavelengthSavingStep.Text, out Double value))
                {
                    if (value >= 0.0 && value <= (SettingsManager.SaveEndWavelength - SettingsManager.SaveStartWavelength))
                    {
                        textBoxWavelengthSavingStep.BackColor = Color.White;
                        e.Cancel = false;
                    }
                    else
                    {
                        textBoxWavelengthSavingStep.BackColor = Color.Red;
                        e.Cancel = true;
                    }
                }
                else
                {
                    textBoxWavelengthSavingStep.BackColor = Color.Red;
                    e.Cancel = true;
                }
            }
        }

        private void textBoxWavelengthSavingStep_Validated(object sender, EventArgs e)
        {
            if (Double.TryParse(textBoxWavelengthSavingStep.Text, out double value))
            {
                if (SettingsManager.SaveRangeUnits == Options.RangeUnits.Pixels)
                {
                    if (value > 0 && value < (SettingsManager.SaveEndPixel - SettingsManager.SaveStartPixel + 1))
                    {
                        SettingsManager.SaveStepPixel = (int)value;

                        textBoxWavelengthSavingStep.Text = SettingsManager.SaveStepPixel.ToString(CultureInfo.InvariantCulture);

                        if (SettingsManager.SaveStepPixel == 1)
                        {
                            labelWavelengthSavingStepExplanation.Text = "pixel";
                        }
                        else
                        {
                            labelWavelengthSavingStepExplanation.Text = "pixels";
                        }
                    }
                }
                else
                {
                    if (value >= 0.0 && value <= (SettingsManager.SaveEndWavelength - SettingsManager.SaveStartWavelength))
                    {
                        // Update the saving step
                        int pixelStep = PixelStepFromWavelength(value);
                        SettingsManager.SaveStepPixel = pixelStep;
                    }
                }

                // Update the explanation
                if (SettingsManager.SaveStepPixel == 1)
                {
                    labelConvertedPixels.Text = "Saves everything.";
                }
                else
                {
                    labelConvertedPixels.Text = "Saves every " + SettingsManager.SaveStepPixel.ToString(CultureInfo.InvariantCulture) + " pixels.";
                }
            }
        }

        private void textBoxWavelengthSavingStep_TextChanged(object sender, EventArgs e)
        {
            // Validate on change
            this.Validate();
        }

        private void textBoxSaveStartRange_TextChanged(object sender, EventArgs e)
        {
            // Validate on change
            this.Validate();
        }

        private void textBoxSaveEndRange_TextChanged(object sender, EventArgs e)
        {
            // Validate on change
            this.Validate();
        }
    }
}
