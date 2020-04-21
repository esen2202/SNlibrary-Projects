using Microsoft.VisualStudio.TestTools.UnitTesting;
using SN.Cmd;
using SN.Network.Cmd;
using SN.Network.Information;
using SN.Network.Model;
using System;

namespace SN.NetSet.UnitTest.SNNetwork
{
    [TestClass]
    public class SNNetwork
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
            NetAdapterInfoManager nac = new NetAdapterInfoManager();
            var adpterlist = nac.GetAdapterList();

            var captionlist = nac.GetAdapterCaptionList();

            var netadap = nac.GetAdapter(captionlist[1].Description);

        }

        NetAdapterInfoManager nac;
        Netsh netsh;
        [TestMethod]
        public void NetshControl()
        {
            nac = new NetAdapterInfoManager();
            var captionlist = nac.GetAdapterCaptionList();

            ICommandGenerator commandGenerator = new CommandNetshIpConfig()
            {
                InterfaceDescription = "VMware Virtual Ethernet Adapter for VMnet10",
                NetIpConfig = new NetIpConfigModel
                {
                    IpAddress = "192.5.5.5",
                    SubnetMask = "255.255.0.0",
                    DnsServer1 ="192.5.5.1",
                    IsDhcpEnabled = false
                }
            };

            netsh = new Netsh(new CommandLine(), commandGenerator);
            netsh.ProcessCompleted += Netsh_ProcessCompleted;
            netsh.Execute();

        }

        private void Netsh_ProcessCompleted(object sender, EventArgs e)
        {
            var result = netsh.ResultData;
            var netadap = nac.GetAdapter("VMware Virtual Ethernet Adapter for VMnet10");
        }
    }
}
