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
using spectra.utils;

namespace SpectraSorterUnitTests
{
    /// <summary>
    /// Tests for the CircularBuffer class.
    /// </summary>
    /// 
    /// See http://geekswithblogs.net/blackrob/archive/2014/09/01/circular-buffer-in-c.aspx.
    [TestClass]
    public class CircularBufferTests
    {
        [TestMethod]
        public void TestOverwrite()
        {
            var buffer = new CircularBuffer<long>(3);
            Assert.AreEqual(default(long), buffer.Enqueue(1));
            Assert.AreEqual(default(long), buffer.Enqueue(2));
            Assert.AreEqual(default(long), buffer.Enqueue(3));
            Assert.AreEqual(1, buffer.Enqueue(4));
            Assert.AreEqual(3, buffer.Count);
            Assert.AreEqual(2, buffer.Dequeue());
            Assert.AreEqual(3, buffer.Dequeue());
            Assert.AreEqual(4, buffer.Dequeue());
            Assert.AreEqual(0, buffer.Count);
        }

        [TestMethod]
        public void TestUnderwrite()
        {
            var buffer = new CircularBuffer<long>(5);
            Assert.AreEqual(default(long), buffer.Enqueue(1));
            Assert.AreEqual(default(long), buffer.Enqueue(2));
            Assert.AreEqual(default(long), buffer.Enqueue(3));
            Assert.AreEqual(3, buffer.Count);
            Assert.AreEqual(1, buffer.Dequeue());
            Assert.AreEqual(2, buffer.Dequeue());
            Assert.AreEqual(3, buffer.Dequeue());
            Assert.AreEqual(0, buffer.Count);
        }

        [TestMethod]
        public void TestIncreaseCapacityWhenFull()
        {
            var buffer = new CircularBuffer<long>(3);
            Assert.AreEqual(default(long), buffer.Enqueue(1));
            Assert.AreEqual(default(long), buffer.Enqueue(2));
            Assert.AreEqual(default(long), buffer.Enqueue(3));
            Assert.AreEqual(3, buffer.Count);
            buffer.Capacity = 4;
            Assert.AreEqual(3, buffer.Count);
            Assert.AreEqual(1, buffer.Dequeue());
            Assert.AreEqual(2, buffer.Dequeue());
            Assert.AreEqual(3, buffer.Dequeue());
            Assert.AreEqual(0, buffer.Count);
        }

        [TestMethod]
        public void TestDecreaseCapacityWhenFull()
        {
            var buffer = new CircularBuffer<long>(3);
            Assert.AreEqual(default(long), buffer.Enqueue(1));
            Assert.AreEqual(default(long), buffer.Enqueue(2));
            Assert.AreEqual(default(long), buffer.Enqueue(3));
            Assert.AreEqual(3, buffer.Count);
            buffer.Capacity = 2;
            Assert.AreEqual(2, buffer.Count);
            Assert.AreEqual(1, buffer.Dequeue());
            Assert.AreEqual(2, buffer.Dequeue());
            Assert.AreEqual(0, buffer.Count);
        }

        [TestMethod]
        public void TestEnumerationWhenFull()
        {
            var buffer = new CircularBuffer<long>(3);
            Assert.AreEqual(default(long), buffer.Enqueue(1));
            Assert.AreEqual(default(long), buffer.Enqueue(2));
            Assert.AreEqual(default(long), buffer.Enqueue(3));
            var i = 0;
            foreach (var value in buffer)
                Assert.AreEqual(++i, value);
            Assert.AreEqual(i, 3);
        }

        [TestMethod]
        public void TestEnumerationWhenPartiallyFull()
        {
            var buffer = new CircularBuffer<long>(3);
            Assert.AreEqual(default(long), buffer.Enqueue(1));
            Assert.AreEqual(default(long), buffer.Enqueue(2));
            var i = 0;
            foreach (var value in buffer)
                Assert.AreEqual(++i, value);
            Assert.AreEqual(i, 2);
        }

        [TestMethod]
        public void TestEnumerationWhenEmpty()
        {
            var buffer = new CircularBuffer<long>(3);
            foreach (var value in buffer)
                Assert.Fail("Unexpected Value: " + value);
        }

        [TestMethod]
        public void TestRemoveAt()
        {
            var buffer = new CircularBuffer<long>(5);
            Assert.AreEqual(default(long), buffer.Enqueue(1));
            Assert.AreEqual(default(long), buffer.Enqueue(2));
            Assert.AreEqual(default(long), buffer.Enqueue(3));
            Assert.AreEqual(default(long), buffer.Enqueue(4));
            Assert.AreEqual(default(long), buffer.Enqueue(5));
            buffer.RemoveAt(buffer.IndexOf(2));
            buffer.RemoveAt(buffer.IndexOf(4));
            Assert.AreEqual(3, buffer.Count);
            Assert.AreEqual(1, buffer.Dequeue());
            Assert.AreEqual(3, buffer.Dequeue());
            Assert.AreEqual(5, buffer.Dequeue());
            Assert.AreEqual(0, buffer.Count);
            Assert.AreEqual(default(long), buffer.Enqueue(1));
            Assert.AreEqual(default(long), buffer.Enqueue(2));
            Assert.AreEqual(default(long), buffer.Enqueue(3));
            Assert.AreEqual(default(long), buffer.Enqueue(4));
            Assert.AreEqual(default(long), buffer.Enqueue(5));
            buffer.RemoveAt(buffer.IndexOf(1));
            buffer.RemoveAt(buffer.IndexOf(3));
            buffer.RemoveAt(buffer.IndexOf(5));
            Assert.AreEqual(2, buffer.Count);
            Assert.AreEqual(2, buffer.Dequeue());
            Assert.AreEqual(4, buffer.Dequeue());
            Assert.AreEqual(0, buffer.Count);
        }

        [TestMethod]
        public void TestCircularArray()
        {
            var buffer = new CircularBuffer<long>(5);
            buffer.Enqueue(1);
            buffer.Enqueue(2);
            buffer.Enqueue(3);
            buffer.Enqueue(4);
            buffer.Enqueue(5);
            buffer.Enqueue(6);
            buffer.Enqueue(7);
            int v = 3;
            foreach (long value in buffer)
            {
                Assert.AreEqual(v++, value);
            }

            v = 3;
            long[] arr = buffer.toArray();
            for (int i = 0; i < arr.Length; i++)
            {
                Assert.AreEqual(v++, arr[i]);
            }
        }
    }
}
