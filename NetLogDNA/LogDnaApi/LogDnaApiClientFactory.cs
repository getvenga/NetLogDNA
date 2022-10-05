using System;
using System.Net.Http;

namespace NetLogDNA.LogDnaApi
{
    public class LogDnaApiClientFactory : ILogDnaApiFactory
    {
        public ILogDnaApi Create()
        {
            var httpClient = new HttpClient() { BaseAddress = new Uri("https://logs.logdna.com") };
            return new LogDnaApiClient(httpClient);
        }
    }
}
