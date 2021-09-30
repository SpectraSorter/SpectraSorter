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

namespace spectra.ui.components
{
    partial class AcquisitionParametersControl
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
            this.checkBoxBufferingEnabled = new System.Windows.Forms.CheckBox();
            this.textBoxSpectraPerRequest = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.checkBoxSingleSWTrigger = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.comboBoxTriggerMode = new System.Windows.Forms.ComboBox();
            this.comboBoxAcquireUnits = new System.Windows.Forms.ComboBox();
            this.textBoxAcquireDuration = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxAcquisitionDelay = new System.Windows.Forms.TextBox();
            this.textBoxBackToBack = new System.Windows.Forms.TextBox();
            this.textBoxScansToAverage = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxIntegrationTime = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxContinuousStrobeWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxContinuousStrobePeriod = new System.Windows.Forms.TextBox();
            this.checkBoxContinuousStrobeEnabled = new System.Windows.Forms.CheckBox();
            this.checkBoxSingleStrobeEnabled = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxSingleStrobePulseWidth = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxSingleStrobePulseDelay = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.labelStrobeParameters = new System.Windows.Forms.Label();
            this.tabControlAcquisitionParameters = new System.Windows.Forms.TabControl();
            this.tabPageCommunication = new System.Windows.Forms.TabPage();
            this.tabPageSaving = new System.Windows.Forms.TabPage();
            this.checkBoxEnableFileNameAutoUpdate = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableSavingToFile = new System.Windows.Forms.CheckBox();
            this.labelFileName = new System.Windows.Forms.Label();
            this.labelExperimentName = new System.Windows.Forms.Label();
            this.textBoxExperiment = new System.Windows.Forms.TextBox();
            this.buttonUpdateFileTime = new System.Windows.Forms.Button();
            this.labelExperimentConditions = new System.Windows.Forms.Label();
            this.buttonSaveDir = new System.Windows.Forms.Button();
            this.textBoxConditions = new System.Windows.Forms.TextBox();
            this.textBoxSaveFilename = new System.Windows.Forms.TextBox();
            this.tabPageStrobe = new System.Windows.Forms.TabPage();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxFWVersion = new System.Windows.Forms.TextBox();
            this.textBoxFWSubversion = new System.Windows.Forms.TextBox();
            this.textBoxFPGAVersion = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBoxSerialNum = new System.Windows.Forms.TextBox();
            this.labelResultFileName = new System.Windows.Forms.Label();
            this.labelSettingsFileName = new System.Windows.Forms.Label();
            this.textBoxSettingsFileName = new System.Windows.Forms.TextBox();
            this.tabControlAcquisitionParameters.SuspendLayout();
            this.tabPageCommunication.SuspendLayout();
            this.tabPageSaving.SuspendLayout();
            this.tabPageStrobe.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxBufferingEnabled
            // 
            this.checkBoxBufferingEnabled.AutoSize = true;
            this.checkBoxBufferingEnabled.Location = new System.Drawing.Point(8, 7);
            this.checkBoxBufferingEnabled.Name = "checkBoxBufferingEnabled";
            this.checkBoxBufferingEnabled.Size = new System.Drawing.Size(110, 17);
            this.checkBoxBufferingEnabled.TabIndex = 170;
            this.checkBoxBufferingEnabled.Text = "Buffering Enabled";
            this.checkBoxBufferingEnabled.UseVisualStyleBackColor = true;
            this.checkBoxBufferingEnabled.CheckedChanged += new System.EventHandler(this.checkBoxBufferingEnabled_CheckedChanged);
            // 
            // textBoxSpectraPerRequest
            // 
            this.textBoxSpectraPerRequest.Location = new System.Drawing.Point(208, 119);
            this.textBoxSpectraPerRequest.Name = "textBoxSpectraPerRequest";
            this.textBoxSpectraPerRequest.Size = new System.Drawing.Size(89, 20);
            this.textBoxSpectraPerRequest.TabIndex = 175;
            this.textBoxSpectraPerRequest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSpectraPerRequest.TextChanged += new System.EventHandler(this.textBoxSpectraPerRequest_TextChanged);
            this.textBoxSpectraPerRequest.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSpectraPerRequest_Validating);
            this.textBoxSpectraPerRequest.Validated += new System.EventHandler(this.textBoxSpectraPerRequest_Validated);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(8, 123);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(169, 13);
            this.label30.TabIndex = 185;
            this.label30.Text = "Num spectra per request (max=15)";
            // 
            // checkBoxSingleSWTrigger
            // 
            this.checkBoxSingleSWTrigger.AutoSize = true;
            this.checkBoxSingleSWTrigger.Location = new System.Drawing.Point(8, 207);
            this.checkBoxSingleSWTrigger.Name = "checkBoxSingleSWTrigger";
            this.checkBoxSingleSWTrigger.Size = new System.Drawing.Size(130, 17);
            this.checkBoxSingleSWTrigger.TabIndex = 178;
            this.checkBoxSingleSWTrigger.Text = "Single software trigger";
            this.checkBoxSingleSWTrigger.UseVisualStyleBackColor = true;
            this.checkBoxSingleSWTrigger.CheckedChanged += new System.EventHandler(this.checkBoxSingleSWTrigger_CheckedChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(8, 179);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(69, 13);
            this.label25.TabIndex = 183;
            this.label25.Text = "Trigger mode";
            // 
            // comboBoxTriggerMode
            // 
            this.comboBoxTriggerMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTriggerMode.FormattingEnabled = true;
            this.comboBoxTriggerMode.ItemHeight = 13;
            this.comboBoxTriggerMode.Items.AddRange(new object[] {
            "Software  (default)",
            "HW - Rising Edge",
            "HW - Falling Edge",
            "HW - Level",
            "HW - Legacy Synchronous",
            "HW - Synchronous  Start/Stop",
            "OFF   (disabled)"});
            this.comboBoxTriggerMode.Location = new System.Drawing.Point(132, 175);
            this.comboBoxTriggerMode.Name = "comboBoxTriggerMode";
            this.comboBoxTriggerMode.Size = new System.Drawing.Size(165, 21);
            this.comboBoxTriggerMode.TabIndex = 177;
            this.comboBoxTriggerMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxTriggerMode_SelectedIndexChanged);
            // 
            // comboBoxAcquireUnits
            // 
            this.comboBoxAcquireUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAcquireUnits.FormattingEnabled = true;
            this.comboBoxAcquireUnits.Items.AddRange(new object[] {
            "seconds",
            "minutes",
            "hours",
            "spectra"});
            this.comboBoxAcquireUnits.Location = new System.Drawing.Point(230, 235);
            this.comboBoxAcquireUnits.Name = "comboBoxAcquireUnits";
            this.comboBoxAcquireUnits.Size = new System.Drawing.Size(67, 21);
            this.comboBoxAcquireUnits.TabIndex = 180;
            this.comboBoxAcquireUnits.SelectedIndexChanged += new System.EventHandler(this.comboBoxAcquireUnits_SelectedIndexChanged);
            // 
            // textBoxAcquireDuration
            // 
            this.textBoxAcquireDuration.Location = new System.Drawing.Point(132, 235);
            this.textBoxAcquireDuration.Name = "textBoxAcquireDuration";
            this.textBoxAcquireDuration.Size = new System.Drawing.Size(89, 20);
            this.textBoxAcquireDuration.TabIndex = 179;
            this.textBoxAcquireDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxAcquireDuration.TextChanged += new System.EventHandler(this.textBoxAcquireDuration_TextChanged);
            this.textBoxAcquireDuration.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAcquireDuration_Validating);
            this.textBoxAcquireDuration.Validated += new System.EventHandler(this.textBoxAcquireDuration_Validated);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 239);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 13);
            this.label16.TabIndex = 182;
            this.label16.Text = "Acquire for";
            // 
            // textBoxAcquisitionDelay
            // 
            this.textBoxAcquisitionDelay.Location = new System.Drawing.Point(208, 147);
            this.textBoxAcquisitionDelay.Name = "textBoxAcquisitionDelay";
            this.textBoxAcquisitionDelay.Size = new System.Drawing.Size(89, 20);
            this.textBoxAcquisitionDelay.TabIndex = 176;
            this.textBoxAcquisitionDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxAcquisitionDelay.TextChanged += new System.EventHandler(this.textBoxAcquisitionDelay_TextChanged);
            this.textBoxAcquisitionDelay.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAcquisitionDelay_Validating);
            this.textBoxAcquisitionDelay.Validated += new System.EventHandler(this.textBoxAcquisitionDelay_Validated);
            // 
            // textBoxBackToBack
            // 
            this.textBoxBackToBack.Location = new System.Drawing.Point(208, 91);
            this.textBoxBackToBack.Name = "textBoxBackToBack";
            this.textBoxBackToBack.Size = new System.Drawing.Size(89, 20);
            this.textBoxBackToBack.TabIndex = 174;
            this.textBoxBackToBack.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxBackToBack.TextChanged += new System.EventHandler(this.textBoxBackToBack_TextChanged);
            this.textBoxBackToBack.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxBackToBack_Validating);
            this.textBoxBackToBack.Validated += new System.EventHandler(this.textBoxBackToBack_Validated);
            // 
            // textBoxScansToAverage
            // 
            this.textBoxScansToAverage.Location = new System.Drawing.Point(208, 63);
            this.textBoxScansToAverage.Name = "textBoxScansToAverage";
            this.textBoxScansToAverage.Size = new System.Drawing.Size(89, 20);
            this.textBoxScansToAverage.TabIndex = 172;
            this.textBoxScansToAverage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxScansToAverage.TextChanged += new System.EventHandler(this.textBoxScansToAverage_TextChanged);
            this.textBoxScansToAverage.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxScansToAverage_Validating);
            this.textBoxScansToAverage.Validated += new System.EventHandler(this.textBoxScansToAverage_Validated);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 151);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(106, 13);
            this.label14.TabIndex = 169;
            this.label14.Text = "Acquisition delay (µs)";
            // 
            // textBoxIntegrationTime
            // 
            this.textBoxIntegrationTime.Location = new System.Drawing.Point(208, 35);
            this.textBoxIntegrationTime.Name = "textBoxIntegrationTime";
            this.textBoxIntegrationTime.Size = new System.Drawing.Size(89, 20);
            this.textBoxIntegrationTime.TabIndex = 171;
            this.textBoxIntegrationTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxIntegrationTime.TextChanged += new System.EventHandler(this.textBoxIntegrationTime_TextChanged);
            this.textBoxIntegrationTime.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxIntegrationTime_Validating);
            this.textBoxIntegrationTime.Validated += new System.EventHandler(this.textBoxIntegrationTime_Validated);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 95);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(185, 13);
            this.label13.TabIndex = 168;
            this.label13.Text = "Back-to-back per trigger (max=65535)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 67);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(149, 13);
            this.label12.TabIndex = 167;
            this.label12.Text = "Scans to average (max=5000)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 13);
            this.label11.TabIndex = 166;
            this.label11.Text = "Integration time (µs)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(251, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 13);
            this.label6.TabIndex = 253;
            this.label6.Text = "µs";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(178, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 252;
            this.label4.Text = "Width";
            // 
            // textBoxContinuousStrobeWidth
            // 
            this.textBoxContinuousStrobeWidth.Location = new System.Drawing.Point(224, 74);
            this.textBoxContinuousStrobeWidth.Name = "textBoxContinuousStrobeWidth";
            this.textBoxContinuousStrobeWidth.Size = new System.Drawing.Size(69, 20);
            this.textBoxContinuousStrobeWidth.TabIndex = 251;
            this.textBoxContinuousStrobeWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxContinuousStrobeWidth.TextChanged += new System.EventHandler(this.textBoxContinuousStrobeWidth_TextChanged);
            this.textBoxContinuousStrobeWidth.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxContinuousStrobeWidth_Validating);
            this.textBoxContinuousStrobeWidth.Validated += new System.EventHandler(this.textBoxContinuousStrobeWidth_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 250;
            this.label3.Text = "Period";
            // 
            // textBoxContinuousStrobePeriod
            // 
            this.textBoxContinuousStrobePeriod.Location = new System.Drawing.Point(224, 48);
            this.textBoxContinuousStrobePeriod.Name = "textBoxContinuousStrobePeriod";
            this.textBoxContinuousStrobePeriod.Size = new System.Drawing.Size(69, 20);
            this.textBoxContinuousStrobePeriod.TabIndex = 249;
            this.textBoxContinuousStrobePeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxContinuousStrobePeriod.TextChanged += new System.EventHandler(this.textBoxContinuousStrobePeriod_TextChanged);
            this.textBoxContinuousStrobePeriod.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxContinuousStrobePeriod_Validating);
            this.textBoxContinuousStrobePeriod.Validated += new System.EventHandler(this.textBoxContinuousStrobePeriod_Validated);
            // 
            // checkBoxContinuousStrobeEnabled
            // 
            this.checkBoxContinuousStrobeEnabled.AutoSize = true;
            this.checkBoxContinuousStrobeEnabled.Location = new System.Drawing.Point(178, 25);
            this.checkBoxContinuousStrobeEnabled.Name = "checkBoxContinuousStrobeEnabled";
            this.checkBoxContinuousStrobeEnabled.Size = new System.Drawing.Size(111, 17);
            this.checkBoxContinuousStrobeEnabled.TabIndex = 248;
            this.checkBoxContinuousStrobeEnabled.Text = "Continuous strobe";
            this.checkBoxContinuousStrobeEnabled.UseVisualStyleBackColor = true;
            this.checkBoxContinuousStrobeEnabled.CheckedChanged += new System.EventHandler(this.checkBoxContinuousStrobeEnabled_CheckedChanged);
            // 
            // checkBoxSingleStrobeEnabled
            // 
            this.checkBoxSingleStrobeEnabled.AutoSize = true;
            this.checkBoxSingleStrobeEnabled.Location = new System.Drawing.Point(7, 25);
            this.checkBoxSingleStrobeEnabled.Name = "checkBoxSingleStrobeEnabled";
            this.checkBoxSingleStrobeEnabled.Size = new System.Drawing.Size(87, 17);
            this.checkBoxSingleStrobeEnabled.TabIndex = 241;
            this.checkBoxSingleStrobeEnabled.Text = "Single strobe";
            this.checkBoxSingleStrobeEnabled.UseVisualStyleBackColor = true;
            this.checkBoxSingleStrobeEnabled.CheckedChanged += new System.EventHandler(this.checkBoxSingleStrobeEnabled_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(98, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 13);
            this.label8.TabIndex = 246;
            this.label8.Text = "µs";
            // 
            // textBoxSingleStrobePulseWidth
            // 
            this.textBoxSingleStrobePulseWidth.Location = new System.Drawing.Point(77, 74);
            this.textBoxSingleStrobePulseWidth.Name = "textBoxSingleStrobePulseWidth";
            this.textBoxSingleStrobePulseWidth.Size = new System.Drawing.Size(69, 20);
            this.textBoxSingleStrobePulseWidth.TabIndex = 244;
            this.textBoxSingleStrobePulseWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSingleStrobePulseWidth.TextChanged += new System.EventHandler(this.textBoxSingleStrobePulseWidth_TextChanged);
            this.textBoxSingleStrobePulseWidth.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSingleStrobePulseWidth_Validating);
            this.textBoxSingleStrobePulseWidth.Validated += new System.EventHandler(this.textBoxSingleStrobePulseWidth_Validated);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 78);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 245;
            this.label9.Text = "Pulse Width";
            // 
            // textBoxSingleStrobePulseDelay
            // 
            this.textBoxSingleStrobePulseDelay.Location = new System.Drawing.Point(76, 48);
            this.textBoxSingleStrobePulseDelay.Name = "textBoxSingleStrobePulseDelay";
            this.textBoxSingleStrobePulseDelay.Size = new System.Drawing.Size(69, 20);
            this.textBoxSingleStrobePulseDelay.TabIndex = 242;
            this.textBoxSingleStrobePulseDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSingleStrobePulseDelay.TextChanged += new System.EventHandler(this.textBoxSingleStrobePulseDelay_TextChanged);
            this.textBoxSingleStrobePulseDelay.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSingleStrobePulseDelay_Validating);
            this.textBoxSingleStrobePulseDelay.Validated += new System.EventHandler(this.textBoxSingleStrobePulseDelay_Validated);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 243;
            this.label10.Text = "Pulse Delay";
            // 
            // labelStrobeParameters
            // 
            this.labelStrobeParameters.AutoSize = true;
            this.labelStrobeParameters.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStrobeParameters.Location = new System.Drawing.Point(3, 3);
            this.labelStrobeParameters.Name = "labelStrobeParameters";
            this.labelStrobeParameters.Size = new System.Drawing.Size(286, 13);
            this.labelStrobeParameters.TabIndex = 255;
            this.labelStrobeParameters.Text = "Strobe parameters are applied only at acquisition start";
            // 
            // tabControlAcquisitionParameters
            // 
            this.tabControlAcquisitionParameters.Controls.Add(this.tabPageCommunication);
            this.tabControlAcquisitionParameters.Controls.Add(this.tabPageSaving);
            this.tabControlAcquisitionParameters.Controls.Add(this.tabPageStrobe);
            this.tabControlAcquisitionParameters.Controls.Add(this.tabPageInfo);
            this.tabControlAcquisitionParameters.Location = new System.Drawing.Point(3, 3);
            this.tabControlAcquisitionParameters.Name = "tabControlAcquisitionParameters";
            this.tabControlAcquisitionParameters.SelectedIndex = 0;
            this.tabControlAcquisitionParameters.Size = new System.Drawing.Size(315, 348);
            this.tabControlAcquisitionParameters.TabIndex = 256;
            // 
            // tabPageCommunication
            // 
            this.tabPageCommunication.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageCommunication.Controls.Add(this.textBoxIntegrationTime);
            this.tabPageCommunication.Controls.Add(this.checkBoxBufferingEnabled);
            this.tabPageCommunication.Controls.Add(this.label11);
            this.tabPageCommunication.Controls.Add(this.textBoxSpectraPerRequest);
            this.tabPageCommunication.Controls.Add(this.label12);
            this.tabPageCommunication.Controls.Add(this.label30);
            this.tabPageCommunication.Controls.Add(this.label13);
            this.tabPageCommunication.Controls.Add(this.checkBoxSingleSWTrigger);
            this.tabPageCommunication.Controls.Add(this.label14);
            this.tabPageCommunication.Controls.Add(this.label25);
            this.tabPageCommunication.Controls.Add(this.textBoxScansToAverage);
            this.tabPageCommunication.Controls.Add(this.comboBoxTriggerMode);
            this.tabPageCommunication.Controls.Add(this.textBoxBackToBack);
            this.tabPageCommunication.Controls.Add(this.comboBoxAcquireUnits);
            this.tabPageCommunication.Controls.Add(this.textBoxAcquisitionDelay);
            this.tabPageCommunication.Controls.Add(this.textBoxAcquireDuration);
            this.tabPageCommunication.Controls.Add(this.label16);
            this.tabPageCommunication.Location = new System.Drawing.Point(4, 22);
            this.tabPageCommunication.Name = "tabPageCommunication";
            this.tabPageCommunication.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPageCommunication.Size = new System.Drawing.Size(307, 322);
            this.tabPageCommunication.TabIndex = 0;
            this.tabPageCommunication.Text = "Communication";
            // 
            // tabPageSaving
            // 
            this.tabPageSaving.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSaving.Controls.Add(this.labelSettingsFileName);
            this.tabPageSaving.Controls.Add(this.labelResultFileName);
            this.tabPageSaving.Controls.Add(this.checkBoxEnableFileNameAutoUpdate);
            this.tabPageSaving.Controls.Add(this.checkBoxEnableSavingToFile);
            this.tabPageSaving.Controls.Add(this.labelFileName);
            this.tabPageSaving.Controls.Add(this.labelExperimentName);
            this.tabPageSaving.Controls.Add(this.textBoxExperiment);
            this.tabPageSaving.Controls.Add(this.buttonUpdateFileTime);
            this.tabPageSaving.Controls.Add(this.labelExperimentConditions);
            this.tabPageSaving.Controls.Add(this.buttonSaveDir);
            this.tabPageSaving.Controls.Add(this.textBoxConditions);
            this.tabPageSaving.Controls.Add(this.textBoxSaveFilename);
            this.tabPageSaving.Controls.Add(this.textBoxSettingsFileName);
            this.tabPageSaving.Location = new System.Drawing.Point(4, 22);
            this.tabPageSaving.Name = "tabPageSaving";
            this.tabPageSaving.Size = new System.Drawing.Size(307, 322);
            this.tabPageSaving.TabIndex = 3;
            this.tabPageSaving.Text = "Saving";
            // 
            // checkBoxEnableFileNameAutoUpdate
            // 
            this.checkBoxEnableFileNameAutoUpdate.AutoSize = true;
            this.checkBoxEnableFileNameAutoUpdate.Location = new System.Drawing.Point(128, 16);
            this.checkBoxEnableFileNameAutoUpdate.Name = "checkBoxEnableFileNameAutoUpdate";
            this.checkBoxEnableFileNameAutoUpdate.Size = new System.Drawing.Size(155, 17);
            this.checkBoxEnableFileNameAutoUpdate.TabIndex = 204;
            this.checkBoxEnableFileNameAutoUpdate.Text = "Auto-update save file name";
            this.checkBoxEnableFileNameAutoUpdate.UseVisualStyleBackColor = true;
            this.checkBoxEnableFileNameAutoUpdate.CheckedChanged += new System.EventHandler(this.checkBoxEnableFileNameAutoUpdate_CheckedChanged);
            // 
            // checkBoxEnableSavingToFile
            // 
            this.checkBoxEnableSavingToFile.AutoSize = true;
            this.checkBoxEnableSavingToFile.Location = new System.Drawing.Point(9, 16);
            this.checkBoxEnableSavingToFile.Name = "checkBoxEnableSavingToFile";
            this.checkBoxEnableSavingToFile.Size = new System.Drawing.Size(79, 17);
            this.checkBoxEnableSavingToFile.TabIndex = 203;
            this.checkBoxEnableSavingToFile.Text = "Save to file";
            this.checkBoxEnableSavingToFile.UseVisualStyleBackColor = true;
            this.checkBoxEnableSavingToFile.CheckedChanged += new System.EventHandler(this.checkBoxEnableSavingToFile_CheckedChanged);
            // 
            // labelFileName
            // 
            this.labelFileName.AutoSize = true;
            this.labelFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFileName.Location = new System.Drawing.Point(6, 73);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(67, 13);
            this.labelFileName.TabIndex = 202;
            this.labelFileName.Text = "File names";
            // 
            // labelExperimentName
            // 
            this.labelExperimentName.AutoSize = true;
            this.labelExperimentName.Location = new System.Drawing.Point(126, 81);
            this.labelExperimentName.Name = "labelExperimentName";
            this.labelExperimentName.Size = new System.Drawing.Size(59, 13);
            this.labelExperimentName.TabIndex = 198;
            this.labelExperimentName.Text = "Experiment";
            // 
            // textBoxExperiment
            // 
            this.textBoxExperiment.Location = new System.Drawing.Point(3, 98);
            this.textBoxExperiment.Name = "textBoxExperiment";
            this.textBoxExperiment.Size = new System.Drawing.Size(301, 20);
            this.textBoxExperiment.TabIndex = 199;
            this.textBoxExperiment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxExperiment.TextChanged += new System.EventHandler(this.TextBoxExperiment_TextChanged);
            // 
            // buttonUpdateFileTime
            // 
            this.buttonUpdateFileTime.Location = new System.Drawing.Point(124, 296);
            this.buttonUpdateFileTime.Name = "buttonUpdateFileTime";
            this.buttonUpdateFileTime.Size = new System.Drawing.Size(59, 23);
            this.buttonUpdateFileTime.TabIndex = 197;
            this.buttonUpdateFileTime.Text = "Update";
            this.buttonUpdateFileTime.UseVisualStyleBackColor = true;
            this.buttonUpdateFileTime.Click += new System.EventHandler(this.ButtonUpdateFileTime_Click);
            // 
            // labelExperimentConditions
            // 
            this.labelExperimentConditions.AutoSize = true;
            this.labelExperimentConditions.Location = new System.Drawing.Point(127, 122);
            this.labelExperimentConditions.Name = "labelExperimentConditions";
            this.labelExperimentConditions.Size = new System.Drawing.Size(56, 13);
            this.labelExperimentConditions.TabIndex = 201;
            this.labelExperimentConditions.Text = "Conditions";
            // 
            // buttonSaveDir
            // 
            this.buttonSaveDir.Location = new System.Drawing.Point(3, 47);
            this.buttonSaveDir.Name = "buttonSaveDir";
            this.buttonSaveDir.Size = new System.Drawing.Size(301, 23);
            this.buttonSaveDir.TabIndex = 195;
            this.buttonSaveDir.Text = "Set target directory...";
            this.buttonSaveDir.UseVisualStyleBackColor = true;
            this.buttonSaveDir.Click += new System.EventHandler(this.buttonSaveDir_Click);
            // 
            // textBoxConditions
            // 
            this.textBoxConditions.Location = new System.Drawing.Point(3, 139);
            this.textBoxConditions.Name = "textBoxConditions";
            this.textBoxConditions.Size = new System.Drawing.Size(301, 20);
            this.textBoxConditions.TabIndex = 200;
            this.textBoxConditions.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxConditions.TextChanged += new System.EventHandler(this.TextBoxConditions_TextChanged);
            // 
            // textBoxSaveFilename
            // 
            this.textBoxSaveFilename.Location = new System.Drawing.Point(3, 187);
            this.textBoxSaveFilename.Multiline = true;
            this.textBoxSaveFilename.Name = "textBoxSaveFilename";
            this.textBoxSaveFilename.ReadOnly = true;
            this.textBoxSaveFilename.Size = new System.Drawing.Size(301, 40);
            this.textBoxSaveFilename.TabIndex = 196;
            this.textBoxSaveFilename.Text = "oceanfx_spectrum.csv";
            this.textBoxSaveFilename.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabPageStrobe
            // 
            this.tabPageStrobe.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageStrobe.Controls.Add(this.labelStrobeParameters);
            this.tabPageStrobe.Controls.Add(this.label10);
            this.tabPageStrobe.Controls.Add(this.label6);
            this.tabPageStrobe.Controls.Add(this.textBoxSingleStrobePulseDelay);
            this.tabPageStrobe.Controls.Add(this.label4);
            this.tabPageStrobe.Controls.Add(this.label9);
            this.tabPageStrobe.Controls.Add(this.textBoxContinuousStrobeWidth);
            this.tabPageStrobe.Controls.Add(this.textBoxSingleStrobePulseWidth);
            this.tabPageStrobe.Controls.Add(this.label3);
            this.tabPageStrobe.Controls.Add(this.label8);
            this.tabPageStrobe.Controls.Add(this.textBoxContinuousStrobePeriod);
            this.tabPageStrobe.Controls.Add(this.checkBoxSingleStrobeEnabled);
            this.tabPageStrobe.Controls.Add(this.checkBoxContinuousStrobeEnabled);
            this.tabPageStrobe.Location = new System.Drawing.Point(4, 22);
            this.tabPageStrobe.Name = "tabPageStrobe";
            this.tabPageStrobe.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPageStrobe.Size = new System.Drawing.Size(307, 322);
            this.tabPageStrobe.TabIndex = 1;
            this.tabPageStrobe.Text = "Strobe";
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageInfo.Controls.Add(this.label17);
            this.tabPageInfo.Controls.Add(this.label19);
            this.tabPageInfo.Controls.Add(this.label20);
            this.tabPageInfo.Controls.Add(this.textBoxFWVersion);
            this.tabPageInfo.Controls.Add(this.textBoxFWSubversion);
            this.tabPageInfo.Controls.Add(this.textBoxFPGAVersion);
            this.tabPageInfo.Controls.Add(this.label21);
            this.tabPageInfo.Controls.Add(this.textBoxSerialNum);
            this.tabPageInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Size = new System.Drawing.Size(307, 322);
            this.tabPageInfo.TabIndex = 2;
            this.tabPageInfo.Text = "Info";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(25, 10);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(87, 13);
            this.label17.TabIndex = 137;
            this.label17.Text = "Firmware Version";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 37);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(105, 13);
            this.label19.TabIndex = 138;
            this.label19.Text = "Firmware Subversion";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(39, 64);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(73, 13);
            this.label20.TabIndex = 139;
            this.label20.Text = "FPGA Version";
            // 
            // textBoxFWVersion
            // 
            this.textBoxFWVersion.Location = new System.Drawing.Point(133, 6);
            this.textBoxFWVersion.Name = "textBoxFWVersion";
            this.textBoxFWVersion.ReadOnly = true;
            this.textBoxFWVersion.Size = new System.Drawing.Size(162, 20);
            this.textBoxFWVersion.TabIndex = 136;
            // 
            // textBoxFWSubversion
            // 
            this.textBoxFWSubversion.Location = new System.Drawing.Point(133, 33);
            this.textBoxFWSubversion.Name = "textBoxFWSubversion";
            this.textBoxFWSubversion.ReadOnly = true;
            this.textBoxFWSubversion.Size = new System.Drawing.Size(162, 20);
            this.textBoxFWSubversion.TabIndex = 140;
            // 
            // textBoxFPGAVersion
            // 
            this.textBoxFPGAVersion.Location = new System.Drawing.Point(133, 60);
            this.textBoxFPGAVersion.Name = "textBoxFPGAVersion";
            this.textBoxFPGAVersion.ReadOnly = true;
            this.textBoxFPGAVersion.Size = new System.Drawing.Size(162, 20);
            this.textBoxFPGAVersion.TabIndex = 141;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(39, 91);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(73, 13);
            this.label21.TabIndex = 142;
            this.label21.Text = "Serial Number";
            // 
            // textBoxSerialNum
            // 
            this.textBoxSerialNum.Location = new System.Drawing.Point(133, 87);
            this.textBoxSerialNum.Name = "textBoxSerialNum";
            this.textBoxSerialNum.ReadOnly = true;
            this.textBoxSerialNum.Size = new System.Drawing.Size(162, 20);
            this.textBoxSerialNum.TabIndex = 143;
            // 
            // labelResultFileName
            // 
            this.labelResultFileName.AutoSize = true;
            this.labelResultFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResultFileName.Location = new System.Drawing.Point(6, 171);
            this.labelResultFileName.Name = "labelResultFileName";
            this.labelResultFileName.Size = new System.Drawing.Size(37, 13);
            this.labelResultFileName.TabIndex = 205;
            this.labelResultFileName.Text = "Result";
            // 
            // labelSettingsFileName
            // 
            this.labelSettingsFileName.AutoSize = true;
            this.labelSettingsFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSettingsFileName.Location = new System.Drawing.Point(6, 232);
            this.labelSettingsFileName.Name = "labelSettingsFileName";
            this.labelSettingsFileName.Size = new System.Drawing.Size(45, 13);
            this.labelSettingsFileName.TabIndex = 207;
            this.labelSettingsFileName.Text = "Settings";
            // 
            // textBoxSettingsFileName
            // 
            this.textBoxSettingsFileName.Location = new System.Drawing.Point(3, 250);
            this.textBoxSettingsFileName.Multiline = true;
            this.textBoxSettingsFileName.Name = "textBoxSettingsFileName";
            this.textBoxSettingsFileName.ReadOnly = true;
            this.textBoxSettingsFileName.Size = new System.Drawing.Size(301, 40);
            this.textBoxSettingsFileName.TabIndex = 206;
            this.textBoxSettingsFileName.Text = "oceanfx_spectrum_settings.xml";
            this.textBoxSettingsFileName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AcquisitionParametersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlAcquisitionParameters);
            this.Name = "AcquisitionParametersControl";
            this.Size = new System.Drawing.Size(319, 356);
            this.tabControlAcquisitionParameters.ResumeLayout(false);
            this.tabPageCommunication.ResumeLayout(false);
            this.tabPageCommunication.PerformLayout();
            this.tabPageSaving.ResumeLayout(false);
            this.tabPageSaving.PerformLayout();
            this.tabPageStrobe.ResumeLayout(false);
            this.tabPageStrobe.PerformLayout();
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBoxBufferingEnabled;
        private System.Windows.Forms.TextBox textBoxSpectraPerRequest;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.CheckBox checkBoxSingleSWTrigger;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox comboBoxTriggerMode;
        private System.Windows.Forms.ComboBox comboBoxAcquireUnits;
        private System.Windows.Forms.TextBox textBoxAcquireDuration;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxAcquisitionDelay;
        private System.Windows.Forms.TextBox textBoxBackToBack;
        private System.Windows.Forms.TextBox textBoxScansToAverage;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxIntegrationTime;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxContinuousStrobeWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxContinuousStrobePeriod;
        private System.Windows.Forms.CheckBox checkBoxContinuousStrobeEnabled;
        private System.Windows.Forms.CheckBox checkBoxSingleStrobeEnabled;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxSingleStrobePulseWidth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxSingleStrobePulseDelay;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelStrobeParameters;
        private System.Windows.Forms.TabControl tabControlAcquisitionParameters;
        private System.Windows.Forms.TabPage tabPageCommunication;
        private System.Windows.Forms.TabPage tabPageStrobe;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxFWVersion;
        private System.Windows.Forms.TextBox textBoxFWSubversion;
        private System.Windows.Forms.TextBox textBoxFPGAVersion;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBoxSerialNum;
        private System.Windows.Forms.TabPage tabPageSaving;
        private System.Windows.Forms.Button buttonSaveDir;
        private System.Windows.Forms.Button buttonUpdateFileTime;
        private System.Windows.Forms.TextBox textBoxSaveFilename;
        private System.Windows.Forms.Label labelExperimentName;
        private System.Windows.Forms.TextBox textBoxExperiment;
        private System.Windows.Forms.TextBox textBoxConditions;
        private System.Windows.Forms.Label labelExperimentConditions;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.CheckBox checkBoxEnableSavingToFile;
        private System.Windows.Forms.CheckBox checkBoxEnableFileNameAutoUpdate;
        private System.Windows.Forms.Label labelSettingsFileName;
        private System.Windows.Forms.Label labelResultFileName;
        private System.Windows.Forms.TextBox textBoxSettingsFileName;
    }
}
