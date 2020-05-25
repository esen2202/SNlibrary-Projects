using SN.Data.DataAccess.SQLiteNet;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.Entities.Concrete.Network;
using System.Collections.Generic;

namespace SN.NetSet.DataAccess.Concrete.SQLite
{
    public class SQLiteNetConfigDal : SQLiteEntityRepositoryBase<NetConfigBase>, INetConfigDal,ISQLiteDBProperties
    {
        public SQLiteNetConfigDal() : base(SQLiteContext.GetInstance.ConnectionManager) { }

        public string GetDBPath()
        {
            return SQLiteContext.GetInstance.ConnectionManager.GetConnection().DatabasePath;
        }
    }
}
