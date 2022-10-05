using FluentAssertions;
using NetLogDNA.LogDnaApi;
using Xunit;

namespace NetLogDNA.Test.LogDnaApi
{
    public class LogDnaApiFactory_When_creating
    {
        private readonly LogDnaApiClientFactory _factory;

        public LogDnaApiFactory_When_creating()
        {
            _factory = new LogDnaApiClientFactory();
        }

        [Fact]
        public void Then_return_log_dna_api()
        {
            // Act
            var logDnaApi = _factory.Create();
            
            // Assert
            logDnaApi.Should().NotBeNull();
        }
    }
}