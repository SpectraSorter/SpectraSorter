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
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace spectra.state
{
    public static class SettingsReader
    {
        public static bool Load(string fileName, bool asXML=true)
        {
            // Keep track of success
            bool success = true;

            // Object to be deserialized
            SettingsSnapshot snapshot = new SettingsSnapshot();

            // Open file stream
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            if (asXML == true)
            {
                // Use an XML reader
                XmlReader reader = XmlReader.Create(stream);

                // Deserialize from XML
                try
                {
                    System.Xml.Serialization.XmlSerializer formatter =
                        new System.Xml.Serialization.XmlSerializer(snapshot.GetType());
                    snapshot = (SettingsSnapshot)formatter.Deserialize(reader);
                }
                catch (Exception)
                {
                    success = false;

                }
                finally
                {
                    // Dispose of the XML reader
                    reader.Dispose();
                }
            }
            else
            {
                // Deserialize from binary
                IFormatter formatter = new BinaryFormatter();
                try
                {
                    snapshot = (SettingsSnapshot)formatter.Deserialize(stream);
                } catch (Exception)
                {
                    success = false;
                }
                
            }

            // Close file stream
            stream.Close();

            // Update the Settings
            if (success == true)
            {
                snapshot.Apply();
            }

            return success;
        }
    }
}
