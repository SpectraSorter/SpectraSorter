namespace FXStreamer
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataGridViewIPDevices = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewUSBDeviceList = new System.Windows.Forms.DataGridView();
            this.ColumnDeviceDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonRescan = new System.Windows.Forms.Button();
            this.groupBoxSingleStrobe = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxSingleStrobeEnabled = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxSingleStrobePulseWidth = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxSingleStrobePulseDelay = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBoxContinuousStrobe = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxContinuousStrobeWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxContinuousStrobePeriod = new System.Windows.Forms.TextBox();
            this.checkBoxContinuousStrobeEnabled = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.checkBoxBufferingEnabled = new System.Windows.Forms.CheckBox();
            this.label29 = new System.Windows.Forms.Label();
            this.textBoxSpectraPerRequest = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.checkBoxSingleSWTrigger = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.comboBoxTriggerMode = new System.Windows.Forms.ComboBox();
            this.comboBoxAcquireUnits = new System.Windows.Forms.ComboBox();
            this.textBoxAcquireDuration = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxAcquisitionDelay = new System.Windows.Forms.TextBox();
            this.textBoxBackToBack = new System.Windows.Forms.TextBox();
            this.textBoxScansToAverage = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxIntegrationTime = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBoxLampEnable = new System.Windows.Forms.CheckBox();
            this.labelFileSaveError = new System.Windows.Forms.Label();
            this.label134 = new System.Windows.Forms.Label();
            this.textBoxSaveEndRange = new System.Windows.Forms.TextBox();
            this.label133 = new System.Windows.Forms.Label();
            this.textBoxSaveStartRange = new System.Windows.Forms.TextBox();
            this.buttonSaveDir = new System.Windows.Forms.Button();
            this.textBoxSaveFilename = new System.Windows.Forms.TextBox();
            this.checkBoxSaveToFile = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxFWVersion = new System.Windows.Forms.TextBox();
            this.textBoxFWSubversion = new System.Windows.Forms.TextBox();
            this.textBoxFPGAVersion = new System.Windows.Forms.TextBox();
            this.textBoxSerialNum = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.buttonStartAcquisition = new System.Windows.Forms.Button();
            this.buttonAbortAcquisition = new System.Windows.Forms.Button();
            this.labelSaveStart = new System.Windows.Forms.Label();
            this.labelSaveEnd = new System.Windows.Forms.Label();
            this.comboBoxSaveRangeUnits = new System.Windows.Forms.ComboBox();
            this.labelCurSpectraPerSec = new System.Windows.Forms.Label();
            this.textBoxNumInBuffer = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.buttonUpdateSpectraInBuffer = new System.Windows.Forms.Button();
            this.labelConnectStatus = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonTakeDark = new System.Windows.Forms.Button();
            this.comboBoxSaveType = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.labelDarkStatus = new System.Windows.Forms.Label();
            this.labelSaveTypeStatus = new System.Windows.Forms.Label();
            this.labelReferenceStatus = new System.Windows.Forms.Label();
            this.buttonTakeReference = new System.Windows.Forms.Button();
            this.buttonClearBuffer = new System.Windows.Forms.Button();
            this.buttonClearDark = new System.Windows.Forms.Button();
            this.buttonClearReference = new System.Windows.Forms.Button();
            this.chartSpectrum = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.checkBoxClearBufferBeforeAcquisition = new System.Windows.Forms.CheckBox();
            this.buttonUpdateFileTime = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.labelTotalTime = new System.Windows.Forms.Label();
            this.labelTotalSpectra = new System.Windows.Forms.Label();
            this.labelSpectraPerSec = new System.Windows.Forms.Label();
            this.labelSpectraPerRequest = new System.Windows.Forms.Label();
            this.labelTotalRequests = new System.Windows.Forms.Label();
            this.labelTotalBytes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIPDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUSBDeviceList)).BeginInit();
            this.groupBoxSingleStrobe.SuspendLayout();
            this.groupBoxContinuousStrobe.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSpectrum)).BeginInit();
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
            this.dataGridViewIPDevices.Location = new System.Drawing.Point(349, 32);
            this.dataGridViewIPDevices.MultiSelect = false;
            this.dataGridViewIPDevices.Name = "dataGridViewIPDevices";
            this.dataGridViewIPDevices.ReadOnly = true;
            this.dataGridViewIPDevices.RowHeadersVisible = false;
            this.dataGridViewIPDevices.RowTemplate.ReadOnly = true;
            this.dataGridViewIPDevices.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewIPDevices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewIPDevices.Size = new System.Drawing.Size(269, 103);
            this.dataGridViewIPDevices.TabIndex = 28;
            this.dataGridViewIPDevices.SelectionChanged += new System.EventHandler(this.dataGridViewIPDevices_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 125F;
            this.dataGridViewTextBoxColumn2.HeaderText = "IP Address";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 256;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 95;
            // 
            // Port
            // 
            this.Port.HeaderText = "Port";
            this.Port.MaxInputLength = 256;
            this.Port.Name = "Port";
            this.Port.ReadOnly = true;
            this.Port.Width = 60;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 115F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Serial Num";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 256;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 115;
            // 
            // dataGridViewUSBDeviceList
            // 
            this.dataGridViewUSBDeviceList.AllowUserToAddRows = false;
            this.dataGridViewUSBDeviceList.AllowUserToDeleteRows = false;
            this.dataGridViewUSBDeviceList.AllowUserToResizeRows = false;
            this.dataGridViewUSBDeviceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUSBDeviceList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDeviceDescription});
            this.dataGridViewUSBDeviceList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewUSBDeviceList.Location = new System.Drawing.Point(13, 32);
            this.dataGridViewUSBDeviceList.MultiSelect = false;
            this.dataGridViewUSBDeviceList.Name = "dataGridViewUSBDeviceList";
            this.dataGridViewUSBDeviceList.ReadOnly = true;
            this.dataGridViewUSBDeviceList.RowHeadersVisible = false;
            this.dataGridViewUSBDeviceList.RowTemplate.ReadOnly = true;
            this.dataGridViewUSBDeviceList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewUSBDeviceList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUSBDeviceList.Size = new System.Drawing.Size(228, 103);
            this.dataGridViewUSBDeviceList.TabIndex = 30;
            this.dataGridViewUSBDeviceList.SelectionChanged += new System.EventHandler(this.dataGridViewUSBDeviceList_SelectionChanged);
            // 
            // ColumnDeviceDescription
            // 
            this.ColumnDeviceDescription.FillWeight = 225F;
            this.ColumnDeviceDescription.HeaderText = "Description";
            this.ColumnDeviceDescription.Name = "ColumnDeviceDescription";
            this.ColumnDeviceDescription.ReadOnly = true;
            this.ColumnDeviceDescription.Width = 225;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(74, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "USB Devices";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(435, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Network Devices";
            // 
            // buttonRescan
            // 
            this.buttonRescan.Enabled = false;
            this.buttonRescan.Location = new System.Drawing.Point(259, 32);
            this.buttonRescan.Name = "buttonRescan";
            this.buttonRescan.Size = new System.Drawing.Size(75, 23);
            this.buttonRescan.TabIndex = 33;
            this.buttonRescan.Text = "Rescan";
            this.buttonRescan.UseVisualStyleBackColor = true;
            this.buttonRescan.Click += new System.EventHandler(this.buttonRescan_Click);
            // 
            // groupBoxSingleStrobe
            // 
            this.groupBoxSingleStrobe.Controls.Add(this.label7);
            this.groupBoxSingleStrobe.Controls.Add(this.checkBoxSingleStrobeEnabled);
            this.groupBoxSingleStrobe.Controls.Add(this.label8);
            this.groupBoxSingleStrobe.Controls.Add(this.textBoxSingleStrobePulseWidth);
            this.groupBoxSingleStrobe.Controls.Add(this.label9);
            this.groupBoxSingleStrobe.Controls.Add(this.textBoxSingleStrobePulseDelay);
            this.groupBoxSingleStrobe.Controls.Add(this.label10);
            this.groupBoxSingleStrobe.Location = new System.Drawing.Point(349, 158);
            this.groupBoxSingleStrobe.Name = "groupBoxSingleStrobe";
            this.groupBoxSingleStrobe.Size = new System.Drawing.Size(213, 127);
            this.groupBoxSingleStrobe.TabIndex = 34;
            this.groupBoxSingleStrobe.TabStop = false;
            this.groupBoxSingleStrobe.Text = "Single Strobe";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(166, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "usec";
            // 
            // checkBoxSingleStrobeEnabled
            // 
            this.checkBoxSingleStrobeEnabled.AutoSize = true;
            this.checkBoxSingleStrobeEnabled.Location = new System.Drawing.Point(20, 29);
            this.checkBoxSingleStrobeEnabled.Name = "checkBoxSingleStrobeEnabled";
            this.checkBoxSingleStrobeEnabled.Size = new System.Drawing.Size(65, 17);
            this.checkBoxSingleStrobeEnabled.TabIndex = 0;
            this.checkBoxSingleStrobeEnabled.Text = "Enabled";
            this.checkBoxSingleStrobeEnabled.UseVisualStyleBackColor = true;
            this.checkBoxSingleStrobeEnabled.CheckedChanged += new System.EventHandler(this.checkBoxSingleStrobeEnabled_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(166, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "usec";
            // 
            // textBoxSingleStrobePulseWidth
            // 
            this.textBoxSingleStrobePulseWidth.Location = new System.Drawing.Point(86, 86);
            this.textBoxSingleStrobePulseWidth.Name = "textBoxSingleStrobePulseWidth";
            this.textBoxSingleStrobePulseWidth.Size = new System.Drawing.Size(78, 20);
            this.textBoxSingleStrobePulseWidth.TabIndex = 10;
            this.textBoxSingleStrobePulseWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Pulse Width";
            // 
            // textBoxSingleStrobePulseDelay
            // 
            this.textBoxSingleStrobePulseDelay.Location = new System.Drawing.Point(86, 57);
            this.textBoxSingleStrobePulseDelay.Name = "textBoxSingleStrobePulseDelay";
            this.textBoxSingleStrobePulseDelay.Size = new System.Drawing.Size(78, 20);
            this.textBoxSingleStrobePulseDelay.TabIndex = 8;
            this.textBoxSingleStrobePulseDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Pulse Delay";
            // 
            // groupBoxContinuousStrobe
            // 
            this.groupBoxContinuousStrobe.Controls.Add(this.label6);
            this.groupBoxContinuousStrobe.Controls.Add(this.label5);
            this.groupBoxContinuousStrobe.Controls.Add(this.label4);
            this.groupBoxContinuousStrobe.Controls.Add(this.textBoxContinuousStrobeWidth);
            this.groupBoxContinuousStrobe.Controls.Add(this.label3);
            this.groupBoxContinuousStrobe.Controls.Add(this.textBoxContinuousStrobePeriod);
            this.groupBoxContinuousStrobe.Controls.Add(this.checkBoxContinuousStrobeEnabled);
            this.groupBoxContinuousStrobe.Location = new System.Drawing.Point(620, 158);
            this.groupBoxContinuousStrobe.Name = "groupBoxContinuousStrobe";
            this.groupBoxContinuousStrobe.Size = new System.Drawing.Size(213, 127);
            this.groupBoxContinuousStrobe.TabIndex = 35;
            this.groupBoxContinuousStrobe.TabStop = false;
            this.groupBoxContinuousStrobe.Text = "Continuous Strobe";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(166, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "usec";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(166, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "usec";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Width";
            // 
            // textBoxContinuousStrobeWidth
            // 
            this.textBoxContinuousStrobeWidth.Location = new System.Drawing.Point(86, 86);
            this.textBoxContinuousStrobeWidth.Name = "textBoxContinuousStrobeWidth";
            this.textBoxContinuousStrobeWidth.Size = new System.Drawing.Size(78, 20);
            this.textBoxContinuousStrobeWidth.TabIndex = 4;
            this.textBoxContinuousStrobeWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Period";
            // 
            // textBoxContinuousStrobePeriod
            // 
            this.textBoxContinuousStrobePeriod.Location = new System.Drawing.Point(86, 57);
            this.textBoxContinuousStrobePeriod.Name = "textBoxContinuousStrobePeriod";
            this.textBoxContinuousStrobePeriod.Size = new System.Drawing.Size(78, 20);
            this.textBoxContinuousStrobePeriod.TabIndex = 2;
            this.textBoxContinuousStrobePeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBoxContinuousStrobeEnabled
            // 
            this.checkBoxContinuousStrobeEnabled.AutoSize = true;
            this.checkBoxContinuousStrobeEnabled.Location = new System.Drawing.Point(19, 29);
            this.checkBoxContinuousStrobeEnabled.Name = "checkBoxContinuousStrobeEnabled";
            this.checkBoxContinuousStrobeEnabled.Size = new System.Drawing.Size(65, 17);
            this.checkBoxContinuousStrobeEnabled.TabIndex = 1;
            this.checkBoxContinuousStrobeEnabled.Text = "Enabled";
            this.checkBoxContinuousStrobeEnabled.UseVisualStyleBackColor = true;
            this.checkBoxContinuousStrobeEnabled.CheckedChanged += new System.EventHandler(this.checkBoxContinuousStrobeEnabled_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.checkBoxBufferingEnabled);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.textBoxSpectraPerRequest);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.checkBoxSingleSWTrigger);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.comboBoxTriggerMode);
            this.groupBox1.Controls.Add(this.comboBoxAcquireUnits);
            this.groupBox1.Controls.Add(this.textBoxAcquireDuration);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.textBoxAcquisitionDelay);
            this.groupBox1.Controls.Add(this.textBoxBackToBack);
            this.groupBox1.Controls.Add(this.textBoxScansToAverage);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.textBoxIntegrationTime);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(13, 155);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 331);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Acquisition Parameters";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(220, 86);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(56, 13);
            this.label22.TabIndex = 141;
            this.label22.Text = "max=5000";
            // 
            // checkBoxBufferingEnabled
            // 
            this.checkBoxBufferingEnabled.AutoSize = true;
            this.checkBoxBufferingEnabled.Location = new System.Drawing.Point(118, 24);
            this.checkBoxBufferingEnabled.Name = "checkBoxBufferingEnabled";
            this.checkBoxBufferingEnabled.Size = new System.Drawing.Size(110, 17);
            this.checkBoxBufferingEnabled.TabIndex = 140;
            this.checkBoxBufferingEnabled.Text = "Buffering Enabled";
            this.checkBoxBufferingEnabled.UseVisualStyleBackColor = true;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(220, 139);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(44, 13);
            this.label29.TabIndex = 139;
            this.label29.Text = "max=15";
            // 
            // textBoxSpectraPerRequest
            // 
            this.textBoxSpectraPerRequest.Location = new System.Drawing.Point(140, 136);
            this.textBoxSpectraPerRequest.Name = "textBoxSpectraPerRequest";
            this.textBoxSpectraPerRequest.Size = new System.Drawing.Size(78, 20);
            this.textBoxSpectraPerRequest.TabIndex = 138;
            this.textBoxSpectraPerRequest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(11, 139);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(123, 13);
            this.label30.TabIndex = 137;
            this.label30.Text = "Num spectra per request";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(220, 112);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(62, 13);
            this.label28.TabIndex = 136;
            this.label28.Text = "max=65535";
            // 
            // checkBoxSingleSWTrigger
            // 
            this.checkBoxSingleSWTrigger.AutoSize = true;
            this.checkBoxSingleSWTrigger.Location = new System.Drawing.Point(82, 242);
            this.checkBoxSingleSWTrigger.Name = "checkBoxSingleSWTrigger";
            this.checkBoxSingleSWTrigger.Size = new System.Drawing.Size(136, 17);
            this.checkBoxSingleSWTrigger.TabIndex = 135;
            this.checkBoxSingleSWTrigger.Text = "Single Software Trigger";
            this.checkBoxSingleSWTrigger.UseVisualStyleBackColor = true;
            this.checkBoxSingleSWTrigger.Visible = false;
            this.checkBoxSingleSWTrigger.CheckedChanged += new System.EventHandler(this.checkBoxSingleSWTrigger_CheckedChanged);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(9, 210);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(70, 13);
            this.label25.TabIndex = 26;
            this.label25.Text = "Trigger Mode";
            // 
            // comboBoxTriggerMode
            // 
            this.comboBoxTriggerMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTriggerMode.FormattingEnabled = true;
            this.comboBoxTriggerMode.Items.AddRange(new object[] {
            "Software  (default)",
            "HW - Rising Edge",
            "HW - Falling Edge",
            "HW - Level",
            "HW - Legacy Synchronous",
            "HW - Synchronous  Start/Stop",
            "OFF   (disabled)"});
            this.comboBoxTriggerMode.Location = new System.Drawing.Point(82, 207);
            this.comboBoxTriggerMode.Name = "comboBoxTriggerMode";
            this.comboBoxTriggerMode.Size = new System.Drawing.Size(178, 21);
            this.comboBoxTriggerMode.TabIndex = 25;
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
            this.comboBoxAcquireUnits.Location = new System.Drawing.Point(172, 292);
            this.comboBoxAcquireUnits.Name = "comboBoxAcquireUnits";
            this.comboBoxAcquireUnits.Size = new System.Drawing.Size(88, 21);
            this.comboBoxAcquireUnits.TabIndex = 24;
            this.comboBoxAcquireUnits.SelectedIndexChanged += new System.EventHandler(this.comboBoxAcquireUnits_SelectedIndexChanged);
            // 
            // textBoxAcquireDuration
            // 
            this.textBoxAcquireDuration.Location = new System.Drawing.Point(82, 293);
            this.textBoxAcquireDuration.Name = "textBoxAcquireDuration";
            this.textBoxAcquireDuration.Size = new System.Drawing.Size(78, 20);
            this.textBoxAcquireDuration.TabIndex = 23;
            this.textBoxAcquireDuration.Text = "10";
            this.textBoxAcquireDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(18, 296);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 13);
            this.label16.TabIndex = 22;
            this.label16.Text = "Acquire for";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(220, 165);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(30, 13);
            this.label18.TabIndex = 21;
            this.label18.Text = "usec";
            // 
            // textBoxAcquisitionDelay
            // 
            this.textBoxAcquisitionDelay.Location = new System.Drawing.Point(140, 162);
            this.textBoxAcquisitionDelay.Name = "textBoxAcquisitionDelay";
            this.textBoxAcquisitionDelay.Size = new System.Drawing.Size(78, 20);
            this.textBoxAcquisitionDelay.TabIndex = 20;
            this.textBoxAcquisitionDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxBackToBack
            // 
            this.textBoxBackToBack.Location = new System.Drawing.Point(140, 109);
            this.textBoxBackToBack.Name = "textBoxBackToBack";
            this.textBoxBackToBack.Size = new System.Drawing.Size(78, 20);
            this.textBoxBackToBack.TabIndex = 18;
            this.textBoxBackToBack.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxScansToAverage
            // 
            this.textBoxScansToAverage.Location = new System.Drawing.Point(140, 83);
            this.textBoxScansToAverage.Name = "textBoxScansToAverage";
            this.textBoxScansToAverage.Size = new System.Drawing.Size(78, 20);
            this.textBoxScansToAverage.TabIndex = 16;
            this.textBoxScansToAverage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(220, 59);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(30, 13);
            this.label15.TabIndex = 15;
            this.label15.Text = "usec";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(46, 165);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Acquisition Delay";
            // 
            // textBoxIntegrationTime
            // 
            this.textBoxIntegrationTime.Location = new System.Drawing.Point(140, 56);
            this.textBoxIntegrationTime.Name = "textBoxIntegrationTime";
            this.textBoxIntegrationTime.Size = new System.Drawing.Size(78, 20);
            this.textBoxIntegrationTime.TabIndex = 14;
            this.textBoxIntegrationTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 112);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(126, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Back to Back per Trigger";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(38, 86);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Scans To Average";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(51, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Integration Time";
            // 
            // checkBoxLampEnable
            // 
            this.checkBoxLampEnable.AutoSize = true;
            this.checkBoxLampEnable.Location = new System.Drawing.Point(349, 305);
            this.checkBoxLampEnable.Name = "checkBoxLampEnable";
            this.checkBoxLampEnable.Size = new System.Drawing.Size(88, 17);
            this.checkBoxLampEnable.TabIndex = 37;
            this.checkBoxLampEnable.Text = "Lamp Enable";
            this.checkBoxLampEnable.UseVisualStyleBackColor = true;
            this.checkBoxLampEnable.CheckedChanged += new System.EventHandler(this.checkBoxLampEnable_CheckedChanged);
            // 
            // labelFileSaveError
            // 
            this.labelFileSaveError.AutoSize = true;
            this.labelFileSaveError.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFileSaveError.ForeColor = System.Drawing.Color.Crimson;
            this.labelFileSaveError.Location = new System.Drawing.Point(12, 579);
            this.labelFileSaveError.Name = "labelFileSaveError";
            this.labelFileSaveError.Size = new System.Drawing.Size(106, 13);
            this.labelFileSaveError.TabIndex = 121;
            this.labelFileSaveError.Text = "* File save error *";
            this.labelFileSaveError.Visible = false;
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Location = new System.Drawing.Point(360, 522);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(54, 13);
            this.label134.TabIndex = 120;
            this.label134.Text = "(inclusive)";
            // 
            // textBoxSaveEndRange
            // 
            this.textBoxSaveEndRange.Enabled = false;
            this.textBoxSaveEndRange.Location = new System.Drawing.Point(311, 519);
            this.textBoxSaveEndRange.Name = "textBoxSaveEndRange";
            this.textBoxSaveEndRange.Size = new System.Drawing.Size(46, 20);
            this.textBoxSaveEndRange.TabIndex = 119;
            this.textBoxSaveEndRange.Text = "0";
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(291, 522);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(16, 13);
            this.label133.TabIndex = 118;
            this.label133.Text = "to";
            // 
            // textBoxSaveStartRange
            // 
            this.textBoxSaveStartRange.Enabled = false;
            this.textBoxSaveStartRange.Location = new System.Drawing.Point(242, 519);
            this.textBoxSaveStartRange.Name = "textBoxSaveStartRange";
            this.textBoxSaveStartRange.Size = new System.Drawing.Size(46, 20);
            this.textBoxSaveStartRange.TabIndex = 117;
            this.textBoxSaveStartRange.Text = "0";
            // 
            // buttonSaveDir
            // 
            this.buttonSaveDir.Enabled = false;
            this.buttonSaveDir.Location = new System.Drawing.Point(510, 550);
            this.buttonSaveDir.Name = "buttonSaveDir";
            this.buttonSaveDir.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveDir.TabIndex = 115;
            this.buttonSaveDir.Text = "Directory";
            this.buttonSaveDir.UseVisualStyleBackColor = true;
            this.buttonSaveDir.Click += new System.EventHandler(this.buttonSaveDir_Click);
            // 
            // textBoxSaveFilename
            // 
            this.textBoxSaveFilename.Enabled = false;
            this.textBoxSaveFilename.Location = new System.Drawing.Point(13, 552);
            this.textBoxSaveFilename.Name = "textBoxSaveFilename";
            this.textBoxSaveFilename.Size = new System.Drawing.Size(491, 20);
            this.textBoxSaveFilename.TabIndex = 114;
            this.textBoxSaveFilename.Text = "oceanfx_spectrum.csv";
            this.textBoxSaveFilename.TextChanged += new System.EventHandler(this.textBoxSaveFilename_TextChanged);
            // 
            // checkBoxSaveToFile
            // 
            this.checkBoxSaveToFile.AutoSize = true;
            this.checkBoxSaveToFile.Checked = true;
            this.checkBoxSaveToFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSaveToFile.Location = new System.Drawing.Point(14, 521);
            this.checkBoxSaveToFile.Name = "checkBoxSaveToFile";
            this.checkBoxSaveToFile.Size = new System.Drawing.Size(82, 17);
            this.checkBoxSaveToFile.TabIndex = 113;
            this.checkBoxSaveToFile.Text = "Save to File";
            this.checkBoxSaveToFile.UseVisualStyleBackColor = true;
            this.checkBoxSaveToFile.CheckedChanged += new System.EventHandler(this.checkBoxSaveToFile_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(668, 37);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(87, 13);
            this.label17.TabIndex = 25;
            this.label17.Text = "Firmware Version";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(650, 64);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(105, 13);
            this.label19.TabIndex = 122;
            this.label19.Text = "Firmware Subversion";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(682, 91);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(73, 13);
            this.label20.TabIndex = 123;
            this.label20.Text = "FPGA Version";
            // 
            // textBoxFWVersion
            // 
            this.textBoxFWVersion.Location = new System.Drawing.Point(755, 34);
            this.textBoxFWVersion.Name = "textBoxFWVersion";
            this.textBoxFWVersion.ReadOnly = true;
            this.textBoxFWVersion.Size = new System.Drawing.Size(78, 20);
            this.textBoxFWVersion.TabIndex = 8;
            // 
            // textBoxFWSubversion
            // 
            this.textBoxFWSubversion.Location = new System.Drawing.Point(755, 61);
            this.textBoxFWSubversion.Name = "textBoxFWSubversion";
            this.textBoxFWSubversion.ReadOnly = true;
            this.textBoxFWSubversion.Size = new System.Drawing.Size(78, 20);
            this.textBoxFWSubversion.TabIndex = 124;
            // 
            // textBoxFPGAVersion
            // 
            this.textBoxFPGAVersion.Location = new System.Drawing.Point(755, 88);
            this.textBoxFPGAVersion.Name = "textBoxFPGAVersion";
            this.textBoxFPGAVersion.ReadOnly = true;
            this.textBoxFPGAVersion.Size = new System.Drawing.Size(78, 20);
            this.textBoxFPGAVersion.TabIndex = 125;
            // 
            // textBoxSerialNum
            // 
            this.textBoxSerialNum.Location = new System.Drawing.Point(755, 115);
            this.textBoxSerialNum.Name = "textBoxSerialNum";
            this.textBoxSerialNum.ReadOnly = true;
            this.textBoxSerialNum.Size = new System.Drawing.Size(78, 20);
            this.textBoxSerialNum.TabIndex = 127;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(682, 118);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(73, 13);
            this.label21.TabIndex = 126;
            this.label21.Text = "Serial Number";
            // 
            // buttonStartAcquisition
            // 
            this.buttonStartAcquisition.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonStartAcquisition.Enabled = false;
            this.buttonStartAcquisition.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartAcquisition.ForeColor = System.Drawing.Color.White;
            this.buttonStartAcquisition.Location = new System.Drawing.Point(127, 627);
            this.buttonStartAcquisition.Name = "buttonStartAcquisition";
            this.buttonStartAcquisition.Size = new System.Drawing.Size(163, 42);
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
            this.buttonAbortAcquisition.Location = new System.Drawing.Point(336, 627);
            this.buttonAbortAcquisition.Name = "buttonAbortAcquisition";
            this.buttonAbortAcquisition.Size = new System.Drawing.Size(168, 42);
            this.buttonAbortAcquisition.TabIndex = 129;
            this.buttonAbortAcquisition.Text = "Abort Acquisition";
            this.buttonAbortAcquisition.UseVisualStyleBackColor = false;
            this.buttonAbortAcquisition.Click += new System.EventHandler(this.buttonAbortAcquisition_Click);
            // 
            // labelSaveStart
            // 
            this.labelSaveStart.AutoSize = true;
            this.labelSaveStart.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelSaveStart.Location = new System.Drawing.Point(242, 502);
            this.labelSaveStart.Name = "labelSaveStart";
            this.labelSaveStart.Size = new System.Drawing.Size(13, 13);
            this.labelSaveStart.TabIndex = 131;
            this.labelSaveStart.Text = "0";
            // 
            // labelSaveEnd
            // 
            this.labelSaveEnd.AutoSize = true;
            this.labelSaveEnd.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelSaveEnd.Location = new System.Drawing.Point(311, 502);
            this.labelSaveEnd.Name = "labelSaveEnd";
            this.labelSaveEnd.Size = new System.Drawing.Size(31, 13);
            this.labelSaveEnd.TabIndex = 132;
            this.labelSaveEnd.Text = "2135";
            // 
            // comboBoxSaveRangeUnits
            // 
            this.comboBoxSaveRangeUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSaveRangeUnits.FormattingEnabled = true;
            this.comboBoxSaveRangeUnits.Items.AddRange(new object[] {
            "Pixel Range",
            "Wavelength Range"});
            this.comboBoxSaveRangeUnits.Location = new System.Drawing.Point(114, 519);
            this.comboBoxSaveRangeUnits.Name = "comboBoxSaveRangeUnits";
            this.comboBoxSaveRangeUnits.Size = new System.Drawing.Size(117, 21);
            this.comboBoxSaveRangeUnits.TabIndex = 133;
            this.comboBoxSaveRangeUnits.SelectedIndexChanged += new System.EventHandler(this.comboBoxSaveRangeUnits_SelectedIndexChanged);
            // 
            // labelCurSpectraPerSec
            // 
            this.labelCurSpectraPerSec.AutoSize = true;
            this.labelCurSpectraPerSec.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurSpectraPerSec.ForeColor = System.Drawing.Color.SteelBlue;
            this.labelCurSpectraPerSec.Location = new System.Drawing.Point(773, 349);
            this.labelCurSpectraPerSec.Name = "labelCurSpectraPerSec";
            this.labelCurSpectraPerSec.Size = new System.Drawing.Size(137, 18);
            this.labelCurSpectraPerSec.TabIndex = 134;
            this.labelCurSpectraPerSec.Text = "4500 spectra/sec";
            this.labelCurSpectraPerSec.Visible = false;
            // 
            // textBoxNumInBuffer
            // 
            this.textBoxNumInBuffer.Location = new System.Drawing.Point(755, 314);
            this.textBoxNumInBuffer.Name = "textBoxNumInBuffer";
            this.textBoxNumInBuffer.ReadOnly = true;
            this.textBoxNumInBuffer.Size = new System.Drawing.Size(78, 20);
            this.textBoxNumInBuffer.TabIndex = 136;
            this.textBoxNumInBuffer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(640, 317);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(111, 13);
            this.label26.TabIndex = 135;
            this.label26.Text = "Num Spectra in Buffer";
            // 
            // buttonUpdateSpectraInBuffer
            // 
            this.buttonUpdateSpectraInBuffer.Enabled = false;
            this.buttonUpdateSpectraInBuffer.Location = new System.Drawing.Point(839, 312);
            this.buttonUpdateSpectraInBuffer.Name = "buttonUpdateSpectraInBuffer";
            this.buttonUpdateSpectraInBuffer.Size = new System.Drawing.Size(53, 23);
            this.buttonUpdateSpectraInBuffer.TabIndex = 137;
            this.buttonUpdateSpectraInBuffer.Text = "Update";
            this.buttonUpdateSpectraInBuffer.UseVisualStyleBackColor = true;
            this.buttonUpdateSpectraInBuffer.Click += new System.EventHandler(this.buttonUpdateSpectraInBuffer_Click);
            // 
            // labelConnectStatus
            // 
            this.labelConnectStatus.AutoSize = true;
            this.labelConnectStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnectStatus.ForeColor = System.Drawing.Color.Crimson;
            this.labelConnectStatus.Location = new System.Drawing.Point(245, 115);
            this.labelConnectStatus.MaximumSize = new System.Drawing.Size(102, 15);
            this.labelConnectStatus.MinimumSize = new System.Drawing.Size(102, 15);
            this.labelConnectStatus.Name = "labelConnectStatus";
            this.labelConnectStatus.Size = new System.Drawing.Size(102, 15);
            this.labelConnectStatus.TabIndex = 138;
            this.labelConnectStatus.Text = "Failed to Connect";
            this.labelConnectStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelConnectStatus.Visible = false;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(259, 81);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 139;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonTakeDark
            // 
            this.buttonTakeDark.Enabled = false;
            this.buttonTakeDark.Location = new System.Drawing.Point(349, 341);
            this.buttonTakeDark.Name = "buttonTakeDark";
            this.buttonTakeDark.Size = new System.Drawing.Size(96, 23);
            this.buttonTakeDark.TabIndex = 140;
            this.buttonTakeDark.Text = "Take Dark";
            this.buttonTakeDark.UseVisualStyleBackColor = true;
            this.buttonTakeDark.Click += new System.EventHandler(this.buttonTakeDark_Click);
            // 
            // comboBoxSaveType
            // 
            this.comboBoxSaveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSaveType.FormattingEnabled = true;
            this.comboBoxSaveType.Items.AddRange(new object[] {
            "Raw Spectrum",
            "Dark Corrected",
            "Absorbance",
            "Transmission"});
            this.comboBoxSaveType.Location = new System.Drawing.Point(484, 519);
            this.comboBoxSaveType.Name = "comboBoxSaveType";
            this.comboBoxSaveType.Size = new System.Drawing.Size(101, 21);
            this.comboBoxSaveType.TabIndex = 141;
            this.comboBoxSaveType.SelectedIndexChanged += new System.EventHandler(this.comboBoxSaveType_SelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(435, 522);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(46, 13);
            this.label27.TabIndex = 142;
            this.label27.Text = "Save as";
            // 
            // labelDarkStatus
            // 
            this.labelDarkStatus.AutoSize = true;
            this.labelDarkStatus.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelDarkStatus.Location = new System.Drawing.Point(454, 347);
            this.labelDarkStatus.Name = "labelDarkStatus";
            this.labelDarkStatus.Size = new System.Drawing.Size(49, 13);
            this.labelDarkStatus.TabIndex = 144;
            this.labelDarkStatus.Text = "00:00:00";
            this.labelDarkStatus.Visible = false;
            // 
            // labelSaveTypeStatus
            // 
            this.labelSaveTypeStatus.AutoSize = true;
            this.labelSaveTypeStatus.ForeColor = System.Drawing.Color.Crimson;
            this.labelSaveTypeStatus.Location = new System.Drawing.Point(484, 500);
            this.labelSaveTypeStatus.Name = "labelSaveTypeStatus";
            this.labelSaveTypeStatus.Size = new System.Drawing.Size(88, 13);
            this.labelSaveTypeStatus.TabIndex = 145;
            this.labelSaveTypeStatus.Text = "no dark available";
            this.labelSaveTypeStatus.Visible = false;
            // 
            // labelReferenceStatus
            // 
            this.labelReferenceStatus.AutoSize = true;
            this.labelReferenceStatus.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelReferenceStatus.Location = new System.Drawing.Point(454, 424);
            this.labelReferenceStatus.Name = "labelReferenceStatus";
            this.labelReferenceStatus.Size = new System.Drawing.Size(49, 13);
            this.labelReferenceStatus.TabIndex = 147;
            this.labelReferenceStatus.Text = "00:00:00";
            this.labelReferenceStatus.Visible = false;
            // 
            // buttonTakeReference
            // 
            this.buttonTakeReference.Enabled = false;
            this.buttonTakeReference.Location = new System.Drawing.Point(349, 419);
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
            this.buttonClearBuffer.Location = new System.Drawing.Point(898, 311);
            this.buttonClearBuffer.Name = "buttonClearBuffer";
            this.buttonClearBuffer.Size = new System.Drawing.Size(53, 23);
            this.buttonClearBuffer.TabIndex = 148;
            this.buttonClearBuffer.Text = "Clear";
            this.buttonClearBuffer.UseVisualStyleBackColor = true;
            this.buttonClearBuffer.Click += new System.EventHandler(this.buttonClearBuffer_Click);
            // 
            // buttonClearDark
            // 
            this.buttonClearDark.Enabled = false;
            this.buttonClearDark.Location = new System.Drawing.Point(349, 370);
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
            this.buttonClearReference.Location = new System.Drawing.Point(349, 448);
            this.buttonClearReference.Name = "buttonClearReference";
            this.buttonClearReference.Size = new System.Drawing.Size(96, 23);
            this.buttonClearReference.TabIndex = 150;
            this.buttonClearReference.Text = "Clear Reference";
            this.buttonClearReference.UseVisualStyleBackColor = true;
            this.buttonClearReference.Click += new System.EventHandler(this.buttonClearReference_Click);
            // 
            // chartSpectrum
            // 
            this.chartSpectrum.BackColor = System.Drawing.SystemColors.Control;
            chartArea2.AxisX.LabelStyle.Format = "#.";
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea2.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.AxisY.LabelStyle.Format = "#.";
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea2.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea2.BackColor = System.Drawing.SystemColors.Control;
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.CursorY.IsUserEnabled = true;
            chartArea2.CursorY.IsUserSelectionEnabled = true;
            chartArea2.Name = "ChartArea1";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 100F;
            chartArea2.Position.Width = 100F;
            this.chartSpectrum.ChartAreas.Add(chartArea2);
            this.chartSpectrum.Location = new System.Drawing.Point(591, 370);
            this.chartSpectrum.Name = "chartSpectrum";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.DimGray;
            series4.Name = "SeriesDark";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.Goldenrod;
            series5.Name = "SeriesReference";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.DodgerBlue;
            series6.Name = "SeriesResult";
            this.chartSpectrum.Series.Add(series4);
            this.chartSpectrum.Series.Add(series5);
            this.chartSpectrum.Series.Add(series6);
            this.chartSpectrum.Size = new System.Drawing.Size(483, 370);
            this.chartSpectrum.TabIndex = 151;
            this.chartSpectrum.Text = "Spectrum";
            // 
            // checkBoxClearBufferBeforeAcquisition
            // 
            this.checkBoxClearBufferBeforeAcquisition.AutoSize = true;
            this.checkBoxClearBufferBeforeAcquisition.Checked = true;
            this.checkBoxClearBufferBeforeAcquisition.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxClearBufferBeforeAcquisition.Location = new System.Drawing.Point(131, 603);
            this.checkBoxClearBufferBeforeAcquisition.Name = "checkBoxClearBufferBeforeAcquisition";
            this.checkBoxClearBufferBeforeAcquisition.Size = new System.Drawing.Size(169, 17);
            this.checkBoxClearBufferBeforeAcquisition.TabIndex = 142;
            this.checkBoxClearBufferBeforeAcquisition.Text = "Clear Buffer Before Acquisition";
            this.checkBoxClearBufferBeforeAcquisition.UseVisualStyleBackColor = true;
            this.checkBoxClearBufferBeforeAcquisition.CheckedChanged += new System.EventHandler(this.checkBoxClearBufferBeforeAcquisition_CheckedChanged);
            // 
            // buttonUpdateFileTime
            // 
            this.buttonUpdateFileTime.Enabled = false;
            this.buttonUpdateFileTime.Location = new System.Drawing.Point(510, 579);
            this.buttonUpdateFileTime.Name = "buttonUpdateFileTime";
            this.buttonUpdateFileTime.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateFileTime.TabIndex = 152;
            this.buttonUpdateFileTime.Text = "Update";
            this.buttonUpdateFileTime.UseVisualStyleBackColor = true;
            this.buttonUpdateFileTime.Click += new System.EventHandler(this.buttonUpdateFileTime_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(15, 688);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(74, 13);
            this.label23.TabIndex = 153;
            this.label23.Text = "Total Spectra:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(421, 688);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(82, 13);
            this.label24.TabIndex = 154;
            this.label24.Text = "Total Requests:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(215, 688);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(69, 13);
            this.label31.TabIndex = 155;
            this.label31.Text = "Spectra/sec:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(29, 710);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(60, 13);
            this.label32.TabIndex = 156;
            this.label32.Text = "Total Time:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(192, 710);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(92, 13);
            this.label33.TabIndex = 157;
            this.label33.Text = "Spectra/Request:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(440, 710);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(63, 13);
            this.label34.TabIndex = 158;
            this.label34.Text = "Total Bytes:";
            // 
            // labelTotalTime
            // 
            this.labelTotalTime.AutoSize = true;
            this.labelTotalTime.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelTotalTime.Location = new System.Drawing.Point(92, 710);
            this.labelTotalTime.Name = "labelTotalTime";
            this.labelTotalTime.Size = new System.Drawing.Size(49, 13);
            this.labelTotalTime.TabIndex = 159;
            this.labelTotalTime.Text = "00:00:00";
            // 
            // labelTotalSpectra
            // 
            this.labelTotalSpectra.AutoSize = true;
            this.labelTotalSpectra.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelTotalSpectra.Location = new System.Drawing.Point(92, 688);
            this.labelTotalSpectra.Name = "labelTotalSpectra";
            this.labelTotalSpectra.Size = new System.Drawing.Size(13, 13);
            this.labelTotalSpectra.TabIndex = 160;
            this.labelTotalSpectra.Text = "0";
            // 
            // labelSpectraPerSec
            // 
            this.labelSpectraPerSec.AutoSize = true;
            this.labelSpectraPerSec.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelSpectraPerSec.Location = new System.Drawing.Point(290, 688);
            this.labelSpectraPerSec.Name = "labelSpectraPerSec";
            this.labelSpectraPerSec.Size = new System.Drawing.Size(13, 13);
            this.labelSpectraPerSec.TabIndex = 161;
            this.labelSpectraPerSec.Text = "0";
            // 
            // labelSpectraPerRequest
            // 
            this.labelSpectraPerRequest.AutoSize = true;
            this.labelSpectraPerRequest.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelSpectraPerRequest.Location = new System.Drawing.Point(291, 710);
            this.labelSpectraPerRequest.Name = "labelSpectraPerRequest";
            this.labelSpectraPerRequest.Size = new System.Drawing.Size(13, 13);
            this.labelSpectraPerRequest.TabIndex = 162;
            this.labelSpectraPerRequest.Text = "0";
            // 
            // labelTotalRequests
            // 
            this.labelTotalRequests.AutoSize = true;
            this.labelTotalRequests.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelTotalRequests.Location = new System.Drawing.Point(509, 688);
            this.labelTotalRequests.Name = "labelTotalRequests";
            this.labelTotalRequests.Size = new System.Drawing.Size(13, 13);
            this.labelTotalRequests.TabIndex = 163;
            this.labelTotalRequests.Text = "0";
            // 
            // labelTotalBytes
            // 
            this.labelTotalBytes.AutoSize = true;
            this.labelTotalBytes.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelTotalBytes.Location = new System.Drawing.Point(509, 710);
            this.labelTotalBytes.Name = "labelTotalBytes";
            this.labelTotalBytes.Size = new System.Drawing.Size(13, 13);
            this.labelTotalBytes.TabIndex = 164;
            this.labelTotalBytes.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 752);
            this.Controls.Add(this.labelTotalBytes);
            this.Controls.Add(this.labelTotalRequests);
            this.Controls.Add(this.labelSpectraPerRequest);
            this.Controls.Add(this.labelSpectraPerSec);
            this.Controls.Add(this.labelTotalSpectra);
            this.Controls.Add(this.labelTotalTime);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.buttonUpdateFileTime);
            this.Controls.Add(this.checkBoxClearBufferBeforeAcquisition);
            this.Controls.Add(this.chartSpectrum);
            this.Controls.Add(this.buttonClearReference);
            this.Controls.Add(this.buttonClearDark);
            this.Controls.Add(this.buttonClearBuffer);
            this.Controls.Add(this.labelReferenceStatus);
            this.Controls.Add(this.buttonTakeReference);
            this.Controls.Add(this.labelSaveTypeStatus);
            this.Controls.Add(this.labelDarkStatus);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.comboBoxSaveType);
            this.Controls.Add(this.buttonTakeDark);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.labelConnectStatus);
            this.Controls.Add(this.buttonUpdateSpectraInBuffer);
            this.Controls.Add(this.textBoxNumInBuffer);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.labelCurSpectraPerSec);
            this.Controls.Add(this.comboBoxSaveRangeUnits);
            this.Controls.Add(this.labelSaveEnd);
            this.Controls.Add(this.labelSaveStart);
            this.Controls.Add(this.buttonAbortAcquisition);
            this.Controls.Add(this.buttonStartAcquisition);
            this.Controls.Add(this.textBoxSerialNum);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.textBoxFPGAVersion);
            this.Controls.Add(this.textBoxFWSubversion);
            this.Controls.Add(this.textBoxFWVersion);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.labelFileSaveError);
            this.Controls.Add(this.label134);
            this.Controls.Add(this.textBoxSaveEndRange);
            this.Controls.Add(this.label133);
            this.Controls.Add(this.textBoxSaveStartRange);
            this.Controls.Add(this.buttonSaveDir);
            this.Controls.Add(this.textBoxSaveFilename);
            this.Controls.Add(this.checkBoxSaveToFile);
            this.Controls.Add(this.checkBoxLampEnable);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxContinuousStrobe);
            this.Controls.Add(this.groupBoxSingleStrobe);
            this.Controls.Add(this.buttonRescan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewUSBDeviceList);
            this.Controls.Add(this.dataGridViewIPDevices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "FX Streamer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIPDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUSBDeviceList)).EndInit();
            this.groupBoxSingleStrobe.ResumeLayout(false);
            this.groupBoxSingleStrobe.PerformLayout();
            this.groupBoxContinuousStrobe.ResumeLayout(false);
            this.groupBoxContinuousStrobe.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSpectrum)).EndInit();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonRescan;
        private System.Windows.Forms.GroupBox groupBoxSingleStrobe;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxSingleStrobeEnabled;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxSingleStrobePulseWidth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxSingleStrobePulseDelay;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBoxContinuousStrobe;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxContinuousStrobeWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxContinuousStrobePeriod;
        private System.Windows.Forms.CheckBox checkBoxContinuousStrobeEnabled;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxAcquireUnits;
        private System.Windows.Forms.TextBox textBoxAcquireDuration;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxAcquisitionDelay;
        private System.Windows.Forms.TextBox textBoxBackToBack;
        private System.Windows.Forms.TextBox textBoxScansToAverage;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxIntegrationTime;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBoxLampEnable;
        private System.Windows.Forms.Label labelFileSaveError;
        private System.Windows.Forms.Label label134;
        private System.Windows.Forms.TextBox textBoxSaveEndRange;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.TextBox textBoxSaveStartRange;
        private System.Windows.Forms.Button buttonSaveDir;
        private System.Windows.Forms.TextBox textBoxSaveFilename;
        private System.Windows.Forms.CheckBox checkBoxSaveToFile;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxFWVersion;
        private System.Windows.Forms.TextBox textBoxFWSubversion;
        private System.Windows.Forms.TextBox textBoxFPGAVersion;
        private System.Windows.Forms.TextBox textBoxSerialNum;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button buttonStartAcquisition;
        private System.Windows.Forms.Button buttonAbortAcquisition;
        private System.Windows.Forms.Label labelSaveStart;
        private System.Windows.Forms.Label labelSaveEnd;
        private System.Windows.Forms.ComboBox comboBoxSaveRangeUnits;
        private System.Windows.Forms.Label labelCurSpectraPerSec;
        private System.Windows.Forms.ComboBox comboBoxTriggerMode;
        private System.Windows.Forms.CheckBox checkBoxSingleSWTrigger;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBoxNumInBuffer;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button buttonUpdateSpectraInBuffer;
        private System.Windows.Forms.Label labelConnectStatus;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonTakeDark;
        private System.Windows.Forms.ComboBox comboBoxSaveType;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label labelDarkStatus;
        private System.Windows.Forms.Label labelSaveTypeStatus;
        private System.Windows.Forms.Label labelReferenceStatus;
        private System.Windows.Forms.Button buttonTakeReference;
        private System.Windows.Forms.Button buttonClearBuffer;
        private System.Windows.Forms.CheckBox checkBoxBufferingEnabled;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox textBoxSpectraPerRequest;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button buttonClearDark;
        private System.Windows.Forms.Button buttonClearReference;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSpectrum;
        private System.Windows.Forms.CheckBox checkBoxClearBufferBeforeAcquisition;
        private System.Windows.Forms.Button buttonUpdateFileTime;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label labelTotalTime;
        private System.Windows.Forms.Label labelTotalSpectra;
        private System.Windows.Forms.Label labelSpectraPerSec;
        private System.Windows.Forms.Label labelSpectraPerRequest;
        private System.Windows.Forms.Label labelTotalRequests;
        private System.Windows.Forms.Label labelTotalBytes;
    }
}

