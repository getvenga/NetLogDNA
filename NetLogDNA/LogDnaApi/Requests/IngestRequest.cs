using System;
using System.Linq;
using System.Net.NetworkInformation;
using NetLogDNA.Utils;

namespace NetLogDNA.LogDnaApi.Requests
{
    public class IngestRequest
    {
        public string HostName { get; set; }

        public string IpAddress { get; set; }

//        [AliasAs("mac")]
//        public string MacAddress => GetMacAddress();

        public long UnitEpochTimestamp => DateTime.UtcNow.ToUnixTimestamp();
        
        private static string GetMacAddress()
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .FirstOrDefault(net => net.OperationalStatus == OperationalStatus.Up
                                       && net.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                ?.GetPhysicalAddress()
                .ToString();
        }
    }
}