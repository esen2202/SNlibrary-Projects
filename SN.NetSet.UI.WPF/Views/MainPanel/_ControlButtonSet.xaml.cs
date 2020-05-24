using SN.NetSet.UI.WPF.Views.About;
using System.Windows;
using System.Windows.Controls;

namespace SN.NetSet.UI.WPF.Views.MainPanel
{
    /// <summary>
    /// Interaction logic for _ControlButtonSet.xaml
    /// </summary>
    public partial class _ControlButtonSet : UserControl
    {
        public Visibility ExitDialog
        {
            get { return (Visibility)GetValue(ExitDialogProperty); }
            set { SetValue(ExitDialogProperty, value); }
        }

        public static readonly DependencyProperty ExitDialogProperty =
            DependencyProperty.Register("ExitDialog", typeof(Visibility), typeof(_ControlButtonSet), new PropertyMetadata(Visibility.Collapsed));

        MainWindow mainWindow;

        public _ControlButtonSet()
        {
            InitializeComponent();
            this.Loaded += _ControlButtonSet_Loaded;
        }

        private void _ControlButtonSet_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            PinnedIconChanger();
            TopMostIconChanger();
        }

        private void BtnAlwaysTop_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.SetPinSideBar(!MainWindow.GetStatusPinSideBar());
            PinnedIconChanger();
        }

        private void BtnShowHideList_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow.MainPanel.ListVisibility == Visibility.Collapsed)
            {
                mainWindow.MainPanel.ListVisibility = Visibility.Visible;
                mainWindow.SizeToContent = SizeToContent.Manual;
                mainWindow.Height = SystemParameters.WorkArea.Height;
            }
            else
            {
                mainWindow.SizeToContent = SizeToContent.Height;
                mainWindow.MainPanel.ListVisibility = Visibility.Collapsed;
            }
            ShowHideIconChanger(mainWindow.MainPanel.ListVisibility == Visibility.Visible);
        }

        private void BtnTopMost_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.SetTopMost(!MainWindow.GetStatusTopMost(), true);
            TopMostIconChanger();
        }

        private void ShowHideIconChanger(bool visible)
        {
            iconShowHide.Kind = visible == false ? MaterialDesignThemes.Wpf.PackIconKind.ArrowExpand : MaterialDesignThemes.Wpf.PackIconKind.ArrowCollapse;
        }

        private void PinnedIconChanger()
        {
            if (MainWindow.GetStatusPinSideBar())
                iconPin.Kind = MaterialDesignThemes.Wpf.PackIconKind.PinOutline;
            else
                iconPin.Kind = MaterialDesignThemes.Wpf.PackIconKind.PinOffOutline;

        }

        private void TopMostIconChanger()
        {
            iconTopMost.Kind = MainWindow.GetStatusTopMost()
                ? MaterialDesignThemes.Wpf.PackIconKind.ArrangeBringToFront
                : MaterialDesignThemes.Wpf.PackIconKind.ArrangeSendToBack;
        }

        private void BtnConfig_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).ConfigPanel.OpenConfig();
            (Application.Current.MainWindow as MainWindow).MainPanel.PreventClose = true;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.SetSettings();
            Application.Current.Shutdown();
        }

        private void BtnCancelClose_Click(object sender, RoutedEventArgs e)
        {
            HideExitDailog();
        }

        private void BtnCloseDialog_Click(object sender, RoutedEventArgs e)
        {
            ToggleExitDailog();
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutMe aboutMe = new AboutMe();
            aboutMe.ShowDialog();
        }

        public void HideExitDailog()
        {
            ExitDialog = Visibility.Collapsed;
        }

        public void ToggleExitDailog()
        {
            ExitDialog = ExitDialog == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
