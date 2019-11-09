using System;
using NetLogDNA.LogDnaApi.Dto;
using NetLogDNA.Utils;

namespace NetLogDNA.Test.Builders
{
    public class LogLineBuilder
    {
        public static LogLineBuilder New => new LogLineBuilder();
        
        private string _env;

        private string _line;

        private LoggingLevel _loggingLevel;

        private long _unixTimestamp;

        private Meta _meta;

        private LogLineBuilder()
        {
            _env = "Death.Star";
            _line = "May the force be with you!";
            _loggingLevel = LoggingLevel.DEBUG;
            _unixTimestamp = DateTime.UtcNow.ToUnixTimestamp();
            _meta = MetaBuilder.New.Build();
        }

        public LogLineBuilder WithLine(string line)
        {
            _line = line;
            return this;
        }
        
        public LogLineBuilder WithEnv(string env)
        {
            _env = env;
            return this;
        }
        
        public LogLineBuilder WithUnixTimestamp(long unixTimestamp)
        {
            _unixTimestamp = unixTimestamp;
            return this;
        }
        
        public LogLineBuilder WithLoggingLevel(LoggingLevel loggingLevel)
        {
            _loggingLevel = loggingLevel;
            return this;
        }

        public LogLine Build()
        {
            return new LogLine()
            {
                Env = _env,
                Line = _line,
                Level = _loggingLevel,
                Meta = _meta,
                UnixEpochTimestamp = _unixTimestamp
            };
        }
    }
}