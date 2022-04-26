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
using spectra.processing;
using spectra.ui;
using spectra.ui.components;
using spectra.utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SpectraSorterUnitTests
{
    [TestClass]
    public class PlotterTests
    {
        MainPlotter mMainPlotter = new MainPlotter(new MainChart());

        [TestInitialize]
        public void TestInitialize()
        {
            this.mMainPlotter.Reset();
        }

        [TestMethod]
        public void TestXAxisSetBoundsFromData()
        {
            float[] XData = { 5.0f, 6.0f, 7.0f, 8.0f, 9.0f, 10.0f, 11.0f, 12.0f };

            this.mMainPlotter.XAxisSetBoundsFromData(XData);

            Assert.AreEqual(this.mMainPlotter.XAxisLowerBound, 5.0f);
            Assert.AreEqual(this.mMainPlotter.XAxisUpperBound, 12.0f);
        }

        [TestMethod]
        public void TestYAxisInitFromData()
        {
            float[] YData = { 5.0f, 6.0f, 7.0f, 8.0f, 9.0f, 10.0f, 11.0f, 12.0f };
            CircularBuffer<float[]> CircYData = new CircularBuffer<float[]>(1);
            CircYData.Enqueue(YData);

            this.mMainPlotter.YAxisInitFromData(CircYData);

            Assert.AreEqual(this.mMainPlotter.YAxisLowerBound, 5.0f);
            Assert.AreEqual(this.mMainPlotter.YAxisUpperBound, 12.0f);
        }

        [TestMethod]
        public void TestYAxisSetBoundsAsAllowedFromDataFloat()
        {
            float[] YData1 = { 5.0f, 6.0f, 7.0f, 8.0f, 9.0f, 10.0f, 11.0f, 12.0f };
            float[] YData2 = { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f };
            float[] YData3 = { 9.0f, 10.0f, 11.0f, 12.0f, 13.0f, 14.0f, 15.0f, 16.0f };

            Dictionary<string, CircularBuffer<float>> MapCircYData = new Dictionary<string, CircularBuffer<float>>();

            CircularBuffer<float> CircYData1 = new CircularBuffer<float>(YData1.Length);
            foreach (float value in YData1)
            {
                CircYData1.Enqueue(value);
            }

            CircularBuffer<float> CircYData2 = new CircularBuffer<float>(YData2.Length);
            foreach (float value in YData2)
            {
                CircYData2.Enqueue(value);
            }

            CircularBuffer<float> CircYData3 = new CircularBuffer<float>(YData3.Length);
            foreach (float value in YData3)
            {
                CircYData3.Enqueue(value);
            }

            MapCircYData["ID1"] = CircYData1;
            MapCircYData["ID2"] = CircYData2;
            MapCircYData["ID3"] = CircYData3;

            this.mMainPlotter.YAxisSetBoundsAsAllowedFromData(MapCircYData);

            Assert.AreEqual(this.mMainPlotter.YAxisLowerBound, 1.0f);
            Assert.AreEqual(this.mMainPlotter.YAxisUpperBound, 16.0f);
        }

        [TestMethod]
        public void TestYAxisSetBoundsAsAllowedFromDataInt()
        {
            float[] YData1 = { 5, 6, 7, 8, 9, 10, 11, 12 };
            float[] YData2 = { 1, 2, 3, 4, 5, 6, 7, 8 };
            float[] YData3 = { 9, 10, 11, 12, 13, 14, 15, 16 };

            Dictionary<string, CircularBuffer<int>> MapCircYData = new Dictionary<string, CircularBuffer<int>>();

            CircularBuffer<int> CircYData1 = new CircularBuffer<int>(YData1.Length);
            foreach (int value in YData1)
            {
                CircYData1.Enqueue(value);
            }

            CircularBuffer<int> CircYData2 = new CircularBuffer<int>(YData2.Length);
            foreach (int value in YData2)
            {
                CircYData2.Enqueue(value);
            }

            CircularBuffer<int> CircYData3 = new CircularBuffer<int>(YData3.Length);
            foreach (int value in YData3)
            {
                CircYData3.Enqueue(value);
            }

            MapCircYData["ID1"] = CircYData1;
            MapCircYData["ID2"] = CircYData2;
            MapCircYData["ID3"] = CircYData3;

            this.mMainPlotter.YAxisSetBoundsAsAllowedFromData(MapCircYData);

            Assert.AreEqual(this.mMainPlotter.YAxisLowerBound, 1);
            Assert.AreEqual(this.mMainPlotter.YAxisUpperBound, 16);
        }

        [TestMethod]
        public void TestYAxisSetBoundsConstrainedByCurrentXAxisRange()
        {
            float[] XData = { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f, 9.0f, 10.0f };
            float[] YData = { 11.0f, 12.0f, 13.0f, 14.0f, 15.0f, 16.0f, 17.0f, 18.0f, 19.0f, 20.0f };

            this.mMainPlotter.XAxisLowerBound = 3.0f;
            this.mMainPlotter.XAxisUpperBound = 8.0f;

            this.mMainPlotter.YAxisSetBoundsConstrainedByCurrentXAxisRange(XData, YData);

            Assert.AreEqual(this.mMainPlotter.YAxisLowerBound, 13.0f);
            Assert.AreEqual(this.mMainPlotter.YAxisUpperBound, 18.0f);
        }
    }
}