using AutoMapper;
using SN.Class.Derivations;
using SN.Cmd;
using SN.NetSet.Business.Abstract;
using SN.NetSet.Business.DependencyResolvers.Ninject;
using SN.NetSet.Entities.Concrete.Network;
using SN.NetSet.UI.WPF.Commands.Generic;
using SN.NetSet.UI.WPF.Views;
using SN.Network.Model;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace SN.NetSet.UI.WPF.ViewModels
{
    public class _ConfigPanelViewModel : ViewModelBase
    {
        public event EventHandler<EventArgsWithStrMessage> ReturnedResultMessage;

        private bool _enabledAddingMode;
        public bool EnabledAddingMode
        {
            get { return _enabledAddingMode; }
            set
            {
                _enabledAddingMode = value;
                base.OnPropertyChanged(() => EnabledAddingMode);
            }
        }

        private bool _enabledUpdateMode;
        public bool EnabledUpdateMode
        {
            get { return _enabledUpdateMode; }
            set
            {
                _enabledUpdateMode = value;
                base.OnPropertyChanged(() => EnabledUpdateMode);
            }
        }

        private bool _operationInProcess;
        public bool OperationInProcess
        {
            get { return _operationInProcess; }
            set
            {
                _operationInProcess = value;
                base.OnPropertyChanged(() => OperationInProcess);
            }
        }

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

        private NetConfigBase _newNetConfig;
        public NetConfigBase NewNetConfig
        {
            get { return _newNetConfig; }
            set
            {
                _newNetConfig = value;
                base.OnPropertyChanged(() => NewNetConfig);
            }
        }

        private string _dbPath;
        public string DbPath
        {
            get { return _dbPath; }
            set
            {
                _dbPath = value;
                base.OnPropertyChanged(() => DbPath);
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
            _netAdapterIpConfigService.SetIpOperationCompleted += this._netAdapterIpConfigService_SetIpOperationCompleted;

            DbPath = _netConfigDataService.GetAddress();
            NewNetConfig = new NetConfigBase();

            GetConfigList();
            GetLastSelectedConfig();

            _mainWindow = Application.Current.MainWindow as MainWindow;
        }

        private void _netAdapterIpConfigService_SetIpOperationCompleted(object sender, SN.Cmd.EventArgsWithStrMessage e)
        {
            ReturnedResultMessage?.Invoke(this, e);
            OperationInProcess = false;
        }

        private void GetConfigList()
        {
            NetConfigList = _mapper.Map<ObservableCollectionPropertyNotify<NetConfigBase>>(_netConfigDataService.GetConfigList());
        }

        private void GetLastSelectedConfig()
        {
            var config = (NetConfigBase)_netConfigDataService.GetConfig(MainWindow.LastConfigId);

            CurrentNetConfig = config != null
                ? (NetConfigBase)config.Clone()
                : new NetConfigBase();
        }

        private void RefreshConfigList()
        {
            GetConfigList();
            GetLastSelectedConfig();
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
                MainWindow.LastConfigId = CurrentNetConfig != null ? CurrentNetConfig.Id : 0;
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
                OperationInProcess = true;
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
                OperationInProcess = true;
                _netAdapterIpConfigService.SetIpConfig(interfaceName,
                    new NetIpConfigModel { IsDhcpEnabled = true });
            }
        }

        private ICommand _enableAddingModeCommand;
        public ICommand EnableAddingModeCommand
        {
            get
            {
                if (_enableAddingModeCommand == null)
                {
                    _enableAddingModeCommand = new RelayCommand(
                        p =>
                        {
                            EnableAddingModeMethod();
                        });
                }
                return _enableAddingModeCommand;
            }
        }
        private void EnableAddingModeMethod()
        {
            NewNetConfig = new NetConfigBase();
            EnabledAddingMode = true;
        }

        private ICommand _addConfigCommand;
        public ICommand AddConfigCommand
        {
            get
            {
                if (_addConfigCommand == null)
                {
                    _addConfigCommand = new RelayCommand(
                        p =>
                        {
                            AddConfigMethod((NetConfigBase)p);
                        });
                }
                return _addConfigCommand;
            }
        }
        private void AddConfigMethod(NetConfigBase config)
        {
            var ex = Class.Helpers.ExceptionHelper.CatchException(() =>
            {
                _netConfigDataService.AddNewConfig(config);
                RefreshConfigList();
                EnabledAddingMode = false;
            });

            if (ex != null) ReturnedResultMessage?.Invoke(this, new EventArgsWithStrMessage
            {
                Message = ex.Message,
            });
        }

        private ICommand _updateConfigCommand;
        public ICommand UpdateConfigCommand
        {
            get
            {
                if (_updateConfigCommand == null)
                {
                    _updateConfigCommand = new RelayCommand(
                        p =>
                        {
                            UpdateConfigMethod((NetConfigBase)p);
                        });
                }
                return _updateConfigCommand;
            }
        }
        private void UpdateConfigMethod(NetConfigBase config)
        {
            var ex = Class.Helpers.ExceptionHelper.CatchException(() =>
            {
                _netConfigDataService.UpdateConfig(config);
                RefreshConfigList();
                EnabledUpdateMode = false;
            });

            if (ex != null) ReturnedResultMessage?.Invoke(this, new EventArgsWithStrMessage
            {
                Message = ex.Message
            });
        }

        private ICommand _enableUpdateModeCommand;
        public ICommand EnableUpdateModeCommand
        {
            get
            {
                if (_enableUpdateModeCommand == null)
                {
                    _enableUpdateModeCommand = new RelayCommand(
                        p =>
                        {
                            EnableUpdateModeMethod((NetConfigBase)p);
                        });
                }
                return _enableUpdateModeCommand;
            }
        }
        private void EnableUpdateModeMethod(NetConfigBase config)
        {
            NewNetConfig = config;
            SelectConfigMethod(config);
            EnabledUpdateMode = true;
        }

        private ICommand _deleteConfigCommand;
        public ICommand DeleteConfigCommand
        {
            get
            {
                if (_deleteConfigCommand == null)
                {
                    _deleteConfigCommand = new RelayCommand(
                        p =>
                        {
                            DeleteConfigMethod((NetConfigBase)p);
                        });
                }
                return _deleteConfigCommand;
            }
        }
        private void DeleteConfigMethod(NetConfigBase config)
        {
            _netConfigDataService.DeleteConfig(config);
            RefreshConfigList();
        }

        private ICommand _cancelModeCommand;
        public ICommand CancelModeCommand
        {
            get
            {
                if (_cancelModeCommand == null)
                {
                    _cancelModeCommand = new RelayCommand(
                        p =>
                        {
                            CancelUpdateModeMethod();
                            CancelAddingModeMethod();
                        });
                }
                return _cancelModeCommand;
            }
        }

        private ICommand _addUpdateConfigCommand;
        public ICommand AddUpdateConfigCommand
        {
            get
            {
                if (_addUpdateConfigCommand == null)
                {
                    _addUpdateConfigCommand = new RelayCommand(
                        p =>
                        {
                            if (EnabledAddingMode)
                                AddConfigMethod((NetConfigBase)p);
                            if (EnabledUpdateMode)
                                UpdateConfigMethod((NetConfigBase)p);
                        });
                }
                return _addUpdateConfigCommand;
            }
        }

        private void CancelUpdateModeMethod()
        {
            EnabledUpdateMode = false;
        }
        private void CancelAddingModeMethod()
        {
            EnabledAddingMode = false;
        }


        public void ImportConfigDb(string json)
        {
            _netConfigDataService.ImportFromJsonListConfig(json);
            RefreshConfigList();
        }

        public void ExportConfigDb(string fileName)
        {
            _netConfigDataService.SaveToFileLisConfig(fileName);
        }

        #endregion

    }
}
