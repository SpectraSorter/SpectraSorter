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
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using spectra.state;

namespace spectra.ui.components
{
    public partial class AcquisitionParametersControl : UserControl
    {
        public AcquisitionParametersControl()
        {
            InitializeComponent();

            this.RegisterEventHandlers();

            this.Refresh();
        }

        override public void Refresh()
        {
            // Fill in the UI elements
            checkBoxBufferingEnabled.Checked = SettingsManager.BufferEnabled == (byte)1;
            textBoxIntegrationTime.Text = SettingsManager.IntegrationTime.ToString(CultureInfo.InvariantCulture);
            textBoxScansToAverage.Text = SettingsManager.ScansToAverage.ToString(CultureInfo.InvariantCulture);
            textBoxBackToBack.Text = SettingsManager.BackToBackPerTrigger.ToString(CultureInfo.InvariantCulture);
            textBoxSpectraPerRequest.Text = SettingsManager.NumSpectraPerRequest.ToString(CultureInfo.InvariantCulture);
            textBoxAcquisitionDelay.Text = SettingsManager.AcquisitionDelay.ToString(CultureInfo.InvariantCulture);
            if (SettingsManager.TriggerMode == 0xFF)
            {
                comboBoxTriggerMode.SelectedIndex = 6;
            }
            else
            {
                comboBoxTriggerMode.SelectedIndex = SettingsManager.TriggerMode;
            }
            checkBoxSingleSWTrigger.Checked = SettingsManager.SingleSoftwareTriggerEnabled;
            textBoxAcquireDuration.Text = SettingsManager.AcquisitionDuration.ToString(CultureInfo.InvariantCulture);
            comboBoxAcquireUnits.SelectedIndex = SettingsManager.AcquisitionDurationIndex;

            checkBoxSingleStrobeEnabled.Checked = SettingsManager.SingleStrobePulseEnabled;
            textBoxSingleStrobePulseDelay.Text = SettingsManager.SingleStrobePulseDelay.ToString(CultureInfo.InvariantCulture);
            textBoxSingleStrobePulseWidth.Text = SettingsManager.SingleStrobePulseWidth.ToString(CultureInfo.InvariantCulture);

            checkBoxContinuousStrobeEnabled.Checked = SettingsManager.ContinuousStrobePulseEnabled;
            textBoxContinuousStrobePeriod.Text = SettingsManager.ContinuousStrobePulsePeriod.ToString(CultureInfo.InvariantCulture);
            textBoxContinuousStrobeWidth.Text = SettingsManager.ContinuousStrobePulseWidth.ToString(CultureInfo.InvariantCulture);

            textBoxExperiment.Text = SettingsManager.ExperimentInfo;
            textBoxConditions.Text = SettingsManager.ConditionsInfo;

            this.ToggleSavingUIElements(SettingsManager.SaveToFile);
            checkBoxEnableFileNameAutoUpdate.Checked = SettingsManager.SaveFileNameAutoUpdate;
            this.UpdateSaveFilename();
        }

        private void checkBoxBufferingEnabled_CheckedChanged(object sender, System.EventArgs e)
        {
            SettingsManager.BufferEnabled = Convert.ToByte(checkBoxBufferingEnabled.Checked);
        }

