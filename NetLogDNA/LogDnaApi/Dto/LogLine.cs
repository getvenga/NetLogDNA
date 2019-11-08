using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NetLogDNA.LogDnaApi.Dto
{
    public class LogLine
    {
        [JsonProperty("line")]
        public string Line { get; set; }

        [JsonProperty("level")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LoggingLevel Level { get; set; }

        [JsonProperty("app")]
        public string App { get; set; }

        [JsonProperty("env")]
        public string Env { get; set; }
        
        [JsonProperty("timestamp")]
        public long UnixEpochTimestamp { get; set; }
        
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}