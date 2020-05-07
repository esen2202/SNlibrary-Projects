using SN.NetSet.UI.WPF.Properties;
using SN.NetSet.UI.WPF.ViewModels;
using SN.Windows.UI;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace SN.NetSet.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool PinnedSideBar { get; set; }

        public bool StatusTopMost { get; set; }

        private void GetSettings()
        {
            PinnedSideBar = AppSettings.Default.PinnedSideBar;
            StatusTopMost = AppSettings.Default.StatusTopMost;
        }

        private void SetSettings()
        {
            AppSettings.Default.PinnedSideBar = PinnedSideBar;
            AppSettings.Default.StatusTopMost = StatusTopMost;

            AppSettings.Default.Save();
        }

        public bool GetStatusTopMost() => this.StatusTopMost;

        public void SetTopMost(bool status, bool set = false)
        {
            this.Topmost = status;
            if (set)
            {
                StatusTopMost = status;
            }
        }

        public bool GetStatusPinSideBar() => PinnedSideBar;

        public void SetPinSideBar(bool status) => PinnedSideBar = status;

        private void InitializeViewStyles()
        {
            this.Left = SystemParameters.WorkArea.Left;
            this.Top = SystemParameters.WorkArea.Top;
            this.Width = SystemParameters.WorkArea.Width;
            this.Height = SystemParameters.WorkArea.Height;
            this.SetTopMost(StatusTopMost);
        }

        public MainWindow()
        {
            GetSettings();
            InitializeComponent();
            InitializeViewStyles();

            this.DataContext = new MainWindowViewModel();

            this.Closing += MainWindow_Closing;
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           WindowsApiHelper.WpfAltTabMenuBlocker(this);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SetSettings();
        }
    }
}
