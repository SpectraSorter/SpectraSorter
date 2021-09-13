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
            this.checkBoxEnableFileNameAutoUpdate = new System.Windows.Forms.CheckBox();
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
            this.checkBoxBufferingEnabled.Location = new System.Drawing.Point(11, 9);
            this.checkBoxBufferingEnabled.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxBufferingEnabled.Name = "checkBoxBufferingEnabled";
            this.checkBoxBufferingEnabled.Size = new System.Drawing.Size(143, 21);
            this.checkBoxBufferingEnabled.TabIndex = 170;
            this.checkBoxBufferingEnabled.Text = "Buffering Enabled";
            this.checkBoxBufferingEnabled.UseVisualStyleBackColor = true;
            this.checkBoxBufferingEnabled.CheckedChanged += new System.EventHandler(this.checkBoxBufferingEnabled_CheckedChanged);
            // 
            // textBoxSpectraPerRequest
            // 
            this.textBoxSpectraPerRequest.Location = new System.Drawing.Point(277, 146);
            this.textBoxSpectraPerRequest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSpectraPerRequest.Name = "textBoxSpectraPerRequest";
            this.textBoxSpectraPerRequest.Size = new System.Drawing.Size(117, 22);
            this.textBoxSpectraPerRequest.TabIndex = 175;
            this.textBoxSpectraPerRequest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSpectraPerRequest.TextChanged += new System.EventHandler(this.textBoxSpectraPerRequest_TextChanged);
            this.textBoxSpectraPerRequest.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSpectraPerRequest_Validating);
            this.textBoxSpectraPerRequest.Validated += new System.EventHandler(this.textBoxSpectraPerRequest_Validated);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(11, 151);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(228, 17);
            this.label30.TabIndex = 185;
            this.label30.Text = "Num spectra per request (max=15)";
            // 
            // checkBoxSingleSWTrigger
            // 
            this.checkBoxSingleSWTrigger.AutoSize = true;
            this.checkBoxSingleSWTrigger.Location = new System.Drawing.Point(11, 255);
            this.checkBoxSingleSWTrigger.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxSingleSWTrigger.Name = "checkBoxSingleSWTrigger";
            this.checkBoxSingleSWTrigger.Size = new System.Drawing.Size(171, 21);
            this.checkBoxSingleSWTrigger.TabIndex = 178;
            this.checkBoxSingleSWTrigger.Text = "Single software trigger";
            this.checkBoxSingleSWTrigger.UseVisualStyleBackColor = true;
            this.checkBoxSingleSWTrigger.CheckedChanged += new System.EventHandler(this.checkBoxSingleSWTrigger_CheckedChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(11, 220);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(93, 17);
            this.label25.TabIndex = 183;
            this.label25.Text = "Trigger mode";
            // 
            // comboBoxTriggerMode
            // 
            this.comboBoxTriggerMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTriggerMode.FormattingEnabled = true;
            this.comboBoxTriggerMode.ItemHeight = 16;
            this.comboBoxTriggerMode.Items.AddRange(new object[] {
            "Software  (default)",
            "HW - Rising Edge",
            "HW - Falling Edge",
            "HW - Level",
            "HW - Legacy Synchronous",
            "HW - Synchronous  Start/Stop",
            "OFF   (disabled)"});
            this.comboBoxTriggerMode.Location = new System.Drawing.Point(176, 215);
            this.comboBoxTriggerMode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxTriggerMode.Name = "comboBoxTriggerMode";
            this.comboBoxTriggerMode.Size = new System.Drawing.Size(219, 24);
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
            this.comboBoxAcquireUnits.Location = new System.Drawing.Point(307, 289);
            this.comboBoxAcquireUnits.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxAcquireUnits.Name = "comboBoxAcquireUnits";
            this.comboBoxAcquireUnits.Size = new System.Drawing.Size(88, 24);
            this.comboBoxAcquireUnits.TabIndex = 180;
            this.comboBoxAcquireUnits.SelectedIndexChanged += new System.EventHandler(this.comboBoxAcquireUnits_SelectedIndexChanged);
            // 
            // textBoxAcquireDuration
            // 
            this.textBoxAcquireDuration.Location = new System.Drawing.Point(176, 289);
            this.textBoxAcquireDuration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxAcquireDuration.Name = "textBoxAcquireDuration";
            this.textBoxAcquireDuration.Size = new System.Drawing.Size(117, 22);
            this.textBoxAcquireDuration.TabIndex = 179;
            this.textBoxAcquireDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxAcquireDuration.TextChanged += new System.EventHandler(this.textBoxAcquireDuration_TextChanged);
            this.textBoxAcquireDuration.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAcquireDuration_Validating);
            this.textBoxAcquireDuration.Validated += new System.EventHandler(this.textBoxAcquireDuration_Validated);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(11, 294);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 17);
            this.label16.TabIndex = 182;
            this.label16.Text = "Acquire for";
            // 
            // textBoxAcquisitionDelay
            // 
            this.textBoxAcquisitionDelay.Location = new System.Drawing.Point(277, 181);
            this.textBoxAcquisitionDelay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxAcquisitionDelay.Name = "textBoxAcquisitionDelay";
            this.textBoxAcquisitionDelay.Size = new System.Drawing.Size(117, 22);
            this.textBoxAcquisitionDelay.TabIndex = 176;
            this.textBoxAcquisitionDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxAcquisitionDelay.TextChanged += new System.EventHandler(this.textBoxAcquisitionDelay_TextChanged);
            this.textBoxAcquisitionDelay.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAcquisitionDelay_Validating);
            this.textBoxAcquisitionDelay.Validated += new System.EventHandler(this.textBoxAcquisitionDelay_Validated);
            // 
            // textBoxBackToBack
            // 
            this.textBoxBackToBack.Location = new System.Drawing.Point(277, 112);
            this.textBoxBackToBack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxBackToBack.Name = "textBoxBackToBack";
            this.textBoxBackToBack.Size = new System.Drawing.Size(117, 22);
            this.textBoxBackToBack.TabIndex = 174;
            this.textBoxBackToBack.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxBackToBack.TextChanged += new System.EventHandler(this.textBoxBackToBack_TextChanged);
            this.textBoxBackToBack.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxBackToBack_Validating);
            this.textBoxBackToBack.Validated += new System.EventHandler(this.textBoxBackToBack_Validated);
            // 
            // textBoxScansToAverage
            // 
            this.textBoxScansToAverage.Location = new System.Drawing.Point(277, 78);
            this.textBoxScansToAverage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxScansToAverage.Name = "textBoxScansToAverage";
            this.textBoxScansToAverage.Size = new System.Drawing.Size(117, 22);
            this.textBoxScansToAverage.TabIndex = 172;
            this.textBoxScansToAverage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxScansToAverage.TextChanged += new System.EventHandler(this.textBoxScansToAverage_TextChanged);
            this.textBoxScansToAverage.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxScansToAverage_Validating);
            this.textBoxScansToAverage.Validated += new System.EventHandler(this.textBoxScansToAverage_Validated);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 186);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(143, 17);
            this.label14.TabIndex = 169;
            this.label14.Text = "Acquisition delay (µs)";
            // 
            // textBoxIntegrationTime
            // 
            this.textBoxIntegrationTime.Location = new System.Drawing.Point(277, 43);
            this.textBoxIntegrationTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxIntegrationTime.Name = "textBoxIntegrationTime";
            this.textBoxIntegrationTime.Size = new System.Drawing.Size(117, 22);
            this.textBoxIntegrationTime.TabIndex = 171;
            this.textBoxIntegrationTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxIntegrationTime.TextChanged += new System.EventHandler(this.textBoxIntegrationTime_TextChanged);
            this.textBoxIntegrationTime.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxIntegrationTime_Validating);
            this.textBoxIntegrationTime.Validated += new System.EventHandler(this.textBoxIntegrationTime_Validated);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 117);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(248, 17);
            this.label13.TabIndex = 168;
            this.label13.Text = "Back-to-back per trigger (max=65535)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 82);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(198, 17);
            this.label12.TabIndex = 167;
            this.label12.Text = "Scans to average (max=5000)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 48);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 17);
            this.label11.TabIndex = 166;
            this.label11.Text = "Integration time (µs)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(335, 119);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 17);
            this.label6.TabIndex = 253;
            this.label6.Text = "µs";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 252;
            this.label4.Text = "Width";
            // 
            // textBoxContinuousStrobeWidth
            // 
            this.textBoxContinuousStrobeWidth.Location = new System.Drawing.Point(299, 91);
            this.textBoxContinuousStrobeWidth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxContinuousStrobeWidth.Name = "textBoxContinuousStrobeWidth";
            this.textBoxContinuousStrobeWidth.Size = new System.Drawing.Size(91, 22);
            this.textBoxContinuousStrobeWidth.TabIndex = 251;
            this.textBoxContinuousStrobeWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxContinuousStrobeWidth.TextChanged += new System.EventHandler(this.textBoxContinuousStrobeWidth_TextChanged);
            this.textBoxContinuousStrobeWidth.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxContinuousStrobeWidth_Validating);
            this.textBoxContinuousStrobeWidth.Validated += new System.EventHandler(this.textBoxContinuousStrobeWidth_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(237, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 17);
            this.label3.TabIndex = 250;
            this.label3.Text = "Period";
            // 
            // textBoxContinuousStrobePeriod
            // 
            this.textBoxContinuousStrobePeriod.Location = new System.Drawing.Point(299, 59);
            this.textBoxContinuousStrobePeriod.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxContinuousStrobePeriod.Name = "textBoxContinuousStrobePeriod";
            this.textBoxContinuousStrobePeriod.Size = new System.Drawing.Size(91, 22);
            this.textBoxContinuousStrobePeriod.TabIndex = 249;
            this.textBoxContinuousStrobePeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxContinuousStrobePeriod.TextChanged += new System.EventHandler(this.textBoxContinuousStrobePeriod_TextChanged);
            this.textBoxContinuousStrobePeriod.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxContinuousStrobePeriod_Validating);
            this.textBoxContinuousStrobePeriod.Validated += new System.EventHandler(this.textBoxContinuousStrobePeriod_Validated);
            // 
            // checkBoxContinuousStrobeEnabled
            // 
            this.checkBoxContinuousStrobeEnabled.AutoSize = true;
            this.checkBoxContinuousStrobeEnabled.Location = new System.Drawing.Point(237, 31);
            this.checkBoxContinuousStrobeEnabled.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxContinuousStrobeEnabled.Name = "checkBoxContinuousStrobeEnabled";
            this.checkBoxContinuousStrobeEnabled.Size = new System.Drawing.Size(145, 21);
            this.checkBoxContinuousStrobeEnabled.TabIndex = 248;
            this.checkBoxContinuousStrobeEnabled.Text = "Continuous strobe";
            this.checkBoxContinuousStrobeEnabled.UseVisualStyleBackColor = true;
            this.checkBoxContinuousStrobeEnabled.CheckedChanged += new System.EventHandler(this.checkBoxContinuousStrobeEnabled_CheckedChanged);
            // 
            // checkBoxSingleStrobeEnabled
            // 
            this.checkBoxSingleStrobeEnabled.AutoSize = true;
            this.checkBoxSingleStrobeEnabled.Location = new System.Drawing.Point(9, 31);
            this.checkBoxSingleStrobeEnabled.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxSingleStrobeEnabled.Name = "checkBoxSingleStrobeEnabled";
            this.checkBoxSingleStrobeEnabled.Size = new System.Drawing.Size(113, 21);
            this.checkBoxSingleStrobeEnabled.TabIndex = 241;
            this.checkBoxSingleStrobeEnabled.Text = "Single strobe";
            this.checkBoxSingleStrobeEnabled.UseVisualStyleBackColor = true;
            this.checkBoxSingleStrobeEnabled.CheckedChanged += new System.EventHandler(this.checkBoxSingleStrobeEnabled_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(131, 119);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 17);
            this.label8.TabIndex = 246;
            this.label8.Text = "µs";
            // 
            // textBoxSingleStrobePulseWidth
            // 
            this.textBoxSingleStrobePulseWidth.Location = new System.Drawing.Point(103, 91);
            this.textBoxSingleStrobePulseWidth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSingleStrobePulseWidth.Name = "textBoxSingleStrobePulseWidth";
            this.textBoxSingleStrobePulseWidth.Size = new System.Drawing.Size(91, 22);
            this.textBoxSingleStrobePulseWidth.TabIndex = 244;
            this.textBoxSingleStrobePulseWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSingleStrobePulseWidth.TextChanged += new System.EventHandler(this.textBoxSingleStrobePulseWidth_TextChanged);
            this.textBoxSingleStrobePulseWidth.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSingleStrobePulseWidth_Validating);
            this.textBoxSingleStrobePulseWidth.Validated += new System.EventHandler(this.textBoxSingleStrobePulseWidth_Validated);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 96);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 17);
            this.label9.TabIndex = 245;
            this.label9.Text = "Pulse Width";
            // 
            // textBoxSingleStrobePulseDelay
            // 
            this.textBoxSingleStrobePulseDelay.Location = new System.Drawing.Point(101, 59);
            this.textBoxSingleStrobePulseDelay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSingleStrobePulseDelay.Name = "textBoxSingleStrobePulseDelay";
            this.textBoxSingleStrobePulseDelay.Size = new System.Drawing.Size(91, 22);
            this.textBoxSingleStrobePulseDelay.TabIndex = 242;
            this.textBoxSingleStrobePulseDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSingleStrobePulseDelay.TextChanged += new System.EventHandler(this.textBoxSingleStrobePulseDelay_TextChanged);
            this.textBoxSingleStrobePulseDelay.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSingleStrobePulseDelay_Validating);
            this.textBoxSingleStrobePulseDelay.Validated += new System.EventHandler(this.textBoxSingleStrobePulseDelay_Validated);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 64);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 17);
            this.label10.TabIndex = 243;
            this.label10.Text = "Pulse Delay";
            // 
            // labelStrobeParameters
            // 
            this.labelStrobeParameters.AutoSize = true;
            this.labelStrobeParameters.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStrobeParameters.Location = new System.Drawing.Point(4, 4);
            this.labelStrobeParameters.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStrobeParameters.Name = "labelStrobeParameters";
            this.labelStrobeParameters.Size = new System.Drawing.Size(340, 19);
            this.labelStrobeParameters.TabIndex = 255;
            this.labelStrobeParameters.Text = "Strobe parameters are applied only at acquisition start";
            // 
            // tabControlAcquisitionParameters
            // 
            this.tabControlAcquisitionParameters.Controls.Add(this.tabPageCommunication);
            this.tabControlAcquisitionParameters.Controls.Add(this.tabPageSaving);
            this.tabControlAcquisitionParameters.Controls.Add(this.tabPageStrobe);
            this.tabControlAcquisitionParameters.Controls.Add(this.tabPageInfo);
            this.tabControlAcquisitionParameters.Location = new System.Drawing.Point(4, 4);
            this.tabControlAcquisitionParameters.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControlAcquisitionParameters.Name = "tabControlAcquisitionParameters";
            this.tabControlAcquisitionParameters.SelectedIndex = 0;
            this.tabControlAcquisitionParameters.Size = new System.Drawing.Size(420, 363);
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
            this.tabPageCommunication.Location = new System.Drawing.Point(4, 25);
            this.tabPageCommunication.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageCommunication.Name = "tabPageCommunication";
            this.tabPageCommunication.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageCommunication.Size = new System.Drawing.Size(412, 334);
            this.tabPageCommunication.TabIndex = 0;
            this.tabPageCommunication.Text = "Communication";
            // 
            // tabPageSaving
            // 
            this.tabPageSaving.BackColor = System.Drawing.SystemColors.Control;
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
            this.tabPageSaving.Location = new System.Drawing.Point(4, 25);
            this.tabPageSaving.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageSaving.Name = "tabPageSaving";
            this.tabPageSaving.Size = new System.Drawing.Size(412, 334);
            this.tabPageSaving.TabIndex = 3;
            this.tabPageSaving.Text = "Saving";
            // 
            // checkBoxEnableSavingToFile
            // 
            this.checkBoxEnableSavingToFile.AutoSize = true;
            this.checkBoxEnableSavingToFile.Location = new System.Drawing.Point(12, 20);
            this.checkBoxEnableSavingToFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxEnableSavingToFile.Name = "checkBoxEnableSavingToFile";
            this.checkBoxEnableSavingToFile.Size = new System.Drawing.Size(100, 21);
            this.checkBoxEnableSavingToFile.TabIndex = 203;
            this.checkBoxEnableSavingToFile.Text = "Save to file";
            this.checkBoxEnableSavingToFile.UseVisualStyleBackColor = true;
            this.checkBoxEnableSavingToFile.CheckedChanged += new System.EventHandler(this.checkBoxEnableSavingToFile_CheckedChanged);
            // 
            // labelFileName
            // 
            this.labelFileName.AutoSize = true;
            this.labelFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFileName.Location = new System.Drawing.Point(8, 90);
            this.labelFileName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(78, 17);
            this.labelFileName.TabIndex = 202;
            this.labelFileName.Text = "File name";
            // 
            // labelExperimentName
            // 
            this.labelExperimentName.AutoSize = true;
            this.labelExperimentName.Location = new System.Drawing.Point(168, 100);
            this.labelExperimentName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelExperimentName.Name = "labelExperimentName";
            this.labelExperimentName.Size = new System.Drawing.Size(78, 17);
            this.labelExperimentName.TabIndex = 198;
            this.labelExperimentName.Text = "Experiment";
            // 
            // textBoxExperiment
            // 
            this.textBoxExperiment.Location = new System.Drawing.Point(4, 121);
            this.textBoxExperiment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxExperiment.Name = "textBoxExperiment";
            this.textBoxExperiment.Size = new System.Drawing.Size(400, 22);
            this.textBoxExperiment.TabIndex = 199;
            this.textBoxExperiment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxExperiment.TextChanged += new System.EventHandler(this.TextBoxExperiment_TextChanged);
            // 
            // buttonUpdateFileTime
            // 
            this.buttonUpdateFileTime.Location = new System.Drawing.Point(168, 299);
            this.buttonUpdateFileTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonUpdateFileTime.Name = "buttonUpdateFileTime";
            this.buttonUpdateFileTime.Size = new System.Drawing.Size(79, 28);
            this.buttonUpdateFileTime.TabIndex = 197;
            this.buttonUpdateFileTime.Text = "Update";
            this.buttonUpdateFileTime.UseVisualStyleBackColor = true;
            this.buttonUpdateFileTime.Click += new System.EventHandler(this.ButtonUpdateFileTime_Click);
            // 
            // labelExperimentConditions
            // 
            this.labelExperimentConditions.AutoSize = true;
            this.labelExperimentConditions.Location = new System.Drawing.Point(169, 150);
            this.labelExperimentConditions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelExperimentConditions.Name = "labelExperimentConditions";
            this.labelExperimentConditions.Size = new System.Drawing.Size(74, 17);
            this.labelExperimentConditions.TabIndex = 201;
            this.labelExperimentConditions.Text = "Conditions";
            // 
            // buttonSaveDir
            // 
            this.buttonSaveDir.Location = new System.Drawing.Point(4, 58);
            this.buttonSaveDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSaveDir.Name = "buttonSaveDir";
            this.buttonSaveDir.Size = new System.Drawing.Size(401, 28);
            this.buttonSaveDir.TabIndex = 195;
            this.buttonSaveDir.Text = "Set target directory...";
            this.buttonSaveDir.UseVisualStyleBackColor = true;
            this.buttonSaveDir.Click += new System.EventHandler(this.buttonSaveDir_Click);
            // 
            // textBoxConditions
            // 
            this.textBoxConditions.Location = new System.Drawing.Point(4, 171);
            this.textBoxConditions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxConditions.Name = "textBoxConditions";
            this.textBoxConditions.Size = new System.Drawing.Size(400, 22);
            this.textBoxConditions.TabIndex = 200;
            this.textBoxConditions.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxConditions.TextChanged += new System.EventHandler(this.TextBoxConditions_TextChanged);
            // 
            // textBoxSaveFilename
            // 
            this.textBoxSaveFilename.Location = new System.Drawing.Point(4, 219);
            this.textBoxSaveFilename.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSaveFilename.Multiline = true;
            this.textBoxSaveFilename.Name = "textBoxSaveFilename";
            this.textBoxSaveFilename.ReadOnly = true;
            this.textBoxSaveFilename.Size = new System.Drawing.Size(400, 72);
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
            this.tabPageStrobe.Location = new System.Drawing.Point(4, 25);
            this.tabPageStrobe.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageStrobe.Name = "tabPageStrobe";
            this.tabPageStrobe.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageStrobe.Size = new System.Drawing.Size(412, 334);
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
            this.tabPageInfo.Location = new System.Drawing.Point(4, 25);
            this.tabPageInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Size = new System.Drawing.Size(412, 334);
            this.tabPageInfo.TabIndex = 2;
            this.tabPageInfo.Text = "Info";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(33, 12);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(117, 17);
            this.label17.TabIndex = 137;
            this.label17.Text = "Firmware Version";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 46);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(140, 17);
            this.label19.TabIndex = 138;
            this.label19.Text = "Firmware Subversion";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(52, 79);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(97, 17);
            this.label20.TabIndex = 139;
            this.label20.Text = "FPGA Version";
            // 
            // textBoxFWVersion
            // 
            this.textBoxFWVersion.Location = new System.Drawing.Point(177, 7);
            this.textBoxFWVersion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxFWVersion.Name = "textBoxFWVersion";
            this.textBoxFWVersion.ReadOnly = true;
            this.textBoxFWVersion.Size = new System.Drawing.Size(215, 22);
            this.textBoxFWVersion.TabIndex = 136;
            // 
            // textBoxFWSubversion
            // 
            this.textBoxFWSubversion.Location = new System.Drawing.Point(177, 41);
            this.textBoxFWSubversion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxFWSubversion.Name = "textBoxFWSubversion";
            this.textBoxFWSubversion.ReadOnly = true;
            this.textBoxFWSubversion.Size = new System.Drawing.Size(215, 22);
            this.textBoxFWSubversion.TabIndex = 140;
            // 
            // textBoxFPGAVersion
            // 
            this.textBoxFPGAVersion.Location = new System.Drawing.Point(177, 74);
            this.textBoxFPGAVersion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxFPGAVersion.Name = "textBoxFPGAVersion";
            this.textBoxFPGAVersion.ReadOnly = true;
            this.textBoxFPGAVersion.Size = new System.Drawing.Size(215, 22);
            this.textBoxFPGAVersion.TabIndex = 141;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(52, 112);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(98, 17);
            this.label21.TabIndex = 142;
            this.label21.Text = "Serial Number";
            // 
            // textBoxSerialNum
            // 
            this.textBoxSerialNum.Location = new System.Drawing.Point(177, 107);
            this.textBoxSerialNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSerialNum.Name = "textBoxSerialNum";
            this.textBoxSerialNum.ReadOnly = true;
            this.textBoxSerialNum.Size = new System.Drawing.Size(215, 22);
            this.textBoxSerialNum.TabIndex = 143;
            // 
            // checkBoxEnableFileNameAutoUpdate
            // 
            this.checkBoxEnableFileNameAutoUpdate.AutoSize = true;
            this.checkBoxEnableFileNameAutoUpdate.Location = new System.Drawing.Point(171, 20);
            this.checkBoxEnableFileNameAutoUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxEnableFileNameAutoUpdate.Name = "checkBoxEnableFileNameAutoUpdate";
            this.checkBoxEnableFileNameAutoUpdate.Size = new System.Drawing.Size(203, 21);
            this.checkBoxEnableFileNameAutoUpdate.TabIndex = 204;
            this.checkBoxEnableFileNameAutoUpdate.Text = "Auto-update save file name";
            this.checkBoxEnableFileNameAutoUpdate.UseVisualStyleBackColor = true;
            this.checkBoxEnableFileNameAutoUpdate.CheckedChanged += new System.EventHandler(this.checkBoxEnableFileNameAutoUpdate_CheckedChanged);
            // 
            // AcquisitionParametersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlAcquisitionParameters);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AcquisitionParametersControl";
            this.Size = new System.Drawing.Size(425, 373);
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
    }
}
