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
    class APDUReadProperty : APDUService
    { // ReadProperty-Request; 

        public APDUReadProperty() { }

        private Boolean MissReqPar = false;
        BACVnetObjectIdentifier ObjId = null;
        BACVnetVar PropId = null;
        BACVnetVar AnyVar = null;

        public override APDUService Decode(BACPacket cm) 
        {
            if (cm.hasMore() == true)
                ObjId = (BACVnetObjectIdentifier) new BACVnetObjectIdentifier().Decode(cm, BACVnetVar.CONTEXT_TAG.OBJECT);
            else
            {
                MissReqPar = true;
                return this;
            }
            if (cm.hasMore() == true)
                PropId = new BACVnetPropertyId().Decode(cm, BACVnetVar.CONTEXT_TAG.ENUM);
            else
            {
                MissReqPar = true;
                return this;
            }
            
            // Index of the desired property to read (if requesting the data)
            // If omitted with an array value; returns the entire array list 

            if (cm.hasMore() == true)
                if (cm.GetAction() == BACPacket.Action_Type.REQUEST)
                {
                    switch(ObjId.getObjectType())
                    {
                        case BACVnetObjectIdentifier.BACnetObjectType.ANALOG_INPUT: 
                        case BACVnetObjectIdentifier.BACnetObjectType.ANALOG_OUTPUT:
                        case BACVnetObjectIdentifier.BACnetObjectType.ANALOG_VALUE:
                            AnyVar = new BACVnetVar().Decode(cm,BACVnetVar.CONTEXT_TAG.REAL);
                            break;
                        case BACVnetObjectIdentifier.BACnetObjectType.BINARY_INPUT:
                        case BACVnetObjectIdentifier.BACnetObjectType.BINARY_OUTPUT:
                        case BACVnetObjectIdentifier.BACnetObjectType.BINARY_VALUE:
                            AnyVar = new BACVnetVar().Decode(cm, BACVnetVar.CONTEXT_TAG.BSTRING);
                            break;
                        default:
                            break;
                    }
                }
                else
                    AnyVar = new BACVnetVar().Decode(cm);

            return this;
        }

        public override string ToString()
        {
            String sRet = "";
            if (MissReqPar == true)
                sRet = "<Reject><Reason>MISSING_REQUIRED_PARAMETER</Reason></Reject>";
            else 
                sRet = "<ReadPropety> " + ObjId.ToString() + PropId.ToString() + (AnyVar != null ? AnyVar.ToString() : "") + " </ReadPropety>";
            return sRet;
        }

//  ReadProperty-Request ::= SEQUENCE {
//  objectIdentifier [0] BACnetObjectIdentifier,
//  propertyIdentifier [1] BACnetPropertyIdentifier,
//  propertyArrayIndex [2] Unsigned OPTIONAL --used only with array datatype
//  -- if omitted with an array the entire array is referenced

//  ReadProperty-ACK ::= SEQUENCE {
//  objectIdentifier   [0] BACnetObjectIdentifier,
//  propertyIdentifier [1] BACnetPropertyIdentifier,
//  propertyArrayIndex [2] Unsigned OPTIONAL, --used only with array datatype
//  -- if omitted with an array the entire array is referenced
//  propertyValue [3] ABSTRACT-SYNTAX.&Type

    }
}
