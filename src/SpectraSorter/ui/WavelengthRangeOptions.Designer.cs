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

namespace spectra.ui
{
    partial class WavelengthRangeOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelRange = new System.Windows.Forms.Label();
            this.comboBoxSaveRangeUnits = new System.Windows.Forms.ComboBox();
            this.textBoxSaveStartRange = new System.Windows.Forms.TextBox();
            this.label133 = new System.Windows.Forms.Label();
            this.textBoxSaveEndRange = new System.Windows.Forms.TextBox();
            this.groupBoxWavelengthSubset = new System.Windows.Forms.GroupBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelConvertedPixels = new System.Windows.Forms.Label();
            this.labelWavelengthSavingStepExplanation = new System.Windows.Forms.Label();
            this.textBoxWavelengthSavingStep = new System.Windows.Forms.TextBox();
            this.labelTotalPixelRange = new System.Windows.Forms.Label();
            this.labelWavelengthSavingStep = new System.Windows.Forms.Label();
            this.buttonResetRange = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCorrespondingTo = new System.Windows.Forms.Label();
            this.labelConvertedUnit = new System.Windows.Forms.Label();
            this.labelConvertedEndRange = new System.Windows.Forms.Label();
            this.labelConvertedStartRange = new System.Windows.Forms.Label();
            this.labelUnit = new System.Windows.Forms.Label();
            this.groupBoxWavelengthSubset.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelRange
            // 
            this.labelRange.AutoSize = true;
            this.labelRange.Location = new System.Drawing.Point(102, 30);
            this.labelRange.Name = "labelRange";
            this.labelRange.Size = new System.Drawing.Size(37, 13);
            this.labelRange.TabIndex = 203;
            this.labelRange.Text = "Select";
            // 
            // comboBoxSaveRangeUnits
            // 
            this.comboBoxSaveRangeUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSaveRangeUnits.FormattingEnabled = true;
            this.comboBoxSaveRangeUnits.Items.AddRange(new object[] {
            "Pixel Range",
            "Wavelength Range"});
            this.comboBoxSaveRangeUnits.Location = new System.Drawing.Point(145, 27);
            this.comboBoxSaveRangeUnits.Name = "comboBoxSaveRangeUnits";
            this.comboBoxSaveRangeUnits.Size = new System.Drawing.Size(142, 21);
            this.comboBoxSaveRangeUnits.TabIndex = 1;
            this.comboBoxSaveRangeUnits.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSaveRangeUnits_SelectedIndexChanged);
            // 
            // textBoxSaveStartRange
            // 
            this.textBoxSaveStartRange.Location = new System.Drawing.Point(293, 27);
            this.textBoxSaveStartRange.Name = "textBoxSaveStartRange";
            this.textBoxSaveStartRange.Size = new System.Drawing.Size(69, 22);
            this.textBoxSaveStartRange.TabIndex = 2;
            this.textBoxSaveStartRange.Text = "0";
            this.textBoxSaveStartRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxSaveStartRange.TextChanged += new System.EventHandler(this.textBoxSaveStartRange_TextChanged);
            this.textBoxSaveStartRange.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxSaveStartRange_Validating);
            this.textBoxSaveStartRange.Validated += new System.EventHandler(this.TextBoxSaveStartRange_Validated);
            // 
            // label133
            // 
            this.label133.Location = new System.Drawing.Point(368, 29);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(30, 17);
            this.label133.TabIndex = 197;
            this.label133.Text = "to";
            this.label133.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSaveEndRange
            // 
            this.textBoxSaveEndRange.Location = new System.Drawing.Point(404, 27);
            this.textBoxSaveEndRange.Name = "textBoxSaveEndRange";
            this.textBoxSaveEndRange.Size = new System.Drawing.Size(66, 22);
            this.textBoxSaveEndRange.TabIndex = 3;
            this.textBoxSaveEndRange.Text = "0";
            this.textBoxSaveEndRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxSaveEndRange.TextChanged += new System.EventHandler(this.textBoxSaveEndRange_TextChanged);
            this.textBoxSaveEndRange.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxSaveEndRange_Validating);
            this.textBoxSaveEndRange.Validated += new System.EventHandler(this.TextBoxSaveEndRange_Validated);
            // 
            // groupBoxWavelengthSubset
            // 
            this.groupBoxWavelengthSubset.Controls.Add(this.labelInfo);
            this.groupBoxWavelengthSubset.Controls.Add(this.labelConvertedPixels);
            this.groupBoxWavelengthSubset.Controls.Add(this.labelWavelengthSavingStepExplanation);
            this.groupBoxWavelengthSubset.Controls.Add(this.textBoxWavelengthSavingStep);
            this.groupBoxWavelengthSubset.Controls.Add(this.labelTotalPixelRange);
            this.groupBoxWavelengthSubset.Controls.Add(this.labelWavelengthSavingStep);
            this.groupBoxWavelengthSubset.Controls.Add(this.buttonResetRange);
            this.groupBoxWavelengthSubset.Controls.Add(this.label1);
            this.groupBoxWavelengthSubset.Controls.Add(this.labelCorrespondingTo);
            this.groupBoxWavelengthSubset.Controls.Add(this.labelConvertedUnit);
            this.groupBoxWavelengthSubset.Controls.Add(this.labelConvertedEndRange);
            this.groupBoxWavelengthSubset.Controls.Add(this.labelConvertedStartRange);
            this.groupBoxWavelengthSubset.Controls.Add(this.labelUnit);
            this.groupBoxWavelengthSubset.Controls.Add(this.comboBoxSaveRangeUnits);
            this.groupBoxWavelengthSubset.Controls.Add(this.labelRange);
            this.groupBoxWavelengthSubset.Controls.Add(this.textBoxSaveStartRange);
            this.groupBoxWavelengthSubset.Controls.Add(this.textBoxSaveEndRange);
            this.groupBoxWavelengthSubset.Controls.Add(this.label133);
            this.groupBoxWavelengthSubset.Enabled = false;
            this.groupBoxWavelengthSubset.Location = new System.Drawing.Point(5, 12);
            this.groupBoxWavelengthSubset.Name = "groupBoxWavelengthSubset";
            this.groupBoxWavelengthSubset.Size = new System.Drawing.Size(550, 241);
            this.groupBoxWavelengthSubset.TabIndex = 205;
            this.groupBoxWavelengthSubset.TabStop = false;
            this.groupBoxWavelengthSubset.Text = "Save range";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfo.Location = new System.Drawing.Point(6, 183);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(69, 13);
            this.labelInfo.TabIndex = 230;
            this.labelInfo.Text = "Information";
            // 
            // labelConvertedPixels
            // 
            this.labelConvertedPixels.AutoSize = true;
            this.labelConvertedPixels.ForeColor = System.Drawing.Color.Black;
            this.labelConvertedPixels.Location = new System.Drawing.Point(293, 136);
            this.labelConvertedPixels.Name = "labelConvertedPixels";
            this.labelConvertedPixels.Size = new System.Drawing.Size(95, 13);
            this.labelConvertedPixels.TabIndex = 229;
            this.labelConvertedPixels.Text = "Saves everything.";
            this.labelConvertedPixels.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelWavelengthSavingStepExplanation
            // 
            this.labelWavelengthSavingStepExplanation.AutoSize = true;
            this.labelWavelengthSavingStepExplanation.Location = new System.Drawing.Point(368, 102);
            this.labelWavelengthSavingStepExplanation.Name = "labelWavelengthSavingStepExplanation";
            this.labelWavelengthSavingStepExplanation.Size = new System.Drawing.Size(36, 13);
            this.labelWavelengthSavingStepExplanation.TabIndex = 223;
            this.labelWavelengthSavingStepExplanation.Text = "pixels";
            // 
            // textBoxWavelengthSavingStep
            // 
            this.textBoxWavelengthSavingStep.Location = new System.Drawing.Point(293, 99);
            this.textBoxWavelengthSavingStep.Name = "textBoxWavelengthSavingStep";
            this.textBoxWavelengthSavingStep.Size = new System.Drawing.Size(69, 22);
            this.textBoxWavelengthSavingStep.TabIndex = 4;
            this.textBoxWavelengthSavingStep.Text = "1";
            this.textBoxWavelengthSavingStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxWavelengthSavingStep.TextChanged += new System.EventHandler(this.textBoxWavelengthSavingStep_TextChanged);
            this.textBoxWavelengthSavingStep.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxWavelengthSavingStep_Validating);
            this.textBoxWavelengthSavingStep.Validated += new System.EventHandler(this.textBoxWavelengthSavingStep_Validated);
            // 
            // labelTotalPixelRange
            // 
            this.labelTotalPixelRange.AutoSize = true;
            this.labelTotalPixelRange.Location = new System.Drawing.Point(7, 210);
            this.labelTotalPixelRange.Name = "labelTotalPixelRange";
            this.labelTotalPixelRange.Size = new System.Drawing.Size(209, 13);
            this.labelTotalPixelRange.TabIndex = 226;
            this.labelTotalPixelRange.Text = "Spectrum information not yet available.";
            // 
            // labelWavelengthSavingStep
            // 
            this.labelWavelengthSavingStep.AutoSize = true;
            this.labelWavelengthSavingStep.Location = new System.Drawing.Point(218, 102);
            this.labelWavelengthSavingStep.Name = "labelWavelengthSavingStep";
            this.labelWavelengthSavingStep.Size = new System.Drawing.Size(69, 13);
            this.labelWavelengthSavingStep.TabIndex = 221;
            this.labelWavelengthSavingStep.Text = "Saving step:";
            // 
            // buttonResetRange
            // 
            this.buttonResetRange.Location = new System.Drawing.Point(21, 25);
            this.buttonResetRange.Name = "buttonResetRange";
            this.buttonResetRange.Size = new System.Drawing.Size(75, 23);
            this.buttonResetRange.TabIndex = 0;
            this.buttonResetRange.Text = "Reset";
            this.buttonResetRange.UseVisualStyleBackColor = true;
            this.buttonResetRange.Click += new System.EventHandler(this.buttonResetRange_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(368, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 17);
            this.label1.TabIndex = 219;
            this.label1.Text = "to";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCorrespondingTo
            // 
            this.labelCorrespondingTo.AutoSize = true;
            this.labelCorrespondingTo.Location = new System.Drawing.Point(185, 65);
            this.labelCorrespondingTo.Name = "labelCorrespondingTo";
            this.labelCorrespondingTo.Size = new System.Drawing.Size(102, 13);
            this.labelCorrespondingTo.TabIndex = 218;
            this.labelCorrespondingTo.Text = "Corresponding to:";
            // 
            // labelConvertedUnit
            // 
            this.labelConvertedUnit.AutoSize = true;
            this.labelConvertedUnit.Location = new System.Drawing.Point(477, 65);
            this.labelConvertedUnit.Name = "labelConvertedUnit";
            this.labelConvertedUnit.Size = new System.Drawing.Size(23, 13);
            this.labelConvertedUnit.TabIndex = 215;
            this.labelConvertedUnit.Text = "nm";
            // 
            // labelConvertedEndRange
            // 
            this.labelConvertedEndRange.ForeColor = System.Drawing.Color.Black;
            this.labelConvertedEndRange.Location = new System.Drawing.Point(404, 62);
            this.labelConvertedEndRange.Name = "labelConvertedEndRange";
            this.labelConvertedEndRange.Size = new System.Drawing.Size(65, 19);
            this.labelConvertedEndRange.TabIndex = 214;
            this.labelConvertedEndRange.Text = "0";
            this.labelConvertedEndRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelConvertedStartRange
            // 
            this.labelConvertedStartRange.ForeColor = System.Drawing.Color.Black;
            this.labelConvertedStartRange.Location = new System.Drawing.Point(293, 62);
            this.labelConvertedStartRange.Name = "labelConvertedStartRange";
            this.labelConvertedStartRange.Size = new System.Drawing.Size(69, 19);
            this.labelConvertedStartRange.TabIndex = 213;
            this.labelConvertedStartRange.Text = "0";
            this.labelConvertedStartRange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelUnit
            // 
            this.labelUnit.AutoSize = true;
            this.labelUnit.Location = new System.Drawing.Point(477, 31);
            this.labelUnit.Name = "labelUnit";
            this.labelUnit.Size = new System.Drawing.Size(36, 13);
            this.labelUnit.TabIndex = 211;
            this.labelUnit.Text = "pixels";
            // 
            // WavelengthRangeOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 265);
            this.Controls.Add(this.groupBoxWavelengthSubset);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "WavelengthRangeOptions";
            this.Text = "Spectrum ranges";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SavingParameters_FormClosing);
            this.groupBoxWavelengthSubset.ResumeLayout(false);
            this.groupBoxWavelengthSubset.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelRange;
        private System.Windows.Forms.ComboBox comboBoxSaveRangeUnits;
        private System.Windows.Forms.TextBox textBoxSaveStartRange;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.TextBox textBoxSaveEndRange;
        private System.Windows.Forms.GroupBox groupBoxWavelengthSubset;
        private System.Windows.Forms.Label labelUnit;
        private System.Windows.Forms.Label labelConvertedUnit;
        private System.Windows.Forms.Label labelConvertedEndRange;
        private System.Windows.Forms.Label labelConvertedStartRange;
        private System.Windows.Forms.Label labelCorrespondingTo;
        private System.Windows.Forms.Label labelTotalPixelRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonResetRange;
        private System.Windows.Forms.Label labelWavelengthSavingStepExplanation;
        private System.Windows.Forms.TextBox textBoxWavelengthSavingStep;
        private System.Windows.Forms.Label labelWavelengthSavingStep;
        private System.Windows.Forms.Label labelConvertedPixels;
        private System.Windows.Forms.Label labelInfo;
    }
}