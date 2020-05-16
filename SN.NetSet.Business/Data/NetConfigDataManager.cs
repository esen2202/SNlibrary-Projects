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

        public NetConfigBase GetConfig(int id)
        {
            return _netConfigDal.Get(o => o.Id == id);
        }

        public IList<NetConfigBase> GetConfigList() => _netConfigDal.GetList();

        public void AddNewConfig(NetConfigBase config)
        {
            if (!string.IsNullOrEmpty(config.ConfigName) &&
                !string.IsNullOrEmpty(config.IpAddress) &&
                !string.IsNullOrEmpty(config.SubnetMask))
            {
                _netConfigDal.Add(config);
            }
            else
            {
                throw new System.Exception("* fileds are required");
            }
        }

        public void UpdateConfig(NetConfigBase config)
        {
            if (!string.IsNullOrEmpty(config.ConfigName) &&
               !string.IsNullOrEmpty(config.IpAddress) &&
               !string.IsNullOrEmpty(config.SubnetMask))
            {
                _netConfigDal.Update(config, record => record.Id == config.Id);
            }
            else
            {
                throw new System.Exception("* fileds are required");
            }


        }

        public void DeleteConfig(NetConfigBase config)
        {
            _netConfigDal.Delete(config);
        }
    }
}
