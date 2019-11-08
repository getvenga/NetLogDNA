using System.Text;

namespace NetLogDNA.LogDnaApi
{
    public static class LogDnaApiConfig
    {
        public static string ApiKey { get; set; }

        public static string AuthorizationHeader => GetBase64AuthHeader();
        
        private static string GetBase64AuthHeader()
        {
            var authString = Encoding.UTF8.GetBytes($"{ApiKey}:");
            return $"Basic {System.Convert.ToBase64String(authString)}";
        }
    }
}