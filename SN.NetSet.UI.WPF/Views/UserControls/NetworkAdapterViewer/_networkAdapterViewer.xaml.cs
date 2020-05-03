using SN.Network.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SN.NetSet.UI.WPF.Views.UserControls.NetworkAdapterViewer
{
    /// <summary>
    /// Interaction logic for _networkAdapterViewer.xaml
    /// </summary>
    public partial class _NetworkAdapterViewer : UserControl
    {
        public _NetworkAdapterViewer()
        {
            InitializeComponent();
            this.DataContextChanged += NetListItem_DataContextChanged;
        }

        private void NetListItem_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var dataContext = (NetAdapterModel)this.DataContext;
            IsDHCPIconChanger(dataContext.IpConfig.IsDhcpEnabled);
            IsNetworkIconChanger(dataContext.IsOperationalStatusUp);
            IsInternetIconChanger(dataContext.Internet);
        }

        private void IsDHCPIconChanger(bool status)
        {
            iconDHCP.Kind = status ? MaterialDesignThemes.Wpf.PackIconKind.Check : MaterialDesignThemes.Wpf.PackIconKind.Cancel;
            iconDHCP.Foreground = status ? new SolidColorBrush(Color.FromArgb(255, 59, 255, 0)) : new SolidColorBrush(Color.FromArgb(255, 255, 97, 89));
        }

        private void IsNetworkIconChanger(bool status)
        {
            iconNetworkStatus.Kind = status ? MaterialDesignThemes.Wpf.PackIconKind.LanConnect : MaterialDesignThemes.Wpf.PackIconKind.LanDisconnect;
            iconNetworkStatus.Foreground = status ? new SolidColorBrush(Color.FromArgb(255, 59, 255, 0)) : new SolidColorBrush(Color.FromArgb(255, 255, 97, 89));
        }

        private void IsInternetIconChanger(bool status)
        {
            iconInternetStatus.Kind = status ? MaterialDesignThemes.Wpf.PackIconKind.Earth : MaterialDesignThemes.Wpf.PackIconKind.EarthOff;
            iconInternetStatus.Foreground = status ? new SolidColorBrush(Color.FromArgb(255, 59, 255, 0)) : new SolidColorBrush(Color.FromArgb(255, 255, 97, 89));
        }

        private void HyperlinkGateway_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
