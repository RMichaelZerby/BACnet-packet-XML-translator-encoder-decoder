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
    class BACVnetVar
    {
        protected CLASS classType = CLASS.APPLICATION;
        protected byte  length;

        public BACVnetVar()
        {
        }

        public virtual BACVnetVar Decode(BACPacket cm) 
        {
            byte tag = cm.getByte();
            switch (DecodeClass(tag))
            {
                case CLASS.APPLICATION:
                    CONTEXT_TAG ct = (CONTEXT_TAG)(cm.getByte() >> 4);
                    return Decode(cm, ct);
                case CLASS.CONTEXT:
                    // IF this is an Openning Tag
                    if ((tag & 0x0F) == 0x0E)
                    {
                        BACVnetConstructed constr = new BACVnetConstructed();
                        cm.getNextByte();
                        while (cm.hasMore() == true)
                        {
                            tag = cm.getByte();
                            // IF this is the closing Tag
                            if ((tag & 0x0F) == 0x0F)
                            {
                                cm.getNextByte();
                                break;
                            }
                            BACVnetVar vn = new BACVnetVar().Decode(cm);
                            constr.AddVar(vn);                            
                            //constr.AddVar(cm.getNextByte());
                        }
                        return constr;
                    }
                    else
                    {
                        // This would be an error ??
                        CONTEXT_TAG ct1 = (CONTEXT_TAG)(cm.getByte() >> 4);
                        return Decode(cm, ct1);
                    }
                    
                default:
                    break;
            }

            return this;
        }


        public virtual BACVnetVar Decode(BACPacket cm, CONTEXT_TAG ct)
        {
            byte a = cm.getNextByte();
            classType = DecodeClass(a);
            length = DecodeLenValType(a);

            switch (ct)
            {
                case CONTEXT_TAG.NULL:
                    return new BACVnetNull().Decode(cm);
                case CONTEXT_TAG.BOOLEAN:
                    return new BACVnetBoolean().Decode(cm);
                case CONTEXT_TAG.INT:
                    return new BACVnetInt().Decode(cm, length);
                case CONTEXT_TAG.UINT:
                    return new BACVnetUInt().Decode(cm, length);
                case CONTEXT_TAG.REAL:
                    return new BACVnetReal().Decode(cm, length);
                case CONTEXT_TAG.DOUBLE:
                    return new BACVnetDouble().Decode(cm, length);
                case CONTEXT_TAG.BSTRING:
                    return new BACVnetBString().Decode(cm, length);
                case CONTEXT_TAG.CSTRING:
                    return new BACVnetCString().Decode(cm, length);
                case CONTEXT_TAG.OSTRING:
                    return new BACVnetOString().Decode(cm, length);
                case CONTEXT_TAG.DATE:
                    return new BACVnetDate().Decode(cm, length);
                case CONTEXT_TAG.TIME:
                    return new BACVnetTime().Decode(cm, length);
                case CONTEXT_TAG.ENUM: // 20.2.11 Encoding of an Enumerated Value; pg 1496
                    return new BACVnetEnum().Decode(cm, length);
                case CONTEXT_TAG.OBJECT:
                    return new BACVnetObjectIdentifier().Decode(cm, length);
            }
            return this; 
        }

        protected byte DecodeTagNumber(byte t)
        {
            return (byte)(t >> 4);
        }

        protected CLASS DecodeClass(byte c)
        {
            return (CLASS)( (c & 0x08) >> 3);
        }

        protected byte DecodeLenValType(byte t)
        {
            return (byte)(t & 0x07);
        }

        public enum APP_TAG : byte // Application Tags
        { // pg 626
            NULL = 0,
            BOOLEAN = 1,
            UINT = 2,
            INT = 3,      // Signed Integer (2's complement notation) 
            REAL = 4,     // Real (ANSI/IEEE-754 floating point)
            DOUBLE = 5,   // Double (ANSI/IEEE-754 double precision floating point)
            OSTRING = 6,
            CSTRING = 7,
            BSTRING = 8,  // Encoding of a Bit String Value
            ENUM = 9,     // Encoding of an Enumerated Value 
            DATE = 10,    // Encoding of a Date Value
            TIME = 11,    // Encoding of a Time Value  
            OBJECT = 12  // Encoding of an Object Identifier Value

        }

        public enum CONTEXT_TAG : byte
        {
            NULL = 0,
            BOOLEAN = 1,
            UINT = 2,
            INT = 3,      // Signed Integer (2's complement notation) 
            REAL = 4,     // Real (ANSI/IEEE-754 floating point)
            DOUBLE = 5,   // Double (ANSI/IEEE-754 double precision floating point)
            OSTRING = 6,
            CSTRING = 7,
            BSTRING = 8,  // Encoding of a Bit String Value
            ENUM = 9,     // Encoding of an Enumerated Value 
            DATE = 10,    // Encoding of a Date Value
            TIME = 11,    // Encoding of a Time Value  
            OBJECT = 12,  // Encoding of an Object Identifier Value
            ANY = 99      // Encoding of a Value of the ANY Type (This is a made up value)


        }

        public enum CLASS : byte
        {
            APPLICATION = 0,
            CONTEXT = 1
        }

        public override string ToString()
        {
            return "BACVnetVar:base;";
        }


    }
}
