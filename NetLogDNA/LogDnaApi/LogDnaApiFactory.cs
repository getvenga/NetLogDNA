using Refit;

namespace NetLogDNA.LogDnaApi
{
    public class LogDnaApiFactory : ILogDnaApiFactory
    {
        private const string LogDnaBaseUrl = "https://logs.logdna.com";
        
        public ILogDnaApi Create()
        {
            return RestService.For<ILogDnaApi>(LogDnaBaseUrl);
        }
    }
}