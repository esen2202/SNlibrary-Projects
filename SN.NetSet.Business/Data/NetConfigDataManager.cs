using SN.NetSet.Business.Abstract;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.Entities.Concrete.Network;
using System.Collections.Generic;

namespace SN.NetSet.Business.Data
{
    public class NetConfigDbManager : INetConfigDataService
    {
        INetConfigDal _netConfigDal;
        public NetConfigDbManager(INetConfigDal netConfigDal)
        {
            _netConfigDal = netConfigDal;
        }

        public NetConfigBase GetConfig(int id) => _netConfigDal.Get(config => config.Id == id);

        public IList<NetConfigBase> GetConfigList() => _netConfigDal.GetList();
     
        public void AddNewConfig(NetConfigBase config) => _netConfigDal.Add(config);
  
        public void UpdateConfig(NetConfigBase config) => _netConfigDal.Update(config);

        public void DeleteConfig(NetConfigBase config) => _netConfigDal.Delete(config);
    }
}
