using System;
using System.Threading;
using System.Threading.Tasks;
using NetLogDNA.NLog.Target;
using NLog;
using NLog.Fluent;

namespace NetLogDNA.NLog.Example
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            
            logger.Info()
                .Message("With NLOG!!!")
                .Write();

            await Task.Delay(1000);
        }
    }
}
