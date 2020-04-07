using SN.Library.DataAccess.FakeDb;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.Entities.Concrete.Network;
using System.Collections.Generic;

namespace SN.NetSet.DataAccess.Concrete.FakeDb
{
    public class FakeDbNetConfigDal : FakeDbEntityRepositoryBase <NetConfig,List<NetConfig>>, INetConfigDal
    {
        public FakeDbNetConfigDal(): base(FakeDbContext.GetInstance.NetConfigs)
        {
            
        }
    }
}
