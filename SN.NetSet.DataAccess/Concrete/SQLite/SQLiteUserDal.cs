using SN.Data.DataAccess.SQLiteNet;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.Entities.Concrete.User;

namespace SN.NetSet.DataAccess.Concrete.SQLite
{
    public class SQLiteUserDal : SQLiteEntityRepositoryBase<User>, IUserDal
    {
        public SQLiteUserDal() : base(SQLiteContext.GetInstance.ConnectionManager) { }
    }
}
