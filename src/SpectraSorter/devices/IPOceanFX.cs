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

using OceanUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spectra.devices
{
    public class IPOceanFX : OceanFX
    {
        private TcpIpIO mTCPIO = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public IPOceanFX(System.Net.IPAddress ipAddr, int portNum) : base()
        {
            try
            {
                // Instantiate the new IP device
                mTCPIO = new TcpIpIO(ipAddr, portNum);
                if (mTCPIO != null)
                {
                    if (mTCPIO.MySocket == null)
                    {
                        mTCPIO = null;
                    }
                    else if (!mTCPIO.MySocket.Connected)
                    {
                        mTCPIO.Connect();
                    }

                    if (!mTCPIO.MySocket.Connected)
                    {
                        mTCPIO = null;
                    }
                    else
                    {
                        mActiveIO = mTCPIO;
                    }
                }
            }
            catch (Exception)
            {
                mTCPIO = null;
            }

            // Assign it to the ISendReceive parent reference
            mActiveIO = mTCPIO;
        }

        public override bool IsConnected => (this.mTCPIO != null);

        public override void Connect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Disconnect from device.
        /// </summary>
        public override void Disconnect()
        {
            try
            {
                if (mTCPIO != null)
                {
                    mTCPIO.MySocket.Close(500);
                    mTCPIO = null;
                }

            }
            catch (Exception)
            {
                // nothing to do
            }
        }
    }
}
