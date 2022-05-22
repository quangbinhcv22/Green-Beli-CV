using System;
using System.Globalization;
using UnityEditor;

namespace Utils
{
    public static class DateTimeUtils
    {
        private static readonly CultureInfo DefaultCulture = System.Globalization.CultureInfo.InvariantCulture;

        public const string FranceFormatDate = "dd-MM-yyyy";
        
        public const string GreenBeliFullDateFormat = "dd-MMM-yyyy HH:mm:ss"; //22-Nov-2021 17:37:37
        public const string GreenBeliOnlyDayFormat = "MMM dd, yyyy";
        public const string FullFranceFormatDate = "dd-MM-yyyy HH:mm:ss";


        public static DateTime ToDateTime(this string formattedDateString, string format)
        {
            try
            {
                return DateTime.ParseExact(formattedDateString, format, DefaultCulture);
            }
            catch
            {
                return new DateTime();                
            }
        }

        public static string ConvertToFormattedDateString(DateTime dateTime, string format)
        {
            return dateTime.ToString(format);
        }

        public static int DistanceDay(DateTime startDate, DateTime endDate)
        {
            return (endDate.Date - startDate.Date).Days;
        }

        public static int DistanceDay(string formattedStartDateString, string formattedEndDateString, string format)
        {
            var startDate = ToDateTime(formattedStartDateString, format);
            var endDate = ToDateTime(formattedEndDateString, format);

            return DistanceDay(startDate, endDate);
        }

        public static int DistanceDayFromToday(this DateTime dateTime)
        {
            return DistanceDay(DateTime.Today.ToUniversalTime(), dateTime);
        }

        public static int DistanceDayFromToday(string formattedEndDateString, string format)
        {
            var startDate = DateTime.Today.ToUniversalTime();
            var endDate = ToDateTime(formattedEndDateString, format);

            return DistanceDay(startDate, endDate);
        }

        public static bool IsTodayOrPast(this DateTime dateTime)
        {
            return DistanceDayFromToday(dateTime) <= 0;
        }

        public static DateTime FromVietnameseTimeToGmt(this DateTime dateTime)
        {
            var difference = new TimeSpan(hours: 7, minutes: 0, seconds: 0);
            return dateTime - difference;
        }

        public static string FromVietnameseDateToFormattedGmtString(this string originalString, string originalFormat, string newFormat)
        {
           return originalString.ToDateTime(originalFormat).FromVietnameseTimeToGmt().ToString(newFormat);
        }
    }
}