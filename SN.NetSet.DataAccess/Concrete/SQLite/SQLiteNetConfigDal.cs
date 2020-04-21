using SN.Data.DataAccess.SQLiteNet;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.Entities.Concrete.Network;

namespace SN.NetSet.DataAccess.Concrete.SQLite
{
    public class SQLiteNetConfigDal : SQLiteEntityRepositoryBase<NetConfigBase>, INetConfigDal
    {
        public SQLiteNetConfigDal() : base(SQLiteContext.GetInstance.ConnectionManager) { }

    }
}
