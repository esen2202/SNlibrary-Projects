using SQLite;

namespace SN.Data.DataAccess.SQLiteNet
{
    public interface ISQLiteConnectionService
    {
        SQLiteConnection GetConnection();
    }
}
