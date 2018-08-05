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
    class BACVnetfloatingpointmath : BACVnetVar
    { 
        UInt32 hexValue;


        public String Decode(List<byte> cm, int length)
        {
            hexValue = cm[0];
            for (int x = 1; x < length; x++)
            {
                hexValue *= 0x0100;
                hexValue += cm[x];
            }

            String binVal = "";
            for (int x = 0; x < 32; x++)
            {
                binVal += ((hexValue & 0x80000000U) > 0U) ? "1" : "0";
                hexValue <<= 1;
            }

            return Decode(binVal);
        }


        public String Decode(String bPatt)
        {
            UInt32 ieee754 = 0;
            int lcnt = bPatt.Length;
            foreach (byte bit in bPatt)
            {
                ieee754 += (UInt32)('1' == bit ? 1 : 0);
                if (--lcnt > 0)
                    ieee754 <<= 1;
            }

            Boolean signNeg = ieee754 >> 31 == 1;
            byte exp = (byte)((ieee754 & 0x7F800000U) >> 23);
            String strmantissa22 = bPatt.Substring(9);

            strmantissa22 = new String(strmantissa22.Reverse().ToArray());

            float flt1 = 0.0f;
            foreach (byte bit in strmantissa22)
            {
                flt1 += ('1' == bit ? 1 : 0);
                flt1 /= 2;
                System.Console.WriteLine(flt1.ToString());
            }

            flt1 += 1;

            if (exp >> 7 == 1) 
            for (int x = exp - 127; x > 0; x--)
            {
                flt1 *= 2f;
                System.Console.WriteLine("pos exp flt1 = " + flt1.ToString());
            }
            else
            for (int x = exp - 127; x < 0; x++)
            {
                flt1 /= 2f;
                System.Console.WriteLine("neg exp flt1 = " + flt1.ToString());
            }

            return flt1.ToString();
        }

        public String Encode(float flt1)
        {
            Boolean signNeg = System.Math.Sign(flt1) == -1;
            if (signNeg == true)
                flt1 = System.Math.Abs(flt1);
            byte cnt = 0;
            System.Console.WriteLine("normalize flt1 = " + flt1.ToString());
            // Stage 1 - Normilize to the 1's form of sinitific notation to find the exponent  
            Boolean bWholeNum = (System.Math.Truncate(flt1) >= 1);
            while (true)
            {
                if (System.Math.Truncate(flt1) == 1)
                {
                    break;
                }
                cnt++;
                if (bWholeNum == true)
                    flt1 /= 2;
                else
                    flt1 *= 2;
                System.Console.WriteLine("normalize flt1 = " + flt1.ToString());
            }
            System.Console.WriteLine("Exp = " + cnt.ToString());

            byte exp = 127;
            if (bWholeNum == true)
                exp += cnt;
            else
                exp -= cnt;

            UInt32 mantissa22 = 0;
            cnt = 0;
            flt1 -= 1;
            while (true)
            {
                if (System.Math.Truncate(flt1) == 1)
                {
                    System.Console.WriteLine("man flt1 = " + flt1.ToString() + " add 1");
                    mantissa22 += 1;
                    flt1 -= 1;
                }
                else
                    System.Console.WriteLine("man flt1 = " + flt1.ToString() + " add 0");

                if (flt1 == 0.0f)
                {
                    mantissa22 <<= (23 - cnt);
                    break;
                }
                if (cnt++ >= 23) break;
                mantissa22 *= 2;
                flt1 *= 2;
                System.Console.WriteLine("man flt1 = " + flt1.ToString());
            }

            if (signNeg == true)
                mantissa22 += (1U << 31);

            mantissa22 += ((UInt32)exp) << 23;

            String binVal = "";
            for (int x=0; x < 32; x++)
            {
                binVal += ((mantissa22 & 0x80000000U) > 0U) ? "1" : "0";
                mantissa22 <<= 1;
            }


            return binVal;
        }

    }
}
