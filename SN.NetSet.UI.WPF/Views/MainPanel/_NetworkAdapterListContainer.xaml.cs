using SN.NetSet.UI.WPF.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SN.NetSet.UI.WPF.Views.MainPanel
{
    /// <summary>
    /// Interaction logic for _NetworkAdapterListContainer.xaml
    /// </summary>
    public partial class _NetworkAdapterListContainer : UserControl
    {
        private int _selectedItemIndex = 0;

        public _NetworkAdapterListContainer()
        {
            InitializeComponent();

            LBAdapterList.SelectionChanged += LBAdapterList_SelectionChanged;
            this.Loaded += _NetworkAdapterListContainer_Loaded;
        }
  
        private void _NetworkAdapterListContainer_Loaded(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow.DataContext as MainWindowViewModel).NetAdaptersUpdated += _NetworkAdapterListContainer_NetAdaptersUpdated;
        }

        private void _NetworkAdapterListContainer_NetAdaptersUpdated(object sender, ReceivedDataEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                //ListBoxItem lbi = (ListBoxItem)LBAdapterList.ItemContainerGenerator.ContainerFromIndex(2);
                //if (lbi != null)
                //    lbi.IsSelected = true;
     
            });
        }

        private void LBAdapterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBAdapterList.SelectedIndex != -1)
                _selectedItemIndex = LBAdapterList.SelectedIndex;
        }


    }
}
