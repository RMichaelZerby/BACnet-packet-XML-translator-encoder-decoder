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
using System.Text;
using System.Xml;
using System.Linq;
using System.Xml.Linq;


namespace BACcom
{
    using System.Xml.Linq;
    using ConfirmedServiceChoice = BACnetConfirmedServiceChoice;
    using System.Collections.Generic;

    class PDUConfirmedService : PDU
    { // 20.1.2 BACnet-Confirmed-Request-PDU

        private Boolean segMessage = false;  // 20.1.2.1 segmented-message
        private Boolean segResponseAccp = false;  // 20.1.2.3 segmented-response-accepted
        private Boolean moreFollows = false; // 20.1.2.2 more-follows
        private MAX_SEGMENTS_ACCEPTED maxSegmentsAccepted = 0; // 20.1.2.4 max-segments-accepted
        private MAX_APDU_LENGTH_ACCEPTED maxAPDUlengthAccepted = MAX_APDU_LENGTH_ACCEPTED.UPTO1024; // 20.1.2.5 max-APDU-length-accepted
        private byte invokeID = 0; // 20.1.2.6 invokeID
        private byte sequenceNumber = 0; // 20.1.2.7 sequence-number
        private byte proposedWindowSize = 0; // 20.1.2.8 proposed-window-size
        private ConfirmedServiceChoice serviceChoice = 0; // 20.1.2.9 service-choice - BACnetConfirmedServiceChoice
        // What follows - This parameter shall contain the parameters of the specific service that is being requested, encoded according to the rules of 20.2.
        APDUService apduService = new APDUService();


        new public PDU Decode(BACPacket cm)
        {
            System.Console.Write("Decoding PDUComplexAck");
            byte a = cm.getNextByte();
            segMessage = DecodeSegMessage(a);
            moreFollows = DecodeMoreFollows(a);
            segResponseAccp = DecodeSegResponse(a);

            if (segResponseAccp == true) 
            {
                maxSegmentsAccepted = (MAX_SEGMENTS_ACCEPTED)cm.getByte();
            }

            maxAPDUlengthAccepted = DecodeMaxAPDUlengthAccp(cm.getNextByte());

            invokeID = cm.getNextByte();

            // If the optional parameter are present
            if (segMessage) 
            {
                sequenceNumber = cm.getNextByte();
                proposedWindowSize = cm.getNextByte();
            }

            serviceChoice = (ConfirmedServiceChoice)cm.getNextByte();

            switch (serviceChoice)
            {
                case ConfirmedServiceChoice.READPROPERTY:
                    apduService = new APDUReadProperty();
                    apduService.Decode(cm);
                        
                    break;

            }
            return this;
        }


        private Boolean DecodeSegMessage(byte s) 
        {
            return (s & 0x08) == 0x08 ? true: false;
        }

        private Boolean DecodeSegResponse(byte s) 
        {
            return (s & 0x08) == 0x02 ? true: false;
        }

        private Boolean DecodeMoreFollows(byte s)
        {
            return (s & 0x08) == 0x04 ? true : false;
        }

        private MAX_SEGMENTS_ACCEPTED DecodeMaxSegments(byte s)
        {
            return (MAX_SEGMENTS_ACCEPTED)( s >> 4 & 0x03); 
        }

        private MAX_APDU_LENGTH_ACCEPTED DecodeMaxAPDUlengthAccp(byte s)
        {
            return (MAX_APDU_LENGTH_ACCEPTED)( s >> 1 & 0x03); 
        }

        public override List<byte> Encode(XElement xParentElem)
        {
            return packet;
        }



