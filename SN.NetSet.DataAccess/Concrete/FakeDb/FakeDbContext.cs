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

        public List<NetConfig> NetConfigs { get; set; }
        public List<User> Users { get; set; }

        public override void OnConfigure()
        {
            NetConfigs = new List<NetConfig>();
            Users = new List<User>();
        }

    }
}
