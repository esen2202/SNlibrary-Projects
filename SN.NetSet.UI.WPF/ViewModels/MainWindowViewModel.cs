using AutoMapper;
using Ninject.Infrastructure.Language;
using SN.NetSet.Business.Abstract;
using SN.NetSet.Business.DependencyResolvers.Ninject;
using SN.NetSet.Business.Network;
using SN.NetSet.UI.WPF.Commands.Generic;
using SN.Network.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SN.NetSet.UI.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private ObservableCollection<NetAdapterModelBase> _adapterList;
        public ObservableCollection<NetAdapterModelBase> AdapterList
        {
            get { return _adapterList; }
            set
            {
                _adapterList = value;
                base.OnPropertyChanged(() => AdapterList);
            }
        }

        private NetAdapterModel _currentAdapter;
        public NetAdapterModel CurrentAdapter
        {
            get { return _currentAdapter; }
            set
            {
                _currentAdapter = value;
                base.OnPropertyChanged(() => CurrentAdapter);
            }
        }

        public string GlobalIp
        {
            get { return NetworkTools.GetGlobalIp(); }
            set
            {
                base.OnPropertyChanged(() => GlobalIp);
            }
        }

        private readonly INetAdapterInfoService _netAdapterInfoService;
        private readonly INetAdapterIpConfigService _netAdapterIpConfigService;

        private readonly IMapper _mapper;
        public MainWindowViewModel()
        {
            _mapper = InstanceFactory.GetInstance<IMapper>();

            _netAdapterInfoService = InstanceFactory.GetInstance<INetAdapterInfoService>();
            _netAdapterInfoService.ReloadedAdapterList += _netAdapterInfoService_ReloadedAdapterList;

            _netAdapterIpConfigService = InstanceFactory.GetInstance<INetAdapterIpConfigService>();
            
        }

        private void _netAdapterInfoService_ReloadedAdapterList(object sender, EventArgs e)
        {
            AdapterList = _mapper.Map<ObservableCollection<NetAdapterModelBase>>(_netAdapterInfoService.GetAdapterCaptionList());

            if(CurrentAdapter == null) 
                CurrentAdapter = _netAdapterInfoService.GetAdapterList().First();
        }

        private ICommand _selectNetAdaptCommand;
        public ICommand SelectNetAdaptCommand
        {
            get
            {
                if (_selectNetAdaptCommand == null)
                {
                    _selectNetAdaptCommand = new RelayCommand(
                    p => this.SelectedAdapter(p));
                }
                return _selectNetAdaptCommand;
            }
        }
        private void SelectedAdapter(object p)
        {
            if (p != null)
            {
                NetAdapterModelBase obj = p as NetAdapterModelBase;
                CurrentAdapter = _netAdapterInfoService.GetAdapter(obj.Description);
            }
        }

        private ICommand _refreshExecuteCommand;
        public ICommand RefreshExecuteCommand
        {
            get
            {
                if (_refreshExecuteCommand == null)
                {
                    _refreshExecuteCommand = new RelayCommand(
                        p =>
                        {
                            //Action
                            ChangeAdapterSettingsTest(p);
                        });
                }
                return _refreshExecuteCommand;
            }
        }

        int i;
        private void ChangeAdapterSettingsTest(object p)
        {
            AdapterList[0].Description = "description " + i.ToString();
            //var c = (NetAdapterModel)CurrentAdapter.Clone();
            CurrentAdapter.Speed = i * 5;
            i++;
            //CurrentAdapter = c;
            //GC.SuppressFinalize(c);

            //base.OnPropertyChanged(() => CurrentAdapter);
            CurrentAdapter = (NetAdapterModel)CurrentAdapter.Clone();
            //AdapterList.Add(new NetAdapterModelBase { Description = "description " + i.ToString(), IsOperationalStatusUp = false, Name = "name " + i.ToString() });

            var changeAdapter = CurrentAdapter.IpConfig;
            CurrentAdapter.IpConfig.IpAddress = "ip " + i.ToString();

            _netAdapterIpConfigService.SetIpConfig(CurrentAdapter.Description, changeAdapter );

            CurrentAdapter = _netAdapterInfoService.GetAdapter(CurrentAdapter.Description);
            Network.Info.NetAdapter.NetAdapterInfoFake.netAdapterModels[0].Name =  "name " + i.ToString();
        }

        private ICommand _closeWindowCommand;
        public ICommand CloseWindowCommand
        {
            get
            {
                if (_closeWindowCommand == null)
                {
                    _closeWindowCommand = new RelayCommand(
                        p => Application.Current.Shutdown());
                }
                return _closeWindowCommand;
            }
        }

        private ICommand _showNetworksCommand;
        public ICommand ShowNetworksCommand
        {
            get
            {
                if (_showNetworksCommand == null)
                {
                    _showNetworksCommand =
                        new RelayCommand(
                        p => NetworkTools.ShowNetConnections());
                }
                return _showNetworksCommand;
            }
        }

        private ICommand _copyToClipboard;
        public ICommand CopyToClipboard
        {
            get
            {
                if (_copyToClipboard == null)
                {
                    _copyToClipboard = new RelayCommand(
                    p => Clipboard.SetText((string)p));
                }
                return _copyToClipboard;
            }
        }

    }
}
