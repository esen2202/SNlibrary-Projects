using SN.Cmd;
using SN.NetSet.Business.Abstract;
using SN.Network.Config.IpConfigurator;
using SN.Network.Model;
using System;

namespace SN.NetSet.Business.Network
{
    public class NetAdapterIpConfigManager : INetAdapterIpConfigService
    {
        public event EventHandler<EventArgsWithStrMessage> SetIpOperationCompleted;

        IIpConfigurator _ipConfigurator;

        public NetAdapterIpConfigManager(IIpConfigurator ipConfigurator)
        {
            _ipConfigurator = ipConfigurator;
            _ipConfigurator.SetIpOperationCompleted += delegate { };
            _ipConfigurator.SetIpOperationCompleted += _ipConfigurator_SetIpOperationCompleted; ;
        }

        private void _ipConfigurator_SetIpOperationCompleted(object sender, EventArgsWithStrMessage e)
        {

            e.Message = e.Message.Replace(System.Environment.NewLine, "") == "" 
                ? "Success" 
                : e.Message.Replace(System.Environment.NewLine, "");
            SetIpOperationCompleted?.Invoke(sender, e);
        }

        public void SetIpConfig(string interfaceName, NetIpConfigModel netIpConfigModel)
        {
            _ipConfigurator.SetIpConfig(interfaceName, netIpConfigModel);
        }
    }
}
