using System.Threading.Tasks;
using NetLogDNA.NLog.Extensions;
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
                .SetLogDnaTags("example", "nlog")
                .Write();

            await Task.Delay(5000);
        }
    }
}
