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
        public static Snackbar Snackbar;

        private Storyboard openConfigStoryboard;

        private Storyboard closeConfigStoryboard;

        public _ConfigPanel()
        {
            InitializeComponent();
            this.DataContext = new _ConfigPanelViewModel();
            (this.DataContext as _ConfigPanelViewModel).ReturnedResultMessage += _ConfigPanel_ReturnedResultMessage;
            this.Loaded += _ConfigPanel_Loaded;

            Snackbar = this.MainSnackbar;
            BorderConfiguration.Width = 0;

            openConfigStoryboard = new Storyboard();
            closeConfigStoryboard = new Storyboard();

            openConfigStoryboard.Completed += OpenConfigStoryboard_Completed; ;
            closeConfigStoryboard.Completed += CloseConfigStoryboard_Completed; ;
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
            BorderWidthAnimation(BorderConfiguration, openConfigStoryboard, BorderConfiguration.Width, 320);
        }

        public void CloseConfig()
        {
            BorderWidthAnimation(BorderConfiguration, closeConfigStoryboard, BorderConfiguration.Width, 0);
        }

        private void BtnHideConfiguration_Click(object sender, RoutedEventArgs e)
        {
            CloseConfig();
        }
    }
}
