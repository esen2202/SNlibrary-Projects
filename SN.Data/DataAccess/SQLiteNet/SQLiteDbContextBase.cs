using System;

namespace SN.Data.DataAccess.SQLiteNet
{
    public abstract class SQLiteDbContextBase
    {
        public abstract void OnConfigure();
    }
}
