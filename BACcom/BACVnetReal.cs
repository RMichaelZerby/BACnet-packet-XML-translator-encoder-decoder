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
    class BACVnetReal : BACVnetVar
    { //
        private byte[] stringValue;

        public BACVnetVar Decode(BACPacket cm, byte length)
        {
            stringValue = new byte[length];
            stringValue[0] = cm.getNextByte();
            for (int x = 1; x < length; x++)
            {
                stringValue[x] = cm.getNextByte();
            }

            return this;
        }

        public override BACVnetVar Decode(BACPacket cm)
        {
            byte tag = cm.getNextByte();
            int length = tag & 0x07;
            stringValue = new byte[length];
            stringValue[0] = cm.getNextByte();
            for (int x = 1; x < length; x++)
            {
                stringValue[x] = cm.getNextByte();
            }

            return this; 
        }

        public override string ToString()
        {
            String sRet;
            sRet = "<Real>";
            sRet += BitConverter.ToString(stringValue);
            sRet += "</Real>";
            return sRet;
        }


    }
}
