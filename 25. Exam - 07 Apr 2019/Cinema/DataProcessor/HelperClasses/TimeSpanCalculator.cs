namespace Cinema.DataProcessor.HelperClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TimeSpanCalculator
    {
        public static string TotalTime(this IEnumerable<TimeSpan> durations)
        {
            int i = 0;
            int TotalSeconds = 0;

            var ArrayDuration = durations.ToArray();

            for (i = 0; i < ArrayDuration.Length; i++)
            {
                TotalSeconds = (int)(ArrayDuration[i].TotalSeconds) + TotalSeconds;
            }

            return TimeSpan.FromSeconds(TotalSeconds).ToString(@"hh\:mm\:ss");
        }
    }
}
