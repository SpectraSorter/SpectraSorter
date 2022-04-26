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
using System.Collections.Generic;
using spectra.utils;

namespace SpectraSorterUnitTests
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void TestClosestValueInArray()
        {
            double[] array1 = { 100.0f, 200.0f, 300.0f, 400.0f, 500.0f };

            int index1 = Utils.FindIndexOfClosestValueInSortedArray(array1, 210.0f);
            Assert.AreEqual(index1, 1);

            int index2 = Utils.FindIndexOfClosestValueInSortedArray(array1, 420.0f);
            Assert.AreEqual(index2, 3);

            int index3 = Utils.FindIndexOfClosestValueInSortedArray(array1, -100.0f);
            Assert.AreEqual(index3, 0);

            int index4 = Utils.FindIndexOfClosestValueInSortedArray(array1, 600.0f);
            Assert.AreEqual(index4, 4);

        }

        [TestMethod]
        public void TestMeanAndStdOfList()
        {
            List<Int64> list1 = new List<Int64> { 100, 200, 300, 400, 500 };

            double mean1 = Utils.Mean(list1);
            Assert.AreEqual(mean1, 300.0);

            double std1 = Utils.Std(list1);
            Assert.AreEqual(std1, 158.1139, 0.001);
        }

        [TestMethod]
        public void TestMedian()
        {
            double[] arr1= new double[] { 0.0, 1.0, 2.0, 3.0, 4.0, 5.0 };
            double median1 = Utils.Median(arr1);
            Assert.AreEqual(2.5, median1, delta:0.001);

            double[] arr2 = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
            double median2 = Utils.Median(arr2);
            Assert.AreEqual(3.5, median2, 0.001);

            double[] arr3 = new double[] { 0.0, 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
            double median3 = Utils.Median(arr3);
            Assert.AreEqual(3.0, median3, delta: 0.001);

            double[] arr4 = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0 };
            double median4 = Utils.Median(arr4);
            Assert.AreEqual(4.0, median4, 0.001);
        }

    }
}
