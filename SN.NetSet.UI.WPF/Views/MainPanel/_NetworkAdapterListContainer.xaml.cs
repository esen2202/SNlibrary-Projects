using SN.NetSet.UI.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SN.NetSet.UI.WPF.Views.MainPanel
{
    /// <summary>
    /// Interaction logic for _NetworkAdapterListContainer.xaml
    /// </summary>
    public partial class _NetworkAdapterListContainer : UserControl
    {
        public _NetworkAdapterListContainer()
        {
            InitializeComponent();
            LBAdapterList.DataContextChanged += LBAdapterList_DataContextChanged;
            LBAdapterList.SelectionChanged += LBAdapterList_SelectionChanged;
        }

        private void LBAdapterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBAdapterList.SelectedIndex != -1)
                _selectedItemIndex = LBAdapterList.SelectedIndex;
        }

        private int _selectedItemIndex = 0;

        private void LBAdapterList_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((MainWindowViewModel)Application.Current.MainWindow.DataContext).AdapterList.CollectionChanged += AdapterList_CollectionChanged;
        }

        private void AdapterList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ListBoxItem lbi = (ListBoxItem)LBAdapterList.ItemContainerGenerator.ContainerFromIndex(_selectedItemIndex);
            if (lbi != null)
                lbi.IsSelected = true;
        }

    }
}
