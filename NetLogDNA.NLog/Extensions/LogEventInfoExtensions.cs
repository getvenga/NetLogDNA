using NLog.Fluent;

namespace NetLogDNA.NLog.Extensions
{
    public static class LogEventInfoExtensions
    {
        internal const string LogDnaTagPropertyName = "__logDnaTags__";

        public static LogBuilder SetLogDnaTags(this LogBuilder me, params string[] logDnaTags)
        {
            return logDnaTags != null
                ? me.Property(LogDnaTagPropertyName, logDnaTags)
                : me.Property(LogDnaTagPropertyName, new string[0]);
        }
    }
}