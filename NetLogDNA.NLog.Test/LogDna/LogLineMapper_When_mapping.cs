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
        
        [Fact]
        public void Then_exception_is_mapped_to_exception_info()
        {
            // Arrange
            var exceptionMessage = "I am your father!";
            var exception = new Exception(exceptionMessage);

            var logEventInfo = LogEventInfoBuilder.New
                .WithException(exception)
                .Build();
            
            // Act
            var logLine = _mapper.Map(logEventInfo, "It's a trap!!");
            
            // Assert
            logLine.Meta.Exception.Message.Should().Be(exceptionMessage);
            logLine.Meta.Exception.Name.Should().Be(exception.GetType().Name);
            logLine.Meta.Exception.StackTrace.Should().Be(exception.StackTrace);
        }
        
        [Fact]
        public void Then_inner_exception_is_mapped_to_exception_info()
        {
            // Arrange
            var innerExceptionMessage = "I am your father!";
            var innerException = new Exception(innerExceptionMessage);
            var exception = new ArgumentException("Noooooo!", innerException);

            var logEventInfo = LogEventInfoBuilder.New
                .WithException(exception)
                .Build();
            
            // Act
            var logLine = _mapper.Map(logEventInfo, "It's a trap!!");
            
            // Assert
            logLine.Meta.Exception.InnerMessage.Should().Be(innerExceptionMessage);
            logLine.Meta.Exception.InnerName.Should().Be(innerException.GetType().Name);
            logLine.Meta.Exception.InnerStackTrace.Should().Be(innerException.StackTrace);
        }
        
        [Fact]
        public void Then_all_properties_are_mapped()
        {
            // Arrange
            var (prop1Key, prop1Val) = ("p", 1);
            var (prop2Key, prop2Val) = (1.4, false);

            var logEventInfo = LogEventInfoBuilder.New
                .AddProperty(prop1Key, prop1Val)
                .AddProperty(prop2Key, prop2Val)
                .Build();
            
            // Act
            var logLine = _mapper.Map(logEventInfo, "It's a trap!!");
            
            // Assert
            logLine.Meta.Properties.Should().HaveCount(2);
            logLine.Meta.Properties.Should().ContainKey(prop1Key);
            logLine.Meta.Properties.Should().ContainKey(prop2Key);
            logLine.Meta.Properties[prop1Key].Should().Be(prop1Val);
            logLine.Meta.Properties[prop2Key].Should().Be(prop2Val);
        }
        
        [Fact]
        public void Then_tags_are_mapped()
        {
            // Arrange
            var tag1 = "luke";
            var tag2 = "leia";

            var logEventInfo = LogEventInfoBuilder.New
                .AddProperty(LogEventInfoExtensions.LogDnaTagPropertyName, new[] { tag1, tag2 })
                .Build();
            
            // Act
            var logLine = _mapper.Map(logEventInfo, "It's a trap!!");
            
            // Assert
            logLine.Meta.Tags.Should().HaveCount(2);
            logLine.Meta.Tags.Should().Contain(tag1);
            logLine.Meta.Tags.Should().Contain(tag2);
        }
    }
}