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
    partial class WavelengthHub
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonAddWavelength = new System.Windows.Forms.Button();
            this.buttonRemoveWavelength = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.labelWavelengthsCurrentlySaved = new System.Windows.Forms.Label();
            this.labelWavelengthsCurrentlySavedTitle = new System.Windows.Forms.Label();
            this.buttonSaveWavelengthRange = new System.Windows.Forms.Button();
            this.checkBoxSaveWavelengthRange = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.wavelengthsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.removeToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.addToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxSaving = new System.Windows.Forms.GroupBox();
            this.groupBoxtriggering = new System.Windows.Forms.GroupBox();
            this.checkBoxTrigeringAllAboveEnabled = new System.Windows.Forms.CheckBox();
            this.checkBoxTriggeringEnable = new System.Windows.Forms.CheckBox();
            this.labelTriggeringInfo2 = new System.Windows.Forms.Label();
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thresholdValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isForThresholdingDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.thresholdAboveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isToBePlottedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isToBeSavedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.seriesColorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wavelengthsBindingSource)).BeginInit();
            this.groupBoxSaving.SuspendLayout();
            this.groupBoxtriggering.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAddWavelength
            // 
            this.buttonAddWavelength.Location = new System.Drawing.Point(12, 286);
            this.buttonAddWavelength.Name = "buttonAddWavelength";
            this.buttonAddWavelength.Size = new System.Drawing.Size(280, 23);
            this.buttonAddWavelength.TabIndex = 176;
            this.buttonAddWavelength.Text = "Add";
            this.buttonAddWavelength.UseVisualStyleBackColor = true;
            this.buttonAddWavelength.Click += new System.EventHandler(this.ButtonAddWavelength_Click);
            // 
            // buttonRemoveWavelength
            // 
            this.buttonRemoveWavelength.Location = new System.Drawing.Point(298, 286);
            this.buttonRemoveWavelength.Name = "buttonRemoveWavelength";
            this.buttonRemoveWavelength.Size = new System.Drawing.Size(280, 23);
            this.buttonRemoveWavelength.TabIndex = 177;
            this.buttonRemoveWavelength.Text = "Remove";
            this.buttonRemoveWavelength.UseVisualStyleBackColor = true;
            this.buttonRemoveWavelength.Click += new System.EventHandler(this.ButtonRemoveWavelength_Click);
            // 
            // labelWavelengthsCurrentlySaved
            // 
            this.labelWavelengthsCurrentlySaved.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelWavelengthsCurrentlySaved.Location = new System.Drawing.Point(174, 47);
            this.labelWavelengthsCurrentlySaved.Name = "labelWavelengthsCurrentlySaved";
            this.labelWavelengthsCurrentlySaved.Size = new System.Drawing.Size(382, 25);
            this.labelWavelengthsCurrentlySaved.TabIndex = 222;
            this.labelWavelengthsCurrentlySaved.Text = "none";
            this.labelWavelengthsCurrentlySaved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelWavelengthsCurrentlySavedTitle
            // 
            this.labelWavelengthsCurrentlySavedTitle.Location = new System.Drawing.Point(3, 47);
            this.labelWavelengthsCurrentlySavedTitle.Name = "labelWavelengthsCurrentlySavedTitle";
            this.labelWavelengthsCurrentlySavedTitle.Size = new System.Drawing.Size(162, 25);
            this.labelWavelengthsCurrentlySavedTitle.TabIndex = 221;
            this.labelWavelengthsCurrentlySavedTitle.Text = "Wavelengths currently saved:";
            this.labelWavelengthsCurrentlySavedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonSaveWavelengthRange
            // 
            this.buttonSaveWavelengthRange.Location = new System.Drawing.Point(285, 21);
            this.buttonSaveWavelengthRange.Name = "buttonSaveWavelengthRange";
            this.buttonSaveWavelengthRange.Size = new System.Drawing.Size(278, 23);
            this.buttonSaveWavelengthRange.TabIndex = 189;
            this.buttonSaveWavelengthRange.Text = "Select range";
            this.buttonSaveWavelengthRange.UseVisualStyleBackColor = true;
            this.buttonSaveWavelengthRange.Click += new System.EventHandler(this.buttonSaveWavelengthRange_Click);
            // 
            // checkBoxSaveWavelengthRange
            // 
            this.checkBoxSaveWavelengthRange.AutoSize = true;
            this.checkBoxSaveWavelengthRange.Location = new System.Drawing.Point(6, 25);
            this.checkBoxSaveWavelengthRange.Name = "checkBoxSaveWavelengthRange";
            this.checkBoxSaveWavelengthRange.Size = new System.Drawing.Size(259, 17);
            this.checkBoxSaveWavelengthRange.TabIndex = 188;
            this.checkBoxSaveWavelengthRange.Text = "Save range instead of individual wavelengths";
            this.checkBoxSaveWavelengthRange.UseVisualStyleBackColor = true;
            this.checkBoxSaveWavelengthRange.CheckedChanged += new System.EventHandler(this.checkBoxSaveWavelengthRange_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.valueDataGridViewTextBoxColumn,
            this.thresholdValueDataGridViewTextBoxColumn,
            this.isForThresholdingDataGridViewCheckBoxColumn,
            this.thresholdAboveDataGridViewCheckBoxColumn,
            this.isToBePlottedDataGridViewCheckBoxColumn,
            this.isToBeSavedDataGridViewCheckBoxColumn,
            this.seriesColorDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.wavelengthsBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 7);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(566, 273);
            this.dataGridView1.TabIndex = 188;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValidated);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // wavelengthsBindingSource
            // 
            this.wavelengthsBindingSource.DataMember = "Wavelengths";
            this.wavelengthsBindingSource.DataSource = typeof(spectra.processing.WavelengthManager);
            // 
            // groupBoxSaving
            // 
            this.groupBoxSaving.Controls.Add(this.buttonSaveWavelengthRange);
            this.groupBoxSaving.Controls.Add(this.labelWavelengthsCurrentlySavedTitle);
            this.groupBoxSaving.Controls.Add(this.labelWavelengthsCurrentlySaved);
            this.groupBoxSaving.Controls.Add(this.checkBoxSaveWavelengthRange);
            this.groupBoxSaving.Location = new System.Drawing.Point(12, 315);
            this.groupBoxSaving.Name = "groupBoxSaving";
            this.groupBoxSaving.Size = new System.Drawing.Size(566, 80);
            this.groupBoxSaving.TabIndex = 223;
            this.groupBoxSaving.TabStop = false;
            this.groupBoxSaving.Text = "Saving";
            // 
            // groupBoxtriggering
            // 
            this.groupBoxtriggering.Controls.Add(this.checkBoxTrigeringAllAboveEnabled);
            this.groupBoxtriggering.Controls.Add(this.checkBoxTriggeringEnable);
            this.groupBoxtriggering.Controls.Add(this.labelTriggeringInfo2);
            this.groupBoxtriggering.Location = new System.Drawing.Point(12, 402);
            this.groupBoxtriggering.Name = "groupBoxtriggering";
            this.groupBoxtriggering.Size = new System.Drawing.Size(566, 77);
            this.groupBoxtriggering.TabIndex = 224;
            this.groupBoxtriggering.TabStop = false;
            this.groupBoxtriggering.Text = "Triggering";
            // 
            // checkBoxTrigeringAllAboveEnabled
            // 
            this.checkBoxTrigeringAllAboveEnabled.AutoSize = true;
            this.checkBoxTrigeringAllAboveEnabled.Location = new System.Drawing.Point(286, 21);
            this.checkBoxTrigeringAllAboveEnabled.Name = "checkBoxTrigeringAllAboveEnabled";
            this.checkBoxTrigeringAllAboveEnabled.Size = new System.Drawing.Size(239, 17);
            this.checkBoxTrigeringAllAboveEnabled.TabIndex = 228;
            this.checkBoxTrigeringAllAboveEnabled.Text = "All thresholds must be satisfied to trigger";
            this.checkBoxTrigeringAllAboveEnabled.UseVisualStyleBackColor = true;
            this.checkBoxTrigeringAllAboveEnabled.CheckedChanged += new System.EventHandler(this.checkBoxTrigeringAllAboveEnabled_CheckedChanged);
            // 
            // checkBoxTriggeringEnable
            // 
            this.checkBoxTriggeringEnable.AutoSize = true;
            this.checkBoxTriggeringEnable.Location = new System.Drawing.Point(9, 21);
            this.checkBoxTriggeringEnable.Name = "checkBoxTriggeringEnable";
            this.checkBoxTriggeringEnable.Size = new System.Drawing.Size(61, 17);
            this.checkBoxTriggeringEnable.TabIndex = 227;
            this.checkBoxTriggeringEnable.Text = "Enable";
            this.checkBoxTriggeringEnable.UseVisualStyleBackColor = true;
            this.checkBoxTriggeringEnable.CheckedChanged += new System.EventHandler(this.checkBoxTriggeringEnable_CheckedChanged);
            // 
            // labelTriggeringInfo2
            // 
            this.labelTriggeringInfo2.Location = new System.Drawing.Point(6, 50);
            this.labelTriggeringInfo2.Name = "labelTriggeringInfo2";
            this.labelTriggeringInfo2.Size = new System.Drawing.Size(557, 23);
            this.labelTriggeringInfo2.TabIndex = 226;
            this.labelTriggeringInfo2.Text = "More parameters can be set in the Arduino tab of the main application window.";
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            dataGridViewCellStyle1.NullValue = null;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.valueDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.valueDataGridViewTextBoxColumn.FillWeight = 1F;
            this.valueDataGridViewTextBoxColumn.HeaderText = "Wavelength";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            this.valueDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.valueDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.valueDataGridViewTextBoxColumn.ToolTipText = "Wavelength in nm.";
            // 
            // thresholdValueDataGridViewTextBoxColumn
            // 
            this.thresholdValueDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.thresholdValueDataGridViewTextBoxColumn.DataPropertyName = "ThresholdValue";
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.thresholdValueDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.thresholdValueDataGridViewTextBoxColumn.FillWeight = 1F;
            this.thresholdValueDataGridViewTextBoxColumn.HeaderText = "Threshold";
            this.thresholdValueDataGridViewTextBoxColumn.Name = "thresholdValueDataGridViewTextBoxColumn";
            this.thresholdValueDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.thresholdValueDataGridViewTextBoxColumn.ToolTipText = "Intiensity threshold for triggering.";
            // 
            // isForThresholdingDataGridViewCheckBoxColumn
            // 
            this.isForThresholdingDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.isForThresholdingDataGridViewCheckBoxColumn.DataPropertyName = "IsForThresholding";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.isForThresholdingDataGridViewCheckBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.isForThresholdingDataGridViewCheckBoxColumn.FillWeight = 1F;
            this.isForThresholdingDataGridViewCheckBoxColumn.HeaderText = "Trigger";
            this.isForThresholdingDataGridViewCheckBoxColumn.Name = "isForThresholdingDataGridViewCheckBoxColumn";
            this.isForThresholdingDataGridViewCheckBoxColumn.ToolTipText = "Use this wavelength to trigger.";
            // 
            // thresholdAboveDataGridViewCheckBoxColumn
            // 
            this.thresholdAboveDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.thresholdAboveDataGridViewCheckBoxColumn.DataPropertyName = "IsThresholdAbove";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = false;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.thresholdAboveDataGridViewCheckBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.thresholdAboveDataGridViewCheckBoxColumn.FillWeight = 1.4F;
            this.thresholdAboveDataGridViewCheckBoxColumn.HeaderText = "Above threshold";
            this.thresholdAboveDataGridViewCheckBoxColumn.Name = "thresholdAboveDataGridViewCheckBoxColumn";
            this.thresholdAboveDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.thresholdAboveDataGridViewCheckBoxColumn.ToolTipText = "Intensity must be above (or below if unchecked) to trigger.";
            // 
            // isToBePlottedDataGridViewCheckBoxColumn
            // 
            this.isToBePlottedDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.isToBePlottedDataGridViewCheckBoxColumn.DataPropertyName = "IsToBePlotted";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.NullValue = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.isToBePlottedDataGridViewCheckBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.isToBePlottedDataGridViewCheckBoxColumn.FillWeight = 1F;
            this.isToBePlottedDataGridViewCheckBoxColumn.HeaderText = "Time series";
            this.isToBePlottedDataGridViewCheckBoxColumn.Name = "isToBePlottedDataGridViewCheckBoxColumn";
            this.isToBePlottedDataGridViewCheckBoxColumn.ToolTipText = "Plot this wavelength in time series view.";
            // 
            // isToBeSavedDataGridViewCheckBoxColumn
            // 
            this.isToBeSavedDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.isToBeSavedDataGridViewCheckBoxColumn.DataPropertyName = "IsToBeSaved";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.NullValue = false;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.isToBeSavedDataGridViewCheckBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.isToBeSavedDataGridViewCheckBoxColumn.FillWeight = 1F;
            this.isToBeSavedDataGridViewCheckBoxColumn.HeaderText = "Save";
            this.isToBeSavedDataGridViewCheckBoxColumn.Name = "isToBeSavedDataGridViewCheckBoxColumn";
            this.isToBeSavedDataGridViewCheckBoxColumn.ToolTipText = "Save this wavelength\'s intensity value to disk.";
            // 
            // seriesColorDataGridViewTextBoxColumn
            // 
            this.seriesColorDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.seriesColorDataGridViewTextBoxColumn.FillWeight = 1F;
            this.seriesColorDataGridViewTextBoxColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.seriesColorDataGridViewTextBoxColumn.HeaderText = "Color";
            this.seriesColorDataGridViewTextBoxColumn.Name = "seriesColorDataGridViewTextBoxColumn";
            this.seriesColorDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.seriesColorDataGridViewTextBoxColumn.ToolTipText = "Color of the line in the plot.";
            // 
            // WavelengthHub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 487);
            this.Controls.Add(this.groupBoxtriggering);
            this.Controls.Add(this.groupBoxSaving);
            this.Controls.Add(this.buttonRemoveWavelength);
            this.Controls.Add(this.buttonAddWavelength);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WavelengthHub";
            this.Text = "Wavelength Hub";
            this.Activated += new System.EventHandler(this.WavelengthHub_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ThresholdingSettings_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wavelengthsBindingSource)).EndInit();
            this.groupBoxSaving.ResumeLayout(false);
            this.groupBoxSaving.PerformLayout();
            this.groupBoxtriggering.ResumeLayout(false);
            this.groupBoxtriggering.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonAddWavelength;
        private System.Windows.Forms.Button buttonRemoveWavelength;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource wavelengthsBindingSource;
        private System.Windows.Forms.ToolTip removeToolTip;
        private System.Windows.Forms.ToolTip addToolTip;
        private System.Windows.Forms.Button buttonSaveWavelengthRange;
        private System.Windows.Forms.CheckBox checkBoxSaveWavelengthRange;
        private System.Windows.Forms.Label labelWavelengthsCurrentlySavedTitle;
        private System.Windows.Forms.Label labelWavelengthsCurrentlySaved;
        private System.Windows.Forms.GroupBox groupBoxSaving;
        private System.Windows.Forms.GroupBox groupBoxtriggering;
        private System.Windows.Forms.CheckBox checkBoxTrigeringAllAboveEnabled;
        private System.Windows.Forms.CheckBox checkBoxTriggeringEnable;
        private System.Windows.Forms.Label labelTriggeringInfo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn thresholdValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isForThresholdingDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn thresholdAboveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isToBePlottedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isToBeSavedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn seriesColorDataGridViewTextBoxColumn;
    }
}