using SN.Cmd;
using SN.Network.Model;
using System;

namespace SN.Network.Config.CommandGenerator
{
    public class CommandGeneratorIpFake : ICommandGeneratorIpConfigurator
    {
        string _interfaceName;
        NetIpConfigModel _netIpConfigModel;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public string Generate()
        {
            return "cmd : " + _interfaceName + " | " + _netIpConfigModel.IpAddress;
        }

        public ICommandGenerator ParameterInject(string interfaceName, NetIpConfigModel netIpConfigModel = null)
        {
            _interfaceName = interfaceName;
            _netIpConfigModel = netIpConfigModel;
            return this;
        }
    }
}
