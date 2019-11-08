namespace NetLogDNA.Logging
{
    public interface ILogDnsWriterFactory
    {
        ILogDnaWriter Create();
    }
}