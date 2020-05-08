using SN.Data.DataAccess.FakeDb;
using SN.NetSet.Entities.Concrete.Network;
using SN.NetSet.Entities.Concrete.User;
using System.Collections.Generic;

namespace SN.NetSet.DataAccess.Concrete.FakeDb
{
    public class FakeDbContext : FakeDbContextBase
    {
        private static readonly object _lock = new object();
        private static FakeDbContext _instance;
        public static FakeDbContext GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new FakeDbContext();
                    }
                }
                return _instance;
            }
        }
        private FakeDbContext()
        {
            OnConfigure();

        }

        public List<NetConfigBase> NetConfigs { get; set; }
        public List<User> Users { get; set; }

        public override void OnConfigure()
        {
            NetConfigs = new List<NetConfigBase>() {
              new NetConfigBase
                {
                    Id = 1,
                    ConfigName = "Config1",
                    IpAddress = "192.168.1.123",
                    SubnetMask = "255.255.255.0",
                    Gateway = "192.168.1.1",
                    DnsServer1 = "192.27.27.12",
                    DnsServer2 = "192.2.2.12"
                },
                new NetConfigBase
                {
                    Id = 2,
                    ConfigName = "Config2",
                    IpAddress = "10.10.10.27",
                    SubnetMask = "255.255.0.0",
                    Gateway = "10.10.10.1",
                    DnsServer1 = "8.8.4.8",
                    DnsServer2 = ""
                },
                                new NetConfigBase
                {
                    Id = 3,
                    ConfigName = "Config3",
                    IpAddress = "192.168.0.27",
                    SubnetMask = "255.255.255.0",
                    Gateway = "",
                    DnsServer1 = "",
                    DnsServer2 = ""
                }
            };

            Users = new List<User>() {
                new User { Id = 1, UserName = "esen" },
                new User { Id = 2, UserName = "snuser" },
                new User { Id = 3, UserName = "admin" },
            };
        }


    }
}
