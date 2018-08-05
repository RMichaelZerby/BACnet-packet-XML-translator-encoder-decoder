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
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace BACcom
{
    class PDUUnConfirmedService : PDU
    {

        private BACnetUnconfirmedServiceChoice srvChoise;
        List<BACVnetVar> bv = new List<BACVnetVar>();


        public enum BACnetUnconfirmedServiceChoice : byte
        {
            I_AM = 0,
            I_HAVE = 1,
            UNCONFIRMEDCOVNOTIFICATION = 2,
            UNCONFIRMEDEVENTNOTIFICATION = 3,
            UNCONFIRMEDPRIVATETRANSFER = 4,
            UNCONFIRMEDTEXTMESSAGE = 5,
            TIMESYNCHRONIZATION = 6,
            WHO_HAS = 7,
            WHO_IS = 8,
            UTCTIMESYNCHRONIZATION = 9,
            WRITEGROUP = 10
        }



        new public PDU Decode(BACPacket cm)
        {
            cm.getNextByte();
            srvChoise = (BACnetUnconfirmedServiceChoice)cm.getNextByte();

            switch (srvChoise) 
            {
                case BACnetUnconfirmedServiceChoice.I_AM:
                    //a = new I_AM_Srv_Struct();
                    while (cm.hasMore() == true)
                    {
                        bv.Add(new BACVnetVar().Decode(cm));
                    }
                    break;
                case BACnetUnconfirmedServiceChoice.I_HAVE:
                    while (cm.hasMore() == true)
                    {
                        bv.Add( new BACVnetVar().Decode(cm));
                    }
                    break;
                case BACnetUnconfirmedServiceChoice.WHO_HAS:
                    while (cm.hasMore() == true)
                    {
                        bv.Add( new BACVnetVar().Decode(cm));
                    }
                    break;
                case BACnetUnconfirmedServiceChoice.WHO_IS:
                    while (cm.hasMore() == true)
                    {
                        bv.Add(new BACVnetVar().Decode(cm, BACVnetVar.CONTEXT_TAG.UINT));
                    }
                    break;
                case BACnetUnconfirmedServiceChoice.TIMESYNCHRONIZATION:
                    while (cm.hasMore() == true)
                    {
                        bv.Add( new BACVnetVar().Decode(cm));
                    }
                    break;
                default:
                    while (cm.hasMore() == true)
                    {
                        bv.Add( new BACVnetVar().Decode(cm));
                    }
                    break;
            }

            return this;
        }
        // See page 639 for structures
        // Unconfirmed Remote Device Management Services PG 1514

        public override List<byte> Encode(XElement xElem)
        {
            packet = new List<byte>(0);
            var field = xElem.Element("Service").Value;
            srvChoise = (BACnetUnconfirmedServiceChoice)System.Enum.Parse(typeof(BACnetUnconfirmedServiceChoice), field, true);
            packet.Add(((byte)BACnetPDU.PDU_UNCONFIRMED_SERVICE_REQUEST)<<4);
            packet.Add((byte)srvChoise);

            switch (srvChoise)
            {
                case BACnetUnconfirmedServiceChoice.I_AM:
                    break;
                case BACnetUnconfirmedServiceChoice.I_HAVE:
                    break;
                case BACnetUnconfirmedServiceChoice.WHO_HAS:
                    break;
                case BACnetUnconfirmedServiceChoice.WHO_IS:
                    break;
                case BACnetUnconfirmedServiceChoice.TIMESYNCHRONIZATION:
                    break;
                default:
                    break;
            }

            return packet;
        }


        public override string ToString()
        {
            String sRet  = "<" + this.GetType().Name + "> ";
            sRet += "<Service>" + srvChoise + "</Service>";
                foreach(BACVnetVar b in bv )
                {
                    sRet += b.ToString();
                }
                sRet += "</" + this.GetType().Name + "> ";
            return sRet;
        }

    }



    
}
