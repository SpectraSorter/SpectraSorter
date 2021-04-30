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
using spectra.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spectra.processing
{
    static class WavelengthMapper
    {

        /// <summary>
        /// Checks that the requested start pixel index is valid or corrects it;
        /// then synchronizes SaveStartPixel with SaveStartWavelength.
        /// </summary>
        public static void SyncStartPixelToWavelengthAndStore(int pixel, double[] wavelengths)
        {
            if (pixel > SettingsManager.SaveEndPixel - 1)
            {
                pixel = SettingsManager.SaveEndPixel - 1;
            }

            if (pixel < 0)
            {
                pixel = 0;
            }

            SettingsManager.SaveStartPixel = pixel;
            SettingsManager.SaveStartWavelength = wavelengths[pixel];
        }

        /// <summary>
        /// Checks that the requested end pixel index is valid or corrects it;
        /// then synchronizes SaveEndPixel with SaveEndWavelength.
        /// </summary>
        public static void SyncEndPixelToWavelengthAndStore(int pixel, double[] wavelengths)
        {
            if (pixel < SettingsManager.SaveStartPixel + 1)
            {
                pixel = SettingsManager.SaveStartPixel + 1;
            }

            if (pixel > wavelengths.Length - 1)
            {
                pixel = wavelengths.Length - 1;
            }

            SettingsManager.SaveEndPixel = pixel;
            SettingsManager.SaveEndWavelength = wavelengths[pixel];
        }

        /// <summary>
        /// Checks that the requested start wavelength is valid or corrects it
        /// (more precisely, the associated pixel index); then synchronizes 
        /// SaveStartWavelength with SaveStartPixel.
        /// </summary>
        public static void SyncStartWavelengthToPixelAndStore(double wavelength, double[] wavelengths)
        {
            int pixel = Utils.FindIndexOfClosestValueInSortedArray(wavelengths, wavelength);

            if (pixel > SettingsManager.SaveEndPixel - 1)
            {
                pixel = SettingsManager.SaveEndPixel - 1;
            }

            SettingsManager.SaveStartPixel = pixel;
            SettingsManager.SaveStartWavelength = wavelengths[pixel];
        }


        /// <summary>
        /// Checks that the requested end wavelength is valid or corrects it
        /// (more precisely, the associated pixel index); then synchronizes 
        /// SaveEndWavelength with SaveEndPixel.
        /// </summary>
        public static void SyncEndPixelToWavelengthAndStore(double wavelength, double[] wavelengths)
        {
            int pixel = Utils.FindIndexOfClosestValueInSortedArray(wavelengths, wavelength);

            if (pixel < SettingsManager.SaveStartPixel + 1)
            {
                pixel = SettingsManager.SaveStartPixel + 1;
            }

            if (pixel > wavelengths.Length - 1)
            {
                pixel = wavelengths.Length - 1;
            }

            SettingsManager.SaveEndPixel = pixel;
            SettingsManager.SaveEndWavelength = wavelengths[pixel];
        }
    }
}
