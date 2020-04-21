using SN.Network.Model;
using System.Collections.Generic;

namespace SN.Network.Information
{
    public interface INetAdapterInfoService
    {
        List<NetAdapterModel> GetAdapterList();
        List<NetAdapterModelBase> GetAdapterCaptionList();
        NetAdapterModel GetAdapter(string adapterDesc);
    }
}