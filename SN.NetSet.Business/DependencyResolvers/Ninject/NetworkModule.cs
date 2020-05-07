using Ninject.Modules;
using SN.Cmd;
using SN.NetSet.Business.Abstract;
using SN.NetSet.Business.Network;
using SN.Network.Config.CommandGenerator;
using SN.Network.Config.IpConfigurator;
using SN.Network.Info.NetAdapter;

namespace SN.NetSet.Business.DependencyResolvers.Ninject
{
    public class NetworkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<INetAdapterInfoService>().To<NetAdapterInfoManager>();
            Bind<INetAdapterIpConfigService>().To<NetAdapterIpConfigManager>();

            Bind<INetAdapterInfo>().To<NetAdapterInfoSystem>().InSingletonScope();
            Bind<IIpConfigurator>().To<IpConfiguratorNetsh>();
 
            Bind<ICommandLine>().To<CommandLineCmd>();
            Bind<ICommandGeneratorIpConfigurator>().To<CommandGeneratorNetshIpConfigurator>();
        
        }
    }
}
