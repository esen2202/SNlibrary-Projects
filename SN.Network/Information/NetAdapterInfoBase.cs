using SN.Network.Abstract;

namespace SN.Network.Information
{
    public class NetAdapterInfoBase :  INetAdapterInfo
    {
        public string IpAddress { get; set; }
        public string SubnetMask { get; set; }
        public string Gateway { get; set; }
        public string DnsServer1 { get; set; }
        public string DnsServer2 { get; set; }
        public bool IsDhcpEnabled { get; set; }

    }
}
