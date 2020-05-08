using AutoMapper;
using SN.NetSet.Business.Abstract;
using SN.NetSet.Business.DependencyResolvers.Ninject;
using SN.Network.Info.NetAdapter;
using SN.Network.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SN.NetSet.Business.Network
{
    public class NetAdapterInfoManager : INetAdapterInfoService
    {
        INetAdapterInfo _netAdapterInfo;
        static List<NetAdapterModel> _netAdapterList;
        static List<NetAdapterModelBase> _netAdapterCaptionList;
        Timer timer;
        IMapper _mapper;

        public bool SuspendThread { get; set; }

        public event EventHandler ReloadedAdapterList;

        private void GetAdapterListThread(object state)
        {
            if (!SuspendThread)
            {
                _netAdapterList = _netAdapterInfo.GetAdapterList();
                _netAdapterCaptionList = _mapper.Map<List<NetAdapterModelBase>>(_netAdapterList);

                ReloadedAdapterList?.Invoke(this, new EventArgs());
            }
        }

        public NetAdapterInfoManager(INetAdapterInfo netAdapterInfo)
        {
            _netAdapterInfo = netAdapterInfo;
            _mapper = InstanceFactory.GetInstance<IMapper>();

            _netAdapterList = new List<NetAdapterModel>();
            _netAdapterCaptionList = new List<NetAdapterModelBase>();

            timer = new Timer(new TimerCallback(GetAdapterListThread), null, 1000, 5000);
        }

        public List<NetAdapterModel> GetAdapterList()
        {
            return _netAdapterList;
        }

        public List<NetAdapterModelBase> GetAdapterCaptionList()
        {
            return _netAdapterCaptionList;
        }

        public NetAdapterModel GetAdapter(string interfaceName)
        {
            var adapter = _netAdapterList.SingleOrDefault(o => o.Name == interfaceName) ?? new NetAdapterModel();
            adapter.IpConfig = adapter.IpConfig ?? new NetIpConfigModel();
            return adapter;
        }

        public void Dispose()
        {
            timer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
