using SN.Network.Info.NetAdapter;
using System;

namespace SN.NetSet.Business.Abstract
{
    public interface INetAdapterInfoService : INetAdapterInfo, IDisposable
    {
        event EventHandler ReloadedAdapterList;

        bool SuspendThread { get; set; }
    }
}
