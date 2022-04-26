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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spectra.ui
{
    public partial class ShortcutsDialog : Form
    {
        Regex expression = new Regex(@"^D(?<digit>\d+)$");

        public ShortcutsDialog(
            Keys wavelengthEditorShortcutKeys,
            Keys enabledFilteringShortcutKeys,
            Keys enabledThresholdingShortcutKeys,
            Keys enabledSaveToFileShortcutKeys,
            Keys startAcquisitionShortcutKeys,
            Keys abortAcquisitionShortcutKeys,
            Keys plotSpectrumShortcutKeys,
            Keys plotTimeSeriesShortcutKeys,
            Keys plotDarkSpectrumShortcutKeys,
            Keys plotReferenceSpectrumShortcutKeys,
            Keys plotCorrectedReferenceSpectrumShortcutKeys,
            Keys plotAccumulatedSpectraShortcutKeys,
            Keys plotAccumulatedTimeSeriesShortcutKeys,
            Keys showThresholdsShortcutKeys,
            Keys showTriggerPointsShortcutKeys,
            Keys plotAutoScaleYAxisShortcutKeys
            )
        {
            InitializeComponent();

            // Update shortcutss
            labelWavelengthHubShortcut.Text = KeysToString(wavelengthEditorShortcutKeys);
            labelFilteringShortcut.Text = KeysToString(enabledFilteringShortcutKeys);
            labelTriggeringShortcut.Text = KeysToString(enabledThresholdingShortcutKeys);
            labelSavingShortcut.Text = KeysToString(enabledSaveToFileShortcutKeys);
            labelAcquisitionStartShortcut.Text = KeysToString(startAcquisitionShortcutKeys);
            labelAcquisitionAbortShortcut.Text = KeysToString(abortAcquisitionShortcutKeys);
            labelPlotOutputSpectrumShortcut.Text = KeysToString(plotSpectrumShortcutKeys);
            labelPlotTimeSeriesShortcut.Text = KeysToString(plotTimeSeriesShortcutKeys);
            labelPlotDarkSpectrumShortcut.Text = KeysToString(plotDarkSpectrumShortcutKeys);
            labelPlotRefSpectrumShortcut.Text = KeysToString(plotReferenceSpectrumShortcutKeys);
            labelPlotCorrRefSpectrumShortcut.Text = KeysToString(plotCorrectedReferenceSpectrumShortcutKeys);
            labelPlotAccumulatedSpectraShortcut.Text = KeysToString(plotAccumulatedSpectraShortcutKeys);
            labelPlotAccumulatedTimeSeriesShortcut.Text = KeysToString(plotAccumulatedTimeSeriesShortcutKeys);
            labelPlotThresholdsShortcut.Text = KeysToString(showThresholdsShortcutKeys);
            labelPlotTriggerPointsShortcut.Text = KeysToString(showTriggerPointsShortcutKeys);
            labelPlotAutoScaleYAxisShortcut.Text = KeysToString(plotAutoScaleYAxisShortcutKeys);
        }

        private void ShortcutsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Do not close; just hide
            e.Cancel = true;
            this.Hide();
        }

        private string KeysToString(Keys keys)
        {
            string[] parts = keys.ToString().Split(',');

            Match match = expression.Match(parts[0]);
            if (match.Success)
            {
                // Get digit
                string result = match.Groups["digit"].Value;
                parts[0] = result;
            }

            if (! string.IsNullOrEmpty(parts[1]))
            {
                return parts[1] + " + " + parts[0];
            }
            else
            {
                return parts[0];
            }
        }
    }
}
