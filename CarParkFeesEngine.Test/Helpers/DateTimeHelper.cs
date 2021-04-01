using System;

namespace CarParkFeesEngine.Test.Helpers
{
    public class DateTimeHelpers
    {
        private static readonly Random rnd = new Random();
        public static DateTime GetRandomDateTimeBetweenRange(DateTime from, DateTime to)
        {
            var range = to - from;

            var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));

            return from + randTimeSpan;
        }
    }
}
