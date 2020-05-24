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

namespace SN.NetSet.UI.WPF.Views.UserControls.NetworkConfigList
{
    /// <summary>
    /// Interaction logic for _NetworkConfigListItem.xaml
    /// </summary>
    public partial class _NetworkConfigListItem : UserControl
    {
        public Visibility DeleteDialog
        {
            get { return (Visibility)GetValue(DeleteDialogProperty); }
            set { SetValue(DeleteDialogProperty, value); }
        }

        public static readonly DependencyProperty DeleteDialogProperty =
            DependencyProperty.Register("DeleteDialog", typeof(Visibility), typeof(_NetworkConfigListItem), new PropertyMetadata(Visibility.Collapsed));

        public _NetworkConfigListItem()
        {
            InitializeComponent();
        }

        private void BtnDeleteDialog_Click(object sender, RoutedEventArgs e)
        {
            DeleteDialog = Visibility.Visible;
        }

        private void BtnCancelDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteDialog = Visibility.Collapsed;
        }

        public void DeleteDialogReset()
        {
            DeleteDialog = Visibility.Collapsed;
        }
    }
}
