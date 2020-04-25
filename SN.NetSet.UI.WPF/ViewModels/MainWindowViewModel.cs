using AutoMapper;
using SN.NetSet.Business.Abstract;
using SN.NetSet.Business.DependencyResolvers.Ninject;
using SN.NetSet.Business.Network;
using SN.Network.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SN.NetSet.UI.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private ObservableCollection<NetAdapterModelBase> _adapterList;

        public ObservableCollection<NetAdapterModelBase> AdapterList
        {
            get { return _adapterList; }
            set { _adapterList = value; }
        }

        private NetAdapterModel _currentAdapter;

        public NetAdapterModel CurrentAdapter
        {
            get { return _currentAdapter; }
            set { _currentAdapter = value; }
        }


        INetAdapterInfoService _netAdapterInfoService;

        IMapper _mapper;
        public MainWindowViewModel()
        {
            _mapper = InstanceFactory.GetInstance<IMapper>();

            _netAdapterInfoService = InstanceFactory.GetInstance<INetAdapterInfoService>();
            AdapterList = _mapper.Map<ObservableCollection<NetAdapterModelBase>>(_netAdapterInfoService.GetAdapterCaptionList());

            CurrentAdapter = _netAdapterInfoService.GetAdapterList().First();
        }


    }
}
