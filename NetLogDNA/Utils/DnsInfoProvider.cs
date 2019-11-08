namespace NetLogDNA.Utils
{
    public class DnsInfoProvider : IDnsInfoProvider
    {
        public string GetHostName()
        {
            return System.Environment.MachineName;
        }
    }
}