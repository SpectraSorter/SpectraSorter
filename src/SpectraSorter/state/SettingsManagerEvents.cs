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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spectra.state
{
    public static partial class SettingsManager
    {
        #region events

        // Changing some of the settings triggers events.
        public static event EventHandler ToggleFiltering;
        public static event EventHandler ToggleThresholding;
        public static event EventHandler ToggleThresholdingAllSatisfied;
        public static event EventHandler ToggleSaving;
        public static event EventHandler TogglePlotThresholds;
        public static event EventHandler TogglePlotTriggerPoints;
        public static event EventHandler ChangeReferenceType;
        public static event EventHandler ChangePlotType;
        public static event EventHandler ChangeResultSpectrumType;
        public static event EventHandler ChangeSpectrumThresholdingWavelengths;
        public static event EventHandler ChangeNumberOfSpectraForDynamicAccumulation;
        public static event EventHandler ToggleSavingWavelengthRange;

        public static void OnToggleFiltering(object sender, SingleBooleanEventArgs e)
        {
            EventHandler handler = ToggleFiltering;
            handler?.Invoke(null, e);
        }

        public static void OnToggleThresholding(object sender, SingleBooleanEventArgs e)
        {
            EventHandler handler = ToggleThresholding;
            handler?.Invoke(null, e);
        }

        public static void OnToggleThresholdingAllSatisfied(object sender, SingleBooleanEventArgs e)
        {
            EventHandler handler = ToggleThresholdingAllSatisfied;
            handler?.Invoke(null, e);
        }

        public static void OnToggleSaving(object sender, SingleBooleanEventArgs e)
        {
            EventHandler handler = ToggleSaving;
            handler?.Invoke(null, e);
        }

        public static void OnTogglePlotThresholds(object sender, SingleBooleanEventArgs e)
        {
            EventHandler handler = TogglePlotThresholds;
            handler?.Invoke(null, e);
        }

        public static void OnTogglePlotTriggerPoints(object sender, SingleBooleanEventArgs e)
        {
            EventHandler handler = TogglePlotTriggerPoints;
            handler?.Invoke(null, e);
        }

        public static void OnChangeReferenceType(object sender, ReferenceTypeEventArgs e)
        {
            EventHandler handler = ChangeReferenceType;
            handler?.Invoke(null, e);
        }

        public static void OnChangePlotType(object sender, PlotTypeEventArgs e)
        {
            EventHandler handler = ChangePlotType;
            handler?.Invoke(null, e);
        }

        public static void OnChangeResultSpectrumType(object sender, ResultSpectrumTypeEventArgs e)
        {
            EventHandler handler = ChangeResultSpectrumType;
            handler?.Invoke(null, e);
        }

        public static void OnChangeSpectrumThresholdingWavelengths(object sender, SpectrumThresholdingWavelengthsEventArgs e)
        {
            EventHandler handler = ChangeSpectrumThresholdingWavelengths;
            handler?.Invoke(null, e);
        }

        public static void OnChangeNumberOfSpectraForDynamicAccumulation(object sender, NumberOfSpectraForDynamicAccumulationEventArgs e)
        {
            EventHandler handler = ChangeNumberOfSpectraForDynamicAccumulation;
            handler?.Invoke(null, e);
        }

        public static void OnToggleSavingWavelengthRange(object sender, SingleBooleanEventArgs e)
        {
            EventHandler handler = ToggleSavingWavelengthRange;
            handler?.Invoke(null, e);
        }

        #region event_args

        public class SingleBooleanEventArgs : EventArgs
        {
            public bool Enabled { get; set; }
        }

        public class ReferenceTypeEventArgs : EventArgs
        {
            public Options.ReferenceType Type { get; set; }
        }

        public class PlotTypeEventArgs : EventArgs
        {
            public Options.PlotType Type { get; set; }
        }

        public class ResultSpectrumTypeEventArgs : EventArgs
        {
            public Options.ResultSpectrumType Type { get; set; }
        }

        public class SpectrumThresholdingWavelengthsEventArgs : EventArgs
        {
            public List<float> Values { get; set; }
        }

        public class NumberOfSpectraForDynamicAccumulationEventArgs : EventArgs
        {
            public int Value { get; set; }
        }

        #endregion event_args

        #endregion events

    }
}
