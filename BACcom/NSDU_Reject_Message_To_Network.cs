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
    class NSDU_Reject_Message_To_Network : NSDU
    {
        private ERROR_MSG eMsg = (ERROR_MSG)0xFF; 
        private byte[] networkNumber = new byte[0];

        public override NSDU Decode(BACPacket cm)
        {
            nlm = (Network_Layer_Message)cm.getNextByte();
            eMsg = (ERROR_MSG)cm.getNextByte();
            if (cm.hasMore() == true)
            {
                networkNumber = new byte[2];
                for (int x = 0; x < 2; x++)
                {
                    networkNumber[x] = cm.getNextByte(); 
                }
            }
            return this;
        }

        public enum ERROR_MSG : byte
        {
            OTHER_ERROR = 0,
            NOT_DIRECTLY_CONNECTED = 1,
            ROUTER_IS_BUSY = 2,
            UNKNOWN_NETWORK_LAYER = 3,
            MESSAGE_TOO_LONG = 4,
            SECURITY_ERROR = 5,
            ADDRESSING_ERROR = 6
        }


        public override string  ToString()
        {
            String sRet = "<NSDU_" + nlm + ">";
            sRet += "<ERROR>" + eMsg + "</ERROR>";
            if (networkNumber.Length > 0)
                sRet += "<Network>" + BitConverter.ToString(networkNumber).Replace("-", "") + "</Network>";
            return sRet += "</NSDU_" + nlm + ">";
        }
    }
}
