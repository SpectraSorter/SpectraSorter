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

using spectra.state;

namespace spectra.processing
{
    public class SpectrumFilterer
    {
        #region members

        // Instance
        private static SpectrumFilterer mInstance = null;

        //private bool mSpectrumFilteringEnabled = SettingsManager.SpectrumFilteringEnabled;
        //private bool mSpectrumFilteringAverage = SettingsManager.SpectrumFilteringAverage;
        //private bool mSpectrumFilteringGaussian = SettingsManager.SpectrumFilteringGaussian;
        private Filter mFilter = null;

        #endregion members

        #region methods

        #region public

        public void Reset()
        {
            this.UseAverageFilter(1);
        }

        public int UseAverageFilter(int support)
        {
            if (this.mFilter == null)
            {
                this.mFilter = new Filter();
            }
            return this.mFilter.UseAverageFilter(support);
        }

        public int UseGaussianFilter(int support)
        {
            if (this.mFilter == null)
            {
                this.mFilter = new Filter();
            }
            return this.mFilter.UseGaussianFilter(support);
        }

        public int UseGaussianFilter(double sigma)
        {
            if (this.mFilter == null)
            {
                this.mFilter = new Filter();
            }
            return this.mFilter.UseGaussianFilter(sigma);
        }
        public int UseGaussianFilter(int support, double sigma)
        {
            if (this.mFilter == null)
            {
                this.mFilter = new Filter();
            }
            return this.mFilter.UseGaussianFilter(support, sigma);
        }

        public void UseCustomFilter(double[] kernel)
        {
            if (this.mFilter == null)
            {
                this.mFilter = new Filter();
            }
            this.mFilter.UseCustomFilter(kernel);
        }

        public double[] Convolve(double[] signal, bool full = true, bool symmetric = false)
        {
            if (this.mFilter == null)
            {
                this.mFilter = new Filter();
            }
            return this.mFilter.Convolve(signal, full, symmetric);
        }

        public float[] Convolve(float[] signal, bool full = true, bool symmetric = false)
        {
            if (this.mFilter == null)
            {
                this.mFilter = new Filter();
            }
            return this.mFilter.Convolve(signal, full, symmetric);
        }

        public int[] Convolve(int[] signal, bool full = true, bool symmetric = false)
        {
            if (this.mFilter == null)
            {
                this.mFilter = new Filter();
            }
            return this.mFilter.Convolve(signal, full, symmetric);
        }

        public ushort[] Convolve(ushort[] signal, bool full = true, bool symmetric = false)
        {
            if (this.mFilter == null)
            {
                this.mFilter = new Filter();
            }
            return this.mFilter.Convolve(signal, full, symmetric);
        }

        public int SupportFromSigma(double sigma)
        {
            return this.mFilter.SupportFromSigma(sigma);
        }

        public double SigmaFromSupport(int support)
        {
            return this.mFilter.SigmaFromSupport(support);
        }

        public void InitializeFilterFromCurrentSettings()
        {
            if (SettingsManager.SpectrumFilteringEnabled == false)
            {
                // Filtering is not requested; we can stop here.
                return;
            }

            if (SettingsManager.SpectrumFilteringAverage == true)
            {
                this.UseAverageFilter(SettingsManager.SpectrumFilteringSupport);
                return;
            }

            if (SettingsManager.SpectrumFilteringGaussian == true)
            {
                this.UseGaussianFilter(SettingsManager.SpectrumFilteringSupport);
                return;
            }
        }

        #endregion public

        #region private

        private SpectrumFilterer()
        {
            this.mFilter = new Filter();
            this.Reset();
        }

        #endregion private

        #endregion methods

        #region properties

        /// <summary>
        /// SpectrumFilterer (singleton) instance.
        /// </summary>
        public static SpectrumFilterer Instance
        {
            get
            {
                // If the Form has not been created yet,
                // instantiate it now.
                if (mInstance == null)
                {
                    mInstance = new SpectrumFilterer();
                }

                // Return a reference
                return mInstance;
            }
        }

        //public bool SpectrumFilteringEnabled { get => this.mSpectrumFilteringEnabled; set => this.mSpectrumFilteringEnabled = value; }
        //public bool SpectrumFilteringAverage { get => this.mSpectrumFilteringAverage; set => this.mSpectrumFilteringAverage = value; }
        //public bool SpectrumFilteringGaussian { get => this.mSpectrumFilteringGaussian; set => this.mSpectrumFilteringGaussian = value; }
        public double Sigma { get => this.mFilter.Sigma; }
        public int Support { get => this.mFilter.Support; }
        public bool IsAverage { get => this.mFilter.IsAverage; }
        public bool IsGaussian { get => this.mFilter.IsGaussian; }
        public bool IsUnit { get => this.mFilter.IsUnit; }
        public double[] Kernel { get => this.mFilter.Kernel; }

        #endregion properties
    }
}
