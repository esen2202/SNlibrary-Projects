﻿using SN.Cmd;
using SN.Network.Model;
using System;

namespace SN.Network.Config.CommandGenerator
{
    public class CommandGeneratorNetshIpConfigurator : ICommandGeneratorIpConfigurator
    {
        private string _interfaceName;
        private NetIpConfigModel _netIpConfigModel;

        public ICommandGenerator ParameterInject(string interfaceName, NetIpConfigModel netIpConfigModel = null)
        {
            _interfaceName = interfaceName;
            _netIpConfigModel = netIpConfigModel;
            return this;
        }

        public string Generate()
        {
            string command = "";

            if (_netIpConfigModel == null || _netIpConfigModel.IsDhcpEnabled)
            {
                command = "/c netsh interface ip set address \"" + _interfaceName + "\" dhcp &  netsh interface ip set dns \"" + _interfaceName + "\" dhcp";
            }
            else
            {
                command = "/c netsh interface ip set address \"" + _interfaceName + "\" static "
                    + _netIpConfigModel.IpAddress + " " + _netIpConfigModel.SubnetMask + " " + _netIpConfigModel.Gateway;

                if (!string.IsNullOrEmpty(_netIpConfigModel.DnsServer1))
                {
                    command += " & netsh interface ip set dnsservers \"" + _interfaceName + "\" static " + _netIpConfigModel.DnsServer1 + " primary";

                    if (!string.IsNullOrEmpty(_netIpConfigModel.DnsServer2))
                        command += " & netsh interface ip add dnsservers \"" + _interfaceName + "\" " + _netIpConfigModel.DnsServer2 + " index =2";
                }
                else
                {
                    command += " & netsh interface ip set dns \"" + _interfaceName + "\" dhcp";
                }
            }

            return command;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}