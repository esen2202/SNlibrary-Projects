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
        Thread thread;
        IMapper _mapper;
        public event EventHandler ReloadedAdapterList;

        private void GetAdapterListThread()
        {
            while (true)
            {
                _netAdapterList = _netAdapterInfo.GetAdapterList();
                _netAdapterCaptionList = _mapper.Map<List<NetAdapterModelBase>>(_netAdapterList);

                ReloadedAdapterList?.Invoke(this,new EventArgs());

                Thread.Sleep(3000);
            }
        }

        public NetAdapterInfoManager(INetAdapterInfo netAdapterInfo)
        {
            _netAdapterInfo = netAdapterInfo;
            _mapper = InstanceFactory.GetInstance<IMapper>();

            _netAdapterList = new List<NetAdapterModel>();
            _netAdapterCaptionList = new List<NetAdapterModelBase>();
            thread = new Thread(GetAdapterListThread);
            thread.Start();
        }

        public List<NetAdapterModel> GetAdapterList()
        {
            return _netAdapterList;
                //_netAdapterInfo.GetAdapterList();
        }

        public List<NetAdapterModelBase> GetAdapterCaptionList()
        {
            return _netAdapterCaptionList;
                //_netAdapterInfo.GetAdapterCaptionList();
        }

        public NetAdapterModel GetAdapter(string adapterDesc)
        {
            return _netAdapterList.SingleOrDefault(o => o.Description == adapterDesc) ?? new NetAdapterModel();
            //_netAdapterInfo.GetAdapter(adapterDesc);
        }



    }
}
