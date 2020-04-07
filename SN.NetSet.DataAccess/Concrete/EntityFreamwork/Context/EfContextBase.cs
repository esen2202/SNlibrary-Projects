using Microsoft.EntityFrameworkCore;
using SN.NetSet.Entities.Concrete.Network;
using SN.NetSet.Entities.Concrete.User;

namespace SN.NetSet.DataAccess.Concrete.EntityFreamwork
{
    public class EfContextBase : DbContext
    {
        public DbSet<NetConfig> NetConfigs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}