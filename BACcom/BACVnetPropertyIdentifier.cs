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
    class BACVnetPropertyId : BACVnetVar
    { // 20.2.11 Encoding of an Enumerated Value; pg 1496
        private BACnetPropertyIdentifier id;

        public override BACVnetVar Decode(BACPacket cm, CONTEXT_TAG ct)
        {
            byte a = cm.getNextByte();
            classType = DecodeClass(a);
            length = DecodeLenValType(a);
            Decode(cm, length);

            return this;
        }

        public BACVnetVar Decode(BACPacket cm, byte length)
        {
            ushort enumVal;
            enumVal = cm.getNextByte();
            for (int x = 1; x < length; x++)
            {
                enumVal *= 0x0100;
                enumVal += cm.getNextByte();
            }
            id = (BACnetPropertyIdentifier)enumVal;

            return (BACVnetVar)this;
        }

        public BACnetPropertyIdentifier getPropertyId()
        {
            return id;
        }

        // BACnetPropertyIdentifier ::= ENUMERATED
        public enum BACnetPropertyIdentifier : ushort
        {
            ABSENTEE_LIMIT = 244,
            ACCEPTED_MODES = 175,
            ACCESS_ALARM_EVENTS = 245,
            ACCESS_DOORS = 246,
            ACCESS_EVENT = 247,
            ACCESS_EVENT_AUTHENTICATION_FACTOR = 248,
            ACCESS_EVENT_CREDENTIAL = 249,
            ACCESS_EVENT_TAG = 322,
            ACCESS_EVENT_TIME = 250,
            ACCESS_TRANSACTION_EVENTS = 251,
            ACCOMPANIMENT = 252,
            ACCOMPANIMENT_TIME = 253,
            ACK_REQUIRED = 1,
            ACKED_TRANSITIONS = 0,
            ACTION = 2,
            ACTION_TEXT = 3,
            ACTIVATION_TIME = 254,
            ACTIVE_AUTHENTICATION_POLICY = 255,
            ACTIVE_COV_SUBSCRIPTIONS = 152,
            ACTIVE_TEXT = 4,
            ACTIVE_VT_SESSIONS = 5,
            ACTUAL_SHED_LEVEL = 212,
            ADJUST_VALUE = 176,
            ALARM_VALUE = 6,
            ALARM_VALUES = 7,
            ALIGN_INTERVALS = 193,
            ALL = 8,
            ALL_WRITES_SUCCESSFUL = 9,
            ALLOW_GROUP_DELAY_INHIBIT = 365,
            APDU_SEGMENT_TIMEOUT = 10,
            APDU_TIMEOUT = 11,
            APPLICATION_SOFTWARE_VERSION = 12,
            ARCHIVE = 13,
            ASSIGNED_ACCESS_RIGHTS = 256,
            ATTEMPTED_SAMPLES = 124,
            AUTHENTICATION_FACTORS = 257,
            AUTHENTICATION_POLICY_LIST = 258,
            AUTHENTICATION_POLICY_NAMES = 259,
            AUTHENTICATION_STATUS = 260,
            AUTHORIZATION_EXEMPTIONS = 364,
            AUTHORIZATION_MODE = 261,
            AUTO_SLAVE_DISCOVERY = 169,
            AVERAGE_VALUE = 125,
            BACKUP_AND_RESTORE_STATE = 338,
            BACKUP_FAILURE_TIMEOUT = 153,
            BACKUP_PREPARATION_TIME = 339,
            BASE_DEVICE_SECURITY_POLICY = 327,
            BELONGS_TO = 262,
            BIAS = 14,
            BIT_MASK = 342,
            BIT_TEXT = 343,
            BLINK_WARN_ENABLE = 373,
            BUFFER_SIZE = 126,
            CHANGE_OF_STATE_COUNT = 15,
            CHANGE_OF_STATE_TIME = 16,
            CHANNEL_NUMBER = 366,
            CLIENT_COV_INCREMENT = 127,
            CONFIGURATION_FILES = 154,
            CONTROL_GROUPS = 367,
            CONTROLLED_VARIABLE_REFERENCE = 19,
            CONTROLLED_VARIABLE_UNITS = 20,
            CONTROLLED_VARIABLE_VALUE = 21,
            COUNT = 177,
            COUNT_BEFORE_CHANGE = 178,
            COUNT_CHANGE_TIME = 179,
            COV_INCREMENT = 22,
            COV_PERIOD = 180,
            COV_RESUBSCRIPTION_INTERVAL = 128,
            COVU_PERIOD = 349,
            COVU_RECIPIENTS = 350,
            CREDENTIAL_DISABLE = 263,
            CREDENTIAL_STATUS = 264,
            CREDENTIALS = 265,
            CREDENTIALS_IN_ZONE = 266,
            DATABASE_REVISION = 155,
            DATE_LIST = 23,
            DAYLIGHT_SAVINGS_STATUS = 24,
            DAYS_REMAINING = 267,
            DEADBAND = 25,
            DEFAULT_FADE_TIME = 374,
            DEFAULT_RAMP_RATE = 375,
            DEFAULT_STEP_INCREMENT = 376,
            DERIVATIVE_CONSTANT = 26,
            DERIVATIVE_CONSTANT_UNITS = 27,
            DESCRIPTION = 28,
            DESCRIPTION_OF_HALT = 29,
            DEVICE_ADDRESS_BINDING = 30,
            DEVICE_TYPE = 31,
            DIRECT_READING = 156,
            DISTRIBUTION_KEY_REVISION = 328,
            DO_NOT_HIDE = 329,
            DOOR_ALARM_STATE = 226,
            DOOR_EXTENDED_PULSE_TIME = 227,
            DOOR_MEMBERS = 228,
            DOOR_OPEN_TOO_LONG_TIME = 229,
            DOOR_PULSE_TIME = 230,
            DOOR_STATUS = 231,
            DOOR_UNLOCK_DELAY_TIME = 232,
            DUTY_WINDOW = 213,
            EFFECTIVE_PERIOD = 32,
            EGRESS_TIME = 377,
            EGRESS_ACTIVE = 386,
            ELAPSED_ACTIVE_TIME = 33,
            ENTRY_POINTS = 268,
            ENABLE = 133,
            ERROR_LIMIT = 34,
            EVENT_ALGORITHM_INHIBIT = 354,
            EVENT_ALGORITHM_INHIBIT_REF = 355,
            EVENT_DETECTION_ENABLE = 353,
            EVENT_ENABLE = 35,
            EVENT_MESSAGE_TEXTS = 351,
            EVENT_MESSAGE_TEXTS_CONFIG = 352,
            EVENT_STATE = 36,
            EVENT_TIME_STAMPS = 130,
            EVENT_TYPE = 37,
            EVENT_PARAMETERS = 83,
            EXCEPTION_SCHEDULE = 38,
            EXECUTION_DELAY = 368,
            EXIT_POINTS = 269,
            EXPECTED_SHED_LEVEL = 214,
            EXPIRY_TIME = 270,
            EXTENDED_TIME_ENABLE = 271,
            FAILED_ATTEMPT_EVENTS = 272,
            FAILED_ATTEMPTS = 273,
            FAILED_ATTEMPTS_TIME = 274,
            FAULT_PARAMETERS = 358,
            FAULT_TYPE = 359,
            FAULT_VALUES = 39,
            FEEDBACK_VALUE = 40,
            FILE_ACCESS_METHOD = 41,
            FILE_SIZE = 42,
            FILE_TYPE = 43,
            FIRMWARE_REVISION = 44,
            FULL_DUTY_BASELINE = 215,
            GLOBAL_IDENTIFIER = 323,
            GROUP_MEMBERS = 345,
            GROUP_MEMBER_NAMES = 346,
            HIGH_LIMIT = 45,
            INACTIVE_TEXT = 46,
            IN_PROCESS = 47,
            IN_PROGRESS = 378,
            INPUT_REFERENCE = 181,
            INSTANCE_OF = 48,
            INSTANTANEOUS_POWER = 379,
            INTEGRAL_CONSTANT = 49,
            INTEGRAL_CONSTANT_UNITS = 50,
            INTERVAL_OFFSET = 195,
            IS_UTC = 344,
            KEY_SETS = 330,
            LAST_ACCESS_EVENT = 275,
            LAST_ACCESS_POINT = 276,
            LAST_CREDENTIAL_ADDED = 277,
            LAST_CREDENTIAL_ADDED_TIME = 278,
            LAST_CREDENTIAL_REMOVED = 279,
            LAST_CREDENTIAL_REMOVED_TIME = 280,
            LAST_KEY_SERVER = 331,
            LAST_NOTIFY_RECORD = 173,
            LAST_PRIORITY = 369,
            LAST_RESTART_REASON = 196,
            LAST_RESTORE_TIME = 157,
            LAST_USE_TIME = 281,
            LIFE_SAFETY_ALARM_VALUES = 166,
            LIGHTING_COMMAND = 380,
            LIGHTING_COMMAND_DEFAULT_PRIORITY = 381,
            LIMIT_ENABLE = 52,
            LIMIT_MONITORING_INTERVAL = 182,
            LIST_OF_GROUP_MEMBERS = 53,
            LIST_OF_OBJECT_PROPERTY_REFERENCES = 54,
            LOCAL_DATE = 56,
            LOCAL_FORWARDING_ONLY = 360,
            LOCAL_TIME = 57,
            LOCATION = 58,
            LOCK_STATUS = 233,
            LOCKOUT = 282,
            LOCKOUT_RELINQUISH_TIME = 283,
            LOG_BUFFER = 131,
            LOG_DEVICE_OBJECT_PROPERTY = 132,
            LOG_INTERVAL = 134,
            LOGGING_OBJECT = 183,
            LOGGING_RECORD = 184,
            LOGGING_TYPE = 197,
            LOW_LIMIT = 59,
            MAINTENANCE_REQUIRED = 158,
            MANIPULATED_VARIABLE_REFERENCE = 60,
            MANUAL_SLAVE_ADDRESS_BINDING = 170,
            MASKED_ALARM_VALUES = 234,
            MAXIMUM_OUTPUT = 61,
            MAXIMUM_VALUE = 135,
            MAXIMUM_VALUE_TIMESTAMP = 149,
            MAX_ACTUAL_VALUE = 382,
            MAX_APDU_LENGTH_ACCEPTED = 62,
            MAX_FAILED_ATTEMPTS = 285,
            MAX_INFO_FRAMES = 63,
            MAX_MASTER = 64,
            MAX_PRES_VALUE = 65,
            MAX_SEGMENTS_ACCEPTED = 167,
            MEMBER_OF = 159,
            MEMBER_STATUS_FLAGS = 347,
            MEMBERS = 286,
            MINIMUM_OFF_TIME = 66,
            MINIMUM_ON_TIME = 67,
            MINIMUM_OUTPUT = 68,
            MINIMUM_VALUE = 136,
            MINIMUM_VALUE_TIMESTAMP = 150,
            MIN_ACTUAL_VALUE = 383,
            MIN_PRES_VALUE = 69,
            MODE = 160,
            MODEL_NAME = 70,
            MODIFICATION_DATE = 71,
            MUSTER_POINT = 287,
            NEGATIVE_ACCESS_RULES = 288,
            NETWORK_ACCESS_SECURITY_POLICIES = 332,
            NODE_SUBTYPE = 207,
            NODE_TYPE = 208,
            NOTIFICATION_CLASS = 17,
            NOTIFICATION_THRESHOLD = 137,
            NOTIFY_TYPE = 72,
            NUMBER_OF_APDU_RETRIES = 73,
            NUMBER_OF_AUTHENTICATION_POLICIES = 289,
            NUMBER_OF_STATES = 74,
            OBJECT_IDENTIFIER = 75,
            OBJECT_LIST = 76,
            OBJECT_NAME = 77,
            OBJECT_PROPERTY_REFERENCE = 78,
            OBJECT_TYPE = 79,
            OCCUPANCY_COUNT = 290,
            OCCUPANCY_COUNT_ADJUST = 291,
            OCCUPANCY_COUNT_ENABLE = 292,
            OCCUPANCY_LOWER_LIMIT = 294,
            OCCUPANCY_LOWER_LIMIT_ENFORCED = 295,
            OCCUPANCY_STATE = 296,
            OCCUPANCY_UPPER_LIMIT = 297,
            OCCUPANCY_UPPER_LIMIT_ENFORCED = 298,
            OPERATION_EXPECTED = 161,
            OPTIONAL = 80,
            OUT_OF_SERVICE = 81,
            OUTPUT_UNITS = 82,
            PACKET_REORDER_TIME = 333,
            PASSBACK_MODE = 300,
            PASSBACK_TIMEOUT = 301,
            POLARITY = 84,
            PORT_FILTER = 363,
            POSITIVE_ACCESS_RULES = 302,
            POWER = 384,
            PRESCALE = 185,
            PRESENT_VALUE = 85,
            PRIORITY = 86,
            PRIORITY_ARRAY = 87,
            PRIORITY_FOR_WRITING = 88,
            PROCESS_IDENTIFIER = 89,
            PROCESS_IDENTIFIER_FILTER = 361,
            PROFILE_NAME = 168,
            PROGRAM_CHANGE = 90,
            PROGRAM_LOCATION = 91,
            PROGRAM_STATE = 92,
            PROPERTY_LIST = 371,
            PROPORTIONAL_CONSTANT = 93,
            PROPORTIONAL_CONSTANT_UNITS = 94,
            PROTOCOL_OBJECT_TYPES_SUPPORTED = 96,
            PROTOCOL_REVISION = 139,
            PROTOCOL_SERVICES_SUPPORTED = 97,
            PROTOCOL_VERSION = 98,
            PULSE_RATE = 186,
            READ_ONLY = 99,
            REASON_FOR_DISABLE = 303,
            REASON_FOR_HALT = 100,
            RECIPIENT_LIST = 102,
            RECORDS_SINCE_NOTIFICATION = 140,
            RECORD_COUNT = 141,
            RELIABILITY = 103,
            RELIABILITY_EVALUATION_INHIBIT = 357,
            RELINQUISH_DEFAULT = 104,
            REQUESTED_SHED_LEVEL = 218,
            REQUESTED_UPDATE_INTERVAL = 348,
            REQUIRED = 105,
            RESOLUTION = 106,
            RESTART_NOTIFICATION_RECIPIENTS = 202,
            RESTORE_COMPLETION_TIME = 340,
            RESTORE_PREPARATION_TIME = 341,
            SCALE = 187,
            SCALE_FACTOR = 188,
            SCHEDULE_DEFAULT = 174,
            SECURED_STATUS = 235,
            SECURITY_PDU_TIMEOUT = 334,
            SECURITY_TIME_WINDOW = 335,
            SEGMENTATION_SUPPORTED = 107,
            SERIAL_NUMBER = 372,
            SETPOINT = 108,
            SETPOINT_REFERENCE = 109,
            SETTING = 162,
            SHED_DURATION = 219,
            SHED_LEVEL_DESCRIPTIONS = 220,
            SHED_LEVELS = 221,
            SILENCED = 163,
            SLAVE_ADDRESS_BINDING = 171,
            SLAVE_PROXY_ENABLE = 172,
            START_TIME = 142,
            STATE_DESCRIPTION = 222,
            STATE_TEXT = 110,
            STATUS_FLAGS = 111,
            STOP_TIME = 143,
            STOP_WHEN_FULL = 144,
            STRUCTURED_OBJECT_LIST = 209,
            SUBORDINATE_ANNOTATIONS = 210,
            SUBORDINATE_LIST = 211,
            SUBSCRIBED_RECIPIENTS = 362,
            SUPPORTED_FORMATS = 304,
            SUPPORTED_FORMAT_CLASSES = 305,
            SUPPORTED_SECURITY_ALGORITHMS = 336,
            SYSTEM_STATUS = 112,
            THREAT_AUTHORITY = 306,
            THREAT_LEVEL = 307,
            TIME_DELAY = 113,
            TIME_DELAY_NORMAL = 356,
            TIME_OF_ACTIVE_TIME_RESET = 114,
            TIME_OF_DEVICE_RESTART = 203,
            TIME_OF_STATE_COUNT_RESET = 115,
            TIME_SYNCHRONIZATION_INTERVAL = 204,
            TIME_SYNCHRONIZATION_RECIPIENTS = 116,
            TOTAL_RECORD_COUNT = 145,
            TRACE_FLAG = 308,
            TRACKING_VALUE = 164,
            TRANSACTION_NOTIFICATION_CLASS = 309,
            TRANSITION = 385,
            TRIGGER = 205,
            UNITS = 117,
            UPDATE_INTERVAL = 118,
            UPDATE_KEY_SET_TIMEOUT = 337,
            UPDATE_TIME = 189,
            USER_EXTERNAL_IDENTIFIER = 310,
            USER_INFORMATION_REFERENCE = 311,
            USER_NAME = 317,
            USER_TYPE = 318,
            USES_REMAINING = 319,
            UTC_OFFSET = 119,
            UTC_TIME_SYNCHRONIZATION_RECIPIENTS = 206,
            VALID_SAMPLES = 146,
            VALUE_BEFORE_CHANGE = 190,
            VALUE_SET = 191,
            VALUE_CHANGE_TIME = 192,
            VARIANCE_VALUE = 151,
            VENDOR_IDENTIFIER = 120,
            VENDOR_NAME = 121,
            VERIFICATION_TIME = 326,
            VT_CLASSES_SUPPORTED = 122,
            WEEKLY_SCHEDULE = 123,
            WINDOW_INTERVAL = 147,
            WINDOW_SAMPLES = 148,
            WRITE_STATUS = 370,
            ZONE_FROM = 320,
            ZONE_MEMBERS = 165,
            ZONE_TO = 321
        }

        public String BACnetPropertyIdentifierToString()
        {
            switch ( id )
            {
                case BACnetPropertyIdentifier.ABSENTEE_LIMIT:
                    return "ABSENTEE_LIMIT";
                case BACnetPropertyIdentifier.ACCEPTED_MODES:
                    return "ACCEPTED_MODES";
                case BACnetPropertyIdentifier.ACCESS_ALARM_EVENTS:
                    return "ACCESS_ALARM_EVENTS";
                case BACnetPropertyIdentifier.ACCESS_DOORS:
                    return "ACCESS_DOORS";
                case BACnetPropertyIdentifier.ACCESS_EVENT:
                    return "ACCESS_EVENT";
                case BACnetPropertyIdentifier.ACCESS_EVENT_AUTHENTICATION_FACTOR:
                    return "ACCESS_EVENT_AUTHENTICATION_FACTOR";
                case BACnetPropertyIdentifier.ACCESS_EVENT_CREDENTIAL:
                    return "ACCESS_EVENT_CREDENTIAL";
                case BACnetPropertyIdentifier.ACCESS_EVENT_TAG:
                    return "ACCESS_EVENT_TAG";
                case BACnetPropertyIdentifier.ACCESS_EVENT_TIME:
                    return "ACCESS_EVENT_TIME";
                case BACnetPropertyIdentifier.ACCESS_TRANSACTION_EVENTS:
                    return "ACCESS_TRANSACTION_EVENTS";
                case BACnetPropertyIdentifier.ACCOMPANIMENT:
                    return "ACCOMPANIMENT";
                case BACnetPropertyIdentifier.ACCOMPANIMENT_TIME:
                    return "ACCOMPANIMENT_TIME";
                case BACnetPropertyIdentifier.ACK_REQUIRED:
                    return "ACK_REQUIRED";
                case BACnetPropertyIdentifier.ACKED_TRANSITIONS:
                    return "ACKED_TRANSITIONS";
                case BACnetPropertyIdentifier.ACTION:
                    return "ACTION";
                case BACnetPropertyIdentifier.ACTION_TEXT:
                    return "ACTION_TEXT";
                case BACnetPropertyIdentifier.ACTIVATION_TIME:
                    return "ACTIVATION_TIME";
                case BACnetPropertyIdentifier.ACTIVE_AUTHENTICATION_POLICY:
                    return "ACTIVE_AUTHENTICATION_POLICY";
                case BACnetPropertyIdentifier.ACTIVE_COV_SUBSCRIPTIONS:
                    return "ACTIVE_COV_SUBSCRIPTIONS";
                case BACnetPropertyIdentifier.ACTIVE_TEXT:
                    return "ACTIVE_TEXT";
                case BACnetPropertyIdentifier.ACTIVE_VT_SESSIONS:
                    return "ACTIVE_VT_SESSIONS";
                case BACnetPropertyIdentifier.ACTUAL_SHED_LEVEL:
                    return "ACTUAL_SHED_LEVEL";
                case BACnetPropertyIdentifier.ADJUST_VALUE:
                    return "ADJUST_VALUE";
                case BACnetPropertyIdentifier.ALARM_VALUE:
                    return "ALARM_VALUE";
                case BACnetPropertyIdentifier.ALARM_VALUES:
                    return "ALARM_VALUES";
                case BACnetPropertyIdentifier.ALIGN_INTERVALS:
                    return "ALIGN_INTERVALS";
                case BACnetPropertyIdentifier.ALL:
                    return "ALL";
                case BACnetPropertyIdentifier.ALL_WRITES_SUCCESSFUL:
                    return "ALL_WRITES_SUCCESSFUL";
                case BACnetPropertyIdentifier.ALLOW_GROUP_DELAY_INHIBIT:
                    return "ALLOW_GROUP_DELAY_INHIBIT";
                case BACnetPropertyIdentifier.APDU_SEGMENT_TIMEOUT:
                    return "APDU_SEGMENT_TIMEOUT";
                case BACnetPropertyIdentifier.APDU_TIMEOUT:
                    return "APDU_TIMEOUT";
                case BACnetPropertyIdentifier.APPLICATION_SOFTWARE_VERSION:
                    return "APPLICATION_SOFTWARE_VERSION";
                case BACnetPropertyIdentifier.ARCHIVE:
                    return "ARCHIVE";
                case BACnetPropertyIdentifier.ASSIGNED_ACCESS_RIGHTS:
                    return "ASSIGNED_ACCESS_RIGHTS";
                case BACnetPropertyIdentifier.ATTEMPTED_SAMPLES:
                    return "ATTEMPTED_SAMPLES";
                case BACnetPropertyIdentifier.AUTHENTICATION_FACTORS:
                    return "AUTHENTICATION_FACTORS";
                case BACnetPropertyIdentifier.AUTHENTICATION_POLICY_LIST:
                    return "AUTHENTICATION_POLICY_LIST";
                case BACnetPropertyIdentifier.AUTHENTICATION_POLICY_NAMES:
                    return "AUTHENTICATION_POLICY_NAMES";
                case BACnetPropertyIdentifier.AUTHENTICATION_STATUS:
                    return "AUTHENTICATION_STATUS";
                case BACnetPropertyIdentifier.AUTHORIZATION_EXEMPTIONS:
                    return "AUTHORIZATION_EXEMPTIONS";
                case BACnetPropertyIdentifier.AUTHORIZATION_MODE:
                    return "AUTHORIZATION_MODE";
                case BACnetPropertyIdentifier.AUTO_SLAVE_DISCOVERY:
                    return "AUTO_SLAVE_DISCOVERY";
                case BACnetPropertyIdentifier.AVERAGE_VALUE:
                    return "AVERAGE_VALUE";
                case BACnetPropertyIdentifier.BACKUP_AND_RESTORE_STATE:
                    return "BACKUP_AND_RESTORE_STATE";
                case BACnetPropertyIdentifier.BACKUP_FAILURE_TIMEOUT:
                    return "BACKUP_FAILURE_TIMEOUT";
                case BACnetPropertyIdentifier.BACKUP_PREPARATION_TIME:
                    return "BACKUP_PREPARATION_TIME";
                case BACnetPropertyIdentifier.BASE_DEVICE_SECURITY_POLICY:
                    return "BASE_DEVICE_SECURITY_POLICY";
                case BACnetPropertyIdentifier.BELONGS_TO:
                    return "BELONGS_TO";
                case BACnetPropertyIdentifier.BIAS:
                    return "BIAS";
                case BACnetPropertyIdentifier.BIT_MASK:
                    return "BIT_MASK";
                case BACnetPropertyIdentifier.BIT_TEXT:
                    return "BIT_TEXT";
                case BACnetPropertyIdentifier.BLINK_WARN_ENABLE:
                    return "BLINK_WARN_ENABLE";
                case BACnetPropertyIdentifier.BUFFER_SIZE:
                    return "BUFFER_SIZE";
                case BACnetPropertyIdentifier.CHANGE_OF_STATE_COUNT:
                    return "CHANGE_OF_STATE_COUNT";
                case BACnetPropertyIdentifier.CHANGE_OF_STATE_TIME:
                    return "CHANGE_OF_STATE_TIME";
                case BACnetPropertyIdentifier.CHANNEL_NUMBER:
                    return "CHANNEL_NUMBER";
                case BACnetPropertyIdentifier.CLIENT_COV_INCREMENT:
                    return "CLIENT_COV_INCREMENT";
                case BACnetPropertyIdentifier.CONFIGURATION_FILES:
                    return "CONFIGURATION_FILES";
                case BACnetPropertyIdentifier.CONTROL_GROUPS:
                    return "CONTROL_GROUPS";
                case BACnetPropertyIdentifier.CONTROLLED_VARIABLE_REFERENCE:
                    return "CONTROLLED_VARIABLE_REFERENCE";
                case BACnetPropertyIdentifier.CONTROLLED_VARIABLE_UNITS:
                    return "CONTROLLED_VARIABLE_UNITS";
                case BACnetPropertyIdentifier.CONTROLLED_VARIABLE_VALUE:
                    return "CONTROLLED_VARIABLE_VALUE";
                case BACnetPropertyIdentifier.COUNT:
                    return "COUNT";
                case BACnetPropertyIdentifier.COUNT_BEFORE_CHANGE:
                    return "COUNT_BEFORE_CHANGE";
                case BACnetPropertyIdentifier.COUNT_CHANGE_TIME:
                    return "COUNT_CHANGE_TIME";
                case BACnetPropertyIdentifier.COV_INCREMENT:
                    return "COV_INCREMENT";
                case BACnetPropertyIdentifier.COV_PERIOD:
                    return "COV_PERIOD";
                case BACnetPropertyIdentifier.COV_RESUBSCRIPTION_INTERVAL:
                    return "COV_RESUBSCRIPTION_INTERVAL";
                case BACnetPropertyIdentifier.COVU_PERIOD:
                    return "COVU_PERIOD";
                case BACnetPropertyIdentifier.COVU_RECIPIENTS:
                    return "COVU_RECIPIENTS";
                case BACnetPropertyIdentifier.CREDENTIAL_DISABLE:
                    return "CREDENTIAL_DISABLE";
                case BACnetPropertyIdentifier.CREDENTIAL_STATUS:
                    return "CREDENTIAL_STATUS";
                case BACnetPropertyIdentifier.CREDENTIALS:
                    return "CREDENTIALS";
                case BACnetPropertyIdentifier.CREDENTIALS_IN_ZONE:
                    return "CREDENTIALS_IN_ZONE";
                case BACnetPropertyIdentifier.DATABASE_REVISION:
                    return "DATABASE_REVISION";
                case BACnetPropertyIdentifier.DATE_LIST:
                    return "DATE_LIST";
                case BACnetPropertyIdentifier.DAYLIGHT_SAVINGS_STATUS:
                    return "DAYLIGHT_SAVINGS_STATUS";
                case BACnetPropertyIdentifier.DAYS_REMAINING:
                    return "DAYS_REMAINING";
                case BACnetPropertyIdentifier.DEADBAND:
                    return "DEADBAND";
                case BACnetPropertyIdentifier.DEFAULT_FADE_TIME:
                    return "DEFAULT_FADE_TIME";
                case BACnetPropertyIdentifier.DEFAULT_RAMP_RATE:
                    return "DEFAULT_RAMP_RATE";
                case BACnetPropertyIdentifier.DEFAULT_STEP_INCREMENT:
                    return "DEFAULT_STEP_INCREMENT";
                case BACnetPropertyIdentifier.DERIVATIVE_CONSTANT:
                    return "DERIVATIVE_CONSTANT";
                case BACnetPropertyIdentifier.DERIVATIVE_CONSTANT_UNITS:
                    return "DERIVATIVE_CONSTANT_UNITS";
                case BACnetPropertyIdentifier.DESCRIPTION:
                    return "DESCRIPTION";
                case BACnetPropertyIdentifier.DESCRIPTION_OF_HALT:
                    return "DESCRIPTION_OF_HALT";
                case BACnetPropertyIdentifier.DEVICE_ADDRESS_BINDING:
                    return "DEVICE_ADDRESS_BINDING";
                case BACnetPropertyIdentifier.DEVICE_TYPE:
                    return "DEVICE_TYPE";
                case BACnetPropertyIdentifier.DIRECT_READING:
                    return "DIRECT_READING";
                case BACnetPropertyIdentifier.DISTRIBUTION_KEY_REVISION:
                    return "DISTRIBUTION_KEY_REVISION";
                case BACnetPropertyIdentifier.DO_NOT_HIDE:
                    return "DO_NOT_HIDE";
                case BACnetPropertyIdentifier.DOOR_ALARM_STATE:
                    return "DOOR_ALARM_STATE";
                case BACnetPropertyIdentifier.DOOR_EXTENDED_PULSE_TIME:
                    return "DOOR_EXTENDED_PULSE_TIME";
                case BACnetPropertyIdentifier.DOOR_MEMBERS:
                    return "DOOR_MEMBERS";
                case BACnetPropertyIdentifier.DOOR_OPEN_TOO_LONG_TIME:
                    return "DOOR_OPEN_TOO_LONG_TIME";
                case BACnetPropertyIdentifier.DOOR_PULSE_TIME:
                    return "DOOR_PULSE_TIME";
                case BACnetPropertyIdentifier.DOOR_STATUS:
                    return "DOOR_STATUS";
                case BACnetPropertyIdentifier.DOOR_UNLOCK_DELAY_TIME:
                    return "DOOR_UNLOCK_DELAY_TIME";
                case BACnetPropertyIdentifier.DUTY_WINDOW:
                    return "DUTY_WINDOW";
                case BACnetPropertyIdentifier.EFFECTIVE_PERIOD:
                    return "EFFECTIVE_PERIOD";
                case BACnetPropertyIdentifier.EGRESS_TIME:
                    return "EGRESS_TIME";
                case BACnetPropertyIdentifier.EGRESS_ACTIVE:
                    return "EGRESS_ACTIVE";
                case BACnetPropertyIdentifier.ELAPSED_ACTIVE_TIME:
                    return "ELAPSED_ACTIVE_TIME";
                case BACnetPropertyIdentifier.ENTRY_POINTS:
                    return "ENTRY_POINTS";
                case BACnetPropertyIdentifier.ENABLE:
                    return "ENABLE";
                case BACnetPropertyIdentifier.ERROR_LIMIT:
                    return "ERROR_LIMIT";
                case BACnetPropertyIdentifier.EVENT_ALGORITHM_INHIBIT:
                    return "EVENT_ALGORITHM_INHIBIT";
                case BACnetPropertyIdentifier.EVENT_ALGORITHM_INHIBIT_REF:
                    return "EVENT_ALGORITHM_INHIBIT_REF";
                case BACnetPropertyIdentifier.EVENT_DETECTION_ENABLE:
                    return "EVENT_DETECTION_ENABLE";
                case BACnetPropertyIdentifier.EVENT_ENABLE:
                    return "EVENT_ENABLE";
                case BACnetPropertyIdentifier.EVENT_MESSAGE_TEXTS:
                    return "EVENT_MESSAGE_TEXTS";
                case BACnetPropertyIdentifier.EVENT_MESSAGE_TEXTS_CONFIG:
                    return "EVENT_MESSAGE_TEXTS_CONFIG";
                case BACnetPropertyIdentifier.EVENT_STATE:
                    return "EVENT_STATE";
                case BACnetPropertyIdentifier.EVENT_TIME_STAMPS:
                    return "EVENT_TIME_STAMPS";
                case BACnetPropertyIdentifier.EVENT_TYPE:
                    return "EVENT_TYPE";
                case BACnetPropertyIdentifier.EVENT_PARAMETERS:
                    return "EVENT_PARAMETERS";
                case BACnetPropertyIdentifier.EXCEPTION_SCHEDULE:
                    return "EXCEPTION_SCHEDULE";
                case BACnetPropertyIdentifier.EXECUTION_DELAY:
                    return "EXECUTION_DELAY";
                case BACnetPropertyIdentifier.EXIT_POINTS:
                    return "EXIT_POINTS";
                case BACnetPropertyIdentifier.EXPECTED_SHED_LEVEL:
                    return "EXPECTED_SHED_LEVEL";
                case BACnetPropertyIdentifier.EXPIRY_TIME:
                    return "EXPIRY_TIME";
                case BACnetPropertyIdentifier.EXTENDED_TIME_ENABLE:
                    return "EXTENDED_TIME_ENABLE";
                case BACnetPropertyIdentifier.FAILED_ATTEMPT_EVENTS:
                    return "FAILED_ATTEMPT_EVENTS";
                case BACnetPropertyIdentifier.FAILED_ATTEMPTS:
                    return "FAILED_ATTEMPTS";
                case BACnetPropertyIdentifier.FAILED_ATTEMPTS_TIME:
                    return "FAILED_ATTEMPTS_TIME";
                case BACnetPropertyIdentifier.FAULT_PARAMETERS:
                    return "FAULT_PARAMETERS";
                case BACnetPropertyIdentifier.FAULT_TYPE:
                    return "FAULT_TYPE";
                case BACnetPropertyIdentifier.FAULT_VALUES:
                    return "FAULT_VALUES";
                case BACnetPropertyIdentifier.FEEDBACK_VALUE:
                    return "FEEDBACK_VALUE";
                case BACnetPropertyIdentifier.FILE_ACCESS_METHOD:
                    return "FILE_ACCESS_METHOD";
                case BACnetPropertyIdentifier.FILE_SIZE:
                    return "FILE_SIZE";
                case BACnetPropertyIdentifier.FILE_TYPE:
                    return "FILE_TYPE";
                case BACnetPropertyIdentifier.FIRMWARE_REVISION:
                    return "FIRMWARE_REVISION";
                case BACnetPropertyIdentifier.FULL_DUTY_BASELINE:
                    return "FULL_DUTY_BASELINE";
                case BACnetPropertyIdentifier.GLOBAL_IDENTIFIER:
                    return "GLOBAL_IDENTIFIER";
                case BACnetPropertyIdentifier.GROUP_MEMBERS:
                    return "GROUP_MEMBERS";
                case BACnetPropertyIdentifier.GROUP_MEMBER_NAMES:
                    return "GROUP_MEMBER_NAMES";
                case BACnetPropertyIdentifier.HIGH_LIMIT:
                    return "HIGH_LIMIT";
                case BACnetPropertyIdentifier.INACTIVE_TEXT:
                    return "INACTIVE_TEXT";
                case BACnetPropertyIdentifier.IN_PROCESS:
                    return "IN_PROCESS";
                case BACnetPropertyIdentifier.IN_PROGRESS:
                    return "IN_PROGRESS";
                case BACnetPropertyIdentifier.INPUT_REFERENCE:
                    return "INPUT_REFERENCE";
                case BACnetPropertyIdentifier.INSTANCE_OF:
                    return "INSTANCE_OF";
                case BACnetPropertyIdentifier.INSTANTANEOUS_POWER:
                    return "INSTANTANEOUS_POWER";
                case BACnetPropertyIdentifier.INTEGRAL_CONSTANT:
                    return "INTEGRAL_CONSTANT";
                case BACnetPropertyIdentifier.INTEGRAL_CONSTANT_UNITS:
                    return "INTEGRAL_CONSTANT_UNITS";
                case BACnetPropertyIdentifier.INTERVAL_OFFSET:
                    return "INTERVAL_OFFSET";
                case BACnetPropertyIdentifier.IS_UTC:
                    return "IS_UTC";
                case BACnetPropertyIdentifier.KEY_SETS:
                    return "KEY_SETS";
                case BACnetPropertyIdentifier.LAST_ACCESS_EVENT:
                    return "LAST_ACCESS_EVENT";
                case BACnetPropertyIdentifier.LAST_ACCESS_POINT:
                    return "LAST_ACCESS_POINT";
                case BACnetPropertyIdentifier.LAST_CREDENTIAL_ADDED:
                    return "LAST_CREDENTIAL_ADDED";
                case BACnetPropertyIdentifier.LAST_CREDENTIAL_ADDED_TIME:
                    return "LAST_CREDENTIAL_ADDED_TIME";
                case BACnetPropertyIdentifier.LAST_CREDENTIAL_REMOVED:
                    return "LAST_CREDENTIAL_REMOVED";
                case BACnetPropertyIdentifier.LAST_CREDENTIAL_REMOVED_TIME:
                    return "LAST_CREDENTIAL_REMOVED_TIME";
                case BACnetPropertyIdentifier.LAST_KEY_SERVER:
                    return "LAST_KEY_SERVER";
                case BACnetPropertyIdentifier.LAST_NOTIFY_RECORD:
                    return "LAST_NOTIFY_RECORD";
                case BACnetPropertyIdentifier.LAST_PRIORITY:
                    return "LAST_PRIORITY";
                case BACnetPropertyIdentifier.LAST_RESTART_REASON:
                    return "LAST_RESTART_REASON";
                case BACnetPropertyIdentifier.LAST_RESTORE_TIME:
                    return "LAST_RESTORE_TIME";
                case BACnetPropertyIdentifier.LAST_USE_TIME:
                    return "LAST_USE_TIME";
                case BACnetPropertyIdentifier.LIFE_SAFETY_ALARM_VALUES:
                    return "LIFE_SAFETY_ALARM_VALUES";
                case BACnetPropertyIdentifier.LIGHTING_COMMAND:
                    return "LIGHTING_COMMAND";
                case BACnetPropertyIdentifier.LIGHTING_COMMAND_DEFAULT_PRIORITY:
                    return "LIGHTING_COMMAND_DEFAULT_PRIORITY";
                case BACnetPropertyIdentifier.LIMIT_ENABLE:
                    return "LIMIT_ENABLE";
                case BACnetPropertyIdentifier.LIMIT_MONITORING_INTERVAL:
                    return "LIMIT_MONITORING_INTERVAL";
                case BACnetPropertyIdentifier.LIST_OF_GROUP_MEMBERS:
                    return "LIST_OF_GROUP_MEMBERS";
                case BACnetPropertyIdentifier.LIST_OF_OBJECT_PROPERTY_REFERENCES:
                    return "LIST_OF_OBJECT_PROPERTY_REFERENCES";
                case BACnetPropertyIdentifier.LOCAL_DATE:
                    return "LOCAL_DATE";
                case BACnetPropertyIdentifier.LOCAL_FORWARDING_ONLY:
                    return "LOCAL_FORWARDING_ONLY";
                case BACnetPropertyIdentifier.LOCAL_TIME:
                    return "LOCAL_TIME";
                case BACnetPropertyIdentifier.LOCATION:
                    return "LOCATION";
                case BACnetPropertyIdentifier.LOCK_STATUS:
                    return "LOCK_STATUS";
                case BACnetPropertyIdentifier.LOCKOUT:
                    return "LOCKOUT";
                case BACnetPropertyIdentifier.LOCKOUT_RELINQUISH_TIME:
                    return "LOCKOUT_RELINQUISH_TIME";
                case BACnetPropertyIdentifier.LOG_BUFFER:
                    return "LOG_BUFFER";
                case BACnetPropertyIdentifier.LOG_DEVICE_OBJECT_PROPERTY:
                    return "LOG_DEVICE_OBJECT_PROPERTY";
                case BACnetPropertyIdentifier.LOG_INTERVAL:
                    return "LOG_INTERVAL";
                case BACnetPropertyIdentifier.LOGGING_OBJECT:
                    return "LOGGING_OBJECT";
                case BACnetPropertyIdentifier.LOGGING_RECORD:
                    return "LOGGING_RECORD";
                case BACnetPropertyIdentifier.LOGGING_TYPE:
                    return "LOGGING_TYPE";
                case BACnetPropertyIdentifier.LOW_LIMIT:
                    return "LOW_LIMIT";
                case BACnetPropertyIdentifier.MAINTENANCE_REQUIRED:
                    return "MAINTENANCE_REQUIRED";
                case BACnetPropertyIdentifier.MANIPULATED_VARIABLE_REFERENCE:
                    return "MANIPULATED_VARIABLE_REFERENCE";
                case BACnetPropertyIdentifier.MANUAL_SLAVE_ADDRESS_BINDING:
                    return "MANUAL_SLAVE_ADDRESS_BINDING";
                case BACnetPropertyIdentifier.MASKED_ALARM_VALUES:
                    return "MASKED_ALARM_VALUES";
                case BACnetPropertyIdentifier.MAXIMUM_OUTPUT:
                    return "MAXIMUM_OUTPUT";
                case BACnetPropertyIdentifier.MAXIMUM_VALUE:
                    return "MAXIMUM_VALUE";
                case BACnetPropertyIdentifier.MAXIMUM_VALUE_TIMESTAMP:
                    return "MAXIMUM_VALUE_TIMESTAMP";
                case BACnetPropertyIdentifier.MAX_ACTUAL_VALUE:
                    return "MAX_ACTUAL_VALUE";
                case BACnetPropertyIdentifier.MAX_APDU_LENGTH_ACCEPTED:
                    return "MAX_APDU_LENGTH_ACCEPTED";
                case BACnetPropertyIdentifier.MAX_FAILED_ATTEMPTS:
                    return "MAX_FAILED_ATTEMPTS";
                case BACnetPropertyIdentifier.MAX_INFO_FRAMES:
                    return "MAX_INFO_FRAMES";
                case BACnetPropertyIdentifier.MAX_MASTER:
                    return "MAX_MASTER";
                case BACnetPropertyIdentifier.MAX_PRES_VALUE:
                    return "MAX_PRES_VALUE";
                case BACnetPropertyIdentifier.MAX_SEGMENTS_ACCEPTED:
                    return "MAX_SEGMENTS_ACCEPTED";
                case BACnetPropertyIdentifier.MEMBER_OF:
                    return "MEMBER_OF";
                case BACnetPropertyIdentifier.MEMBER_STATUS_FLAGS:
                    return "MEMBER_STATUS_FLAGS";
                case BACnetPropertyIdentifier.MEMBERS:
                    return "MEMBERS";
                case BACnetPropertyIdentifier.MINIMUM_OFF_TIME:
                    return "MINIMUM_OFF_TIME";
                case BACnetPropertyIdentifier.MINIMUM_ON_TIME:
                    return "MINIMUM_ON_TIME";
                case BACnetPropertyIdentifier.MINIMUM_OUTPUT:
                    return "MINIMUM_OUTPUT";
                case BACnetPropertyIdentifier.MINIMUM_VALUE:
                    return "MINIMUM_VALUE";
                case BACnetPropertyIdentifier.MINIMUM_VALUE_TIMESTAMP:
                    return "MINIMUM_VALUE_TIMESTAMP";
                case BACnetPropertyIdentifier.MIN_ACTUAL_VALUE:
                    return "MIN_ACTUAL_VALUE";
                case BACnetPropertyIdentifier.MIN_PRES_VALUE:
                    return "MIN_PRES_VALUE";
                case BACnetPropertyIdentifier.MODE:
                    return "MODE";
                case BACnetPropertyIdentifier.MODEL_NAME:
                    return "MODEL_NAME";
                case BACnetPropertyIdentifier.MODIFICATION_DATE:
                    return "MODIFICATION_DATE";
                case BACnetPropertyIdentifier.MUSTER_POINT:
                    return "MUSTER_POINT";
                case BACnetPropertyIdentifier.NEGATIVE_ACCESS_RULES:
                    return "NEGATIVE_ACCESS_RULES";
                case BACnetPropertyIdentifier.NETWORK_ACCESS_SECURITY_POLICIES:
                    return "NETWORK_ACCESS_SECURITY_POLICIES";
                case BACnetPropertyIdentifier.NODE_SUBTYPE:
                    return "NODE_SUBTYPE";
                case BACnetPropertyIdentifier.NODE_TYPE:
                    return "NODE_TYPE";
                case BACnetPropertyIdentifier.NOTIFICATION_CLASS:
                    return "NOTIFICATION_CLASS";
                case BACnetPropertyIdentifier.NOTIFICATION_THRESHOLD:
                    return "NOTIFICATION_THRESHOLD";
                case BACnetPropertyIdentifier.NOTIFY_TYPE:
                    return "NOTIFY_TYPE";
                case BACnetPropertyIdentifier.NUMBER_OF_APDU_RETRIES:
                    return "NUMBER_OF_APDU_RETRIES";
                case BACnetPropertyIdentifier.NUMBER_OF_AUTHENTICATION_POLICIES:
                    return "NUMBER_OF_AUTHENTICATION_POLICIES";
                case BACnetPropertyIdentifier.NUMBER_OF_STATES:
                    return "NUMBER_OF_STATES";
                case BACnetPropertyIdentifier.OBJECT_IDENTIFIER:
                    return "OBJECT_IDENTIFIER";
                case BACnetPropertyIdentifier.OBJECT_LIST:
                    return "OBJECT_LIST";
                case BACnetPropertyIdentifier.OBJECT_NAME:
                    return "OBJECT_NAME";
                case BACnetPropertyIdentifier.OBJECT_PROPERTY_REFERENCE:
                    return "OBJECT_PROPERTY_REFERENCE";
                case BACnetPropertyIdentifier.OBJECT_TYPE:
                    return "OBJECT_TYPE";
                case BACnetPropertyIdentifier.OCCUPANCY_COUNT:
                    return "OCCUPANCY_COUNT";
                case BACnetPropertyIdentifier.OCCUPANCY_COUNT_ADJUST:
                    return "OCCUPANCY_COUNT_ADJUST";
                case BACnetPropertyIdentifier.OCCUPANCY_COUNT_ENABLE:
                    return "OCCUPANCY_COUNT_ENABLE";
                case BACnetPropertyIdentifier.OCCUPANCY_LOWER_LIMIT:
                    return "OCCUPANCY_LOWER_LIMIT";
                case BACnetPropertyIdentifier.OCCUPANCY_LOWER_LIMIT_ENFORCED:
                    return "OCCUPANCY_LOWER_LIMIT_ENFORCED";
                case BACnetPropertyIdentifier.OCCUPANCY_STATE:
                    return "OCCUPANCY_STATE";
                case BACnetPropertyIdentifier.OCCUPANCY_UPPER_LIMIT:
                    return "OCCUPANCY_UPPER_LIMIT";
                case BACnetPropertyIdentifier.OCCUPANCY_UPPER_LIMIT_ENFORCED:
                    return "OCCUPANCY_UPPER_LIMIT_ENFORCED";
                case BACnetPropertyIdentifier.OPERATION_EXPECTED:
                    return "OPERATION_EXPECTED";
                case BACnetPropertyIdentifier.OPTIONAL:
                    return "OPTIONAL";
                case BACnetPropertyIdentifier.OUT_OF_SERVICE:
                    return "OUT_OF_SERVICE";
                case BACnetPropertyIdentifier.OUTPUT_UNITS:
                    return "OUTPUT_UNITS";
                case BACnetPropertyIdentifier.PACKET_REORDER_TIME:
                    return "PACKET_REORDER_TIME";
                case BACnetPropertyIdentifier.PASSBACK_MODE:
                    return "PASSBACK_MODE";
                case BACnetPropertyIdentifier.PASSBACK_TIMEOUT:
                    return "PASSBACK_TIMEOUT";
                case BACnetPropertyIdentifier.POLARITY:
                    return "POLARITY";
                case BACnetPropertyIdentifier.PORT_FILTER:
                    return "PORT_FILTER";
                case BACnetPropertyIdentifier.POSITIVE_ACCESS_RULES:
                    return "POSITIVE_ACCESS_RULES";
                case BACnetPropertyIdentifier.POWER:
                    return "POWER";
                case BACnetPropertyIdentifier.PRESCALE:
                    return "PRESCALE";
                case BACnetPropertyIdentifier.PRESENT_VALUE:
                    return "PRESENT_VALUE";
                case BACnetPropertyIdentifier.PRIORITY:
                    return "PRIORITY";
                case BACnetPropertyIdentifier.PRIORITY_ARRAY:
                    return "PRIORITY_ARRAY";
                case BACnetPropertyIdentifier.PRIORITY_FOR_WRITING:
                    return "PRIORITY_FOR_WRITING";
                case BACnetPropertyIdentifier.PROCESS_IDENTIFIER:
                    return "PROCESS_IDENTIFIER";
                case BACnetPropertyIdentifier.PROCESS_IDENTIFIER_FILTER:
                    return "PROCESS_IDENTIFIER_FILTER";
                case BACnetPropertyIdentifier.PROFILE_NAME:
                    return "PROFILE_NAME";
                case BACnetPropertyIdentifier.PROGRAM_CHANGE:
                    return "PROGRAM_CHANGE";
                case BACnetPropertyIdentifier.PROGRAM_LOCATION:
                    return "PROGRAM_LOCATION";
                case BACnetPropertyIdentifier.PROGRAM_STATE:
                    return "PROGRAM_STATE";
                case BACnetPropertyIdentifier.PROPERTY_LIST:
                    return "PROPERTY_LIST";
                case BACnetPropertyIdentifier.PROPORTIONAL_CONSTANT:
                    return "PROPORTIONAL_CONSTANT";
                case BACnetPropertyIdentifier.PROPORTIONAL_CONSTANT_UNITS:
                    return "PROPORTIONAL_CONSTANT_UNITS";
                case BACnetPropertyIdentifier.PROTOCOL_OBJECT_TYPES_SUPPORTED:
                    return "PROTOCOL_OBJECT_TYPES_SUPPORTED";
                case BACnetPropertyIdentifier.PROTOCOL_REVISION:
                    return "PROTOCOL_REVISION";
                case BACnetPropertyIdentifier.PROTOCOL_SERVICES_SUPPORTED:
                    return "PROTOCOL_SERVICES_SUPPORTED";
                case BACnetPropertyIdentifier.PROTOCOL_VERSION:
                    return "PROTOCOL_VERSION";
                case BACnetPropertyIdentifier.PULSE_RATE:
                    return "PULSE_RATE";
                case BACnetPropertyIdentifier.READ_ONLY:
                    return "READ_ONLY";
                case BACnetPropertyIdentifier.REASON_FOR_DISABLE:
                    return "REASON_FOR_DISABLE";
                case BACnetPropertyIdentifier.REASON_FOR_HALT:
                    return "REASON_FOR_HALT";
                case BACnetPropertyIdentifier.RECIPIENT_LIST:
                    return "RECIPIENT_LIST";
                case BACnetPropertyIdentifier.RECORDS_SINCE_NOTIFICATION:
                    return "RECORDS_SINCE_NOTIFICATION";
                case BACnetPropertyIdentifier.RECORD_COUNT:
                    return "RECORD_COUNT";
                case BACnetPropertyIdentifier.RELIABILITY:
                    return "RELIABILITY";
                case BACnetPropertyIdentifier.RELIABILITY_EVALUATION_INHIBIT:
                    return "RELIABILITY_EVALUATION_INHIBIT";
                case BACnetPropertyIdentifier.RELINQUISH_DEFAULT:
                    return "RELINQUISH_DEFAULT";
                case BACnetPropertyIdentifier.REQUESTED_SHED_LEVEL:
                    return "REQUESTED_SHED_LEVEL";
                case BACnetPropertyIdentifier.REQUESTED_UPDATE_INTERVAL:
                    return "REQUESTED_UPDATE_INTERVAL";
                case BACnetPropertyIdentifier.REQUIRED:
                    return "REQUIRED";
                case BACnetPropertyIdentifier.RESOLUTION:
                    return "RESOLUTION";
                case BACnetPropertyIdentifier.RESTART_NOTIFICATION_RECIPIENTS:
                    return "RESTART_NOTIFICATION_RECIPIENTS";
                case BACnetPropertyIdentifier.RESTORE_COMPLETION_TIME:
                    return "RESTORE_COMPLETION_TIME";
                case BACnetPropertyIdentifier.RESTORE_PREPARATION_TIME:
                    return "RESTORE_PREPARATION_TIME";
                case BACnetPropertyIdentifier.SCALE:
                    return "SCALE";
                case BACnetPropertyIdentifier.SCALE_FACTOR:
                    return "SCALE_FACTOR";
                case BACnetPropertyIdentifier.SCHEDULE_DEFAULT:
                    return "SCHEDULE_DEFAULT";
                case BACnetPropertyIdentifier.SECURED_STATUS:
                    return "SECURED_STATUS";
                case BACnetPropertyIdentifier.SECURITY_PDU_TIMEOUT:
                    return "SECURITY_PDU_TIMEOUT";
                case BACnetPropertyIdentifier.SECURITY_TIME_WINDOW:
                    return "SECURITY_TIME_WINDOW";
                case BACnetPropertyIdentifier.SEGMENTATION_SUPPORTED:
                    return "SEGMENTATION_SUPPORTED";
                case BACnetPropertyIdentifier.SERIAL_NUMBER:
                    return "SERIAL_NUMBER";
                case BACnetPropertyIdentifier.SETPOINT:
                    return "SETPOINT";
                case BACnetPropertyIdentifier.SETPOINT_REFERENCE:
                    return "SETPOINT_REFERENCE";
                case BACnetPropertyIdentifier.SETTING:
                    return "SETTING";
                case BACnetPropertyIdentifier.SHED_DURATION:
                    return "SHED_DURATION";
                case BACnetPropertyIdentifier.SHED_LEVEL_DESCRIPTIONS:
                    return "SHED_LEVEL_DESCRIPTIONS";
                case BACnetPropertyIdentifier.SHED_LEVELS:
                    return "SHED_LEVELS";
                case BACnetPropertyIdentifier.SILENCED:
                    return "SILENCED";
                case BACnetPropertyIdentifier.SLAVE_ADDRESS_BINDING:
                    return "SLAVE_ADDRESS_BINDING";
                case BACnetPropertyIdentifier.SLAVE_PROXY_ENABLE:
                    return "SLAVE_PROXY_ENABLE";
                case BACnetPropertyIdentifier.START_TIME:
                    return "START_TIME";
                case BACnetPropertyIdentifier.STATE_DESCRIPTION:
                    return "STATE_DESCRIPTION";
                case BACnetPropertyIdentifier.STATE_TEXT:
                    return "STATE_TEXT";
                case BACnetPropertyIdentifier.STATUS_FLAGS:
                    return "STATUS_FLAGS";
                case BACnetPropertyIdentifier.STOP_TIME:
                    return "STOP_TIME";
                case BACnetPropertyIdentifier.STOP_WHEN_FULL:
                    return "STOP_WHEN_FULL";
                case BACnetPropertyIdentifier.STRUCTURED_OBJECT_LIST:
                    return "STRUCTURED_OBJECT_LIST";
                case BACnetPropertyIdentifier.SUBORDINATE_ANNOTATIONS:
                    return "SUBORDINATE_ANNOTATIONS";
                case BACnetPropertyIdentifier.SUBORDINATE_LIST:
                    return "SUBORDINATE_LIST";
                case BACnetPropertyIdentifier.SUBSCRIBED_RECIPIENTS:
                    return "SUBSCRIBED_RECIPIENTS";
                case BACnetPropertyIdentifier.SUPPORTED_FORMATS:
                    return "SUPPORTED_FORMATS";
                case BACnetPropertyIdentifier.SUPPORTED_FORMAT_CLASSES:
                    return "SUPPORTED_FORMAT_CLASSES";
                case BACnetPropertyIdentifier.SUPPORTED_SECURITY_ALGORITHMS:
                    return "SUPPORTED_SECURITY_ALGORITHMS";
                case BACnetPropertyIdentifier.SYSTEM_STATUS:
                    return "SYSTEM_STATUS";
                case BACnetPropertyIdentifier.THREAT_AUTHORITY:
                    return "THREAT_AUTHORITY";
                case BACnetPropertyIdentifier.THREAT_LEVEL:
                    return "THREAT_LEVEL";
                case BACnetPropertyIdentifier.TIME_DELAY:
                    return "TIME_DELAY";
                case BACnetPropertyIdentifier.TIME_DELAY_NORMAL:
                    return "TIME_DELAY_NORMAL";
                case BACnetPropertyIdentifier.TIME_OF_ACTIVE_TIME_RESET:
                    return "TIME_OF_ACTIVE_TIME_RESET";
                case BACnetPropertyIdentifier.TIME_OF_DEVICE_RESTART:
                    return "TIME_OF_DEVICE_RESTART";
                case BACnetPropertyIdentifier.TIME_OF_STATE_COUNT_RESET:
                    return "TIME_OF_STATE_COUNT_RESET";
                case BACnetPropertyIdentifier.TIME_SYNCHRONIZATION_INTERVAL:
                    return "TIME_SYNCHRONIZATION_INTERVAL";
                case BACnetPropertyIdentifier.TIME_SYNCHRONIZATION_RECIPIENTS:
                    return "TIME_SYNCHRONIZATION_RECIPIENTS";
                case BACnetPropertyIdentifier.TOTAL_RECORD_COUNT:
                    return "TOTAL_RECORD_COUNT";
                case BACnetPropertyIdentifier.TRACE_FLAG:
                    return "TRACE_FLAG";
                case BACnetPropertyIdentifier.TRACKING_VALUE:
                    return "TRACKING_VALUE";
                case BACnetPropertyIdentifier.TRANSACTION_NOTIFICATION_CLASS:
                    return "TRANSACTION_NOTIFICATION_CLASS";
                case BACnetPropertyIdentifier.TRANSITION:
                    return "TRANSITION";
                case BACnetPropertyIdentifier.TRIGGER:
                    return "TRIGGER";
                case BACnetPropertyIdentifier.UNITS:
                    return "UNITS";
                case BACnetPropertyIdentifier.UPDATE_INTERVAL:
                    return "UPDATE_INTERVAL";
                case BACnetPropertyIdentifier.UPDATE_KEY_SET_TIMEOUT:
                    return "UPDATE_KEY_SET_TIMEOUT";
                case BACnetPropertyIdentifier.UPDATE_TIME:
                    return "UPDATE_TIME";
                case BACnetPropertyIdentifier.USER_EXTERNAL_IDENTIFIER:
                    return "USER_EXTERNAL_IDENTIFIER";
                case BACnetPropertyIdentifier.USER_INFORMATION_REFERENCE:
                    return "USER_INFORMATION_REFERENCE";
                case BACnetPropertyIdentifier.USER_NAME:
                    return "USER_NAME";
                case BACnetPropertyIdentifier.USER_TYPE:
                    return "USER_TYPE";
                case BACnetPropertyIdentifier.USES_REMAINING:
                    return "USES_REMAINING";
                case BACnetPropertyIdentifier.UTC_OFFSET:
                    return "UTC_OFFSET";
                case BACnetPropertyIdentifier.UTC_TIME_SYNCHRONIZATION_RECIPIENTS:
                    return "UTC_TIME_SYNCHRONIZATION_RECIPIENTS";
                case BACnetPropertyIdentifier.VALID_SAMPLES:
                    return "VALID_SAMPLES";
                case BACnetPropertyIdentifier.VALUE_BEFORE_CHANGE:
                    return "VALUE_BEFORE_CHANGE";
                case BACnetPropertyIdentifier.VALUE_SET:
                    return "VALUE_SET";
                case BACnetPropertyIdentifier.VALUE_CHANGE_TIME:
                    return "VALUE_CHANGE_TIME";
                case BACnetPropertyIdentifier.VARIANCE_VALUE:
                    return "VARIANCE_VALUE";
                case BACnetPropertyIdentifier.VENDOR_IDENTIFIER:
                    return "VENDOR_IDENTIFIER";
                case BACnetPropertyIdentifier.VENDOR_NAME:
                    return "VENDOR_NAME";
                case BACnetPropertyIdentifier.VERIFICATION_TIME:
                    return "VERIFICATION_TIME";
                case BACnetPropertyIdentifier.VT_CLASSES_SUPPORTED:
                    return "VT_CLASSES_SUPPORTED";
                case BACnetPropertyIdentifier.WEEKLY_SCHEDULE:
                    return "WEEKLY_SCHEDULE";
                case BACnetPropertyIdentifier.WINDOW_INTERVAL:
                    return "WINDOW_INTERVAL";
                case BACnetPropertyIdentifier.WINDOW_SAMPLES:
                    return "WINDOW_SAMPLES";
                case BACnetPropertyIdentifier.WRITE_STATUS:
                    return "WRITE_STATUS";
                case BACnetPropertyIdentifier.ZONE_FROM:
                    return "ZONE_FROM";
                case BACnetPropertyIdentifier.ZONE_MEMBERS:
                    return "ZONE_MEMBERS";
                case BACnetPropertyIdentifier.ZONE_TO:
                    return "ZONE_TO";
            }
            return "NAN_" + id.ToString();
        }

        public override string ToString()
        {
            return "<PropertyId>" + id + "</PropertyId>";
        }
    
    }
}
