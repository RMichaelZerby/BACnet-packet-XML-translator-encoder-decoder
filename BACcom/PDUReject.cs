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

    class PDUReject : PDU
    {
        private byte invokerId;
        //BACNET_ServiceChoice_Error serviceChoseError;
        private BACNET_Reject_Reason rej_reason;

        // PAGE 
        new public PDU Decode(BACPacket cm)
        {
            cm.getNextByte();
            invokerId = cm.getNextByte();
            rej_reason = (BACNET_Reject_Reason)cm.getNextByte();
            return this;
        }


        //    BACnet-Reject-PDU ::= SEQUENCE {
        //    pdu-type [0] Unsigned (0..15), -- 6 for this PDU type
        //    reserved [1] Unsigned (0..15), -- must be set to zero
        //    original-invokeID [2] Unsigned (0..255),
        //    reject reason [3] BACnetRejectReason
        //    -- Context specific tags 0..3 are NOT used in header encoding


        //  X'50' PDU Type=5 (BACnet-Error-PDU)
        //  X'58' Original Invoke ID=88
        //  X'0B' Error Choice=l 1 (DeleteObject)
        //  X'91' Application Tag 9 (Enumerated, L=l) (Error Class)
        //  X'01' 1 (OBJECT)
        //  X'91' Application Tag 9 (Enumerated, L=l) (Error Code)
        //  X'17' 23 (OBJECT_DELETION_NOT_PERMITTED)

        public override string ToString()
        {
            string rStr = " <" + this.GetType().Name + "> ";
            rStr += "<Type>" + base.pduType + "</Type>";
            rStr += "<InvokedId>" + invokerId + "</InvokedId>";
            rStr += "<Reason>" + rej_reason + "</Reason>";
            return rStr += "</" + this.GetType().Name + "> ";
        }

    }


}


