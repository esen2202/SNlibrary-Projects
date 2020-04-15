using Microsoft.VisualStudio.TestTools.UnitTesting;
using SN.Network.Configuration;
using SN.Network.Information;
using SN.Network.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


            NetAdapterModel  net2 = new NetAdapterModel ();
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
    }
}
