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

using MadWizard.WinUSBNet;
using System.Collections.Generic;
using System.Globalization;

namespace spectra.devices
{
    public class USBScanner
    {
        #region members

        // Instance
        private static USBScanner mInstance = null;

        // The available USB device descriptors
        private Dictionary<string, USBDeviceInfo> mUsbDeviceDescriptors = new Dictionary<string, USBDeviceInfo>();

        #endregion members

        #region methods

        #region public

        public bool Scan()
        {
            // Remove any previously found devices
            this.mUsbDeviceDescriptors.Clear();

            foreach (string devGuid in OceanFX.DEVICE_GUIDS)
            {
                // Find all devices with the Ocean GUID
                USBDeviceInfo[] usbDeviceDescriptors = USBDevice.GetDevices(devGuid.ToUpper(CultureInfo.InvariantCulture));

                // If one or more devices were found with the specified GUID...
                if (usbDeviceDescriptors.Length > 0)
                {
                    foreach (USBDeviceInfo devInfo in usbDeviceDescriptors)
                    {
                        if (devInfo.PID == OceanFX.PID)
                        {
                            mUsbDeviceDescriptors.Add(devInfo.DeviceDescription, devInfo);
                        }
                    }
                }
            }
            return true;
        }

        #endregion public

        #region private

        /// <summary>
        /// Private constructor.
        /// </summary>
        private USBScanner()
        {
            this.mUsbDeviceDescriptors = new Dictionary<string, USBDeviceInfo>();
        }

        #endregion private

        #endregion methods

        #region properties

        /// <summary>
        /// USBScanner (singleton) instance.
        /// </summary>
        public static USBScanner Instance
        {
            get
            {
                // If the Form has not been created yet,
                // instantiate it now.
                if (mInstance == null)
                {
                    mInstance = new USBScanner();
                }

                // Return a reference
                return mInstance;
            }
        }

        public Dictionary<string, USBDeviceInfo> DeviceDescriptors
        {
            get => this.mUsbDeviceDescriptors;
        }

        #endregion properties
    }
}