using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NetLogDNA.LogDnaApi.Dto
{
    public class LogLineBatch
    {
        [JsonPropertyName("lines")]
        public IEnumerable<LogLine> Lines { get; private set; }

        public LogLineBatch(IEnumerable<LogLine> logLines)
        {
            Lines = logLines;
        }
    }
}