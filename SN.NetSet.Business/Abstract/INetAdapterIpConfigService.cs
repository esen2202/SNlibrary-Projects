using SN.Network.Model;

namespace SN.NetSet.Business.Abstract
{
    public interface INetAdapterIpConfigService
    {
        void SetIpConfig(string interfaceName, NetIpConfigModel netIpConfigModel);
    }
}