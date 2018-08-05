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
    class PDU
    {
        public BACnetPDU pduType { get; set; }
        public List<byte> packet {get; set; } 
         
        public enum BACnetPDU : byte 
        {
            PDU_CONFIRMED_REQUEST = 0,
            PDU_UNCONFIRMED_SERVICE_REQUEST = 1,
            PDU_SIMPLEACK = 2,
            PDU_COMPLEXACK = 3,
            PDU_SEGMENTACK = 4,
            PDU_ERROR = 5,
            PDU_REJECT = 6,
            PDU_ABORT = 7,
        } 

        public PDU Decode(BACPacket cm)
        {
            pduType = (BACnetPDU)(cm.getByte() >> 4);
            switch (pduType)
            {
                case BACnetPDU.PDU_CONFIRMED_REQUEST:
                    pduType = BACnetPDU.PDU_CONFIRMED_REQUEST;
                    cm.SetAction(BACPacket.Action_Type.REQUEST);
                    return new PDUConfirmedService().Decode(cm);
                case BACnetPDU.PDU_UNCONFIRMED_SERVICE_REQUEST:
                    pduType = BACnetPDU.PDU_UNCONFIRMED_SERVICE_REQUEST;
                    cm.SetAction(BACPacket.Action_Type.REQUEST);
                    return new PDUUnConfirmedService().Decode(cm);
                case BACnetPDU.PDU_SIMPLEACK:
                    pduType = BACnetPDU.PDU_SEGMENTACK;
                    cm.SetAction(BACPacket.Action_Type.RESPONSE);
                    return new PDUSimpleAck().Decode(cm);
                case BACnetPDU.PDU_COMPLEXACK:
                    pduType = BACnetPDU.PDU_COMPLEXACK;
                    cm.SetAction(BACPacket.Action_Type.RESPONSE);
                    return new PDUComplexAck().Decode(cm);
                case BACnetPDU.PDU_SEGMENTACK:
                case BACnetPDU.PDU_ERROR:
                    pduType = BACnetPDU.PDU_ERROR;
                    cm.SetAction(BACPacket.Action_Type.RESPONSE);
                    return new PDUError().Decode(cm);
                case BACnetPDU.PDU_REJECT:
                    pduType = BACnetPDU.PDU_REJECT;
                    cm.SetAction(BACPacket.Action_Type.RESPONSE);
                    return new PDUReject().Decode(cm);
                case BACnetPDU.PDU_ABORT:
                    return this;


            }
            return this;
        }

        public virtual List<byte> Encode(XElement xParentElem)
        {
            packet = new List<byte>(0);
            PDU pdu = this;
            XElement xElem = (XElement)xParentElem.Elements().First(x => ((XElement)x).Name.ToString().StartsWith("PDU") == true);
            pdu = (PDU)System.Activator.CreateInstance(Type.GetType(this.GetType().Namespace.ToString() + "." + xElem.Name.ToString()));
            //If not the base class 
            if ( pdu.GetType().IsSubclassOf(this.GetType()) )
                packet.AddRange(pdu.Encode(xElem));
            // Working here
            return packet;
        }


        //public BACnetPDU getPduType()
        //{
        //    return pduType;
        //}


        public override string ToString()
        {
            String sRet;
            sRet = " <" + pduType + "> code not implemented ";
            sRet += " </" + pduType + "> "; ;
            return sRet;
        }




    }
}
