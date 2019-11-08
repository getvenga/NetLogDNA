using System.Text;
using NetLogDNA.Exceptions;

namespace NetLogDNA
{
    public static class LogDnaConfig
    {
        private static int _writerLoopDelay = 100;

        public static int WriterLoopInterval
        {
            set
            {
                if (value <= 0)
                    throw new InvalidLogDnaConfigException($"{value} is invalid as a writer loop interval");
                
                _writerLoopDelay = value;
            }
            get => _writerLoopDelay;
        }
        
        private static int _writerMaxRetries = 5;

        public static int WriterMaxRetries
        {
            set
            {
                if (value < 1 || value > 50)
                    throw new InvalidLogDnaConfigException("The max retries have to be between 1 and 50");
                
                _writerMaxRetries = value;
            }
            get => _writerMaxRetries;
        }
        
        public static string ApiKey { get; set; }

        public static string AuthorizationHeader => GetBase64AuthHeader();
        
        private static string GetBase64AuthHeader()
        {
            var bytes = Encoding.UTF8.GetBytes($"{ApiKey}:");
            return $"Basic {System.Convert.ToBase64String(bytes)}";
        }
    }
}