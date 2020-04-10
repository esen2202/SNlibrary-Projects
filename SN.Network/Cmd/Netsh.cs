using SN.Cmd;
using System;

namespace SN.Network.Abstract
{
    public class Netsh : INetAdapterConfigure
    {
         string adapter, ip, subnet, gateway, dns1;

        ICommandLine _cmd { get; set; }

        public Netsh(ICommandLine cmd)
        {
            _cmd = cmd;
        }

        void Execute(String action)
        {
            int exitCode;
            var result = _cmd.Execute(action, out exitCode);
        }

        void SetIP4Configuration()
        {

        }

        void Create()
        {
            var dhcp = "/c netsh interface ip set address \"" + adapter + "\" dhcp & netsh interface ip set dns \"" + adapter + "\" dhcp";

            var ipset ="/c netsh interface ip set address \"" + adapter + "\" static " + ip +
              " " + subnet + " " + gateway + " & netsh interface ip set dns \"" +
              adapter + "\" static " + dns1;

        }
    }
}
