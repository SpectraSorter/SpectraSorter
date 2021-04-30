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
using System.Drawing;
using System.Globalization;

namespace spectra.processing
{
    [Serializable]
    public class Wavelength
    {
        #region properties

        public float Value { get; set; } = 0.0f;
        public string ID { get; set; } = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
        public string ThresholdID { get; set; } = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
        public int Index { get; set; } = -1;
        public float ThresholdValue { get; set; } = 0;
        public bool IsForThresholding { get; set; } = false;
        public bool IsToBePlotted { get; set; } = false;
        public bool IsToBeSaved { get; set; } = false;
        public bool IsThresholdAbove { get; set; } = true;
        public Int32 SeriesColor { get; set; } = Color.DimGray.ToArgb();
        private string Arrow
        {
            get { return this.IsThresholdAbove ? "↑" : "↓"; }
            set { }
        }

        public string SeriesName
        {
            get { return this.Value.ToString(CultureInfo.InvariantCulture) + "nm"; }
            set { }
        }

        public string ThresholdSeriesName
        {
            get { return "T(" + this.SeriesName + " " + this.Arrow + ")"; }
            set { }
        }

        public override string ToString()
        {
            string thresholdingString = "";
            if (IsForThresholding == true)
            {
                thresholdingString = this.ThresholdSeriesName;
            }
            else
            {
                thresholdingString = this.SeriesName;
            }

            string plottingString = this.IsToBePlotted == true ? " [P]" : "";

            string savingString = this.IsToBeSaved == true ? " [S]" : "";

            string colorString = " " + this.SeriesColor.ToString(CultureInfo.InvariantCulture);

            return thresholdingString + plottingString + savingString + colorString;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Wavelength()
        {
            // Instantiate with default values.
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="other">Wavelength object to be copied.</param>
        public Wavelength(Wavelength other)
        {
            if (other == null)
            {
                throw new NullReferenceException("Wavelength object 'other' is null.");
            }

            this.Value = other.Value;
            this.ID = other.ID;
            this.ThresholdID = other.ThresholdID;
            this.Index = other.Index;
            this.ThresholdValue = other.ThresholdValue;
            this.IsForThresholding = other.IsForThresholding;
            this.IsToBePlotted = other.IsToBePlotted;
            this.IsToBeSaved = other.IsToBeSaved;
            this.IsThresholdAbove = other.IsThresholdAbove;
            this.SeriesColor = other.SeriesColor;
        }

        #endregion properties
    }
}
