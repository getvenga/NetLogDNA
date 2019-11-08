using System;
using System.Threading.Tasks;
using NetLogDNA.LogDnaApi;
using NetLogDNA.LogDnaApi.Dto;
using NetLogDNA.Logging;
using NetLogDNA.Utils;
using Xunit;

namespace NetLogDNA.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            // Arrange
            LogDnaConfig.ApiKey = "";
            LogDnaConfig.AppName = "NetLogDNA";

            var writer = new LogDnaWriter(new LogDnaApiFactory(), new DnsInfoProvider())
            {
                Verbose = true
            };
            
            writer.Start();
            
            // Act
            for (int i = 0; i < 5; i++)
            {
                var line = BuildLogLine($"Writing line number {i} - {DateTime.UtcNow.Ticks}");
                writer.AddLine(line);
            }
            
            await writer.Stop();
        }

        private LogLine BuildLogLine(string content)
        {
            return new LogLine()
            {
                Level = LoggingLevel.TRACE,
                Env = "Test",
                Line = content,
                UnixEpochTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };
        }
    }
}
