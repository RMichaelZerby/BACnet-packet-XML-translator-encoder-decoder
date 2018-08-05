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


namespace BACcom
{
    // page 55, 6.2 Network Layer PDU Structure
    // Network Layer Protocol Control Information
    // Bit  7: 1 indicates that the NSDU conveys a network layer message or application message
    // Bit  6: Reserved. Shall be zero.
    // Bit  5: destPresent specifier
    // Bit  4: Reserved.
    // Bit  3: Source specifier
    // Bit  2: The value of this bit corresponds to the data_expecting_reply parameter in the N-UNITDATA primitives.
    // Bits 1,0: Network priority where:
    // Followed by the APDU 

    class NPCI
    {
        private uint pciVersion = 0;
        private MTYPE messageType = MTYPE.APPICATION;
        private PRESENCES destPresent = PRESENCES.ABSENT;
        private PRESENCES srcPresent = PRESENCES.ABSENT;
        private CONFIRM expectingReply = CONFIRM.NO;
        private PRIORITY priority = PRIORITY.NORMAL;
        private AddrUnit destAddr = new AddrUnit(AddrUnit.ADDR_TYPE.DESTONATION);
        private AddrUnit srcAddr = new AddrUnit(AddrUnit.ADDR_TYPE.SOURCE);
        private byte hopCnt = 0;
        // Variable APDU follows pg 620
        PDU pdu = new PDU();
        NSDU nsdu = new NSDU();
        public List<byte> packet { get; set;}
        


        public MTYPE getMessgeType() 
        {
            return messageType;
        }

        public void getMessageType(MTYPE a)
        {
            messageType = a;
        }

        public PRESENCES getDestPresent()
        {
            return destPresent;
        }

        public void setDestPresent(PRESENCES a)
        {
            destPresent = a;
        }

        public PRESENCES getSrcPresent()
        {
            return srcPresent;
        }

        public void setSrcPresent(PRESENCES a)
        {
            srcPresent = a;
        }

        public CONFIRM getExpectingReply()
        {
            return expectingReply;
        }

        public PRIORITY getPriority()
        {
            return priority;
        }

        public void setPriority(PRIORITY a)
        {
            priority = a;
        }

        public Boolean Decode(BACPacket cm)
        {
            pciVersion = cm.getNextByte(); // PIC Version is allways 0x01

            if (pciVersion != 0x01)
                return false;

            byte picByt = cm.getNextByte();
            messageType = this.DecodeMType(picByt);
            destPresent = this.DecodeDestonation(picByt);
            srcPresent =  this.DecodeSource(picByt);
            expectingReply = this.DecodeExpectingReply(picByt);
            priority = this.DecodePriority(picByt);
            
            //IF it contains the destonation addr get it
            if (destPresent == PRESENCES.EXIST)
            {
                destAddr.Decode(cm);
            }
            
            //IF it contains the source addr get it
            if (srcPresent == PRESENCES.EXIST)
            {
                srcAddr.Decode(cm);
            }
            
            //IF it contains the destonation don't forget the hop count
            if (destPresent == PRESENCES.EXIST)
            {
                hopCnt = cm.getNextByte();
            }

            if (messageType == MTYPE.APPICATION)
                pdu = pdu.Decode(cm);
            else
                nsdu = nsdu.Decode(cm);

            return true;
        }

        public List<byte> Encode(XElement xParentElem)
        {
            packet = new List<byte>();
            XElement xElem = (XElement)xParentElem.Element("NetworkCtl");

            packet.Add(0x01); // Version 
            packet.Add(0x00); // default values
            var value = xElem.Element("MessageType").Value;
            messageType = (MTYPE)System.Enum.Parse(typeof(MTYPE), value, true);
            packet[1]= EncodeMType(messageType, packet[1]);
            value = xElem.Element("ExpectingReply").Value;
            expectingReply = (CONFIRM)System.Enum.Parse(typeof(CONFIRM), value, true);
            if (xElem.Element("Destination") != null)
            {
                packet[1] = EncodeDestonation(1, packet[1]);
                AddrUnit a = new AddrUnit(AddrUnit.ADDR_TYPE.DESTONATION);
                packet.AddRange(a.Encode(xElem.Element("Destination")));
            }
            if (xElem.Element("Source") != null)
            {
                packet[1] = EncodeSource(1, packet[1]);
                AddrUnit a = new AddrUnit(AddrUnit.ADDR_TYPE.SOURCE);
                packet.AddRange(a.Encode(xElem.Element("Destination")));
            }
            if (xElem.Element("Destination") != null)
            {
                byte hops = 0;
                if (xElem.Element("Destination").Element("HopCount") != null)
                {
                    value = xElem.Element("Destination").Element("HopCount").Value;
                    hops = Convert.ToByte(value, 16);
                }
                packet.Add(hops);
            }
            if (messageType == MTYPE.APPICATION)
            {
                packet.AddRange(pdu.Encode(xParentElem));
            }
            else
            {
                nsdu = nsdu.Encode(xParentElem);
            }

            return packet;
        }

        protected MTYPE DecodeMType(byte b)
        {
            return (MTYPE)((b & 0x80) / 0x80);
        }

        protected byte EncodeMType(MTYPE a, byte b)
        {
            return b = (byte)(((byte)a) * 0x80);
        }

        protected PRESENCES DecodeDestonation(byte b)
        {
            return (PRESENCES)((b & 0x20) / 0x20);
        }

        protected byte EncodeDestonation(byte v, byte b)
        {
            return  b |= (byte)(((byte)v) * 0x20);
        }

        protected PRESENCES DecodeSource(byte b)
        {
            return (PRESENCES)((b & 0x08) / 0x08);
        }

