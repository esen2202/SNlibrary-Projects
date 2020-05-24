#define Ef
#if NoEf
using SN.Data.DataAccess.EntityFramework;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.Entities.Concrete.Network;

namespace SN.NetSet.DataAccess.Concrete.EntityFreamwork
{
    public class EfNetConfigDal : EfEntityRepositoryBase<NetConfigBase, EfDbContext>, INetConfigDal
    {
    }
}
#endif