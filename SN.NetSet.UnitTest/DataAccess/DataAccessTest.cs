using Microsoft.VisualStudio.TestTools.UnitTesting;
using SN.Data.Entities;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.DataAccess.Concrete.EntityFreamwork;
using SN.NetSet.DataAccess.Concrete.EntityFreamwork.Context;
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
            entity = new NetConfig();
            dal = new FakeDbNetConfigDal();
        }

        private void InitializeEfDB()
        {
            entity = new NetConfig();
            dal = new EfNetConfigDal<EfMssqlContext>();
        }

        private void AddSampleRecords()
        {
            dal.Add(
               new NetConfig()
               {
                   NetConfigId = 1,
                   ConfigName = "1",
                   IpAddress = "ip1",
                   Gateway = "gate1",
                   SubnetMask = "sub1"
               });

            dal.Add(
               new NetConfig()
               {
                   NetConfigId = 2,
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
            dal.Update(new NetConfig()
            {
                ConfigName = "yeni",
                NetConfigId = 1,
                Gateway = "y",
                IpAddress = "e",
                SubnetMask = "n"
            },
                o => o.NetConfigId == 1);
            list = dal.GetList();
            var del = dal.Get(o => o.NetConfigId == 2);
            dal.Delete(del);

            list = dal.GetList();
        }

    }
}
