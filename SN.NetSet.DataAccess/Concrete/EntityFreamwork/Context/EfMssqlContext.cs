using Microsoft.EntityFrameworkCore;

namespace SN.NetSet.DataAccess.Concrete.EntityFreamwork.Context
{
    public class EfMssqlContext : EfContextBase
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=NetDb;Trusted_Connection=true");
        }
    }
}
