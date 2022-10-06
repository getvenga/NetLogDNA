using NetLogDNA.LogDnaApi.Dto;
using NetLogDNA.LogDnaApi.Requests;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetLogDNA.LogDnaApi
{
    public class LogDnaApiClient : ILogDnaApi
    {
        private readonly HttpClient _httpClient;

        public LogDnaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> Ingest(IngestRequest ingestRequestParams, string authorization, LogLineBatch logLineBatch)
        {
            _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(authorization);
            
            var httpContent = JsonContent.Create(logLineBatch, options: new JsonSerializerOptions() { Converters = { new JsonStringEnumConverter() }});
            
            var postresult = await _httpClient.PostAsync(
                $"https://logs.logdna.com/logs/ingest?hostname={ingestRequestParams.HostName}&now={ingestRequestParams.UnitEpochTimestamp}", httpContent
            );

            return postresult;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
