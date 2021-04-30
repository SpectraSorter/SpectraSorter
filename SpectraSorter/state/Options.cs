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

namespace spectra.state
{
    public static class Options
    {
        /// <summary>
        /// Plot type
        /// </summary>
        public enum PlotType
        {
            OUTPUT = 0,
            TIMESERIES = 1,
            DARK_SPECTRUM = 2,
            REFERENCE_SPECTRUM = 3,
            CORRECTED_REFERENCE_SPECTRUM = 4,
            ACCUMULATED_SPECTRA = 5,
            ACCUMULATED_TIMESERIES = 6,
            ACCUMULATING_SPECTRUM = 7,
            ACCUMULATING_TIMESERIES = 8
        };

        /// <summary>
        /// Range units
        /// </summary>
        public enum RangeUnits
        {
            Pixels = 0,
            Wavelengths = 1
        }

        /// <summary>
        /// Input spectrum type
        /// </summary>
        public enum InputSpectrumType
        {
            RAW = 0,
            DARK = 1,
            REFERENCE = 2,
            CORRECTED_REFERENCE
        }

        /// <summary>
        /// Result spectrum type
        /// </summary>
        public enum ResultSpectrumType
        {
            RAW_SPECTRUM = 0,
            DARK_CORRECTED = 1,
            ABSORBANCE = 2,
            TRANSMISSION = 3,
        }

        /// <summary>
        /// Acquisition duration units.
        /// </summary>
        public enum AcquisitionDurationUnits
        {
            SECONDS = 0,
            MINUTES = 1,
            HOURS = 2,
            SPECTRA = 3
        }

        public enum ReferenceType
        {
            NONE = 0,
            STATIC = 1,
            STATIC_SINGLE = 2,
            STATIC_ACCUMULATED = 3,
            DYNAMIC = 4
        }
    }
}