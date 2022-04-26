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

using spectra.plotting;
using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace spectra.ui.components
{
    public partial class MainChart : Chart
    {
        #region members

        private Random mRandom = new Random();

        #endregion members

        #region methods

        #region public

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainChart() : base() 
        {
            this.RegisterEventHandlers();
        }

        /// <summary>
        /// Return existing Series with given name.
        /// </summary>
        /// If the Series does not exist, the method returns null.
        /// <param name="seriesName">Name of the Series to be used.</param>
        /// <returns>Series with given name if it exists, or null.</returns>
        public MainSeries GetSeriesByName(string seriesName)
        {
            return (MainSeries)this.Series.FindByName(seriesName);
        }

        /// <summary>
        /// Return existing Series with given ID.
        /// </summary>
        /// If the Series does not exist, the method returns null.
        /// <param name="ID">ID of the Series to be used.</param>
        /// <returns>Series with given ID if it exists, or null.</returns>
        public MainSeries GetSeriesByID(string seriesName)
        {
            return (MainSeries)this.Series.FindByName(seriesName);
        }

        /// <summary>
        /// Use Series with given name.
        /// </summary>
        /// 
        /// If the Series does not exist it will be created.
        /// <param name="seriesName">Name of the Series to be used.</param>
        /// <param name="clearPoints">Set to true to return a Series with no Points.</param>
        /// <returns>Series with given name (optionally, with cleared data).</returns>
        public MainSeries UseSeriesByName(string seriesName, bool clearPoints=false)
        {
            MainSeries s = this.FindSeriesByName(seriesName);
            if (s == null)
            {
                s = new MainSeries(seriesName);

                // Assign a color
                switch (seriesName)
                {
                    case PlottingConstants.OUTPUT_SERIES_NAME:

                        s.Color = PlottingConstants.OUTPUT_SERIES_COLOR;

                        break;
                        
                    case PlottingConstants.DARK_SERIES_NAME:

                        s.Color = PlottingConstants.DARK_SERIES_COLOR;

                        break;

                    case PlottingConstants.REFERENCE_SERIES_NAME:

                        s.Color = PlottingConstants.REFERENCE_SERIES_COLOR;

                        break;

                    case PlottingConstants.CORR_REFERENCE_SERIES_NAME:

                        s.Color = PlottingConstants.CORR_REFERENCE_SERIES_COLOR;

                        break;

                    case PlottingConstants.ACCUMULATING_SPECTRUM_SERIES_NAME:

                        s.Color = PlottingConstants.ACCUMULATING_SPECTRUM_SERIES_COLOR;

                        break;

                    case PlottingConstants.ACCUMULATED_SPECTRA_SERIES_NAME:

                        s.Color = PlottingConstants.ACCUMULATED_SPECTRA_SERIES_COLOR;

                        break;

                    default:

                        // Give a random color
                        int A = 255;
                        int R = mRandom.Next(0, 256);
                        int G = mRandom.Next(0, 256);
                        int B = mRandom.Next(0, 256);
                        s.Color = Color.FromArgb(A, R, G, B);

                        break;
                }

                this.Series.Add(s);
            }

            if (clearPoints)
            {
                s.Points.Clear();
            }

            s.IsVisibleInLegend = true;
            s.Enabled = true;

            return s;
        }

        /// <summary>
        /// Use Series with given name.
        /// </summary>
        /// 
        /// If the Series does not exist it will be created.
        /// <param name="seriesID">ID of the series.</param>
        /// <param name="seriesName">New name of the Series. Set to null to leave unchanged.</param>
        /// <param name="clearPoints">Set to true to return a Series with no Points.</param>
        /// <returns>Series with given name (optionally, with cleared data).</returns>
        public MainSeries UseSeriesByID(string seriesID, string seriesName = null, bool clearPoints = false)
        {
            MainSeries s = this.FindSeriesByID(seriesID);
            if (s == null)
            {
                s = new MainSeries(seriesName, seriesID);

                // Assign a color
                switch (seriesName)
                {
                    case PlottingConstants.OUTPUT_SERIES_NAME:

                        s.Color = PlottingConstants.OUTPUT_SERIES_COLOR;

                        break;

                    case PlottingConstants.DARK_SERIES_NAME:

                        s.Color = PlottingConstants.DARK_SERIES_COLOR;

                        break;

                    case PlottingConstants.REFERENCE_SERIES_NAME:

                        s.Color = PlottingConstants.REFERENCE_SERIES_COLOR;

                        break;

                    case PlottingConstants.CORR_REFERENCE_SERIES_NAME:

                        s.Color = PlottingConstants.CORR_REFERENCE_SERIES_COLOR;

                        break;

                    case PlottingConstants.ACCUMULATING_SPECTRUM_SERIES_NAME:

                        s.Color = PlottingConstants.ACCUMULATING_SPECTRUM_SERIES_COLOR;

                        break;

                    case PlottingConstants.ACCUMULATED_SPECTRA_SERIES_NAME:

                        s.Color = PlottingConstants.ACCUMULATED_SPECTRA_SERIES_COLOR;

                        break;

                    default:

                        // Give a random color
                        int A = 255;
                        int R = mRandom.Next(0, 256);
                        int G = mRandom.Next(0, 256);
                        int B = mRandom.Next(0, 256);
                        s.Color = Color.FromArgb(A, R, G, B);

                        break;
                }

                this.Series.Add(s);
            }

            if (clearPoints)
            {
                s.Points.Clear();
            }

            // Rename if requested            
            if (seriesName != null && !s.Name.Equals(seriesName))
            {
                s.Name = seriesName;
            }

            s.IsVisibleInLegend = true;
            s.Enabled = true;

            return s;
        }

        public MainSeries FindSeriesByName(string seriesName)
        {
            return (MainSeries)this.Series.FindByName(seriesName);
        }

        public MainSeries FindSeriesByID(string seriesID)
        {
            foreach(MainSeries s in this.Series)
            {
                if (s.ID.Equals(seriesID))
                {
                    return s;
                }
            }
            return null;
        }

        /// <summary>
        /// Clear all Series from the plot.
        /// </summary>
        public void ClearAllSeries()
        {
            this.Series.Clear();
        }

        /// <summary>
        /// Hide all Series on the plot.
        /// </summary>
        public void HideAllSeries()
        {
            foreach (var series in this.Series)
            {
                series.Enabled = false;
                series.IsVisibleInLegend = false;
            }
        }

        /// <summary>
        /// Return the number of visible series in the plot.
        /// </summary>
        /// <returns>Number of visible series in the plot.</returns>
        public int GetNumberOfVisibleSeries()
        {
            int num = 0;
            foreach (var s in this.Series)
            {
                if (s.Enabled == true)
                {
                    num++;
                }
            }
            return num;
        }

        public (double, double, double, double) GetCurrentDataRange()
        {
            double minX = Double.PositiveInfinity;
            double maxX = Double.NegativeInfinity;
            double minY = Double.PositiveInfinity;
            double maxY = Double.NegativeInfinity;

            foreach (var s in this.Series)
            {
                if (s.Points.Count == 0)
                {
                    continue;
                }

                foreach (DataPoint point in s.Points)
                {
                    if (point.XValue < minX)
                    {
                        minX = point.XValue;
                    }

                    if (point.XValue > maxX)
                    {
                        maxX = point.XValue;
                    }

                    if (point.YValues[0] < minY)
                    {
                        minY = point.YValues[0];
                    }

                    if (point.YValues[0] > maxY)
                    {
                        maxY = point.YValues[0];
                    }
                }
            }

            return (minX, maxX, minY, maxY);
        }

        #endregion public

        #endregion methods

        #region properties

        /// <summary>
        /// Return X axis.
        /// </summary>
        public Axis AxisX { get => this.ChartAreas[0].AxisX; }

        /// <summary>
        /// Return Y axis.
        /// </summary>
        public Axis AxisY { get => this.ChartAreas[0].AxisY; }

        #endregion properties
    }
}