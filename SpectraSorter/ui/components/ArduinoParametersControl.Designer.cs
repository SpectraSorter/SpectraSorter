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
    partial class ArduinoParametersControl
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
            this.tabControlArduinoParameters = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonTriggerArduino = new System.Windows.Forms.Button();
            this.buttonTriggerArduinoOff = new System.Windows.Forms.Button();
            this.labelTestTrigger = new System.Windows.Forms.Label();
            this.labelArduinoTriggerStatus = new System.Windows.Forms.Label();
            this.textBoxDuration = new System.Windows.Forms.TextBox();
            this.labelTriggerDuration = new System.Windows.Forms.Label();
            this.buttonArduinoShowCount = new System.Windows.Forms.Button();
            this.buttonResetCount = new System.Windows.Forms.Button();
            this.buttonQueryTriggerDuration = new System.Windows.Forms.Button();
            this.labelTriggerCounter = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.buttonQueryPin = new System.Windows.Forms.Button();
            this.buttonSendTriggerDuration = new System.Windows.Forms.Button();
            this.buttonSendDigitalPin = new System.Windows.Forms.Button();
            this.textBoxDelay = new System.Windows.Forms.TextBox();
            this.labelPin = new System.Windows.Forms.Label();
            this.labelTriggerDelay = new System.Windows.Forms.Label();
            this.textBoxPin = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelArduinoTotalTestReport = new System.Windows.Forms.Label();
            this.labelArduinoSpeed = new System.Windows.Forms.Label();
            this.labelArduinoSpeedTestReport = new System.Windows.Forms.Label();
            this.textBoxArduinoSpeedTest = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonArduinoSpeedTest = new System.Windows.Forms.Button();
            this.tabControlArduinoParameters.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlArduinoParameters
            // 
            this.tabControlArduinoParameters.Controls.Add(this.tabPage1);
            this.tabControlArduinoParameters.Controls.Add(this.tabPage2);
            this.tabControlArduinoParameters.Enabled = false;
            this.tabControlArduinoParameters.Location = new System.Drawing.Point(4, 4);
            this.tabControlArduinoParameters.Name = "tabControlArduinoParameters";
            this.tabControlArduinoParameters.SelectedIndex = 0;
            this.tabControlArduinoParameters.Size = new System.Drawing.Size(308, 257);
            this.tabControlArduinoParameters.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.buttonTriggerArduino);
            this.tabPage1.Controls.Add(this.buttonTriggerArduinoOff);
            this.tabPage1.Controls.Add(this.labelTestTrigger);
            this.tabPage1.Controls.Add(this.labelArduinoTriggerStatus);
            this.tabPage1.Controls.Add(this.textBoxDuration);
            this.tabPage1.Controls.Add(this.labelTriggerDuration);
            this.tabPage1.Controls.Add(this.buttonArduinoShowCount);
            this.tabPage1.Controls.Add(this.buttonResetCount);
            this.tabPage1.Controls.Add(this.buttonQueryTriggerDuration);
            this.tabPage1.Controls.Add(this.labelTriggerCounter);
            this.tabPage1.Controls.Add(this.labelCount);
            this.tabPage1.Controls.Add(this.buttonQueryPin);
            this.tabPage1.Controls.Add(this.buttonSendTriggerDuration);
            this.tabPage1.Controls.Add(this.buttonSendDigitalPin);
            this.tabPage1.Controls.Add(this.textBoxDelay);
            this.tabPage1.Controls.Add(this.labelPin);
            this.tabPage1.Controls.Add(this.labelTriggerDelay);
            this.tabPage1.Controls.Add(this.textBoxPin);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(300, 231);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Parameters";
            // 
            // buttonTriggerArduino
            // 
            this.buttonTriggerArduino.Location = new System.Drawing.Point(3, 191);
            this.buttonTriggerArduino.Name = "buttonTriggerArduino";
            this.buttonTriggerArduino.Size = new System.Drawing.Size(149, 23);
            this.buttonTriggerArduino.TabIndex = 229;
            this.buttonTriggerArduino.Text = "On";
            this.buttonTriggerArduino.UseVisualStyleBackColor = true;
            this.buttonTriggerArduino.Click += new System.EventHandler(this.buttonTriggerArduino_Click);
            // 
            // buttonTriggerArduinoOff
            // 
            this.buttonTriggerArduinoOff.Location = new System.Drawing.Point(152, 191);
            this.buttonTriggerArduinoOff.Name = "buttonTriggerArduinoOff";
            this.buttonTriggerArduinoOff.Size = new System.Drawing.Size(142, 23);
            this.buttonTriggerArduinoOff.TabIndex = 230;
            this.buttonTriggerArduinoOff.Text = "Off";
            this.buttonTriggerArduinoOff.Click += new System.EventHandler(this.buttonTriggerArduinoOff_Click);
            // 
            // labelTestTrigger
            // 
            this.labelTestTrigger.AutoSize = true;
            this.labelTestTrigger.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTestTrigger.Location = new System.Drawing.Point(6, 168);
            this.labelTestTrigger.Name = "labelTestTrigger";
            this.labelTestTrigger.Size = new System.Drawing.Size(65, 13);
            this.labelTestTrigger.TabIndex = 228;
            this.labelTestTrigger.Text = "Test trigger";
            // 
            // labelArduinoTriggerStatus
            // 
            this.labelArduinoTriggerStatus.AutoSize = true;
            this.labelArduinoTriggerStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelArduinoTriggerStatus.Location = new System.Drawing.Point(6, 116);
            this.labelArduinoTriggerStatus.Name = "labelArduinoTriggerStatus";
            this.labelArduinoTriggerStatus.Size = new System.Drawing.Size(39, 13);
            this.labelArduinoTriggerStatus.TabIndex = 227;
            this.labelArduinoTriggerStatus.Text = "Status";
            // 
            // textBoxDuration
            // 
            this.textBoxDuration.Location = new System.Drawing.Point(115, 68);
            this.textBoxDuration.Name = "textBoxDuration";
            this.textBoxDuration.Size = new System.Drawing.Size(60, 20);
            this.textBoxDuration.TabIndex = 225;
            this.textBoxDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxDuration.TextChanged += new System.EventHandler(this.textBoxDuration_TextChanged_1);
            this.textBoxDuration.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDuration_Validating);
            this.textBoxDuration.Validated += new System.EventHandler(this.textBoxDuration_Validated);
            // 
            // labelTriggerDuration
            // 
            this.labelTriggerDuration.AutoSize = true;
            this.labelTriggerDuration.Location = new System.Drawing.Point(6, 72);
            this.labelTriggerDuration.Name = "labelTriggerDuration";
            this.labelTriggerDuration.Size = new System.Drawing.Size(101, 13);
            this.labelTriggerDuration.TabIndex = 226;
            this.labelTriggerDuration.Text = "Trigger duration (µs)";
            // 
            // buttonArduinoShowCount
            // 
            this.buttonArduinoShowCount.Location = new System.Drawing.Point(247, 136);
            this.buttonArduinoShowCount.Name = "buttonArduinoShowCount";
            this.buttonArduinoShowCount.Size = new System.Drawing.Size(47, 23);
            this.buttonArduinoShowCount.TabIndex = 221;
            this.buttonArduinoShowCount.Text = "Get";
            this.buttonArduinoShowCount.UseVisualStyleBackColor = true;
            this.buttonArduinoShowCount.Click += new System.EventHandler(this.ButtonArduinoShowCount_Click);
            // 
            // buttonResetCount
            // 
            this.buttonResetCount.Location = new System.Drawing.Point(194, 136);
            this.buttonResetCount.Name = "buttonResetCount";
            this.buttonResetCount.Size = new System.Drawing.Size(47, 23);
            this.buttonResetCount.TabIndex = 222;
            this.buttonResetCount.Text = "Reset";
            this.buttonResetCount.UseVisualStyleBackColor = true;
            this.buttonResetCount.Click += new System.EventHandler(this.ButtonResetCount_Click);
            // 
            // buttonQueryTriggerDuration
            // 
            this.buttonQueryTriggerDuration.Location = new System.Drawing.Point(247, 67);
            this.buttonQueryTriggerDuration.Name = "buttonQueryTriggerDuration";
            this.buttonQueryTriggerDuration.Size = new System.Drawing.Size(47, 23);
            this.buttonQueryTriggerDuration.TabIndex = 223;
            this.buttonQueryTriggerDuration.Text = "Get";
            this.buttonQueryTriggerDuration.UseVisualStyleBackColor = true;
            this.buttonQueryTriggerDuration.Click += new System.EventHandler(this.ButtonQueryTriggerDuration_Click);
            // 
            // labelTriggerCounter
            // 
            this.labelTriggerCounter.AutoSize = true;
            this.labelTriggerCounter.Location = new System.Drawing.Point(6, 141);
            this.labelTriggerCounter.Name = "labelTriggerCounter";
            this.labelTriggerCounter.Size = new System.Drawing.Size(79, 13);
            this.labelTriggerCounter.TabIndex = 219;
            this.labelTriggerCounter.Text = "Trigger counter";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(139, 141);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(13, 13);
            this.labelCount.TabIndex = 220;
            this.labelCount.Text = "0";
            // 
            // buttonQueryPin
            // 
            this.buttonQueryPin.Location = new System.Drawing.Point(247, 11);
            this.buttonQueryPin.Name = "buttonQueryPin";
            this.buttonQueryPin.Size = new System.Drawing.Size(47, 23);
            this.buttonQueryPin.TabIndex = 218;
            this.buttonQueryPin.Text = "Get";
            this.buttonQueryPin.UseVisualStyleBackColor = true;
            this.buttonQueryPin.Click += new System.EventHandler(this.ButtonQueryPin_Click);
            // 
            // buttonSendTriggerDuration
            // 
            this.buttonSendTriggerDuration.Location = new System.Drawing.Point(194, 67);
            this.buttonSendTriggerDuration.Name = "buttonSendTriggerDuration";
            this.buttonSendTriggerDuration.Size = new System.Drawing.Size(47, 23);
            this.buttonSendTriggerDuration.TabIndex = 217;
            this.buttonSendTriggerDuration.Text = "Set";
            this.buttonSendTriggerDuration.UseVisualStyleBackColor = true;
            this.buttonSendTriggerDuration.Click += new System.EventHandler(this.buttonSendTriggerDuration_Click_1);
            // 
            // buttonSendDigitalPin
            // 
            this.buttonSendDigitalPin.Location = new System.Drawing.Point(194, 11);
            this.buttonSendDigitalPin.Name = "buttonSendDigitalPin";
            this.buttonSendDigitalPin.Size = new System.Drawing.Size(47, 23);
            this.buttonSendDigitalPin.TabIndex = 216;
            this.buttonSendDigitalPin.Text = "Set";
            this.buttonSendDigitalPin.UseVisualStyleBackColor = true;
            this.buttonSendDigitalPin.Click += new System.EventHandler(this.buttonSendDigitalPin_Click_1);
            // 
            // textBoxDelay
            // 
            this.textBoxDelay.Location = new System.Drawing.Point(115, 40);
            this.textBoxDelay.Name = "textBoxDelay";
            this.textBoxDelay.Size = new System.Drawing.Size(60, 20);
            this.textBoxDelay.TabIndex = 214;
            this.textBoxDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxDelay.TextChanged += new System.EventHandler(this.textBoxDelay_TextChanged_1);
            this.textBoxDelay.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDelay_Validating);
            this.textBoxDelay.Validated += new System.EventHandler(this.textBoxDelay_Validated);
            // 
            // labelPin
            // 
            this.labelPin.AutoSize = true;
            this.labelPin.Location = new System.Drawing.Point(6, 16);
            this.labelPin.Name = "labelPin";
            this.labelPin.Size = new System.Drawing.Size(53, 13);
            this.labelPin.TabIndex = 213;
            this.labelPin.Text = "Digital pin";
            // 
            // labelTriggerDelay
            // 
            this.labelTriggerDelay.AutoSize = true;
            this.labelTriggerDelay.Location = new System.Drawing.Point(6, 44);
            this.labelTriggerDelay.Name = "labelTriggerDelay";
            this.labelTriggerDelay.Size = new System.Drawing.Size(88, 13);
            this.labelTriggerDelay.TabIndex = 215;
            this.labelTriggerDelay.Text = "Trigger delay (µs)";
            // 
            // textBoxPin
            // 
            this.textBoxPin.Location = new System.Drawing.Point(115, 12);
            this.textBoxPin.Name = "textBoxPin";
            this.textBoxPin.Size = new System.Drawing.Size(60, 20);
            this.textBoxPin.TabIndex = 211;
            this.textBoxPin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxPin.TextChanged += new System.EventHandler(this.textBoxPin_TextChanged_1);
            this.textBoxPin.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxPin_Validating);
            this.textBoxPin.Validated += new System.EventHandler(this.textBoxPin_Validated);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.labelArduinoTotalTestReport);
            this.tabPage2.Controls.Add(this.labelArduinoSpeed);
            this.tabPage2.Controls.Add(this.labelArduinoSpeedTestReport);
            this.tabPage2.Controls.Add(this.textBoxArduinoSpeedTest);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.buttonArduinoSpeedTest);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(300, 231);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Performance";
            // 
            // labelArduinoTotalTestReport
            // 
            this.labelArduinoTotalTestReport.AutoSize = true;
            this.labelArduinoTotalTestReport.Location = new System.Drawing.Point(9, 102);
            this.labelArduinoTotalTestReport.Name = "labelArduinoTotalTestReport";
            this.labelArduinoTotalTestReport.Size = new System.Drawing.Size(119, 13);
            this.labelArduinoTotalTestReport.TabIndex = 214;
            this.labelArduinoTotalTestReport.Text = "Total elapsed time: N/A";
            // 
            // labelArduinoSpeed
            // 
            this.labelArduinoSpeed.AutoSize = true;
            this.labelArduinoSpeed.Location = new System.Drawing.Point(6, 46);
            this.labelArduinoSpeed.Name = "labelArduinoSpeed";
            this.labelArduinoSpeed.Size = new System.Drawing.Size(149, 13);
            this.labelArduinoSpeed.TabIndex = 213;
            this.labelArduinoSpeed.Text = "Arduino average trigger speed";
            // 
            // labelArduinoSpeedTestReport
            // 
            this.labelArduinoSpeedTestReport.AutoSize = true;
            this.labelArduinoSpeedTestReport.Location = new System.Drawing.Point(9, 76);
            this.labelArduinoSpeedTestReport.Name = "labelArduinoSpeedTestReport";
            this.labelArduinoSpeedTestReport.Size = new System.Drawing.Size(127, 13);
            this.labelArduinoSpeedTestReport.TabIndex = 212;
            this.labelArduinoSpeedTestReport.Text = "Average trigger time: N/A";
            // 
            // textBoxArduinoSpeedTest
            // 
            this.textBoxArduinoSpeedTest.Location = new System.Drawing.Point(115, 8);
            this.textBoxArduinoSpeedTest.Name = "textBoxArduinoSpeedTest";
            this.textBoxArduinoSpeedTest.Size = new System.Drawing.Size(60, 20);
            this.textBoxArduinoSpeedTest.TabIndex = 211;
            this.textBoxArduinoSpeedTest.Text = "1000";
            this.textBoxArduinoSpeedTest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxArduinoSpeedTest.TextChanged += new System.EventHandler(this.textBoxArduinoSpeedTest_TextChanged);
            this.textBoxArduinoSpeedTest.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxArduinoSpeedTest_Validating);
            this.textBoxArduinoSpeedTest.Validated += new System.EventHandler(this.textBoxArduinoSpeedTest_Validated);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 210;
            this.label7.Text = "# of triggers";
            // 
            // buttonArduinoSpeedTest
            // 
            this.buttonArduinoSpeedTest.Location = new System.Drawing.Point(194, 7);
            this.buttonArduinoSpeedTest.Name = "buttonArduinoSpeedTest";
            this.buttonArduinoSpeedTest.Size = new System.Drawing.Size(100, 23);
            this.buttonArduinoSpeedTest.TabIndex = 209;
            this.buttonArduinoSpeedTest.Text = "Run speed test";
            this.buttonArduinoSpeedTest.UseVisualStyleBackColor = true;
            this.buttonArduinoSpeedTest.Click += new System.EventHandler(this.buttonArduinoSpeedTest_Click);
            // 
            // ArduinoParametersControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlArduinoParameters);
            this.Name = "ArduinoParametersControl";
            this.Size = new System.Drawing.Size(315, 264);
            this.tabControlArduinoParameters.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlArduinoParameters;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label labelArduinoTriggerStatus;
        private System.Windows.Forms.TextBox textBoxDuration;
        private System.Windows.Forms.Label labelTriggerDuration;
        private System.Windows.Forms.Button buttonArduinoShowCount;
        private System.Windows.Forms.Button buttonResetCount;
        private System.Windows.Forms.Button buttonQueryTriggerDuration;
        private System.Windows.Forms.Label labelTriggerCounter;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Button buttonQueryPin;
        private System.Windows.Forms.Button buttonSendTriggerDuration;
        private System.Windows.Forms.Button buttonSendDigitalPin;
        private System.Windows.Forms.TextBox textBoxDelay;
        private System.Windows.Forms.Label labelPin;
        private System.Windows.Forms.Label labelTriggerDelay;
        private System.Windows.Forms.TextBox textBoxPin;
        private System.Windows.Forms.Label labelArduinoTotalTestReport;
        private System.Windows.Forms.Label labelArduinoSpeed;
        private System.Windows.Forms.Label labelArduinoSpeedTestReport;
        private System.Windows.Forms.TextBox textBoxArduinoSpeedTest;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonArduinoSpeedTest;
        private System.Windows.Forms.Button buttonTriggerArduino;
        private System.Windows.Forms.Button buttonTriggerArduinoOff;
        private System.Windows.Forms.Label labelTestTrigger;
    }
}
