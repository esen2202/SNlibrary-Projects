using SN.Data.DataAccess.FakeDb;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.Entities.Concrete.Network;
using System.Collections.Generic;

namespace SN.NetSet.DataAccess.Concrete.FakeDb
{
    public class FakeDbNetConfigDal : FakeDbEntityRepositoryBase<NetConfigBase, List<NetConfigBase>>, INetConfigDal
    {
        static int id = 4;
        public FakeDbNetConfigDal() : base(FakeDbContext.GetInstance.NetConfigs)
        {
        }
        public override void Add(NetConfigBase entity)
        {
            entity.Id = id++;
            base.Add(entity);
        }

        public override void Delete(NetConfigBase entity)
        {
            var record = Get(o => o.Id == entity.Id);
            base.Delete(record);
        }
    }
}
