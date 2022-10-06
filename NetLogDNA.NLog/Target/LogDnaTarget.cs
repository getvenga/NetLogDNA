using NetLogDNA.Logging;
using NetLogDNA.NLog.LogDna;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets;

namespace NetLogDNA.NLog.Target
{
    [Target("LogDNA")]
    public class LogDnaTarget : TargetWithLayout
    {
        private readonly ILogDnaWriter _logDnaWriter;

        private readonly LogLineMapper _logLineMapper;
        
        [RequiredParameter] 
        public string ApiKey { get; set; }

        public string AppName { get; set; } = "Test";

        public LogDnaTarget()
        {
            _logDnaWriter = new LogDnaWriterFactory().Create();
            _logDnaWriter.Start();
            _logLineMapper = new LogLineMapper();
        }

        protected override void InitializeTarget()
        {
            base.InitializeTarget();
            
            LogDnaConfig.AppName = AppName;
            LogDnaConfig.ApiKey = ApiKey;
        }

        protected override void Write(LogEventInfo logEvent)
        {
            var logMessage = Layout.Render(logEvent);
            var logLine = _logLineMapper.Map(logEvent, logMessage);

            _logDnaWriter.AddLine(logLine);
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }
    }
}