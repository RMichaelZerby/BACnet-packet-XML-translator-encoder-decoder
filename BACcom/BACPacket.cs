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
    public class BACPacket
    {
        private byte[] comPacket;
        private int idx1 = 0;
        private int length = 0;
        private Action_Type action;

        public BACPacket(byte[] c)
        {
            comPacket = c;
            idx1 = 0;
        }

        public BACPacket(byte[] c, int x)
        {
            comPacket = c;
            idx1 = x;
        }

        public byte[] getPacket()
        {
            return comPacket;
        }

        public Action_Type GetAction()
        {
            return action;
        }

        public void SetAction(Action_Type a)
        {
            action = a;
        }

        public int getIndex1()
        {
            return idx1;
        }

        public void setIndex1(int i)
        {
            idx1 = i;
        }

        public int getLength()
        {
            return length;
        }

        public void setLength(int l)
        {
            length = l;
        }

        public Boolean hasMore()
        {
            return idx1 < length;
        }

        public byte getNextByte()
        {
            return comPacket[idx1++];
        }

        public byte getByte()
        {
            return comPacket[idx1];
        }

        public enum Action_Type : byte
        {
            REQUEST = 1,
            RESPONSE = 2
        }

    }
}
