using SN.Regedit.Startup;
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

namespace SN.NetSet.UI.WPF.Views.MainPanel
{
    /// <summary>
    /// Interaction logic for _AppSettings.xaml
    /// </summary>
    public partial class _AppSettings : UserControl
    {
        const string APPNAME = "SNNetSet";

        public _AppSettings()
        {
            InitializeComponent();
            this.Loaded += _AppSettings_Loaded;
        }

        private void _AppSettings_Loaded(object sender, RoutedEventArgs e)
        {
            CBStartup.IsChecked = RegeditHelper.IsExistStartup(APPNAME);
        }

        private void CBStartup_Checked(object sender, RoutedEventArgs e)
        {
            RegeditHelper.SetStartup(System.Reflection.Assembly.GetEntryAssembly().Location, APPNAME);
        }

        private void CBStartup_Unchecked(object sender, RoutedEventArgs e)
        {
            RegeditHelper.DeleteStartup(APPNAME);
        }
    }
}
