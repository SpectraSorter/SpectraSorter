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

namespace spectra.processing
{
    /// <summary>
    /// A Filter class that can be used to perform (circular) convolution with
    /// an average filter of user-defined support (width) or a Gaussian filter
    /// of user-defined support (width) and sigma.
    ///
    /// The Filter is initialized as an average filter with a kernel of support
    /// (width) 1 of value 1.0 and sigma 0.0.
    ///
    /// </summary>
    public class Filter
    {
        public double[] Kernel { get; private set; }
        public bool IsAverage { get; private set; } = true;
        public bool IsGaussian { get; private set; } = false;
        public bool IsCustom { get; private set; } = false;
        public int Support { get; private set; } = 1;
        public double Sigma { get; private set; } = 0.0;

        /// <summary>
        /// Return true if the kernel has support 1 and value 1.0.
        /// </summary>
        ///
        /// Convolving with the default kernel does not change the signal.
        /// <returns>True if the kernel has support 1 and value 1.0, false otherwise.</returns>
        public bool IsUnit { get => (Support == 1 && Kernel[0] == 1.0); }

        public Filter()
        {
            Kernel = new double[1] { 1.0 };
        }

        /// <summary>
        /// Create an average filter kernel with given support (size) to be used for convolution.
        /// </summary>
        ///
        /// <param name="support">Support (size) of the kernel. Please notice that support must
        /// be odd; if it is even, support is increased by one.</param>
        /// <returns>Support used (it might be different from the input value if this was even.</returns>
        public int UseAverageFilter(int support)
        {
            if (support < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(support), @"Support must be >= 1.");
            }

            if (support % 2 == 0)
            {
                support++;
            }

            Kernel = new double[support];

            for (int p = 0; p < Kernel.Length; p++)
            {
                Kernel[p] = 1.0 / (double)support;
            }

            // Set flags
            IsAverage = true;
            IsGaussian = false;
            IsCustom = false;

            // Set kernel parameters
            Support = support;
            Sigma = 0.0;

            return support;
        }

        /// <summary>
        /// Create a Gaussian filter kernel with given sigma to be used for convolution.
        /// </summary>
        ///
        /// The support is automatically calculated from sigma.
        /// <param name="sigma">Sigma of the Gaussian kernel.</param>
        /// <returns>Support used.</returns>
        public int UseGaussianFilter(double sigma)
        {
            return UseGaussianFilter(SupportFromSigma(sigma), sigma);
        }

        /// <summary>
        /// Create a Gaussian filter kernel with given support to be used for convolution.
        /// </summary>
        ///
        /// Sigma is automatically calculated from the support.
        /// <param name="support">Support (size) of the kernel.</param>
        /// <returns>Support used (it might be different from the input value if this was even.</returns>
        public int UseGaussianFilter(int support)
        {
            return UseGaussianFilter(support, SigmaFromSupport(support));
        }

        /// <summary>
        /// Create a Gaussian filter kernel with given mSupport (size) and mSigma to be used for convolution.
        /// </summary>
        /// <param name="support">Support (size) of the kernel. Please notice that support must
        /// be odd; if it is even, support is increased by one.</param>
        /// <param name="sigma">Sigma of the Gaussian kernel.</param>
        /// <returns>support used (it might be different from the input value if this was even.</returns>
        public int UseGaussianFilter(int support, double sigma)
        {
            if (support < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(support), @"Support must be >= 1.");
            }

            if (sigma <= 0)
            {
                sigma = 1.0;
                support = 7;
            }

            if (support % 2 == 0)
            {
                support++;
            }

            Kernel = new double[support];
            int n = support / 2;
            double den = 2.0 * sigma * sigma;
            for (int i = -n, p = 0; i <= n; i++, p++)
            {
                double iSq = (double)i * (double)i;

                Kernel[p] = Math.Exp(-iSq / den);
            }

            // Normalize to 1
            double sum = 0;
            for (int p = 0; p < Kernel.Length; p++)
            {
                sum += Kernel[p];
            }
            for (int p = 0; p < Kernel.Length; p++)
            {
                Kernel[p] = Kernel[p] / sum;
            }

