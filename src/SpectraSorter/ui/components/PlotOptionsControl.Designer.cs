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
    partial class PlotOptionsControl
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
            this.groupBoxYAxis = new System.Windows.Forms.GroupBox();
            this.buttonYAxisReset = new System.Windows.Forms.Button();
            this.checkBoxYAxisAutoScale = new System.Windows.Forms.CheckBox();
            this.textBoxYAxisRangeMax = new System.Windows.Forms.TextBox();
            this.labelYAxisRangeMin = new System.Windows.Forms.Label();
            this.labelYAxisRangeMax = new System.Windows.Forms.Label();
            this.textBoxYAxisRangeMin = new System.Windows.Forms.TextBox();
            this.groupBoxXAxis = new System.Windows.Forms.GroupBox();
            this.buttonXAxisReset = new System.Windows.Forms.Button();
            this.textBoxXAxisRangeMax = new System.Windows.Forms.TextBox();
            this.labelXAxisRangeMin = new System.Windows.Forms.Label();
            this.labelXAxisRangeMax = new System.Windows.Forms.Label();
            this.textBoxXAxisRangeMin = new System.Windows.Forms.TextBox();
            this.groupBoxTimeSeriesOptions = new System.Windows.Forms.GroupBox();
            this.trackBarNumberOfPointsToPlot = new System.Windows.Forms.TrackBar();
            this.textBoxNumberOfTimePointsToPlot = new System.Windows.Forms.TextBox();
            this.labelNumberOfTimePointsToPlot = new System.Windows.Forms.Label();
            this.textBoxNumberOfTimePointsToBuffer = new System.Windows.Forms.TextBox();
            this.labelNumberOfTimePointsToBuffer = new System.Windows.Forms.Label();
            this.groupBoxTriggeringOptions = new System.Windows.Forms.GroupBox();
            this.textBoxNumberOfTriggerPointsToBuffer = new System.Windows.Forms.TextBox();
            this.labelNumberOfTriggerPointsToBuffer = new System.Windows.Forms.Label();
            this.checkBoxShowTriggerPoints = new System.Windows.Forms.CheckBox();
            this.checkBoxShowThresholds = new System.Windows.Forms.CheckBox();
            this.comboBoxPlotType = new System.Windows.Forms.ComboBox();
            this.groupBoxAccumulatedSpectraOptions = new System.Windows.Forms.GroupBox();
            this.labelAccumulateSpectraSlider = new System.Windows.Forms.Label();
            this.trackBarAccumulateSpectraSlider = new System.Windows.Forms.TrackBar();
            this.groupBoxYAxis.SuspendLayout();
            this.groupBoxXAxis.SuspendLayout();
            this.groupBoxTimeSeriesOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNumberOfPointsToPlot)).BeginInit();
            this.groupBoxTriggeringOptions.SuspendLayout();
            this.groupBoxAccumulatedSpectraOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAccumulateSpectraSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxYAxis
            // 
            this.groupBoxYAxis.Controls.Add(this.buttonYAxisReset);
            this.groupBoxYAxis.Controls.Add(this.checkBoxYAxisAutoScale);
            this.groupBoxYAxis.Controls.Add(this.textBoxYAxisRangeMax);
            this.groupBoxYAxis.Controls.Add(this.labelYAxisRangeMin);
            this.groupBoxYAxis.Controls.Add(this.labelYAxisRangeMax);
            this.groupBoxYAxis.Controls.Add(this.textBoxYAxisRangeMin);
            this.groupBoxYAxis.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.groupBoxYAxis.Location = new System.Drawing.Point(3, 39);
            this.groupBoxYAxis.Name = "groupBoxYAxis";
            this.groupBoxYAxis.Size = new System.Drawing.Size(309, 75);
            this.groupBoxYAxis.TabIndex = 227;
            this.groupBoxYAxis.TabStop = false;
            this.groupBoxYAxis.Text = "Y axis";
            // 
            // buttonYAxisReset
            // 
            this.buttonYAxisReset.Location = new System.Drawing.Point(250, 47);
            this.buttonYAxisReset.Name = "buttonYAxisReset";
            this.buttonYAxisReset.Size = new System.Drawing.Size(52, 23);
            this.buttonYAxisReset.TabIndex = 190;
            this.buttonYAxisReset.Text = "Reset";
            this.buttonYAxisReset.UseVisualStyleBackColor = true;
            this.buttonYAxisReset.Click += new System.EventHandler(this.buttonYAxisReset_Click);
            // 
            // checkBoxYAxisAutoScale
            // 
            this.checkBoxYAxisAutoScale.AutoSize = true;
            this.checkBoxYAxisAutoScale.Location = new System.Drawing.Point(14, 22);
            this.checkBoxYAxisAutoScale.Name = "checkBoxYAxisAutoScale";
            this.checkBoxYAxisAutoScale.Size = new System.Drawing.Size(79, 17);
            this.checkBoxYAxisAutoScale.TabIndex = 189;
            this.checkBoxYAxisAutoScale.Text = "Auto scale";
            this.checkBoxYAxisAutoScale.UseVisualStyleBackColor = true;
            this.checkBoxYAxisAutoScale.CheckedChanged += new System.EventHandler(this.checkBoxYAxisAutoScale_CheckedChanged);
            // 
            // textBoxYAxisRangeMax
            // 
            this.textBoxYAxisRangeMax.Location = new System.Drawing.Point(165, 47);
            this.textBoxYAxisRangeMax.Name = "textBoxYAxisRangeMax";
            this.textBoxYAxisRangeMax.Size = new System.Drawing.Size(78, 22);
            this.textBoxYAxisRangeMax.TabIndex = 188;
            this.textBoxYAxisRangeMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxYAxisRangeMax.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxYAxisRangeMax_Validating);
            this.textBoxYAxisRangeMax.Validated += new System.EventHandler(this.textBoxYAxisRangeMax_Validated);
            // 
            // labelYAxisRangeMin
            // 
            this.labelYAxisRangeMin.AutoSize = true;
            this.labelYAxisRangeMin.Location = new System.Drawing.Point(11, 51);
            this.labelYAxisRangeMin.Name = "labelYAxisRangeMin";
            this.labelYAxisRangeMin.Size = new System.Drawing.Size(27, 13);
            this.labelYAxisRangeMin.TabIndex = 183;
            this.labelYAxisRangeMin.Text = "Min";
            // 
            // labelYAxisRangeMax
            // 
            this.labelYAxisRangeMax.AutoSize = true;
            this.labelYAxisRangeMax.Location = new System.Drawing.Point(131, 51);
            this.labelYAxisRangeMax.Name = "labelYAxisRangeMax";
            this.labelYAxisRangeMax.Size = new System.Drawing.Size(28, 13);
            this.labelYAxisRangeMax.TabIndex = 185;
            this.labelYAxisRangeMax.Text = "Max";
            // 
            // textBoxYAxisRangeMin
            // 
            this.textBoxYAxisRangeMin.Location = new System.Drawing.Point(40, 47);
            this.textBoxYAxisRangeMin.Name = "textBoxYAxisRangeMin";
            this.textBoxYAxisRangeMin.Size = new System.Drawing.Size(77, 22);
            this.textBoxYAxisRangeMin.TabIndex = 187;
            this.textBoxYAxisRangeMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxYAxisRangeMin.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxYAxisRangeMin_Validating);
            this.textBoxYAxisRangeMin.Validated += new System.EventHandler(this.textBoxYAxisRangeMin_Validated);
            // 
            // groupBoxXAxis
            // 
            this.groupBoxXAxis.Controls.Add(this.buttonXAxisReset);
            this.groupBoxXAxis.Controls.Add(this.textBoxXAxisRangeMax);
            this.groupBoxXAxis.Controls.Add(this.labelXAxisRangeMin);
            this.groupBoxXAxis.Controls.Add(this.labelXAxisRangeMax);
            this.groupBoxXAxis.Controls.Add(this.textBoxXAxisRangeMin);
            this.groupBoxXAxis.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.groupBoxXAxis.Location = new System.Drawing.Point(3, 120);
            this.groupBoxXAxis.Name = "groupBoxXAxis";
            this.groupBoxXAxis.Size = new System.Drawing.Size(309, 51);
            this.groupBoxXAxis.TabIndex = 228;
            this.groupBoxXAxis.TabStop = false;
            this.groupBoxXAxis.Text = "X axis";
            // 
            // buttonXAxisReset
            // 
            this.buttonXAxisReset.Location = new System.Drawing.Point(250, 21);
            this.buttonXAxisReset.Name = "buttonXAxisReset";
            this.buttonXAxisReset.Size = new System.Drawing.Size(52, 23);
            this.buttonXAxisReset.TabIndex = 191;
            this.buttonXAxisReset.Text = "Reset";
            this.buttonXAxisReset.UseVisualStyleBackColor = true;
            this.buttonXAxisReset.Click += new System.EventHandler(this.buttonXAxisReset_Click);
            // 
            // textBoxXAxisRangeMax
            // 
            this.textBoxXAxisRangeMax.Location = new System.Drawing.Point(165, 22);
            this.textBoxXAxisRangeMax.Name = "textBoxXAxisRangeMax";
            this.textBoxXAxisRangeMax.Size = new System.Drawing.Size(78, 22);
            this.textBoxXAxisRangeMax.TabIndex = 188;
            this.textBoxXAxisRangeMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxXAxisRangeMax.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxXAxisRangeMax_Validating);
            this.textBoxXAxisRangeMax.Validated += new System.EventHandler(this.textBoxXAxisRangeMax_Validated);
            // 
            // labelXAxisRangeMin
            // 
            this.labelXAxisRangeMin.AutoSize = true;
            this.labelXAxisRangeMin.Location = new System.Drawing.Point(11, 25);
            this.labelXAxisRangeMin.Name = "labelXAxisRangeMin";
            this.labelXAxisRangeMin.Size = new System.Drawing.Size(27, 13);
            this.labelXAxisRangeMin.TabIndex = 183;
            this.labelXAxisRangeMin.Text = "Min";
            // 
            // labelXAxisRangeMax
            // 
            this.labelXAxisRangeMax.AutoSize = true;
            this.labelXAxisRangeMax.Location = new System.Drawing.Point(131, 25);
            this.labelXAxisRangeMax.Name = "labelXAxisRangeMax";
            this.labelXAxisRangeMax.Size = new System.Drawing.Size(28, 13);
            this.labelXAxisRangeMax.TabIndex = 185;
            this.labelXAxisRangeMax.Text = "Max";
            // 
            // textBoxXAxisRangeMin
            // 
            this.textBoxXAxisRangeMin.Location = new System.Drawing.Point(40, 22);
            this.textBoxXAxisRangeMin.Name = "textBoxXAxisRangeMin";
            this.textBoxXAxisRangeMin.Size = new System.Drawing.Size(77, 22);
            this.textBoxXAxisRangeMin.TabIndex = 187;
            this.textBoxXAxisRangeMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxXAxisRangeMin.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxXAxisRangeMin_Validating);
            this.textBoxXAxisRangeMin.Validated += new System.EventHandler(this.textBoxXAxisRangeMin_Validated);
            // 
            // groupBoxTimeSeriesOptions
            // 
            this.groupBoxTimeSeriesOptions.Controls.Add(this.trackBarNumberOfPointsToPlot);
            this.groupBoxTimeSeriesOptions.Controls.Add(this.textBoxNumberOfTimePointsToPlot);
            this.groupBoxTimeSeriesOptions.Controls.Add(this.labelNumberOfTimePointsToPlot);
            this.groupBoxTimeSeriesOptions.Controls.Add(this.textBoxNumberOfTimePointsToBuffer);
            this.groupBoxTimeSeriesOptions.Controls.Add(this.labelNumberOfTimePointsToBuffer);
            this.groupBoxTimeSeriesOptions.Location = new System.Drawing.Point(4, 274);
            this.groupBoxTimeSeriesOptions.Name = "groupBoxTimeSeriesOptions";
            this.groupBoxTimeSeriesOptions.Size = new System.Drawing.Size(308, 104);
            this.groupBoxTimeSeriesOptions.TabIndex = 229;
            this.groupBoxTimeSeriesOptions.TabStop = false;
            this.groupBoxTimeSeriesOptions.Text = "Time series options";
            // 
            // trackBarNumberOfPointsToPlot
            // 
            this.trackBarNumberOfPointsToPlot.AutoSize = false;
            this.trackBarNumberOfPointsToPlot.Location = new System.Drawing.Point(16, 71);
            this.trackBarNumberOfPointsToPlot.Name = "trackBarNumberOfPointsToPlot";
            this.trackBarNumberOfPointsToPlot.Size = new System.Drawing.Size(275, 25);
            this.trackBarNumberOfPointsToPlot.TabIndex = 231;
            this.trackBarNumberOfPointsToPlot.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarNumberOfPointsToPlot.Scroll += new System.EventHandler(this.trackBarNumberOfPointsToPlot_Scroll);
            // 
            // textBoxNumberOfTimePointsToPlot
            // 
            this.textBoxNumberOfTimePointsToPlot.Location = new System.Drawing.Point(195, 45);
            this.textBoxNumberOfTimePointsToPlot.Name = "textBoxNumberOfTimePointsToPlot";
            this.textBoxNumberOfTimePointsToPlot.Size = new System.Drawing.Size(107, 20);
            this.textBoxNumberOfTimePointsToPlot.TabIndex = 188;
            this.textBoxNumberOfTimePointsToPlot.TextChanged += new System.EventHandler(this.textBoxNumberOfTimePointsToPlot_TextChanged);
            this.textBoxNumberOfTimePointsToPlot.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNumberOfTimePointsToPlot_Validating);
            this.textBoxNumberOfTimePointsToPlot.Validated += new System.EventHandler(this.textBoxNumberOfTimePointsToPlot_Validated);
            // 
            // labelNumberOfTimePointsToPlot
            // 
            this.labelNumberOfTimePointsToPlot.AutoSize = true;
            this.labelNumberOfTimePointsToPlot.Location = new System.Drawing.Point(10, 48);
            this.labelNumberOfTimePointsToPlot.Name = "labelNumberOfTimePointsToPlot";
            this.labelNumberOfTimePointsToPlot.Size = new System.Drawing.Size(141, 13);
            this.labelNumberOfTimePointsToPlot.TabIndex = 187;
            this.labelNumberOfTimePointsToPlot.Text = "Number of time points to plot";
            // 
            // textBoxNumberOfTimePointsToBuffer
            // 
            this.textBoxNumberOfTimePointsToBuffer.Location = new System.Drawing.Point(195, 19);
            this.textBoxNumberOfTimePointsToBuffer.Name = "textBoxNumberOfTimePointsToBuffer";
            this.textBoxNumberOfTimePointsToBuffer.Size = new System.Drawing.Size(107, 20);
            this.textBoxNumberOfTimePointsToBuffer.TabIndex = 186;
            this.textBoxNumberOfTimePointsToBuffer.TextChanged += new System.EventHandler(this.textBoxNumberOfTimePointsToBuffer_TextChanged);
            this.textBoxNumberOfTimePointsToBuffer.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNumberOfTimePointsToBuffer_Validating);
            this.textBoxNumberOfTimePointsToBuffer.Validated += new System.EventHandler(this.textBoxNumberOfTimePointsToBuffer_Validated);
            // 
            // labelNumberOfTimePointsToBuffer
            // 
            this.labelNumberOfTimePointsToBuffer.AutoSize = true;
            this.labelNumberOfTimePointsToBuffer.Location = new System.Drawing.Point(10, 22);
            this.labelNumberOfTimePointsToBuffer.Name = "labelNumberOfTimePointsToBuffer";
            this.labelNumberOfTimePointsToBuffer.Size = new System.Drawing.Size(147, 13);
            this.labelNumberOfTimePointsToBuffer.TabIndex = 185;
            this.labelNumberOfTimePointsToBuffer.Text = "Number of time points to store";
            // 
            // groupBoxTriggeringOptions
            // 
            this.groupBoxTriggeringOptions.Controls.Add(this.textBoxNumberOfTriggerPointsToBuffer);
            this.groupBoxTriggeringOptions.Controls.Add(this.labelNumberOfTriggerPointsToBuffer);
            this.groupBoxTriggeringOptions.Controls.Add(this.checkBoxShowTriggerPoints);
            this.groupBoxTriggeringOptions.Controls.Add(this.checkBoxShowThresholds);
            this.groupBoxTriggeringOptions.Location = new System.Drawing.Point(3, 177);
            this.groupBoxTriggeringOptions.Name = "groupBoxTriggeringOptions";
            this.groupBoxTriggeringOptions.Size = new System.Drawing.Size(308, 91);
            this.groupBoxTriggeringOptions.TabIndex = 230;
            this.groupBoxTriggeringOptions.TabStop = false;
            this.groupBoxTriggeringOptions.Text = "Triggering options";
            // 
            // textBoxNumberOfTriggerPointsToBuffer
            // 
            this.textBoxNumberOfTriggerPointsToBuffer.Location = new System.Drawing.Point(195, 64);
            this.textBoxNumberOfTriggerPointsToBuffer.Name = "textBoxNumberOfTriggerPointsToBuffer";
            this.textBoxNumberOfTriggerPointsToBuffer.Size = new System.Drawing.Size(107, 20);
            this.textBoxNumberOfTriggerPointsToBuffer.TabIndex = 188;
            this.textBoxNumberOfTriggerPointsToBuffer.TextChanged += new System.EventHandler(this.textBoxNumberOfTriggerPointsToBuffer_TextChanged);
            this.textBoxNumberOfTriggerPointsToBuffer.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNumberOfTriggerPointsToBuffer_Validating);
            this.textBoxNumberOfTriggerPointsToBuffer.Validated += new System.EventHandler(this.textBoxNumberOfTriggerPointsToBuffer_Validated);
            // 
            // labelNumberOfTriggerPointsToBuffer
            // 
            this.labelNumberOfTriggerPointsToBuffer.AutoSize = true;
            this.labelNumberOfTriggerPointsToBuffer.Location = new System.Drawing.Point(10, 67);
            this.labelNumberOfTriggerPointsToBuffer.Name = "labelNumberOfTriggerPointsToBuffer";
            this.labelNumberOfTriggerPointsToBuffer.Size = new System.Drawing.Size(151, 13);
            this.labelNumberOfTriggerPointsToBuffer.TabIndex = 187;
            this.labelNumberOfTriggerPointsToBuffer.Text = "Number of trigger points to plot";
            // 
            // checkBoxShowTriggerPoints
            // 
            this.checkBoxShowTriggerPoints.AutoSize = true;
            this.checkBoxShowTriggerPoints.Location = new System.Drawing.Point(13, 43);
            this.checkBoxShowTriggerPoints.Name = "checkBoxShowTriggerPoints";
            this.checkBoxShowTriggerPoints.Size = new System.Drawing.Size(116, 17);
            this.checkBoxShowTriggerPoints.TabIndex = 1;
            this.checkBoxShowTriggerPoints.Text = "Show trigger points";
            this.checkBoxShowTriggerPoints.UseVisualStyleBackColor = true;
            this.checkBoxShowTriggerPoints.CheckedChanged += new System.EventHandler(this.checkBoxShowTriggerPoints_CheckedChanged);
            // 
            // checkBoxShowThresholds
            // 
            this.checkBoxShowThresholds.AutoSize = true;
            this.checkBoxShowThresholds.Location = new System.Drawing.Point(13, 20);
            this.checkBoxShowThresholds.Name = "checkBoxShowThresholds";
            this.checkBoxShowThresholds.Size = new System.Drawing.Size(104, 17);
            this.checkBoxShowThresholds.TabIndex = 0;
            this.checkBoxShowThresholds.Text = "Show thresholds";
            this.checkBoxShowThresholds.UseVisualStyleBackColor = true;
            this.checkBoxShowThresholds.CheckedChanged += new System.EventHandler(this.checkBoxShowThresholds_CheckedChanged);
            // 
            // comboBoxPlotType
            // 
            this.comboBoxPlotType.FormattingEnabled = true;
            this.comboBoxPlotType.Items.AddRange(new object[] {
            "[Live] Output spectrum",
            "[Live] Time series",
            "Dark spectrum",
            "Reference spectrum",
            "Dark-corrected reference spectrum",
            "Accumulated reference",
            "Accumulated time series"});
            this.comboBoxPlotType.Location = new System.Drawing.Point(4, 12);
            this.comboBoxPlotType.Name = "comboBoxPlotType";
            this.comboBoxPlotType.Size = new System.Drawing.Size(308, 21);
            this.comboBoxPlotType.TabIndex = 231;
            this.comboBoxPlotType.SelectedIndexChanged += new System.EventHandler(this.comboBoxPlotType_SelectedIndexChanged);
            // 
            // groupBoxAccumulatedSpectraOptions
            // 
            this.groupBoxAccumulatedSpectraOptions.Controls.Add(this.labelAccumulateSpectraSlider);
            this.groupBoxAccumulatedSpectraOptions.Controls.Add(this.trackBarAccumulateSpectraSlider);
            this.groupBoxAccumulatedSpectraOptions.Enabled = false;
            this.groupBoxAccumulatedSpectraOptions.Location = new System.Drawing.Point(4, 394);
            this.groupBoxAccumulatedSpectraOptions.Name = "groupBoxAccumulatedSpectraOptions";
            this.groupBoxAccumulatedSpectraOptions.Size = new System.Drawing.Size(308, 76);
            this.groupBoxAccumulatedSpectraOptions.TabIndex = 232;
            this.groupBoxAccumulatedSpectraOptions.TabStop = false;
            this.groupBoxAccumulatedSpectraOptions.Text = "Accumulated spectra options";
            // 
            // labelAccumulateSpectraSlider
            // 
            this.labelAccumulateSpectraSlider.Enabled = false;
            this.labelAccumulateSpectraSlider.Location = new System.Drawing.Point(16, 16);
            this.labelAccumulateSpectraSlider.Name = "labelAccumulateSpectraSlider";
            this.labelAccumulateSpectraSlider.Size = new System.Drawing.Size(275, 19);
            this.labelAccumulateSpectraSlider.TabIndex = 198;
            this.labelAccumulateSpectraSlider.Text = "0";
            this.labelAccumulateSpectraSlider.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBarAccumulateSpectraSlider
            // 
            this.trackBarAccumulateSpectraSlider.AutoSize = false;
            this.trackBarAccumulateSpectraSlider.Location = new System.Drawing.Point(16, 38);
            this.trackBarAccumulateSpectraSlider.Name = "trackBarAccumulateSpectraSlider";
            this.trackBarAccumulateSpectraSlider.Size = new System.Drawing.Size(275, 25);
            this.trackBarAccumulateSpectraSlider.TabIndex = 197;
            this.trackBarAccumulateSpectraSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarAccumulateSpectraSlider.ValueChanged += new System.EventHandler(this.trackBarAccumulateSpectraSlider_ValueChanged);
            // 
            // PlotOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxAccumulatedSpectraOptions);
            this.Controls.Add(this.comboBoxPlotType);
            this.Controls.Add(this.groupBoxTriggeringOptions);
            this.Controls.Add(this.groupBoxTimeSeriesOptions);
            this.Controls.Add(this.groupBoxXAxis);
            this.Controls.Add(this.groupBoxYAxis);
            this.Name = "PlotOptionsControl";
            this.Size = new System.Drawing.Size(315, 502);
            this.Load += new System.EventHandler(this.PlotOptionsControl_Load);
            this.groupBoxYAxis.ResumeLayout(false);
            this.groupBoxYAxis.PerformLayout();
            this.groupBoxXAxis.ResumeLayout(false);
            this.groupBoxXAxis.PerformLayout();
            this.groupBoxTimeSeriesOptions.ResumeLayout(false);
            this.groupBoxTimeSeriesOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarNumberOfPointsToPlot)).EndInit();
            this.groupBoxTriggeringOptions.ResumeLayout(false);
            this.groupBoxTriggeringOptions.PerformLayout();
            this.groupBoxAccumulatedSpectraOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAccumulateSpectraSlider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxYAxis;
        private System.Windows.Forms.TextBox textBoxYAxisRangeMax;
        private System.Windows.Forms.Label labelYAxisRangeMin;
        private System.Windows.Forms.Label labelYAxisRangeMax;
        private System.Windows.Forms.TextBox textBoxYAxisRangeMin;
        private System.Windows.Forms.CheckBox checkBoxYAxisAutoScale;
        private System.Windows.Forms.GroupBox groupBoxXAxis;
        private System.Windows.Forms.TextBox textBoxXAxisRangeMax;
        private System.Windows.Forms.Label labelXAxisRangeMin;
        private System.Windows.Forms.Label labelXAxisRangeMax;
        private System.Windows.Forms.TextBox textBoxXAxisRangeMin;
        private System.Windows.Forms.GroupBox groupBoxTimeSeriesOptions;
        private System.Windows.Forms.TextBox textBoxNumberOfTimePointsToBuffer;
        private System.Windows.Forms.Label labelNumberOfTimePointsToBuffer;
        private System.Windows.Forms.GroupBox groupBoxTriggeringOptions;
        private System.Windows.Forms.CheckBox checkBoxShowTriggerPoints;
        private System.Windows.Forms.CheckBox checkBoxShowThresholds;
        private System.Windows.Forms.TextBox textBoxNumberOfTriggerPointsToBuffer;
        private System.Windows.Forms.Label labelNumberOfTriggerPointsToBuffer;
        private System.Windows.Forms.TextBox textBoxNumberOfTimePointsToPlot;
        private System.Windows.Forms.Label labelNumberOfTimePointsToPlot;
        private System.Windows.Forms.TrackBar trackBarNumberOfPointsToPlot;
        private System.Windows.Forms.ComboBox comboBoxPlotType;
        private System.Windows.Forms.GroupBox groupBoxAccumulatedSpectraOptions;
        private System.Windows.Forms.TrackBar trackBarAccumulateSpectraSlider;
        private System.Windows.Forms.Label labelAccumulateSpectraSlider;
        private System.Windows.Forms.Button buttonYAxisReset;
        private System.Windows.Forms.Button buttonXAxisReset;
    }
}
