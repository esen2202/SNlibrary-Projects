using SN.Data.DataAccess.SQLiteNet;
using SN.NetSet.Entities.Concrete.Network;
using SN.NetSet.Entities.Concrete.User;
using SQLite;
using System.IO;
using System.Reflection;

namespace SN.NetSet.DataAccess.Concrete.SQLite
{
    public class SQLiteContext : SQLiteDbContextBase
    {
        private static readonly object _lock = new object();

        private static SQLiteContext _instance;
        public static SQLiteContext GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new SQLiteContext();
                    }
                }
                return _instance;
            }
        }

        private SQLiteConnection sqliteConnection { get; set; }

        public ISQLiteConnectionService ConnectionManager { get; set; }

        private SQLiteContext()
        {
            OnConfigure();
        }

        public override void OnConfigure()
        {
            ConnectionManager = new SQLiteConnectionManager(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "NetSetDB"));
            sqliteConnection = ConnectionManager.GetConnection();
            var mapping = sqliteConnection.GetMapping(typeof(NetConfigBase), CreateFlags.AutoIncPK);

            sqliteConnection.CreateTable<User>();
            sqliteConnection.CreateTable<NetConfigBase>();
        }


    }
}