        private void textBoxIntegrationTime_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Int32.TryParse(textBoxIntegrationTime.Text, out int value))
            {
                if (value < 1)
                {
                    textBoxIntegrationTime.BackColor = Color.Red;
                    e.Cancel = true;
                }
                else
                {
                    textBoxIntegrationTime.BackColor = Color.White;
                    e.Cancel = false;
                }
            }
            else
            {
                textBoxIntegrationTime.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxIntegrationTime_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxIntegrationTime.Text, out uint integrationTime))
            {
                SettingsManager.IntegrationTime = integrationTime;
            }
        }

        private void textBoxIntegrationTime_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();   
        }

        private void textBoxScansToAverage_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void textBoxScansToAverage_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Int32.TryParse(textBoxScansToAverage.Text, out int value))
            {
                if (value < 1 || value > 5000)
                {
                    textBoxScansToAverage.BackColor = Color.Red;
                    e.Cancel = true;
                }
                else
                {
                    textBoxScansToAverage.BackColor = Color.White;
                    e.Cancel = false;
                }
            }
            else
            {
                textBoxScansToAverage.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxScansToAverage_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxScansToAverage.Text, out uint scansToAverage))
            {
                SettingsManager.ScansToAverage = scansToAverage;
            }
        }

        private void textBoxBackToBack_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void textBoxBackToBack_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Int32.TryParse(textBoxBackToBack.Text, out int value))
            {
                if (value < 1 || value > 65535)
                {
                    textBoxBackToBack.BackColor = Color.Red;
                    e.Cancel = true;
                }
                else
                {
                    textBoxBackToBack.BackColor = Color.White;
                    e.Cancel = false;
                }
            }
            else
            {
                textBoxBackToBack.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxBackToBack_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxBackToBack.Text, out uint backToBackPerTrigger))
            {
                SettingsManager.BackToBackPerTrigger = backToBackPerTrigger;
            }
        }

        private void textBoxSpectraPerRequest_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void textBoxSpectraPerRequest_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Int32.TryParse(textBoxSpectraPerRequest.Text, out int value))
            {
                if (value < 1 || value > 15)
                {
                    textBoxSpectraPerRequest.BackColor = Color.Red;
                    e.Cancel = true;
                }
                else
                {
                    textBoxSpectraPerRequest.BackColor = Color.White;
                    e.Cancel = false;
                }
            }
            else
            {
                textBoxSpectraPerRequest.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxSpectraPerRequest_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxSpectraPerRequest.Text, out uint numSpectraPerRequest))
            {
                SettingsManager.NumSpectraPerRequest = numSpectraPerRequest;
            }
        }

        private void textBoxAcquisitionDelay_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void textBoxAcquisitionDelay_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxAcquisitionDelay.Text, out uint acquisitionDelay))
            {
                SettingsManager.AcquisitionDelay = acquisitionDelay;
            }
        }

        private void textBoxAcquisitionDelay_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Int32.TryParse(textBoxAcquisitionDelay.Text, out int value))
            {
                if (value < 0)
                { 
                    textBoxAcquisitionDelay.BackColor = Color.Red;
                    e.Cancel = true;
                }
                else
                {
                    textBoxAcquisitionDelay.BackColor = Color.White;
                    e.Cancel = false;
                }
            }
            else
            {
                textBoxAcquisitionDelay.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void comboBoxTriggerMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte index = (byte)comboBoxTriggerMode.SelectedIndex;
            byte value;
            if (index == 6)
            {
                value = 0xFF;
            }
            else
            {
                value = index;
            }
            SettingsManager.TriggerMode = value;
        }

        private void checkBoxSingleSWTrigger_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.SingleSoftwareTriggerEnabled = checkBoxSingleSWTrigger.Checked;
        }

        private void comboBoxAcquireUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Store the value
            SettingsManager.AcquisitionDurationIndex = comboBoxAcquireUnits.SelectedIndex;
        }

        private void textBoxAcquireDuration_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void textBoxAcquireDuration_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Int32.TryParse(textBoxAcquireDuration.Text, out int value))
            {
                if (value <= 0)
                {
                    textBoxAcquireDuration.BackColor = Color.Red;
                    e.Cancel = true;
                }
                else
                {
                    textBoxAcquireDuration.BackColor = Color.White;
                    e.Cancel = false;
                }
            }
            else
            {
                textBoxAcquireDuration.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxAcquireDuration_Validated(object sender, EventArgs e)
        {
            // Store the value
            if (Double.TryParse(textBoxAcquireDuration.Text, out double acquisitionDuration))
            {
                SettingsManager.AcquisitionDuration = acquisitionDuration;
            }
        }

        private void checkBoxSingleStrobeEnabled_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.SingleStrobePulseEnabled = checkBoxSingleStrobeEnabled.Checked;
        }

        private void checkBoxContinuousStrobeEnabled_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.ContinuousStrobePulseEnabled = checkBoxContinuousStrobeEnabled.Checked;
        }

        private void textBoxSingleStrobePulseDelay_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void textBoxSingleStrobePulseDelay_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Int32.TryParse(textBoxSingleStrobePulseDelay.Text, out int value))
            {
                if (value < 0)
                {
                    textBoxSingleStrobePulseDelay.BackColor = Color.Red;
                    e.Cancel = true;
                }
                else
                {
                    textBoxSingleStrobePulseDelay.BackColor = Color.White;
                    e.Cancel = false;
                }
            }
            else
            {
                textBoxSingleStrobePulseDelay.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxSingleStrobePulseDelay_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxSingleStrobePulseDelay.Text, out uint delay))
            {
                SettingsManager.SingleStrobePulseDelay = delay;
            }
        }

        private void textBoxSingleStrobePulseWidth_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void textBoxSingleStrobePulseWidth_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Int32.TryParse(textBoxSingleStrobePulseWidth.Text, out int value))
            {
                if (value < 0)
                {
                    textBoxSingleStrobePulseWidth.BackColor = Color.Red;
                    e.Cancel = true;
                }
                else
                {
                    textBoxSingleStrobePulseWidth.BackColor = Color.White;
                    e.Cancel = false;
                }
            }
            else
            {
                textBoxSingleStrobePulseWidth.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxSingleStrobePulseWidth_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxSingleStrobePulseWidth.Text, out uint width))
            {
                SettingsManager.SingleStrobePulseWidth = width;
            }
        }

        private void textBoxContinuousStrobePeriod_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void textBoxContinuousStrobePeriod_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Int32.TryParse(textBoxContinuousStrobePeriod.Text, out int value))
            {
                if (value < 0)
                {
                    textBoxContinuousStrobePeriod.BackColor = Color.Red;
                    e.Cancel = true;
                }
                else
                {
                    textBoxContinuousStrobePeriod.BackColor = Color.White;
                    e.Cancel = false;
                }
            }
            else
            {
                textBoxContinuousStrobePeriod.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxContinuousStrobePeriod_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxContinuousStrobePeriod.Text, out uint period))
            {
                SettingsManager.ContinuousStrobePulsePeriod = period;
            }
        }

        private void textBoxContinuousStrobeWidth_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void textBoxContinuousStrobeWidth_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Int32.TryParse(textBoxContinuousStrobeWidth.Text, out int value))
            {
                if (value < 0)
                {
                    textBoxContinuousStrobeWidth.BackColor = Color.Red;
                    e.Cancel = true;
                }
                else
                {
                    textBoxContinuousStrobeWidth.BackColor = Color.White;
                    e.Cancel = false;
                }
            }
            else
            {
                textBoxContinuousStrobeWidth.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxContinuousStrobeWidth_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxContinuousStrobeWidth.Text, out uint width))
            {
                SettingsManager.ContinuousStrobePulseWidth = width;
            }
        }

        /// <summary>
        /// Update the version fields.
        /// </summary>
        /// <param name="version">OceanFX version</param>
        /// <param name="subVersion">OceanFX subversion</param>
        /// <param name="fpgaVersion">FPGA version</param>
        /// <param name="serialNum">Serial number</param>
        public void SetHardwareInfoFields(string version = "",
            string subVersion = "", string fpgaVersion = "",
            string serialNum = "")
        {
            textBoxFWVersion.Text = version;
            textBoxFWSubversion.Text = subVersion;
            textBoxFPGAVersion.Text = fpgaVersion;
            textBoxSerialNum.Text = serialNum;
        }

        private void buttonSaveDir_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (SettingsManager.SaveDirectory != null && SettingsManager.SaveDirectory.Length > 0)
            {
                folderBrowserDialog.SelectedPath = SettingsManager.SaveDirectory;
            }

            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SettingsManager.SaveDirectory = folderBrowserDialog.SelectedPath;
                if (!SettingsManager.SaveDirectory.EndsWith("\\", StringComparison.InvariantCulture))
                {
                    SettingsManager.SaveDirectory += "\\";
                }

                string saveFilename = textBoxSaveFilename.Text;
                int fileIndex = saveFilename.LastIndexOf('\\');
                if (fileIndex > 0)
                {
                    saveFilename = saveFilename.Substring(fileIndex + 1);
                }

                buttonSaveDir.Text = SettingsManager.SaveDirectory;
                textBoxSaveFilename.Text = saveFilename;

                SettingsManager.SaveFileName = Path.Combine(SettingsManager.SaveDirectory, saveFilename);
            }
        }

        //
        // Update the save filename in response to changes in the data type being saved
        //
        public void UpdateSaveFilename()
        {
            string baseName = "spectra";
            if (SettingsManager.SaveDirectory.Length == 0)
            {
                SettingsManager.SaveDirectory = Path.GetTempPath();
            }
            buttonSaveDir.Text = SettingsManager.SaveDirectory;

            // Do we have experiment or conditions information?
            string experiment = textBoxExperiment.Text;
            string conditions = textBoxConditions.Text;

            if (experiment.Length > 0)
            {
                experiment = "_" + experiment.Replace(" ", "_");
            }

            if (conditions.Length > 0)
            {
                conditions = "_" + conditions.Replace(" ", "_");
            }

            textBoxSaveFilename.Text = baseName + experiment + conditions + "_" +
                SettingsManager.ResultSpectrumType.ToString("g").ToLower(CultureInfo.InvariantCulture) +
                DateTime.Now.ToString("_ddMMMyyyy_HHmmss", CultureInfo.InvariantCulture) + ".csv";

            SettingsManager.SaveFileName = Path.Combine(SettingsManager.SaveDirectory, textBoxSaveFilename.Text);
        }

        private void TextBoxExperiment_TextChanged(object sender, EventArgs e)
        {
            SettingsManager.ExperimentInfo = textBoxExperiment.Text;
            this.UpdateSaveFilename();
        }

        private void TextBoxConditions_TextChanged(object sender, EventArgs e)
        {
            SettingsManager.ConditionsInfo = textBoxConditions.Text;
            this.UpdateSaveFilename();
        }

        private void ButtonUpdateFileTime_Click(object sender, EventArgs e)
        {
            this.UpdateSaveFilename();
        }

        private void checkBoxEnableSavingToFile_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.SaveToFile = checkBoxEnableSavingToFile.Checked;

            this.ToggleSavingUIElements(SettingsManager.SaveToFile);
        }

        private void ToggleSavingUIElements(bool enabled)
        {
            checkBoxEnableSavingToFile.Checked = enabled;

            checkBoxEnableFileNameAutoUpdate.Enabled = enabled;
            buttonSaveDir.Enabled = enabled;
            labelFileName.Enabled = enabled;
            labelExperimentName.Enabled = enabled;
            textBoxExperiment.Enabled = enabled;
            labelExperimentConditions.Enabled = enabled;
            textBoxConditions.Enabled = enabled;
            textBoxSaveFilename.Enabled = enabled;
            buttonUpdateFileTime.Enabled = enabled;
        }

        private void checkBoxEnableFileNameAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.SaveFileNameAutoUpdate = checkBoxEnableFileNameAutoUpdate.Checked;
            if (SettingsManager.SaveFileNameAutoUpdate == true)
            {
                this.UpdateSaveFilename();
            }
        }
    }
}
