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

namespace BACcom
{
    using ConfirmedServiceChoice = BACnetConfirmedServiceChoice;
     
    class PDUComplexAck : PDU
    {
        // pg 619 
        private Boolean segMessage = false;  // 20.1.5.1 segmented-message
        private Boolean moreFollows = false; // 20.1.5.2 more-follows 
        private byte orgInvokeID = 0; // 20.1.5.3 original-invokeID
        private byte sequenceNumber = 0; // 20.1.5.4 sequence-number
        private byte proposedWindowSize = 0; // 20.1.5.5 proposed-window-size
        private ConfirmedServiceChoice serviceACKchoice = 0; // 20.1.5.6 service-ACK-choice
        // What follows is the variable type matching the serviceACKchoice - Variable Encoding per 20.2. - 20.1.5.7 service-ACK
        APDUService apduService = new APDUService();



        new public PDU Decode(BACPacket cm)
        {
            System.Console.Write("Decoding PDUComplexAck");
            byte a = cm.getNextByte();
            segMessage = DecodeSegMessage(a);
            moreFollows = DecodeMoreFollows(a);
            orgInvokeID = cm.getNextByte();
            // If the optional parameter are present
            if (segMessage) 
            {
                sequenceNumber = cm.getNextByte();
                proposedWindowSize = cm.getNextByte();
            }

            serviceACKchoice = (ConfirmedServiceChoice)cm.getNextByte();

            switch (serviceACKchoice)
            {
                case ConfirmedServiceChoice.READPROPERTY:
                    apduService = new APDUReadProperty();
                    apduService.Decode(cm);

                    break;

            }

            return this;
        }

        private Boolean DecodeSegMessage(byte s) 
        {
            return (s & 0x08) == 0x08 ? true: false;
        }

        Boolean DecodeMoreFollows(byte s)
        {
            return (s & 0x08) == 0x04 ? true : false;
        }

        public override string ToString()
        {
            String sRet;
            sRet = " <" + this.GetType().Name + "> ";
            sRet += " <InvokeID>" + orgInvokeID.ToString() + "</InvokeID> ";
            sRet += " <serviceACKchoice>" + serviceACKchoice + "</serviceACKchoice>";
            sRet += apduService.ToString();
            sRet += " </" + this.GetType().Name + "> ";
            return sRet;
        }

//      Pg 619, 20.1.5 BACnet-ComplexACK-PDU
//   BACnet-ComplexACK-PDU ::= SEQUENCE {
//      pdu-type [0] Unsigned (0..15), -- 3 for this PDU type
//      segmented-message [1] BOOLEAN,
//      more-follows [2] BOOLEAN,
//      reserved [3] Unsigned (0..3), -- must be set to zero
//      original-invokeID [4] Unsigned (0..255),
//      sequence-number [5] Unsigned (0..255) OPTIONAL, --only if segment
//      proposed-window-size [6] Unsigned (1..127) OPTIONAL, -- only if segment
//      service-ACK-choice [7] BACnetConfirmedServiceChoice,
//      service-ACK [8] BACnet-Confirmed-Service-ACK
//-- Context specific tags 0..8 are NOT used in header encoding


    }
}
