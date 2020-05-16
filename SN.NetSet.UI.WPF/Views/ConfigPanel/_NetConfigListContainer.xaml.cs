using SN.NetSet.UI.WPF.Views.UserControls.NetworkConfigList;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SN.NetSet.UI.WPF.Views.ConfigPanel
{
    /// <summary>
    /// Interaction logic for _NetConfigListContainer.xaml
    /// </summary>
    public partial class _NetConfigListContainer : UserControl
    {
        public _NetConfigListContainer()
        {
            InitializeComponent();
        }

        private void LBConfigList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item1 in LBConfigList.Items)
            {
                var border = LBConfigList.ItemContainerGenerator.ContainerFromItem(item1) as FrameworkElement;
                if (border != null)
                {
                    _NetworkConfigListItem currentControl =
                        VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(VisualTreeHelper.GetChild(border, 0) as Border, 0) as ContentPresenter, 0) as _NetworkConfigListItem;
                    currentControl?.DeleteDialogReset();
                }
            }
        }
    }
}
