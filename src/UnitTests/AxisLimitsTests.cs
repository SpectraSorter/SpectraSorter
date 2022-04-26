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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using spectra;
using spectra.plotting;

namespace SpectraSorterUnitTests
{
    [TestClass]
    public class AxisLimitsTests
    {
        AxisLimits axisLimits;

        [TestInitialize]
        public void TestInitialize()
        {
            axisLimits = new AxisLimits();
        }

        [TestMethod]
        public void TestAxisLimitsAreSet()
        {           
            // Check if bounds are set
            Assert.IsFalse(axisLimits.IsLowerBoundSet());
            Assert.IsFalse(axisLimits.IsUpperBoundSet());
        }

        [TestMethod]
        public void TestAxisLimitsSetValidMinAndMaxAllowedBounds()
        {
            // Reset min and max allowed bounds
            axisLimits.MaxAllowedValueForLowerBound = 500;
            axisLimits.MinAllowedValueForUpperBound = 100;

            // This works as epxected
            Assert.AreEqual(axisLimits.MaxAllowedValueForLowerBound, 500);
            Assert.AreEqual(axisLimits.MinAllowedValueForUpperBound, 100);

        }

        [TestMethod]
        public void TestAxisLimitsSetInvalidMinAndMaxAllowedBounds()
        {
            // Reset min and max allowed bounds
            axisLimits.ResetMaxAllowedValueForLowerBound();
            axisLimits.ResetMinAllowedValueForUpperBound();

            // Set min and max allowed bounds
            axisLimits.MaxAllowedValueForLowerBound = 100;
            axisLimits.MinAllowedValueForUpperBound = 500;

            // Min allowed value for upper bound is reset to 100
            Assert.AreEqual(axisLimits.MaxAllowedValueForLowerBound, 100);
            Assert.AreEqual(axisLimits.MinAllowedValueForUpperBound, 100);
        }

        [TestMethod]
        public void TestAxisLimitsSetValidMin()
        {
            // Set lower bound to 200
            axisLimits.LowerBound = 200;
            Assert.AreEqual(axisLimits.LowerBound, 200);

        }

        [TestMethod]
        public void TestAxisLimitsSetLowerBoundSmallerThanAllowed()
        {
            // Max allowed value for lower bound is 200
            axisLimits.MaxAllowedValueForLowerBound = 200;

            // Try to set lower bound to 100
            axisLimits.LowerBound = 100;

            // This works
            Assert.AreEqual(axisLimits.LowerBound, 100);

            // Try to set lower bound to 300
            axisLimits.LowerBound = 300;

            // The lower bound is reset to 200
            Assert.AreEqual(axisLimits.LowerBound, 200);

        }

        [TestMethod]
        public void TestAxisLimitsSetUpperBoundLargerThanAllowed()
        {
            // Min allowed value for upper bound is 200
            axisLimits.MinAllowedValueForUpperBound = 200;

            // Try to set upper bound to 300
            axisLimits.UpperBound = 300;

            // This works
            Assert.AreEqual(axisLimits.UpperBound, 300);

            // Try to set lower bound to 100
            axisLimits.UpperBound = 100;

            // The upper bound is reset to 200
            Assert.AreEqual(axisLimits.UpperBound, 200);

        }

        [TestMethod]
        public void TestAxisLimitsSetUpperBoundSmallerThanLowerBound()
        {
            // Allow for some space
            axisLimits.MaxAllowedValueForLowerBound = 1000;
            axisLimits.MinAllowedValueForUpperBound = 0;

            // Set upper bound to 300
            axisLimits.UpperBound = 300;

            // Try setting lower bound to 400
            axisLimits.LowerBound = 400;

            // The upper bound is 300
            Assert.AreEqual(axisLimits.UpperBound, 300);

            // The lower bound is reset to 300
            Assert.AreEqual(axisLimits.LowerBound, 299);
        }

        [TestMethod]
        public void TestAxisLimitsSetUpperBoundSmallerThanLowerBoundReverseOrder()
        {
            // Allow for some space
            axisLimits.MaxAllowedValueForLowerBound = 1000;
            axisLimits.MinAllowedValueForUpperBound = 0;

            // Try setting lower bound to 400
            axisLimits.LowerBound = 400;

            // Set upper bound to 300
            axisLimits.UpperBound = 300;

            // The lower bound is reset to 400
            Assert.AreEqual(axisLimits.LowerBound, 400);

            // The upper bound is 400
            Assert.AreEqual(axisLimits.UpperBound, 401);
        }

        [TestMethod]
        public void TestAxisLimitsParseLowerBoundFromString()
        {
            // Set lower bound from ""
            axisLimits.LowerBoundFromString("");

            // The lower bound is not set
            Assert.IsFalse(axisLimits.IsLowerBoundSet());

            // Set lower bound from "100"
            axisLimits.LowerBoundFromString("100");

            // The lower bound is 100
            Assert.AreEqual(axisLimits.LowerBound, 100);
        }

