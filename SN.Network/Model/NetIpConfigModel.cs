namespace SN.Network.Model
{
    public class NetIpConfigModel : IModel
    {
        public bool IsDhcpEnabled { get; set; }
        public string IpAddress { get; set; }
        public string Gateway { get; set; }
        public string SubnetMask { get; set; }
        public string DnsServer1 { get; set; }
        public string DnsServer2 { get; set; }
        public string DhcpServer { get; set; }
        public IModel Clone()
        {
            return (IModel)this.MemberwiseClone();
        }
    }
}
