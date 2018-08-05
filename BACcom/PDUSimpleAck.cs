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
    class PDUSimpleAck : PDU
    {

        new public PDU Decode(BACPacket cm)
        {
            cm.getNextByte();

            return this;
        }

//    Pg 618, 20.1.4 BACnet-SimpleACK-PDU
//     BACnet-SimpleACK-PDU ::= SEQUENCE {
//        pdu-type [0] Unsigned (0..15), -- 2 for this PDU type
//        reserved [1] Unsigned (0..15), -- must be set to zero
//        original-invokeID [2] Unsigned (0..255),
//        service-ACK-choice [3] BACnetConfirmedServiceChoice
//        -- Context specific tags 0..3 are NOT used in header encoding
//}

        public override string ToString()
        {
            String sRet = "<" + this.GetType().Name + "> ";
            sRet += " Not implemented ";
            sRet += "</" + this.GetType().Name + "> ";
            return sRet;
        }

    }
}
