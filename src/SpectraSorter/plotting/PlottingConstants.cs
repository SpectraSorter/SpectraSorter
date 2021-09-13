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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spectra.plotting
{
    class PlottingConstants
    {
        // Standard series names
        public const string OUTPUT_SERIES_NAME = "Output";
        public const string DARK_SERIES_NAME = "Dark";
        public const string REFERENCE_SERIES_NAME = "Reference";
        public const string CORR_REFERENCE_SERIES_NAME = "Reference (corr)";
        public const string ACCUMULATING_SPECTRUM_SERIES_NAME = "Accumulating";
        public const string ACCUMULATED_SPECTRA_SERIES_NAME = "Accumulated";

        // Standard series colors
        public static Color OUTPUT_SERIES_COLOR = Color.DimGray;
        public static Color DARK_SERIES_COLOR = Color.Goldenrod;
        public static Color REFERENCE_SERIES_COLOR = Color.Crimson;
        public static Color CORR_REFERENCE_SERIES_COLOR = Color.DarkGreen;
        public static Color ACCUMULATING_SPECTRUM_SERIES_COLOR = Color.Gold;
        public static Color ACCUMULATED_SPECTRA_SERIES_COLOR = Color.OrangeRed;
    }
}
