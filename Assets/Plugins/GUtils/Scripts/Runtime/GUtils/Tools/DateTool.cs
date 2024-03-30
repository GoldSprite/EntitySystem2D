using System;

namespace GoldSprite.GUtils {
    public class DateTool {
        public static string simpleFormat = "[yy/MM/dd-HH:mm:ss:fff] ";
        public static DateTime dateTime;
        public static string CurrentFormatTime()
        {
            return DateTime.Now.ToString(simpleFormat);
        }

        public static string FormatTimeByMillis(long millis)
        {
            return new DateTime(millis * TimeSpan.TicksPerMillisecond).ToString(simpleFormat);
        }

        public static long CurrentTimeMillis()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }
    }
}