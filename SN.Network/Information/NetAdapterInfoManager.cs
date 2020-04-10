using SN.Network.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SN.Network.Information
{
    public class NetAdapterInfoManager : INetAdapterInfoService
    {
        private readonly static object objLock = new object();

        private static NetAdapterInfoManager _adapterInfoManager;
        private NetworkInterface[] adapters;
        public List<NetAdapterInfo> listAdapter;

        private NetAdapterInfoManager()
        {
            listAdapter = new List<NetAdapterInfo>();
            RefreshInfos();
        }

        public static NetAdapterInfoManager CreateInstance()
        {
            if (_adapterInfoManager == null)
                lock (objLock)
                {
                    if (_adapterInfoManager == null)
                        _adapterInfoManager = new NetAdapterInfoManager();
                }

            return _adapterInfoManager;
        }

        public void RefreshAdapterSpeed(ref NetAdapterInfo adapterInfo)
        {
            adapters = NetworkInterface.GetAllNetworkInterfaces();
            var adapterName = adapterInfo.Name;

            var adapter = adapters.Where(x => x.Name == adapterName).SingleOrDefault();

            if (adapter != null)
            {
                adapterInfo.Speed = adapter.Speed;
            }
        }

        public void RefreshInfos()
        {
            listAdapter.Clear();

            adapters = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in adapters)
            {
                var ipProp = adapter.GetIPProperties();

                foreach (UnicastIPAddressInformation ip in ipProp.UnicastAddresses)
                {
                    if ((adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
                        ip.Address.AddressFamily == AddressFamily.InterNetwork) //
                    {
                        IPInterfaceProperties adapterProp = adapter.GetIPProperties();
                        IPv4InterfaceProperties adapterPropV4 = adapterProp.GetIPv4Properties();
                        GatewayIPAddressInformationCollection gate = adapter.GetIPProperties().GatewayAddresses;

                        listAdapter.Add(new NetAdapterInfo
                        {
                            Name = adapter.Name,
                            Description = adapter.Description,
                            NetworkInterfaceType = adapter.NetworkInterfaceType.ToString(),
                            PhysicalAddress = adapter.GetPhysicalAddress().ToString(),
                            IsReceiveOnly = adapter.IsReceiveOnly,
                            SupportMulticast = adapter.SupportsMulticast,
                            IsOperationalStatusUp = adapter.OperationalStatus == OperationalStatus.Up,
                            Speed = adapter.Speed,


                            DnsSuffix = adapterProp.DnsSuffix,
                            IsDnsEnabled = adapterProp.IsDnsEnabled,
                            IsDynamicDnsEnabled = adapterProp.IsDynamicDnsEnabled,

                            Index = adapterPropV4.Index,
                            Mtu = adapterPropV4.Mtu,
                            IsAutomaticPrivateAddressingActive = adapterPropV4.IsAutomaticPrivateAddressingActive,
                            IsAutomaticPrivateAddressingEnabled = adapterPropV4.IsAutomaticPrivateAddressingEnabled,
                            IsForwardingEnabled = adapterPropV4.IsForwardingEnabled,
                            UsesWins = adapterPropV4.UsesWins,
                            Internet = adapter.GetIPv4Statistics().BytesReceived > 0 && adapter.GetIPv4Statistics().BytesSent > 0,

                            IsDhcpEnabled = adapterPropV4.IsDhcpEnabled,
                            IpAddress = ip.Address.ToString(),
                            SubnetMask = ip.IPv4Mask.ToString(),
                            Gateway = gate.Any() ? gate.FirstOrDefault().Address.ToString() : "",

                            DnsServer1 = (adapterProp.DnsAddresses.Count > 0 && adapterProp.DnsAddresses[0].AddressFamily == AddressFamily.InterNetwork) ? adapterProp.DnsAddresses[0]?.ToString() : "",
                            DnsServer2 = (adapterProp.DnsAddresses.Count > 1 && adapterProp.DnsAddresses[1].AddressFamily == AddressFamily.InterNetwork) ? adapterProp.DnsAddresses[1]?.ToString() : "",
                            DhcpServer = adapterProp.DhcpServerAddresses.FirstOrDefault() != null ? adapterProp.DhcpServerAddresses.FirstOrDefault().ToString() : "",
                        });
                    }
                }

            }
        }

    }
}
