using System;

namespace NetLogDNA.Utils
{
    public static class DateTimeExtensions
    {
        public static long ToUnixTimestamp(this DateTime dt)
        {
            return new DateTimeOffset(dt).ToUnixTimeMilliseconds();
        }
    }
}