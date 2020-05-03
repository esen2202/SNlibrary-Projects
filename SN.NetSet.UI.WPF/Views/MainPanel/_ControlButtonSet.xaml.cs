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
    /// Interaction logic for _ControlButtonSet.xaml
    /// </summary>
    public partial class _ControlButtonSet : UserControl
    {
        public _ControlButtonSet()
        {
            InitializeComponent();
        }

        private void BtnAlwaysTop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnShowHideList_Click(object sender, RoutedEventArgs e)
        {
            var obj = (MainWindow)Application.Current.MainWindow;
            
            
            if (obj.MainPanel.ListVisibility == Visibility.Collapsed)
            {
                obj.MainPanel.ListVisibility = Visibility.Visible;
                obj.SizeToContent = SizeToContent.Manual;
                obj.Height = SystemParameters.WorkArea.Height;

            }
            else
            {
                obj.SizeToContent = SizeToContent.Height;
                obj.MainPanel.ListVisibility = Visibility.Collapsed;
            }
            ShowHideIconChanger(obj.MainPanel.ListVisibility == Visibility.Visible);
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowHideIconChanger(bool visible)
        {
            iconShowHide.Kind = visible == false ? MaterialDesignThemes.Wpf.PackIconKind.ArrowExpand : MaterialDesignThemes.Wpf.PackIconKind.ArrowCollapse;
        }
    }
}