            // Set flags
            IsAverage = false;
            IsGaussian = true;
            IsCustom = false;

            // Set kernel parameters
            Support = support;
            Sigma = sigma;

            return support;
        }

        /// <summary>
        /// Sets a user-defined filter kernel to be used for convolution.
        /// </summary>
        ///
        /// <param name="kernel">A double precision array of kernel weights. No normalization is performed.</param>
        /// <returns>Support of the kernel.</returns>
        public int UseCustomFilter(double[] kernel)
        {
            if (kernel.Length % 2 == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(kernel), @"Kernel must have odd length.");
            }

            // Set the kernel
            Kernel = kernel;

            // Set flags
            IsAverage = false;
            IsGaussian = false;
            IsCustom = true;

            // Set kernel parameters
            Support = kernel.Length;
            Sigma = 0.0;

            return Support;
        }

        /// <summary>
        /// Perform convolution with the current kernel.
        /// </summary>
        /// <param name="signal">Signal to be convolved.</param>
        /// <param name="full">Whether a full convolution is returned, or only the central
        /// part that corresponds to the signal size.</param>
        /// <returns>Convolved signal.</returns>
        public double[] Convolve(double[] signal, bool full = true, bool symmetric = false)
        {
            if (full)
            {
                return ConvolveFull(signal, symmetric);
            }
            else
            {
                return ConvolveSame(signal, symmetric);
            }
        }

        /// <summary>
        /// Perform convolution with the current kernel.
        /// </summary>
        /// <param name="signal">Signal to be convolved.</param>
        /// <param name="full">Whether a full convolution is returned, or only the central
        /// part that corresponds to the signal size.</param>
        /// <returns>Convolved signal.</returns>
        public float[] Convolve(float[] signal, bool full = true, bool symmetric = false)
        {
            if (full)
            {
                return ConvolveFull(signal, symmetric);
            }
            else
            {
                return ConvolveSame(signal, symmetric);
            }
        }

        /// <summary>
        /// Perform convolution with the current kernel.
        /// </summary>
        /// <param name="signal">Signal to be convolved.</param>
        /// <param name="full">Whether a full convolution is returned, or only the central
        /// part that corresponds to the signal size.</param>
        /// <returns>Convolved signal.</returns>
        public int[] Convolve(int[] signal, bool full = true, bool symmetric = false)
        {
            if (full)
            {
                return ConvolveFull(signal, symmetric);
            }
            else
            {
                return ConvolveSame(signal, symmetric);
            }
        }

        /// <summary>
        /// Perform convolution with the current kernel.
        /// </summary>
        /// <param name="signal">Signal to be convolved.</param>
        /// <param name="full">Whether a full convolution is returned, or only the central
        /// part that corresponds to the signal size.</param>
        /// <returns>Convolved signal.</returns>
        public ushort[] Convolve(ushort[] signal, bool full = true, bool symmetric = false)
        {
            if (full)
            {
                return ConvolveFull(signal, symmetric);
            }
            else
            {
                return ConvolveSame(signal, symmetric);
            }
        }

        /// <summary>
        /// Return an acceptable support for given sigma (Gaussian kernel).
        /// </summary>
        /// <param name="sigma">Sigma for the Gaussian kernel.</param>
        /// <returns>Suggested support.</returns>
        public int SupportFromSigma(double sigma)
        {
            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma), @"Sigma must be a positive number.");
            }

            return (int)(6 * sigma + 1);
        }

        /// <summary>
        /// Return an acceptable sigma value for given support (Gaussian kernel).
        /// </summary>
        /// <param name="support">Support for the Gaussian kernel.</param>
        /// <returns>Suggested sigma.</returns>
        public double SigmaFromSupport(int support)
        {
            if (support < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(support), @"Support must be at least 1.");
            }

            return ((double)support - 1) / 6;
        }

        /// <summary>
        /// Perform full convolution with current kernel.
        /// </summary>
        /// <param name="signal">Signal to be convolved.</param>
        /// <returns>Convolved signal.</returns>
        private double[] ConvolveFull(double[] signal, bool symmetric)
        {
            int size = signal.Length + Kernel.Length - 1;

            double[] result = new double[size];

            if (symmetric)
            {
                // Convolution with symmetric (virtual) padding
                int maxK = Kernel.Length - 1;
                int maxS = signal.Length - 1;

                for (int i = 0; i < size; i++)
                {
                    for (int k = maxK; k >= 0; k--)
                    {
                        int r = i - k;
                        if (r < 0)
                        {
                            r = -r;
                        }

                        if (r > maxS)
                        {
                            r = maxS - (r - maxS);
                        }

                        result[i] += signal[r] * Kernel[k];
                    }
                }
            }
            else
            {
                // Convolution with 0 (virtual) padding
                for (int i = 0; i < size; i++)
                {
                    int kMin = (i >= Kernel.Length - 1) ? i - (Kernel.Length - 1) : 0;
                    int kMax = (i < signal.Length - 1) ? i : signal.Length - 1;

                    for (int k = kMin; k <= kMax; k++)
                    {
                        result[i] += signal[k] * Kernel[i - k];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Perform full convolution with current kernel.
        /// </summary>
        /// <param name="signal">Signal to be convolved.</param>
        /// <returns>Convolved signal.</returns>
        private float[] ConvolveFull(float[] signal, bool symmetric)
        {
            int size = signal.Length + Kernel.Length - 1;

            float[] result = new float[size];

            if (symmetric)
            {
                // Convolution with symmetric (virtual) padding
                int maxK = Kernel.Length - 1;
                int maxS = signal.Length - 1;

                for (int i = 0; i < size; i++)
                {
                    for (int k = maxK; k >= 0; k--)
                    {
                        int r = i - k;

                        if (r < 0)
                        {
                            r = -r;
                        }

                        if (r > maxS)
                        {
                            r = maxS - (r - maxS);
                        }

                        result[i] += signal[r] * (float)Kernel[k];
                    }
                }
            }
            else
            {
                // Convolution with 0 (virtual) padding
                for (int i = 0; i < size; i++)
                {
                    int kMin = (i >= Kernel.Length - 1) ? i - (Kernel.Length - 1) : 0;
                    int kMax = (i < signal.Length - 1) ? i : signal.Length - 1;

                    for (int k = kMin; k <= kMax; k++)
                    {
                        result[i] += signal[k] * (float)Kernel[i - k];
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Perform full convolution with current kernel.
        /// </summary>
        /// <param name="signal">Signal to be convolved.</param>
        /// <returns>Convolved signal.</returns>
        private int[] ConvolveFull(int[] signal, bool symmetric)
        {
            int size = signal.Length + Kernel.Length - 1;

            int[] result = new int[size];

            if (symmetric)
            {
                // Convolution with symmetric (virtual) padding
                int maxK = Kernel.Length - 1;
                int maxS = signal.Length - 1;

                for (int i = 0; i < size; i++)
                {
                    double tmp = result[i];
                    for (int k = maxK; k >= 0; k--)
                    {
                        int r = i - k;
                        if (r < 0)
                        {
                            r = -r;
                        }

                        if (r > maxS)
                        {
                            r = maxS - (r - maxS);
                        }

                        tmp += (double)signal[r] * Kernel[k];
                    }
                    result[i] = (int)Math.Round(tmp);
                }
            }
            else
            {
                // Convolution with 0 (virtual) padding
                for (int i = 0; i < size; i++)
                {
                    int kMin = (i >= Kernel.Length - 1) ? i - (Kernel.Length - 1) : 0;
                    int kMax = (i < signal.Length - 1) ? i : signal.Length - 1;

                    double tmp = result[i];
                    for (int k = kMin; k <= kMax; k++)
                    {
                        tmp += (double)signal[k] * Kernel[i - k];
                    }

                    result[i] = (int)Math.Round(tmp);
                }
            }

            return result;
        }

        /// <summary>
        /// Perform full convolution with current kernel.
        /// </summary>
        /// <param name="signal">Signal to be convolved.</param>
        /// <returns>Convolved signal.</returns>
        private ushort[] ConvolveFull(ushort[] signal, bool symmetric)
        {
            int size = signal.Length + Kernel.Length - 1;

            ushort[] result = new ushort[size];

            if (symmetric)
            {
                // Convolution with symmetric (virtual) padding
                int maxK = Kernel.Length - 1;
                int maxS = signal.Length - 1;

                for (int i = 0; i < size; i++)
                {
                    double tmp = result[i];
                    for (int k = maxK; k >= 0; k--)
                    {
                        int r = i - k;
                        if (r < 0)
                        {
                            r = -r;
                        }

                        if (r > maxS)
                        {
                            r = maxS - (r - maxS);
                        }

                        tmp += (double)signal[r] * Kernel[k];
                    }
                    result[i] = (ushort)Math.Round(tmp);
                }
            }
            else
            {
                // Convolution with 0 (virtual) padding
                for (int i = 0; i < size; i++)
                {
                    int kMin = (i >= Kernel.Length - 1) ? i - (Kernel.Length - 1) : 0;
                    int kMax = (i < signal.Length - 1) ? i : signal.Length - 1;

                    double tmp = result[i];
                    for (int k = kMin; k <= kMax; k++)
                    {
                        tmp += (double)signal[k] * Kernel[i - k];
                    }

                    result[i] = (ushort)Math.Round(tmp);
                }
            }

            return result;
        }

        /// <summary>
        /// Perform convolution with current kernel and return the central
        /// part of the convolution that is the same size as signal.
        /// </summary>
        /// <param name="signal">Signal to be convolved.</param>
        /// <returns>Convolved signal.</returns>
        private double[] ConvolveSame(double[] signal, bool symmetric)
        {
            int size = signal.Length + Kernel.Length - 1;

            double[] result = new double[signal.Length];

            if (symmetric)
            {
                int iMin = Kernel.Length / 2;
                int iMax = size - Kernel.Length / 2;

                // Convolution with symmetric (virtual) padding
                int maxK = Kernel.Length - 1;
                int maxS = signal.Length - 1;

                for (int i = iMin; i < iMax; i++)
                {
                    for (int k = maxK; k >= 0; k--)
                    {
                        int r = i - k;
                        if (r < 0)
                        {
                            r = -r;
                        }

                        if (r > maxS)
                        {
                            r = maxS - (r - maxS);
                        }

                        result[i - iMin] += signal[r] * Kernel[k];
                    }
                }
            }
            else
            {
                // Convolution with 0 (virtual) padding
                int iMin = Kernel.Length / 2;
                int iMax = size - Kernel.Length / 2 - 1;

                for (int i = 0; i < size; i++)
                {
                    if (i - iMin >= 0 && i <= iMax)
                    {
                        int kMin = (i >= Kernel.Length - 1) ? i - (Kernel.Length - 1) : 0;
                        int kMax = (i < signal.Length - 1) ? i : signal.Length - 1;

                        for (int k = kMin; k <= kMax; k++)
                        {
                            result[i - iMin] += signal[k] * Kernel[i - k];
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Perform convolution with current kernel and return the central
        /// part of the convolution that is the same size as signal.
        /// </summary>
        /// <param name="signal">Signal to be convolved.</param>
        /// <returns>Convolved signal.</returns>
        private float[] ConvolveSame(float[] signal, bool symmetric)
        {
            int size = signal.Length + Kernel.Length - 1;

            float[] result = new float[signal.Length];

            if (symmetric)
            {
                int iMin = Kernel.Length / 2;
                int iMax = size - Kernel.Length / 2;

                // Convolution with symmetric (virtual) padding
                int maxK = Kernel.Length - 1;
                int maxS = signal.Length - 1;

                for (int i = iMin; i < iMax; i++)
                {
                    for (int k = maxK; k >= 0; k--)
                    {
                        int r = i - k;
                        if (r < 0)
                        {
                            r = -r;
                        }

                        if (r > maxS)
                        {
                            r = maxS - (r - maxS);
                        }

                        result[i - iMin] += signal[r] * (float)Kernel[k];
                    }
                }
            }
            else
            {
                // Convolution with 0 (virtual) padding
                int iMin = Kernel.Length / 2;
                int iMax = size - Kernel.Length / 2 - 1;

                for (int i = 0; i < size; i++)
                {
                    if (i - iMin >= 0 && i <= iMax)
                    {
                        int kMin = (i >= Kernel.Length - 1) ? i - (Kernel.Length - 1) : 0;
                        int kMax = (i < signal.Length - 1) ? i : signal.Length - 1;

                        for (int k = kMin; k <= kMax; k++)
                        {
                            result[i - iMin] += signal[k] * (float)Kernel[i - k];
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Perform convolution with current kernel and return the central
        /// part of the convolution that is the same size as signal.
        /// </summary>
        /// <param name="signal">Signal to be convolved.</param>
        /// <returns>Convolved signal.</returns>
        private int[] ConvolveSame(int[] signal, bool symmetric)
        {
            int size = signal.Length + Kernel.Length - 1;

            int[] result = new int[signal.Length];

            if (symmetric)
            {
                int iMin = Kernel.Length / 2;
                int iMax = size - Kernel.Length / 2;

                // Convolution with symmetric (virtual) padding
                int maxK = Kernel.Length - 1;
                int maxS = signal.Length - 1;

                for (int i = iMin; i < iMax; i++)
                {
                    double tmp = result[i - iMin];
                    for (int k = maxK; k >= 0; k--)
                    {
                        int r = i - k;
                        if (r < 0)
                        {
                            r = -r;
                        }

                        if (r > maxS)
                        {
                            r = maxS - (r - maxS);
                        }

                        tmp += (double)signal[r] * Kernel[k];
                    }
                    result[i - iMin] = (int)Math.Round(tmp);
                }
            }
            else
            {
                // Convolution with 0 (virtual) padding
                int iMin = Kernel.Length / 2;
                int iMax = size - Kernel.Length / 2 - 1;

                for (int i = 0; i < size; i++)
                {
                    if (i - iMin >= 0 && i <= iMax)
                    {
                        int kMin = (i >= Kernel.Length - 1) ? i - (Kernel.Length - 1) : 0;
                        int kMax = (i < signal.Length - 1) ? i : signal.Length - 1;

                        double tmp = result[i - iMin];

                        for (int k = kMin; k <= kMax; k++)
                        {
                            tmp += (double)signal[k] * Kernel[i - k];
                        }

                        result[i - iMin] = (int)Math.Round(tmp);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Perform convolution with current kernel and return the central
        /// part of the convolution that is the same size as signal.
        /// </summary>
        /// <param name="signal">Signal to be convolved.</param>
        /// <returns>Convolved signal.</returns>
        private ushort[] ConvolveSame(ushort[] signal, bool symmetric)
        {
            int size = signal.Length + Kernel.Length - 1;

            ushort[] result = new ushort[signal.Length];

            if (symmetric)
            {
                int iMin = Kernel.Length / 2;
                int iMax = size - Kernel.Length / 2;

                // Convolution with symmetric (virtual) padding
                int maxK = Kernel.Length - 1;
                int maxS = signal.Length - 1;

                for (int i = iMin; i < iMax; i++)
                {
                    double tmp = result[i - iMin];
                    for (int k = maxK; k >= 0; k--)
                    {
                        int r = i - k;
                        if (r < 0)
                        {
                            r = -r;
                        }

                        if (r > maxS)
                        {
                            r = maxS - (r - maxS);
                        }

                        tmp += (double)signal[r] * Kernel[k];
                    }
                    result[i - iMin] = (ushort)Math.Round(tmp);
                }
            }
            else
            {
                // Convolution with 0 (virtual) padding
                int iMin = Kernel.Length / 2;
                int iMax = size - Kernel.Length / 2 - 1;

                for (int i = 0; i < size; i++)
                {
                    if (i - iMin >= 0 && i <= iMax)
                    {
                        int kMin = (i >= Kernel.Length - 1) ? i - (Kernel.Length - 1) : 0;
                        int kMax = (i < signal.Length - 1) ? i : signal.Length - 1;

                        double tmp = result[i - iMin];

                        for (int k = kMin; k <= kMax; k++)
                        {
                            tmp += (double)signal[k] * Kernel[i - k];
                        }

                        result[i - iMin] = (ushort)Math.Round(tmp);
                    }
                }
            }

            return result;
        }

    }
}