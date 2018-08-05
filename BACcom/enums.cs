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


namespace BACcom
{

    public enum BACnetSegmentation : byte  {
        segmented_both =0,
        segmented_transmit = 1,
        segmented_receive = 2,
        no_segmentation = 3
    }

    // page 641 
    public enum BACnetConfirmedServiceChoice : byte
    {
        // Alarm and Event Services
        ACKNOWLEDGEALARM = 0,
        CONFIRMEDCOVNOTIFICATION = 1,
        CONFIRMEDEVENTNOTIFICATION = 2,
        GETALARMSUMMARY = 3,
        GETENROLLMENTSUMMARY = 4,
        GETEVENTINFORMATION = 29,
        SUBSCRIBECOV = 5,
        SUBSCRIBECOVPROPERTY = 28,
        LIFESAFETYOPERATION = 27,
        // File Access Services
        ATOMICREADFILE = 6,
        ATOMICWRITEFILE = 7,
        // Object Access Services
        ADDLISTELEMENT = 8,
        REMOVELISTELEMENT = 9,
        CREATEOBJECT = 10,
        DELETEOBJECT = 11,
        READPROPERTY = 12,                         // ReadProperty-Request; ReadProperty-ACK 
        READPROPERTYMULTIPLE = 14,
        READRANGE = 26,
        WRITEPROPERTY = 15,
        WRITEPROPERTYMULTIPLE = 16,
        // Remote Device Management Services
        DEVICECOMMUNICATIONCONTROL = 17,
        CONFIRMEDPRIVATETRANSFER = 18,
        CONFIRMEDTEXTMESSAGE = 19,
        REINITIALIZEDEVICE = 20,
        // Virtual Terminal Services
        VTOPEN = 21,
        VTCLOSE = 22,
        VTDATA = 23
        // Removed Services
        // formerly: authenticate (24), removed in version 1 revision 11
        // formerly: requestKey (25), removed in version 1 revision 11
        // formerly: readPropertyConditional (13), removed in version 1 revision 12
        // Services added after 1995
        // readRange (26) see Object Access Services
        // lifeSafetyOperation (27) see Alarm and Event Services
        // subscribeCOVProperty (28) see Alarm and Event Services
        // getEventInformation (29) see Alarm and Event Services
    }

    public enum BACNET_ServiceChoice_Error : byte
    {
        OTHER_ERROR = 127,
        ACKNOWLEDGEALARM_ERROR =0,
        CONFIRMEDCOVNOTIFICATION_ERROR = 1,
        CONFIRMEDEVENTNOTIFICATION_ERROR = 2,
        GETALARMSUMMARY_ERROR = 3,
        GETENROLLMENTSUMMARY_ERROR = 4,
        GETEVENTINFORMATION_ERROR  = 29,
        SUBSCRIBECOV_ERROR = 5,
        SUBSCRIBECOVPROPERTY_ERROR  = 28,
        LIFESAFETYOPERATION_ERROR  = 27,
        // FILE ACCESS SERVICES
        ATOMICREADFILE_ERROR = 6 ,
        ATOMICWRITEFILE_ERROR =  7,
        // OBJECT ACCESS SERVICES
        ADDLISTELEMENT_CHANGELIST_ERROR  = 8,
        REMOVELISTELEMENT_CHANGELIST_ERROR = 9,
        CREATEOBJECT_CREATEOBJECT_ERROR =  10,
        DELETEOBJECT_ERROR =  11,
        READPROPERTY_ERROR =  12,
        READPROPERTYMULTIPLE__ERROR =  14,
        READRANGE_ERROR =  26,
        WRITEPROPERTY_ERROR =  15,
        WRITEPROPERTYMULTIPLE_WRITEPROPERTYMULTIPLE_ERROR =  16,
        // REMOTE DEVICE MANAGEMENT SERVICES
        DEVICECOMMUNICATIONCONTROL_ERROR =  17,
        CONFIRMEDPRIVATETRANSFER_CONFIRMEDPRIVATETRANSFER_ERROR =  18,
        CONFIRMEDTEXTMESSAGE_ERROR =  19,
        REINITIALIZEDEVICE_ERROR =  20,
        // VIRTUAL TERMINAL SERVICES
        VTOPEN_ERROR =  21,
        VTCLOSE_VTCLOSE_ERROR =  22,
        VTDATA_ERROR = 23
        // REMOVED SERVICES
        // FORMERLY: AUTHENTICATE [24] REMOVED IN VERSION 1 REVISION 11
        // FORMERLY: REQUESTKEY [25] REMOVED IN VERSION 1 REVISION 11
        // FORMERLY: READPROPERTYCONDITIONAL [13] REMOVED IN VERSION 1 REVISION 12
        // SERVICES ADDED AFTER 1995
        // READRANGE [26] SEE OBJECT ACCESS SERVICES
        // LIFESAFETYOPERATION [27] SEE ALARM AND EVENT SERVICES
        // SUBSCRIBECOVPROPERTY [28] SEE ALARM AND EVENT SERVICES
        // GETEVENTINFORMATION [29] SEE ALARM AND EVENT SERVICES
     }

