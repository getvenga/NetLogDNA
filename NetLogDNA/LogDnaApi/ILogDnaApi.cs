using System.Net.Http;
using System.Threading.Tasks;
using NetLogDNA.LogDnaApi.Dto;
using NetLogDNA.LogDnaApi.Requests;

namespace NetLogDNA.LogDnaApi
{
    public interface ILogDnaApi
    {
        Task<HttpResponseMessage> Ingest(
            IngestRequest ingestRequestParams,
            string authorization,
            LogLineBatch logLineBatch);
    }
}