using SN.NetSet.Business.Abstract;
using SN.NetSet.DataAccess.Abstract;
using SN.NetSet.Entities.Concrete.Network;

namespace SN.NetSet.Business.Data
{
    public class NetConfigDbManager : INetConfigDataService
    {
        INetConfigDal _netConfigDal;
        public NetConfigDbManager(INetConfigDal netConfigDal)
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
