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

namespace SpectraSorterUnitTests
{
    [TestClass]
    public class PlayFieldTests
    {
        [TestMethod]
        public void TestCopyArraysDifferentTypes()
        {
            // Test copying arrays of different type

            // Int to float
            int[] source1 = {1, 2, 3, 4, 5};
            float[] target1 = new float[source1.Length];

            Array.Copy(source1, target1, source1.Length);

            for (int i = 0; i < source1.Length; i++)
            {
                Assert.AreEqual(source1[i], (int)target1[i]);
            }

            // Float to int
            float[] source2 = { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f };
            int[] target2 = new int[source2.Length];

            target2 = Array.ConvertAll(source2, x => (int)x);

            for (int i = 0; i < source2.Length; i++)
            {
                Assert.AreEqual(source2[i], (float)target2[i]);
            }

        }
    }
}
