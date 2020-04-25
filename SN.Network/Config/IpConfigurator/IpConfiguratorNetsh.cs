using SN.Cmd;
using SN.Network.Config.CommandGenerator;
using SN.Network.Model;
using System;

namespace SN.Network.Config.IpConfigurator
{
    public class IpConfiguratorNetsh : IIpConfigurator
    {
        ICommandLine _commandLine;
        ICommandGeneratorIpConfigurator _commandGeneratorIpConfigurator;

        public event EventHandler SetIpOperationCompleted;

        public IpConfiguratorNetsh(ICommandLine commandLine, ICommandGeneratorIpConfigurator commandGeneratorIpConfigurator)
        {
            _commandLine = commandLine;
            _commandGeneratorIpConfigurator = commandGeneratorIpConfigurator;

            _commandLine.ProcessCompleted += delegate { };
        }

        public void SetIpConfig(string interfaceName, NetIpConfigModel netIpConfigModel)
        {
            var command = _commandGeneratorIpConfigurator.ParameterInject(interfaceName, netIpConfigModel).Generate();
            _commandLine.Execute(command);
        }
    }
}
