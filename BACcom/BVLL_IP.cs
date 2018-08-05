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
using System.Xml;
using System.Linq;
using System.Xml.Linq;

// Templet for the BACnet virtual link control 
//
// BACnet Virtual Link Control (BVLC) functions required to support BACnet/IP directed
//    and broadcast messages.

namespace BACcom
{
    class BVLL_IP
    {
        protected UInt16 length = 0;
        protected BVLL_IP_Fnct function = 0;
        public byte BVLL_IP_TYPE = 0x81;
        protected String inner = "";

        public List<byte> packet;

        public enum BVLL_IP_Fnct : byte
        {
            BVLC_RESULT = 0x00,
            WRITE_BROADCAST_DISTRIBUTUION_TABLE = 0x01,
            READ_BROADCAST_DISTRIBUTUION_TABLE = 0x02,
            READ_BROADCAST_DISTRIBUTUION_ACT = 0x03,
            FOWARD_NPDU = 0x04,
            REGISTER_FOREIGN_DEVICE = 0x05,
            READ_FOREIGN_DEVICE_TABLE = 0x06,
            READ_FOREIGN_DEVICE_TABLE_ACK = 0x07,
            DELETE_FOREIGN_DEVICE_TABLE_ENTRY = 0x08,
            DISTRIBUTE_BROADCAST_TO_NETWORK = 0x09,

            ORIGINAL_UNICAST_NPDU = 0x0A,
            ORIGINAL_BROADCAST_NPDU = 0x0B,

            SECURE_BVLL = 0x0C,

        };

        public String BVLL_IP_Fnct_ToString()
        {

            switch( function )
            {
                case BVLL_IP_Fnct.ORIGINAL_UNICAST_NPDU:
                    return "ORIGINAL_UNICAST_NPDU";
                case BVLL_IP_Fnct.ORIGINAL_BROADCAST_NPDU:
                    return "ORIGINAL_BROADCAST_NPDU";

                case BVLL_IP_Fnct.BVLC_RESULT:
                    return  "BVLC_RESULT";
                case BVLL_IP_Fnct.WRITE_BROADCAST_DISTRIBUTUION_TABLE:
                    return  "WRITE_BROADCAST_DISTRIBUTUION_TABLE";
                case BVLL_IP_Fnct.READ_BROADCAST_DISTRIBUTUION_TABLE:
                    return  "READ_BROADCAST_DISTRIBUTUION_TABLE";
                case BVLL_IP_Fnct.READ_BROADCAST_DISTRIBUTUION_ACT:
                    return  "READ_BROADCAST_DISTRIBUTUION_ACT";
                case BVLL_IP_Fnct.FOWARD_NPDU:
                    return  "FOWARD_NPDU";
                case BVLL_IP_Fnct.REGISTER_FOREIGN_DEVICE:
                    return  "REGISTER_FOREIGN_DEVICE";
                case BVLL_IP_Fnct.READ_FOREIGN_DEVICE_TABLE:
                    return  "READ_FOREIGN_DEVICE_TABLE";
                case BVLL_IP_Fnct.READ_FOREIGN_DEVICE_TABLE_ACK:
                    return  "READ_FOREIGN_DEVICE_TABLE_ACK";
                case BVLL_IP_Fnct.DELETE_FOREIGN_DEVICE_TABLE_ENTRY:
                    return  "DELETE_FOREIGN_DEVICE_TABLE_ENTRY";
                case BVLL_IP_Fnct.DISTRIBUTE_BROADCAST_TO_NETWORK:
                    return  "DISTRIBUTE_BROADCAST_TO_NETWORK";
                case BVLL_IP_Fnct.SECURE_BVLL:
                    return  "SECURE_BVLL";
                default:
                    return "IDV_" + function.ToString();
            }
        }


        public Boolean Decode(BACPacket cm)
        {
            if (0 == cm.getNextByte().CompareTo(0x81))
            {
                function = (BVLL_IP_Fnct)cm.getNextByte();
                length = cm.getNextByte();
                length = (UInt16)(length << 4);
                length += cm.getNextByte();
                cm.setLength(length);

                switch (function)
                {
                    case BVLL_IP_Fnct.ORIGINAL_UNICAST_NPDU:
                        NPCI pic = new NPCI();
                        pic.Decode(cm);
                        inner = pic.ToString();
                        System.Console.WriteLine(ToString()); 
                        break;
                    case BVLL_IP_Fnct.ORIGINAL_BROADCAST_NPDU:
                        NPCI pic1 = new NPCI();
                        pic1.Decode(cm);
                        inner = pic1.ToString();
                        System.Console.WriteLine(ToString()); 
                        break;
                    default:
                        break;
                }
                return true;
            }

            return false;
        }

        public List<byte> Encode(XElement xElem)
        {
            packet = new List<byte>(0);
            packet.Add(0x81);
            var field = xElem.Element("Function").Value;
            function = (BVLL_IP_Fnct)System.Enum.Parse(typeof(BVLL_IP_Fnct), field, true);
            packet.Add((byte)function);
            packet.Add(0); // Lenght bytes
            packet.Add(0); // Lenght bytes
            switch (function)
            {
                case BVLL_IP_Fnct.ORIGINAL_UNICAST_NPDU:
                    NPCI pic = new NPCI();
                    packet.AddRange(pic.Encode(xElem));
                    break;
                case BVLL_IP_Fnct.ORIGINAL_BROADCAST_NPDU:
                    //NPCI pic1 = new NPCI();
                    //pic1.Decode(cm);
                    break;
                default:
                    break;
            }
            packet[2] = (byte) (packet.Count >> 16);
            packet[3] = (byte) packet.Count;	
 
            packet.Add(0); // Lenght bytes
            return packet;
        }

        public override string ToString( )
        {
            return "<BVLL_IP> " + "<Function>" + BVLL_IP_Fnct_ToString() + "</Function>" + inner + " </BVLL_IP>";
        }

    }
}
