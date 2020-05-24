#define Ef
#if NoEf
using Microsoft.EntityFrameworkCore;
using SN.Data.DataAccess.EntityFramework;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.Entities.Concrete.User;

namespace SN.NetSet.DataAccess.Concrete.EntityFreamwork
{
    public class EfUserDal : EfEntityRepositoryBase<User, EfDbContext>, IUserDal
    {
    }
}
#endif