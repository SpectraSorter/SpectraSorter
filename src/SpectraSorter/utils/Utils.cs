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
using System.Globalization;
using System.Linq;

namespace spectra.utils
{
    public static class Utils
    {
        /// <summary>
        /// Convert a comma-separated-value string to a list of floats.
        /// </summary>
        /// <param name="str">String to be converted</param>
        /// <param name="mustBePositive">If true, no negative values are allowed (and are discarded).</param>
        /// <param name="canBeEmpty">If true, the string can be empty.</param>
        /// <returns>Tuple with the array of floats and the string (reconstructed if some values were corrected or omitted).</returns>

        /// <summary>
        /// Validate and convert a comma-separated-value string to a list of floats.
        /// </summary>
        /// <param name="str">String to be converted.</param>
        /// <param name="newValues">Output parameter: array of floats.</param>
        /// <param name="mustBePositive">If true, no negative values are allowed (and are discarded).</param>
        /// <param name="canBeEmpty">If true, the string can be empty.</param>
        /// <returns>True if the conversion was successful, false otherwise.</returns>        
        public static bool TryStringToFloatList(string str, out List<float> newValues, bool mustBePositive = true, bool canBeEmpty = false)
        {
            // Initialize output 
            newValues = new List<float>(capacity: 10) { };

            // Is there anything in the string?
            if (str.Replace(" ", "").Length == 0 && canBeEmpty == true)
            {
                return true;
            }

            // Split by comma
            string[] values = str.Split(',');

            foreach (String val in values)
            {
                // Firt remove blanks
                string value = val.Replace(" ", "");

                if (value.Length == 0)
                {
                    continue;
                }

                if (float.TryParse(value, out float current))
                {
                    if (mustBePositive && current < 0.0)
                    {
                        newValues.Clear();
                        return false;
                    }
                }
                else
                {
                    newValues.Clear();
                    return false;
                }

                // Add value
                newValues.Add(current);
            }

            if (newValues.Count == 0)
            {
                if (canBeEmpty == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Convert a list of floats to a comma-separated-value string.
        /// </summary>
        /// <param name="floatList">List of floats.</param>
        /// <returns></returns>
        public static String FloatListToString(List<float> floatList)
        {
            if (floatList == null)
            {
                return "";
            }
            return String.Join(", ", floatList);
        }

        /// <summary>
        /// Convert a list of Booleans to a comma-separated-value string.
        /// </summary>
        /// <param name="floatList">List of floats.</param>
        /// <returns></returns>
        public static String BooleanListToString(List<bool> booleanList)
        {
            if (booleanList == null)
            {
                return "";
            }
            return String.Join(", ", booleanList);
        }

        /// <summary>
        /// Convert a list of ints to a comma-separated-value string.
        /// </summary>
        /// <param name="intList">List of integers.</param>
        /// <returns></returns>
        public static String IntListToString(List<int> intList)
        {
            if (intList == null)
            {
                return "";
            }
            return String.Join(", ", intList);
        }

        /// <summary>
        /// Convert number of bytes to human-readable form.
        /// </summary>
        /// <param name="numBytes">Number of bytes.</param>
        /// <returns></returns>
        public static string ToByteString(double numBytes)
        {
            string byteStr = " bytes";
            if (numBytes >= 1024)
            {
                numBytes /= 1024;
                byteStr = " KB";
            }

            if (numBytes >= 1024)
            {
                numBytes /= 1024;
                byteStr = " MB";
            }

            if (numBytes >= 1024)
            {
                numBytes /= 1024;
                byteStr = " GB";
            }

            if (numBytes >= 1024)
            {
                numBytes /= 1024;
                byteStr = " PB";
            }

            if (numBytes >= 1024)
            {
                numBytes /= 1024;
                byteStr = " EB";
            }

            return numBytes.ToString("0.0", CultureInfo.InvariantCulture) + byteStr;
        }

        /// <summary>
        /// Return an age string from a TimeSpan
        /// </summary>
        /// <param name="ts">TimeSpan object.</param>
        /// <returns></returns>
        public static string GetAgeString(TimeSpan ts)
        {
            string ageStr = ts.ToString(@"hh\:mm\:ss", CultureInfo.InvariantCulture);

            if (ts.Days == 1)
            {
                ageStr = ts.ToString("%d", CultureInfo.InvariantCulture) + " day " + ageStr;
            }
            else if (ts.Days >= 1)
            {
                ageStr = ts.ToString("%d", CultureInfo.InvariantCulture) + " days " + ageStr;
            }

            return ageStr;

        }

        /// <summary>
        /// Find the index of the closest value in a sorted array
        /// </summary>
        /// 
        /// Notice: this method only works if the array is sorted but, for performance
        /// reasons, it does not test whether the array is sorted, nor it sorts it!
        /// 
        /// <param name="array">Sorted array of double values.</param>
        /// <param name="value">Value to find in the array.</param>
        /// <returns>Index of the closest value in the array.</returns>
        public static int FindIndexOfClosestValueInSortedArray(double[] array, double value)
        {
            // Find the index
            return FindIndexOfClosestValueInSortedList(new List<double>(array), value);
        }

        /// <summary>
        /// Find the index of the closest value in a sorted list
        /// </summary>
        /// 
        /// Notice: this method only works if the list is sorted but, for performance
        /// reasons, it does not test whether the list is sorted, nor it sorts it!
        /// 
        /// <param name="array">Sorted list of double values.</param>
        /// <param name="value">Value to find in the list.</param>
        /// <returns>Index of the closest value in the list.</returns>
        public static int FindIndexOfClosestValueInSortedList(List<double> list, double value)
        {
            if (value < list[0])
            {
                value = list[0];
            }

            if (value > list[list.Count - 1])
            {
                value = list[list.Count - 1];
            }

            // Use LINQ to find the element
            double closest = list.Aggregate((x, y) => Math.Abs(x - value) < Math.Abs(y - value) ? x : y);
            int index = list.FindIndex(x => x == closest);

            return index;
        }

        /// <summary>
        /// Find the index of the closest value in a sorted array
        /// </summary>
        /// 
        /// Notice: this method only works if the array is sorted but, for performance
        /// reasons, it does not test whether the array is sorted, nor it sorts it!
        /// 
        /// <param name="array">Sorted array of double values.</param>
        /// <param name="value">Value to find in the array.</param>
        /// <returns>Index of the closest value in the array.</returns>
        public static int FindIndexOfClosestValueInSortedArray(float[] array, float value)
        {
            // Find the index
            return FindIndexOfClosestValueInSortedList(new List<float>(array), value);
        }

        /// <summary>
        /// Find the index of the closest value in a sorted list
        /// </summary>
        /// 
        /// Notice: this method only works if the list is sorted but, for performance
        /// reasons, it does not test whether the list is sorted, nor it sorts it!
        /// 
        /// <param name="array">Sorted list of double values.</param>
        /// <param name="value">Value to find in the list.</param>
        /// <returns>Index of the closest value in the list.</returns>
        public static int FindIndexOfClosestValueInSortedList(List<float> list, float value)
        {
            if (value < list[0])
            {
                value = list[0];
            }

            if (value > list[list.Count - 1])
            {
                value = list[list.Count - 1];
            }

            // Use LINQ to find the element
            float closest = list.Aggregate((x, y) => Math.Abs(x - value) < Math.Abs(y - value) ? x : y);
            int index = list.FindIndex(x => x == closest);

            return index;
        }

        /// <summary>
        /// // Calculates the mean of a List of Integer64 values.
        /// </summary>
        /// <param name="values">List if Integer64 values.</param>
        /// <returns>Mean of the List of Integer64 values.</returns>
        public static double Mean(List<Int64> values)
        {
            return values.Sum() / values.Count;
        }

        /// <summary>
        /// // Calculates the mean of a List of Integer64 values.
        /// </summary>
        /// <param name="values">List if Integer64 values.</param>
        /// <returns>Mean of the List of Integer64 values.</returns>
        public static float Median(float[] values)
        {
            if (values.Length == 1)
            {
                return values[0];
            }

            Array.Sort(values);

            if (values.Length % 2 == 1)
            {
                return values[values.Length / 2];
            }
            else
            {
                return 0.5f * (values[values.Length / 2 - 1] + values[values.Length / 2]);
            }
        }

        /// <summary>
        /// // Calculates the mean of a List of Integer64 values.
        /// </summary>
        /// <param name="values">List if Integer64 values.</param>
        /// <returns>Mean of the List of Integer64 values.</returns>
        public static double Median(double[] values)
        {
            if (values.Length == 1)
            {
                return values[0];
            }

            Array.Sort(values);

            if (values.Length % 2 == 1)
            {
                return values[values.Length / 2];
            }
            else
            {
                return 0.5 * (values[values.Length / 2 - 1] + values[values.Length / 2]);
            }
        }

        /// <summary>
        /// // Calculates the standard deviation of a List of Integer64 values.
        /// </summary>
        /// <param name="values">List if Integer64 values.</param>
        /// <returns>Standard deviation of the List of Integer64 values.</returns>
        public static double Std(List<Int64> values, bool as_sample = true)
        {
            // Calculate the mean
            double mean = Mean(values);

            // Get the sum of the squares of the differences
            // between the values and the mean.
            var squares_query =
                from Int64 value in values
                select (value - mean) * (value - mean);
            double sum_of_squares = squares_query.Sum();

            if (as_sample)
            {
                return Math.Sqrt(sum_of_squares / (values.Count - 1));
            }
            else
            {
                return Math.Sqrt(sum_of_squares / values.Count);
            }
        }

        /// <summary>
        /// Return an integer range array.
        /// </summary>
        /// <param name="min">Min value.</param>
        /// <param name="max">Max value.</param>
        /// <param name="step">Step.</param>
        /// <returns>Integer range array.</returns>
        public static int[] IntRange(int min, int max, int step=1)
        {
            int[] range = new int[1 + (max - min) / step];
            int i = min;
            int n = 0;
            while (i <= max)
            {
                range[n] = i;
                n++;
                i += step;
            }

            return range;
        }

        /// <summary>
        /// Return a float range array.
        /// </summary>
        /// <param name="min">Min value.</param>
        /// <param name="max">Max value.</param>
        /// <param name="step">Step.</param>
        /// <returns>Float range array.</returns>
        public static float[] FloatRange(float min, float max, float step = 1.0f)
        {
            List<float> list = new List<float>(capacity:(int)max + 1);

            float current = min;
            while (current <= max)
            {
                list.Add(current);
                current += step;
            }

            return list.ToArray();
        }

        /// <summary>
        /// Return a double range array.
        /// </summary>
        /// <param name="min">Min value.</param>
        /// <param name="max">Max value.</param>
        /// <param name="step">Step.</param>
        /// <returns>Float range array.</returns>
        public static double[] DoubleRange(double min, double max, double step = 1.0)
        {
            List<double> list = new List<double>(capacity: (int)max + 1);

            double current = min;
            while (current <= max)
            {
                list.Add(current);
                current += step;
            }

            return list.ToArray();
        }

        /// <summary>
        /// Parse Hex or Dec from string to int.
        /// </summary>
        /// <param name="numStr">Hex or Dec as string.</param>
        /// <returns>Integer</returns>
        public static int IntParseHexOrDec(String numStr)
        {
            int rtnVal = 0;
            try
            {
                rtnVal = (numStr.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase)) ?
                    int.Parse(numStr.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture) :
                    int.Parse(numStr, CultureInfo.InvariantCulture);
            }
            finally
            {
            }
            return rtnVal;
        }

        /// <summary>
        /// Parse Hex or Dec from string to uint.
        /// </summary>
        /// <param name="numStr">Hex or Dec as string.</param>
        /// <returns>Unsigned integer</returns>
        public static uint UintParseHexOrDec(String numStr)
        {
            uint rtnVal = 0;
            try
            {
                rtnVal = (numStr.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase)) ?
                    uint.Parse(numStr.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture) :
                    uint.Parse(numStr, CultureInfo.InvariantCulture);
            }
            finally
            {
            }
            return rtnVal;
        }

        /// <summary>
        /// Parse Hex or Dec from string to long.
        /// </summary>
        /// <param name="numStr">Hex or Dec as string.</param>
        /// <returns>Long</returns>
        public static long LongParseHexOrDec(String numStr)
        {
            long rtnVal = 0;
            try
            {
                rtnVal = (numStr.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase)) ?
                    long.Parse(numStr.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture) :
                    long.Parse(numStr, CultureInfo.InvariantCulture);
            }
            finally
            {
            }
            return rtnVal;
        }
        /// <summary>
        /// Parse Hex or Dec from string to short.
        /// </summary>
        /// <param name="numStr">Hex or Dec as string.</param>
        /// <returns>Short</returns>
        public static short ShortParseHexOrDec(String numStr)
        {
            short rtnVal = 0;
            try
            {
                rtnVal = (numStr.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase)) ?
                    short.Parse(numStr.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture) :
                    short.Parse(numStr, CultureInfo.InvariantCulture);
            }
            finally
            {
            }
            return rtnVal;
        }

