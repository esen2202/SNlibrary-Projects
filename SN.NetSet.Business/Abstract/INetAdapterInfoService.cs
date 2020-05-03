using SN.Network.Info.NetAdapter;
using System;

namespace SN.NetSet.Business.Abstract
{
    public interface INetAdapterInfoService : INetAdapterInfo
    {
        event EventHandler ReloadedAdapterList;
    }
}
