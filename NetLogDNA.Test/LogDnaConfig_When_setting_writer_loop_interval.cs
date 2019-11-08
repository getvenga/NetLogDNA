using System;
using FluentAssertions;
using NetLogDNA.Exceptions;
using Xunit;

namespace NetLogDNA.Test
{
    public class LogDnaConfig_When_setting_writer_loop_interval
    {
        [Fact]
        public void Then_new_value_is_set_correctly()
        {
            // Arrange
            const int interval = 20;
            
            // Act
            LogDnaConfig.WriterLoopInterval = interval;
            
            // Assert
            LogDnaConfig.WriterLoopInterval.Should().Be(interval);
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void If_new_value_is_invalid_Then_throw(int interval)
        {
            // Act
            Action act = () => LogDnaConfig.WriterLoopInterval = interval;
            
            // Assert
            act.Should().ThrowExactly<InvalidLogDnaConfigException>();
        }
    }
}