using System;
using System.Collections.Generic;
using System.Linq;
using NetLogDNA.LogDnaApi.Dto;
using NetLogDNA.Utils;
using NLog;

namespace NetLogDNA.NLog.LogDna
{
    public class LogLineMapper
    {
        public LogLine Map(LogEventInfo logEventInfo, string message)
        {
            var meta = new Meta()
            {
                Exception = GetExceptionInfo(logEventInfo),
                Tags = GetTags(logEventInfo),
                Properties = logEventInfo.HasProperties ? logEventInfo.Properties : new Dictionary<object, object>()
            };
            
            return new LogLine()
            {
                Line = message,
                Env = logEventInfo.LoggerName,
                Meta = meta,
                Level = MapLogLevel(logEventInfo.Level),
                UnixEpochTimestamp = logEventInfo.TimeStamp.ToUnixTimestamp()
            };
        }

        private ExceptionInfo GetExceptionInfo(LogEventInfo logEventInfo)
        {
            if (logEventInfo.Exception == null)
                return null;
            
            var exceptionInfo = new ExceptionInfo()
            {
                Message = logEventInfo.Exception.Message,
                Name = logEventInfo.Exception.GetType().Name,
                StackTrace = logEventInfo.Exception.StackTrace
            };

            if (logEventInfo.Exception.InnerException != null)
            {
                exceptionInfo.InnerStackTrace = logEventInfo.Exception.InnerException.StackTrace;
                exceptionInfo.InnerMessage = logEventInfo.Exception.InnerException.Message;
                exceptionInfo.InnerName = logEventInfo.Exception.InnerException.GetType().Name;
            }

            return exceptionInfo;
        }

        private string[] GetTags(LogEventInfo logEventInfo)
        {
            if (!logEventInfo.HasProperties || !logEventInfo.Properties.Any(prop => prop.Key.ToString() == "tags"))
                return new string[0];

            return logEventInfo
                .Properties["tags"]
                .ToString()
                .Split(',');
        }

        private LoggingLevel MapLogLevel(LogLevel logLevel)
        {
            if (logLevel == LogLevel.Trace)
                return LoggingLevel.TRACE;
            if (logLevel == LogLevel.Debug)
                return LoggingLevel.DEBUG;
            if (logLevel == LogLevel.Info)
                return LoggingLevel.INFO;
            if (logLevel == LogLevel.Warn)
                return LoggingLevel.WARNING;
            if (logLevel == LogLevel.Error)
                return LoggingLevel.ERROR;
            if (logLevel == LogLevel.Fatal)
                return LoggingLevel.FATAL;

            throw new NotImplementedException($"No mapping exists for log level: {logLevel}");
        }
    }
}