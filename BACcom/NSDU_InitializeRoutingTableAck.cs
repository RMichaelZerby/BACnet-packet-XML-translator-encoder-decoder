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
    class NSDU_Initialize_Routing_Table_Ack : NSDU
    {
        private byte Ports;
        private RoutingData[] rd;

        public override NSDU Decode(BACPacket cm)
        {
            nlm = (Network_Layer_Message)cm.getNextByte();
            Ports = cm.getNextByte();
            rd = new RoutingData[Ports];
            for (int x = 0; x < Ports; x++)
            {
                rd[x] = new RoutingData().Decoder(cm);
            }
            return this;
        }

        private class RoutingData
        {
            byte[] ConnectedDNET = new byte[2];
            byte   PortId; 
            byte   PortInfLng;
            byte[] PortInf = new byte[0];

            public RoutingData Decoder(BACPacket cm)
            {
                for (int x = 0; x < 2; x++)
                {
                    ConnectedDNET[x] = cm.getNextByte(); 
                }
                PortId = cm.getNextByte();
                PortInfLng = cm.getNextByte();
                PortInf = new byte[PortInfLng];
                for (int x = 0; x < PortInfLng; x++)
                {
                    PortInf[x] = cm.getNextByte();
                }
                return this;
            }

            public override string ToString()
            {
                String sRet = "<Route> <ConnectedDNET>";
                sRet += BitConverter.ToString(ConnectedDNET).Replace("-", "") + "</ConnectedDNET> ";
                sRet += " <PortId>" + PortId.ToString("X") + "</PortId> ";
                sRet += "<PortInfo>" + BitConverter.ToString(ConnectedDNET).Replace("-", "") + "</PortInfo> ";
                return sRet += " </Route>";
            }
        }

        public override string  ToString()
        {
            String sRet = "<NSDU_" + nlm + ">";
                foreach(RoutingData rdv in rd)
                {
                    sRet += rdv.ToString();
                }
                return sRet += "</NSDU_" + nlm + ">";
        }
    }
}
