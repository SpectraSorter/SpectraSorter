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

using MadWizard.WinUSBNet;
using OBP_Library;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace spectra.devices
{
    public class IPScanner
    {
        #region members

        // Instance
        private static IPScanner mInstance = null;

        public struct IPDevice
        {
            public string ipAddr, portNum, serialNum;
        }

        // The available IP device descriptors (the Dictionary is used to prevent duplicate entries)
        public Dictionary<string, IPDevice> mDevicesFound = null;

        #endregion members

        #region methods

        #region public

        public bool Scan()
        {
            // Get the IP addresses for this system
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

            // Build the set of local IPs
            HashSet<string> localIPs = new HashSet<string>();
            foreach (IPAddress localIPAddr in ipHostInfo.AddressList)
            {
                localIPs.Add(localIPAddr.ToString());
            }

            // Remove any previously found devices
            this.mDevicesFound.Clear();

            IPAddress multicastIP = new IPAddress(new byte[] { 239, 239, 239, 239 });
            int multicastPort = 57357;
            int multicastTTL = 3;

            //
            // Bind to each local IPv4 and multicast
            //
            // Note: Duplicate entries can occur if a system is bound to the same network on multiple local IPs
            //       such as from a Wifi network and a wired Ethernet network.  In this case the entries
            //       really aren't duplicate entries since you should be able to connect through either
            //       local interface.  However, at this time spectra only connects via the primary (default)
            //       local interface so duplicates are removed.
            //
            //       Ultimately something like “Device IP Address via Local IP Address” should probably be
            //       supported when discovering and connecting to devices.
            //
            foreach (IPAddress localIPAddr in ipHostInfo.AddressList)
            {
                // If IPv4...
                if (localIPAddr.AddressFamily == AddressFamily.InterNetwork)
                {
                    using (UdpClient udpclient = new UdpClient(AddressFamily.InterNetwork))
                    {
                        udpclient.JoinMulticastGroup(multicastIP, multicastTTL);

                        IPEndPoint localEP = new IPEndPoint(localIPAddr, multicastPort);
                        udpclient.Client.Bind(localEP);

                        IPEndPoint multicastEP = new IPEndPoint(multicastIP, multicastPort);
                        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, multicastPort);

                        // Initialize the OBP request serial number message
                        OBPBuffer sendBuffer = new OBPBuffer(64);
                        sendBuffer.IOBuffer[0] = 0xC1;
                        sendBuffer.IOBuffer[1] = 0xC0;
                        sendBuffer.IOBuffer[3] = 0x00;
                        sendBuffer.IOBuffer[9] = 0x01;
                        sendBuffer.IOBuffer[40] = 0x14;
                        sendBuffer.IOBuffer[60] = 0xC5;
                        sendBuffer.IOBuffer[61] = 0xC4;
                        sendBuffer.IOBuffer[62] = 0xC3;
                        sendBuffer.IOBuffer[63] = 0xC2;

                        // Initialize the send/receive timeouts
                        udpclient.Client.ReceiveTimeout = 200;
                        udpclient.Client.SendTimeout = 200;

                        try
                        {
                            // Send the multicast request for serial number
                            udpclient.Send(sendBuffer.IOBuffer, 64, multicastEP);
                        }
                        catch (SocketException)
                        {
                            // nothing to do
                        }

                        bool bReceivedData = false;
                        do
                        {
                            bReceivedData = false;
                            byte[] responseData = null;
                            try
                            {
                                responseData = udpclient.Receive(ref remoteEP);
                            }
                            catch (SocketException)
                            {
                                // nothing to do
                            }

                            if (responseData != null && responseData.Length > 0 && remoteEP != null)
                            {
                                bReceivedData = true;

                                char[] splitChar = { ':' };
                                string[] ipPort = remoteEP.ToString().Split(splitChar);
                                string ipAddr = ipPort.Length > 0 ? ipPort[0] : "unknown";
                                string portNum = ipPort.Length > 1 ? ipPort[1] : "unknown";
                                string serialNum = "unknown";
                                if (responseData.Length > 63)
                                {
                                    OBPBuffer responseBuffer = new OBPBuffer(responseData);
                                    OBPGetSerialNumber serialMessage = new OBPGetSerialNumber(sendBuffer, responseBuffer, false);
                                    serialMessage.initFromResponse();
                                    serialNum = serialMessage.SerialNum;
                                }

                                // Build a unique ID to prevent duplicate entries
                                string uniqueId = ipAddr + "-" + portNum + "-" + serialNum;
                                IPDevice dev;
                                dev.ipAddr = ipAddr;
                                dev.portNum = portNum;
                                dev.serialNum = serialNum;

                                if (!localIPs.Contains(ipAddr) && !this.mDevicesFound.ContainsKey(uniqueId))
                                {
                                    this.mDevicesFound.Add(uniqueId, dev);
                                }
                            }
                        } while (bReceivedData);
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
        private IPScanner()
        {
            this.mDevicesFound = new Dictionary<string, IPDevice>();
        }

        #endregion private

        #endregion methods

        #region properties

        /// <summary>
        /// USBScanner (singleton) instance.
        /// </summary>
        public static IPScanner Instance
        {
            get
            {
                // If the Form has not been created yet,
                // instantiate it now.
                if (mInstance == null)
                {
                    mInstance = new IPScanner();
                }

                // Return a reference
                return mInstance;
            }
        }

        public Dictionary<string, IPDevice> DeviceDescriptors
        {
            get => this.mDevicesFound;
        }

        #endregion properties
    }
}