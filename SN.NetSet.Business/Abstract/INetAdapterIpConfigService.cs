using SN.Cmd;
using SN.Network.Model;
using System;

namespace SN.NetSet.Business.Abstract
{
    public interface INetAdapterIpConfigService
    {
        event EventHandler<EventArgsWithStrMessage> SetIpOperationCompleted;

        void SetIpConfig(string interfaceName, NetIpConfigModel netIpConfigModel);
    }
}