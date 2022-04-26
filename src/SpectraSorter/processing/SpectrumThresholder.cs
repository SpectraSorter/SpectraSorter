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

using spectra.state;
using spectra.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spectra.processing
{
    class SpectrumThresholder
    {
        #region methods

        #region public

        /// <summary>
        /// Threshold the spectrum based on current settings.
        /// </summary>
        /// <param name="currentSpectrum">Current spectrum as array of integers.</param>
        /// <returns>True if the spectrum thresholding was satisfied, false otherwise.</returns>
        public static bool IsSpectrumSatisfyingThresholding(int[] currentSpectrum)
        {
            // Initialize result
            bool[] result = new bool[WavelengthManager.Instance.WavelengthsForThresholding.Count];

            // Process all wawelengths
            for (int i = 0; i < WavelengthManager.Instance.WavelengthsForThresholding.Count; i++)
            {
                // Make sure the wavelength is enabled
                Wavelength wavelength = WavelengthManager.Instance.WavelengthsForThresholding[i];

                if (wavelength.IsForThresholding == false)
                {
                    throw new Exception("The wavelength must have the IsForThresholding flag active!");
                }

                // Get threshold
                float threshold = wavelength.ThresholdValue;

                // Get intensity
                float intensity = (float)currentSpectrum[wavelength.Index];

                // Must this wavelength's intensity be above or below threshold?
                if (wavelength.IsThresholdAbove == true)
                {
                    // The wavelength must be larger to trigger
                    result[i] = intensity > threshold;
                }
                else
                {
                    // The wavelength must be smaller to trigger
                    result[i] = intensity < threshold;
                }

                // Should we continue?
                if (SettingsManager.SpectrumThresholdingAllSatisfiedEnabled == true && result[i] == false)
                {
                    // All wavelengths should pass thresholding, but here we already found one
                    // that does not: we can return failure.
                    return false;
                }

                // Should we continue?
                if (SettingsManager.SpectrumThresholdingAllSatisfiedEnabled == false && result[i] == true)
                {
                    // Just one wavelength that satisfy its threshold is enough to trigger.
                    return true;
                }
            }

            // Return failure ailure
            return false;
        }

        /// <summary>
        /// Threshold the spectrum based on current settings.
        /// </summary>
        /// <param name="currentSpectrum">Current spectrum as array of floats.</param>
        /// <returns>True if the spectrum thresholding was satisfied, false otherwise.</returns>
        public static bool IsSpectrumSatisfyingThresholding(float[] currentSpectrum)
        {
            // Initialize result
            bool[] result = new bool[WavelengthManager.Instance.WavelengthsForThresholding.Count];

            // Cache the query
            List<Wavelength> wavelengthsForThresholding = WavelengthManager.Instance.WavelengthsForThresholding;

            // Process all wavelengths
            for (int i = 0; i < wavelengthsForThresholding.Count; i++)
            {
                // Make sure the wavelength is enabled
                Wavelength wavelength = wavelengthsForThresholding[i];

                if (wavelength.IsForThresholding == false)
                {
                    throw new Exception("The wavelength must have the IsForThresholding flag active!");
                }

                // Get threshold
                float threshold = wavelength.ThresholdValue;

                // Get intensity
                float intensity = currentSpectrum[wavelength.Index];

                // Must this wavelength's intensity be above or below threshold?
                if (wavelength.IsThresholdAbove == true)
                {
                    // The wavelength must be larger to trigger
                    result[i] = intensity > threshold;
                }
                else
                {
                    // The wavelength must be smaller to trigger
                    result[i] = intensity < threshold;
                }

                // Should we continue?
                if (SettingsManager.SpectrumThresholdingAllSatisfiedEnabled == true && result[i] == false)
                {
                    // All wavelengths should pass thresholding, but here we already found one
                    // that does not: we can return failure.
                    return false;
                }

                // Should we continue?
                if (SettingsManager.SpectrumThresholdingAllSatisfiedEnabled == false && result[i] == true)
                {
                    // Just one wavelength that satisfy its threshold is enough to trigger.
                    return true;
                }
            }

            /* 
             
            We tested all wavelengths. If we are here, we either have:
            
             - SpectrumThresholdingAllSatisfiedEnabled == true and all intensities > threshold
             - SpectrumThresholdingAllSatisfiedEnabled == false and all intensities < threshold

            Any other combination would have caused the function to return earlier.

            In the first case we return true, in the second we return false.
            */
            if (SettingsManager.SpectrumThresholdingAllSatisfiedEnabled == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion public

        #endregion methods

    }
}
