using SN.Library.DataAccess.FakeDb;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.Entities.Concrete.User;
using System.Collections.Generic;
namespace SN.NetSet.DataAccess.Concrete.FakeDb
{
    public class FakeDbUserDal : FakeDbEntityRepositoryBase<User, List<User>>, IUserDal
    {
        public FakeDbUserDal() : base(FakeDbContext.GetInstance.Users)
        {
        }
    }
}
