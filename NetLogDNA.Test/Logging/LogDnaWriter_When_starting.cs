using System.Linq;
using System.Threading.Tasks;
using NetLogDNA.LogDnaApi.Dto;
using NetLogDNA.LogDnaApi.Requests;
using NetLogDNA.Test.Builders;
using NSubstitute;
using Xunit;

namespace NetLogDNA.Test.Logging
{
    public class LogDnaWriter_When_starting : AbstractLogDnaWriterTest
    {
        [Fact]
        public async Task Then_added_lines_will_be_passed_to_log_dna_api()
        {
            // Arrange
            var line = LogLineBuilder.New.Build();

            Writer.AddLine(line);
            
            // Act
            Writer.Start();

            await Task.Delay(100);

            // Assert
            await LogDnaApi.Received(1)
                .Ingest(
                    Arg.Any<IngestRequest>(),
                    LogDnaConfig.AuthorizationHeader,
                    Arg.Is<LogLineBatch>(b => b.Lines.Count() == 1 && b.Lines.Contains(line))
                );
        }
        
        [Fact]
        public async Task Then_dns_info_provider_is_used_to_determine_hostname()
        {
            // Arrange
            var hostname = "Tattooine";
            Writer.AddLine(LogLineBuilder.New.Build());

            DnsInfoProvider.GetHostName().Returns(hostname);
            
            // Act
            Writer.Start();

            await Task.Delay(100);

            // Assert
            await LogDnaApi.Received(1)
                .Ingest(
                    Arg.Is<IngestRequest>(r => r.HostName == hostname),
                    LogDnaConfig.AuthorizationHeader,
                    Arg.Any<LogLineBatch>()
                );
        }

        [Fact]
        public async Task If_no_lines_are_added_Then_log_dna_api_is_not_called()
        {
            // Act
            Writer.Start();

            await Task.Delay(100);
            
            // Assert
            await LogDnaApi.DidNotReceive()
                .Ingest(Arg.Any<IngestRequest>(), Arg.Any<string>(), Arg.Any<LogLineBatch>());
        }
    }
}