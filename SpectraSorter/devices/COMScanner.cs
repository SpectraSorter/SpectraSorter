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
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace spectra.devices
{
    public class COMScanner
    {
        #region members

        // Instance
        private static COMScanner mInstance = null;

        // The available COM device descriptors
        private Dictionary<string, SerialPortWrapper> mCOMDeviceDescriptors = null;

        #endregion members

        #region methods

        #region public

        public bool Scan()
        {
            // Clear list of known COM devices
            this.mCOMDeviceDescriptors.Clear();

            // Scan the COM ports
            ManagementScope connectionScope = new ManagementScope();
            SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, serialQuery);

            try
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    String desc = item["Description"].ToString();
                    String deviceId = item["DeviceID"].ToString();

                    if (desc.Contains("Arduino"))
                    {
                        SerialPortWrapper device = new SerialPortWrapper(desc, deviceId);
                        mCOMDeviceDescriptors.Add(device.ToString(), device);
                    }
                }
            }
            catch (ManagementException)
            {
                this.mCOMDeviceDescriptors.Clear();
                return false;
            }

            return true;
        }

        #endregion public

        #region private

        /// <summary>
        /// Private constructor. 
        /// </summary>
        private COMScanner()
        {
            this.mCOMDeviceDescriptors = new Dictionary<string, SerialPortWrapper>();
        }

        #endregion private

        #endregion methods

        #region properties

        /// <summary>
        /// COMScanner (singleton) instance.
        /// </summary>
        public static COMScanner Instance
        {
            get
            {
                // If the Form has not been created yet,
                // instantiate it now.
                if (mInstance == null)
                {
                    mInstance = new COMScanner();
                }

                // Return a reference
                return mInstance;
            }
        }

        public Dictionary<string, SerialPortWrapper> DeviceDescriptors
        {
            get => this.mCOMDeviceDescriptors;
        }

        #endregion properties

    }
}
