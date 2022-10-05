using System.Text.Json.Serialization;
namespace NetLogDNA.LogDnaApi.Dto
{
    public class LogLine
    {
        [JsonPropertyName("line")]
        public string Line { get; set; }

        [JsonPropertyName("level")]
        public LoggingLevel Level { get; set; }

        [JsonPropertyName("app")] 
        public string App => LogDnaConfig.AppName;

        [JsonPropertyName("env")]
        public string Env { get; set; }
        
        [JsonPropertyName("timestamp")]
        public long UnixEpochTimestamp { get; set; }
    }
}