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

using spectra.utils;
using System;
using System.IO.Ports;
using System.Threading;

namespace spectra.devices
{
    public class Arduino
    {
        // Instantiate an high-res timer
        HighResStopWatch stopwatch = new HighResStopWatch();

        public enum COMMANDS
        {
            RESET_COUNTER = 0,   // No parameters
            START = 1,           // No parameters
            TRIGGER = 2,         // No parameters
            QUERY_COUNTER = 3,   // No parameters
            STOP = 4,            // No parameters
            SET_PIN = 5,         // UInt32 parameter
            QUERY_PIN = 6,       // No parameters
            SET_DURATION = 7,    // UInt32 parameter
            QUERY_DURATION = 8   // No parameters
        };

        private String portName = "";
        private SerialPort serialPortObj = null;
        private int baudRate = 115200;
        private UInt32 onBoardCounter = 0;
        private UInt32 pin = 0;
        private UInt32 triggerDuration = 0;
        private UInt32 triggerDelayInMicros = 0;

        // Properties
        public string PortName { get => portName; set => portName = value; }
        public int BaudRate { get => baudRate; set => baudRate = value; }

        // Arduino properties
        public UInt32 OnBoardCounter { get => onBoardCounter; }
        public UInt32 Pin { get => pin; }
        public UInt32 TriggerDuration { get => triggerDuration; }
        public UInt32 TriggerDelayInMicros {
            get => triggerDelayInMicros;
            set => triggerDelayInMicros = value;
        }

        /**
        * Default constructor.
        */
        public Arduino()
        {
            // Set defaults
            portName = "";
            baudRate = 115200;
        }

        /**
         * Connect to Arduino.
         */
        public bool Connect()
        {
            bool connected = false;

            if (string.IsNullOrEmpty(portName))
            {
                throw new Exception("COM port not specified! Cannot connect to Arduino.");
            }

            if (serialPortObj == null)
            {
                // Set up the SerialPort object
                serialPortObj = new SerialPort
                {
                    PortName = portName,
                    BaudRate = baudRate
                };

                serialPortObj.DataReceived += this.Received;
            }

            // Open the serial connection
            if (!serialPortObj.IsOpen)
            {
                serialPortObj.Open();

                connected = true;
            }

            return connected;
        }

        /**
         * Disconnect from Arduino.
         */
        public void Disconnect()
        {
            // Close the serial connection
            if (serialPortObj != null && serialPortObj.IsOpen)
            {
                serialPortObj.Close();
                serialPortObj = null;
            }
        }

        /**
         * Destructor.
         */
        ~Arduino()
        {
            // Make sure to disconnect on destruction.
            Disconnect();
        }

        /**
         * Check if  Arduino is currently connected.
         * @return true is the Arduino is connected, false otherwise.
         */ 
        public bool IsConnected()
        {
            return (serialPortObj != null && serialPortObj.IsOpen);
        }

        /**
         * Send one of the predefined COMMANDS to Arduino.
         * 
         * @param One of COMMANDS.{RESET_COUNTER, START, TRIGGER, QUERY_COUNTER, STOP, QUERY_PIN}; 
         * 
         * These commands do not take any arguments. For simplicity, all command packets 
         * are 9 bytes long (whether they send parameters or not), and have following 
         * structure:
         * 
         * HEADER   0:  0xFF
         *          1:  0xFE
         *          2:  0xFD
         * COMMAND  3:  of of COMMANDS enum
         * PARAM    4:  0
         *          5:  0
         *          6:  0
         *          7:  0
         * FOOTER   8:  0xFF        
         */
        public void SendCommand(COMMANDS command)
        {
            // Command packet
            byte[] bytes = {
                0xFF,                     // Header (3 bytes)
                0xFE,
                0xFD,
                (byte)command,            // Command
                0,                        // Parameter is 0 (ignored)
                0,
                0,
                0,
                0xFF                      // Footer (1 byte)
            };

            if (command == COMMANDS.TRIGGER)
            {
                // Apply the requested delay in microseconds
                stopwatch.Start();
                stopwatch.WaitForMicroseconds(triggerDelayInMicros);
            }

            // Send the packet
            SendBytes(bytes);

            if (command == COMMANDS.TRIGGER)
            {
                stopwatch.Stop();
            }

            // Context switch
            Thread.Sleep(0);
        }

