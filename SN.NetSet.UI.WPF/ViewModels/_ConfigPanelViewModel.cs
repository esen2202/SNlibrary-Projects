using AutoMapper;
using SN.Class.Derivations;
using SN.NetSet.Business.Abstract;
using SN.NetSet.Business.DependencyResolvers.Ninject;
using SN.NetSet.Entities.Concrete.Network;
using SN.NetSet.UI.WPF.Commands.Generic;
using SN.NetSet.UI.WPF.Views;
using SN.Network.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SN.NetSet.UI.WPF.ViewModels
{
    public class _ConfigPanelViewModel : ViewModelBase
    {
        private ObservableCollectionPropertyNotify<NetConfigBase> _netConfigList;

        public ObservableCollectionPropertyNotify<NetConfigBase> NetConfigList
        {
            get { return _netConfigList; }
            set
            {
                _netConfigList = value;
                base.OnPropertyChanged(() => NetConfigList);
            }
        }

        private NetConfigBase _currentNetConfig;

        public NetConfigBase CurrentNetConfig
        {
            get { return _currentNetConfig; }
            set
            {
                _currentNetConfig = value;
                base.OnPropertyChanged(() => CurrentNetConfig);
            }
        }

        private readonly INetConfigDataService _netConfigDataService;

        private readonly INetAdapterIpConfigService _netAdapterIpConfigService;

        private readonly IMapper _mapper;

        private readonly MainWindow _mainWindow;

        public _ConfigPanelViewModel()
        {
            _mapper = InstanceFactory.GetInstance<IMapper>();
            _netConfigDataService = InstanceFactory.GetInstance<INetConfigDataService>();
            _netAdapterIpConfigService = InstanceFactory.GetInstance<INetAdapterIpConfigService>();

            NetConfigList = _mapper.Map<ObservableCollectionPropertyNotify<NetConfigBase>>(_netConfigDataService.GetConfigList());

            CurrentNetConfig = _netConfigDataService.GetConfig(MainWindow.LastConfigId);

            _mainWindow = Application.Current.MainWindow as MainWindow;
        }

        #region Commands
        private ICommand _selectConfigCommand;

        public ICommand SelectConfigCommand
        {
            get
            {
                if (_selectConfigCommand == null)
                {
                    _selectConfigCommand = new RelayCommand(
                    p => this.SelectConfigMethod(p));
                }
                return _selectConfigCommand;
            }
        }

        private void SelectConfigMethod(object p)
        {
            if (p != null)
            {
                NetConfigBase obj = p as NetConfigBase;
                CurrentNetConfig = (NetConfigBase)_netConfigDataService.GetConfig(obj.Id).Clone();
            }
        }

        private ICommand _applyConfigCommand;

        public ICommand ApplyConfigCommand
        {
            get
            {
                if (_applyConfigCommand == null)
                {
                    _applyConfigCommand = new RelayCommand(
                        p =>
                        {
                            ApplyConfigMethod((string)p);
                        });
                }
                return _applyConfigCommand;
            }
        }

        private void ApplyConfigMethod(string interfaceName)
        {
            if (CurrentNetConfig != null && !string.IsNullOrEmpty(interfaceName))
            {
                _netAdapterIpConfigService.SetIpConfig(interfaceName,
                    _mapper.Map<NetIpConfigModel>(CurrentNetConfig));
            }
        }

        private ICommand _activateDhcpCommand;

        public ICommand ActivateDhcpCommand
        {
            get
            {
                if (_activateDhcpCommand == null)
                {
                    _activateDhcpCommand = new RelayCommand(
                        p =>
                        {
                            ActivateDhcpMethod((string)p);
                        });
                }
                return _activateDhcpCommand;
            }
        }

        private void ActivateDhcpMethod(string interfaceName)
        {
            if (!string.IsNullOrEmpty(interfaceName))
            {
                _netAdapterIpConfigService.SetIpConfig(interfaceName,
                    new NetIpConfigModel { IsDhcpEnabled = true });
            }
        }

        #endregion

    }
}
