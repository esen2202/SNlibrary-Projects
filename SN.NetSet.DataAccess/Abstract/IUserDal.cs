using SN.Data.DataAccess;
using SN.NetSet.Entities.Concrete.User;

namespace SN.NetSet.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        //Custom operations
    }
}
