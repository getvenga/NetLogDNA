using FluentAssertions;
using NetLogDNA.Logging;
using Xunit;

namespace NetLogDNA.Test.Logging
{
    public class LogDnaWriterFactory_When_creating
    {
        private readonly LogDnaWriterFactory _factory;

        public LogDnaWriterFactory_When_creating()
        {
            _factory = new LogDnaWriterFactory();
        }

        [Fact]
        public void Then_log_dna_writer_is_returned()
        {
            // Act
            var writer = _factory.Create();
            
            // Assert
            writer.Should().NotBeNull();
            writer.Should().BeOfType<LogDnaWriter>();
        }
        
        [Fact]
        public void If_create_is_called_multiple_times_Then_return_same_singleton_writer()
        {
            // Arrange
            var writer1 = _factory.Create();
            
            // Act
            var writer2 = _factory.Create();
            
            // Assert
            writer2.Should().BeSameAs(writer1);
        }
    }
}