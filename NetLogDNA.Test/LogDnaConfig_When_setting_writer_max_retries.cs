using System;
using FluentAssertions;
using NetLogDNA.Exceptions;
using Xunit;

namespace NetLogDNA.Test
{
    public class LogDnaConfig_When_setting_writer_max_retries
    {
        [Fact]
        public void Then_new_value_is_set_correctly()
        {
            // Arrange
            const int maxRetries = 20;
            
            // Act
            LogDnaConfig.WriterMaxRetries = maxRetries;
            
            // Assert
            LogDnaConfig.WriterMaxRetries.Should().Be(maxRetries);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(51)]
        [InlineData(-1)]
        public void If_new_value_is_invalid_Then_throw(int maxRetries)
        {
            // Act
            Action act = () => LogDnaConfig.WriterMaxRetries = maxRetries;
            
            // Assert
            act.Should().ThrowExactly<InvalidLogDnaConfigException>();
        }
    }
}