        // page 641 
        //public enum BACnetConfirmedServiceChoice : byte
        //{
        //    // Alarm and Event Services
        //    ACKNOWLEDGEALARM = 0,
        //    CONFIRMEDCOVNOTIFICATION = 1,
        //    CONFIRMEDEVENTNOTIFICATION = 2,
        //    GETALARMSUMMARY = 3,
        //    GETENROLLMENTSUMMARY = 4,
        //    GETEVENTINFORMATION = 29,
        //    SUBSCRIBECOV = 5,
        //    SUBSCRIBECOVPROPERTY = 28,
        //    LIFESAFETYOPERATION = 27,
        //    // File Access Services
        //    ATOMICREADFILE = 6,
        //    ATOMICWRITEFILE = 7,
        //    // Object Access Services
        //    ADDLISTELEMENT = 8,
        //    REMOVELISTELEMENT = 9,
        //    CREATEOBJECT = 10,
        //    DELETEOBJECT = 11,
        //    READPROPERTY = 12,                         // ReadProperty-Request; ReadProperty-ACK 
        //    READPROPERTYMULTIPLE = 14,
        //    READRANGE = 26,
        //    WRITEPROPERTY = 15,
        //    WRITEPROPERTYMULTIPLE = 16,
        //    // Remote Device Management Services
        //    DEVICECOMMUNICATIONCONTROL = 17,
        //    CONFIRMEDPRIVATETRANSFER = 18,
        //    CONFIRMEDTEXTMESSAGE = 19,
        //    REINITIALIZEDEVICE = 20,
        //    // Virtual Terminal Services
        //    VTOPEN = 21,
        //    VTCLOSE = 22,
        //    VTDATA = 23
        //    // Removed Services
        //    // formerly: authenticate (24), removed in version 1 revision 11
        //    // formerly: requestKey (25), removed in version 1 revision 11
        //    // formerly: readPropertyConditional (13), removed in version 1 revision 12
        //    // Services added after 1995
        //    // readRange (26) see Object Access Services
        //    // lifeSafetyOperation (27) see Alarm and Event Services
        //    // subscribeCOVProperty (28) see Alarm and Event Services
        //    // getEventInformation (29) see Alarm and Event Services
        //}

        public enum MAX_SEGMENTS_ACCEPTED : byte // 20.1.2.4 max-segments-accepted
        {
        // B'000' Unspecified number of segments accepted.
            UNSPECIFIED = 0x00,
        // B'001' 2 segments accepted.
            TWO = 0x01,
        // B'010' 4 segments accepted.
            FOUR = 0x2,
        // B'011' 8 segments accepted.
            EIGHT = 0x03,
        // B'100' 16 segments accepted.
            SIXTEEN = 0x04,
        // B'101' 32 segments accepted.
            THIRTY_TWO = 0x05,
        // B'110' 64 segments accepted.
            SIXTY_FOUR = 0x06,
        // B'111' Greater than 64 segments accepted.
            GREATER_THAN_64 = 0x07
        }


        public enum MAX_APDU_LENGTH_ACCEPTED : byte // 20.1.2.5 max-APDU-length-accepted
        {
            // B'0000' Up to MinimumMessageSize (50 octets)
                UPTO50 = 0x00,
            // B'0001' Up to 128 octets
                UPTO128 = 0x01,
            // B'0010' Up to 206 octets (fits in a LonTalk frame)
                UPTO206 = 0x02,
            // B'0011' Up to 480 octets (fits in an ARCNET frame)
                UPTO480 = 0x03,
            // B'0100' Up to 1024 octets
                UPTO1024 = 0x04,
            // B'0101' Up to 1476 octets (fits in an ISO 8802-3 frame)
                UPTO1476 = 0x05
        }


        public override string ToString()
        {
            String sRet;
            sRet = "<" + this.GetType().Name + "> <InvokeID>" + invokeID.ToString() + "</InvokeID>";
            if (segResponseAccp == true)
            {
                sRet += " <maxseg>" + maxSegmentsAccepted + "</maxseg> ";
            }

            sRet += " <maxAPDUlengthAccepted>" + maxAPDUlengthAccepted + "</maxAPDUlengthAccepted> ";

            // If the optional parameter are present
            if (segMessage)
            {
                sRet += " <Seqnmber>" + sequenceNumber.ToString() + "<Seqnmber> <ProposedWindowSize>" + proposedWindowSize.ToString() + "</ProposedWindowSize> ";
            }

            sRet += " <ServiceChoice>" + serviceChoice.ToString() + "</ServiceChoice> ";

            sRet += " </" + this.GetType().Name + "> ";

            sRet += apduService.ToString();

            return sRet;
        }

//  BACnet-Confirmed-Request-PDU ::= SEQUENCE {
//  pdu-type [0] Unsigned (0..15), -- 0 for this PDU type
//  segmented-message [1] BOOLEAN,
//  more-follows [2] BOOLEAN,
//  segmented-response-accepted [3] BOOLEAN,
//  reserved [4] Unsigned (0..3), -- must be set to zero
//  max-segments-accepted [5] Unsigned (0..7), -- as per 20.1.2.4
//  max-APDU-length-accepted [6] Unsigned (0..15), -- as per 20.1.2.5
//  invokeID [7] Unsigned (0..255),
//  sequence-number [8] Unsigned (0..255) OPTIONAL, -- only if segmented msg
//  proposed-window-size [9] Unsigned (1..127) OPTIONAL, -- only if segmented msg
//  service-choice [10] BACnetConfirmedServiceChoice,
//  service-request [11] BACnet-Confirmed-Service-Request
//-- Context specific tags 0..11 are NOT used in header encoding

    }
}
