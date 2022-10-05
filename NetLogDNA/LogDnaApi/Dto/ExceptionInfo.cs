using System.Text.Json.Serialization;

namespace NetLogDNA.LogDnaApi.Dto
{
    public class ExceptionInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("stackTrace")]
        public string StackTrace { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("innerName")]
        public string InnerName { get; set; }
        
        [JsonPropertyName("innerStackTrace")]
        public string InnerStackTrace { get; set; }

        [JsonPropertyName("innerMessage")]
        public string InnerMessage { get; set; }
    }
}