using SN.NetSet.UI.WPF.Views.UserControls.NetworkConfigList;
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
                    currentControl?.DeleteQuestionReset();
                }
            }
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
    where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
