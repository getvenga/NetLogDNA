using System;
using System.Collections.Generic;
using NLog;

namespace NetLogDNA.NLog.Test.Builders
{
    public class LogEventInfoBuilder
    {
        public static LogEventInfoBuilder New => new LogEventInfoBuilder();
        
        private Exception _exception;

        private string _loggerName;

        private string _message;

        private LogLevel _logLevel;

        private IDictionary<object, object> _properties;

        private LogEventInfoBuilder()
        {
            _exception = null;
            _loggerName = "NLog.Test";
            _message = "May the force be with you";
            _logLevel = LogLevel.Info;
            _properties = new Dictionary<object, object>();
        }

        public LogEventInfoBuilder WithLogLevel(LogLevel logLevel)
        {
            _logLevel = logLevel;
            return this;
        }
        
        public LogEventInfoBuilder WithMessage(string message)
        {
            _message = message;
            return this;
        }
        
        public LogEventInfoBuilder WithLoggerName(string loggerName)
        {
            _loggerName = loggerName;
            return this;
        }

        public LogEventInfoBuilder AddProperty(object key, object value)
        {
            _properties.Add(key, value);
            return this;
        }

        public LogEventInfoBuilder WithException(Exception exception)
        {
            _exception = exception;
            return this;
        }
        
        public LogEventInfo Build()
        {
            var logEventInfo = new LogEventInfo()
            {
                Message = _message,
                Exception = _exception,
                Level = _logLevel,
                LoggerName = _loggerName
            };

            foreach (var property in _properties)
                logEventInfo.Properties.Add(property);

            return logEventInfo;
        }
    }
}