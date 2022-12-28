using System;

namespace BookPlayer.Helpers
{
    /// <summary>
    /// Helps the parse <see cref="TimeSpan"/>
    /// </summary>
    public static class TimeSpanParser
    {
        private const int HoursInDay = 24;

        public static TimeSpan Parse(string sourceString)
        {
            if (string.IsNullOrWhiteSpace(sourceString))
            {
                return default;
            }

            var splittedTime = sourceString.Split(':');
            if (splittedTime.Length == 3)
            {
                var hours = int.Parse(splittedTime[0]);

                if (hours >= HoursInDay)
                {
                    splittedTime[0] = (hours % HoursInDay).ToString();
                    return TimeSpan.Parse(string.Join(':', splittedTime)) + new TimeSpan(hours / HoursInDay, 0, 0, 0);
                }                
            }
            return TimeSpan.Parse(sourceString);
        }
    }
}
