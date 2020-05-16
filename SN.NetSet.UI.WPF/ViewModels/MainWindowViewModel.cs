using AutoMapper;
using SN.Class.Derivations;
using SN.NetSet.Business.Abstract;
using SN.NetSet.Business.DependencyResolvers.Ninject;
using SN.NetSet.Business.Network;
using SN.NetSet.UI.WPF.Commands.Generic;
using SN.NetSet.UI.WPF.Views;
using SN.Network.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SN.NetSet.UI.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IDisposable
    {
        public event EventHandler<ReceivedDataEventArgs> NetAdaptersUpdated;

        private readonly INetAdapterInfoService _netAdapterInfoService;

        private readonly IMapper _mapper;

        private ObservableCollectionPropertyNotify<NetAdapterModelBase> _adapterList;

        public ObservableCollectionPropertyNotify<NetAdapterModelBase> AdapterList
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

        private string _globalIp;

        public string GlobalIp
        {
            get { return _globalIp; }
            set
            {
                _globalIp = value;
                base.OnPropertyChanged(() => GlobalIp);
            }
        }

        public MainWindowViewModel()
        {
            _mapper = InstanceFactory.GetInstance<IMapper>();
            _netAdapterInfoService = InstanceFactory.GetInstance<INetAdapterInfoService>();

            _netAdapterInfoService.ReloadedAdapterList += _netAdapterInfoService_ReloadedAdapterList;

            GetGlobalIp();
        }

        private void _netAdapterInfoService_ReloadedAdapterList(object sender, EventArgs e)
        {
            AdapterList = _mapper.Map<ObservableCollectionPropertyNotify<NetAdapterModelBase>>(_netAdapterInfoService.GetAdapterCaptionList());

            MainWindow.LastAdapterName = CurrentAdapter == null ? MainWindow.LastAdapterName : CurrentAdapter.Name;

            NetAdapterModel adapter = (NetAdapterModel)_netAdapterInfoService.GetAdapter(MainWindow.LastAdapterName).Clone();

            if (adapter.Name != null)
            {
                CurrentAdapter = adapter;
            }
            else
            {
                CurrentAdapter = AdapterList.Count > 0
                         ? (NetAdapterModel)_netAdapterInfoService.GetAdapterList().First().Clone()
                         : new NetAdapterModel()
                         {
                             Name = "No Adapter",
                             Description = "Not Found Adapter",
                             IpConfig = new NetIpConfigModel()
                         };
            }

            NetAdaptersUpdated?.Invoke(this, new ReceivedDataEventArgs() { Id = 1, Message = "" });
        }

        public void SuspendInfoService()
        {
            _netAdapterInfoService.SuspendThread = true;
        }

        public void ContinueInfoService()
        {
            _netAdapterInfoService.SuspendThread = false;
        }

        #region Commands
        private ICommand _selectNetAdaptCommand;

        public ICommand SelectNetAdaptCommand
        {
            get
            {
                if (_selectNetAdaptCommand == null)
                {
                    _selectNetAdaptCommand = new RelayCommand(
                    p => this.SelectNetAdaptMethod(p));
                }
                return _selectNetAdaptCommand;
            }
        }

        private void SelectNetAdaptMethod(object p)
        {
            if (p != null)
            {
                NetAdapterModelBase obj = p as NetAdapterModelBase;
                CurrentAdapter = (NetAdapterModel)_netAdapterInfoService.GetAdapter(obj.Name).Clone();
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
                            RefreshExecuteMethod();
                        });
                }
                return _refreshExecuteCommand;
            }
        }

        private void RefreshExecuteMethod()
        {

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
                        p => ShowNetworksMethod());
                }
                return _showNetworksCommand;
            }
        }

        private void ShowNetworksMethod()
        {
            NetworkTools.ShowNetConnections();
        }

        private ICommand _copyToClipboard;

        public ICommand CopyToClipboard
        {
            get
            {
                if (_copyToClipboard == null)
                {
                    _copyToClipboard = new RelayCommand(
                    p => CopyToClipboardMethod(p));
                }
                return _copyToClipboard;
            }
        }

        private static void CopyToClipboardMethod(object p)
        {
            Clipboard.SetText((string)p);
        }

        private ICommand _refreshGlobalIp;

        public ICommand RefreshGlobalIp
        {
            get
            {
                if (_refreshGlobalIp == null)
                {
                    _refreshGlobalIp = new RelayCommand(
                    p => GetGlobalIp());
                }
                return _refreshGlobalIp;
            }
        }

        private async void GetGlobalIp()
        {
            GlobalIp = await NetworkTools.GetGlobalIpAsync();
        }

        public void Dispose()
        {
            _netAdapterInfoService.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class ReceivedDataEventArgs : EventArgs
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
