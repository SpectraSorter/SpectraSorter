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

using OBP_Library;
using System;
using System.Globalization;
using System.Linq;

namespace spectra.plotting
{
    /// <summary>
    /// This class maintain consistent axis limits for plotting in a DataVisualization.Charting.Chart.
    /// </summary>
    ///
    /// The lower (LowerBound) and upper bound (UpperBound) values define the axis limits in the plot.
    ///
    /// The lower bound has an optional maximum allowed value (MaxAllowedValueForLowerBound), while
    /// the upper bound has an optional minimum allowed value (MinAllowedValueForUpperBound).
    /// Please notice that MinAllowedValueForUpperBound is always smaller or equal MaxAllowedValueForLowerBound.
    public class AxisLimits
    {
        /// <summary>
        /// Private class members.
        /// </summary>
        private double mLowerBound, mUpperBound, mMaxAllowedValueForLowerBound, mMinAllowedValueForUpperBound;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lowerBound">
        /// Minimum axis value (optional, default is Double.NegativeInfinity === not set)
        /// </param>
        /// <param name="upperBound">
        /// Maximum axis value (optional, default is Double.PositiveInfinity === not set)
        /// </param>
        /// <param name="maxAllowedValueForLowerBound">
        /// Maximum allowed value for lower bound (optional, default is Double.PositiveInfinity ===
        /// not set)
        /// </param>
        /// <param name="minAllowedValueForUpperBound">
        /// Minimum allowed value for upper bound (optional, default is Double.NegativeInfinity -&gt;
        /// not set)
        /// </param>
        public AxisLimits(
            double lowerBound = Double.NegativeInfinity,
            double upperBound = Double.PositiveInfinity,
            double maxAllowedValueForLowerBound = Double.PositiveInfinity,
            double minAllowedValueForUpperBound = Double.NegativeInfinity)
        {
            this.mLowerBound = lowerBound;
            this.mUpperBound = upperBound;
            this.mMaxAllowedValueForLowerBound = maxAllowedValueForLowerBound;
            this.mMinAllowedValueForUpperBound = minAllowedValueForUpperBound;
        }

        /// <summary>
        /// Current lower bound of the Axis.
        ///
        /// If LowerBound is Double.NegativeInfinity, it is considered not set. If LowerBound is
        /// larger than UpperBound, it is reset to (UpperBound - 1.0). If LowerBound is larger than
        /// MaxAllowedValueForLowerBound, it is reset to MaxAllowedValueForLowerBound.
        /// </summary>
        public double LowerBound
        {
            get => this.mLowerBound;
            set
            {
                if (Double.IsNegativeInfinity(value))
                {
                    this.mLowerBound = Double.NegativeInfinity;
                }
                else
                {
                    double tmp = value;

                    if (tmp >= this.mUpperBound)
                    {
                        tmp = this.mUpperBound - 1.0;
                    }

                    if (tmp > this.mMaxAllowedValueForLowerBound)
                    {
                        tmp = this.mMaxAllowedValueForLowerBound;
                    }

                    this.mLowerBound = tmp;
                }
            }
        }

        /// <summary>
        /// Current minimum allowed value of the Axis.
        ///
        /// If MaxAllowedValueForLowerBound is Double.PositiveInfinity, it is considered not set. If
        /// MaxAllowedValueForLowerBound is smaller than MinAllowedValueForUpperBound, it is reset to
        /// MinAllowedValueForUpperBound. If MaxAllowedValueForLowerBound is smaller than LowerBound,
        /// LowerBound is reset to MaxAllowedValueForLowerBound.
        /// </summary>
        public double MaxAllowedValueForLowerBound
        {
            get => this.mMaxAllowedValueForLowerBound;
            set
            {
                if (Double.IsPositiveInfinity(value))
                {
                    this.mMaxAllowedValueForLowerBound = Double.PositiveInfinity;
                }
                else
                {
                    double tmp;

                    if (value < this.MinAllowedValueForUpperBound)
                    {
                        tmp = this.mMinAllowedValueForUpperBound;
                    }
                    else
                    {
                        tmp = value;
                    }

                    if (tmp < this.mLowerBound)
                    {
                        this.mLowerBound = tmp;
                    }
                    this.mMaxAllowedValueForLowerBound = tmp;
                }
            }
        }

        /// <summary>
        /// Current maximum allowed value of the Axis.
        ///
        /// If MinAllowedValueForUpperBound is Double.NegativeInfinity, it is considered not set.
        /// If MinAllowedValueForUpperBound is larger than MaxAllowedValueForLowerBound, it is reset to MaxAllowedValueForLowerBound.
        /// If MinAllowedValueForUpperBound is larger than UpperBound, UpperBound is reset to MinAllowedValueForUpperBound.
        /// </summary>
        public double MinAllowedValueForUpperBound
        {
            get => this.mMinAllowedValueForUpperBound;
            set
            {
                if (Double.IsNegativeInfinity(value))
                {
                    this.mMinAllowedValueForUpperBound = Double.NegativeInfinity;
                }
                else
                {
                    double tmp;

                    if (value > this.MaxAllowedValueForLowerBound)
                    {
                        tmp = this.mMaxAllowedValueForLowerBound;
                    }
                    else
                    {
                        tmp = value;
                    }

                    if (tmp > this.mUpperBound)
                    {
                        this.mUpperBound = tmp;
                    }
                    this.mMinAllowedValueForUpperBound = tmp;
                }
            }
        }

