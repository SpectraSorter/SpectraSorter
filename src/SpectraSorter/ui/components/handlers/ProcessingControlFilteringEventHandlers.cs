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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static spectra.state.SettingsManager;

namespace spectra.ui.components
{
    public partial class ProcessingControl
    {
        #region event_handlers

        private void RegisterEventHandlers()
        {
            SettingsManager.ToggleFiltering += ToggleFilteringHandler;
            MainWindow.AcquisitionStarted += ToggleFilterSupportDisableHandler;
            MainWindow.AcquisitionCompleted += ToggleFilterSupportEnableHandler;
        }

        // Event handlers
        void ToggleFilteringHandler(object sender, EventArgs e)
        {
            if (SettingsManager.SpectrumFilteringEnabled == true)
            {
                // Enable checkbox
                checkBoxFilteringEnable.Checked = true;

                // Enable elements
                this.ToggleUIElements(true);

                // Make sure the filter is appropriately initialized
                SpectrumFilterer.Instance.InitializeFilterFromCurrentSettings();
            }
            else
            {

                // Disable checkbox
                checkBoxFilteringEnable.Checked = false;

                // Disable elements
                this.ToggleUIElements(false);
            }
        }

        void ToggleFilterSupportEnableHandler(object sender, EventArgs e)
        {
            if (SettingsManager.SpectrumFilteringEnabled == true)
            {
                comboBoxFilteringKernelType.Enabled = true;
                textBoxSpectrumFilterWidth.Enabled = true;
            }
        }

        void ToggleFilterSupportDisableHandler(object sender, EventArgs e)
        {
            comboBoxFilteringKernelType.Enabled = false;
            textBoxSpectrumFilterWidth.Enabled = false;
        }

        #endregion event_handlers

        #region methods

        #region private

        /// <summary>
        /// Enable/disable ui elements.
        /// </summary>
        /// <param name="state">True or false.</param>
        private void ToggleUIElements(bool state)
        {
            if (State.Instance.IsPerformingStandardAcquisition == true ||
                State.Instance.IsPerformingAccumulationAcquisition == true)
            {
                this.comboBoxFilteringKernelType.Enabled = false;
                this.labelSpectrumFilterWidth.Enabled = false;
                this.textBoxSpectrumFilterWidth.Enabled = false;
                this.labelFilteringSummary.Enabled = false;
            }
            else
            {
                this.comboBoxFilteringKernelType.Enabled = state;
                this.labelSpectrumFilterWidth.Enabled = state;
                this.textBoxSpectrumFilterWidth.Enabled = state;
                this.labelFilteringSummary.Enabled = state;
            }
        }

        #endregion private

        #endregion methods
    }
}
