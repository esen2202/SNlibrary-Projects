using Ninject.Modules;
using SN.NetSet.Business.Abstract;
using SN.NetSet.Business.Data;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.DataAccess.Concrete.FakeDb;
using SN.NetSet.DataAccess.Concrete.SQLite;

namespace SN.NetSet.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<INetConfigDataService>().To<NetConfigDbManager>();
            Bind<INetConfigDal>().To<SQLiteNetConfigDal>();
            Bind<IUserDal>().To<SQLiteUserDal>();
        }
    }
}
