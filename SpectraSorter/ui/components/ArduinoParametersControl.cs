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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using spectra.state;
using spectra.devices;
using System.Threading;
using System.Diagnostics;
using spectra.utils;
using System.Globalization;

namespace spectra.ui.components
{
    public partial class ArduinoParametersControl : UserControl
    {
        private Arduino mArduino;

        public ArduinoParametersControl()
        {
            InitializeComponent();

            this.tabControlArduinoParameters.Enabled = false;
        }

        override public void Refresh()
        {
            if (this.mArduino == null)
            {
                return;
            }

            // Set the trigger delay
            if (mArduino.TriggerDelayInMicros != SettingsManager.ArduinoTriggerDelay)
            {
                mArduino.TriggerDelayInMicros = SettingsManager.ArduinoTriggerDelay;
            }
            textBoxDelay.Text = mArduino.TriggerDelayInMicros.ToString(CultureInfo.InvariantCulture);

            Task.Run(() =>
            {
                this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_PIN);
                Thread.Sleep(250);
                this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_DURATION);
                Thread.Sleep(250);
                this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_COUNTER);
                Thread.Sleep(250);

                Invoke((MethodInvoker)delegate
                {
                    onQueryComplete();  // Invoke on UI thread
                });
            });
        }

        private void onQueryComplete()
        {
            textBoxPin.Text = "" + mArduino.Pin;
            textBoxDuration.Text = "" + mArduino.TriggerDuration;
            labelCount.Text = "" + this.mArduino.OnBoardCounter;
        }

        public void SetArduino(Arduino arduino)
        {
            this.mArduino = arduino;

            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            // Update the UI fields
            this.Refresh();
        }

        public void UpdateTriggerCounter()
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            Task.Run(() =>
            {
                this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_COUNTER);
                Thread.Sleep(250);

                Invoke((MethodInvoker)delegate
                {
                    onQueryComplete();  // Invoke on UI thread
                });
            });
        }

        private void ButtonSendDigitalPin_Click(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            if (UInt32.TryParse(textBoxPin.Text, out UInt32 value))
            {
                Task.Run(() =>
                {
                    this.mArduino.SendCommandWithParameter(Arduino.COMMANDS.SET_PIN, value);
                    Thread.Sleep(250);

                    Invoke((MethodInvoker)delegate
                    {
                        onQueryComplete();  // Invoke on UI thread
                    });
                });
            }
            else
            {
                Task.Run(() =>
                {
                    this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_PIN);
                    Thread.Sleep(250);

                    Invoke((MethodInvoker)delegate
                    {
                        onQueryComplete();  // Invoke on UI thread
                    });
                });
            }
        }

        private void ButtonQueryPin_Click(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            Task.Run(() =>
            {
                this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_PIN);
                Thread.Sleep(250);

                Invoke((MethodInvoker)delegate
                {
                    onQueryComplete();  // Invoke on UI thread
                });
            });
        }

        private void TextBoxPin_TextChanged(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            if (!UInt32.TryParse(textBoxPin.Text, out UInt32 value))
            {
                Task.Run(() =>
                {
                    this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_PIN);
                    Thread.Sleep(250);

                    Invoke((MethodInvoker)delegate
                    {
                        onQueryComplete();  // Invoke on UI thread
                    });
                });
            }
        }

        private void TextBoxDelay_TextChanged(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            if (UInt32.TryParse(textBoxDelay.Text, out UInt32 value))
            {
                this.mArduino.TriggerDelayInMicros = value;

                SettingsManager.ArduinoTriggerDelay = value;
            }
        }

        private void TextBoxDuration_TextChanged(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;


            if (!UInt32.TryParse(textBoxDuration.Text, out UInt32 value))
            {
                Task.Run(() =>
                {
                    this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_DURATION);
                    Thread.Sleep(250);

                    Invoke((MethodInvoker)delegate
                    {
                        onQueryComplete();  // Invoke on UI thread
                    });
                });
            }
        }

        private void ButtonSendTriggerDuration_Click(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;


            if (UInt32.TryParse(textBoxDuration.Text, out UInt32 value))
            {
                Task.Run(() =>
                {
                    this.mArduino.SendCommandWithParameter(Arduino.COMMANDS.SET_DURATION, value);
                    Thread.Sleep(250);

                    Invoke((MethodInvoker)delegate
                    {
                        onQueryComplete();  // Invoke on UI thread
                    });
                });
            }
            else
            {
                Task.Run(() =>
                {
                    this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_DURATION);
                    Thread.Sleep(250);

                    Invoke((MethodInvoker)delegate
                    {
                        onQueryComplete();  // Invoke on UI thread
                    });
                });
            }
        }

        private void ButtonQueryTriggerDuration_Click(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            Task.Run(() =>
            {
                this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_DURATION);
                Thread.Sleep(250);

                Invoke((MethodInvoker)delegate
                {
                    onQueryComplete();  // Invoke on UI thread
                });
            });
        }

        private void ButtonResetCount_Click(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            Task.Run(() =>
            {
                this.mArduino.SendCommand(Arduino.COMMANDS.RESET_COUNTER);
                Thread.Sleep(250);
                this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_COUNTER);
                Thread.Sleep(250);

                Invoke((MethodInvoker)delegate
                {
                    onQueryComplete();  // Invoke on UI thread
                });
            });
        }

        private void ButtonArduinoShowCount_Click(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            Task.Run(() =>
            {
                this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_COUNTER);
                Thread.Sleep(250);

                Invoke((MethodInvoker)delegate
                {
                    onQueryComplete();  // Invoke on UI thread
                });
            });
        }

        private void buttonArduinoSpeedTest_Click(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;


            if (!Int32.TryParse(textBoxArduinoSpeedTest.Text, out Int32 numTriggers))
            {
                return;
            }

            // Resetting counter on UI
            labelArduinoSpeedTestReport.Text = "Running speed test...";
            labelCount.Text = "";

            tabControlArduinoParameters.Enabled = false;

            Task.Run(() =>
            {
                // Reset the counter
                this.mArduino.SendCommand(Arduino.COMMANDS.RESET_COUNTER);
                Thread.Sleep(500);

                // Allocate enough space to prevent or at least limit resizing the back-end array
                List<Int64> times = new List<Int64>(capacity: 2 * numTriggers * 1000);

                // Start timer
                Stopwatch sw = new Stopwatch();
                sw.Start();

                for (int i = 0; i < numTriggers; i++)
                {
                    Int64 before = sw.ElapsedTicks;
                    this.mArduino.SendCommand(Arduino.COMMANDS.TRIGGER);
                    Int64 elapsed = sw.ElapsedTicks - before;
                    times.Add(elapsed);
                }

                // Stop timer
                sw.Stop();

                // Calculate statistics
                double factor = Stopwatch.Frequency / (1000000L);
                double meanDuration = Utils.Mean(times) / factor;
                double stdDuration = Utils.Std(times) / factor;

                // Display the results
                string mean = "Average trigger time: " +
                              meanDuration.ToString("N0", CultureInfo.InvariantCulture) + " +/- " +
                              stdDuration.ToString("N0", CultureInfo.InvariantCulture) + " us";
                string total = "Total elapsed time: " +
                               (times.Sum() / factor).ToString("N0", CultureInfo.InvariantCulture) + " us";

                // Retrieve current counter
                this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_COUNTER);
                Thread.Sleep(500);

                Invoke((MethodInvoker)delegate
                {
                    onSpeedTestComplete(mean, total);  // Invoke on UI thread
                });
            });
        }

        private void onSpeedTestComplete(string mean, string total)
        {
            labelArduinoSpeedTestReport.Text = mean;
            labelArduinoTotalTestReport.Text = total;
            labelCount.Text = "" + this.mArduino.OnBoardCounter;

            tabControlArduinoParameters.Enabled = true;
        }

        private void buttonSendDigitalPin_Click_1(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            if (UInt32.TryParse(textBoxPin.Text, out UInt32 value))
            {
                Task.Run(() =>
                {
                    this.mArduino.SendCommandWithParameter(Arduino.COMMANDS.SET_PIN, value);
                    Thread.Sleep(250);

                    Invoke((MethodInvoker)delegate
                    {
                        onQueryComplete();  // Invoke on UI thread
                    });
                });
            }
            else
            {
                Task.Run(() =>
                {
                    this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_PIN);
                    Thread.Sleep(250);

                    Invoke((MethodInvoker)delegate
                    {
                        onQueryComplete();  // Invoke on UI thread
                    });
                });
            }
        }

        private void buttonQueryPin_Click_1(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            Task.Run(() =>
            {
                this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_PIN);
                Thread.Sleep(250);

                Invoke((MethodInvoker)delegate
                {
                    onQueryComplete();  // Invoke on UI thread
                });
            });
        }

        private void buttonSendTriggerDuration_Click_1(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            if (UInt32.TryParse(textBoxDuration.Text, out UInt32 value))
            {
                Task.Run(() =>
                {
                    this.mArduino.SendCommandWithParameter(Arduino.COMMANDS.SET_DURATION, value);
                    Thread.Sleep(250);

                    Invoke((MethodInvoker)delegate
                    {
                        onQueryComplete();  // Invoke on UI thread
                    });
                });
            }
            else
            {
                Task.Run(() =>
                {
                    this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_DURATION);
                    Thread.Sleep(250);

                    Invoke((MethodInvoker)delegate
                    {
                        onQueryComplete();  // Invoke on UI thread
                    });
                });
            }
        }

        private void buttonQueryTriggerDuration_Click_1(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            Task.Run(() =>
            {
                this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_DURATION);
                Thread.Sleep(250);

                Invoke((MethodInvoker)delegate
                {
                    onQueryComplete();  // Invoke on UI thread
                });
            });
        }

        private void buttonResetCount_Click_1(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            Task.Run(() =>
            {
                this.mArduino.SendCommand(Arduino.COMMANDS.RESET_COUNTER);
                Thread.Sleep(250);
                this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_COUNTER);
                Thread.Sleep(250);

                Invoke((MethodInvoker)delegate
                {
                    onQueryComplete();  // Invoke on UI thread
                });
            });
        }

        private void buttonArduinoShowCount_Click_1(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            Task.Run(() =>
            {
                this.mArduino.SendCommand(Arduino.COMMANDS.QUERY_COUNTER);
                Thread.Sleep(250);

                Invoke((MethodInvoker)delegate
                {
                    onQueryComplete();  // Invoke on UI thread
                });
            });           
        }

        private void buttonTriggerArduino_Click(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            mArduino.SendCommand(Arduino.COMMANDS.TRIGGER);
        }

        private void buttonTriggerArduinoOff_Click(object sender, EventArgs e)
        {
            if (this.mArduino == null || !this.mArduino.IsConnected())
            {
                this.tabControlArduinoParameters.Enabled = false;
                return;
            }

            this.tabControlArduinoParameters.Enabled = true;

            mArduino.SendCommand(Arduino.COMMANDS.STOP);
        }

        private void textBoxPin_Validating(object sender, CancelEventArgs e)
        {
            if (UInt32.TryParse(textBoxPin.Text, out UInt32 value))
            {
                textBoxPin.BackColor = Color.White;
                e.Cancel = false;
            }
            else
            {
                textBoxPin.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxPin_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxPin.Text, out UInt32 value))
            {
                textBoxPin.BackColor = Color.White;
            }
        }

        private void textBoxDelay_Validating(object sender, CancelEventArgs e)
        {
            if (UInt32.TryParse(textBoxDelay.Text, out UInt32 value))
            {
                textBoxDelay.BackColor = Color.White;
                e.Cancel = false;
            }
            else
            {
                textBoxDelay.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxDelay_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxDelay.Text, out UInt32 value))
            {
                textBoxDelay.BackColor = Color.White;
            }
        }

        private void textBoxPin_TextChanged_1(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void textBoxDelay_TextChanged_1(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void textBoxDuration_TextChanged_1(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void textBoxDuration_Validating(object sender, CancelEventArgs e)
        {
            if (UInt32.TryParse(textBoxDuration.Text, out UInt32 value))
            {
                textBoxDuration.BackColor = Color.White;
                e.Cancel = false;
            }
            else
            {
                textBoxDuration.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxDuration_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxDuration.Text, out UInt32 value))
            {
                textBoxDuration.BackColor = Color.White;
            }
        }

        private void textBoxArduinoSpeedTest_TextChanged(object sender, EventArgs e)
        {
            this.ValidateChildren();
        }

        private void textBoxArduinoSpeedTest_Validating(object sender, CancelEventArgs e)
        {
            if (UInt32.TryParse(textBoxArduinoSpeedTest.Text, out UInt32 value))
            {
                textBoxArduinoSpeedTest.BackColor = Color.White;
                e.Cancel = false;
            }
            else
            {
                textBoxArduinoSpeedTest.BackColor = Color.Red;
                e.Cancel = true;
            }
        }

        private void textBoxArduinoSpeedTest_Validated(object sender, EventArgs e)
        {
            if (UInt32.TryParse(textBoxArduinoSpeedTest.Text, out UInt32 value))
            {
                textBoxArduinoSpeedTest.BackColor = Color.White;
            }
        }
    }
}
