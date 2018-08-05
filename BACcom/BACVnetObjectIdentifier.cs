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
    class BACVnetObjectIdentifier : BACVnetVar
    {
        UInt32 rawValue = 4194303;
        BACnetObjectType objectType;    // A 10-bit object type, representing the BACnetObjectType
        UInt32 instanceNumber; // A 22-bit object instance number 


        public BACVnetVar Decode(BACPacket cm, byte length)
        {
            if (length != 4)
                return null;
            UInt32 val = cm.getNextByte();
            for (int x = 0; x < 3; x++)
            {
                val *= 0x0100;
                val += cm.getNextByte();
            }
            ((BACVnetObjectIdentifier)this).Decode(val);
            return this;
        }


        // H.5.2.1 Object_Identifier, pg 841
        // 20.2.14 Encoding of an Object Identifier Value, pg 632
        // BACnetObjectIdentifier ::= [APPLICATION 12] OCTET STRING (SIZE(4)) -- see 20.2.14
        // BACnetObjectIdentifiers value of 4194303 to indicate that the property is not initialized
        // Table 12-63 – Datatype Coercion Rules

        public void Decode(UInt32 v)
        {
            rawValue = v;
            objectType = (BACnetObjectType)((rawValue >> 22) & 0x3ff);
            instanceNumber = (rawValue & 0x03FFFFF);
        }

        public BACnetObjectType getObjectType()
        {
            return objectType;  
        }

        public UInt32 getInstanceNumber()
        {
            return instanceNumber;
        }
        
        public enum BACnetObjectType : ushort
        { 
            ALERT_ENROLLMENT = 52,
            ACCESS_CREDENTIAL = 32,
            ACCESS_DOOR = 30,
            ACCESS_POINT = 33,
            ACCESS_RIGHTS = 34,
            ACCESS_USER = 35,
            ACCESS_ZONE = 36,
            ACCUMULATOR = 23,
            ANALOG_INPUT = 0,
            ANALOG_OUTPUT = 1,
            ANALOG_VALUE = 2,
            BINARY_INPUT = 3,
            BINARY_OUTPUT = 4,
            BINARY_VALUE = 5,
            BITSTRING_VALUE = 39,
            CALENDAR = 6,
            CHANNEL = 53,
            CHARACTERSTRING_VALUE = 40,
            COMMAND = 7,
            CREDENTIAL_DATA_INPUT = 37,
            DATE_PATTERN_VALUE = 41,
            DATE_VALUE = 42,
            DATETIME_PATTERN_VALUE = 43,
            DATETIME_VALUE = 44,
            DEVICE = 8,
            EVENT_ENROLLMENT = 9,
            EVENT_LOG = 25,
            FILE = 10,
            GLOBAL_GROUP = 26,
            GROUP = 11,
            INTEGER_VALUE = 45,
            LARGE_ANALOG_VALUE = 46,
            LIFE_SAFETY_POINT = 21,
            LIFE_SAFETY_ZONE = 22,
            LIGHTING_OUTPUT = 54,
            LOAD_CONTROL = 28,
            LOOP = 12,
            MULTI_STATE_INPUT = 13,
            MULTI_STATE_OUTPUT = 14,
            MULTI_STATE_VALUE = 19,
            NETWORK_SECURITY = 38,
            NOTIFICATION_CLASS = 15,
            NOTIFICATION_FORWARDER = 51,
            OCTETSTRING_VALUE = 47,
            POSITIVE_INTEGER_VALUE = 48,
            PROGRAM = 16,
            PULSE_CONVERTER = 24,
            SCHEDULE = 17,
            STRUCTURED_VIEW = 29,
            TIME_PATTERN_VALUE = 49,
            TIME_VALUE = 50,
            TREND_LOG = 20,
            TREND_LOG_MULTIPLE = 27
        }

        public String ObjectTypeToString()
        {

            switch (objectType) 
            {
                case BACnetObjectType.ALERT_ENROLLMENT:
                    return "ALERT_ENROLLMENT";
                case BACnetObjectType.ACCESS_CREDENTIAL:
                    return"ACCESS_CREDENTIAL";
                case BACnetObjectType.ACCESS_DOOR:
                    return"ACCESS_DOOR";
                case BACnetObjectType.ACCESS_POINT:
                    return"ACCESS_POINT";
                case BACnetObjectType.ACCESS_RIGHTS:
                    return "ACCESS_RIGHTS";
                case BACnetObjectType.ACCESS_USER:
                    return"ACCESS_USER";
                case BACnetObjectType.ACCESS_ZONE:
                    return"ACCESS_ZONE";
                case BACnetObjectType.ACCUMULATOR:
                    return"ACCUMULATOR";
                case BACnetObjectType.ANALOG_INPUT:
                    return"ANALOG_INPUT";
                case BACnetObjectType.ANALOG_OUTPUT:
                    return"ANALOG_OUTPUT";
                case BACnetObjectType.ANALOG_VALUE:
                    return"ANALOG_VALUE";
                case BACnetObjectType.BINARY_INPUT:
                    return"BINARY_INPUT";
                case BACnetObjectType.BINARY_OUTPUT:
                    return"BINARY_OUTPUT";
                case BACnetObjectType.BINARY_VALUE:
                    return"BINARY_VALUE";
                case BACnetObjectType.BITSTRING_VALUE:
                    return"BITSTRING_VALUE";
                case BACnetObjectType.CALENDAR:
                    return"CALENDAR";
                case BACnetObjectType.CHANNEL:
                    return"CHANNEL";
                case BACnetObjectType.CHARACTERSTRING_VALUE:
                    return"CHARACTERSTRING_VALUE";
                case BACnetObjectType.COMMAND:
                    return"COMMAND";
                case BACnetObjectType.CREDENTIAL_DATA_INPUT:
                    return"CREDENTIAL_DATA_INPUT";
                case BACnetObjectType.DATE_PATTERN_VALUE:
                    return"DATE_PATTERN_VALUE";
                case BACnetObjectType.DATE_VALUE:
                    return"DATE_VALUE";
                case BACnetObjectType.DATETIME_PATTERN_VALUE:
                    return"DATETIME_PATTERN_VALUE";
                case BACnetObjectType.DATETIME_VALUE:
                    return"DATETIME_VALUE";
                case BACnetObjectType.DEVICE:
                    return"DEVICE";
                case BACnetObjectType.EVENT_ENROLLMENT:
                    return"EVENT_ENROLLMENT";
                case BACnetObjectType.EVENT_LOG:
                    return"EVENT_LOG";
                case BACnetObjectType.FILE:
                    return"FILE";
                case BACnetObjectType.GLOBAL_GROUP:
                    return"GLOBAL_GROUP";
                case BACnetObjectType.GROUP:
                    return"GROUP";
                case BACnetObjectType.INTEGER_VALUE:
                    return"INTEGER_VALUE";
                case BACnetObjectType.LARGE_ANALOG_VALUE:
                    return"LARGE_ANALOG_VALUE";
                case BACnetObjectType.LIFE_SAFETY_POINT:
                    return"LIFE_SAFETY_POINT";
                case BACnetObjectType.LIFE_SAFETY_ZONE:
                    return"LIFE_SAFETY_ZONE";
                case BACnetObjectType.LIGHTING_OUTPUT:
                    return"LIGHTING_OUTPUT";
                case BACnetObjectType.LOAD_CONTROL:
                    return"LOAD_CONTROL";
                case BACnetObjectType.LOOP:
                    return"LOOP";
                case BACnetObjectType.MULTI_STATE_INPUT:
                    return"MULTI_STATE_INPUT";
                case BACnetObjectType.MULTI_STATE_OUTPUT:
                    return"MULTI_STATE_OUTPUT";
                case BACnetObjectType.MULTI_STATE_VALUE:
                    return"MULTI_STATE_VALUE";
                case BACnetObjectType.NETWORK_SECURITY:
                    return"NETWORK_SECURITY";
                case BACnetObjectType.NOTIFICATION_CLASS:
                    return"NOTIFICATION_CLASS";
                case BACnetObjectType.NOTIFICATION_FORWARDER:
                    return"NOTIFICATION_FORWARDER";
                case BACnetObjectType.OCTETSTRING_VALUE:
                    return"OCTETSTRING_VALUE";
                case BACnetObjectType.POSITIVE_INTEGER_VALUE:
                    return"POSITIVE_INTEGER_VALUE";
                case BACnetObjectType.PROGRAM:
                    return"PROGRAM";
                case BACnetObjectType.PULSE_CONVERTER:
                    return"PULSE_CONVERTER";
                case BACnetObjectType.SCHEDULE:
                    return"SCHEDULE";
                case BACnetObjectType.STRUCTURED_VIEW:
                    return"STRUCTURED_VIEW";
                case BACnetObjectType.TIME_PATTERN_VALUE:
                    return"TIME_PATTERN_VALUE";
                case BACnetObjectType.TIME_VALUE:
                    return"TIME_VALUE";
                case BACnetObjectType.TREND_LOG:
                    return"TREND_LOG";
                case BACnetObjectType.TREND_LOG_MULTIPLE:
                    return"TREND_LOG_MULTIPLE";
                default:
                    return "IDV_" + objectType.ToString(); 
            }

        }

        public override string ToString()
        {
            return "<ObjectId> " + "<ObjectrType>" + ObjectTypeToString() + "</ObjectrType> " + "<InstanceNumber>" + instanceNumber.ToString("X") + "</InstanceNumber> </ObjectId>";
        }

    }
}
