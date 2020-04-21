using System;
using System.Collections.Generic;
using System.Text;

namespace SN.Data.DataAccess.SQLiteNet
{
    public abstract class SQLiteDbContextBase
    {

        public abstract void OnConfigure();
    }
}
