using SN.Network.Model;
using System;
using System.Collections.Generic;
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

namespace SN.NetSet.UI.WPF.Views.UserControls.NetworkAdapterList
{
    /// <summary>
    /// Interaction logic for _NetworkAdapterList.xaml
    /// </summary>
    public partial class _NetworkAdapterListItem : UserControl
    {
        public _NetworkAdapterListItem()
        {
            InitializeComponent();
            this.DataContextChanged += NetListItem_DataContextChanged;
        }

        private void NetListItem_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext != null)
                IsNetworkIconChanger(((NetAdapterModelBase)this.DataContext).IsOperationalStatusUp);
        }
        private void IsNetworkIconChanger(bool status)
        {
            iconNetworkStatus.Kind = status ? MaterialDesignThemes.Wpf.PackIconKind.LanConnect : MaterialDesignThemes.Wpf.PackIconKind.LanDisconnect;
            iconNetworkStatus.Foreground = status ? new SolidColorBrush(Color.FromArgb(255, 59, 255, 0)) : new SolidColorBrush(Color.FromArgb(255, 255, 97, 89));

        }
    }
}
