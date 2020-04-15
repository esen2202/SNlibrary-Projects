namespace SN.Network.Model
{
    public class NetAdapterModel : NetAdapterModelBase
    {
        public NetIpConfigModel IpConfig { get; set; }
        public string PhysicalAddress { get; set; }
        public bool IsReceiveOnly { get; set; }
        public bool SupportMulticast { get; set; }
        public long Speed { get; set; }
        public bool Internet { get; set; }
        public string DnsSuffix { get; set; }
        public bool IsDnsEnabled { get; set; }
        public bool IsDynamicDnsEnabled { get; set; }
        public int Index { get; set; }
        public int Mtu { get; set; }
        public bool IsAutomaticPrivateAddressingActive { get; set; }
        public bool IsAutomaticPrivateAddressingEnabled { get; set; }
        public bool IsForwardingEnabled { get; set; }
        public bool UsesWins { get; set; }

    }
}
