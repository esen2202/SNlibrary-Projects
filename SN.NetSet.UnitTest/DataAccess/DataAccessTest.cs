using Microsoft.VisualStudio.TestTools.UnitTesting;
using SN.Data.Entities;
using SN.NetSet.Business.DependencyResolvers.Ninject;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.DataAccess.Concrete.EntityFreamwork;
using SN.NetSet.DataAccess.Concrete.FakeDb;
using SN.NetSet.Entities.Concrete.Network;

namespace SN.NetSet.UnitTest
{
    [TestClass]
    public class DataAccessTest
    {
        private INetConfigDal dal;
        private IEntity entity;

        private void InitializeDB()
        {
            entity = new NetConfigBase();
            dal = InstanceFactory.GetInstance<INetConfigDal>();
        }



        private void AddSampleRecords()
        {
            dal.Add(
               new NetConfigBase()
               {
                   Id = 1,
                   ConfigName = "1",
                   IpAddress = "ip1",
                   Gateway = "gate1",
                   SubnetMask = "sub1"
               });

            dal.Add(
               new NetConfigBase()
               {
                   Id = 2,
                   ConfigName = "2",
                   IpAddress = "ip2",
                   Gateway = "gate2",
                   SubnetMask = "sub2"
               });
        }

        [TestMethod]
        public void IsCreatedFakeDBInstance()
        {
            InitializeDB();
            var list = dal.GetList();
            AddSampleRecords();

            list = dal.GetList();
            dal.Update(new NetConfigBase()
            {
                ConfigName = "yeni",
                Id = 1,
                Gateway = "y",
                IpAddress = "e",
                SubnetMask = "n"
            },
                o => o.Id == 1);
            list = dal.GetList();
            var del = dal.Get(o => o.Id == 2);
            dal.Delete(del);

            list = dal.GetList();
        }

    }
}
