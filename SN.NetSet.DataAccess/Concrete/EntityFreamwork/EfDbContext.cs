using Microsoft.EntityFrameworkCore;
using SN.NetSet.Entities.Concrete.Network;
using SN.NetSet.Entities.Concrete.User;

namespace SN.NetSet.DataAccess.Concrete.EntityFreamwork
{
    public class EfDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=NetDb;Trusted_Connection=true");
        }
        public DbSet<NetConfigBase> NetConfigs { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
