using SN.NetSet.UI.WPF.Commands.Generic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SN.NetSet.UI.WPF.ViewModels
{
    public class _PingingViewModel : ViewModelBase
    {
        private string hostName;

        public string HostName
        {
            get { return hostName; }
            set
            {
                hostName = value;
                OnPropertyChanged(() => HostName);
            }
        }


        private ObservableCollection<string> pingResultList;

        public ObservableCollection<string> PingResultList
        {
            get { return pingResultList; }
            set
            {
                pingResultList = value;
                OnPropertyChanged(() => PingResultList);
            }
        }

        public _PingingViewModel()
        {
            PingResultList = new ObservableCollection<string>();
        }

        private ICommand _pingCommand;
        public ICommand PingCommand
        {
            get
            {
                if (_pingCommand == null)
                {
                    _pingCommand = new RelayCommand(
                    p => this.PingMethod((string)p));
                }
                return _pingCommand;
            }
        }

        private async void PingMethod(string p)
        {
            PingResultList.Add(await NetSet.Business.Network.NetworkTools.Ping(p, 1000));
        }

    }
}
