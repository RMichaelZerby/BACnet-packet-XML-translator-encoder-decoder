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
    class BACVnetBoolean : BACVnetVar
    { //
        private Boolean booleanValue;

        public override BACVnetVar Decode(BACPacket cm)
        {
            booleanValue = (cm.getNextByte() & 0x01) == 0x01? true : false ;
            return this;
        }

        public Boolean getValue()
        {
            return booleanValue;
        }

        public void setValue(Boolean v)
        {
            booleanValue = v;
        }

        public override string ToString()
        {
            return "<Boolean>" + booleanValue.ToString() + "<Boolean/>" ;
        }



    }
}
