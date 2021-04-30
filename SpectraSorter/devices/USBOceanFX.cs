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
using OBP_Library;
using OceanUtil;
using System;

namespace spectra.devices
{
    public class USBOceanFX : OceanFX
    {
        private USBIO mUSBIO = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public USBOceanFX(USBDeviceInfo devInfo) : base()
        {
            // Instantiate the new USB device
            mUSBIO = new USBIO(devInfo);

            // Assign it to the ISendReceive parent reference
            mActiveIO = mUSBIO;
        }

        public override bool IsConnected => (this.mUSBIO != null);

        /// <summary>
        /// Connect to OceanFX.
        /// </summary>
        public override void Connect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Disconnect from OceanFX.
        /// </summary>
        public override void Disconnect()
        {
            try
            {
                if (this.mUSBIO != null)
                {
                    this.mUSBIO.Dispose();
                    this.mUSBIO = null;
                }

            }
            catch (Exception)
            {
                // nothing to do
            }
        }
    }
}