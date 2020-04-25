using SN.NetSet.Business.Abstract;
using SN.Network.Config.IpConfigurator;
using SN.Network.Model;

namespace SN.NetSet.Business.Network
{
    public class NetAdapterIpConfigManager : INetAdapterIpConfigService
    {
        IIpConfigurator _ipConfigurator;

        public NetAdapterIpConfigManager(IIpConfigurator ipConfigurator)
        {
            _ipConfigurator = ipConfigurator;
            _ipConfigurator.SetIpOperationCompleted += delegate { };
        }

        public void SetIpConfig(string interfaceName, NetIpConfigModel netIpConfigModel)
        {
            _ipConfigurator.SetIpConfig(interfaceName, netIpConfigModel);
        }
    }
}
