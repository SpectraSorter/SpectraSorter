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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spectra.state
{
    public class State
    {
        #region members

        // Instance
        private static State mInstance = null;

        // Volatile members: they are accessed across threads
        // (UI and processing)
        private volatile bool mIsPerformingStandardAcquisition = false;
        private volatile bool mIsPerformingCompute = false;
        private volatile bool mIsPerformingSave = false;
        private volatile bool mIsPerformingAccumulationAcquisition = false;

        private volatile bool mStopAcquiringRequested = false;
        private volatile uint mTotalHitNumber = 0;

        #endregion members

        #region methods

        #region private

        private State()
        {
        }

        #endregion private

        #endregion methods

        #region properties

        /// <summary>
        /// State (singleton) instance.
        /// </summary>
        public static State Instance
        {
            get
            {
                // If the Form has not been created yet,
                // instantiate it now.
                if (mInstance == null)
                {
                    mInstance = new State();
                }

                // Return a reference
                return mInstance;
            }
        }

        // Volatile!
        public bool IsPerformingStandardAcquisition
        {
            get => this.mIsPerformingStandardAcquisition;
            set => this.mIsPerformingStandardAcquisition = value;
        }

        // Volatile!
        public bool IsStopAcquiringRequested
        {
            get => this.mStopAcquiringRequested;
            set => this.mStopAcquiringRequested = value;
        }

        // Volatile!
        public bool IsPerformingCompute
        {
            get => this.mIsPerformingCompute;
            set => this.mIsPerformingCompute = value;
        }

        // Volatile!
        public bool IsPerformingSave
        {
            get => this.mIsPerformingSave;
            set => this.mIsPerformingSave = value;
        }

        // Volatile!
        public bool IsPerformingAccumulationAcquisition
        {
            get => this.mIsPerformingAccumulationAcquisition;
            set => this.mIsPerformingAccumulationAcquisition = value;
        }

        // Volatile!
        public uint TotalHitNumber
        {
            get => this.mTotalHitNumber;
            set => this.mTotalHitNumber = value;
        }

        public bool IsUSBScanning { get; set; }

        public bool IsIPScanning { get; set; }

        public bool IsCOMScanning { get; set; }

        public bool IsClosing { get; set; }

        public bool IsInit { get; set; }

        #endregion properties
    }
}
