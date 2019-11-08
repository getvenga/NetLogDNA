using Newtonsoft.Json;

namespace NetLogDNA.LogDnaApi.Dto
{
    public class ExceptionInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("stackTrace")]
        public string StackTrace { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("innerName")]
        public string InnerName { get; set; }
        
        [JsonProperty("innerStackTrace")]
        public string InnerStackTrace { get; set; }

        [JsonProperty("innerMessage")]
        public string InnerMessage { get; set; }
    }
}