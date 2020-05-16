using SN.Cmd;
using SN.Network.Config.CommandGenerator;
using SN.Network.Model;
using System;
using System.Linq;

namespace SN.Network.Config.IpConfigurator
{
    public class IpConfiguratorFake : IIpConfigurator
    {
        ICommandLine _commandLine;
        ICommandGeneratorIpConfigurator _commandGeneratorIpConfigurator;

        public event EventHandler<EventArgsWithStrMessage> SetIpOperationCompleted;

        public IpConfiguratorFake(ICommandLine commandLine, ICommandGeneratorIpConfigurator commandGeneratorIpConfigurator)
        {
            _commandLine = commandLine;
            _commandGeneratorIpConfigurator = commandGeneratorIpConfigurator;
            _commandLine.ProcessCompleted += _commandLine_ProcessCompleted;
        }

        private void _commandLine_ProcessCompleted(object sender, EventArgsWithStrMessage e)
        {
            SetIpOperationCompleted?.Invoke(sender, e);
        }

        public void SetIpConfig(string interfaceName, NetIpConfigModel netIpConfigModel)
        {
            var command = _commandGeneratorIpConfigurator.ParameterInject(interfaceName, netIpConfigModel).Generate();
            _commandLine.Execute(command);

            var adapter = Info.NetAdapter.NetAdapterInfoFake.netAdapterModels.Where(o => o.Description == interfaceName).SingleOrDefault();
            if (adapter != null)
            {
                adapter.IpConfig.IpAddress = netIpConfigModel.IpAddress;
                adapter.IpConfig.SubnetMask = netIpConfigModel.SubnetMask;
                adapter.IpConfig.Gateway = netIpConfigModel.Gateway;
                adapter.IpConfig.DnsServer1 = netIpConfigModel.DnsServer1;
                adapter.IpConfig.DnsServer2 = netIpConfigModel.DnsServer2;
                adapter.IpConfig.IsDhcpEnabled = netIpConfigModel.IsDhcpEnabled;
            }
        }
    }
}