        [TestMethod]
        public void TestAxisLimitsParseUpperBoundFromString()
        {
            // Set upper bound from ""
            axisLimits.UpperBoundFromString("");

            // The upper bound is not set
            Assert.IsFalse(axisLimits.IsUpperBoundSet());

            // Set upper bound from "500"
            axisLimits.UpperBoundFromString("500");

            // The upper bound is 500
            Assert.AreEqual(axisLimits.UpperBound, 500);
        }


        [TestMethod]
        public void TestAxisLimitsSetBoundsToAndFromString()
        {
            // Reset min and max allowed values
            axisLimits.ResetMaxAllowedValueForLowerBound();
            axisLimits.ResetMinAllowedValueForUpperBound();

            Assert.AreEqual(axisLimits.LowerBoundToString(), "");
            Assert.AreEqual(axisLimits.UpperBoundToString(), "");

            // Set LowerBound to 200
            axisLimits.LowerBound = 200;
            Assert.AreEqual(axisLimits.LowerBoundToString(), "200");

            // Set LowerBound to ""
            axisLimits.LowerBoundFromString("");
            Assert.AreEqual(axisLimits.LowerBoundToString(), "");

            // Set UpperBound to 500
            axisLimits.UpperBound = 500;
            Assert.AreEqual(axisLimits.UpperBoundToString(), "500");

            // Set UpperBound to ""
            axisLimits.UpperBoundFromString("");
            Assert.AreEqual(axisLimits.UpperBoundToString(), "");
        }

        [TestMethod]
        public void TestAxisLimitsSetMinAndMaxAllowedValuesFourBoundsFromFloatArray()
        {
            // Reset min and max allowed values
            axisLimits.ResetMaxAllowedValueForLowerBound();
            axisLimits.ResetMinAllowedValueForUpperBound();

            // Float array
            float[] floatArray = new float[] { 200.0F, 300.0F, 400.0F, 500.0F, 400.0F, 300.0F, 200.0F, 100.0F };

            // Set the max allowed value for lower bound
            axisLimits.SetMinMaxAllowedValuesForBoundsFromArray(floatArray);
            Assert.AreEqual(axisLimits.MaxAllowedValueForLowerBound, 500);
            Assert.IsFalse(axisLimits.IsLowerBoundSet());
            Assert.AreEqual(axisLimits.MinAllowedValueForUpperBound, 100);
            Assert.IsFalse(axisLimits.IsUpperBoundSet());

        }

        [TestMethod]
        public void TestAxisLimitsSetMinAndMaxAllowedValuesFourBoundsFromUShortArray()
        {
            // Reset min and max allowed values
            axisLimits.ResetMaxAllowedValueForLowerBound();
            axisLimits.ResetMinAllowedValueForUpperBound();

            // Second array
            ushort[] ushortArray = new ushort[] { 200, 300, 400, 500, 400, 300, 200, 100 };

            // Set the max allowed value for lower bound
            axisLimits.SetMinMaxAllowedValuesForBoundsFromArray(ushortArray);
            Assert.AreEqual(axisLimits.MaxAllowedValueForLowerBound, 500);
            Assert.IsFalse(axisLimits.IsLowerBoundSet());
            Assert.AreEqual(axisLimits.MinAllowedValueForUpperBound, 100);
            Assert.IsFalse(axisLimits.IsUpperBoundSet());
        }

        [TestMethod]
        public void TestAxisLimitsSetIncrementalUpperBound()
        {
            // Mimic typing values in the text field

            // Reset min and max allowed values for upper and lower bounds
            axisLimits.ResetMaxAllowedValueForLowerBound();
            axisLimits.ResetMinAllowedValueForUpperBound();

            // Set lower bound
            axisLimits.LowerBound = 500;

            // Mimic typing upper bound value "1000" one character at a time

            axisLimits.UpperBoundFromString("1");
            Assert.AreEqual(axisLimits.UpperBound, 501);

            axisLimits.UpperBoundFromString("10");
            Assert.AreEqual(axisLimits.UpperBound, 501);

            axisLimits.UpperBoundFromString("100");
            Assert.AreEqual(axisLimits.UpperBound, 501);

            axisLimits.UpperBoundFromString("1000");
            Assert.AreEqual(axisLimits.UpperBound, 1000);
        }

