using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NetLogDNA.LogDnaApi.Dto
{
    public class LogLineBatch
    {
        [JsonProperty("lines")]
        public IEnumerable<LogLine> Lines { get; private set; }

        public LogLineBatch(IEnumerable<LogLine> logLines)
        {
            Lines = logLines;
        }
    }
}