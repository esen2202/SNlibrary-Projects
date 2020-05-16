using SQLite;
using System;

namespace SN.Data.DataAccess.SQLiteNet
{
    public class SQLiteConnectionManager : ISQLiteConnectionService, IDisposable
    {
        public string DbName { get; set; }
        private SQLiteConnection _sqliteConnection;

        public SQLiteConnectionManager(string dbName)
        {
            DbName = dbName;
        }

        public SQLiteConnection GetConnection()
        {
            
           // _sqliteConnection = new SQLiteConnection("deneme");

            _sqliteConnection = new SQLiteConnection(DbName);

            return _sqliteConnection;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