        [TestMethod]
        public void TestAxisLimitsUpperBoundSmallerThanMinValueInData()
        {
            // Reset min and max allowed values
            axisLimits.ResetMaxAllowedValueForLowerBound();
            axisLimits.ResetMinAllowedValueForUpperBound();

            // Float array
            float[] floatArray = new float[] { 200.0F, 300.0F, 400.0F, 500.0F, 400.0F, 300.0F, 200.0F, 100.0F };

            // Set the min allowed value for upper bound
            axisLimits.SetMinMaxAllowedValuesForBoundsFromArray(floatArray);

            // Set upper bound larger than max allowed value
            axisLimits.UpperBound = 50;

            // We expect UpperBound to be reset to 100
            Assert.AreEqual(axisLimits.UpperBound, 100);
        }


        [TestMethod]
        public void TestAxisLimitsLowerBoundLargerThanMaxValueInData()
        {
            // Reset min and max allowed values
            axisLimits.ResetMaxAllowedValueForLowerBound();
            axisLimits.ResetMinAllowedValueForUpperBound();

            // Float array
            float[] floatArray = new float[] { 200.0F, 300.0F, 400.0F, 500.0F, 400.0F, 300.0F, 200.0F, 100.0F };

            // Set the max allowed value for lower bound
            axisLimits.SetMinMaxAllowedValuesForBoundsFromArray(floatArray);

            // Set min larger than max allowed value
            axisLimits.LowerBound = 550;

            // We expect LowerBound to be reset to 500
            Assert.AreEqual(axisLimits.LowerBound, 500);
        }

        [TestMethod]
        public void TestAxisLimitsSetMinAndMaxAllowedValuesFourBoundsFromFloatArrayWithRange()
        {
            // Reset min and max allowed values
            axisLimits.ResetMaxAllowedValueForLowerBound();
            axisLimits.ResetMinAllowedValueForUpperBound();

            // Float array
            float[] floatArray = new float[] { 200.0F, 300.0F, 400.0F, 500.0F, 400.0F, 300.0F, 200.0F, 100.0F };

            // Set the bounds from the range
            axisLimits.SetMinMaxAllowedValuesForBoundsFromArrayAndRange(floatArray, 4, 6);
            Assert.AreEqual(axisLimits.MaxAllowedValueForLowerBound, 400.0);
            Assert.IsFalse(axisLimits.IsLowerBoundSet());
            Assert.AreEqual(axisLimits.MinAllowedValueForUpperBound, 200.0);
            Assert.IsFalse(axisLimits.IsUpperBoundSet());
        }

        [TestMethod]
        public void TestAxisLimitsSetMinAndMaxAllowedValuesFourBoundsFromUShortArrayWithRange()
        {
            // Reset min and max allowed values
            axisLimits.ResetMaxAllowedValueForLowerBound();
            axisLimits.ResetMinAllowedValueForUpperBound();

            // Float array
            ushort[] ushortArray = new ushort[] { 200, 300, 400, 500, 400, 300, 200, 100 };

            // Set the bounds
            axisLimits.SetMinMaxAllowedValuesForBoundsFromArrayAndRange(ushortArray, 4, 6);
            Assert.AreEqual(axisLimits.MaxAllowedValueForLowerBound, 400);
            Assert.IsFalse(axisLimits.IsLowerBoundSet());
            Assert.AreEqual(axisLimits.MinAllowedValueForUpperBound, 200);
            Assert.IsFalse(axisLimits.IsUpperBoundSet());
        }

        [TestMethod]
        public void TestConflictingAxisLimits()
        {
            // Reset min and max allowed values
            axisLimits.ResetMaxAllowedValueForLowerBound();
            axisLimits.ResetMinAllowedValueForUpperBound();

            // Float array
            ushort[] ushortArray = new ushort[] { 0, 0, 0, 0, 0, 0, 0};

            // Set the bounds from the range
            axisLimits.SetMinMaxAllowedValuesForBoundsFromArrayAndRange(ushortArray, 0, 6);
            Assert.AreEqual(axisLimits.MaxAllowedValueForLowerBound, 1.0);
            Assert.IsFalse(axisLimits.IsLowerBoundSet());
            Assert.AreEqual(axisLimits.MinAllowedValueForUpperBound, 0.0);
            Assert.IsFalse(axisLimits.IsUpperBoundSet());

            // Reset min and max allowed values
            axisLimits.ResetMaxAllowedValueForLowerBound();
            axisLimits.ResetMinAllowedValueForUpperBound();

            // Float array
            float[] floatArray = new float[] { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

            // Set the bounds from the range
            axisLimits.SetMinMaxAllowedValuesForBoundsFromArrayAndRange(floatArray, 0, 6);
            Assert.AreEqual(axisLimits.MaxAllowedValueForLowerBound, 1.0);
            Assert.IsFalse(axisLimits.IsLowerBoundSet());
            Assert.AreEqual(axisLimits.MinAllowedValueForUpperBound, 0.0);
            Assert.IsFalse(axisLimits.IsUpperBoundSet());
        }
    }
}
