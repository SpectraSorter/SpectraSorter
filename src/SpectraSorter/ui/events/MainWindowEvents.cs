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
using static spectra.state.SettingsManager;

namespace spectra.ui
{
    public partial class MainWindow
    {
        #region events

        public static event EventHandler ToggleYAxisAutoScale;
        public static event EventHandler AcquisitionStarted;
        public static event EventHandler AcquisitionCompleted;

        public static void OnToggleYAxisAutoScale(object sender, SingleBooleanEventArgs e)
        {
            EventHandler handler = ToggleYAxisAutoScale;
            handler?.Invoke(null, e);
        }

        public static void OnAcquisitionStarted(object sender, EventArgs e)
        {
            EventHandler handler = AcquisitionStarted;
            handler?.Invoke(null, e);
        }

        public static void OnAcquisitionCompleted(object sender, EventArgs e)
        {
            EventHandler handler = AcquisitionCompleted;
            handler?.Invoke(null, e);
        }

        #endregion events

    }

}
