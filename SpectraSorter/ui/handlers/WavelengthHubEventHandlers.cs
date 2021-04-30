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

using spectra.state;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static spectra.state.SettingsManager;

namespace spectra.ui
{
    public partial class WavelengthHub
    {
        #region event_handlers

        private void RegisterEventHandlers()
        {
            SettingsManager.ToggleSaving += ToggleSavingHandler;
            SettingsManager.ToggleSavingWavelengthRange += ToggleSavingWavelengthRangeHandler;
            MainWindow.AcquisitionStarted += ToggleElementsOnAcquisitionStarted;
            MainWindow.AcquisitionCompleted += ToggleElementsOnAcquisitionCompleted;
            SettingsManager.ToggleThresholding += ToggleThresholdingHandler;
            SettingsManager.ToggleThresholdingAllSatisfied += ToggleThresholdingAllSatisfiedHandler;
        }

        private void ToggleSavingHandler(object sender, EventArgs e)
        {
            this.UpdateSavingSummaryText();
        }

        private void ToggleSavingWavelengthRangeHandler(object sender, EventArgs e)
        {
            this.ToggleSavingWavelengthRange(SettingsManager.SaveWavelengthRange);
        }

        void ToggleElementsOnAcquisitionStarted(object sender, EventArgs e)
        {
            buttonAddWavelength.Enabled = false;
            buttonRemoveWavelength.Enabled = false;
            checkBoxSaveWavelengthRange.Enabled = false;
            buttonSaveWavelengthRange.Enabled = false;
            
            // Disable changing the values of wavelengths
            this.SetWavelengthValueColumnEnabled(false);

            this.UpdateSavingSummaryText();

            dataGridView1.Refresh();
        }

        void ToggleElementsOnAcquisitionCompleted(object sender, EventArgs e)
        {
            buttonAddWavelength.Enabled = true;
            buttonRemoveWavelength.Enabled = true;
            checkBoxSaveWavelengthRange.Enabled = true;
            buttonSaveWavelengthRange.Enabled = true;

            // Enable changing the values of wavelengths
            this.SetWavelengthValueColumnEnabled(true);

            this.UpdateSavingSummaryText();

            dataGridView1.Refresh();
        }

        private void ToggleThresholdingHandler(object sender, EventArgs e)
        {
            checkBoxTriggeringEnable.Checked = SettingsManager.SpectrumThresholdingEnabled;
        }

        private void ToggleThresholdingAllSatisfiedHandler(object sender, EventArgs e)
        {
            checkBoxTrigeringAllAboveEnabled.Checked = SettingsManager.SpectrumThresholdingAllSatisfiedEnabled;
        }

        #endregion event_handlers

    }
}
