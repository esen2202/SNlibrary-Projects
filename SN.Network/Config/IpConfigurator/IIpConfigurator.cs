using SN.Network.Model;
using System;

namespace SN.Network.Config.IpConfigurator
{
    public interface IIpConfigurator
    {
        event EventHandler SetIpOperationCompleted;
        void SetIpConfig(string interfaceName, NetIpConfigModel netIpConfigModel);
    }
}
