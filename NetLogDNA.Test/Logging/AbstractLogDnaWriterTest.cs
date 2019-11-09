using NetLogDNA.LogDnaApi;
using NetLogDNA.LogDnaApi.Dto;
using NetLogDNA.Logging;
using NetLogDNA.Utils;
using NSubstitute;

namespace NetLogDNA.Test.Logging
{
    public abstract class AbstractLogDnaWriterTest
    {
        protected LogDnaWriter Writer { get; }

        protected ILogDnaApi LogDnaApi { get; }

        protected IDnsInfoProvider DnsInfoProvider { get; }

        public AbstractLogDnaWriterTest()
        {
            DnsInfoProvider = Substitute.For<IDnsInfoProvider>();
            LogDnaApi = Substitute.For<ILogDnaApi>();

            var logDnaApiFactory = Substitute.For<ILogDnaApiFactory>();
            logDnaApiFactory.Create().Returns(LogDnaApi);

            Writer = new LogDnaWriter(logDnaApiFactory, DnsInfoProvider);
        }
    }
}