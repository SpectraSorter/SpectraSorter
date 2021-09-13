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

using spectra.processing;
using spectra.state;
using spectra.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spectra.ui
{
    public partial class WavelengthHub : Form
    {
        #region members

        /// <summary>
        /// Reference to the singleton ThresholdingSettings instance.
        /// </summary>
        private static WavelengthHub mInstance = null;

        // Define wether changes in the DataGridView can be committed to 
        // the data source (i.e., WavelengthManager.Instances.Wavelengths).
        private bool canCommit = true;

        #endregion members

        #region methods

        #region private

        /// <summary>
        /// Hide the form but do not close it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThresholdingSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Do not close; just hide
            e.Cancel = true;
            this.Hide();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        private WavelengthHub()
        {
            InitializeComponent();

            RegisterEventHandlers();

            // Set tooltips
            removeToolTip.SetToolTip(buttonRemoveWavelength, "Select (⯈) whole row for removal.");
            addToolTip.SetToolTip(buttonAddWavelength, "Add a new wavelength.");

            // Make non-resizable
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Make sure there is at least one wavelength
            if (WavelengthManager.Instance.Wavelengths.Count == 0)
            {
                WavelengthManager.Instance.AddEmptyWavelength();
            }

            // Fill the dataGridView
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = WavelengthManager.Instance.Wavelengths;
            dataGridView1.Refresh();
            this.canCommit = true;
        }

        #endregion private

        #region public

        #endregion public

        #endregion methods

        #region properties

        public new void Refresh()
        {
            // Rebind the data
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = WavelengthManager.Instance.Wavelengths;
            dataGridView1.Refresh();

            // Set the general settings
            checkBoxSaveWavelengthRange.Checked = SettingsManager.SaveWavelengthRange;

            // Update saving summary
            this.UpdateSavingSummaryText();

            // Set the thresholding/triggering values
            this.UpdateTriggeringUIElementsFromSettings();

            // Set the canCommit flag to true
            this.canCommit = true;
        }

        /// <summary>
        /// WavelengthHub (singleton) instance.
        /// </summary>
        public static WavelengthHub Instance
        {
            get
            {
                // If the Form has not been created yet, 
                // instantiate it now.
                if (mInstance == null)
                {
                    mInstance = new WavelengthHub();
                }

                // Return a reference
                return mInstance;
            }
        }

        #endregion properties

        #region handlers

        #endregion handlers

        // Toggle between saving individual wavelengths and a whole range
        private void ToggleSavingWavelengthRange(bool enabled)
        {
            checkBoxSaveWavelengthRange.Checked = enabled;
        }

        // Add a new wavelength
        private void ButtonAddWavelength_Click(object sender, EventArgs e)
        {
            if (WavelengthManager.Instance.AreThereEmptyWavelengths())
            {
                return;
            }

            // Create a new Wavelength
            WavelengthManager.Instance.AddEmptyWavelength();

            // Rebind the data
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = WavelengthManager.Instance.Wavelengths;
            dataGridView1.Refresh();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;

            // Update the colors
            UpdateColors();

            // Set the canCommit flag to true
            this.canCommit = true;
        }

        /// <summary>
        /// Remove a wavelength.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRemoveWavelength_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;

            // Make sure to delete the rows in reverse order
            List<int> indices = new List<int>();
            foreach (DataGridViewRow row in rows)
            {
                indices.Add(row.Index);
            }
            indices.Sort();
            indices.Reverse();

            foreach (int index in indices)
            {
                // Remove the wavelength
                WavelengthManager.Instance.RemoveWavelength(index);
            }

            // Rebind the data
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = WavelengthManager.Instance.Wavelengths;
            dataGridView1.Refresh();

            // Set the canCommit flag to true
            this.canCommit = true;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // @TODO Make the error obvious!
        }

        /// <summary>
        /// Change the color of the wavelength.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 6)
            {
                DataGridViewButtonCell cell = (DataGridViewButtonCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.FlatStyle = FlatStyle.Flat;

                // Get the selected object
                Wavelength wavelength = WavelengthManager.Instance.Wavelengths[e.RowIndex];
                DialogResult result = colorDialog.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                wavelength.SeriesColor = colorDialog.Color.ToArgb();

                cell.Style.BackColor = colorDialog.Color;
                cell.Style.ForeColor = colorDialog.Color;
            }

            // Refresh the view
            dataGridView1.Refresh();

        }

        // Add a new wavelength
        private void UpdateColors()
        {
            // Cache the query
            List<Wavelength> wavelengths = WavelengthManager.Instance.Wavelengths;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewButtonCell cell = (DataGridViewButtonCell)dataGridView1.Rows[i].Cells[6];
                cell.FlatStyle = FlatStyle.Flat;
                Color color = Color.FromArgb(wavelengths[i].SeriesColor);
                cell.Style.BackColor = color;
                cell.Style.ForeColor = color;
            }

            dataGridView1.Refresh();
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // We commit changes to the data source only after validation
            if (dataGridView1.IsCurrentCellDirty && this.canCommit == true && dataGridView1.CurrentCell.ColumnIndex != 0)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            UpdateColors();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonRemoveWavelength.Enabled = true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            buttonRemoveWavelength.Enabled = false;
        }

        private void buttonSaveWavelengthRange_Click(object sender, EventArgs e)
        {
            WavelengthRangeOptions.Instance.Show();
            WavelengthRangeOptions.Instance.BringToFront();
        }

        private void SetWavelengthValueColumnEnabled(bool enabled)
        {
            SetColumnByIndexEnabled(0, enabled);
        }

        private void SetColumnByIndexEnabled(int index, bool enabled)
        {
            Color back;
            Color fore;
            Color selBack;
            Color selFore;

            if (enabled)
            {
                back = Color.White;
                fore = Color.Black;
                selBack = Color.FromArgb(255, 192, 192, 255);
                selFore = Color.Black;
            }
            else
            {
                back = Color.LightGray;
                fore = Color.DarkGray;
                selBack = Color.LightGray;
                selFore = Color.DarkGray;
            }

            dataGridView1.Columns[index].ReadOnly = !enabled;
            dataGridView1.Columns[index].DefaultCellStyle.BackColor = back;
            dataGridView1.Columns[index].DefaultCellStyle.ForeColor = fore;
            dataGridView1.Columns[index].DefaultCellStyle.SelectionBackColor = selBack;
            dataGridView1.Columns[index].DefaultCellStyle.SelectionForeColor = selFore;
        }

        private void WavelengthHub_Activated(object sender, EventArgs e)
        {
            if (State.Instance.IsPerformingStandardAcquisition ||
                State.Instance.IsPerformingAccumulationAcquisition)
            {
                buttonAddWavelength.Enabled = false;
                buttonRemoveWavelength.Enabled = false;
                checkBoxSaveWavelengthRange.Enabled = false;
                buttonSaveWavelengthRange.Enabled = false;

                // Disable changing the values of wavelengths
                this.SetWavelengthValueColumnEnabled(false);
            }
            else
            {
                buttonAddWavelength.Enabled = true;
                buttonRemoveWavelength.Enabled = true;
                checkBoxSaveWavelengthRange.Enabled = true;
                buttonSaveWavelengthRange.Enabled = true;

                // Enable changing the values of wavelengths
                this.SetWavelengthValueColumnEnabled(true);
            }

            this.UpdateSavingSummaryText();

            dataGridView1.Refresh();
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Are we filling the data grid view from the data source?
            if (! dataGridView1.IsCurrentCellDirty)
            {
                this.canCommit = true;
                e.Cancel = false;
                return;
            }

            // Validate the wavelength
            if (e.ColumnIndex == 0)
            {
                if (! float.TryParse(e.FormattedValue.ToString(), out float value))
                {
                    // Invalid
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Invalid wavelength!";
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;

                    this.canCommit = false;
                    e.Cancel = true;
                    return;
                }

                if (value < 0.0f)
                {
                    // Invalid
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Invalid wavelength!";
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;

                    this.canCommit = false;
                    e.Cancel = true;
                    return;
                }

                if (value == 0 && WavelengthManager.Instance.Wavelengths.Count == 1)
                {
                    // Valid
                    dataGridView1.Rows[e.RowIndex].ErrorText = "";
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;

                    this.canCommit = true;
                    e.Cancel = false;
                    return;
                }

                // Is the value unique across rows?
                int count = 0;
                foreach (Wavelength wavelength in WavelengthManager.Instance.Wavelengths)
                {
                    if (wavelength.Value == value)
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    // Invalid
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Duplicate wavelength!";
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;

                    this.canCommit = false;
                    e.Cancel = true;
                    return;
                }

                // Should be valid
                dataGridView1.Rows[e.RowIndex].ErrorText = "";
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;

                this.canCommit = true;
                e.Cancel = false;
                return;
            }
            else
            {
                // Other columns are not validated
                this.canCommit = true;
                e.Cancel = false;
            }
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            // We only commit the changes if nothing is wrong in the data grid
            if (this.canCommit == true)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Nothing to do
        }

        private void checkBoxSaveWavelengthRange_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.SaveWavelengthRange = checkBoxSaveWavelengthRange.Checked;

            this.UpdateSavingSummaryText();
        }

        private void UpdateSavingSummaryText()
        {
            if (SettingsManager.SaveToFile == false)
            {
                labelWavelengthsCurrentlySaved.Text = "none (saving is disabled).";
                return;
            }

            if (SettingsManager.SaveWavelengthRange == true) 
            {
                labelWavelengthsCurrentlySaved.Text = "range from " +
                    SettingsManager.SaveStartWavelength.ToString("N1", CultureInfo.InvariantCulture) + "nm to " +
                    SettingsManager.SaveEndWavelength.ToString("N1", CultureInfo.InvariantCulture) + "nm.";
                return;
            }

            List<Wavelength> wavelengths = WavelengthManager.Instance.WavelengthsForSaving;
            if (wavelengths.Count > 0)
            {
                labelWavelengthsCurrentlySaved.Text = WavelengthManager.Instance.WavelengthListToString(wavelengths) + " (nm)";
            }
            else
            {
                labelWavelengthsCurrentlySaved.Text = "none selected.";
            }
            
        }

        private void UpdateTriggeringUIElementsFromSettings()
        {
            checkBoxTriggeringEnable.Checked = SettingsManager.SpectrumThresholdingEnabled;
            checkBoxTrigeringAllAboveEnabled.Checked = SettingsManager.SpectrumThresholdingAllSatisfiedEnabled;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                this.UpdateSavingSummaryText();
            }
        }

        private void checkBoxTriggeringEnable_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.SpectrumThresholdingEnabled = checkBoxTriggeringEnable.Checked;
        }

        private void checkBoxTrigeringAllAboveEnabled_CheckedChanged(object sender, EventArgs e)
        {
            SettingsManager.SpectrumThresholdingAllSatisfiedEnabled = checkBoxTrigeringAllAboveEnabled.Checked;
        }
    }
}
