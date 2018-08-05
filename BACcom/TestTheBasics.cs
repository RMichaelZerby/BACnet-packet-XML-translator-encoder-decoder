/*
* Copyright 2012-2018 the original author or authors.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/


using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

using System.Diagnostics;
using System.Text.RegularExpressions;

// Note: I wrote this several years ago with the free version of Microsoft Studio and the project I was working on never materialized 
// This is more of a demonstration than a test
// Better unit testing is a must

namespace BACcom
{
    class TestTheBasics
    {
            byte[] s1 = new byte[] { 0x81, 0x0B, 0x00, 0x0C, 0x01, 0x20, 0xFF, 0xFF, 0x00, 0xFF, 0x10, 0x08, 0x00 };
            byte[] s2 = new byte[] { 0x81, 0x0A, 0x00, 0x15, 0x01, 0x00, 0x10, 0x00, 0xC4, 0x02, 0x00, 0x00, 0x64, 0x22, 0x05, 0xC4, 0x91, 0x03, 0x22, 0x01, 0x4F, 0x00 };

            byte[] s3 = new byte[] { 0x81, 0x0A, 0x00, 0x1B, 0x01, 0x24, 0x20, 0x4A, 0x06, 0x4A, 0x20, 0x65, 0x00, 0x00, 0x00, 0xFF, 0x02, 0x04, 0x01, 0x0C, 0x0C, 0x02, 0x00, 0x00, 0x65, 0x19, 0x4C, 0x00 };
            byte[] s4 = new byte[] { 0x81, 0x0A, 0x00, 0x2A, 0x01, 0x08, 0x20, 0x4A, 0x06, 0x4A, 0x20, 0x65, 0x00, 0x00, 0x00, 0x30, 0x01, 0x0C, 0x0C, 0x02, 0x00, 0x00, 0x65, 0x19, 0x4C, 0x3E, 0xC4, 0x02, 0x00, 0x00, 0x65, 0xC4, 0x01, 0x00, 0x00, 0x15, 0xC4, 0x01, 0x00, 0x00, 0x29, 0x3F, 0x00 };

            byte[] s5 = new byte[] { 0x81, 0x0B, 0x00, 0x0C, 0x01, 0xA0, 0xFF, 0xFF, 0x00, 0xFF, 0x06, 0x00, 0x00 };
            byte[] s6 = new byte[] { 0x81, 0x0B, 0x00, 0x10, 0x01, 0xA0, 0xFF, 0xFF, 0x00, 0x0E, 0x07, 0x01, 0x20, 0x4A, 0x01, 0x00, 0x00 }; 


            BACPacket cm1;
            BACPacket cm2;

        public static void Main()
        {

            System.Console.Write("Press the enter key to Start");
            Console.ReadLine();

            TestTheBasics test = new TestTheBasics();

            // XML to Packets
            test.TranslateToBACNetPacket();

            // Packets to XML
            test.TranslatePacketsToXMLBodies();

            // Suspend the screen.
            System.Console.Write("Press the enter key to Exit");
            Console.ReadLine();

        }

        // Take Raw BACNet Packets and translate into XML packets
        private void TranslatePacketsToXMLBodies()
        {
            BVLL_IP bvll = new BVLL_IP();
            System.Console.Write("Packet to XML");

            string line;

            System.IO.StreamReader file = new System.IO.StreamReader("DataFiles\\BACPackets.txt");
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);
                System.Diagnostics.Debug.WriteLine(line);
                try
                {
                    bvll.Decode(new BACPacket(StringToByteAry(line)));
                    System.Console.WriteLine(bvll.ToString());
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("{0} Exception caught.", e);
                }
            }

            file.Close();
        }

        void TestThrBasics()
        {
            cm1 = new BACPacket(s5);
            cm2 = new BACPacket(s6);
        }

        // Translate BACNet XML Data into raw BACNet Packets
        void TranslateToBACNetPacket()
        {

            System.Console.Write("XML to Packet");
            XDocument xdo = XDocument.Load("DataFiles\\BACXMLFile.xml");

            var fields = xdo.Root.Element("BVLL_IP").Element("Function").Value;
            XElement xBVLL_IP = xdo.Root.Element("BVLL_IP");
            XElement xNetworkCtl = xdo.Root.Element("BVLL_IP").Element("NetworkCtl");
            XElement xPDUUnConfirmedService = xdo.Root.Element("BVLL_IP").Element("PDUUnConfirmedService");

            BVLL_IP bip = new BVLL_IP();
            bip.Encode(xBVLL_IP);

            System.Console.WriteLine(xdo.ToString());
            String packetFromXML = BitConverter.ToString(bip.packet.ToArray()).Replace("-", "");
            byte[] packAsBytes = bip.packet.ToArray();
            System.Console.WriteLine(packetFromXML);

            if (!packetFromXML.Contains("810A00120120204A064A2065000000FF100800"))
            {
                System.Console.WriteLine("Failed to correctly decode the XML");
                throw new System.ArgumentException("Failed to correctly decode the XML");
            }

            bip.Decode(new BACPacket(packAsBytes));
            System.Console.WriteLine(bip.ToString());

        }

        private byte[] StringToByteAry(String s)
        {
            byte[] b = new byte[s.Length / 2];
            for (int x = 0; x < s.Length / 2; x++)
            {
                String s1 = s.Substring(x * 2, 2);
                b[x] = Convert.ToByte(s1, 16);
            }
            return b;
        }

    }
}
