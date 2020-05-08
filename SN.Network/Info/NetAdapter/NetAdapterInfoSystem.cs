using SN.Network.Helpers;
using SN.Network.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SN.Network.Info.NetAdapter
{
    internal delegate IModel AssignValuesToModel(NetworkInterface ni);
    public class NetAdapterInfoSystem : INetAdapterInfo
    {
        public NetAdapterInfoSystem()
        {
        }

        private NetAdapterModel AssignAdapterValues(NetworkInterface adapter)
        {
            if (adapter == null) return new NetAdapterModel();

            IPInterfaceProperties adapterProp =  adapter.GetIPProperties();
            var ipInfo = adapter.GetIPProperties().UnicastAddresses.Where(o => o.Address.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();

            return new NetAdapterModel
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

                Index = adapterProp.GetIPv4Properties().Index,
                Mtu = adapterProp.GetIPv4Properties().Mtu,
                IsAutomaticPrivateAddressingActive = adapterProp.GetIPv4Properties().IsAutomaticPrivateAddressingActive,
                IsAutomaticPrivateAddressingEnabled = adapterProp.GetIPv4Properties().IsAutomaticPrivateAddressingEnabled,
                IsForwardingEnabled = adapterProp.GetIPv4Properties().IsForwardingEnabled,
                UsesWins = adapterProp.GetIPv4Properties().UsesWins,
                Internet = adapter.GetIPv4Statistics().BytesReceived > 0 && adapter.GetIPv4Statistics().BytesSent > 0,
                IpConfig = new NetIpConfigModel
                {
                    IsDhcpEnabled = adapterProp.GetIPv4Properties().IsDhcpEnabled,
                    IpAddress = ipInfo.Address.ToString(),
                    SubnetMask = ipInfo.IPv4Mask.ToString(),
                    Gateway = adapterProp.GatewayAddresses.Any() ? adapterProp.GatewayAddresses.FirstOrDefault().Address.ToString() : "",

                    DnsServer1 = (adapterProp.DnsAddresses.Count > 0 && adapterProp.DnsAddresses[0].AddressFamily == AddressFamily.InterNetwork) ?
                    adapterProp.DnsAddresses[0]?.ToString() : "",
                    DnsServer2 = (adapterProp.DnsAddresses.Count > 1 && adapterProp.DnsAddresses[1].AddressFamily == AddressFamily.InterNetwork) ?
                    adapterProp.DnsAddresses[1]?.ToString() : "",
                    DhcpServer = adapterProp.DhcpServerAddresses.FirstOrDefault() != null ? adapterProp.DhcpServerAddresses.FirstOrDefault().ToString() : "",
                }
            };
        }

        private NetAdapterModelBase AssignAdapterCaptionValues(NetworkInterface adapter)
        {
            if (adapter == null) return new NetAdapterModelBase();

            IPInterfaceProperties adapterProp = adapter.GetIPProperties();

            return new NetAdapterModelBase
            {
                Name = adapter.Name,
                Description = adapter.Description,
                NetworkInterfaceType = adapter.NetworkInterfaceType.ToString(),
                IsOperationalStatusUp = adapter.OperationalStatus == OperationalStatus.Up,
            };
        }

        private List<TModel> GetAdapterList<TModel>(AssignValuesToModel WhichAssignMethod)
            where TModel : NetAdapterModelBase 
        {
            List<TModel> netAdapterModels = new List<TModel>();
            string[] NwDesc = { "TAP", "VMware", "Windows", "Virtual" };

            var adapterList = NetworkInterface.GetAllNetworkInterfaces().
                Where(o => (o.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || o.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
                !NwDesc.Any(o.Description.Contains) && o.GetIPProperties().UnicastAddresses.Where(p => p.Address.AddressFamily == AddressFamily.InterNetwork).Any()).ToList();

            adapterList.ForEach(adapter =>
            {
                netAdapterModels.Add((TModel)WhichAssignMethod(adapter));
            });

            return netAdapterModels;
        }

        public List<NetAdapterModel> GetAdapterList()
        {
            return GetAdapterList<NetAdapterModel>(AssignAdapterValues);
        }

        public List<NetAdapterModelBase> GetAdapterCaptionList()
        {
            return GetAdapterList<NetAdapterModelBase>(AssignAdapterCaptionValues);
        }

        public NetAdapterModel GetAdapter(string interfaceName)
        {
            var adapter = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(o => o.Name == interfaceName);

            return AssignAdapterValues(adapter);
        }

    }
}
