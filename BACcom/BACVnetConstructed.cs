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
    class BACVnetConstructed : BACVnetVar
    { //

        List<BACVnetVar> VarArr = new List<BACVnetVar>();

        public void AddVar(BACVnetVar v)
        {
            VarArr.Add(v);
        }

        public BACVnetVar Decode()
        {
            return this;
        }

        public override string ToString()
        {
            String sRet = "<Constructed> ";
            foreach (BACVnetVar bv in VarArr)
            {
                sRet += bv.ToString();
            }
            sRet += "</Constructed>";
            return sRet;
        }


    }
}
