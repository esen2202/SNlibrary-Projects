using SN.Network.Model;
using System;

namespace SN.Network.Cmd
{
    public class CommandNetshIpConfig : ICommandGenerator, IDisposable
    {
        public NetIpConfigModel NetIpConfig { get; set; }
        public string InterfaceDescription { get; set; }

        public CommandNetshIpConfig(string interfaceDescription, NetIpConfigModel netIpConfigModel)
        {
            InterfaceDescription = interfaceDescription;
            NetIpConfig = netIpConfigModel;
        }
        public CommandNetshIpConfig()
        {
            InterfaceDescription = "";
            NetIpConfig = new NetIpConfigModel();
        }

        public string Generate()
        {
            if (NetIpConfig.IsDhcpEnabled)
            {
                return "/c netsh interface ip set address \"" + InterfaceDescription + "\" dhcp &  netsh interface ip set dns \"" + InterfaceDescription + "\" dhcp";
            }
            else
            {
                string command = "/c netsh interface ip set address \"" + InterfaceDescription + "\" static "
                    + NetIpConfig.IpAddress + " " + NetIpConfig.SubnetMask + " " + NetIpConfig.Gateway;

                if (NetIpConfig.DnsServer1 != null)
                    command += " & netsh interface ip set dns \"" + InterfaceDescription + "\" static " + NetIpConfig.DnsServer1;

                return command;
            }
        }

        public void Dispose()
        {
            NetIpConfig = null;
            InterfaceDescription = null;
        }

    }
}
