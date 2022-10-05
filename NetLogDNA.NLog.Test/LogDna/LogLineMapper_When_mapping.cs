using System;
using FluentAssertions;
using NetLogDNA.LogDnaApi.Dto;
using NetLogDNA.NLog.Extensions;
using NetLogDNA.NLog.LogDna;
using NetLogDNA.NLog.Test.Builders;
using NLog;
using Xunit;

namespace NetLogDNA.NLog.Test.LogDna
{
    public class LogLineMapper_When_mapping
    {
        private readonly LogLineMapper _mapper;

        public LogLineMapper_When_mapping()
        {
            _mapper = new LogLineMapper();
        }

        [Fact]
        public void Then_message_is_mapped_correctly()
        {
            // Arrange
            var message = "It's a trap!!";

            var logEventInfo = LogEventInfoBuilder.New
                .WithMessage("It's not a trap")
                .Build();
            
            // Act
            var logLine = _mapper.Map(logEventInfo, message);
            
            // Assert
            logLine.Line.Should().Be(message);
        }
        
        [Fact]
        public void Then_logger_name_is_mapped_to_env()
        {
            // Arrange
            var loggerName = "It's a trap!!'";

            var logEventInfo = LogEventInfoBuilder.New
                .WithLoggerName(loggerName)
                .Build();
            
            // Act
            var logLine = _mapper.Map(logEventInfo, "It's a trap!!");
            
            // Assert
            logLine.Env.Should().Be(loggerName);
        }
        
        [Fact]
        public void Then_static_app_name_is_mapped()
        {
            // Act
            var logLine = _mapper.Map(LogEventInfoBuilder.New.Build(), "It's a trap!!");
            
            // Assert
            logLine.App.Should().Be(LogDnaConfig.AppName);
        }
        
        [Theory]
        [InlineData("Trace", LoggingLevel.TRACE)]
        [InlineData("Debug", LoggingLevel.DEBUG)]
        [InlineData("Info", LoggingLevel.INFO)]
        [InlineData("Warn", LoggingLevel.WARNING)]
        [InlineData("Error", LoggingLevel.ERROR)]
        [InlineData("Fatal", LoggingLevel.FATAL)]
        public void Then_log_level_is_mapped_correctly(string logLevelString, LoggingLevel expectedLoggingLevel)
        {
            // Arrange
            var logEventInfo = LogEventInfoBuilder.New
                .WithLogLevel(LogLevel.FromString(logLevelString))
                .Build();
            
            // Act
            var logLine = _mapper.Map(logEventInfo, "It's a trap!!");
            
            // Assert
            logLine.Level.Should().Be(expectedLoggingLevel);
        }
    }
}