        /// <summary>
        /// Current upper bound of the Axis.
        ///
        /// If UpperBound is Double.PositiveInfinity, it is considered not set.
        /// If UpperBound is smaller than LowerBound, it is reset to (LowerBound + 1.0).
        /// If UpperBound is smaller than MinAllowedValueForUpperBound, it is reset to MinAllowedValueForUpperBound.
        /// </summary>
        public double UpperBound
        {
            get => this.mUpperBound;
            set
            {
                if (Double.IsPositiveInfinity(value))
                {
                    this.mUpperBound = Double.PositiveInfinity;
                }
                else
                {
                    double tmp = value;

                    if (tmp <= this.mLowerBound)
                    {
                        tmp = this.mLowerBound + 1.0;
                    }

                    if (tmp < this.MinAllowedValueForUpperBound)
                    {
                        tmp = this.MinAllowedValueForUpperBound;
                    }

                    this.mUpperBound = tmp;
                }
            }
        }

        /// <summary>
        /// Is the lower bound set?
        /// </summary>
        /// <returns>True if the lower bound is set, false otherwise.</returns>
        public bool IsLowerBoundSet()
        {
            return !Double.IsNegativeInfinity(this.mLowerBound);
        }

        /// <summary>
        /// Is the upper bound set?
        /// </summary>
        /// <returns>True if the upper bound is set, false otherwise.</returns>
        public bool IsUpperBoundSet()
        {
            return !Double.IsPositiveInfinity(this.mUpperBound);
        }

        /// <summary>
        /// Parses a string representation and tries to set it as LowerBound.
        /// </summary>
        ///
        /// If "", LowerBound is set to Double.NegativeInfinity.
        ///
        /// <param name="valueStr">String representation of LowerBound.</param>
        /// <returns>The value that was set.</returns>
        public double LowerBoundFromString(String valueStr)
        {
            if (valueStr == null || valueStr.Length == 0)
            {
                this.LowerBound = Double.NegativeInfinity;
            }
            else
            {
                // If parsing does not work, we do not change
                // current LowerBound and just return it.
                if (Double.TryParse(valueStr, out double tmp))
                {
                    // Try setting the value
                    this.LowerBound = tmp;
                }
            }

            // Return current value of LowerBound
            return this.LowerBound;
        }

        /// <summary>
        /// Returns a String representation of the lower bound.
        ///
        /// If the lower bound is not set, returns "".
        /// </summary>
        /// <returns>A string representation of the lower bound, or "" if not set.</returns>
        public String LowerBoundToString()
        {
            if (this.IsLowerBoundSet())
            {
                return this.mLowerBound.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Reset the lower bound of the Axis.
        /// </summary>
        public void ResetLowerBound()
        {
            this.mLowerBound = Double.NegativeInfinity;
        }

        /// <summary>
        /// Reset the maximum allowed value for the lower bound the Axis.
        /// </summary>
        public void ResetMaxAllowedValueForLowerBound()
        {
            this.mMaxAllowedValueForLowerBound = Double.PositiveInfinity;
        }

        /// <summary>
        /// Reset the minimum allowed value for the upper bound of the Axis.
        /// </summary>
        public void ResetMinAllowedValueForUpperBound()
        {
            this.mMinAllowedValueForUpperBound = Double.NegativeInfinity;
        }

        /// <summary>
        /// Reset the upper bound of the Axis.
        /// </summary>
        public void ResetUpperBound()
        {
            this.mUpperBound = Double.PositiveInfinity;
        }

        /// <summary>
        /// Extract the minimum and maximum allowed values for the bounds from a subset of an array of floats.
        /// </summary>
        /// <param name="spectrum">Array from which to extract the minimum and maximum data points.</param>
        /// <param name="firstIndex">First index of the array to consider.</param>
        /// <param name="lastIndex">Last index of the array to consider.</param>
        public void SetMinMaxAllowedValuesForBoundsFromArrayAndRange(float[] array, int firstIndex, int lastIndex)
        {
            if (firstIndex < 0)
            {
                firstIndex = 0;
            }
            if (lastIndex > (array.Length - 1))
            {
                lastIndex = (array.Length - 1);
            }
            float min = float.MaxValue;
            float max = float.MinValue;
            for (int i = firstIndex; i <= lastIndex; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }

                if (array[i] < min)
                {
                    min = array[i];
                }

            }

            double newMin = (double)min;
            double newMax = (double)max;
            if (newMin >= newMax)
            {
                newMax = newMin + 1.0;
            }
            this.mMaxAllowedValueForLowerBound = newMax;
            this.mMinAllowedValueForUpperBound = newMin;
        }

        /// <summary>
        /// Extract the minimum and maximum allowed values for the bounds from a subset of an array of shorts.
        /// </summary>
        /// <param name="spectrum">Array from which to extract the minimum and maximum data points.</param>
        /// <param name="firstIndex">First index of the array to consider.</param>
        /// <param name="lastIndex">Last index of the array to consider.</param>
        public void SetMinMaxAllowedValuesForBoundsFromArrayAndRange(ushort[] array, int firstIndex, int lastIndex)
        {
            if (firstIndex < 0)
            {
                firstIndex = 0;
            }
            if (lastIndex > (array.Length - 1))
            {
                lastIndex = (array.Length - 1);
            }
            ushort min = ushort.MaxValue;
            ushort max = ushort.MinValue;
            for (int i = firstIndex; i <= lastIndex; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }

                if (array[i] < min)
                {
                    min = array[i];
                }

            }

            double newMin = (double)min;
            double newMax = (double)max;
            if (newMin >= newMax)
            {
                newMax = newMin + 1.0;
            }
            this.mMaxAllowedValueForLowerBound = newMax;
            this.mMinAllowedValueForUpperBound = newMin;
        }

