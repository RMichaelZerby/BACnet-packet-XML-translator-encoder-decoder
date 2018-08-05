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
    class NSDU_Router_Busy_To_Network : NSDU
    {
        List<networkNumberID> nNumbers = new List<networkNumberID>();

        public override NSDU Decode(BACPacket cm)
        {
            nlm = (Network_Layer_Message)cm.getNextByte();
            while (cm.hasMore() == true)
            {
                nNumbers.Add( new networkNumberID().Decode(cm));
            }
            return this;
        }

        class networkNumberID
        {
            byte[] networkNumber = new byte[0];

            public networkNumberID Decode(BACPacket cm)
            {
                networkNumber = new byte[2];
                for (int x = 0; x < 2; x++)
                {
                    networkNumber[x] = cm.getNextByte();
                }
                return this;
            }

            public override string ToString()
            {
                return "<Network>" + BitConverter.ToString(networkNumber).Replace("-", "") + "</Network>"; ;
            }
        }


        public override string  ToString()
        {
            String sRet = "<NSDU_" + nlm + ">";
            foreach (networkNumberID n in nNumbers)
                sRet += n.ToString();
            return sRet += "</NSDU_" + nlm + ">";
        }
    }
}
