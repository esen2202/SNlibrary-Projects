using SN.Data.DataAccess.SQLiteNet;
using SN.NetSet.Business.DependencyResolvers.Ninject;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.DataAccess.Concrete.SQLite;
using SN.NetSet.Entities.Concrete.Network;
using System;
using System.Collections.Generic;

namespace SN.NetSet.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CRUDOperations();

            Console.ReadKey();
        }

        private static void CRUDOperations()
        {
             INetConfigDal netConfigDal = InstanceFactory.GetInstance<INetConfigDal>();

            netConfigDal.Add(new NetConfigBase { ConfigName = "net1", Id = 1, Gateway = "192.159.2.23" });
            netConfigDal.Add(new NetConfigBase { ConfigName = "net2", Id = 2, Gateway = "192.1.2.1" });
            netConfigDal.Add(new NetConfigBase { ConfigName = "net3", Id = 3, Gateway = "192.2.2.2" });
            netConfigDal.Add(new NetConfigBase { ConfigName = "net4", Id = 4, Gateway = "192.3.2.3" });

            (netConfigDal.GetList() as List<NetConfigBase>).ForEach(item =>
            {
                Console.WriteLine("CN = {0}, Id {1}, G= {2}", item.ConfigName, item.Id, item.Gateway);

            });


            Console.WriteLine("---------------------------");

            var ent = netConfigDal.Get(o => o.Id == 2);
            netConfigDal.Delete(ent);

            (netConfigDal.GetList() as List<NetConfigBase>).ForEach(item =>
            {
                Console.WriteLine("CN = {0}, Id {1}, G= {2}", item.ConfigName, item.Id, item.Gateway);

            });
        }
    }

}
