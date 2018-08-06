# BACnet-packet-XML-translator-encoder-decoder

The code is C# written with a free version of Visual Studio and is now ported to the community edition.

The Project date back several years 2014 when I was looking to work with BACnet technology.

This was a work in progress and was intended to study and translate the BACnet Stack (packet) structure by translating it to XML and back into the raw packet format.

Rather than throw this work away I have decided to open source the work as it works on a whole (translates the data to and from XML) but was not totally vetted out. 

Running the module named TestTheBasics.cs will run through a series of translations encoding and decoding BACnet communications I recorded at the time and can serve as basic test cases and can be turned into Unit tests.

IEEE 754 Floating-Point Arithmetic translating are found in BACVnetfloatingpointmath.cs
