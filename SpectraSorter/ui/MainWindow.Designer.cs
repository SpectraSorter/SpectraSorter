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

using System.Drawing;

namespace spectra.ui
{
    partial class MainWindow
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
        /// Required method for Designer mSupport - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.dataGridViewIPDevices = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewUSBDeviceList = new System.Windows.Forms.DataGridView();
            this.ColumnDeviceDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonUSBRescan = new System.Windows.Forms.Button();
            this.checkBoxLampEnableDark = new System.Windows.Forms.CheckBox();
            this.labelFileSaveError = new System.Windows.Forms.Label();
            this.buttonStartAcquisition = new System.Windows.Forms.Button();
            this.buttonAbortAcquisition = new System.Windows.Forms.Button();
            this.textBoxNumInBuffer = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.buttonUpdateSpectraInBuffer = new System.Windows.Forms.Button();
            this.buttonUSBConnect = new System.Windows.Forms.Button();
            this.buttonTakeDark = new System.Windows.Forms.Button();
            this.labelDarkStatus = new System.Windows.Forms.Label();
            this.labelReferenceStatus = new System.Windows.Forms.Label();
            this.buttonTakeReference = new System.Windows.Forms.Button();
            this.buttonClearBuffer = new System.Windows.Forms.Button();
            this.buttonClearDark = new System.Windows.Forms.Button();
            this.buttonClearReference = new System.Windows.Forms.Button();
            this.checkBoxClearBufferBeforeAcquisition = new System.Windows.Forms.CheckBox();
            this.groupBoxDark = new System.Windows.Forms.GroupBox();
            this.dataGridViewArduinoDevices = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonConnectToArduino = new System.Windows.Forms.Button();
            this.buttonRescanArduino = new System.Windows.Forms.Button();
            this.buttonDisconnectFromArduino = new System.Windows.Forms.Button();
            this.labelAccumulateSpectraSlider = new System.Windows.Forms.Label();
            this.buttonUSBDisconnect = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.importSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.revertSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wavelengthsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wavelengthHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enabledFilteringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enabledTriggeringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.referenceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.doNotUseReferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dynamicReferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.outputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rawSpectrumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkCorrectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.absorbanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transmissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableSaveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.startToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spectrumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeSeriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.darkSpectrumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.referenceSpectrumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.correctedReferenceSpectrumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.accumulatedSpectraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accumulatedTimeSeriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.showThresholdsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTriggerPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoScaleYAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonIPDisconnect = new System.Windows.Forms.Button();
            this.buttonIPConnect = new System.Windows.Forms.Button();
            this.buttonIPRescan = new System.Windows.Forms.Button();
            this.labelCurSpectraPerSec = new System.Windows.Forms.Label();
            this.buttonAbortAccumulateSpectra = new System.Windows.Forms.Button();
            this.buttonAccumulateSpectra = new System.Windows.Forms.Button();
            this.groupBoxCorrectedReference = new System.Windows.Forms.GroupBox();
            this.labelCorrectedReferenceSpectrumType = new System.Windows.Forms.Label();
            this.labelCorrectedReferenceSpectrumStatus = new System.Windows.Forms.Label();
            this.toolStripOceanFXUSBStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripOceanFXIPStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripArduinoStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusSystemTimeAccuracy = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripToolbar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusFilteringLabel = new System.Windows.Forms.ToolStripDropDownButton();
            this.onToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusThresholdingLabel = new System.Windows.Forms.ToolStripDropDownButton();
            this.onToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusReferenceLabel = new System.Windows.Forms.ToolStripDropDownButton();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staticToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dynamicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusOutput = new System.Windows.Forms.ToolStripDropDownButton();
            this.rawSpectrumToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.darkCorrectedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.absorbanceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.transmissionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusSavingLabel = new System.Windows.Forms.ToolStripDropDownButton();
            this.onToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.offToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.labelTotalHits = new System.Windows.Forms.Label();
            this.labelPlotType = new System.Windows.Forms.Label();
            this.tabControlAcquisition = new System.Windows.Forms.TabControl();
            this.tabPageAcq = new System.Windows.Forms.TabPage();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.comboBoxAcquisitionOutput = new System.Windows.Forms.ComboBox();
            this.acquisitionParametersControl = new spectra.ui.components.AcquisitionParametersControl();
            this.tabPageRef = new System.Windows.Forms.TabPage();
            this.groupBoxReference = new System.Windows.Forms.GroupBox();
            this.tabControlReferenceType = new System.Windows.Forms.TabControl();
            this.tabPageSingle = new System.Windows.Forms.TabPage();
            this.tabPageStatic = new System.Windows.Forms.TabPage();
            this.buttonClearReferenceFromStatic = new System.Windows.Forms.Button();
            this.groupBoxGenerateStaticReferenceSpectrum = new System.Windows.Forms.GroupBox();
            this.labelEstimateReferenceSpectrumExplanation = new System.Windows.Forms.Label();
            this.buttonGenerateReferenceFromAccumulatedSpectra = new System.Windows.Forms.Button();
            this.labelEstimateReferenceSpectrumMax = new System.Windows.Forms.Label();
            this.textBoxEstimateReferenceSpectrumMax = new System.Windows.Forms.TextBox();
            this.labelEstimateReferenceSpectrumMin = new System.Windows.Forms.Label();
            this.textBoxEstimateReferenceSpectrumMin = new System.Windows.Forms.TextBox();
            this.groupBoxAccumulateStaticReferenceSpectrum = new System.Windows.Forms.GroupBox();
            this.labelAcquireForSeconds = new System.Windows.Forms.Label();
            this.trackBarAccumulateSpectraSlider = new System.Windows.Forms.TrackBar();
            this.textBoxAcquireFor = new System.Windows.Forms.TextBox();
            this.labelAcquireFor = new System.Windows.Forms.Label();
            this.tabPageDynamic = new System.Windows.Forms.TabPage();
            this.labelDynamicReferenceExplanation = new System.Windows.Forms.Label();
            this.labelSpectrumIntervalBetweenGeneration = new System.Windows.Forms.Label();
            this.textBoxSpectrumIntervalBetweenGeneration = new System.Windows.Forms.TextBox();
            this.labelNumberOfSpectramToAccumulate = new System.Windows.Forms.Label();
            this.textBoxNumberOfSpectramToAccumulate = new System.Windows.Forms.TextBox();
            this.labelDynamicReferenceExplanation_More = new System.Windows.Forms.Label();
            this.tabPageProc = new System.Windows.Forms.TabPage();
            this.processingControl = new spectra.ui.components.ProcessingControl();
            this.tabPagePlot = new System.Windows.Forms.TabPage();
            this.plotOptionsControl = new spectra.ui.components.PlotOptionsControl();
            this.tabPageArduino = new System.Windows.Forms.TabPage();
            this.arduinoParametersControl = new spectra.ui.components.ArduinoParametersControl();
            this.groupBoxAcqStatistics = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelComputedSpectra = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.labelSavedSpectra = new System.Windows.Forms.Label();
            this.labelTotalTime = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.labelTotalSpectra = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.labelSavedBytes = new System.Windows.Forms.Label();
            this.labelSpectraPerSec = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.labelSpectraPerRequest = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelTotalRequests = new System.Windows.Forms.Label();
            this.labelTotalBytes = new System.Windows.Forms.Label();
            this.mainChart = new spectra.ui.components.MainChart();
            this.groupBoxOceanFXUSB = new System.Windows.Forms.GroupBox();
            this.groupBoxOceanFXNetwork = new System.Windows.Forms.GroupBox();
            this.groupBoxArduinoCOM = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIPDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUSBDeviceList)).BeginInit();
            this.groupBoxDark.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewArduinoDevices)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.groupBoxCorrectedReference.SuspendLayout();
            this.statusStripToolbar.SuspendLayout();
            this.tabControlAcquisition.SuspendLayout();
            this.tabPageAcq.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            this.tabPageRef.SuspendLayout();
            this.groupBoxReference.SuspendLayout();
            this.tabControlReferenceType.SuspendLayout();
            this.tabPageSingle.SuspendLayout();
            this.tabPageStatic.SuspendLayout();
            this.groupBoxGenerateStaticReferenceSpectrum.SuspendLayout();
            this.groupBoxAccumulateStaticReferenceSpectrum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAccumulateSpectraSlider)).BeginInit();
            this.tabPageDynamic.SuspendLayout();
            this.tabPageProc.SuspendLayout();
            this.tabPagePlot.SuspendLayout();
            this.tabPageArduino.SuspendLayout();
            this.groupBoxAcqStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).BeginInit();
            this.groupBoxOceanFXUSB.SuspendLayout();
            this.groupBoxOceanFXNetwork.SuspendLayout();
            this.groupBoxArduinoCOM.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewIPDevices
            // 
            this.dataGridViewIPDevices.AllowUserToAddRows = false;
            this.dataGridViewIPDevices.AllowUserToDeleteRows = false;
            this.dataGridViewIPDevices.AllowUserToResizeRows = false;
            this.dataGridViewIPDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIPDevices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.Port,
            this.dataGridViewTextBoxColumn3});
            this.dataGridViewIPDevices.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewIPDevices.Location = new System.Drawing.Point(6, 21);
            this.dataGridViewIPDevices.MultiSelect = false;
            this.dataGridViewIPDevices.Name = "dataGridViewIPDevices";
            this.dataGridViewIPDevices.ReadOnly = true;
            this.dataGridViewIPDevices.RowHeadersVisible = false;
            this.dataGridViewIPDevices.RowHeadersWidth = 51;
            this.dataGridViewIPDevices.RowTemplate.ReadOnly = true;
            this.dataGridViewIPDevices.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewIPDevices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewIPDevices.Size = new System.Drawing.Size(237, 86);
            this.dataGridViewIPDevices.TabIndex = 4;
            this.dataGridViewIPDevices.SelectionChanged += new System.EventHandler(this.dataGridViewIPDevices_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 125F;
            this.dataGridViewTextBoxColumn2.HeaderText = "IP Address";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 256;
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 95;
            // 
            // Port
            // 
            this.Port.HeaderText = "Port";
            this.Port.MaxInputLength = 256;
            this.Port.MinimumWidth = 6;
            this.Port.Name = "Port";
            this.Port.ReadOnly = true;
            this.Port.Width = 60;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 115F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Serial Num";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 256;
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 115;
            // 
            // dataGridViewUSBDeviceList
            // 
            this.dataGridViewUSBDeviceList.AllowUserToAddRows = false;
            this.dataGridViewUSBDeviceList.AllowUserToDeleteRows = false;
            this.dataGridViewUSBDeviceList.AllowUserToResizeRows = false;
            this.dataGridViewUSBDeviceList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewUSBDeviceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUSBDeviceList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDeviceDescription});
            this.dataGridViewUSBDeviceList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewUSBDeviceList.Location = new System.Drawing.Point(6, 21);
            this.dataGridViewUSBDeviceList.MultiSelect = false;
            this.dataGridViewUSBDeviceList.Name = "dataGridViewUSBDeviceList";
            this.dataGridViewUSBDeviceList.ReadOnly = true;
            this.dataGridViewUSBDeviceList.RowHeadersVisible = false;
            this.dataGridViewUSBDeviceList.RowHeadersWidth = 51;
            this.dataGridViewUSBDeviceList.RowTemplate.ReadOnly = true;
            this.dataGridViewUSBDeviceList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewUSBDeviceList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUSBDeviceList.Size = new System.Drawing.Size(237, 86);
            this.dataGridViewUSBDeviceList.TabIndex = 1;
            this.dataGridViewUSBDeviceList.SelectionChanged += new System.EventHandler(this.dataGridViewUSBDeviceList_SelectionChanged);
            // 
            // ColumnDeviceDescription
            // 
            this.ColumnDeviceDescription.FillWeight = 225F;
            this.ColumnDeviceDescription.HeaderText = "Description";
            this.ColumnDeviceDescription.MinimumWidth = 6;
            this.ColumnDeviceDescription.Name = "ColumnDeviceDescription";
            this.ColumnDeviceDescription.ReadOnly = true;
            // 
            // buttonUSBRescan
            // 
            this.buttonUSBRescan.Enabled = false;
            this.buttonUSBRescan.Location = new System.Drawing.Point(249, 21);
            this.buttonUSBRescan.Name = "buttonUSBRescan";
            this.buttonUSBRescan.Size = new System.Drawing.Size(72, 23);
            this.buttonUSBRescan.TabIndex = 2;
            this.buttonUSBRescan.Text = "Rescan";
            this.buttonUSBRescan.UseVisualStyleBackColor = true;
            this.buttonUSBRescan.Click += new System.EventHandler(this.buttonUSBRescan_Click);
            // 
            // checkBoxLampEnableDark
            // 
            this.checkBoxLampEnableDark.AutoSize = true;
            this.checkBoxLampEnableDark.Location = new System.Drawing.Point(6, 7);
            this.checkBoxLampEnableDark.Name = "checkBoxLampEnableDark";
            this.checkBoxLampEnableDark.Size = new System.Drawing.Size(91, 17);
            this.checkBoxLampEnableDark.TabIndex = 37;
            this.checkBoxLampEnableDark.Text = "Enable Lamp";
            this.checkBoxLampEnableDark.UseVisualStyleBackColor = true;
            this.checkBoxLampEnableDark.CheckedChanged += new System.EventHandler(this.checkBoxLampEnable_CheckedChanged);
            // 
            // labelFileSaveError
            // 
            this.labelFileSaveError.AutoSize = true;
            this.labelFileSaveError.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFileSaveError.ForeColor = System.Drawing.Color.Crimson;
            this.labelFileSaveError.Location = new System.Drawing.Point(12, 144);
            this.labelFileSaveError.Name = "labelFileSaveError";
            this.labelFileSaveError.Size = new System.Drawing.Size(95, 13);
            this.labelFileSaveError.TabIndex = 121;
            this.labelFileSaveError.Text = "* File save error *";
            this.labelFileSaveError.Visible = false;
            // 
            // buttonStartAcquisition
            // 
            this.buttonStartAcquisition.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonStartAcquisition.Enabled = false;
            this.buttonStartAcquisition.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartAcquisition.ForeColor = System.Drawing.Color.White;
            this.buttonStartAcquisition.Location = new System.Drawing.Point(1014, 26);
            this.buttonStartAcquisition.Name = "buttonStartAcquisition";
            this.buttonStartAcquisition.Size = new System.Drawing.Size(332, 57);
            this.buttonStartAcquisition.TabIndex = 128;
            this.buttonStartAcquisition.Text = "Start Acquisition";
            this.buttonStartAcquisition.UseVisualStyleBackColor = false;
            this.buttonStartAcquisition.Click += new System.EventHandler(this.buttonStartAcquisition_Click);
            // 
            // buttonAbortAcquisition
            // 
            this.buttonAbortAcquisition.BackColor = System.Drawing.Color.Maroon;
            this.buttonAbortAcquisition.Enabled = false;
            this.buttonAbortAcquisition.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAbortAcquisition.ForeColor = System.Drawing.Color.White;
            this.buttonAbortAcquisition.Location = new System.Drawing.Point(1014, 82);
            this.buttonAbortAcquisition.Name = "buttonAbortAcquisition";
            this.buttonAbortAcquisition.Size = new System.Drawing.Size(332, 57);
            this.buttonAbortAcquisition.TabIndex = 129;
            this.buttonAbortAcquisition.Text = "Abort Acquisition";
            this.buttonAbortAcquisition.UseVisualStyleBackColor = false;
            this.buttonAbortAcquisition.Click += new System.EventHandler(this.buttonAbortAcquisition_Click);
            // 
            // textBoxNumInBuffer
            // 
            this.textBoxNumInBuffer.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.textBoxNumInBuffer.Location = new System.Drawing.Point(108, 742);
            this.textBoxNumInBuffer.Name = "textBoxNumInBuffer";
            this.textBoxNumInBuffer.ReadOnly = true;
            this.textBoxNumInBuffer.Size = new System.Drawing.Size(61, 22);
            this.textBoxNumInBuffer.TabIndex = 136;
            this.textBoxNumInBuffer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxNumInBuffer.TextChanged += new System.EventHandler(this.textBoxNumInBuffer_TextChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(6, 747);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(93, 13);
            this.label26.TabIndex = 135;
            this.label26.Text = "Spectra in Buffer";
            // 
            // buttonUpdateSpectraInBuffer
            // 
            this.buttonUpdateSpectraInBuffer.Enabled = false;
            this.buttonUpdateSpectraInBuffer.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.buttonUpdateSpectraInBuffer.Location = new System.Drawing.Point(175, 742);
            this.buttonUpdateSpectraInBuffer.Name = "buttonUpdateSpectraInBuffer";
            this.buttonUpdateSpectraInBuffer.Size = new System.Drawing.Size(70, 23);
            this.buttonUpdateSpectraInBuffer.TabIndex = 137;
            this.buttonUpdateSpectraInBuffer.Text = "Update";
            this.buttonUpdateSpectraInBuffer.UseVisualStyleBackColor = true;
            this.buttonUpdateSpectraInBuffer.Click += new System.EventHandler(this.buttonUpdateSpectraInBuffer_Click);
            // 
            // buttonUSBConnect
            // 
            this.buttonUSBConnect.Location = new System.Drawing.Point(249, 52);
            this.buttonUSBConnect.Name = "buttonUSBConnect";
            this.buttonUSBConnect.Size = new System.Drawing.Size(72, 23);
            this.buttonUSBConnect.TabIndex = 3;
            this.buttonUSBConnect.Text = "Connect";
            this.buttonUSBConnect.UseVisualStyleBackColor = true;
            this.buttonUSBConnect.Click += new System.EventHandler(this.buttonUSBConnect_Click);
            // 
            // buttonTakeDark
            // 
            this.buttonTakeDark.Enabled = false;
            this.buttonTakeDark.Location = new System.Drawing.Point(10, 19);
            this.buttonTakeDark.Name = "buttonTakeDark";
            this.buttonTakeDark.Size = new System.Drawing.Size(96, 23);
            this.buttonTakeDark.TabIndex = 140;
            this.buttonTakeDark.Text = "Take Dark";
            this.buttonTakeDark.UseVisualStyleBackColor = true;
            this.buttonTakeDark.Click += new System.EventHandler(this.buttonTakeDark_Click);
            // 
            // labelDarkStatus
            // 
            this.labelDarkStatus.AutoSize = true;
            this.labelDarkStatus.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelDarkStatus.Location = new System.Drawing.Point(135, 24);
            this.labelDarkStatus.Name = "labelDarkStatus";
            this.labelDarkStatus.Size = new System.Drawing.Size(49, 13);
            this.labelDarkStatus.TabIndex = 144;
            this.labelDarkStatus.Text = "00:00:00";
            this.labelDarkStatus.Visible = false;
            // 
            // labelReferenceStatus
            // 
            this.labelReferenceStatus.AutoSize = true;
            this.labelReferenceStatus.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelReferenceStatus.Location = new System.Drawing.Point(122, 11);
            this.labelReferenceStatus.Name = "labelReferenceStatus";
            this.labelReferenceStatus.Size = new System.Drawing.Size(49, 13);
            this.labelReferenceStatus.TabIndex = 147;
            this.labelReferenceStatus.Text = "00:00:00";
            this.labelReferenceStatus.Visible = false;
            // 
            // buttonTakeReference
            // 
            this.buttonTakeReference.Enabled = false;
            this.buttonTakeReference.Location = new System.Drawing.Point(6, 6);
            this.buttonTakeReference.Name = "buttonTakeReference";
            this.buttonTakeReference.Size = new System.Drawing.Size(96, 23);
            this.buttonTakeReference.TabIndex = 146;
            this.buttonTakeReference.Text = "Take Reference";
            this.buttonTakeReference.UseVisualStyleBackColor = true;
            this.buttonTakeReference.Click += new System.EventHandler(this.buttonTakeReference_Click);
            // 
            // buttonClearBuffer
            // 
            this.buttonClearBuffer.Enabled = false;
            this.buttonClearBuffer.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.buttonClearBuffer.Location = new System.Drawing.Point(251, 742);
            this.buttonClearBuffer.Name = "buttonClearBuffer";
            this.buttonClearBuffer.Size = new System.Drawing.Size(74, 23);
            this.buttonClearBuffer.TabIndex = 148;
            this.buttonClearBuffer.Text = "Clear";
            this.buttonClearBuffer.UseVisualStyleBackColor = true;
            this.buttonClearBuffer.Click += new System.EventHandler(this.buttonClearBuffer_Click);
            // 
            // buttonClearDark
            // 
            this.buttonClearDark.Enabled = false;
            this.buttonClearDark.Location = new System.Drawing.Point(213, 19);
            this.buttonClearDark.Name = "buttonClearDark";
            this.buttonClearDark.Size = new System.Drawing.Size(96, 23);
            this.buttonClearDark.TabIndex = 149;
            this.buttonClearDark.Text = "Clear Dark";
            this.buttonClearDark.UseVisualStyleBackColor = true;
            this.buttonClearDark.Click += new System.EventHandler(this.buttonClearDark_Click);
            // 
            // buttonClearReference
            // 
            this.buttonClearReference.Enabled = false;
            this.buttonClearReference.Location = new System.Drawing.Point(191, 6);
            this.buttonClearReference.Name = "buttonClearReference";
            this.buttonClearReference.Size = new System.Drawing.Size(96, 23);
            this.buttonClearReference.TabIndex = 150;
            this.buttonClearReference.Text = "Clear Reference";
            this.buttonClearReference.UseVisualStyleBackColor = true;
            this.buttonClearReference.Click += new System.EventHandler(this.buttonClearReference_Click);
            // 
            // checkBoxClearBufferBeforeAcquisition
            // 
            this.checkBoxClearBufferBeforeAcquisition.AutoSize = true;
            this.checkBoxClearBufferBeforeAcquisition.Checked = true;
            this.checkBoxClearBufferBeforeAcquisition.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxClearBufferBeforeAcquisition.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxClearBufferBeforeAcquisition.Location = new System.Drawing.Point(137, 7);
            this.checkBoxClearBufferBeforeAcquisition.Name = "checkBoxClearBufferBeforeAcquisition";
            this.checkBoxClearBufferBeforeAcquisition.Size = new System.Drawing.Size(185, 17);
            this.checkBoxClearBufferBeforeAcquisition.TabIndex = 142;
            this.checkBoxClearBufferBeforeAcquisition.Text = "Clear Buffer Before Acquisition";
            this.checkBoxClearBufferBeforeAcquisition.UseVisualStyleBackColor = true;
            this.checkBoxClearBufferBeforeAcquisition.CheckedChanged += new System.EventHandler(this.checkBoxClearBufferBeforeAcquisition_CheckedChanged);
            // 
            // groupBoxDark
            // 
            this.groupBoxDark.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxDark.Controls.Add(this.buttonTakeDark);
            this.groupBoxDark.Controls.Add(this.labelDarkStatus);
            this.groupBoxDark.Controls.Add(this.buttonClearDark);
            this.groupBoxDark.Location = new System.Drawing.Point(6, 31);
            this.groupBoxDark.Name = "groupBoxDark";
            this.groupBoxDark.Size = new System.Drawing.Size(316, 52);
            this.groupBoxDark.TabIndex = 169;
            this.groupBoxDark.TabStop = false;
            this.groupBoxDark.Text = "Dark spectrum";
            // 
            // dataGridViewArduinoDevices
            // 
            this.dataGridViewArduinoDevices.AllowUserToAddRows = false;
            this.dataGridViewArduinoDevices.AllowUserToDeleteRows = false;
            this.dataGridViewArduinoDevices.AllowUserToResizeRows = false;
            this.dataGridViewArduinoDevices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewArduinoDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewArduinoDevices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.dataGridViewArduinoDevices.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewArduinoDevices.Location = new System.Drawing.Point(6, 21);
            this.dataGridViewArduinoDevices.MultiSelect = false;
            this.dataGridViewArduinoDevices.Name = "dataGridViewArduinoDevices";
            this.dataGridViewArduinoDevices.ReadOnly = true;
            this.dataGridViewArduinoDevices.RowHeadersVisible = false;
            this.dataGridViewArduinoDevices.RowHeadersWidth = 51;
            this.dataGridViewArduinoDevices.RowTemplate.ReadOnly = true;
            this.dataGridViewArduinoDevices.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewArduinoDevices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewArduinoDevices.Size = new System.Drawing.Size(237, 85);
            this.dataGridViewArduinoDevices.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 225F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Description";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // buttonConnectToArduino
            // 
            this.buttonConnectToArduino.Location = new System.Drawing.Point(249, 52);
            this.buttonConnectToArduino.Name = "buttonConnectToArduino";
            this.buttonConnectToArduino.Size = new System.Drawing.Size(72, 23);
            this.buttonConnectToArduino.TabIndex = 8;
            this.buttonConnectToArduino.Text = "Connect";
            this.buttonConnectToArduino.Click += new System.EventHandler(this.buttonConnectToArduino_Click);
            // 
            // buttonRescanArduino
            // 
            this.buttonRescanArduino.Location = new System.Drawing.Point(249, 21);
            this.buttonRescanArduino.Name = "buttonRescanArduino";
            this.buttonRescanArduino.Size = new System.Drawing.Size(72, 23);
            this.buttonRescanArduino.TabIndex = 7;
            this.buttonRescanArduino.Text = "Rescan";
            this.buttonRescanArduino.UseVisualStyleBackColor = true;
            this.buttonRescanArduino.Click += new System.EventHandler(this.buttonRescanArduino_Click);
            // 
            // buttonDisconnectFromArduino
            // 
            this.buttonDisconnectFromArduino.Enabled = false;
            this.buttonDisconnectFromArduino.Location = new System.Drawing.Point(249, 83);
            this.buttonDisconnectFromArduino.Name = "buttonDisconnectFromArduino";
            this.buttonDisconnectFromArduino.Size = new System.Drawing.Size(72, 23);
            this.buttonDisconnectFromArduino.TabIndex = 9;
            this.buttonDisconnectFromArduino.Text = "Disconnect";
            this.buttonDisconnectFromArduino.Click += new System.EventHandler(this.buttonDisconnectFromArduino_Click);
            // 
            // labelAccumulateSpectraSlider
            // 
            this.labelAccumulateSpectraSlider.Enabled = false;
            this.labelAccumulateSpectraSlider.Location = new System.Drawing.Point(10, 79);
            this.labelAccumulateSpectraSlider.Name = "labelAccumulateSpectraSlider";
            this.labelAccumulateSpectraSlider.Size = new System.Drawing.Size(271, 19);
            this.labelAccumulateSpectraSlider.TabIndex = 197;
            this.labelAccumulateSpectraSlider.Text = "0";
            this.labelAccumulateSpectraSlider.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonUSBDisconnect
            // 
            this.buttonUSBDisconnect.Enabled = false;
            this.buttonUSBDisconnect.Location = new System.Drawing.Point(249, 83);
            this.buttonUSBDisconnect.Name = "buttonUSBDisconnect";
            this.buttonUSBDisconnect.Size = new System.Drawing.Size(72, 23);
            this.buttonUSBDisconnect.TabIndex = 198;
            this.buttonUSBDisconnect.Text = "Disconnect";
            this.buttonUSBDisconnect.UseVisualStyleBackColor = true;
            this.buttonUSBDisconnect.Click += new System.EventHandler(this.ButtonUSBDisconnect_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.wavelengthsToolStripMenuItem,
            this.processToolStripMenuItem,
            this.plotToolStripMenuItem,
            this.toolStripHelpMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1353, 24);
            this.menuStrip.TabIndex = 209;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSettings,
            this.toolStripSeparator4,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripSettings
            // 
            this.toolStripSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importSettingsToolStripMenuItem,
            this.exportSettingsToolStripMenuItem,
            this.toolStripSeparator5,
            this.saveSettingsToolStripMenuItem,
            this.revertSettingsToolStripMenuItem});
            this.toolStripSettings.Name = "toolStripSettings";
            this.toolStripSettings.Size = new System.Drawing.Size(116, 22);
            this.toolStripSettings.Text = "Settings";
            // 
            // importSettingsToolStripMenuItem
            // 
            this.importSettingsToolStripMenuItem.Name = "importSettingsToolStripMenuItem";
            this.importSettingsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.importSettingsToolStripMenuItem.Text = "Import from file";
            this.importSettingsToolStripMenuItem.Click += new System.EventHandler(this.importSettingsToolStripMenuItem_Click);
            // 
            // exportSettingsToolStripMenuItem
            // 
            this.exportSettingsToolStripMenuItem.Name = "exportSettingsToolStripMenuItem";
            this.exportSettingsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.exportSettingsToolStripMenuItem.Text = "Export to file";
            this.exportSettingsToolStripMenuItem.Click += new System.EventHandler(this.exportSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(158, 6);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.saveSettingsToolStripMenuItem.Text = "Store as default";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // revertSettingsToolStripMenuItem
            // 
            this.revertSettingsToolStripMenuItem.Name = "revertSettingsToolStripMenuItem";
            this.revertSettingsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.revertSettingsToolStripMenuItem.Text = "Revert to default";
            this.revertSettingsToolStripMenuItem.Click += new System.EventHandler(this.revertSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(113, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            // 
            // wavelengthsToolStripMenuItem
            // 
            this.wavelengthsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wavelengthHubToolStripMenuItem});
            this.wavelengthsToolStripMenuItem.Name = "wavelengthsToolStripMenuItem";
            this.wavelengthsToolStripMenuItem.Size = new System.Drawing.Size(25, 20);
            this.wavelengthsToolStripMenuItem.Text = "λ";
            // 
            // wavelengthHubToolStripMenuItem
            // 
            this.wavelengthHubToolStripMenuItem.Name = "wavelengthHubToolStripMenuItem";
            this.wavelengthHubToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.wavelengthHubToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.wavelengthHubToolStripMenuItem.Text = "Hub";
            this.wavelengthHubToolStripMenuItem.Click += new System.EventHandler(this.managerToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enabledFilteringToolStripMenuItem,
            this.enabledTriggeringToolStripMenuItem,
            this.referenceToolStripMenuItem1,
            this.toolStripSeparator1,
            this.outputToolStripMenuItem,
            this.enableSaveToFileToolStripMenuItem,
            this.toolStripSeparator2,
            this.startToolStripMenuItem2});
            this.processToolStripMenuItem.Enabled = false;
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.processToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.processToolStripMenuItem.Text = "Process";
            // 
            // enabledFilteringToolStripMenuItem
            // 
            this.enabledFilteringToolStripMenuItem.Name = "enabledFilteringToolStripMenuItem";
            this.enabledFilteringToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.enabledFilteringToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.enabledFilteringToolStripMenuItem.Text = "Filtering";
            this.enabledFilteringToolStripMenuItem.Click += new System.EventHandler(this.enabledFilteringToolStripMenuItem_Click);
            // 
            // enabledTriggeringToolStripMenuItem
            // 
            this.enabledTriggeringToolStripMenuItem.Name = "enabledTriggeringToolStripMenuItem";
            this.enabledTriggeringToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.enabledTriggeringToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.enabledTriggeringToolStripMenuItem.Text = "Triggering";
            this.enabledTriggeringToolStripMenuItem.Click += new System.EventHandler(this.enabledTriggeringToolStripMenuItem_Click);
            // 
            // referenceToolStripMenuItem1
            // 
            this.referenceToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doNotUseReferenceToolStripMenuItem,
            this.staticToolStripMenuItem,
            this.dynamicReferenceToolStripMenuItem});
            this.referenceToolStripMenuItem1.Name = "referenceToolStripMenuItem1";
            this.referenceToolStripMenuItem1.ShowShortcutKeys = false;
            this.referenceToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.referenceToolStripMenuItem1.Text = "Reference";
            // 
            // doNotUseReferenceToolStripMenuItem
            // 
            this.doNotUseReferenceToolStripMenuItem.Name = "doNotUseReferenceToolStripMenuItem";
            this.doNotUseReferenceToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.doNotUseReferenceToolStripMenuItem.Text = "None";
            this.doNotUseReferenceToolStripMenuItem.Click += new System.EventHandler(this.DoNotUseToolStripMenuItem_Click);
            // 
            // staticToolStripMenuItem
            // 
            this.staticToolStripMenuItem.Name = "staticToolStripMenuItem";
            this.staticToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.staticToolStripMenuItem.Text = "Static";
            this.staticToolStripMenuItem.Click += new System.EventHandler(this.StaticToolStripMenuItem_Click);
            // 
            // dynamicReferenceToolStripMenuItem
            // 
            this.dynamicReferenceToolStripMenuItem.Name = "dynamicReferenceToolStripMenuItem";
            this.dynamicReferenceToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.dynamicReferenceToolStripMenuItem.Text = "Dynamic";
            this.dynamicReferenceToolStripMenuItem.Click += new System.EventHandler(this.DynamicReferenceToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(167, 6);
            // 
            // outputToolStripMenuItem
            // 
            this.outputToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rawSpectrumToolStripMenuItem,
            this.darkCorrectedToolStripMenuItem,
            this.absorbanceToolStripMenuItem,
            this.transmissionToolStripMenuItem});
            this.outputToolStripMenuItem.Name = "outputToolStripMenuItem";
            this.outputToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.outputToolStripMenuItem.Text = "Output";
            // 
            // rawSpectrumToolStripMenuItem
            // 
            this.rawSpectrumToolStripMenuItem.Name = "rawSpectrumToolStripMenuItem";
            this.rawSpectrumToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.rawSpectrumToolStripMenuItem.Text = "Raw spectrum";
            this.rawSpectrumToolStripMenuItem.Click += new System.EventHandler(this.RawSpectrumToolStripMenuItem_Click);
            // 
            // darkCorrectedToolStripMenuItem
            // 
            this.darkCorrectedToolStripMenuItem.Name = "darkCorrectedToolStripMenuItem";
            this.darkCorrectedToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.darkCorrectedToolStripMenuItem.Text = "Dark corrected";
            this.darkCorrectedToolStripMenuItem.Click += new System.EventHandler(this.DarkCorrectedToolStripMenuItem_Click);
            // 
            // absorbanceToolStripMenuItem
            // 
            this.absorbanceToolStripMenuItem.Name = "absorbanceToolStripMenuItem";
            this.absorbanceToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.absorbanceToolStripMenuItem.Text = "Absorbance";
            this.absorbanceToolStripMenuItem.Click += new System.EventHandler(this.AbsorbanceToolStripMenuItem_Click);
            // 
            // transmissionToolStripMenuItem
            // 
            this.transmissionToolStripMenuItem.Name = "transmissionToolStripMenuItem";
            this.transmissionToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.transmissionToolStripMenuItem.Text = "Transmission";
            this.transmissionToolStripMenuItem.Click += new System.EventHandler(this.TransmissionToolStripMenuItem_Click);
            // 
            // enableSaveToFileToolStripMenuItem
            // 
            this.enableSaveToFileToolStripMenuItem.Name = "enableSaveToFileToolStripMenuItem";
            this.enableSaveToFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.enableSaveToFileToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.enableSaveToFileToolStripMenuItem.Text = "Saving";
            this.enableSaveToFileToolStripMenuItem.Click += new System.EventHandler(this.enableSaveToFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(167, 6);
            // 
            // startToolStripMenuItem2
            // 
            this.startToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.abortToolStripMenuItem});
            this.startToolStripMenuItem2.Name = "startToolStripMenuItem2";
            this.startToolStripMenuItem2.Size = new System.Drawing.Size(170, 22);
            this.startToolStripMenuItem2.Text = "Acquisition";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Enabled = false;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.startToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.StartToolStripMenuItem_Click);
            // 
            // abortToolStripMenuItem
            // 
            this.abortToolStripMenuItem.Enabled = false;
            this.abortToolStripMenuItem.Name = "abortToolStripMenuItem";
            this.abortToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.abortToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.abortToolStripMenuItem.Text = "Abort";
            this.abortToolStripMenuItem.Click += new System.EventHandler(this.AbortToolStripMenuItem_Click);
            // 
            // plotToolStripMenuItem
            // 
            this.plotToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spectrumToolStripMenuItem,
            this.timeSeriesToolStripMenuItem,
            this.toolStripSeparator9,
            this.darkSpectrumToolStripMenuItem,
            this.referenceSpectrumToolStripMenuItem,
            this.correctedReferenceSpectrumToolStripMenuItem,
            this.toolStripSeparator11,
            this.accumulatedSpectraToolStripMenuItem,
            this.accumulatedTimeSeriesToolStripMenuItem,
            this.toolStripSeparator3,
            this.showThresholdsToolStripMenuItem,
            this.showTriggerPointsToolStripMenuItem,
            this.autoScaleYAxisToolStripMenuItem});
            this.plotToolStripMenuItem.Enabled = false;
            this.plotToolStripMenuItem.Name = "plotToolStripMenuItem";
            this.plotToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.plotToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.plotToolStripMenuItem.Text = "Plot";
            // 
            // spectrumToolStripMenuItem
            // 
            this.spectrumToolStripMenuItem.Checked = true;
            this.spectrumToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.spectrumToolStripMenuItem.Name = "spectrumToolStripMenuItem";
            this.spectrumToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.spectrumToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.spectrumToolStripMenuItem.Text = "[Live] Output spectrum";
            this.spectrumToolStripMenuItem.Click += new System.EventHandler(this.SpectrumToolStripMenuItem_Click);
            // 
            // timeSeriesToolStripMenuItem
            // 
            this.timeSeriesToolStripMenuItem.Name = "timeSeriesToolStripMenuItem";
            this.timeSeriesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.timeSeriesToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.timeSeriesToolStripMenuItem.Text = "[Live] Time series";
            this.timeSeriesToolStripMenuItem.Click += new System.EventHandler(this.TimeSeriesToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(295, 6);
            // 
            // darkSpectrumToolStripMenuItem
            // 
            this.darkSpectrumToolStripMenuItem.Name = "darkSpectrumToolStripMenuItem";
            this.darkSpectrumToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.darkSpectrumToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.darkSpectrumToolStripMenuItem.Text = "Dark spectrum";
            this.darkSpectrumToolStripMenuItem.Click += new System.EventHandler(this.DarkSpectrumToolStripMenuItem_Click);
            // 
            // referenceSpectrumToolStripMenuItem
            // 
            this.referenceSpectrumToolStripMenuItem.Name = "referenceSpectrumToolStripMenuItem";
            this.referenceSpectrumToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.referenceSpectrumToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.referenceSpectrumToolStripMenuItem.Text = "Reference spectrum";
            this.referenceSpectrumToolStripMenuItem.Click += new System.EventHandler(this.ReferenceSpectrumToolStripMenuItem_Click);
            // 
            // correctedReferenceSpectrumToolStripMenuItem
            // 
            this.correctedReferenceSpectrumToolStripMenuItem.Name = "correctedReferenceSpectrumToolStripMenuItem";
            this.correctedReferenceSpectrumToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.correctedReferenceSpectrumToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.correctedReferenceSpectrumToolStripMenuItem.Text = "Dark-corrected reference spectrum";
            this.correctedReferenceSpectrumToolStripMenuItem.Click += new System.EventHandler(this.CorrectedReferenceSpectrumToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(295, 6);
            // 
            // accumulatedSpectraToolStripMenuItem
            // 
            this.accumulatedSpectraToolStripMenuItem.Name = "accumulatedSpectraToolStripMenuItem";
            this.accumulatedSpectraToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
            this.accumulatedSpectraToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.accumulatedSpectraToolStripMenuItem.Text = "Accumulated spectra";
            this.accumulatedSpectraToolStripMenuItem.Click += new System.EventHandler(this.AccumulatedSpectraToolStripMenuItem_Click);
            // 
            // accumulatedTimeSeriesToolStripMenuItem
            // 
            this.accumulatedTimeSeriesToolStripMenuItem.Name = "accumulatedTimeSeriesToolStripMenuItem";
            this.accumulatedTimeSeriesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D7)));
            this.accumulatedTimeSeriesToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.accumulatedTimeSeriesToolStripMenuItem.Text = "Accumulated time series";
            this.accumulatedTimeSeriesToolStripMenuItem.Click += new System.EventHandler(this.AccumulatedTimeSeriesToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(295, 6);
            // 
            // showThresholdsToolStripMenuItem
            // 
            this.showThresholdsToolStripMenuItem.Name = "showThresholdsToolStripMenuItem";
            this.showThresholdsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.showThresholdsToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.showThresholdsToolStripMenuItem.Text = "Show thresholds";
            this.showThresholdsToolStripMenuItem.Click += new System.EventHandler(this.ShowThresholdsToolStripMenuItem_Click);
            // 
            // showTriggerPointsToolStripMenuItem
            // 
            this.showTriggerPointsToolStripMenuItem.Name = "showTriggerPointsToolStripMenuItem";
            this.showTriggerPointsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.showTriggerPointsToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.showTriggerPointsToolStripMenuItem.Text = "Show trigger points";
            this.showTriggerPointsToolStripMenuItem.Click += new System.EventHandler(this.showTriggerPointsToolStripMenuItem_Click);
            // 
            // autoScaleYAxisToolStripMenuItem
            // 
            this.autoScaleYAxisToolStripMenuItem.Name = "autoScaleYAxisToolStripMenuItem";
            this.autoScaleYAxisToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.autoScaleYAxisToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.autoScaleYAxisToolStripMenuItem.Text = "Auto scale Y axis";
            this.autoScaleYAxisToolStripMenuItem.Click += new System.EventHandler(this.YAutoScaleToolStripMenuItem_Click);
            // 
            // toolStripHelpMenuItem
            // 
            this.toolStripHelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcutsToolStripMenuItem,
            this.toolStripSeparator8,
            this.aboutToolStripMenuItem});
            this.toolStripHelpMenuItem.Name = "toolStripHelpMenuItem";
            this.toolStripHelpMenuItem.ShowShortcutKeys = false;
            this.toolStripHelpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolStripHelpMenuItem.Text = "Help";
            // 
            // shortcutsToolStripMenuItem
            // 
            this.shortcutsToolStripMenuItem.Name = "shortcutsToolStripMenuItem";
            this.shortcutsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.shortcutsToolStripMenuItem.Text = "Shortcuts";
            this.shortcutsToolStripMenuItem.Click += new System.EventHandler(this.ShortcutsToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(121, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // buttonIPDisconnect
            // 
            this.buttonIPDisconnect.Enabled = false;
            this.buttonIPDisconnect.Location = new System.Drawing.Point(249, 83);
            this.buttonIPDisconnect.Name = "buttonIPDisconnect";
            this.buttonIPDisconnect.Size = new System.Drawing.Size(72, 23);
            this.buttonIPDisconnect.TabIndex = 213;
            this.buttonIPDisconnect.Text = "Disconnect";
            this.buttonIPDisconnect.UseVisualStyleBackColor = true;
            this.buttonIPDisconnect.Click += new System.EventHandler(this.ButtonIPDisconnect_Click);
            // 
            // buttonIPConnect
            // 
            this.buttonIPConnect.Location = new System.Drawing.Point(249, 52);
            this.buttonIPConnect.Name = "buttonIPConnect";
            this.buttonIPConnect.Size = new System.Drawing.Size(72, 23);
            this.buttonIPConnect.TabIndex = 211;
            this.buttonIPConnect.Text = "Connect";
            this.buttonIPConnect.UseVisualStyleBackColor = true;
            this.buttonIPConnect.Click += new System.EventHandler(this.ButtonIPConnect_Click);
            // 
            // buttonIPRescan
            // 
            this.buttonIPRescan.Enabled = false;
            this.buttonIPRescan.Location = new System.Drawing.Point(249, 21);
            this.buttonIPRescan.Name = "buttonIPRescan";
            this.buttonIPRescan.Size = new System.Drawing.Size(72, 23);
            this.buttonIPRescan.TabIndex = 210;
            this.buttonIPRescan.Text = "Rescan";
            this.buttonIPRescan.UseVisualStyleBackColor = true;
            this.buttonIPRescan.Click += new System.EventHandler(this.ButtonIPRescan_Click);
            // 
            // labelCurSpectraPerSec
            // 
            this.labelCurSpectraPerSec.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurSpectraPerSec.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelCurSpectraPerSec.Location = new System.Drawing.Point(345, 145);
            this.labelCurSpectraPerSec.Name = "labelCurSpectraPerSec";
            this.labelCurSpectraPerSec.Size = new System.Drawing.Size(337, 18);
            this.labelCurSpectraPerSec.TabIndex = 216;
            this.labelCurSpectraPerSec.Text = "0 spectra/sec";
            this.labelCurSpectraPerSec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonAbortAccumulateSpectra
            // 
            this.buttonAbortAccumulateSpectra.Enabled = false;
            this.buttonAbortAccumulateSpectra.Location = new System.Drawing.Point(148, 52);
            this.buttonAbortAccumulateSpectra.Name = "buttonAbortAccumulateSpectra";
            this.buttonAbortAccumulateSpectra.Size = new System.Drawing.Size(133, 23);
            this.buttonAbortAccumulateSpectra.TabIndex = 198;
            this.buttonAbortAccumulateSpectra.Text = "Stop";
            this.buttonAbortAccumulateSpectra.UseVisualStyleBackColor = true;
            this.buttonAbortAccumulateSpectra.Click += new System.EventHandler(this.buttonAbortAccumulateSpectra_Click);
            // 
            // buttonAccumulateSpectra
            // 
            this.buttonAccumulateSpectra.Enabled = false;
            this.buttonAccumulateSpectra.Location = new System.Drawing.Point(6, 52);
            this.buttonAccumulateSpectra.Name = "buttonAccumulateSpectra";
            this.buttonAccumulateSpectra.Size = new System.Drawing.Size(131, 23);
            this.buttonAccumulateSpectra.TabIndex = 146;
            this.buttonAccumulateSpectra.Text = "Accumulate";
            this.buttonAccumulateSpectra.UseVisualStyleBackColor = true;
            this.buttonAccumulateSpectra.Click += new System.EventHandler(this.AccumulateReferenceButton_Click);
            // 
            // groupBoxCorrectedReference
            // 
            this.groupBoxCorrectedReference.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxCorrectedReference.Controls.Add(this.labelCorrectedReferenceSpectrumType);
            this.groupBoxCorrectedReference.Controls.Add(this.labelCorrectedReferenceSpectrumStatus);
            this.groupBoxCorrectedReference.Location = new System.Drawing.Point(6, 485);
            this.groupBoxCorrectedReference.Name = "groupBoxCorrectedReference";
            this.groupBoxCorrectedReference.Size = new System.Drawing.Size(316, 51);
            this.groupBoxCorrectedReference.TabIndex = 220;
            this.groupBoxCorrectedReference.TabStop = false;
            this.groupBoxCorrectedReference.Text = "Corrected reference spectrum";
            // 
            // labelCorrectedReferenceSpectrumType
            // 
            this.labelCorrectedReferenceSpectrumType.AutoSize = true;
            this.labelCorrectedReferenceSpectrumType.Location = new System.Drawing.Point(198, 24);
            this.labelCorrectedReferenceSpectrumType.Name = "labelCorrectedReferenceSpectrumType";
            this.labelCorrectedReferenceSpectrumType.Size = new System.Drawing.Size(0, 13);
            this.labelCorrectedReferenceSpectrumType.TabIndex = 149;
            this.labelCorrectedReferenceSpectrumType.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelCorrectedReferenceSpectrumStatus
            // 
            this.labelCorrectedReferenceSpectrumStatus.AutoSize = true;
            this.labelCorrectedReferenceSpectrumStatus.Location = new System.Drawing.Point(7, 24);
            this.labelCorrectedReferenceSpectrumStatus.Name = "labelCorrectedReferenceSpectrumStatus";
            this.labelCorrectedReferenceSpectrumStatus.Size = new System.Drawing.Size(77, 13);
            this.labelCorrectedReferenceSpectrumStatus.TabIndex = 148;
            this.labelCorrectedReferenceSpectrumStatus.Text = "Not available.";
            // 
            // toolStripOceanFXUSBStatus
            // 
            this.toolStripOceanFXUSBStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripOceanFXUSBStatus.Image = global::spectra.Properties.Resources.oceanfx;
            this.toolStripOceanFXUSBStatus.Name = "toolStripOceanFXUSBStatus";
            this.toolStripOceanFXUSBStatus.Size = new System.Drawing.Size(193, 21);
            this.toolStripOceanFXUSBStatus.Text = "OceanFX (USB): Disconnected";
            // 
            // toolStripOceanFXIPStatus
            // 
            this.toolStripOceanFXIPStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripOceanFXIPStatus.Image = global::spectra.Properties.Resources.ip;
            this.toolStripOceanFXIPStatus.Name = "toolStripOceanFXIPStatus";
            this.toolStripOceanFXIPStatus.Size = new System.Drawing.Size(180, 21);
            this.toolStripOceanFXIPStatus.Text = "OceanFX (IP): Disconnected";
            // 
            // toolStripArduinoStatus
            // 
            this.toolStripArduinoStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripArduinoStatus.Image = global::spectra.Properties.Resources.arduino;
            this.toolStripArduinoStatus.Name = "toolStripArduinoStatus";
            this.toolStripArduinoStatus.Size = new System.Drawing.Size(153, 21);
            this.toolStripArduinoStatus.Text = "Arduino: Disconnected";
            // 
            // toolStripStatusSystemTimeAccuracy
            // 
            this.toolStripStatusSystemTimeAccuracy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusSystemTimeAccuracy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusSystemTimeAccuracy.Image")));
            this.toolStripStatusSystemTimeAccuracy.Name = "toolStripStatusSystemTimeAccuracy";
            this.toolStripStatusSystemTimeAccuracy.Size = new System.Drawing.Size(211, 20);
            this.toolStripStatusSystemTimeAccuracy.Text = "System timer accuracy: unknown";
            // 
            // statusStripToolbar
            // 
            this.statusStripToolbar.Enabled = false;
            this.statusStripToolbar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripOceanFXUSBStatus,
            this.toolStripOceanFXIPStatus,
            this.toolStripArduinoStatus,
            this.toolStripStatusFilteringLabel,
            this.toolStripStatusThresholdingLabel,
            this.toolStripStatusReferenceLabel,
            this.toolStripStatusOutput,
            this.toolStripStatusSavingLabel,
            this.toolStripStatusSystemTimeAccuracy,
            this.toolStripMenuItem1});
            this.statusStripToolbar.Location = new System.Drawing.Point(0, 1024);
            this.statusStripToolbar.Name = "statusStripToolbar";
            this.statusStripToolbar.Size = new System.Drawing.Size(1353, 26);
            this.statusStripToolbar.TabIndex = 214;
            this.statusStripToolbar.Text = "Toolbar";
            // 
            // toolStripStatusFilteringLabel
            // 
            this.toolStripStatusFilteringLabel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem,
            this.offToolStripMenuItem});
            this.toolStripStatusFilteringLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusFilteringLabel.Image = global::spectra.Properties.Resources.tool;
            this.toolStripStatusFilteringLabel.Name = "toolStripStatusFilteringLabel";
            this.toolStripStatusFilteringLabel.Size = new System.Drawing.Size(111, 24);
            this.toolStripStatusFilteringLabel.Text = "Filtering: Off";
            // 
            // onToolStripMenuItem
            // 
            this.onToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.onToolStripMenuItem.Name = "onToolStripMenuItem";
            this.onToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.onToolStripMenuItem.Text = "On";
            this.onToolStripMenuItem.Click += new System.EventHandler(this.OnToolStripMenuItem_Click);
            // 
            // offToolStripMenuItem
            // 
            this.offToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.offToolStripMenuItem.Name = "offToolStripMenuItem";
            this.offToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.offToolStripMenuItem.Text = "Off";
            this.offToolStripMenuItem.Click += new System.EventHandler(this.OffToolStripMenuItem_Click);
            // 
            // toolStripStatusThresholdingLabel
            // 
            this.toolStripStatusThresholdingLabel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem1,
            this.offToolStripMenuItem1});
            this.toolStripStatusThresholdingLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusThresholdingLabel.Image = global::spectra.Properties.Resources.tool;
            this.toolStripStatusThresholdingLabel.Name = "toolStripStatusThresholdingLabel";
            this.toolStripStatusThresholdingLabel.Size = new System.Drawing.Size(122, 24);
            this.toolStripStatusThresholdingLabel.Text = "Triggering: Off";
            // 
            // onToolStripMenuItem1
            // 
            this.onToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.onToolStripMenuItem1.Name = "onToolStripMenuItem1";
            this.onToolStripMenuItem1.Size = new System.Drawing.Size(91, 22);
            this.onToolStripMenuItem1.Text = "On";
            this.onToolStripMenuItem1.Click += new System.EventHandler(this.OnToolStripMenuItem1_Click);
            // 
            // offToolStripMenuItem1
            // 
            this.offToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.offToolStripMenuItem1.Name = "offToolStripMenuItem1";
            this.offToolStripMenuItem1.Size = new System.Drawing.Size(91, 22);
            this.offToolStripMenuItem1.Text = "Off";
            this.offToolStripMenuItem1.Click += new System.EventHandler(this.OffToolStripMenuItem1_Click);
            // 
            // toolStripStatusReferenceLabel
            // 
            this.toolStripStatusReferenceLabel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.staticToolStripMenuItem1,
            this.dynamicToolStripMenuItem});
            this.toolStripStatusReferenceLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusReferenceLabel.Image = global::spectra.Properties.Resources.reference;
            this.toolStripStatusReferenceLabel.Name = "toolStripStatusReferenceLabel";
            this.toolStripStatusReferenceLabel.Size = new System.Drawing.Size(133, 24);
            this.toolStripStatusReferenceLabel.Text = "Reference: none";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.NoneToolStripMenuItem_Click);
            // 
            // staticToolStripMenuItem1
            // 
            this.staticToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.staticToolStripMenuItem1.Name = "staticToolStripMenuItem1";
            this.staticToolStripMenuItem1.Size = new System.Drawing.Size(121, 22);
            this.staticToolStripMenuItem1.Text = "Static";
            this.staticToolStripMenuItem1.Click += new System.EventHandler(this.StaticToolStripMenuItem1_Click);
            // 
            // dynamicToolStripMenuItem
            // 
            this.dynamicToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dynamicToolStripMenuItem.Name = "dynamicToolStripMenuItem";
            this.dynamicToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.dynamicToolStripMenuItem.Text = "Dynamic";
            this.dynamicToolStripMenuItem.Click += new System.EventHandler(this.DynamicToolStripMenuItem_Click);
            // 
            // toolStripStatusOutput
            // 
            this.toolStripStatusOutput.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rawSpectrumToolStripMenuItem1,
            this.darkCorrectedToolStripMenuItem1,
            this.absorbanceToolStripMenuItem1,
            this.transmissionToolStripMenuItem1});
            this.toolStripStatusOutput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusOutput.Image = global::spectra.Properties.Resources.ouput;
            this.toolStripStatusOutput.Name = "toolStripStatusOutput";
            this.toolStripStatusOutput.Size = new System.Drawing.Size(166, 24);
            this.toolStripStatusOutput.Text = "Output: Raw spectrum";
            // 
            // rawSpectrumToolStripMenuItem1
            // 
            this.rawSpectrumToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rawSpectrumToolStripMenuItem1.Name = "rawSpectrumToolStripMenuItem1";
            this.rawSpectrumToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
            this.rawSpectrumToolStripMenuItem1.Text = "Raw spectrum";
            this.rawSpectrumToolStripMenuItem1.Click += new System.EventHandler(this.RawSpectrumToolStripMenuItem1_Click);
            // 
            // darkCorrectedToolStripMenuItem1
            // 
            this.darkCorrectedToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.darkCorrectedToolStripMenuItem1.Name = "darkCorrectedToolStripMenuItem1";
            this.darkCorrectedToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
            this.darkCorrectedToolStripMenuItem1.Text = "Dark Corrected";
            this.darkCorrectedToolStripMenuItem1.Click += new System.EventHandler(this.DarkCorrectedToolStripMenuItem1_Click);
            // 
            // absorbanceToolStripMenuItem1
            // 
            this.absorbanceToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.absorbanceToolStripMenuItem1.Name = "absorbanceToolStripMenuItem1";
            this.absorbanceToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
            this.absorbanceToolStripMenuItem1.Text = "Absorbance";
            this.absorbanceToolStripMenuItem1.Click += new System.EventHandler(this.AbsorbanceToolStripMenuItem1_Click);
            // 
            // transmissionToolStripMenuItem1
            // 
            this.transmissionToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.transmissionToolStripMenuItem1.Name = "transmissionToolStripMenuItem1";
            this.transmissionToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
            this.transmissionToolStripMenuItem1.Text = "Transmission";
            this.transmissionToolStripMenuItem1.Click += new System.EventHandler(this.TransmissionToolStripMenuItem1_Click);
            // 
            // toolStripStatusSavingLabel
            // 
            this.toolStripStatusSavingLabel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onToolStripMenuItem2,
            this.offToolStripMenuItem2});
            this.toolStripStatusSavingLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusSavingLabel.Image = global::spectra.Properties.Resources.save;
            this.toolStripStatusSavingLabel.Name = "toolStripStatusSavingLabel";
            this.toolStripStatusSavingLabel.Size = new System.Drawing.Size(138, 24);
            this.toolStripStatusSavingLabel.Text = "Saving to file: Off";
            // 
            // onToolStripMenuItem2
            // 
            this.onToolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.onToolStripMenuItem2.Name = "onToolStripMenuItem2";
            this.onToolStripMenuItem2.Size = new System.Drawing.Size(91, 22);
            this.onToolStripMenuItem2.Text = "On";
            this.onToolStripMenuItem2.Click += new System.EventHandler(this.OnToolStripMenuItem2_Click);
            // 
            // offToolStripMenuItem2
            // 
            this.offToolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.offToolStripMenuItem2.Name = "offToolStripMenuItem2";
            this.offToolStripMenuItem2.Size = new System.Drawing.Size(91, 22);
            this.offToolStripMenuItem2.Text = "Off";
            this.offToolStripMenuItem2.Click += new System.EventHandler(this.OffToolStripMenuItem2_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 4);
            // 
            // labelTotalHits
            // 
            this.labelTotalHits.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelTotalHits.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalHits.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelTotalHits.Location = new System.Drawing.Point(786, 145);
            this.labelTotalHits.Name = "labelTotalHits";
            this.labelTotalHits.Size = new System.Drawing.Size(221, 18);
            this.labelTotalHits.TabIndex = 221;
            this.labelTotalHits.Text = "0 hits";
            this.labelTotalHits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPlotType
            // 
            this.labelPlotType.BackColor = System.Drawing.SystemColors.Window;
            this.labelPlotType.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlotType.Location = new System.Drawing.Point(12, 1004);
            this.labelPlotType.Name = "labelPlotType";
            this.labelPlotType.Size = new System.Drawing.Size(995, 22);
            this.labelPlotType.TabIndex = 223;
            this.labelPlotType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControlAcquisition
            // 
            this.tabControlAcquisition.Controls.Add(this.tabPageAcq);
            this.tabControlAcquisition.Controls.Add(this.tabPageRef);
            this.tabControlAcquisition.Controls.Add(this.tabPageProc);
            this.tabControlAcquisition.Controls.Add(this.tabPagePlot);
            this.tabControlAcquisition.Controls.Add(this.tabPageArduino);
            this.tabControlAcquisition.Enabled = false;
            this.tabControlAcquisition.Location = new System.Drawing.Point(1014, 145);
            this.tabControlAcquisition.Name = "tabControlAcquisition";
            this.tabControlAcquisition.SelectedIndex = 0;
            this.tabControlAcquisition.Size = new System.Drawing.Size(336, 706);
            this.tabControlAcquisition.TabIndex = 224;
            // 
            // tabPageAcq
            // 
            this.tabPageAcq.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageAcq.Controls.Add(this.groupBoxOutput);
            this.tabPageAcq.Controls.Add(this.acquisitionParametersControl);
            this.tabPageAcq.Controls.Add(this.checkBoxClearBufferBeforeAcquisition);
            this.tabPageAcq.Controls.Add(this.label26);
            this.tabPageAcq.Controls.Add(this.textBoxNumInBuffer);
            this.tabPageAcq.Controls.Add(this.buttonUpdateSpectraInBuffer);
            this.tabPageAcq.Controls.Add(this.buttonClearBuffer);
            this.tabPageAcq.Location = new System.Drawing.Point(4, 22);
            this.tabPageAcq.Name = "tabPageAcq";
            this.tabPageAcq.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAcq.Size = new System.Drawing.Size(328, 680);
            this.tabPageAcq.TabIndex = 0;
            this.tabPageAcq.Text = "Acquisition";
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Controls.Add(this.comboBoxAcquisitionOutput);
            this.groupBoxOutput.Location = new System.Drawing.Point(6, 30);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Size = new System.Drawing.Size(312, 51);
            this.groupBoxOutput.TabIndex = 151;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "Output";
            // 
            // comboBoxAcquisitionOutput
            // 
            this.comboBoxAcquisitionOutput.FormattingEnabled = true;
            this.comboBoxAcquisitionOutput.Items.AddRange(new object[] {
            "Raw spectrum",
            "Dark corrected",
            "Absorbance",
            "Transmission"});
            this.comboBoxAcquisitionOutput.Location = new System.Drawing.Point(7, 21);
            this.comboBoxAcquisitionOutput.Name = "comboBoxAcquisitionOutput";
            this.comboBoxAcquisitionOutput.Size = new System.Drawing.Size(299, 21);
            this.comboBoxAcquisitionOutput.TabIndex = 150;
            this.comboBoxAcquisitionOutput.SelectedIndexChanged += new System.EventHandler(this.comboBoxAcquisitionOutput_SelectedIndexChanged);
            // 
            // acquisitionParametersControl
            // 
            this.acquisitionParametersControl.Enabled = false;
            this.acquisitionParametersControl.Location = new System.Drawing.Point(6, 87);
            this.acquisitionParametersControl.Margin = new System.Windows.Forms.Padding(4);
            this.acquisitionParametersControl.Name = "acquisitionParametersControl";
            this.acquisitionParametersControl.Size = new System.Drawing.Size(319, 539);
            this.acquisitionParametersControl.TabIndex = 149;
            // 
            // tabPageRef
            // 
            this.tabPageRef.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageRef.Controls.Add(this.groupBoxReference);
            this.tabPageRef.Controls.Add(this.checkBoxLampEnableDark);
            this.tabPageRef.Controls.Add(this.groupBoxCorrectedReference);
            this.tabPageRef.Controls.Add(this.groupBoxDark);
            this.tabPageRef.Location = new System.Drawing.Point(4, 22);
            this.tabPageRef.Name = "tabPageRef";
            this.tabPageRef.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRef.Size = new System.Drawing.Size(328, 680);
            this.tabPageRef.TabIndex = 1;
            this.tabPageRef.Text = "Reference";
            // 
            // groupBoxReference
            // 
            this.groupBoxReference.Controls.Add(this.tabControlReferenceType);
            this.groupBoxReference.Location = new System.Drawing.Point(6, 90);
            this.groupBoxReference.Name = "groupBoxReference";
            this.groupBoxReference.Size = new System.Drawing.Size(316, 389);
            this.groupBoxReference.TabIndex = 224;
            this.groupBoxReference.TabStop = false;
            this.groupBoxReference.Text = "Reference spectrum";
            // 
            // tabControlReferenceType
            // 
            this.tabControlReferenceType.Controls.Add(this.tabPageSingle);
            this.tabControlReferenceType.Controls.Add(this.tabPageStatic);
            this.tabControlReferenceType.Controls.Add(this.tabPageDynamic);
            this.tabControlReferenceType.Location = new System.Drawing.Point(10, 21);
            this.tabControlReferenceType.Name = "tabControlReferenceType";
            this.tabControlReferenceType.SelectedIndex = 0;
            this.tabControlReferenceType.Size = new System.Drawing.Size(302, 362);
            this.tabControlReferenceType.TabIndex = 223;
            // 
            // tabPageSingle
            // 
            this.tabPageSingle.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSingle.Controls.Add(this.buttonTakeReference);
            this.tabPageSingle.Controls.Add(this.buttonClearReference);
            this.tabPageSingle.Controls.Add(this.labelReferenceStatus);
            this.tabPageSingle.Location = new System.Drawing.Point(4, 22);
            this.tabPageSingle.Name = "tabPageSingle";
            this.tabPageSingle.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSingle.Size = new System.Drawing.Size(294, 336);
            this.tabPageSingle.TabIndex = 0;
            this.tabPageSingle.Text = "Single";
            // 
            // tabPageStatic
            // 
            this.tabPageStatic.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageStatic.Controls.Add(this.buttonClearReferenceFromStatic);
            this.tabPageStatic.Controls.Add(this.groupBoxGenerateStaticReferenceSpectrum);
            this.tabPageStatic.Controls.Add(this.groupBoxAccumulateStaticReferenceSpectrum);
            this.tabPageStatic.Location = new System.Drawing.Point(4, 22);
            this.tabPageStatic.Name = "tabPageStatic";
            this.tabPageStatic.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStatic.Size = new System.Drawing.Size(294, 336);
            this.tabPageStatic.TabIndex = 1;
            this.tabPageStatic.Text = "Static";
            // 
            // buttonClearReferenceFromStatic
            // 
            this.buttonClearReferenceFromStatic.Location = new System.Drawing.Point(3, 314);
            this.buttonClearReferenceFromStatic.Name = "buttonClearReferenceFromStatic";
            this.buttonClearReferenceFromStatic.Size = new System.Drawing.Size(285, 23);
            this.buttonClearReferenceFromStatic.TabIndex = 223;
            this.buttonClearReferenceFromStatic.Text = "Clear Reference";
            this.buttonClearReferenceFromStatic.UseVisualStyleBackColor = true;
            this.buttonClearReferenceFromStatic.Click += new System.EventHandler(this.buttonClearReferenceFromStatic_Click);
            // 
            // groupBoxGenerateStaticReferenceSpectrum
            // 
            this.groupBoxGenerateStaticReferenceSpectrum.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxGenerateStaticReferenceSpectrum.Controls.Add(this.labelEstimateReferenceSpectrumExplanation);
            this.groupBoxGenerateStaticReferenceSpectrum.Controls.Add(this.buttonGenerateReferenceFromAccumulatedSpectra);
            this.groupBoxGenerateStaticReferenceSpectrum.Controls.Add(this.labelEstimateReferenceSpectrumMax);
            this.groupBoxGenerateStaticReferenceSpectrum.Controls.Add(this.textBoxEstimateReferenceSpectrumMax);
            this.groupBoxGenerateStaticReferenceSpectrum.Controls.Add(this.labelEstimateReferenceSpectrumMin);
            this.groupBoxGenerateStaticReferenceSpectrum.Controls.Add(this.textBoxEstimateReferenceSpectrumMin);
            this.groupBoxGenerateStaticReferenceSpectrum.Enabled = false;
            this.groupBoxGenerateStaticReferenceSpectrum.Location = new System.Drawing.Point(3, 167);
            this.groupBoxGenerateStaticReferenceSpectrum.Name = "groupBoxGenerateStaticReferenceSpectrum";
            this.groupBoxGenerateStaticReferenceSpectrum.Size = new System.Drawing.Size(285, 141);
            this.groupBoxGenerateStaticReferenceSpectrum.TabIndex = 222;
            this.groupBoxGenerateStaticReferenceSpectrum.TabStop = false;
            this.groupBoxGenerateStaticReferenceSpectrum.Text = "Generate";
            // 
            // labelEstimateReferenceSpectrumExplanation
            // 
            this.labelEstimateReferenceSpectrumExplanation.AutoSize = true;
            this.labelEstimateReferenceSpectrumExplanation.Location = new System.Drawing.Point(10, 18);
            this.labelEstimateReferenceSpectrumExplanation.Name = "labelEstimateReferenceSpectrumExplanation";
            this.labelEstimateReferenceSpectrumExplanation.Size = new System.Drawing.Size(256, 13);
            this.labelEstimateReferenceSpectrumExplanation.TabIndex = 187;
            this.labelEstimateReferenceSpectrumExplanation.Text = "Specify the range of accumulated spectra to use:";
            // 
            // buttonGenerateReferenceFromAccumulatedSpectra
            // 
            this.buttonGenerateReferenceFromAccumulatedSpectra.Location = new System.Drawing.Point(6, 105);
            this.buttonGenerateReferenceFromAccumulatedSpectra.Name = "buttonGenerateReferenceFromAccumulatedSpectra";
            this.buttonGenerateReferenceFromAccumulatedSpectra.Size = new System.Drawing.Size(275, 23);
            this.buttonGenerateReferenceFromAccumulatedSpectra.TabIndex = 186;
            this.buttonGenerateReferenceFromAccumulatedSpectra.Text = "Generate";
            this.buttonGenerateReferenceFromAccumulatedSpectra.UseVisualStyleBackColor = true;
            this.buttonGenerateReferenceFromAccumulatedSpectra.Click += new System.EventHandler(this.buttonGenerateReferenceFromAccumulatedSpectra_Click);
            // 
            // labelEstimateReferenceSpectrumMax
            // 
            this.labelEstimateReferenceSpectrumMax.AutoSize = true;
            this.labelEstimateReferenceSpectrumMax.Location = new System.Drawing.Point(10, 78);
            this.labelEstimateReferenceSpectrumMax.Name = "labelEstimateReferenceSpectrumMax";
            this.labelEstimateReferenceSpectrumMax.Size = new System.Drawing.Size(58, 13);
            this.labelEstimateReferenceSpectrumMax.TabIndex = 185;
            this.labelEstimateReferenceSpectrumMax.Text = "End index";
            // 
            // textBoxEstimateReferenceSpectrumMax
            // 
            this.textBoxEstimateReferenceSpectrumMax.Location = new System.Drawing.Point(170, 75);
            this.textBoxEstimateReferenceSpectrumMax.Name = "textBoxEstimateReferenceSpectrumMax";
            this.textBoxEstimateReferenceSpectrumMax.Size = new System.Drawing.Size(109, 22);
            this.textBoxEstimateReferenceSpectrumMax.TabIndex = 184;
            this.textBoxEstimateReferenceSpectrumMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxEstimateReferenceSpectrumMax.TextChanged += new System.EventHandler(this.textBoxEstimateReferenceSpectrumMax_TextChanged);
            // 
            // labelEstimateReferenceSpectrumMin
            // 
            this.labelEstimateReferenceSpectrumMin.AutoSize = true;
            this.labelEstimateReferenceSpectrumMin.Location = new System.Drawing.Point(10, 50);
            this.labelEstimateReferenceSpectrumMin.Name = "labelEstimateReferenceSpectrumMin";
            this.labelEstimateReferenceSpectrumMin.Size = new System.Drawing.Size(79, 13);
            this.labelEstimateReferenceSpectrumMin.TabIndex = 183;
            this.labelEstimateReferenceSpectrumMin.Text = "Starting index";
            // 
            // textBoxEstimateReferenceSpectrumMin
            // 
            this.textBoxEstimateReferenceSpectrumMin.Location = new System.Drawing.Point(170, 47);
            this.textBoxEstimateReferenceSpectrumMin.Name = "textBoxEstimateReferenceSpectrumMin";
            this.textBoxEstimateReferenceSpectrumMin.Size = new System.Drawing.Size(109, 22);
            this.textBoxEstimateReferenceSpectrumMin.TabIndex = 182;
            this.textBoxEstimateReferenceSpectrumMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxEstimateReferenceSpectrumMin.TextChanged += new System.EventHandler(this.textBoxEstimateReferenceSpectrumMin_TextChanged);
            // 
            // groupBoxAccumulateStaticReferenceSpectrum
            // 
            this.groupBoxAccumulateStaticReferenceSpectrum.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxAccumulateStaticReferenceSpectrum.Controls.Add(this.labelAcquireForSeconds);
            this.groupBoxAccumulateStaticReferenceSpectrum.Controls.Add(this.trackBarAccumulateSpectraSlider);
            this.groupBoxAccumulateStaticReferenceSpectrum.Controls.Add(this.labelAccumulateSpectraSlider);
            this.groupBoxAccumulateStaticReferenceSpectrum.Controls.Add(this.textBoxAcquireFor);
            this.groupBoxAccumulateStaticReferenceSpectrum.Controls.Add(this.buttonAccumulateSpectra);
            this.groupBoxAccumulateStaticReferenceSpectrum.Controls.Add(this.buttonAbortAccumulateSpectra);
            this.groupBoxAccumulateStaticReferenceSpectrum.Controls.Add(this.labelAcquireFor);
            this.groupBoxAccumulateStaticReferenceSpectrum.Location = new System.Drawing.Point(3, 6);
            this.groupBoxAccumulateStaticReferenceSpectrum.Name = "groupBoxAccumulateStaticReferenceSpectrum";
            this.groupBoxAccumulateStaticReferenceSpectrum.Size = new System.Drawing.Size(285, 155);
            this.groupBoxAccumulateStaticReferenceSpectrum.TabIndex = 221;
            this.groupBoxAccumulateStaticReferenceSpectrum.TabStop = false;
            this.groupBoxAccumulateStaticReferenceSpectrum.Text = "Accumulate";
            // 
            // labelAcquireForSeconds
            // 
            this.labelAcquireForSeconds.AutoSize = true;
            this.labelAcquireForSeconds.Location = new System.Drawing.Point(199, 24);
            this.labelAcquireForSeconds.Name = "labelAcquireForSeconds";
            this.labelAcquireForSeconds.Size = new System.Drawing.Size(49, 13);
            this.labelAcquireForSeconds.TabIndex = 201;
            this.labelAcquireForSeconds.Text = "seconds";
            // 
            // trackBarAccumulateSpectraSlider
            // 
            this.trackBarAccumulateSpectraSlider.Location = new System.Drawing.Point(6, 101);
            this.trackBarAccumulateSpectraSlider.Name = "trackBarAccumulateSpectraSlider";
            this.trackBarAccumulateSpectraSlider.Size = new System.Drawing.Size(275, 45);
            this.trackBarAccumulateSpectraSlider.TabIndex = 196;
            this.trackBarAccumulateSpectraSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarAccumulateSpectraSlider.ValueChanged += new System.EventHandler(this.trackBarAccumulateSpectraSlider_ValueChanged);
            // 
            // textBoxAcquireFor
            // 
            this.textBoxAcquireFor.Location = new System.Drawing.Point(108, 21);
            this.textBoxAcquireFor.Name = "textBoxAcquireFor";
            this.textBoxAcquireFor.Size = new System.Drawing.Size(80, 22);
            this.textBoxAcquireFor.TabIndex = 199;
            this.textBoxAcquireFor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxAcquireFor.TextChanged += new System.EventHandler(this.textBoxAcquireFor_TextChanged);
            this.textBoxAcquireFor.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxAcquireFor_Validating);
            this.textBoxAcquireFor.Validated += new System.EventHandler(this.textBoxAcquireFor_Validated);
            // 
            // labelAcquireFor
            // 
            this.labelAcquireFor.AutoSize = true;
            this.labelAcquireFor.Location = new System.Drawing.Point(28, 24);
            this.labelAcquireFor.Name = "labelAcquireFor";
            this.labelAcquireFor.Size = new System.Drawing.Size(64, 13);
            this.labelAcquireFor.TabIndex = 200;
            this.labelAcquireFor.Text = "Acquire for";
            // 
            // tabPageDynamic
            // 
            this.tabPageDynamic.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageDynamic.Controls.Add(this.labelDynamicReferenceExplanation);
            this.tabPageDynamic.Controls.Add(this.labelSpectrumIntervalBetweenGeneration);
            this.tabPageDynamic.Controls.Add(this.textBoxSpectrumIntervalBetweenGeneration);
            this.tabPageDynamic.Controls.Add(this.labelNumberOfSpectramToAccumulate);
            this.tabPageDynamic.Controls.Add(this.textBoxNumberOfSpectramToAccumulate);
            this.tabPageDynamic.Controls.Add(this.labelDynamicReferenceExplanation_More);
            this.tabPageDynamic.Location = new System.Drawing.Point(4, 22);
            this.tabPageDynamic.Name = "tabPageDynamic";
            this.tabPageDynamic.Size = new System.Drawing.Size(294, 336);
            this.tabPageDynamic.TabIndex = 2;
            this.tabPageDynamic.Text = "Dynamic";
            // 
            // labelDynamicReferenceExplanation
            // 
            this.labelDynamicReferenceExplanation.Location = new System.Drawing.Point(7, 83);
            this.labelDynamicReferenceExplanation.Name = "labelDynamicReferenceExplanation";
            this.labelDynamicReferenceExplanation.Size = new System.Drawing.Size(284, 65);
            this.labelDynamicReferenceExplanation.TabIndex = 228;
            this.labelDynamicReferenceExplanation.Text = "The dynamic reference is calculated as the dark spectrum-corrected median (across" +
    " wavelengths) of the given number of spectra. It is re-generated and applied wit" +
    "h the specified interval.";
            // 
            // labelSpectrumIntervalBetweenGeneration
            // 
            this.labelSpectrumIntervalBetweenGeneration.AutoSize = true;
            this.labelSpectrumIntervalBetweenGeneration.Location = new System.Drawing.Point(7, 36);
            this.labelSpectrumIntervalBetweenGeneration.Name = "labelSpectrumIntervalBetweenGeneration";
            this.labelSpectrumIntervalBetweenGeneration.Size = new System.Drawing.Size(167, 13);
            this.labelSpectrumIntervalBetweenGeneration.TabIndex = 227;
            this.labelSpectrumIntervalBetweenGeneration.Text = "Spectra between re-generation";
            // 
            // textBoxSpectrumIntervalBetweenGeneration
            // 
            this.textBoxSpectrumIntervalBetweenGeneration.Location = new System.Drawing.Point(199, 31);
            this.textBoxSpectrumIntervalBetweenGeneration.Name = "textBoxSpectrumIntervalBetweenGeneration";
            this.textBoxSpectrumIntervalBetweenGeneration.Size = new System.Drawing.Size(83, 22);
            this.textBoxSpectrumIntervalBetweenGeneration.TabIndex = 226;
            this.textBoxSpectrumIntervalBetweenGeneration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSpectrumIntervalBetweenGeneration.TextChanged += new System.EventHandler(this.textBoxSpectrumIntervalBetweenGeneration_TextChanged);
            this.textBoxSpectrumIntervalBetweenGeneration.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxSpectrumIntervalBetweenGeneration_Validating);
            this.textBoxSpectrumIntervalBetweenGeneration.Validated += new System.EventHandler(this.textBoxSpectrumIntervalBetweenGeneration_Validated);
            // 
            // labelNumberOfSpectramToAccumulate
            // 
            this.labelNumberOfSpectramToAccumulate.AutoSize = true;
            this.labelNumberOfSpectramToAccumulate.Location = new System.Drawing.Point(7, 8);
            this.labelNumberOfSpectramToAccumulate.Name = "labelNumberOfSpectramToAccumulate";
            this.labelNumberOfSpectramToAccumulate.Size = new System.Drawing.Size(169, 13);
            this.labelNumberOfSpectramToAccumulate.TabIndex = 225;
            this.labelNumberOfSpectramToAccumulate.Text = "Number of spectra (max=65535)";
            // 
            // textBoxNumberOfSpectramToAccumulate
            // 
            this.textBoxNumberOfSpectramToAccumulate.Location = new System.Drawing.Point(199, 3);
            this.textBoxNumberOfSpectramToAccumulate.Name = "textBoxNumberOfSpectramToAccumulate";
            this.textBoxNumberOfSpectramToAccumulate.Size = new System.Drawing.Size(83, 22);
            this.textBoxNumberOfSpectramToAccumulate.TabIndex = 224;
            this.textBoxNumberOfSpectramToAccumulate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxNumberOfSpectramToAccumulate.TextChanged += new System.EventHandler(this.textBoxNumberOfSpectramToAccumulate_TextChanged);
            this.textBoxNumberOfSpectramToAccumulate.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNumberOfSpectramToAccumulate_Validating);
            this.textBoxNumberOfSpectramToAccumulate.Validated += new System.EventHandler(this.textBoxNumberOfSpectramToAccumulate_Validated);
            // 
            // labelDynamicReferenceExplanation_More
            // 
            this.labelDynamicReferenceExplanation_More.Location = new System.Drawing.Point(7, 148);
            this.labelDynamicReferenceExplanation_More.Name = "labelDynamicReferenceExplanation_More";
            this.labelDynamicReferenceExplanation_More.Size = new System.Drawing.Size(284, 48);
            this.labelDynamicReferenceExplanation_More.TabIndex = 222;
            this.labelDynamicReferenceExplanation_More.Text = "However, the dynamic reference will only be calculated if the output is set to \'a" +
    "bsorbance\' or \'transmission\'.";
            // 
            // tabPageProc
            // 
            this.tabPageProc.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageProc.Controls.Add(this.processingControl);
            this.tabPageProc.Location = new System.Drawing.Point(4, 22);
            this.tabPageProc.Name = "tabPageProc";
            this.tabPageProc.Size = new System.Drawing.Size(328, 680);
            this.tabPageProc.TabIndex = 3;
            this.tabPageProc.Text = "Processing";
            // 
            // processingControl
            // 
            this.processingControl.Location = new System.Drawing.Point(8, 7);
            this.processingControl.Margin = new System.Windows.Forms.Padding(4);
            this.processingControl.Name = "processingControl";
            this.processingControl.Size = new System.Drawing.Size(315, 256);
            this.processingControl.TabIndex = 0;
            // 
            // tabPagePlot
            // 
            this.tabPagePlot.BackColor = System.Drawing.SystemColors.Control;
            this.tabPagePlot.Controls.Add(this.plotOptionsControl);
            this.tabPagePlot.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlot.Name = "tabPagePlot";
            this.tabPagePlot.Size = new System.Drawing.Size(328, 680);
            this.tabPagePlot.TabIndex = 4;
            this.tabPagePlot.Text = "Plotting";
            // 
            // plotOptionsControl
            // 
            this.plotOptionsControl.Location = new System.Drawing.Point(7, 7);
            this.plotOptionsControl.Margin = new System.Windows.Forms.Padding(4);
            this.plotOptionsControl.Name = "plotOptionsControl";
            this.plotOptionsControl.Size = new System.Drawing.Size(315, 650);
            this.plotOptionsControl.TabIndex = 0;
            // 
            // tabPageArduino
            // 
            this.tabPageArduino.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageArduino.Controls.Add(this.arduinoParametersControl);
            this.tabPageArduino.Location = new System.Drawing.Point(4, 22);
            this.tabPageArduino.Name = "tabPageArduino";
            this.tabPageArduino.Size = new System.Drawing.Size(328, 680);
            this.tabPageArduino.TabIndex = 2;
            this.tabPageArduino.Text = "Arduino";
            // 
            // arduinoParametersControl
            // 
            this.arduinoParametersControl.Location = new System.Drawing.Point(7, 7);
            this.arduinoParametersControl.Margin = new System.Windows.Forms.Padding(4);
            this.arduinoParametersControl.Name = "arduinoParametersControl";
            this.arduinoParametersControl.Size = new System.Drawing.Size(315, 268);
            this.arduinoParametersControl.TabIndex = 0;
            // 
            // groupBoxAcqStatistics
            // 
            this.groupBoxAcqStatistics.Controls.Add(this.label3);
            this.groupBoxAcqStatistics.Controls.Add(this.labelComputedSpectra);
            this.groupBoxAcqStatistics.Controls.Add(this.label33);
            this.groupBoxAcqStatistics.Controls.Add(this.label6);
            this.groupBoxAcqStatistics.Controls.Add(this.label34);
            this.groupBoxAcqStatistics.Controls.Add(this.label32);
            this.groupBoxAcqStatistics.Controls.Add(this.labelSavedSpectra);
            this.groupBoxAcqStatistics.Controls.Add(this.labelTotalTime);
            this.groupBoxAcqStatistics.Controls.Add(this.label31);
            this.groupBoxAcqStatistics.Controls.Add(this.labelTotalSpectra);
            this.groupBoxAcqStatistics.Controls.Add(this.label24);
            this.groupBoxAcqStatistics.Controls.Add(this.labelSavedBytes);
            this.groupBoxAcqStatistics.Controls.Add(this.labelSpectraPerSec);
            this.groupBoxAcqStatistics.Controls.Add(this.label23);
            this.groupBoxAcqStatistics.Controls.Add(this.labelSpectraPerRequest);
            this.groupBoxAcqStatistics.Controls.Add(this.label4);
            this.groupBoxAcqStatistics.Controls.Add(this.labelTotalRequests);
            this.groupBoxAcqStatistics.Controls.Add(this.labelTotalBytes);
            this.groupBoxAcqStatistics.Location = new System.Drawing.Point(1014, 857);
            this.groupBoxAcqStatistics.Name = "groupBoxAcqStatistics";
            this.groupBoxAcqStatistics.Size = new System.Drawing.Size(332, 168);
            this.groupBoxAcqStatistics.TabIndex = 229;
            this.groupBoxAcqStatistics.TabStop = false;
            this.groupBoxAcqStatistics.Text = "Statistics";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label3.Location = new System.Drawing.Point(126, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 167;
            this.label3.Text = "Received";
            // 
            // labelComputedSpectra
            // 
            this.labelComputedSpectra.AutoSize = true;
            this.labelComputedSpectra.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelComputedSpectra.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelComputedSpectra.Location = new System.Drawing.Point(200, 38);
            this.labelComputedSpectra.Name = "labelComputedSpectra";
            this.labelComputedSpectra.Size = new System.Drawing.Size(13, 13);
            this.labelComputedSpectra.TabIndex = 170;
            this.labelComputedSpectra.Text = "0";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label33.Location = new System.Drawing.Point(14, 101);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(91, 13);
            this.label33.TabIndex = 157;
            this.label33.Text = "Spectra/Request";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label6.Location = new System.Drawing.Point(200, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 169;
            this.label6.Text = "Processed";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label34.Location = new System.Drawing.Point(14, 143);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(61, 13);
            this.label34.TabIndex = 158;
            this.label34.Text = "Total Bytes";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label32.Location = new System.Drawing.Point(14, 59);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(30, 13);
            this.label32.TabIndex = 156;
            this.label32.Text = "Time";
            // 
            // labelSavedSpectra
            // 
            this.labelSavedSpectra.AutoSize = true;
            this.labelSavedSpectra.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelSavedSpectra.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelSavedSpectra.Location = new System.Drawing.Point(271, 38);
            this.labelSavedSpectra.Name = "labelSavedSpectra";
            this.labelSavedSpectra.Size = new System.Drawing.Size(13, 13);
            this.labelSavedSpectra.TabIndex = 168;
            this.labelSavedSpectra.Text = "0";
            // 
            // labelTotalTime
            // 
            this.labelTotalTime.AutoSize = true;
            this.labelTotalTime.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelTotalTime.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelTotalTime.Location = new System.Drawing.Point(126, 59);
            this.labelTotalTime.Name = "labelTotalTime";
            this.labelTotalTime.Size = new System.Drawing.Size(49, 13);
            this.labelTotalTime.TabIndex = 159;
            this.labelTotalTime.Text = "00:00:00";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label31.Location = new System.Drawing.Point(14, 80);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(65, 13);
            this.label31.TabIndex = 155;
            this.label31.Text = "Spectra/sec";
            // 
            // labelTotalSpectra
            // 
            this.labelTotalSpectra.AutoSize = true;
            this.labelTotalSpectra.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelTotalSpectra.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelTotalSpectra.Location = new System.Drawing.Point(126, 38);
            this.labelTotalSpectra.Name = "labelTotalSpectra";
            this.labelTotalSpectra.Size = new System.Drawing.Size(13, 13);
            this.labelTotalSpectra.TabIndex = 160;
            this.labelTotalSpectra.Text = "0";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label24.Location = new System.Drawing.Point(14, 122);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(81, 13);
            this.label24.TabIndex = 154;
            this.label24.Text = "Total Requests";
            // 
            // labelSavedBytes
            // 
            this.labelSavedBytes.AutoSize = true;
            this.labelSavedBytes.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelSavedBytes.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelSavedBytes.Location = new System.Drawing.Point(271, 143);
            this.labelSavedBytes.Name = "labelSavedBytes";
            this.labelSavedBytes.Size = new System.Drawing.Size(13, 13);
            this.labelSavedBytes.TabIndex = 166;
            this.labelSavedBytes.Text = "0";
            // 
            // labelSpectraPerSec
            // 
            this.labelSpectraPerSec.AutoSize = true;
            this.labelSpectraPerSec.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelSpectraPerSec.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelSpectraPerSec.Location = new System.Drawing.Point(126, 80);
            this.labelSpectraPerSec.Name = "labelSpectraPerSec";
            this.labelSpectraPerSec.Size = new System.Drawing.Size(13, 13);
            this.labelSpectraPerSec.TabIndex = 161;
            this.labelSpectraPerSec.Text = "0";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label23.Location = new System.Drawing.Point(14, 38);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(45, 13);
            this.label23.TabIndex = 153;
            this.label23.Text = "Spectra";
            // 
            // labelSpectraPerRequest
            // 
            this.labelSpectraPerRequest.AutoSize = true;
            this.labelSpectraPerRequest.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelSpectraPerRequest.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelSpectraPerRequest.Location = new System.Drawing.Point(126, 101);
            this.labelSpectraPerRequest.Name = "labelSpectraPerRequest";
            this.labelSpectraPerRequest.Size = new System.Drawing.Size(13, 13);
            this.labelSpectraPerRequest.TabIndex = 162;
            this.labelSpectraPerRequest.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label4.Location = new System.Drawing.Point(271, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 165;
            this.label4.Text = "Saved";
            // 
            // labelTotalRequests
            // 
            this.labelTotalRequests.AutoSize = true;
            this.labelTotalRequests.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelTotalRequests.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelTotalRequests.Location = new System.Drawing.Point(126, 122);
            this.labelTotalRequests.Name = "labelTotalRequests";
            this.labelTotalRequests.Size = new System.Drawing.Size(13, 13);
            this.labelTotalRequests.TabIndex = 163;
            this.labelTotalRequests.Text = "0";
            // 
            // labelTotalBytes
            // 
            this.labelTotalBytes.AutoSize = true;
            this.labelTotalBytes.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.labelTotalBytes.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelTotalBytes.Location = new System.Drawing.Point(126, 143);
            this.labelTotalBytes.Name = "labelTotalBytes";
            this.labelTotalBytes.Size = new System.Drawing.Size(13, 13);
            this.labelTotalBytes.TabIndex = 164;
            this.labelTotalBytes.Text = "0";
            // 
            // mainChart
            // 
            this.mainChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.LabelStyle.Format = "#.";
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.LabelStyle.Format = "#.";
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 100F;
            chartArea1.Position.Width = 85F;
            this.mainChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend";
            this.mainChart.Legends.Add(legend1);
            this.mainChart.Location = new System.Drawing.Point(12, 170);
            this.mainChart.Name = "mainChart";
            this.mainChart.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.mainChart.Size = new System.Drawing.Size(995, 835);
            this.mainChart.TabIndex = 215;
            this.mainChart.Text = "Main Chart";
            // 
            // groupBoxOceanFXUSB
            // 
            this.groupBoxOceanFXUSB.Controls.Add(this.dataGridViewUSBDeviceList);
            this.groupBoxOceanFXUSB.Controls.Add(this.buttonUSBRescan);
            this.groupBoxOceanFXUSB.Controls.Add(this.buttonUSBConnect);
            this.groupBoxOceanFXUSB.Controls.Add(this.buttonUSBDisconnect);
            this.groupBoxOceanFXUSB.Location = new System.Drawing.Point(12, 26);
            this.groupBoxOceanFXUSB.Name = "groupBoxOceanFXUSB";
            this.groupBoxOceanFXUSB.Size = new System.Drawing.Size(327, 113);
            this.groupBoxOceanFXUSB.TabIndex = 230;
            this.groupBoxOceanFXUSB.TabStop = false;
            this.groupBoxOceanFXUSB.Text = "Ocean FX (USB Devices)";
            // 
            // groupBoxOceanFXNetwork
            // 
            this.groupBoxOceanFXNetwork.Controls.Add(this.buttonIPDisconnect);
            this.groupBoxOceanFXNetwork.Controls.Add(this.buttonIPConnect);
            this.groupBoxOceanFXNetwork.Controls.Add(this.buttonIPRescan);
            this.groupBoxOceanFXNetwork.Controls.Add(this.dataGridViewIPDevices);
            this.groupBoxOceanFXNetwork.Location = new System.Drawing.Point(345, 26);
            this.groupBoxOceanFXNetwork.Name = "groupBoxOceanFXNetwork";
            this.groupBoxOceanFXNetwork.Size = new System.Drawing.Size(327, 113);
            this.groupBoxOceanFXNetwork.TabIndex = 231;
            this.groupBoxOceanFXNetwork.TabStop = false;
            this.groupBoxOceanFXNetwork.Text = "Ocean FX (Network Devices)";
            // 
            // groupBoxArduinoCOM
            // 
            this.groupBoxArduinoCOM.Controls.Add(this.buttonRescanArduino);
            this.groupBoxArduinoCOM.Controls.Add(this.buttonConnectToArduino);
            this.groupBoxArduinoCOM.Controls.Add(this.buttonDisconnectFromArduino);
            this.groupBoxArduinoCOM.Controls.Add(this.dataGridViewArduinoDevices);
            this.groupBoxArduinoCOM.Location = new System.Drawing.Point(679, 26);
            this.groupBoxArduinoCOM.Name = "groupBoxArduinoCOM";
            this.groupBoxArduinoCOM.Size = new System.Drawing.Size(327, 113);
            this.groupBoxArduinoCOM.TabIndex = 232;
            this.groupBoxArduinoCOM.TabStop = false;
            this.groupBoxArduinoCOM.Text = "Arduino (COM Devices)";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 1050);
            this.Controls.Add(this.groupBoxAcqStatistics);
            this.Controls.Add(this.tabControlAcquisition);
            this.Controls.Add(this.labelPlotType);
            this.Controls.Add(this.buttonStartAcquisition);
            this.Controls.Add(this.buttonAbortAcquisition);
            this.Controls.Add(this.labelTotalHits);
            this.Controls.Add(this.labelCurSpectraPerSec);
            this.Controls.Add(this.mainChart);
            this.Controls.Add(this.statusStripToolbar);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.labelFileSaveError);
            this.Controls.Add(this.groupBoxOceanFXUSB);
            this.Controls.Add(this.groupBoxOceanFXNetwork);
            this.Controls.Add(this.groupBoxArduinoCOM);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Spectra Sorter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIPDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUSBDeviceList)).EndInit();
            this.groupBoxDark.ResumeLayout(false);
            this.groupBoxDark.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewArduinoDevices)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBoxCorrectedReference.ResumeLayout(false);
            this.groupBoxCorrectedReference.PerformLayout();
            this.statusStripToolbar.ResumeLayout(false);
            this.statusStripToolbar.PerformLayout();
            this.tabControlAcquisition.ResumeLayout(false);
            this.tabPageAcq.ResumeLayout(false);
            this.tabPageAcq.PerformLayout();
            this.groupBoxOutput.ResumeLayout(false);
            this.tabPageRef.ResumeLayout(false);
            this.tabPageRef.PerformLayout();
            this.groupBoxReference.ResumeLayout(false);
            this.tabControlReferenceType.ResumeLayout(false);
            this.tabPageSingle.ResumeLayout(false);
            this.tabPageSingle.PerformLayout();
            this.tabPageStatic.ResumeLayout(false);
            this.groupBoxGenerateStaticReferenceSpectrum.ResumeLayout(false);
            this.groupBoxGenerateStaticReferenceSpectrum.PerformLayout();
            this.groupBoxAccumulateStaticReferenceSpectrum.ResumeLayout(false);
            this.groupBoxAccumulateStaticReferenceSpectrum.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAccumulateSpectraSlider)).EndInit();
            this.tabPageDynamic.ResumeLayout(false);
            this.tabPageDynamic.PerformLayout();
            this.tabPageProc.ResumeLayout(false);
            this.tabPagePlot.ResumeLayout(false);
            this.tabPageArduino.ResumeLayout(false);
            this.groupBoxAcqStatistics.ResumeLayout(false);
            this.groupBoxAcqStatistics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainChart)).EndInit();
            this.groupBoxOceanFXUSB.ResumeLayout(false);
            this.groupBoxOceanFXNetwork.ResumeLayout(false);
            this.groupBoxArduinoCOM.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewIPDevices;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Port;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridView dataGridViewUSBDeviceList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDeviceDescription;
        private System.Windows.Forms.Button buttonUSBRescan;
        private System.Windows.Forms.CheckBox checkBoxLampEnableDark;
        private System.Windows.Forms.Label labelFileSaveError;
        private System.Windows.Forms.Button buttonStartAcquisition;
        private System.Windows.Forms.Button buttonAbortAcquisition;
        private System.Windows.Forms.TextBox textBoxNumInBuffer;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button buttonUpdateSpectraInBuffer;
        private System.Windows.Forms.Button buttonUSBConnect;
        private System.Windows.Forms.Button buttonTakeDark;
        private System.Windows.Forms.Label labelDarkStatus;
        private System.Windows.Forms.Label labelReferenceStatus;
        private System.Windows.Forms.Button buttonTakeReference;
        private System.Windows.Forms.Button buttonClearBuffer;
        private System.Windows.Forms.Button buttonClearDark;
        private System.Windows.Forms.Button buttonClearReference;
        private System.Windows.Forms.CheckBox checkBoxClearBufferBeforeAcquisition;
        private System.Windows.Forms.GroupBox groupBoxDark;
        private System.Windows.Forms.DataGridView dataGridViewArduinoDevices;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Button buttonConnectToArduino;
        private System.Windows.Forms.Button buttonRescanArduino;
        private System.Windows.Forms.Button buttonDisconnectFromArduino;
        private System.Windows.Forms.Label labelAccumulateSpectraSlider;
        private System.Windows.Forms.Button buttonUSBDisconnect;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.Button buttonIPDisconnect;
        private System.Windows.Forms.Button buttonIPConnect;
        private System.Windows.Forms.Button buttonIPRescan;
        private components.MainChart mainChart;
        private System.Windows.Forms.Label labelCurSpectraPerSec;
        private System.Windows.Forms.GroupBox groupBoxCorrectedReference;
        private System.Windows.Forms.Label labelCorrectedReferenceSpectrumStatus;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawSpectrumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkCorrectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem absorbanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transmissionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem referenceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dynamicReferenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spectrumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timeSeriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accumulatedSpectraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem referenceSpectrumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accumulatedTimeSeriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem autoScaleYAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doNotUseReferenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripOceanFXUSBStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripOceanFXIPStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripArduinoStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusSystemTimeAccuracy;
        private System.Windows.Forms.StatusStrip statusStripToolbar;
        public System.Windows.Forms.Label labelCorrectedReferenceSpectrumType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem darkSpectrumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem correctedReferenceSpectrumToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem staticToolStripMenuItem;
        private System.Windows.Forms.Button buttonAccumulateSpectra;
        private System.Windows.Forms.ToolStripMenuItem toolStripHelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shortcutsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem showThresholdsToolStripMenuItem;
        private System.Windows.Forms.Label labelTotalHits;
        private System.Windows.Forms.ToolStripDropDownButton toolStripStatusFilteringLabel;
        private System.Windows.Forms.ToolStripMenuItem onToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripStatusThresholdingLabel;
        private System.Windows.Forms.ToolStripMenuItem onToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripStatusOutput;
        private System.Windows.Forms.ToolStripMenuItem rawSpectrumToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem darkCorrectedToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem absorbanceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem transmissionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripStatusSavingLabel;
        private System.Windows.Forms.ToolStripMenuItem onToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripStatusReferenceLabel;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem staticToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dynamicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wavelengthsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wavelengthHubToolStripMenuItem;
        private System.Windows.Forms.Label labelPlotType;
        private System.Windows.Forms.TabControl tabControlAcquisition;
        private System.Windows.Forms.TabPage tabPageAcq;
        private System.Windows.Forms.TabPage tabPageRef;
        private System.Windows.Forms.Button buttonAbortAccumulateSpectra;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showTriggerPointsToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxAcqStatistics;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelComputedSpectra;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label labelSavedSpectra;
        private System.Windows.Forms.Label labelTotalTime;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label labelTotalSpectra;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label labelSavedBytes;
        private System.Windows.Forms.Label labelSpectraPerSec;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label labelSpectraPerRequest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelTotalRequests;
        private System.Windows.Forms.Label labelTotalBytes;
        private components.AcquisitionParametersControl acquisitionParametersControl;
        private System.Windows.Forms.TabPage tabPageArduino;
        private components.ArduinoParametersControl arduinoParametersControl;
        private System.Windows.Forms.TabPage tabPageProc;
        private components.ProcessingControl processingControl;
        private System.Windows.Forms.TabControl tabControlReferenceType;
        private System.Windows.Forms.TabPage tabPageSingle;
        private System.Windows.Forms.TabPage tabPageStatic;
        private System.Windows.Forms.TabPage tabPageDynamic;
        private System.Windows.Forms.Label labelAcquireForSeconds;
        private System.Windows.Forms.TextBox textBoxAcquireFor;
        private System.Windows.Forms.Label labelAcquireFor;
        private System.Windows.Forms.GroupBox groupBoxGenerateStaticReferenceSpectrum;
        private System.Windows.Forms.GroupBox groupBoxAccumulateStaticReferenceSpectrum;
        private System.Windows.Forms.Label labelEstimateReferenceSpectrumExplanation;
        private System.Windows.Forms.Button buttonGenerateReferenceFromAccumulatedSpectra;
        private System.Windows.Forms.Label labelEstimateReferenceSpectrumMax;
        private System.Windows.Forms.TextBox textBoxEstimateReferenceSpectrumMax;
        private System.Windows.Forms.Label labelEstimateReferenceSpectrumMin;
        private System.Windows.Forms.TextBox textBoxEstimateReferenceSpectrumMin;
        private System.Windows.Forms.GroupBox groupBoxReference;
        private System.Windows.Forms.Button buttonClearReferenceFromStatic;
        private System.Windows.Forms.Label labelSpectrumIntervalBetweenGeneration;
        private System.Windows.Forms.TextBox textBoxSpectrumIntervalBetweenGeneration;
        private System.Windows.Forms.Label labelNumberOfSpectramToAccumulate;
        private System.Windows.Forms.TextBox textBoxNumberOfSpectramToAccumulate;
        private System.Windows.Forms.ToolStripMenuItem enabledFilteringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enabledTriggeringToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxAcquisitionOutput;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripSettings;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem revertSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.Label labelDynamicReferenceExplanation;
        private System.Windows.Forms.Label labelDynamicReferenceExplanation_More;
        private System.Windows.Forms.ToolStripMenuItem enableSaveToFileToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPagePlot;
        private components.PlotOptionsControl plotOptionsControl;
        private System.Windows.Forms.TrackBar trackBarAccumulateSpectraSlider;
        private System.Windows.Forms.GroupBox groupBoxOceanFXUSB;
        private System.Windows.Forms.GroupBox groupBoxOceanFXNetwork;
        private System.Windows.Forms.GroupBox groupBoxArduinoCOM;
    }
}