    public enum BACNET_Error_Class :  ushort
    {
        DEVICE = 0,
        OBJECT = 1,
        PROPERTY = 2,
        RESOURCES = 3,
        SECURITY = 4,
        SERVICES = 5,
        VT = 6,
        MAX_BACNET = 7,
        FIRST_PROPRIETARY = 64,
        LAST_PROPRIETARY = 65535
    }

    public enum BACNET_Error_Code : ushort 
    {
        OTHER = 0,
        DEVICE_BUSY = 3,
        CONFIGURATION_IN_PROGRESS = 2,
        OPERATIONAL_PROBLEM = 25,
        DYNAMIC_CREATION_NOT_SUPPORTED = 4,
        NO_OBJECTS_OF_SPECIFIED_TYPE = 17,
        OBJECT_DELETION_NOT_PERMITTED = 23,
        OBJECT_IDENTIFIER_ALREADY_EXISTS = 24,
        READ_ACCESS_DENIED = 27,
        UNKNOWN_OBJECT = 31,
        UNSUPPORTED_OBJECT_TYPE = 36,
        CHARACTER_SET_NOT_SUPPORTED = 41,
        DATATYPE_NOT_SUPPORTED = 47,
        INCONSISTENT_SELECTION_CRITERION = 8,
        INVALID_ARRAY_INDEX = 42,
        INVALID_DATA_TYPE = 9,
        NOT_COV_PROPERTY = 44,
        OPTIONAL_FUNCTIONALITY_NOT_SUPPORTED = 45,
        PROPERTY_IS_NOT_AN_ARRAY = 50,
        UNKNOWN_PROPERTY = 32,
        VALUE_OUT_OF_RANGE = 37,
        WRITE_ACCESS_DENIED = 40,
        NO_SPACE_FOR_OBJECT = 18,
        NO_SPACE_TO_ADD_LIST_ELEMENT = 19,
        NO_SPACE_TO_WRITE_PROPERTY = 20,
        AUTHENTICATION_FAILED = 1,
        INCOMPATIBLE_SECURITY_LEVELS = 6,
        INVALID_OPERATOR_NAME = 12,
        KEY_GENERATION_ERROR = 15,
        PASSWORD_FAILURE = 26,
        SECURITY_NOT_SUPPORTED = 28,
        TIMEOUT = 30,
        COV_SUBSCRIPTION_FAILED = 43,
        DUPLICATE_NAME = 48,
        DUPLICATE_OBJECT_ID = 49,
        FILE_ACCESS_DENIED = 5,
        INCONSISTENT_PARAMETERS = 7,
        INVALID_CONFIGURATION_DATA = 46,
        INVALID_FILE_ACCESS_METHOD = 10,
        INVALID_FILE_START_POSITION = 11,
        INVALID_PARAMETER_DATA_TYPE = 13,
        INVALID_TIME_STAMP = 14,
        MISSING_REQUIRED_PARAMETER = 16,
        PROPERTY_IS_NOT_A_LIST = 22,
        SERVICE_REQUEST_DENIED = 29,
        UNKNOWN_VT_CLASS = 34,
        UNKNOWN_VT_SESSION = 35,
        NO_VT_SESSIONS_AVAILABLE = 21,
        VT_SESSION_ALREADY_CLOSED = 38,
        VT_SESSION_TERMINATION_FAILURE = 39,
        RESERVED1 = 33,
        MAX_BACNET = 51,
        FIRST_PROPRIETARY = 256,
        LAST_PROPRIETARY = 65535
    } 

    public enum BACNET_Reject_Reason : byte {
        OTHER = 0,
        BUFFER_OVERFLOW = 1,
        INCONSISTENT_PARAMETERS = 2,
        INVALID_PARAMETER_DATA_TYPE = 3,
        INVALID_TAG = 4,
        MISSING_REQUIRED_PARAMETER = 5,
        PARAMETER_OUT_OF_RANGE = 6,
        TOO_MANY_ARGUMENTS = 7,
        UNDEFINED_ENUMERATION = 8,
        UNRECOGNIZED_SERVICE = 9

     }

    
}
