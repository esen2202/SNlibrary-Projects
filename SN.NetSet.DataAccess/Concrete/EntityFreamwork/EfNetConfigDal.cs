using Microsoft.EntityFrameworkCore;
using SN.Data.DataAccess.EntityFramework;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.Entities.Concrete.Network;

namespace SN.NetSet.DataAccess.Concrete.EntityFreamwork
{
    public class EfNetConfigDal<TContext> : EfEntityRepositoryBase<NetConfig, TContext>, INetConfigDal
        where TContext : DbContext, new()
    {
    }
}
