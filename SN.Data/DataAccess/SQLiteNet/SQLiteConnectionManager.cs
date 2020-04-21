using SQLite;
using System;

namespace SN.Data.DataAccess.SQLiteNet
{
    public class SQLiteConnectionManager : ISQLiteConnectionService,IDisposable
    {
        public string DbName { get; set; }
        public SQLiteConnectionManager(string dbName)
        {
            DbName = dbName;
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(DbName);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