        protected byte EncodeSource(byte v, byte b)
        {
            return b |= (byte)(((byte)v) * 0x08);
        }

        protected CONFIRM DecodeExpectingReply(byte b)
        {
            return (CONFIRM)((b & 0x04) / 0x04);
        }

        protected void EncodeExpectingReply( CONFIRM conf, ref byte b)
        {
            if (conf == CONFIRM.YES)
              b |= 0x04;
        }


        protected PRIORITY DecodePriority(byte b)
        {
            return (PRIORITY)(b & 0x03);
        }

        
        public enum MTYPE  : byte
        {
            NETWORK = 0x01,
            APPICATION = 0x00
        }

        public String MTYPE_ToString()
        {
            switch(messageType)
            {
                case MTYPE.NETWORK:
                    return "NETWORK";
                case MTYPE.APPICATION:
                    return "APPICATION";
                default:
                    return "MTV_" + messageType.ToString();
            }
        }

        
        public enum PRESENCES : byte 
        {
            ABSENT = 0,
            EXIST = 1
        }

        public enum CONFIRM : byte
        {
            YES = 1, NO = 0
        }

        public enum PRIORITY : byte
        {
            NORMAL = 0, URGENT = 1, CRITICAL = 2, LIFESAFTY = 3
        }

        public String PRIORITY_ToString( PRIORITY p )
        {
            switch (p) 
            {
                case PRIORITY.NORMAL:
                    return "NORMAL";
                case PRIORITY.URGENT:
                    return "URGENT";
                case PRIORITY.CRITICAL:
                    return "CRITICAL";
                case PRIORITY.LIFESAFTY:
                    return "LIFESAFTY";
                default:
                    return "PRIORITY_" + ((byte)p).ToString();
            }
        }


        class AddrUnit
        {
            public enum ADDR_TYPE : byte
            {
                SOURCE = 0, DESTONATION = 1, OTHER = 3, NA = 99
            }
            ADDR_TYPE typ = ADDR_TYPE.NA;
            private PRESENCES p = PRESENCES.ABSENT;
            private UInt16 netNumber = 0;
            private byte adrLen = 0;
            // four-octet IP address followed by a two-octet UDP port number; most significant octet first
            // This address shall be referred to as a B/IP address
            // Port between 47808 - 47823 and 49152 - 65535
            // IP class D address (B/IP-M devices) 
            // All B/IP devices sharing a common IP multicast address should also share a common BACnet network number ?
            private byte[] adr = new byte[] { 0, 0, 0, 0 };
            private byte hopCnt = 0;

            public AddrUnit(ADDR_TYPE tp)
            {
                typ = tp;
            }

            public void Decode(BACPacket cm)
            {
                p = PRESENCES.EXIST;
                netNumber  = cm.getNextByte();
                netNumber *= 0x0100;
                netNumber += cm.getNextByte();
                adrLen = cm.getNextByte();
                adr = new byte[0];

                for (int x = 0; x < adrLen; x++)
                {
                   adr = adr.Concat(new byte[1]{cm.getNextByte()}).ToArray();
                }
            }

            public List<byte> Encode(XElement xElem)
            {
                List<byte> pkt = new List<byte>(8);
                var value = xElem.Element("Port").Value;
                Int16 i16 = Convert.ToInt16(value, 16);
                pkt.Add((byte)(i16 / 0x100));
                pkt.Add((byte)(i16));
                if (xElem.Element("IPAddr") != null)
                {
                    value = xElem.Element("IPAddr").Value;
                    String a = value;
                    String[] sa = a.Split('.');
                    pkt.Add((byte)sa.Length);
                    List<byte> ba1 = new List<byte>(6) ;
                    foreach (String s1 in sa)
                    {
                        pkt.Add(Convert.ToByte(s1,16));
                    }
                }
                return pkt;
            }

            public void setHopCnt(byte b)
            {
                hopCnt = b;
            }

            public byte getHopCnt()
            {
                return hopCnt;
            }

            public override string ToString()
            {
                String aout = "";

                if (p == PRESENCES.EXIST)
                {
                    switch (typ)
                    {
                        case ADDR_TYPE.SOURCE:
                            aout = "<Source> ";
                            break;
                        case ADDR_TYPE.DESTONATION:
                            aout = "<Destination> ";
                            break;
                        default:
                            aout = "<Network> ";
                            break;
                    }
                    aout = String.Concat(aout, "<Port>" + netNumber.ToString("X") + "</Port>" + " <IPAddr>");
                    int cnt = 1;
                    foreach (byte addb in adr)
                    {
                        aout += addb.ToString("X");
                        if (cnt < adr.Count())
                            aout += ".";
                    }
                    aout += "</IPAddr> ";
                    if (typ == ADDR_TYPE.DESTONATION)
                        aout += "<HopCount>" + hopCnt.ToString("X") + "</HopCount>";
                    switch (typ)
                    {
                        case ADDR_TYPE.SOURCE:
                            aout += "</Source> ";
                            break;
                        case ADDR_TYPE.DESTONATION:
                            aout += "</Destination> ";
                            break;
                        default:
                            aout += "</Network> ";
                            break;
                    }

                }
                return aout;
            }
        }

        public override string ToString()
        {
            return "<NetworkCtl> " + "<MessageType>" + MTYPE_ToString() + "</MessageType>"
                + "<ExpectingReply>" + expectingReply + "</ExpectingReply>"
                + destAddr.ToString() + srcAddr.ToString() + " </NetworkCtl> " 
                + (messageType == MTYPE.APPICATION ? pdu.ToString():nsdu.ToString());        
        }

    }
}
