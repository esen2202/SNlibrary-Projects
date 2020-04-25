using Microsoft.VisualStudio.TestTools.UnitTesting;
using SN.Cmd;
using SN.NetSet.Business.Abstract;
using SN.NetSet.Business.DependencyResolvers.Ninject;
using SN.NetSet.Business.Network;
using SN.Network.Config.CommandGenerator;
using SN.Network.Config.IpConfigurator;
using SN.Network.Info.NetAdapter;
using SN.Network.Model;
using System;

namespace SN.NetSet.UnitTest.SNNetwork
{
    [TestClass]
    public class SNNetworkTest
    {


        [TestMethod]
        public void ModelBaseObjectClone()
        {
            NetIpConfigModel net = new NetIpConfigModel();
            net.IpAddress = "23494352";
            net.IsDhcpEnabled = true;

            var deneme = (NetIpConfigModel)net.Clone();
            net.DnsServer1 = "yeni";
            deneme.DnsServer1 = "clone";


            NetAdapterModel net2 = new NetAdapterModel();
            net2.Description = "ilk nesne";
            net2.IpConfig.DhcpServer = "ortak ornek";
            var clone = (NetAdapterModel)net2.Clone();
            clone.Description = "clonlandı";
        }

        [TestMethod]
        public void GetAdapterList()
        {
            NetAdapterInfoSystem nac = new NetAdapterInfoSystem();
            var adpterlist = nac.GetAdapterList();

            var captionlist = nac.GetAdapterCaptionList();

            var netadap = nac.GetAdapter(captionlist[1].Description);

        }

        NetAdapterInfoSystem nac;

        [TestMethod]
        public void NetshControl()
        {
            //nac = new NetAdapterInfoSystem();
            //var captionlist = nac.GetAdapterCaptionList();

            //ICommandGeneratorIpConfigurator commandGenerator =
            //    new CommandGeneratorNetshIpConfigurator();
            //commandGenerator.ParameterInject("VMware Virtual Ethernet Adapter for VMnet10", new NetIpConfigModel
            //{
            //    IpAddress = "192.5.5.5",
            //    SubnetMask = "255.255.0.0",
            //    DnsServer1 = "192.5.5.1",
            //    IsDhcpEnabled = false
            //});

            //IpConfiguratorNetsh ipConfiguratorNetsh =
            //   new IpConfiguratorNetsh(new CommandLineCmd(), new CommandGeneratorNetshIpConfigurator());
            //ipConfiguratorNetsh.SetIpConfig("VMware Virtual Ethernet Adapter for VMnet10", new NetIpConfigModel
            //{
            //    IpAddress = "192.5.5.5",
            //    SubnetMask = "255.255.0.0",
            //    DnsServer1 = "192.5.5.1",
            //    IsDhcpEnabled = false          
            //});
            //ipConfiguratorNetsh.SetIpOperationCompleted += İpConfiguratorNetsh_SetIpOperationCompleted;

            INetAdapterInfoService netAdapterInfoManager = 
                InstanceFactory.GetInstance<INetAdapterInfoService>();
            var list = netAdapterInfoManager.GetAdapterCaptionList();
            var adapter = netAdapterInfoManager.GetAdapter("Wireless");

            INetAdapterIpConfigService netAdapterIpConfigService =
                InstanceFactory.GetInstance<INetAdapterIpConfigService>();

            netAdapterIpConfigService.SetIpConfig(adapter.Description, new NetIpConfigModel()
            {
                IpAddress = "10.0.0.5",
                SubnetMask = "255.0.0.0",
                DnsServer1 = "10.0.0.1",
                IsDhcpEnabled = false
            });

            adapter = netAdapterInfoManager.GetAdapter("Wireless");

        }


        private void İpConfiguratorNetsh_SetIpOperationCompleted(object sender, EventArgs e)
        {
            //var result = netsh.ResultData;
            var netadap = nac.GetAdapter("VMware Virtual Ethernet Adapter for VMnet10");
        }
    }
}
