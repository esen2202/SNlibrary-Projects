using SN.Cmd;
using SN.Network.Model;

namespace SN.Network.Config.CommandGenerator
{
    public interface ICommandGeneratorIpConfigurator : ICommandGenerator
    {
        ICommandGenerator ParameterInject(string interfaceName, NetIpConfigModel netIpConfigModel = null);
    }
}
