using SN.NetSet.Business.Abstract;
using SN.Network.Info.NetAdapter;
using SN.Network.Model;
using System.Collections.Generic;

namespace SN.NetSet.Business.Network
{
    public class NetAdapterInfoManager : INetAdapterInfoService
    {
        INetAdapterInfo _netAdapterInfo;

        public NetAdapterInfoManager(INetAdapterInfo netAdapterInfo)
        {
            _netAdapterInfo = netAdapterInfo;
        }

        public  List<NetAdapterModel> GetAdapterList()
        {
            return _netAdapterInfo.GetAdapterList();
        }

        public List<NetAdapterModelBase> GetAdapterCaptionList()
        {
            return _netAdapterInfo.GetAdapterCaptionList();
        }

        public NetAdapterModel GetAdapter(string adapterDesc)
        {
            return _netAdapterInfo.GetAdapter(adapterDesc);
        }
    }
}
