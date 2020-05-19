using MaterialDesignThemes.Wpf;
using SN.Cmd;
using SN.NetSet.UI.WPF.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SN.NetSet.UI.WPF.Views.ConfigPanel
{
    /// <summary>
    /// Interaction logic for _SettingsPanel.xaml
    /// </summary>
    public partial class _ConfigPanel : UserControl
    {
        public static _ConfigPanel ConfigPanelInstance { get; private set; }

        public static Snackbar Snackbar;

        private Storyboard _openConfigStoryboard;

        private Storyboard _closeConfigStoryboard;

        public _ConfigPanel()
        {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;

            this.DataContext = new _ConfigPanelViewModel();
            ConfigPanelInstance = this;

            (this.DataContext as _ConfigPanelViewModel).ReturnedResultMessage += _ConfigPanel_ReturnedResultMessage;
            this.Loaded += _ConfigPanel_Loaded;

            Snackbar = this.MainSnackbar;
            BorderMain.Width = 0;

            _openConfigStoryboard = new Storyboard();
            _closeConfigStoryboard = new Storyboard();

            _openConfigStoryboard.Completed += OpenConfigStoryboard_Completed; ;
            _closeConfigStoryboard.Completed += CloseConfigStoryboard_Completed; ;
        }

        private void _ConfigPanel_ReturnedResultMessage(object sender, EventArgsWithStrMessage e)
        {
            this.Dispatcher.Invoke(() =>
            {
                Snackbar.MessageQueue.Enqueue(e.Message);
            });
        }

        private void _ConfigPanel_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void OpenConfigStoryboard_Completed(object sender, EventArgs e)
        {

        }

        private void CloseConfigStoryboard_Completed(object sender, EventArgs e)
        {
            (Application.Current.MainWindow as MainWindow).MainPanel.PreventClose = false;
            this.Visibility = Visibility.Collapsed;
        }

        private void BorderWidthAnimation(Border border, Storyboard storyboard, Double widthStart, Double widthFinish)
        {
            DoubleAnimation animation = new DoubleAnimation(widthStart, widthFinish, TimeSpan.FromMilliseconds(500));
            animation.EasingFunction = new PowerEase();
            storyboard.Children.Add(animation);

            Storyboard.SetTarget(animation, border);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Width)"));

            storyboard.Begin();
        }

        public void OpenConfig()
        {
            BorderWidthAnimation(BorderMain, _openConfigStoryboard, BorderMain.Width, 320);
            this.Visibility = Visibility.Visible;
        }

        public void CloseConfig()
        {
            BorderWidthAnimation(BorderMain, _closeConfigStoryboard, BorderMain.Width, 0);
        }

        private void BtnHideConfiguration_Click(object sender, RoutedEventArgs e)
        {
            CloseConfig();
        }
    }
}
