using System;
using System.Globalization;

namespace Utils
{
    public static class NumberUtils
    {
        public static string FormattedNumber(float numberUnFormatted)
        {
            return $"{numberUnFormatted:N0}";
        }

        public static string FormattedNumber(float numberUnFormatted, int round)
        {
            return ((float)Math.Round(numberUnFormatted, round)).ToString(CultureInfo.InvariantCulture);
        }

        public static string FormattedPercent(float numberUnFormatted)
        {
            return $"{((float)Math.Round(numberUnFormatted, 3)) * 100}%";
        }

        public static string FormattedFraction(float numerator, float denominator)
        {
            return $"{numerator:N0} / {denominator:N0}";
        }
    }
}