using MaterialDesignThemes.Wpf;
using SN.NetSet.UI.WPF.Properties;
using SN.NetSet.UI.WPF.ViewModels;
using SN.Windows.UI;
using System;
using System.Runtime.CompilerServices;
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
        private static bool PinnedSideBar
        {
            get { return AppSettings.Default.PinnedSideBar; }
            set { AppSettings.Default.PinnedSideBar = value; AppSettings.Default.Save(); }
        }

        private static bool StatusTopMost
        {
            get { return AppSettings.Default.StatusTopMost; }
            set { AppSettings.Default.StatusTopMost = value; AppSettings.Default.Save(); }
        }

        public static string LastAdapterName
        {
            get { return AppSettings.Default.LastAdapterName; }
            set { AppSettings.Default.LastAdapterName = value; AppSettings.Default.Save(); }
        }

        public static int LastConfigId
        {
            get { return AppSettings.Default.LastConfigId; }
            set { AppSettings.Default.LastConfigId = value; AppSettings.Default.Save(); }
        }

        public static int HolderMarginTop
        {
            get { return AppSettings.Default.HolderTopPosition; }
            set { AppSettings.Default.HolderTopPosition = value; AppSettings.Default.Save(); }
        }

        public static bool GetStatusTopMost() => StatusTopMost;

        public void SetTopMost(bool status, bool set = false)
        {
            this.Topmost = status;
            if (set)
            {
                StatusTopMost = status;
            }
        }

        public static bool GetStatusPinSideBar() => PinnedSideBar;

        public void SetPinSideBar(bool status) => PinnedSideBar = status;

        private void InitializeViewStyles()
        {
            this.Left = SystemParameters.WorkArea.Left;
            this.Top = SystemParameters.WorkArea.Top;
            this.Width = SystemParameters.WorkArea.Width;
            this.Height = SystemParameters.WorkArea.Height;
            this.MaxHeight = SystemParameters.WorkArea.Height;
            this.SetTopMost(StatusTopMost);
        }

        public MainWindow()
        {
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
        }

    }
}
