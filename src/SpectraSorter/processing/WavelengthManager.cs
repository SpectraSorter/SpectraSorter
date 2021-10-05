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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spectra.processing
{
    public class WavelengthManager
    {
        #region members
        
        // Instance
        private static WavelengthManager mInstance = null;

        #endregion members

        #region methods

        #region private

        private WavelengthManager()
        {
        }

        #endregion private

        #region public

        public Wavelength AddEmptyWavelength()
        {
            // Add a new Wavelength and return it
            return this.GetWavelength(this.Wavelengths.Count);
        }

        public bool AreThereEmptyWavelengths()
        {
            bool result = false;
            foreach (Wavelength wavelength in Wavelengths)
            {
                if (wavelength.Value == 0.0)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public static void Clear()
        {
            SettingsManager.Wavelengths.Clear();
        }

        public Wavelength GetWavelengthByID(string ID)
        {
            foreach(Wavelength wavelength in Wavelengths)
            {
                if (wavelength.ID.Equals(ID, StringComparison.InvariantCulture))
                {
                    // Found
                    return wavelength;
                }
            }

            // Not found
            return null;
        }

        public Wavelength GetWavelength(int index)
        {
            if (index >= SettingsManager.Wavelengths.Count)
            {
                for (int i = SettingsManager.Wavelengths.Count; i <= index; i++)
                {
                    SettingsManager.Wavelengths.Add(new Wavelength());
                }
            }
            return SettingsManager.Wavelengths[index];
        }

        public Wavelength RemoveWavelength(int index)
        {
            if (index < SettingsManager.Wavelengths.Count)
            {
                SettingsManager.Wavelengths.RemoveAt(index);
            }
            return null;
        }

        public List<Wavelength> Wavelengths
        {
            get
            {
                return SettingsManager.Wavelengths;
            }
        }

        /// <summary>
        /// Extract indices of requested wavelengths for thresholding.
        /// </summary>
        public void MapThresholdingWavelengthsToIndices()
        {
            // Get the wavelengths used for thresholding, sorted
            List<Wavelength> wavelengthsForThresholding = WavelengthManager.Instance.WavelengthsForThresholding;

            // Is there something to process?
            if (wavelengthsForThresholding.Count == 0)
            {
                return;
            }

            if (SpectrumProcessor.Instance.Wavelengths.Length == 0)
            {
                return;
            }

            // Since the wavelengths are sorted, we do not need to always
            // scan the whole array.
            foreach (var wavelength in wavelengthsForThresholding)
            {
                List<double> wavelengthsList = new List<double>(SpectrumProcessor.Instance.Wavelengths);

                // Find the index
                int index = Utils.FindIndexOfClosestValueInSortedList(wavelengthsList, wavelength.Value);

                // Update the index
                wavelength.Index = index;
            }
        }

        /// <summary>
        /// Extract indices of requested wavelengths to plot.
        /// </summary>
        public void MapRequestedWavelengthsToPlotToIndices()
        {
            // Get the wavelengths used for plotting, sorted
            List<Wavelength> wavelengthsForPlotting = WavelengthManager.Instance.WavelengthsForPlotting;

            // Is there something to process?
            if (wavelengthsForPlotting.Count == 0)
            {
                return;
            }

            if (SpectrumProcessor.Instance.Wavelengths.Length == 0)
            {
                return;
            }

            // Since the wavelengths are sorted, we do not need to always
            // scan the whole array.
            foreach (var wavelength in wavelengthsForPlotting)
            {
                List<double> wavelengthsList = new List<double>(SpectrumProcessor.Instance.Wavelengths);

                // Find the index
                int index = Utils.FindIndexOfClosestValueInSortedList(wavelengthsList, wavelength.Value);

                // Update the index
                wavelength.Index = index;
            }
        }

        /// <summary>
        /// Extract indices of requested wavelengths to save to file.
        /// </summary>
        public void MapRequestedWavelengthsToSaveToIndices()
        {
            // Get the wavelengths used for plotting, sorted
            List<Wavelength> wavelengthsForSaving = WavelengthManager.Instance.Wavelengths;

            // Is there something to process?
            if (wavelengthsForSaving.Count == 0)
            {
                return;
            }

            if (SpectrumProcessor.Instance.Wavelengths.Length == 0)
            {
                return;
            }

            // Since the wavelengths are sorted, we do not need to always
            // scan the whole array.
            foreach (var wavelength in wavelengthsForSaving)
            {
                List<double> wavelengthsList = new List<double>(SpectrumProcessor.Instance.Wavelengths);

                // Find the index
                int index = Utils.FindIndexOfClosestValueInSortedList(wavelengthsList, wavelength.Value);

                // Update the index
                wavelength.Index = index;
            }
        }

        /// <summary>
        /// Return a comma-separated list of Wavelength values
        /// </summary>
        /// <param name="wavelengths"></param>
        /// <returns></returns>
        public string WavelengthListToString(List<Wavelength> wavelengths)
        {
            switch (wavelengths.Count)
            {
                case 0:
                    return "";

                case 1:
                    return wavelengths[0].Value.ToString(CultureInfo.InvariantCulture);

                default:

                    StringBuilder builder = new StringBuilder();
                    builder.Append(wavelengths[0].Value.ToString(CultureInfo.InvariantCulture));
                    for (int i = 1; i < wavelengths.Count; i++)
                    {
                        builder.Append(", ");
                        builder.Append(wavelengths[i].Value.ToString(CultureInfo.InvariantCulture));
                    }
                    return builder.ToString();
            }
        }

        /// <summary>
        /// Check whether the passed list of Wavelengths contains the one
        /// with the given ID.
        /// </summary>
        /// <param name="wavelengts">List of Wavelengths.</param>
        /// <param name="ID">Id of the Wavelength to search for.</param>
        /// <returns>True if the wavelength is found, false otherwise.</returns>
        public static bool Contains(List<Wavelength> wavelengts, string ID)
        {
            foreach (Wavelength w in wavelengts)
            {
                if (w.ID.Equals(ID, StringComparison.InvariantCulture))
                {
                    return true;
                }
            }

            return false;
        
        }

        /// <summary>
        /// Check whether the passed list of Wavelengths contains the one
        /// with the given value.
        /// </summary>
        /// <param name="wavelengts">List of Wavelengths.</param>
        /// <param name="value">Value (in nm) of the Wavelength to search for.</param>
        /// <returns>True if the wavelength is found, false otherwise.</returns>
        public static bool Contains(List<Wavelength> wavelengts, float value)
        {
            foreach (Wavelength w in wavelengts)
            {
                if (w.Value.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Clone the given list of Wavelengths.
        /// </summary>
        /// <param name="wavelengths">List of Wavelengths.</param>
        /// <returns>Cloned list of Wavelengths (deep copy).</returns>
        public static List<Wavelength> Clone(List<Wavelength> wavelengths)
        {
            List<Wavelength> copiedList = new List<Wavelength>(wavelengths.Count);
            wavelengths.ForEach((item) =>
            {
                // Use Wavelength copy constructor to deep-copy the object
                copiedList.Add(new Wavelength(item));
            });

            return copiedList;
        }

        #endregion public

        #endregion methods

        #region properties

        /// <summary>
        /// WavelengthManager (singleton) instance.
        /// </summary>
        public static WavelengthManager Instance
        {
            get
            {
                // If the WavelengthManager has not been created yet,
                // instantiate it now.
                if (mInstance == null)
                {
                    mInstance = new WavelengthManager();
                }

                // Return a reference
                return mInstance;
            }
        }
        #endregion properties

        public List<Wavelength> WavelengthsForThresholding
        {
            get
            {
                IEnumerable<Wavelength> wavelengths =
                    from wavelength in SettingsManager.Wavelengths
                    where wavelength.IsForThresholding == true && wavelength.Value > 0
                    orderby wavelength.Value
                    select wavelength;
                return wavelengths.ToList<Wavelength>();
            }
        }

        public List<Wavelength> WavelengthsForPlotting
        {
            get
            {
                IEnumerable<Wavelength> wavelengths =
                    from wavelength in SettingsManager.Wavelengths
                    where wavelength.IsToBePlotted == true && wavelength.Value > 0
                    orderby wavelength.Value
                    select wavelength;
                return wavelengths.ToList<Wavelength>();
            }
        }

        public List<Wavelength> WavelengthsForSaving
        {
            get
            {
                IEnumerable<Wavelength> wavelengths =
                    from wavelength in SettingsManager.Wavelengths
                    where wavelength.IsToBeSaved == true && wavelength.Value > 0
                    orderby wavelength.Value
                    select wavelength;
                return wavelengths.ToList<Wavelength>();
            }
        }
    }
}
