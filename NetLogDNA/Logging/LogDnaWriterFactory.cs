using NetLogDNA.LogDnaApi;
using NetLogDNA.Utils;

namespace NetLogDNA.Logging
{
    public class LogDnaWriterFactory
    {
        private LogDnaWriter _logDnaWriter;
        
        public ILogDnaWriter Create()
        {
            if (_logDnaWriter == null)
                CreateLogDnaWriterSingleton();

            return _logDnaWriter;
        }

        private void CreateLogDnaWriterSingleton()
        {
            _logDnaWriter = new LogDnaWriter(new LogDnaApiFactory(), new DnsInfoProvider());
        }
    }
}