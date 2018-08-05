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
    class NSDU
    {
        internal Network_Layer_Message nlm = (Network_Layer_Message)0xFF;

        public NSDU Encode(XElement xElem)
        {
            return this;
        }

        public virtual NSDU Decode(BACPacket cm)
        {
            nlm = (Network_Layer_Message)cm.getByte();
            switch (nlm)
            {
                case Network_Layer_Message.WHO_IS_ROUTER_TO_NETWORK:
                    return new NSDU_Who_Is_Router_To_Network().Decode(cm);
                case Network_Layer_Message.I_AM_ROUTER_TO_NETWORK:
                    return new NSDU_I_Am_Router_To_Network().Decode(cm);
                case Network_Layer_Message.I_COULD_BE_ROUTER_TO_NETWORK:
                    return new NSDU_I_Could_be_Router_To_Network().Decode(cm);
                case Network_Layer_Message.REJECT_MESSAGE_TO_NETWORK:
                    return new NSDU_Reject_Message_To_Network().Decode(cm);
                case Network_Layer_Message.ROUTER_BUSY_TO_NETWORK:
                    return new NSDU_Router_Busy_To_Network().Decode(cm);
                case Network_Layer_Message.INITIALIZE_ROUTING_TABLE:
                    return new NSDU_Initialize_Routing_Table().Decode(cm);
                case Network_Layer_Message.INITIALIZE_ROUTING_TABLE_ACK:
                    return new NSDU_Initialize_Routing_Table_Ack().Decode(cm);
                case Network_Layer_Message.ESTABLISH_CONNECTION_TO_NETWORK:
                    break;
                case Network_Layer_Message.DISCONNECT_CONNECTION_TO_NETWORK:
                    break;
                case Network_Layer_Message.CHALLENGE_REQUEST:
                    break;
                case Network_Layer_Message.SECURITY_PAYLOAD:
                    break;
                case Network_Layer_Message.SECURITY_RESPONSE:
                    break;
                case Network_Layer_Message.REQUEST_KEY_UPDATE:
                    break;
                case Network_Layer_Message.UPDATE_KEY_SET:
                    break;
                case Network_Layer_Message.UPDATE_DISTRIBUTION_KEY:
                    break;
                case Network_Layer_Message.REQUEST_MASTER_KEY:
                    break;
                case Network_Layer_Message.SET_MASTER_KEY:
                    break;
                case Network_Layer_Message.WHAT_IS_NETWORK_NUMBER:
                    break;
                case Network_Layer_Message.NETWORK_NUMBER_IS:
                    break;
            }
            return this;
        }

        public enum Network_Layer_Message : int 
        {
            WHO_IS_ROUTER_TO_NETWORK = 0x00,
            I_AM_ROUTER_TO_NETWORK = 0x01,
            I_COULD_BE_ROUTER_TO_NETWORK = 0x02,
            REJECT_MESSAGE_TO_NETWORK = 0x03,
            ROUTER_BUSY_TO_NETWORK = 0x04,
            ROUTER_AVAILABLE_TO_NETWORK = 0x05,
            INITIALIZE_ROUTING_TABLE = 0x06,
            INITIALIZE_ROUTING_TABLE_ACK = 0x07,
            ESTABLISH_CONNECTION_TO_NETWORK = 0x08,
            DISCONNECT_CONNECTION_TO_NETWORK = 0x09,
            CHALLENGE_REQUEST = 0x0A,
            SECURITY_PAYLOAD = 0x0B,
            SECURITY_RESPONSE = 0x0C,
            REQUEST_KEY_UPDATE = 0x0D,
            UPDATE_KEY_SET = 0x0E,
            UPDATE_DISTRIBUTION_KEY = 0x0F,
            REQUEST_MASTER_KEY = 0x10,
            SET_MASTER_KEY = 0x011,
            WHAT_IS_NETWORK_NUMBER = 0x12,
            NETWORK_NUMBER_IS = 0x13
        }

        public override string  ToString()
        {
            return "<NSDU_" + nlm + "> Code Not implemented </NSDU_" + nlm + "> ";
        }
    }
}
