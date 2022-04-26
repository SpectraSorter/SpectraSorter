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

namespace spectra.ui.components
{
    partial class ProcessingControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStartWavelengthHub = new System.Windows.Forms.Button();
            this.groupBoxFilteringParameters = new System.Windows.Forms.GroupBox();
            this.labelSpectrumFilterWidth = new System.Windows.Forms.Label();
            this.comboBoxFilteringKernelType = new System.Windows.Forms.ComboBox();
            this.textBoxSpectrumFilterWidth = new System.Windows.Forms.TextBox();
            this.labelFilteringSummary = new System.Windows.Forms.Label();
            this.checkBoxFilteringEnable = new System.Windows.Forms.CheckBox();
            this.groupBoxFilteringParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStartWavelengthHub
            // 
            this.buttonStartWavelengthHub.Location = new System.Drawing.Point(4, 4);
            this.buttonStartWavelengthHub.Name = "buttonStartWavelengthHub";
            this.buttonStartWavelengthHub.Size = new System.Drawing.Size(308, 23);
            this.buttonStartWavelengthHub.TabIndex = 0;
            this.buttonStartWavelengthHub.Text = "Launch Wavelength Hub";
            this.buttonStartWavelengthHub.UseVisualStyleBackColor = true;
            this.buttonStartWavelengthHub.Click += new System.EventHandler(this.buttonStartWavelengthHub_Click);
            // 
            // groupBoxFilteringParameters
            // 
            this.groupBoxFilteringParameters.Controls.Add(this.labelSpectrumFilterWidth);
            this.groupBoxFilteringParameters.Controls.Add(this.comboBoxFilteringKernelType);
            this.groupBoxFilteringParameters.Controls.Add(this.textBoxSpectrumFilterWidth);
            this.groupBoxFilteringParameters.Controls.Add(this.labelFilteringSummary);
            this.groupBoxFilteringParameters.Controls.Add(this.checkBoxFilteringEnable);
            this.groupBoxFilteringParameters.Location = new System.Drawing.Point(4, 34);
            this.groupBoxFilteringParameters.Name = "groupBoxFilteringParameters";
            this.groupBoxFilteringParameters.Size = new System.Drawing.Size(308, 102);
            this.groupBoxFilteringParameters.TabIndex = 1;
            this.groupBoxFilteringParameters.TabStop = false;
            this.groupBoxFilteringParameters.Text = "Filtering";
            // 
            // labelSpectrumFilterWidth
            // 
            this.labelSpectrumFilterWidth.AutoSize = true;
            this.labelSpectrumFilterWidth.Location = new System.Drawing.Point(159, 24);
            this.labelSpectrumFilterWidth.Name = "labelSpectrumFilterWidth";
            this.labelSpectrumFilterWidth.Size = new System.Drawing.Size(57, 13);
            this.labelSpectrumFilterWidth.TabIndex = 174;
            this.labelSpectrumFilterWidth.Text = "Filter width";
            // 
            // comboBoxFilteringKernelType
            // 
            this.comboBoxFilteringKernelType.FormattingEnabled = true;
            this.comboBoxFilteringKernelType.Items.AddRange(new object[] {
            "Average filter",
            "Gaussian filter"});
            this.comboBoxFilteringKernelType.Location = new System.Drawing.Point(7, 44);
            this.comboBoxFilteringKernelType.Name = "comboBoxFilteringKernelType";
            this.comboBoxFilteringKernelType.Size = new System.Drawing.Size(140, 21);
            this.comboBoxFilteringKernelType.TabIndex = 3;
            this.comboBoxFilteringKernelType.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilteringKernelType_SelectedIndexChanged);
            // 
            // textBoxSpectrumFilterWidth
            // 
            this.textBoxSpectrumFilterWidth.Location = new System.Drawing.Point(162, 44);
            this.textBoxSpectrumFilterWidth.Name = "textBoxSpectrumFilterWidth";
            this.textBoxSpectrumFilterWidth.Size = new System.Drawing.Size(140, 20);
            this.textBoxSpectrumFilterWidth.TabIndex = 173;
            this.textBoxSpectrumFilterWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSpectrumFilterWidth.TextChanged += new System.EventHandler(this.textBoxSpectrumFilterWidth_TextChanged);
            this.textBoxSpectrumFilterWidth.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSpectrumFilterWidth_Validating);
            this.textBoxSpectrumFilterWidth.Validated += new System.EventHandler(this.textBoxSpectrumFilterWidth_Validated);
            // 
            // labelFilteringSummary
            // 
            this.labelFilteringSummary.Location = new System.Drawing.Point(4, 76);
            this.labelFilteringSummary.Name = "labelFilteringSummary";
            this.labelFilteringSummary.Size = new System.Drawing.Size(298, 23);
            this.labelFilteringSummary.TabIndex = 2;
            // 
            // checkBoxFilteringEnable
            // 
            this.checkBoxFilteringEnable.AutoSize = true;
            this.checkBoxFilteringEnable.Location = new System.Drawing.Point(7, 20);
            this.checkBoxFilteringEnable.Name = "checkBoxFilteringEnable";
            this.checkBoxFilteringEnable.Size = new System.Drawing.Size(59, 17);
            this.checkBoxFilteringEnable.TabIndex = 0;
            this.checkBoxFilteringEnable.Text = "Enable";
            this.checkBoxFilteringEnable.UseVisualStyleBackColor = true;
            this.checkBoxFilteringEnable.CheckedChanged += new System.EventHandler(this.checkBoxFilteringEnable_CheckedChanged);
            // 
            // ProcessingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxFilteringParameters);
            this.Controls.Add(this.buttonStartWavelengthHub);
            this.Name = "ProcessingControl";
            this.Size = new System.Drawing.Size(315, 411);
            this.groupBoxFilteringParameters.ResumeLayout(false);
            this.groupBoxFilteringParameters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStartWavelengthHub;
        private System.Windows.Forms.GroupBox groupBoxFilteringParameters;
        private System.Windows.Forms.Label labelFilteringSummary;
        private System.Windows.Forms.CheckBox checkBoxFilteringEnable;
        private System.Windows.Forms.ComboBox comboBoxFilteringKernelType;
        private System.Windows.Forms.Label labelSpectrumFilterWidth;
        private System.Windows.Forms.TextBox textBoxSpectrumFilterWidth;
    }
}
