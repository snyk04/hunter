using System;

namespace Hunter.Common
{
    public static class DateTimeExtensions
    {
        public static double GetPassedSeconds(this DateTime timePoint)
        {
            return DateTime.Now.Subtract(timePoint).TotalSeconds;
        }
    }
}
