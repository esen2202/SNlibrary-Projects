using SN.Network.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SN.Network.Info.NetAdapter
{
    public class NetAdapterInfoFake : INetAdapterInfo
    {
        public static List<NetAdapterModel> netAdapterModels;
        public NetAdapterInfoFake()
        {
            netAdapterModels = new List<NetAdapterModel>()
            {
                new NetAdapterModel()
                {
                    Description = "Ethernet",
                    Name = "Ethernet Adapter 1",
                    Internet = true,
                    IpConfig = new NetIpConfigModel{
                        IsDhcpEnabled =  true,
                        IpAddress = "192.167.2.45",
                        SubnetMask="255.255.255.0",
                        Gateway = "192.167.2.1"},
                    Speed= 1,
                    IsOperationalStatusUp = true,
                    PhysicalAddress = "18-5E-0F-1A-61-57".Replace("-", "")
                },
                new NetAdapterModel()
                {
                    Description = "Wireless",
                    Name = "Wireless Adapter 2",
                    Internet = false,
                    IpConfig = new NetIpConfigModel{
                        IsDhcpEnabled =  false,
                        IpAddress = "192.167.1.27",
                        SubnetMask="255.255.255.0",
                        Gateway = "192.167.1.1"},
                    Speed= 1,
                    IsOperationalStatusUp = true,
                    PhysicalAddress = "27-FE-0F-1A-61-AF".Replace("-", "")
                },
                new NetAdapterModel()
                {
                    Description = "Vmware",
                    Name = "Vmware Adapter 3",
                    Internet = false,
                    IpConfig = new NetIpConfigModel{
                        IsDhcpEnabled =  false,
                        IpAddress = "10.0.0.12",
                        SubnetMask="255.0.0.0",
                        Gateway = "10.0.0.1"},
                    Speed= 1,
                    IsOperationalStatusUp = true,
                    PhysicalAddress = "10-FE-0F-FF-61-12".Replace("-", "")
                }
            };
        }

        public NetAdapterModel GetAdapter(string adapterDesc)
        {
            return netAdapterModels.SingleOrDefault(o => o.Description == adapterDesc) ?? new NetAdapterModel();
        }

        public List<NetAdapterModelBase> GetAdapterCaptionList()
        {
            return netAdapterModels.ToList<NetAdapterModelBase>();
        }

        public List<NetAdapterModel> GetAdapterList()
        {
            return netAdapterModels.ToList();
        }


    }
}