        /// <summary>
        /// Parse Hex or Dec from string to unsigned short.
        /// </summary>
        /// <param name="numStr">Hex or Dec as string.</param>
        /// <returns>Unsigned short</returns>
        public static ushort UshortParseHexOrDec(String numStr)
        {
            ushort rtnVal = 0;
            try
            {
                rtnVal = (numStr.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase)) ?
                    ushort.Parse(numStr.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture) :
                    ushort.Parse(numStr, CultureInfo.InvariantCulture);
            }
            finally
            {
            }
            return rtnVal;
        }

        /// <summary>
        /// Parse Hex or Dec from string to byte.
        /// </summary>
        /// <param name="numStr">Hex or Dec as string.</param>
        /// <returns>Byte</returns>
        public static byte ByteParseHexOrDec(String numStr)
        {
            byte rtnVal = 0;
            try
            {
                rtnVal = (numStr.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase)) ?
                    byte.Parse(numStr.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture) :
                    byte.Parse(numStr, CultureInfo.InvariantCulture);
            }
            finally
            {
            }
            return rtnVal;
        }

        /// <summary>
        /// Parse Hex or Dec from string to double.
        /// </summary>
        /// <param name="numStr">Hex or Dec as string.</param>
        /// <returns>Double</returns>
        public static double DoubleParseHexOrDec(String numStr)
        {
            double rtnVal = 0;
            try
            {
                rtnVal = (numStr.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase)) ?
                    double.Parse(numStr.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture) :
                    double.Parse(numStr, CultureInfo.InvariantCulture);
            }
            finally
            {
            }
            return rtnVal;
        }

        /// <summary>
        /// Find the global minimum in a Dictionary of (circular) buffers.
        /// </summary>
        /// <param name="buffer">A Dictionary of (circular) int arrays.</param>
        /// <returns>Minimum value.</returns>
        public static int FindGlobalMin(Dictionary<string, CircularBuffer<int>> mapSpectra)
        {
            int min = int.MaxValue;
            foreach(KeyValuePair<string, CircularBuffer<int>> entry in mapSpectra)
            {
                var spectrum = entry.Value;
                int mn = spectrum.Min();
                if (mn < min)
                {
                    min = mn;
                }
            }

            return min;
        }

        /// <summary>
        /// Find the global minimum in a List of (circular) buffers.
        /// </summary>
        /// <param name="buffer">A List of (circular) float array.</param>
        /// <returns>Minimum value.</returns>
        public static float FindGlobalMin(Dictionary<string, CircularBuffer<float>> mapSpectra)
        {
            float min = float.MaxValue;
            foreach (KeyValuePair<string, CircularBuffer<float>> entry in mapSpectra)
            {
                var spectrum = entry.Value;
                float mn = spectrum.Min();
                if (mn < min)
                {
                    min = mn;
                }
            }

            return min;
        }

        /// <summary>
        /// Find the global maximum in a List of (circular) buffers.
        /// </summary>
        /// <param name="buffer">A List of (circular) int array.</param>
        /// <returns>Maximum value.</returns>
        public static int FindGlobalMax(Dictionary<string, CircularBuffer<int>> mapSpectra)
        {
            int max = int.MinValue;
            foreach (KeyValuePair<string, CircularBuffer<int>> entry in mapSpectra)
            {
                var spectrum = entry.Value;
                int mx = spectrum.Max();
                if (mx > max)
                {
                    max = mx;
                }
            }

            return max;
        }

        /// <summary>
        /// Find the global maximum in a List of (circular) buffers.
        /// </summary>
        /// <param name="buffer">A List of (circular) float array.</param>
        /// <returns>Maximum value.</returns>
        public static float FindGlobalMax(Dictionary<string, CircularBuffer<float>> mapSpectra)
        {
            float max = float.MinValue;
            foreach (KeyValuePair<string, CircularBuffer<float>> entry in mapSpectra)
            {
                var spectrum = entry.Value;
                float mx = spectrum.Max();
                if (mx > max)
                {
                    max = mx;
                }
            }

            return max;
        }

        /// <summary>
        /// Find the global minimum in the (circular) buffer.
        /// </summary>
        /// <param name="buffer">A (circular) float array.</param>
        /// <returns>Minimum value.</returns>
        public static float FindGlobalMin(CircularBuffer<float[]> spectra)
        {
            float min = float.MaxValue;
            foreach (var spectrum in spectra)
            {
                float mn = spectrum.Min();
                if (mn < min)
                {
                    min = mn;
                }
            }

            return min;
        }

        /// <summary>
        /// Find the global maximum in the (circular) buffer.
        /// </summary>
        /// <param name="buffer">A (circular) float array.</param>
        /// <returns>Maximum value.</returns>
        public static float FindGlobalMax(CircularBuffer<float[]> spectra)
        {
            float max = float.MinValue;
            foreach (var spectrum in spectra)
            {
                float mx = spectrum.Max();
                if (mx > max)
                {
                    max = mx;
                }
            }

            return max;
        }

        /// <summary>
        /// Copies an int array into a referenced float array.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static void ToFloatArray(int[] input, ref float[] output)
        {
            for (int p = 0; p < input.Length; p++)
            {
                output[p] = (float)input[p];
            }
        }

        /// <summary>
        /// Returns a copy of an int array into a float array.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static float[] ToFloatArray(int[] input)
        {
            float[] output = new float[input.Length];
            for (int p = 0; p < input.Length; p++)
            {
                output[p] = (float)input[p];
            }
            return output;
        }

        /// <summary>
        /// Copies a float array into a referenced int array.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static void ToIntArray(float[] input, ref int[] output)
        {
            for (int p = 0; p < input.Length; p++)
            {
                output[p] = (int)input[p];
            }
        }

        /// <summary>
        /// Returns a copy of a float array into an int array.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static int[] ToIntArray(float[] input)
        {
            int[] output = new int[input.Length];
            for (int p = 0; p < input.Length; p++)
            {
                output[p] = (int)input[p];
            }
            return output;
        }
    }
}