        /**
         * Send one of the predefined COMMANDS to Arduino followed by a UInt32 parameter.
         *
         * @param One of COMMANDS.{SET_PIN, SET_DURATION}; 
         *
         * These commands take one UINT32 argument passed as 4x 1 byte. All command packets 
         * are 9 bytes long, and have following structure
         * 
         * HEADER   0:  0xFF
         *          1:  0xFE
         *          2:  0xFD
         * COMMAND  3:  of of COMMANDS enum
         * PARAM    4:  0 Least significant bit
         *          5:  0
         *          6:  0
         *          7:  0 Most significant bit
         * FOOTER   8:  0xFF

         */
        public void SendCommandWithParameter(COMMANDS command, UInt32 parameter)
        {
            // Turn the UInt32 parameter into a 4-byte array
            byte[] paramBytes = BitConverter.GetBytes(parameter);

            // Command packet
            byte[] bytes = {
                0xFF,                     // Header (3 bytes)
                0xFE,
                0xFD,
                (byte)command,            // Command
                paramBytes[0],            // Parameter is 0 (ignored)
                paramBytes[1],
                paramBytes[2],
                paramBytes[3],
                0xFF                      // Footer (1 byte)
            };

            // Apply the requested delay in microseconds
            if (command == COMMANDS.TRIGGER)
            {
                stopwatch.Start();
                stopwatch.WaitForMicroseconds(triggerDelayInMicros);
            }

            // Send the packet
            SendBytes(bytes);

            if (command == COMMANDS.TRIGGER)
            {
                stopwatch.Stop();
            }

            // Context switch
            Thread.Sleep(0);
        }

        /*
         * Send a byte array to Arduino.
         */
        private void SendBytes(byte[] bytesToSend)
        {
            if (serialPortObj != null && serialPortObj.IsOpen)
            {
                serialPortObj.Write(bytesToSend, 0, bytesToSend.Length);
            }
        }

        /*
         * Event handler for transmission back from Arduino.
         */
        private void Received(object sender, SerialDataReceivedEventArgs e)
        {
            // Read one byte at a time
            int bufferSize = serialPortObj.BytesToRead;

            // Arduino always returns 5 bytes
            // Byte 0 is the command it received for confirmation.
            // Bytes 1 - 4 contain the response (to be cast to an UInt32). 
            if (bufferSize == 5)
            {
                // Read the data into a buffer
                byte[] data = new byte[bufferSize];
                serialPortObj.Read(data, 0, bufferSize);

                // Get the command
                byte command = data[0];

                // Process the answer
                switch ((COMMANDS)(command))
                {
                    case COMMANDS.RESET_COUNTER:

                        // Store the returned value (bytes 1 - 4)
                        onBoardCounter = BitConverter.ToUInt32(data, 1);

                        break;

                    case COMMANDS.TRIGGER:

                        // This command does not return anything
                        break;

                    case COMMANDS.QUERY_COUNTER:

                        // Store the returned value (bytes 1 - 4)
                        onBoardCounter = BitConverter.ToUInt32(data, 1);

                        break;

                    case COMMANDS.STOP:

                        // This command does not return anything
                        break;

                    case COMMANDS.SET_PIN:

                        // Store the returned value (bytes 1 - 4)
                        pin = BitConverter.ToUInt32(data, 1);

                        break;

                    case COMMANDS.QUERY_PIN:

                        // Store the returned value (bytes 1 - 4)
                        pin = BitConverter.ToUInt32(data, 1);

                        break;

                    case COMMANDS.SET_DURATION:

                        // Store the returned value (bytes 1 - 4)
                        triggerDuration = BitConverter.ToUInt32(data, 1);

                        break;

                    case COMMANDS.QUERY_DURATION:

                        // Store the returned value (bytes 1 - 4)
                        triggerDuration = BitConverter.ToUInt32(data, 1);

                        break;

                    default:

                        // Ignore
                        break;
                }

            }
            else
            {
                // All feedback must come in a 5-byte buffer!

                // Read the data into a buffer
                byte[] data = new byte[bufferSize];
                serialPortObj.Read(data, 0, bufferSize);

                // Turn the buffer into a String and display it on the console
                var str = System.Text.Encoding.Default.GetString(data);
                Console.WriteLine($"Invalid serial communication from Arduino: {str}");
            }
        }
    }
}
