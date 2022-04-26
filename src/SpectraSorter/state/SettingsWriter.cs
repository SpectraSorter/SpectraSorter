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
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace spectra.state
{
    public static class SettingsWriter
    {
        public static void Save(string fileName, bool asXML=true)
        {
            // Take a snapshot of the Settings
            SettingsSnapshot snapshot = new SettingsSnapshot();
            snapshot.Take();

            // Open the file stream
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);

            if (asXML)
            {
                // Serialize as XML
                System.Xml.Serialization.XmlSerializer formatter =
                    new System.Xml.Serialization.XmlSerializer(snapshot.GetType());
                formatter.Serialize(stream, snapshot);
            }
            else 
            {
                // Serialize as binary
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, snapshot);
            }

            // Close the stream
            stream.Close();
        }
    }
}
