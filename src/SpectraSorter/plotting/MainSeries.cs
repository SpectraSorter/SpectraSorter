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
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace spectra.plotting
{
    public class MainSeries : Series
    {
        private bool mIsStandardSeries = false;
        private bool mIsThresholdSeries = false;
        private bool mIsTimeSeries = false;
        private bool mIsTriggerSeries = false;

        public string ID { get; private set; }

        public bool IsStandardSeries()
        {
            return mIsStandardSeries;
        }

        public bool IsThresholdSeries()
        {
            return mIsThresholdSeries;
        }

        public bool IsTimeSeries()
        {
            return mIsTimeSeries;
        }

        public bool IsTriggerSeries()
        {
            return mIsTriggerSeries;
        }

        public void SetAsStandardSeries()
        {
            if (this.mIsStandardSeries == true)
            {
                return;
            }

            // Set the correct type
            this.mIsStandardSeries = true;
            this.mIsThresholdSeries = false;
            this.mIsTimeSeries = false;
            this.mIsTriggerSeries = false;

            // Set line properties
            this.ChartType = SeriesChartType.Line;
            this.BorderWidth = 1;
            this.BorderDashStyle = ChartDashStyle.Solid;
        }

        public void SetAsThresholdSeries()
        {
            if (this.mIsThresholdSeries == true)
            {
                return;
            }

            // Set the correct type
            this.mIsStandardSeries = false;
            this.mIsThresholdSeries = true;
            this.mIsTimeSeries = false;
            this.mIsTriggerSeries = false;

            // Set line properties
            this.ChartType = SeriesChartType.Line;
            this.BorderWidth = 3;
            this.BorderDashStyle = ChartDashStyle.Dot;
        }

        public void SetAsTimeSeries()
        {
            if (this.mIsTimeSeries == true)
            {
                return;
            }

            // Set the correct type
            this.mIsStandardSeries = false;
            this.mIsThresholdSeries = false;
            this.mIsTimeSeries = true;
            this.mIsTriggerSeries = false;

            // Set line properties
            this.ChartType = SeriesChartType.Line;
            this.BorderWidth = 1;
            this.BorderDashStyle = ChartDashStyle.Solid;
        }

        public void SetAsTriggerSeries()
        {
            if (this.mIsTriggerSeries == true)
            {
                return;
            }

            // Set the correct type
            this.mIsStandardSeries = false;
            this.mIsThresholdSeries = false;
            this.mIsTimeSeries = false;
            this.mIsTriggerSeries = true;

            // Set line properties
            this.Color = System.Drawing.Color.Black;
            this.ChartType = SeriesChartType.Line;
            this.BorderWidth = 1;
            this.BorderDashStyle = ChartDashStyle.Dot;

            // This series type is never visible in the legend
            this.IsVisibleInLegend = false;
        }

        public MainSeries(string seriesName, string ID = null) : base(seriesName) 
        {
            // Set series name
            this.Name = seriesName;

            // Set ID
            if (ID == null)
            {
                // Create unique ID
                ID = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);
            }
            this.ID = ID;

            // Set default type (force)
            this.mIsStandardSeries = false;
            this.SetAsStandardSeries();
        }
    }
}
