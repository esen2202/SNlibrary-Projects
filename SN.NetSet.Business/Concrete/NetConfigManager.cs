﻿using SN.NetSet.Business.Abstract;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.Entities.Concrete.Network;

namespace SN.NetSet.Business.Concrete
{
    public class NetConfigManager : INetConfigService
    {
        INetConfigDal _netConfigDal;
        public NetConfigManager(INetConfigDal netConfigDal)
        {
            _netConfigDal = netConfigDal;
        }

        public void GetNetworkList()
        {
            var netlist = _netConfigDal.GetList();
        }

        public void Save()
        {
            _netConfigDal.Add(new  NetConfigBase());
        }
    }
}
