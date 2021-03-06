﻿/*
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
    class BACVnetInt : BACVnetVar
    { //
        private byte[] numericValue;


        public BACVnetVar Decode(BACPacket cm, int length)
        {
            // This is not right and needs to be removed later
            if (length == 0) length = 1;
            numericValue = new byte[length];
            numericValue[0] = cm.getNextByte();
            for (int x = 1; x < length; x++)
            {
                numericValue[x] = cm.getNextByte();
            }

            return this;
        }

        public String getValue()
        {
            return BitConverter.ToString(numericValue);
        }

        public void setValue(String v)
        {
            numericValue = System.Text.Encoding.ASCII.GetBytes(v);
        }

        public override string ToString()
        {
            return "<Integer>" + BitConverter.ToString(numericValue).Replace("-", "") + "</Integer>";
        }


    }
}