        /// <summary>
        /// Extract the minimum and maximum allowed values for the bounds from a spectrum.
        /// </summary>
        /// <param name="spectrum">Spectrum from which to extract the minimum and maximum data points.</param>
        public void SetMinMaxAllowedValueForBoundsFromSeries(RawSpectrumWithMetadataBuffer spectrum)
        {
            double min = Double.MaxValue;
            double max = Double.MinValue;
            for (int i = 0; i < spectrum.NumPixels; i++)
            {
                if (spectrum.GetPixelFloat(i) > max)
                {
                    max = spectrum.GetPixelFloat(i);
                }
                if (spectrum.GetPixelFloat(i) < max)
                {
                    min = spectrum.GetPixelFloat(i);
                }
            }

            if (min >= max)
            {
                max = min + 1.0;
            }
            this.mMaxAllowedValueForLowerBound = max;
            this.mMinAllowedValueForUpperBound = min;
        }

        /// <summary>
        /// Extract the minimum and maximum allowed values for bounds from an array of floats.
        /// </summary>
        /// <param name="array">Array from which to extract the minimum and maximum data points.</param>
        public void SetMinMaxAllowedValuesForBoundsFromArray(float[] array)
        {
            double min = (double)array.Min();
            double max = (double)array.Max();
            if (min >= max)
            {
                max = min + 1.0;
            }
            this.mMaxAllowedValueForLowerBound = max;
            this.mMinAllowedValueForUpperBound = min;
        }

        /// <summary>
        /// Extract the minimum and maximum allowed values for bounds from an array of doubles.
        /// </summary>
        /// <param name="array">Array from which to extract the minimum and maximum data points.</param>
        public void SetMinMaxAllowedValuesForBoundsFromArray(double[] array)
        {
            double min = (double)array.Min();
            double max = (double)array.Max();
            if (min >= max)
            {
                max = min + 1.0;
            }
            this.mMaxAllowedValueForLowerBound = max;
            this.mMinAllowedValueForUpperBound = min;
        }

        /// <summary>
        /// Extract the minimum and maximum allowed values for bounds from an array of ushorts.
        /// </summary>
        /// <param name="array">Array from which to extract the minimum and maximum data points.</param>
        public void SetMinMaxAllowedValuesForBoundsFromArray(ushort[] array)
        {
            double min = (double)array.Min();
            double max = (double)array.Max();
            if (min >= max)
            {
                max = min + 1.0;
            }
            this.mMaxAllowedValueForLowerBound = max;
            this.mMinAllowedValueForUpperBound = min;
        }

        /// <summary>
        /// Parses a string representation and tries to set it as UpperBound.
        /// </summary>
        ///
        /// If "", UpperBound is set to Double.PositiveInfinity.
        ///
        /// <param name="valueStr">String representation of UpperBound.</param>
        /// <returns>The value that was set.</returns>
        public double UpperBoundFromString(String valueStr)
        {
            if (valueStr == null || valueStr.Length == 0)
            {
                this.UpperBound = Double.PositiveInfinity;
            }
            else
            {
                // If parsing does not work, we do not change
                // current UpperBound and just return it.
                if (Double.TryParse(valueStr, out double tmp))
                {
                    // Try setting the value
                    this.UpperBound = tmp;
                }
            }

            // Return current value of UpperBound
            return this.UpperBound;
        }

        /// <summary>
        /// Returns a String representation of the upper bound.
        ///
        /// If the upper bound is not set, returns "".
        /// </summary>
        /// <returns>A string representation of the upper bound, or "" if not set.</returns>
        public String UpperBoundToString()
        {
            if (this.IsUpperBoundSet())
            {
                return this.mUpperBound.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                return "";
            }
        }
    }
}