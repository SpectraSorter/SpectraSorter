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

namespace spectra.ui
{
    partial class ShortcutsDialog
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
            this.labelTriggeringShortcut = new System.Windows.Forms.Label();
            this.labelTriggering = new System.Windows.Forms.Label();
            this.labelFilteringShortcut = new System.Windows.Forms.Label();
            this.labelFiltering = new System.Windows.Forms.Label();
            this.labelSavingShortcut = new System.Windows.Forms.Label();
            this.labelSavingToggle = new System.Windows.Forms.Label();
            this.labelAcquisitionStartShortcut = new System.Windows.Forms.Label();
            this.labelAcquisitionStart = new System.Windows.Forms.Label();
            this.labelAcquisitionAbortShortcut = new System.Windows.Forms.Label();
            this.labelAcquisitionAbort = new System.Windows.Forms.Label();
            this.labelPlotAutoScaleYAxisShortcut = new System.Windows.Forms.Label();
            this.labelPlotAutoScaleYAxis = new System.Windows.Forms.Label();
            this.labelPlotAccumulatedSpectraShortcut = new System.Windows.Forms.Label();
            this.labelPlotAccumulatedSpectra = new System.Windows.Forms.Label();
            this.labelPlotCorrRefSpectrumShortcut = new System.Windows.Forms.Label();
            this.labelPlotCorrRefSpectrum = new System.Windows.Forms.Label();
            this.labelPlotDarkSpectrumShortcut = new System.Windows.Forms.Label();
            this.labelPlotDarkSpectrum = new System.Windows.Forms.Label();
            this.labelPlotRefSpectrumShortcut = new System.Windows.Forms.Label();
            this.labelPlotRefSpectrum = new System.Windows.Forms.Label();
            this.labelPlotTimeSeriesShortcut = new System.Windows.Forms.Label();
            this.labelPlotTimeSeries = new System.Windows.Forms.Label();
            this.labelPlotOutputSpectrumShortcut = new System.Windows.Forms.Label();
            this.labelPlotOutputSpectrum = new System.Windows.Forms.Label();
            this.labelPlotAccumulatedTimeSeriesShortcut = new System.Windows.Forms.Label();
            this.labelPlotAccumulatedTimeSeries = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelPlotThresholdsShortcut = new System.Windows.Forms.Label();
            this.labelPlotThresholds = new System.Windows.Forms.Label();
            this.labelWavelengthHubShortcut = new System.Windows.Forms.Label();
            this.labelWavelengthHub = new System.Windows.Forms.Label();
            this.labelPlotTriggerPointsShortcut = new System.Windows.Forms.Label();
            this.labelPlotTriggerPoints = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTriggeringShortcut
            // 
            this.labelTriggeringShortcut.AutoSize = true;
            this.labelTriggeringShortcut.Location = new System.Drawing.Point(246, 169);
            this.labelTriggeringShortcut.Name = "labelTriggeringShortcut";
            this.labelTriggeringShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelTriggeringShortcut.TabIndex = 5;
            this.labelTriggeringShortcut.Text = "None";
            // 
            // labelTriggering
            // 
            this.labelTriggering.AutoSize = true;
            this.labelTriggering.Location = new System.Drawing.Point(13, 169);
            this.labelTriggering.Name = "labelTriggering";
            this.labelTriggering.Size = new System.Drawing.Size(99, 13);
            this.labelTriggering.TabIndex = 4;
            this.labelTriggering.Text = "Triggering: Toggle";
            // 
            // labelFilteringShortcut
            // 
            this.labelFilteringShortcut.AutoSize = true;
            this.labelFilteringShortcut.Location = new System.Drawing.Point(246, 143);
            this.labelFilteringShortcut.Name = "labelFilteringShortcut";
            this.labelFilteringShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelFilteringShortcut.TabIndex = 7;
            this.labelFilteringShortcut.Text = "None";
            // 
            // labelFiltering
            // 
            this.labelFiltering.AutoSize = true;
            this.labelFiltering.Location = new System.Drawing.Point(13, 143);
            this.labelFiltering.Name = "labelFiltering";
            this.labelFiltering.Size = new System.Drawing.Size(90, 13);
            this.labelFiltering.TabIndex = 6;
            this.labelFiltering.Text = "Filtering: Toggle";
            // 
            // labelSavingShortcut
            // 
            this.labelSavingShortcut.AutoSize = true;
            this.labelSavingShortcut.Location = new System.Drawing.Point(246, 195);
            this.labelSavingShortcut.Name = "labelSavingShortcut";
            this.labelSavingShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelSavingShortcut.TabIndex = 9;
            this.labelSavingShortcut.Text = "None";
            // 
            // labelSavingToggle
            // 
            this.labelSavingToggle.AutoSize = true;
            this.labelSavingToggle.Location = new System.Drawing.Point(13, 195);
            this.labelSavingToggle.Name = "labelSavingToggle";
            this.labelSavingToggle.Size = new System.Drawing.Size(81, 13);
            this.labelSavingToggle.TabIndex = 8;
            this.labelSavingToggle.Text = "Saving: Toggle";
            // 
            // labelAcquisitionStartShortcut
            // 
            this.labelAcquisitionStartShortcut.AutoSize = true;
            this.labelAcquisitionStartShortcut.Location = new System.Drawing.Point(246, 221);
            this.labelAcquisitionStartShortcut.Name = "labelAcquisitionStartShortcut";
            this.labelAcquisitionStartShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelAcquisitionStartShortcut.TabIndex = 11;
            this.labelAcquisitionStartShortcut.Text = "None";
            // 
            // labelAcquisitionStart
            // 
            this.labelAcquisitionStart.AutoSize = true;
            this.labelAcquisitionStart.Location = new System.Drawing.Point(13, 221);
            this.labelAcquisitionStart.Name = "labelAcquisitionStart";
            this.labelAcquisitionStart.Size = new System.Drawing.Size(95, 13);
            this.labelAcquisitionStart.TabIndex = 10;
            this.labelAcquisitionStart.Text = "Acquisition: Start";
            // 
            // labelAcquisitionAbortShortcut
            // 
            this.labelAcquisitionAbortShortcut.AutoSize = true;
            this.labelAcquisitionAbortShortcut.Location = new System.Drawing.Point(246, 247);
            this.labelAcquisitionAbortShortcut.Name = "labelAcquisitionAbortShortcut";
            this.labelAcquisitionAbortShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelAcquisitionAbortShortcut.TabIndex = 13;
            this.labelAcquisitionAbortShortcut.Text = "None";
            // 
            // labelAcquisitionAbort
            // 
            this.labelAcquisitionAbort.AutoSize = true;
            this.labelAcquisitionAbort.Location = new System.Drawing.Point(13, 247);
            this.labelAcquisitionAbort.Name = "labelAcquisitionAbort";
            this.labelAcquisitionAbort.Size = new System.Drawing.Size(100, 13);
            this.labelAcquisitionAbort.TabIndex = 12;
            this.labelAcquisitionAbort.Text = "Acquisition: Abort";
            // 
            // labelPlotAutoScaleYAxisShortcut
            // 
            this.labelPlotAutoScaleYAxisShortcut.AutoSize = true;
            this.labelPlotAutoScaleYAxisShortcut.Location = new System.Drawing.Point(594, 247);
            this.labelPlotAutoScaleYAxisShortcut.Name = "labelPlotAutoScaleYAxisShortcut";
            this.labelPlotAutoScaleYAxisShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelPlotAutoScaleYAxisShortcut.TabIndex = 27;
            this.labelPlotAutoScaleYAxisShortcut.Text = "None";
            // 
            // labelPlotAutoScaleYAxis
            // 
            this.labelPlotAutoScaleYAxis.AutoSize = true;
            this.labelPlotAutoScaleYAxis.Location = new System.Drawing.Point(361, 247);
            this.labelPlotAutoScaleYAxis.Name = "labelPlotAutoScaleYAxis";
            this.labelPlotAutoScaleYAxis.Size = new System.Drawing.Size(118, 13);
            this.labelPlotAutoScaleYAxis.TabIndex = 26;
            this.labelPlotAutoScaleYAxis.Text = "Plot: Auto-scale Y Axis";
            // 
            // labelPlotAccumulatedSpectraShortcut
            // 
            this.labelPlotAccumulatedSpectraShortcut.AutoSize = true;
            this.labelPlotAccumulatedSpectraShortcut.Location = new System.Drawing.Point(594, 143);
            this.labelPlotAccumulatedSpectraShortcut.Name = "labelPlotAccumulatedSpectraShortcut";
            this.labelPlotAccumulatedSpectraShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelPlotAccumulatedSpectraShortcut.TabIndex = 25;
            this.labelPlotAccumulatedSpectraShortcut.Text = "None";
            // 
            // labelPlotAccumulatedSpectra
            // 
            this.labelPlotAccumulatedSpectra.AutoSize = true;
            this.labelPlotAccumulatedSpectra.Location = new System.Drawing.Point(361, 143);
            this.labelPlotAccumulatedSpectra.Name = "labelPlotAccumulatedSpectra";
            this.labelPlotAccumulatedSpectra.Size = new System.Drawing.Size(139, 13);
            this.labelPlotAccumulatedSpectra.TabIndex = 24;
            this.labelPlotAccumulatedSpectra.Text = "Plot: Accumulated spectra";
            // 
            // labelPlotCorrRefSpectrumShortcut
            // 
            this.labelPlotCorrRefSpectrumShortcut.AutoSize = true;
            this.labelPlotCorrRefSpectrumShortcut.Location = new System.Drawing.Point(594, 117);
            this.labelPlotCorrRefSpectrumShortcut.Name = "labelPlotCorrRefSpectrumShortcut";
            this.labelPlotCorrRefSpectrumShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelPlotCorrRefSpectrumShortcut.TabIndex = 23;
            this.labelPlotCorrRefSpectrumShortcut.Text = "None";
            // 
            // labelPlotCorrRefSpectrum
            // 
            this.labelPlotCorrRefSpectrum.AutoSize = true;
            this.labelPlotCorrRefSpectrum.Location = new System.Drawing.Point(361, 117);
            this.labelPlotCorrRefSpectrum.Name = "labelPlotCorrRefSpectrum";
            this.labelPlotCorrRefSpectrum.Size = new System.Drawing.Size(184, 13);
            this.labelPlotCorrRefSpectrum.TabIndex = 22;
            this.labelPlotCorrRefSpectrum.Text = "Plot: Corrected reference spectrum";
            // 
            // labelPlotDarkSpectrumShortcut
            // 
            this.labelPlotDarkSpectrumShortcut.AutoSize = true;
            this.labelPlotDarkSpectrumShortcut.Location = new System.Drawing.Point(594, 65);
            this.labelPlotDarkSpectrumShortcut.Name = "labelPlotDarkSpectrumShortcut";
            this.labelPlotDarkSpectrumShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelPlotDarkSpectrumShortcut.TabIndex = 21;
            this.labelPlotDarkSpectrumShortcut.Text = "None";
            // 
            // labelPlotDarkSpectrum
            // 
            this.labelPlotDarkSpectrum.AutoSize = true;
            this.labelPlotDarkSpectrum.Location = new System.Drawing.Point(361, 65);
            this.labelPlotDarkSpectrum.Name = "labelPlotDarkSpectrum";
            this.labelPlotDarkSpectrum.Size = new System.Drawing.Size(107, 13);
            this.labelPlotDarkSpectrum.TabIndex = 20;
            this.labelPlotDarkSpectrum.Text = "Plot: Dark spectrum";
            // 
            // labelPlotRefSpectrumShortcut
            // 
            this.labelPlotRefSpectrumShortcut.AutoSize = true;
            this.labelPlotRefSpectrumShortcut.Location = new System.Drawing.Point(594, 91);
            this.labelPlotRefSpectrumShortcut.Name = "labelPlotRefSpectrumShortcut";
            this.labelPlotRefSpectrumShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelPlotRefSpectrumShortcut.TabIndex = 19;
            this.labelPlotRefSpectrumShortcut.Text = "None";
            // 
            // labelPlotRefSpectrum
            // 
            this.labelPlotRefSpectrum.AutoSize = true;
            this.labelPlotRefSpectrum.Location = new System.Drawing.Point(361, 91);
            this.labelPlotRefSpectrum.Name = "labelPlotRefSpectrum";
            this.labelPlotRefSpectrum.Size = new System.Drawing.Size(134, 13);
            this.labelPlotRefSpectrum.TabIndex = 18;
            this.labelPlotRefSpectrum.Text = "Plot: Reference spectrum";
            // 
            // labelPlotTimeSeriesShortcut
            // 
            this.labelPlotTimeSeriesShortcut.AutoSize = true;
            this.labelPlotTimeSeriesShortcut.Location = new System.Drawing.Point(594, 39);
            this.labelPlotTimeSeriesShortcut.Name = "labelPlotTimeSeriesShortcut";
            this.labelPlotTimeSeriesShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelPlotTimeSeriesShortcut.TabIndex = 17;
            this.labelPlotTimeSeriesShortcut.Text = "None";
            // 
            // labelPlotTimeSeries
            // 
            this.labelPlotTimeSeries.AutoSize = true;
            this.labelPlotTimeSeries.Location = new System.Drawing.Point(361, 39);
            this.labelPlotTimeSeries.Name = "labelPlotTimeSeries";
            this.labelPlotTimeSeries.Size = new System.Drawing.Size(88, 13);
            this.labelPlotTimeSeries.TabIndex = 16;
            this.labelPlotTimeSeries.Text = "Plot: Time series";
            // 
            // labelPlotOutputSpectrumShortcut
            // 
            this.labelPlotOutputSpectrumShortcut.AutoSize = true;
            this.labelPlotOutputSpectrumShortcut.Location = new System.Drawing.Point(594, 13);
            this.labelPlotOutputSpectrumShortcut.Name = "labelPlotOutputSpectrumShortcut";
            this.labelPlotOutputSpectrumShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelPlotOutputSpectrumShortcut.TabIndex = 15;
            this.labelPlotOutputSpectrumShortcut.Text = "None";
            // 
            // labelPlotOutputSpectrum
            // 
            this.labelPlotOutputSpectrum.AutoSize = true;
            this.labelPlotOutputSpectrum.Location = new System.Drawing.Point(361, 13);
            this.labelPlotOutputSpectrum.Name = "labelPlotOutputSpectrum";
            this.labelPlotOutputSpectrum.Size = new System.Drawing.Size(121, 13);
            this.labelPlotOutputSpectrum.TabIndex = 14;
            this.labelPlotOutputSpectrum.Text = "Plot: Output spectrum";
            // 
            // labelPlotAccumulatedTimeSeriesShortcut
            // 
            this.labelPlotAccumulatedTimeSeriesShortcut.AutoSize = true;
            this.labelPlotAccumulatedTimeSeriesShortcut.Location = new System.Drawing.Point(594, 169);
            this.labelPlotAccumulatedTimeSeriesShortcut.Name = "labelPlotAccumulatedTimeSeriesShortcut";
            this.labelPlotAccumulatedTimeSeriesShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelPlotAccumulatedTimeSeriesShortcut.TabIndex = 29;
            this.labelPlotAccumulatedTimeSeriesShortcut.Text = "None";
            // 
            // labelPlotAccumulatedTimeSeries
            // 
            this.labelPlotAccumulatedTimeSeries.AutoSize = true;
            this.labelPlotAccumulatedTimeSeries.Location = new System.Drawing.Point(361, 169);
            this.labelPlotAccumulatedTimeSeries.Name = "labelPlotAccumulatedTimeSeries";
            this.labelPlotAccumulatedTimeSeries.Size = new System.Drawing.Size(156, 13);
            this.labelPlotAccumulatedTimeSeries.TabIndex = 28;
            this.labelPlotAccumulatedTimeSeries.Text = "Plot: Accumulated time series";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(13, 40);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(88, 13);
            this.labelTitle.TabIndex = 30;
            this.labelTitle.Text = "Menu shortcuts";
            // 
            // labelPlotThresholdsShortcut
            // 
            this.labelPlotThresholdsShortcut.AutoSize = true;
            this.labelPlotThresholdsShortcut.Location = new System.Drawing.Point(594, 195);
            this.labelPlotThresholdsShortcut.Name = "labelPlotThresholdsShortcut";
            this.labelPlotThresholdsShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelPlotThresholdsShortcut.TabIndex = 32;
            this.labelPlotThresholdsShortcut.Text = "None";
            // 
            // labelPlotThresholds
            // 
            this.labelPlotThresholds.AutoSize = true;
            this.labelPlotThresholds.Location = new System.Drawing.Point(361, 195);
            this.labelPlotThresholds.Name = "labelPlotThresholds";
            this.labelPlotThresholds.Size = new System.Drawing.Size(120, 13);
            this.labelPlotThresholds.TabIndex = 31;
            this.labelPlotThresholds.Text = "Plot: Show thresholds";
            // 
            // labelWavelengthHubShortcut
            // 
            this.labelWavelengthHubShortcut.AutoSize = true;
            this.labelWavelengthHubShortcut.Location = new System.Drawing.Point(246, 117);
            this.labelWavelengthHubShortcut.Name = "labelWavelengthHubShortcut";
            this.labelWavelengthHubShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelWavelengthHubShortcut.TabIndex = 34;
            this.labelWavelengthHubShortcut.Text = "None";
            // 
            // labelWavelengthHub
            // 
            this.labelWavelengthHub.AutoSize = true;
            this.labelWavelengthHub.Location = new System.Drawing.Point(13, 117);
            this.labelWavelengthHub.Name = "labelWavelengthHub";
            this.labelWavelengthHub.Size = new System.Drawing.Size(94, 13);
            this.labelWavelengthHub.TabIndex = 33;
            this.labelWavelengthHub.Text = "Wavelength Hub";
            // 
            // labelPlotTriggerPointsShortcut
            // 
            this.labelPlotTriggerPointsShortcut.AutoSize = true;
            this.labelPlotTriggerPointsShortcut.Location = new System.Drawing.Point(594, 221);
            this.labelPlotTriggerPointsShortcut.Name = "labelPlotTriggerPointsShortcut";
            this.labelPlotTriggerPointsShortcut.Size = new System.Drawing.Size(35, 13);
            this.labelPlotTriggerPointsShortcut.TabIndex = 40;
            this.labelPlotTriggerPointsShortcut.Text = "None";
            // 
            // labelPlotTriggerPoints
            // 
            this.labelPlotTriggerPoints.AutoSize = true;
            this.labelPlotTriggerPoints.Location = new System.Drawing.Point(361, 221);
            this.labelPlotTriggerPoints.Name = "labelPlotTriggerPoints";
            this.labelPlotTriggerPoints.Size = new System.Drawing.Size(136, 13);
            this.labelPlotTriggerPoints.TabIndex = 39;
            this.labelPlotTriggerPoints.Text = "Plot: Show trigger points";
            // 
            // ShortcutsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 281);
            this.Controls.Add(this.labelPlotTriggerPointsShortcut);
            this.Controls.Add(this.labelPlotTriggerPoints);
            this.Controls.Add(this.labelWavelengthHubShortcut);
            this.Controls.Add(this.labelWavelengthHub);
            this.Controls.Add(this.labelPlotThresholdsShortcut);
            this.Controls.Add(this.labelPlotThresholds);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelPlotAccumulatedTimeSeriesShortcut);
            this.Controls.Add(this.labelPlotAccumulatedTimeSeries);
            this.Controls.Add(this.labelPlotAutoScaleYAxisShortcut);
            this.Controls.Add(this.labelPlotAutoScaleYAxis);
            this.Controls.Add(this.labelPlotAccumulatedSpectraShortcut);
            this.Controls.Add(this.labelPlotAccumulatedSpectra);
            this.Controls.Add(this.labelPlotCorrRefSpectrumShortcut);
            this.Controls.Add(this.labelPlotCorrRefSpectrum);
            this.Controls.Add(this.labelPlotDarkSpectrumShortcut);
            this.Controls.Add(this.labelPlotDarkSpectrum);
            this.Controls.Add(this.labelPlotRefSpectrumShortcut);
            this.Controls.Add(this.labelPlotRefSpectrum);
            this.Controls.Add(this.labelPlotTimeSeriesShortcut);
            this.Controls.Add(this.labelPlotTimeSeries);
            this.Controls.Add(this.labelPlotOutputSpectrumShortcut);
            this.Controls.Add(this.labelPlotOutputSpectrum);
            this.Controls.Add(this.labelAcquisitionAbortShortcut);
            this.Controls.Add(this.labelAcquisitionAbort);
            this.Controls.Add(this.labelAcquisitionStartShortcut);
            this.Controls.Add(this.labelAcquisitionStart);
            this.Controls.Add(this.labelSavingShortcut);
            this.Controls.Add(this.labelSavingToggle);
            this.Controls.Add(this.labelFilteringShortcut);
            this.Controls.Add(this.labelFiltering);
            this.Controls.Add(this.labelTriggeringShortcut);
            this.Controls.Add(this.labelTriggering);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ShortcutsDialog";
            this.Text = "Shortcuts";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShortcutsDialog_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelTriggeringShortcut;
        private System.Windows.Forms.Label labelTriggering;
        private System.Windows.Forms.Label labelFilteringShortcut;
        private System.Windows.Forms.Label labelFiltering;
        private System.Windows.Forms.Label labelSavingShortcut;
        private System.Windows.Forms.Label labelSavingToggle;
        private System.Windows.Forms.Label labelAcquisitionStartShortcut;
        private System.Windows.Forms.Label labelAcquisitionStart;
        private System.Windows.Forms.Label labelAcquisitionAbortShortcut;
        private System.Windows.Forms.Label labelAcquisitionAbort;
        private System.Windows.Forms.Label labelPlotAutoScaleYAxisShortcut;
        private System.Windows.Forms.Label labelPlotAutoScaleYAxis;
        private System.Windows.Forms.Label labelPlotAccumulatedSpectraShortcut;
        private System.Windows.Forms.Label labelPlotAccumulatedSpectra;
        private System.Windows.Forms.Label labelPlotCorrRefSpectrumShortcut;
        private System.Windows.Forms.Label labelPlotCorrRefSpectrum;
        private System.Windows.Forms.Label labelPlotDarkSpectrumShortcut;
        private System.Windows.Forms.Label labelPlotDarkSpectrum;
        private System.Windows.Forms.Label labelPlotRefSpectrumShortcut;
        private System.Windows.Forms.Label labelPlotRefSpectrum;
        private System.Windows.Forms.Label labelPlotTimeSeriesShortcut;
        private System.Windows.Forms.Label labelPlotTimeSeries;
        private System.Windows.Forms.Label labelPlotOutputSpectrumShortcut;
        private System.Windows.Forms.Label labelPlotOutputSpectrum;
        private System.Windows.Forms.Label labelPlotAccumulatedTimeSeriesShortcut;
        private System.Windows.Forms.Label labelPlotAccumulatedTimeSeries;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelPlotThresholdsShortcut;
        private System.Windows.Forms.Label labelPlotThresholds;
        private System.Windows.Forms.Label labelWavelengthHubShortcut;
        private System.Windows.Forms.Label labelWavelengthHub;
        private System.Windows.Forms.Label labelPlotTriggerPointsShortcut;
        private System.Windows.Forms.Label labelPlotTriggerPoints;
    }
}