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

namespace spectra.ui.components
{
    public partial class MainChart
    {
        private void RegisterEventHandlers()
        {
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(MainChart.CurrentDomain_UnhandledException);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Try recovering from unhandled exceptions that are thrown when setting axis 
            // limits outside of the data range.
            MainWindow.mainPlotter.XAxisResetBothBoundsAndAllowedBounds();
            MainWindow.mainPlotter.YAxisResetBothBoundsAndAllowedBounds();
            MainWindow.mainPlotter.XAxisLowerBound = 157.0;
            MainWindow.mainPlotter.XAxisLowerBound = 1032.0;
            MainWindow.mainPlotter.YAxisLowerBound = 0.0;
            MainWindow.mainPlotter.YAxisUpperBound = 65535;
        }

    }
}
