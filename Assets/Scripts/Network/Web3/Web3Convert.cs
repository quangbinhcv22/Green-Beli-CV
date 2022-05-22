using System;
using System.Globalization;
using System.Numerics;

namespace Network.Web3
{
    public static class Web3Convert
    {
        public static BigInteger IntToEther(int value)
        {
            return BigInteger.Parse($"{value}000000000000000000");
        }


        public static int EtherToInt(string value, int decimals = 18)
        {
            if (value.Length < decimals)
            {
                return 0;
            }

            var numberString = value.Substring(0, value.Length - decimals);

            if (decimals == 18)
            {
                return Int32.Parse(numberString);
            }
            else
            {
                return (int)(Int64.Parse(numberString) / 1000000000);
            }
        }
    }
}