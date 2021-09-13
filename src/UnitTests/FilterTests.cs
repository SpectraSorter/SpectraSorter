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

using spectra.processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SpectraSorterUnitTests
{
    [TestClass]
    public class FilterTests
    {
        private Filter filter;

        [TestInitialize]
        public void TestInitialize()
        {
            filter = new Filter();
        }

        [TestMethod]
        public void TestCreateAverageFilter()
        {
            int support = 5;

            int returnedSupport = filter.UseAverageFilter(support);

            Assert.AreEqual(support, returnedSupport);

            double[] expKernel = new double[] { 0.2000, 0.2000, 0.2000, 0.2000, 0.2000 };

            for (int i = 0; i < filter.Support; i++)
            {
                Assert.AreEqual(filter.Kernel[i], expKernel[i], 0.001);
            }
        }

        [TestMethod]
        public void TestCreateAverageFilterWithSpectrumFilterer()
        {
            int support = 5;

            int returnedSupport = SpectrumFilterer.Instance.UseAverageFilter(support);

            Assert.AreEqual(support, returnedSupport);

            double[] expKernel = new double[] { 0.2000, 0.2000, 0.2000, 0.2000, 0.2000 };

            for (int i = 0; i < SpectrumFilterer.Instance.Support; i++)
            {
                Assert.AreEqual(SpectrumFilterer.Instance.Kernel[i], expKernel[i], 0.001);
            }
        }

        [TestMethod]
        public void TestCreateAverageFilterEvenSupport()
        {
            int evenSupport = 4;

            int support = filter.UseAverageFilter(evenSupport);

            Assert.AreEqual(support, evenSupport + 1);

            double[] expKernel = new double[] { 0.2000, 0.2000, 0.2000, 0.2000, 0.2000 };

            for (int i = 0; i < filter.Support; i++)
            {
                Assert.AreEqual(filter.Kernel[i], expKernel[i], 0.001);
            }
        }

        [TestMethod]
        public void TestCreateAverageFilterEvenSupportWithSpectrumFilterer()
        {
            int evenSupport = 4;

            int support = SpectrumFilterer.Instance.UseAverageFilter(evenSupport);

            Assert.AreEqual(support, evenSupport + 1);

            double[] expKernel = new double[] { 0.2000, 0.2000, 0.2000, 0.2000, 0.2000 };

            for (int i = 0; i < SpectrumFilterer.Instance.Support; i++)
            {
                Assert.AreEqual(SpectrumFilterer.Instance.Kernel[i], expKernel[i], 0.001);
            }
        }

        [TestMethod]
        public void TestCreateGaussianFilter()
        {
            int support = 5;
            double sigma = 1.0;

            int returnedSupport = filter.UseGaussianFilter(support, sigma);

            Assert.AreEqual(support, returnedSupport);

            double[] expKernel = new double[] { 0.0545, 0.2442, 0.4026, 0.2442, 0.0545 };

            for (int i = 0; i < filter.Support; i++)
            {
                Assert.AreEqual(filter.Kernel[i], expKernel[i], 0.001);
            }
        }

        [TestMethod]
        public void TestCreateGaussianFilterWithSpectrumFilterer()
        {
            int support = 5;
            double sigma = 1.0;

            int returnedSupport = SpectrumFilterer.Instance.UseGaussianFilter(support, sigma);

            Assert.AreEqual(support, returnedSupport);

            double[] expKernel = new double[] { 0.0545, 0.2442, 0.4026, 0.2442, 0.0545 };

            for (int i = 0; i < SpectrumFilterer.Instance.Support; i++)
            {
                Assert.AreEqual(SpectrumFilterer.Instance.Kernel[i], expKernel[i], 0.001);
            }
        }

        [TestMethod]
        public void TestCreateGaussianFilterEvenSupport()
        {
            int evenSupport = 4;
            double sigma = 1.0;

            int support = filter.UseGaussianFilter(evenSupport, sigma);

            Assert.AreEqual(support, evenSupport + 1);

            double[] expKernel = new double[] { 0.0545, 0.2442, 0.4026, 0.2442, 0.0545 };

            for (int i = 0; i < filter.Support; i++)
            {
                Assert.AreEqual(filter.Kernel[i], expKernel[i], 0.001);
            }
        }

        [TestMethod]
        public void TestCreateGaussianFilterEvenSupportWithSpectrumFilterer()
        {
            int evenSupport = 4;
            double sigma = 1.0;

            int support = SpectrumFilterer.Instance.UseGaussianFilter(evenSupport, sigma);

            Assert.AreEqual(support, evenSupport + 1);

            double[] expKernel = new double[] { 0.0545, 0.2442, 0.4026, 0.2442, 0.0545 };

            for (int i = 0; i < SpectrumFilterer.Instance.Support; i++)
            {
                Assert.AreEqual(SpectrumFilterer.Instance.Kernel[i], expKernel[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterUnitKernel()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 1;
            filter.UseAverageFilter(support);

            double[] result = filter.Convolve(signal, full: true);

            double[] expResult = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterUnitKernelWithSpectrumFilterer()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 1;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            double[] result = SpectrumFilterer.Instance.Convolve(signal, full: true);

            double[] expResult = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterUnitKernelSymmetric()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 1;
            filter.UseAverageFilter(support);

            double[] result = filter.Convolve(signal, full: true);

            double[] expResult = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterUnitKernelSymmetricWithSpectrumFilterer()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 1;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            double[] result = SpectrumFilterer.Instance.Convolve(signal, full: true);

            double[] expResult = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterLargerKernelDouble()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 5;
            filter.UseAverageFilter(support);

            double[] result = filter.Convolve(signal, full: true);

            double[] expResult = new double[] { 0.2000, 0.6000, 1.2000, 2.0000, 3.0000, 3.6000, 3.8000, 3.6000, 3.0000, 2.0000, 1.2000, 0.6000, 0.2000 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterLargerKernelDoubleWithSpectrumFilterer()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 5;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            double[] result = SpectrumFilterer.Instance.Convolve(signal, full: true);

            double[] expResult = new double[] { 0.2000, 0.6000, 1.2000, 2.0000, 3.0000, 3.6000, 3.8000, 3.6000, 3.0000, 2.0000, 1.2000, 0.6000, 0.2000 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterLargerKernelDoubleSymmetric()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 5;
            filter.UseAverageFilter(support);

            double[] result = filter.Convolve(signal, full: true, symmetric: true);

            double[] expResult = new double[] { 3.0000, 2.4000, 2.2000, 2.4000, 3.0000, 3.6000, 3.8000, 3.6000, 3.0000, 2.4000, 2.2000, 2.4000, 3.0000 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }


        [TestMethod]
        public void TestFullConvolveAverageFilterLargerKernelDoubleSymmetricWithSpectrumFilterer()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 5;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            double[] result = SpectrumFilterer.Instance.Convolve(signal, full: true, symmetric: true);

            double[] expResult = new double[] { 3.0000, 2.4000, 2.2000, 2.4000, 3.0000, 3.6000, 3.8000, 3.6000, 3.0000, 2.4000, 2.2000, 2.4000, 3.0000 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveCustomFilterLargerKernelDouble()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 5;
            filter.UseCustomFilter(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 });

            double[] result = filter.Convolve(signal, full: true);

            double[] expResult = new double[] { 0.1000, 0.4000, 1.0000, 2.0000, 3.5000, 4.8000, 5.7000, 6.0000, 5.5000, 4.0000, 2.6000, 1.4000, 0.5000 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveCustomFilterLargerKernelDoubleWithSpectrumFilterer()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 5;
            SpectrumFilterer.Instance.UseCustomFilter(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 });

            double[] result = SpectrumFilterer.Instance.Convolve(signal, full: true);

            double[] expResult = new double[] { 0.1000, 0.4000, 1.0000, 2.0000, 3.5000, 4.8000, 5.7000, 6.0000, 5.5000, 4.0000, 2.6000, 1.4000, 0.5000 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveCustomFilterVeryLargeKernelDouble()
        {
            double[] signal = new double[] {
                1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0, 12.0, 13.0,
                14.0, 13.0, 12.0, 11.0, 10.0, 9.0, 8.0, 7.0, 6.0, 4.0, 3.0, 2.0, 1.0
            };

            int support = 5;
            filter.UseCustomFilter(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 });

            double[] result = filter.Convolve(signal, full: true);

            double[] expResult = new double[]
            {
                0.1000, 0.4000, 1.0000, 2.0000, 3.5000, 5.0000, 6.5000, 8.0000, 9.5000,
                11.0000, 12.5000, 14.0000, 15.5000, 17.0000, 18.3000, 19.2000, 19.5000,
                19.0000, 17.5000, 16.0000, 14.5000, 13.0000, 11.4000, 9.7000,
                7.9000000, 6.0000, 4.0000, 2.6000, 1.4000, 0.5000
            };

            Assert.AreEqual(result.Length, signal.Length + support - 1);
            Assert.AreEqual(expResult.Length, result.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveCustomFilterVeryLargeKernelDoubleWithSpectrumFilterer()
        {
            double[] signal = new double[] {
                1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0, 12.0, 13.0,
                14.0, 13.0, 12.0, 11.0, 10.0, 9.0, 8.0, 7.0, 6.0, 4.0, 3.0, 2.0, 1.0
            };

            int support = 5;
            SpectrumFilterer.Instance.UseCustomFilter(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 });

            double[] result = SpectrumFilterer.Instance.Convolve(signal, full: true);

            double[] expResult = new double[]
            {
                0.1000, 0.4000, 1.0000, 2.0000, 3.5000, 5.0000, 6.5000, 8.0000, 9.5000,
                11.0000, 12.5000, 14.0000, 15.5000, 17.0000, 18.3000, 19.2000, 19.5000,
                19.0000, 17.5000, 16.0000, 14.5000, 13.0000, 11.4000, 9.7000,
                7.9000000, 6.0000, 4.0000, 2.6000, 1.4000, 0.5000
            };

            Assert.AreEqual(result.Length, signal.Length + support - 1);
            Assert.AreEqual(expResult.Length, result.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveCustomFilterVeryLargeKernelDoubleSymmetric()
        {
            double[] signal = new double[] {
                1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0, 12.0, 13.0,
                14.0, 13.0, 12.0, 11.0, 10.0, 9.0, 8.0, 7.0, 6.0, 4.0, 3.0, 2.0, 1.0
            };

            int support = 5;
            filter.UseCustomFilter(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 });

            double[] result = filter.Convolve(signal, full: true, symmetric: true);

            double[] expResult = new double[]
            {
                5.5000, 4.2000, 3.3000, 3.0000, 3.5000, 5.0000, 6.5000, 8.0000, 9.5000,
                11.0000, 12.5000, 14.0000, 15.5000, 17.0000, 18.3000, 19.2000, 19.5000,
                19.0000, 17.5000, 16.0000, 14.5000, 13.0000, 11.4000, 9.7000, 7.9000,
                6.0000000, 4.2000, 3.3000, 3.0000, 3.6000
            };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);
            Assert.AreEqual(expResult.Length, result.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveCustomFilterVeryLargeKernelDoubleSymmetricWithSpectrumFilterer()
        {
            double[] signal = new double[] {
                1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0, 12.0, 13.0,
                14.0, 13.0, 12.0, 11.0, 10.0, 9.0, 8.0, 7.0, 6.0, 4.0, 3.0, 2.0, 1.0
            };

            int support = 5;
            SpectrumFilterer.Instance.UseCustomFilter(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 });

            double[] result = SpectrumFilterer.Instance.Convolve(signal, full: true, symmetric: true);

            double[] expResult = new double[]
            {
                5.5000, 4.2000, 3.3000, 3.0000, 3.5000, 5.0000, 6.5000, 8.0000, 9.5000,
                11.0000, 12.5000, 14.0000, 15.5000, 17.0000, 18.3000, 19.2000, 19.5000,
                19.0000, 17.5000, 16.0000, 14.5000, 13.0000, 11.4000, 9.7000, 7.9000,
                6.0000000, 4.2000, 3.3000, 3.0000, 3.6000
            };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);
            Assert.AreEqual(expResult.Length, result.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveCustomFilterLargerKernelDouble()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            filter.UseCustomFilter(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 });

            double[] result = filter.Convolve(signal, full: false);

            double[] expResult = new double[] { 1.0000, 2.0000, 3.5000, 4.8000, 5.7000, 6.0000, 5.5000, 4.0000, 2.6000 };

            Assert.AreEqual(expResult.Length, result.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveCustomFilterLargerKernelDoubleWithSpectrumFilterer()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            SpectrumFilterer.Instance.UseCustomFilter(new double[] { 0.1, 0.2, 0.3, 0.4, 0.5 });

            double[] result = SpectrumFilterer.Instance.Convolve(signal, full: false);

            double[] expResult = new double[] { 1.0000, 2.0000, 3.5000, 4.8000, 5.7000, 6.0000, 5.5000, 4.0000, 2.6000 };

            Assert.AreEqual(expResult.Length, result.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterLargerKernelFloat()
        {
            float[] signal = new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 4.0f, 3.0f, 2.0f, 1.0f };

            int support = 5;
            filter.UseAverageFilter(support);

            float[] result = filter.Convolve(signal, full: true);

            float[] expResult = new float[] { 0.2000f, 0.6000f, 1.2000f, 2.0000f, 3.0000f, 3.6000f, 3.8000f, 3.6000f, 3.0000f, 2.0000f, 1.2000f, 0.6000f, 0.2000f };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterLargerKernelInt()
        {
            int[] signal = new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            int support = 5;
            filter.UseAverageFilter(support);

            int[] result = filter.Convolve(signal, full: true);

            int[] expResult = new int[] { 0, 1, 1, 2, 3, 4, 4, 4, 3, 2, 1, 1, 0 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterLargerKernelShort()
        {
            ushort[] signal = new ushort[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            int support = 5;
            filter.UseAverageFilter(support);

            ushort[] result = filter.Convolve(signal, full: true);

            ushort[] expResult = new ushort[] { 0, 1, 1, 2, 3, 4, 4, 4, 3, 2, 1, 1, 0 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterLargerKernelFloatWithSpectrumFilterer()
        {
            float[] signal = new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 4.0f, 3.0f, 2.0f, 1.0f };

            int support = 5;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            float[] result = SpectrumFilterer.Instance.Convolve(signal, full: true);

            float[] expResult = new float[] { 0.2000f, 0.6000f, 1.2000f, 2.0000f, 3.0000f, 3.6000f, 3.8000f, 3.6000f, 3.0000f, 2.0000f, 1.2000f, 0.6000f, 0.2000f };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterLargerKernelFloatSymmetric()
        {
            float[] signal = new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 4.0f, 3.0f, 2.0f, 1.0f };

            int support = 5;
            filter.UseAverageFilter(support);

            float[] result = filter.Convolve(signal, full: true, symmetric: true);

            float[] expResult = new float[] { 3.0000f, 2.4000f, 2.2000f, 2.4000f, 3.0000f, 3.6000f, 3.8000f, 3.6000f, 3.0000f, 2.4000f, 2.2000f, 2.4000f, 3.0000f };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterLargerKernelFloatSymmetricWithSpectrumFilterer()
        {
            float[] signal = new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 4.0f, 3.0f, 2.0f, 1.0f };

            int support = 5;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            float[] result = SpectrumFilterer.Instance.Convolve(signal, full: true, symmetric: true);

            float[] expResult = new float[] { 3.0000f, 2.4000f, 2.2000f, 2.4000f, 3.0000f, 3.6000f, 3.8000f, 3.6000f, 3.0000f, 2.4000f, 2.2000f, 2.4000f, 3.0000f };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveListAverageFilterLargerKernelInt()
        {
            int[] signal = new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            int support = 5;
            filter.UseAverageFilter(support);

            int[] result = filter.Convolve(signal, full: true);

            int[] expResult = new int[] { 0, 1, 1, 2, 3, 4, 4, 4, 3, 2, 1, 1, 0 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveListAverageFilterLargerKernelShort()
        {
            ushort[] signal = new ushort[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            int support = 5;
            filter.UseAverageFilter(support);

            ushort[] result = filter.Convolve(signal, full: true);

            ushort[] expResult = new ushort[] { 0, 1, 1, 2, 3, 4, 4, 4, 3, 2, 1, 1, 0 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveListAverageFilterLargerKernelIntWithSpectrumFilterer()
        {
            int[] signal = new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            int support = 5;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            int[] result = SpectrumFilterer.Instance.Convolve(signal, full: true);

            int[] expResult = new int[] { 0, 1, 1, 2, 3, 4, 4, 4, 3, 2, 1, 1, 0 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveListAverageFilterLargerKernelShortWithSpectrumFilterer()
        {
            ushort[] signal = new ushort[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            int support = 5;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            ushort[] result = SpectrumFilterer.Instance.Convolve(signal, full: true);

            ushort[] expResult = new ushort[] { 0, 1, 1, 2, 3, 4, 4, 4, 3, 2, 1, 1, 0 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterLargerKernelIntSymmetric()
        {
            int[] signal = new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            int support = 5;
            filter.UseAverageFilter(support);

            int[] result = filter.Convolve(signal, full: true, symmetric: true);

            int[] expResult = new int[] { 3, 2, 2, 2, 3, 4, 4, 4, 3, 2, 2, 2, 3 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterLargerKernelShortSymmetric()
        {
            ushort[] signal = new ushort[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            int support = 5;
            filter.UseAverageFilter(support);

            ushort[] result = filter.Convolve(signal, full: true, symmetric: true);

            ushort[] expResult = new ushort[] { 3, 2, 2, 2, 3, 4, 4, 4, 3, 2, 2, 2, 3 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterLargerKernelIntSymmetricWithSpectrumFilterer()
        {
            int[] signal = new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            int support = 5;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            int[] result = SpectrumFilterer.Instance.Convolve(signal, full: true, symmetric: true);

            int[] expResult = new int[] { 3, 2, 2, 2, 3, 4, 4, 4, 3, 2, 2, 2, 3 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFullConvolveAverageFilterLargerKernelShortSymmetricWithSpectrumFilterer()
        {
            ushort[] signal = new ushort[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            int support = 5;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            ushort[] result = SpectrumFilterer.Instance.Convolve(signal, full: true, symmetric: true);

            ushort[] expResult = new ushort[] { 3, 2, 2, 2, 3, 4, 4, 4, 3, 2, 2, 2, 3 };

            Assert.AreEqual(expResult.Length, signal.Length + support - 1);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveAverageFilterUnitKernelDouble()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 1;
            filter.UseAverageFilter(support);

            double[] result = filter.Convolve(signal, full: false);

            double[] expResult = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveAverageFilterUnitKernelDoubleWithSpectrumFilterer()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 1;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            double[] result = SpectrumFilterer.Instance.Convolve(signal, full: false);

            double[] expResult = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveAverageFilterLargerKernelDouble()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 5;
            filter.UseAverageFilter(support);

            double[] result = filter.Convolve(signal, full: false);

            double[] expResult = new double[] { 1.2000, 2.0000, 3.0000, 3.6000, 3.8000, 3.6000, 3.0000, 2.0000, 1.2000 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveAverageFilterLargerKernelDoubleWithSpectrumFilterer()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 5;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            double[] result = SpectrumFilterer.Instance.Convolve(signal, full: false);

            double[] expResult = new double[] { 1.2000, 2.0000, 3.0000, 3.6000, 3.8000, 3.6000, 3.0000, 2.0000, 1.2000 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveAverageFilterLargerKernelFloat()
        {
            float[] signal = new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 4.0f, 3.0f, 2.0f, 1.0f };

            int support = 5;
            filter.UseAverageFilter(support);

            float[] result = filter.Convolve(signal, full: false);

            float[] expResult = new float[] { 1.2000f, 2.0000f, 3.0000f, 3.6000f, 3.8000f, 3.6000f, 3.0000f, 2.0000f, 1.2000f };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveAverageFilterLargerKernelFloatWithSpectrumFilterer()
        {
            float[] signal = new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 4.0f, 3.0f, 2.0f, 1.0f };

            int support = 5;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            float[] result = SpectrumFilterer.Instance.Convolve(signal, full: false);

            float[] expResult = new float[] { 1.2000f, 2.0000f, 3.0000f, 3.6000f, 3.8000f, 3.6000f, 3.0000f, 2.0000f, 1.2000f };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveAverageFilterLargerKernelFloatSymmetric()
        {
            float[] signal = new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 4.0f, 3.0f, 2.0f, 1.0f };

            int support = 5;
            filter.UseAverageFilter(support);

            float[] result = filter.Convolve(signal, full: false, symmetric: true);

            float[] expResult = new float[] { 2.2000f, 2.4000f, 3.0000f, 3.6000f, 3.8000f, 3.6000f, 3.0000f, 2.4000f, 2.2000f };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveAverageFilterLargerKernelFloatSymmetricWithSpectrumFilterer()
        {
            float[] signal = new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 4.0f, 3.0f, 2.0f, 1.0f };

            int support = 5;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            float[] result = SpectrumFilterer.Instance.Convolve(signal, full: false, symmetric: true);

            float[] expResult = new float[] { 2.2000f, 2.4000f, 3.0000f, 3.6000f, 3.8000f, 3.6000f, 3.0000f, 2.4000f, 2.2000f };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveAverageFilterLargerKernelDoubleSymmetric()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 5;
            filter.UseAverageFilter(support);

            double[] result = filter.Convolve(signal, full: false, symmetric: true);

            double[] expResult = new double[] { 2.2000, 2.4000, 3.0000, 3.6000, 3.8000, 3.6000, 3.0000, 2.4000, 2.2000 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveAverageFilterLargerKernelDoubleSymmetricWithSpectrumFilterer()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            int support = 5;
            SpectrumFilterer.Instance.UseAverageFilter(support);

            double[] result = SpectrumFilterer.Instance.Convolve(signal, full: false, symmetric: true);

            double[] expResult = new double[] { 2.2000, 2.4000, 3.0000, 3.6000, 3.8000, 3.6000, 3.0000, 2.4000, 2.2000 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveGaussianFilterLargerKernelDouble()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            filter.UseGaussianFilter(5, 1.0);

            double[] result = filter.Convolve(signal, full: false);

            double[] expResult = new double[] { 1.0545, 2.0000, 3.0000, 3.8910, 4.2936, 3.8910, 3.0000, 2.0000, 1.0545 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveGaussianFilterLargerKernelDoubleWithSpectrumFilterer()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            SpectrumFilterer.Instance.UseGaussianFilter(5, 1.0);

            double[] result = SpectrumFilterer.Instance.Convolve(signal, full: false);

            double[] expResult = new double[] { 1.0545, 2.0000, 3.0000, 3.8910, 4.2936, 3.8910, 3.0000, 2.0000, 1.0545 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveGaussianFilterLargerKernelDoubleSymmetric()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            filter.UseGaussianFilter(5, 1.0);

            double[] result = filter.Convolve(signal, full: false, symmetric: true);

            double[] expResult = new double[] { 1.7064, 2.1090, 3.0000, 3.8910, 4.2936, 3.8910, 3.0000, 2.1090, 1.7064 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveGaussianFilterLargerKernelDoubleSymmetricWithSpectrumFilterer()
        {
            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };

            SpectrumFilterer.Instance.UseGaussianFilter(5, 1.0);

            double[] result = SpectrumFilterer.Instance.Convolve(signal, full: false, symmetric: true);

            double[] expResult = new double[] { 1.7064, 2.1090, 3.0000, 3.8910, 4.2936, 3.8910, 3.0000, 2.1090, 1.7064 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveGaussianFilterLargerKernelFloat()
        {
            float[] signal = new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 4.0f, 3.0f, 2.0f, 1.0f };

            filter.UseGaussianFilter(5, 1.0);

            float[] result = filter.Convolve(signal, full: false);

            float[] expResult = new float[] { 1.0545f, 2.0000f, 3.0000f, 3.8910f, 4.2936f, 3.8910f, 3.0000f, 2.0000f, 1.0545f };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveGaussianFilterLargerKernelFloatWithSpectrumFilterer()
        {
            float[] signal = new float[] { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 4.0f, 3.0f, 2.0f, 1.0f };

            SpectrumFilterer.Instance.UseGaussianFilter(5, 1.0);

            float[] result = SpectrumFilterer.Instance.Convolve(signal, full: false);

            float[] expResult = new float[] { 1.0545f, 2.0000f, 3.0000f, 3.8910f, 4.2936f, 3.8910f, 3.0000f, 2.0000f, 1.0545f };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveGaussianFilterLargerKernelInt()
        {
            int[] signal = new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            filter.UseGaussianFilter(5, 1.0);

            int[] result = filter.Convolve(signal, full: false);

            int[] expResult = new int[] { 1, 2, 3, 4, 4, 4, 3, 2, 1 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveGaussianFilterLargerKernelShort()
        {
            ushort[] signal = new ushort[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            filter.UseGaussianFilter(5, 1.0);

            ushort[] result = filter.Convolve(signal, full: false);

            ushort[] expResult = new ushort[] { 1, 2, 3, 4, 4, 4, 3, 2, 1 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveGaussianFilterLargerKernelIntWithSpectrumFilterer()
        {
            int[] signal = new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            SpectrumFilterer.Instance.UseGaussianFilter(5, 1.0);

            int[] result = SpectrumFilterer.Instance.Convolve(signal, full: false);

            int[] expResult = new int[] { 1, 2, 3, 4, 4, 4, 3, 2, 1 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveGaussianFilterLargerKernelIntSymmetric()
        {
            int[] signal = new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            filter.UseGaussianFilter(5, 1.0);

            int[] result = filter.Convolve(signal, full: false, symmetric: true);

            int[] expResult = new int[] { 2, 2, 3, 4, 4, 4, 3, 2, 2 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveGaussianFilterLargerKernelIntSymmetricWithSpectrumFilterer()
        {
            int[] signal = new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            SpectrumFilterer.Instance.UseGaussianFilter(5, 1.0);

            int[] result = SpectrumFilterer.Instance.Convolve(signal, full: false, symmetric: true);

            int[] expResult = new int[] { 2, 2, 3, 4, 4, 4, 3, 2, 2 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestSameConvolveGaussianFilterLargerKernelShortSymmetricWithSpectrumFilterer()
        {
            ushort[] signal = new ushort[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };

            SpectrumFilterer.Instance.UseGaussianFilter(5, 1.0);

            ushort[] result = SpectrumFilterer.Instance.Convolve(signal, full: false, symmetric: true);

            ushort[] expResult = new ushort[] { 2, 2, 3, 4, 4, 4, 3, 2, 2 };

            Assert.AreEqual(result.Length, expResult.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(result[i], expResult[i], 0.001);
            }
        }

        [TestMethod]
        public void TestDefaultValues()
        {
            Assert.AreEqual(filter.IsAverage, true);
            Assert.AreEqual(filter.IsUnit, true);

            Assert.AreEqual(filter.Support, 1);
            Assert.AreEqual(filter.Sigma, 0.0);
            for (int i = 0; i < filter.Support; i++)
            {
                Assert.AreEqual(1.0, filter.Kernel[i], 0.001);
            }

            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };
            double[] result = filter.Convolve(signal, full: false);

            Assert.AreEqual(signal.Length, result.Length);

            for (int i = 0; i < signal.Length; i++)
            {
                Assert.AreEqual(signal[i], result[i], 0.001);
            }
        }

        [TestMethod]
        public void TestDefaultValuesWithSpectrumFilterer()
        {
            SpectrumFilterer.Instance.Reset();

            Assert.AreEqual(SpectrumFilterer.Instance.IsAverage, true);
            Assert.AreEqual(SpectrumFilterer.Instance.IsUnit, true);

            Assert.AreEqual(SpectrumFilterer.Instance.Support, 1);
            Assert.AreEqual(SpectrumFilterer.Instance.Sigma, 0.0);
            for (int i = 0; i < SpectrumFilterer.Instance.Support; i++)
            {
                Assert.AreEqual(1.0, SpectrumFilterer.Instance.Kernel[i], 0.001);
            }

            double[] signal = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0, 4.0, 3.0, 2.0, 1.0 };
            double[] result = SpectrumFilterer.Instance.Convolve(signal, full: false);

            Assert.AreEqual(signal.Length, result.Length);

            for (int i = 0; i < signal.Length; i++)
            {
                Assert.AreEqual(signal[i], result[i], 0.001);
            }
        }

        [TestMethod]
        public void TestFilterSelection()
        {
            int support = 5;
            double sigma = 1.0;

            filter.UseGaussianFilter(support, sigma);
            Assert.AreEqual(filter.IsGaussian, true);
            Assert.AreEqual(filter.Support, support);
            Assert.AreEqual(filter.Sigma, sigma);

            filter.UseAverageFilter(support);
            Assert.AreEqual(filter.IsAverage, true);
            Assert.AreEqual(filter.Support, support);
            Assert.AreEqual(filter.Sigma, 0.0);
        }

        [TestMethod]
        public void TestFilterSelectionWithSpectrumFilterer()
        {
            int support = 5;
            double sigma = 1.0;

            SpectrumFilterer.Instance.UseGaussianFilter(support, sigma);
            Assert.AreEqual(SpectrumFilterer.Instance.IsGaussian, true);
            Assert.AreEqual(SpectrumFilterer.Instance.Support, support);
            Assert.AreEqual(SpectrumFilterer.Instance.Sigma, sigma);

            SpectrumFilterer.Instance.UseAverageFilter(support);
            Assert.AreEqual(SpectrumFilterer.Instance.IsAverage, true);
            Assert.AreEqual(SpectrumFilterer.Instance.Support, support);
            Assert.AreEqual(SpectrumFilterer.Instance.Sigma, 0.0);
        }

        [TestMethod]
        public void TestSuggestedSupportFromSigma()
        {
            double sigma1 = 1.0;
            int support1 = filter.SupportFromSigma(sigma1);
            Assert.AreEqual(support1, 7);

            double sigma2 = 5.0;
            int support2 = filter.SupportFromSigma(sigma2);
            Assert.AreEqual(support2, 31);
        }

        [TestMethod]
        public void TestSuggestedSupportFromSigmaWithSpectrumFilterer()
        {
            double sigma1 = 1.0;
            int support1 = SpectrumFilterer.Instance.SupportFromSigma(sigma1);
            Assert.AreEqual(support1, 7);

            double sigma2 = 5.0;
            int support2 = SpectrumFilterer.Instance.SupportFromSigma(sigma2);
            Assert.AreEqual(support2, 31);
        }

        [TestMethod]
        public void TestCopyArrays()
        {
            double[] expResult = new double[] { 1.0545, 2.0000, 3.0000, 3.8910, 4.2936, 3.8910, 3.0000, 2.0000, 1.0545 };
            double[] copiedArray = new double[expResult.Length];

            Array.Copy(expResult, copiedArray, expResult.Length);

            Assert.AreEqual(expResult.Length, copiedArray.Length);

            for (int i = 0; i < expResult.Length; i++)
            {
                Assert.AreEqual(expResult[i], copiedArray[i], 0.00001);
            }

            float[] fExpResult = new float[] { 1.0545f, 2.0000f, 3.0000f, 3.8910f, 4.2936f, 3.8910f, 3.0000f, 2.0000f, 1.0545f };
            float[] fCopiedArray = new float[fExpResult.Length];

            Array.Copy(fExpResult, fCopiedArray, fExpResult.Length);

            Assert.AreEqual(fExpResult.Length, fCopiedArray.Length);

            for (int i = 0; i < fExpResult.Length; i++)
            {
                Assert.AreEqual(fExpResult[i], fCopiedArray[i], 0.00001);
            }

            int[] iExpResult = new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };
            int[] iCopiedArray = new int[iExpResult.Length];

            Array.Copy(iExpResult, iCopiedArray, iExpResult.Length);

            Assert.AreEqual(iExpResult.Length, iCopiedArray.Length);

            for (int i = 0; i < iExpResult.Length; i++)
            {
                Assert.AreEqual(iExpResult[i], iCopiedArray[i]);
            }
        }

        [TestMethod]
        public void TestCopyArraysAcrossTypes()
        {
            int[] iExpResult = new int[] { 1, 2, 3, 4, 5, 4, 3, 2, 1 };
            float[] iCopiedArray = new float[iExpResult.Length];

            Array.Copy(iExpResult, iCopiedArray, iExpResult.Length);

            Assert.AreEqual(iExpResult.Length, iCopiedArray.Length);

            for (int i = 0; i < iExpResult.Length; i++)
            {
                Assert.AreEqual(iExpResult[i], iCopiedArray[i], 0.001);
            }
        }
    }
}