using SN.Network.Model;
using System.Collections.Generic;

namespace SN.Network.Info.NetAdapter
{
    public interface INetAdapterInfo
    {
        List<NetAdapterModel> GetAdapterList();
        List<NetAdapterModelBase> GetAdapterCaptionList();
        NetAdapterModel GetAdapter(string adapterDesc);
    }
}