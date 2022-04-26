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

namespace spectra.processing
{
    public static class SpectrumConstants
    {
        // Max absorbance level
        public const double MAX_ABSORBANCE = 4.0;

        // Max transmission level
        public const double MAX_TRANSMISSION = 32737;

        // Max saturation level (max raw spectrum instensity value)
        public const UInt16 SATURATION_LEVEL = 65535;

        // Number of spectrum response buffers to allocate
        public const int NUM_RESPONSES_TO_ALLOCATE = 25;

        // Default IO timeout (UNUSED!)
        public const int SEND_TIMEOUT_MS = 1000;
    }
}
