using Ninject.Modules;
using SN.NetSet.Business.Abstract;
using SN.NetSet.Business.Concrete;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.DataAccess.Concrete.EntityFreamwork;
using SN.NetSet.DataAccess.Concrete.FakeDb;
using SN.NetSet.DataAccess.Concrete.SQLite;

namespace SN.NetSet.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<INetConfigService>().To<NetConfigManager>();
            Bind<INetConfigDal>().To<FakeDbNetConfigDal>();
            Bind<IUserDal>().To<FakeDbUserDal>();
        }
    }
}
