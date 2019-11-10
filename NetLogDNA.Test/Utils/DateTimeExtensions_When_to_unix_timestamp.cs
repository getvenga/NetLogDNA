using System;
using FluentAssertions;
using NetLogDNA.Utils;
using Xunit;

namespace NetLogDNA.Test.Utils
{
    public class DateTimeExtensions_When_to_unix_timestamp
    {
        [Fact(Skip = "Fix it")]
        public void Then_correct_unix_time_stamp_is_returned()
        {
            // Arrange
            var dt = new DateTime(2019, 11, 9, 21, 0, 0);
            
            // Act
            var unixTimestamp = dt.ToUnixTimestamp();
            
            // Assert
            unixTimestamp.Should().Be(1573329600000L);
        }
    }
}