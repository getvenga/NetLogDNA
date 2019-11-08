using System.Net.Http;
using System.Threading.Tasks;
using NetLogDNA.LogDnaApi.Dto;
using NetLogDNA.LogDnaApi.Requests;
using Refit;

namespace NetLogDNA.LogDnaApi
{
    [Headers("Content-Type: application/json; charset=UTF-8")]
    public interface ILogDnaApi
    {
        [Post("/logs/ingest")]
        Task<HttpResponseMessage> Ingest(
            IngestRequest ingestRequestParams,
            [Header("Authorization")] string authorization,
            [Body] LogLineBatch logLineBatch);
    }
}