using SN.NetSet.UI.WPF.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace SN.NetSet.UI.WPF.Views.MainPanel
{
    /// <summary>
    /// Interaction logic for _MainPanel.xaml
    /// </summary>
    public partial class _MainPanel : UserControl
    {
        public bool PreventClose { get; set; }

        private Storyboard openSideBarStoryboard;

        private Storyboard closeSideBarStoryboard;

        MainWindow mainWindow;

        public Visibility ListVisibility
        {
            get { return (Visibility)GetValue(ListVisibilityProperty); }
            set { SetValue(ListVisibilityProperty, value); }
        }

        public static readonly DependencyProperty ListVisibilityProperty =
            DependencyProperty.Register("ListVisibility", typeof(Visibility), typeof(_MainPanel), new PropertyMetadata(Visibility.Visible));

        public _MainPanel()
        {
            InitializeComponent();

            this.Loaded += _MainPanel_Loaded;

            openSideBarStoryboard = new Storyboard();
            closeSideBarStoryboard = new Storyboard();

            openSideBarStoryboard.Completed += OpenStoryboard_Completed;
            closeSideBarStoryboard.Completed += CloseStoryboard_Completed;
        }

        private void _MainPanel_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
        }

        private void CloseStoryboard_Completed(object sender, EventArgs e)
        {
            BorderFloating.Visibility = Visibility.Visible;
            (mainWindow.DataContext as MainWindowViewModel).SuspendInfoService();
            mainWindow.SetTopMost(true, false);
        }

        private void OpenStoryboard_Completed(object sender, EventArgs e)
        {
            BorderFloating.Visibility = Visibility.Collapsed;
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

        private void BorderFloating_OnMouseEnter(object sender, MouseEventArgs e)
        {
            closeSideBarStoryboard.Stop();
            BorderFloating.Visibility = Visibility.Collapsed;
            BorderWidthAnimation(BorderPanel, openSideBarStoryboard, BorderPanel.Width, 320);

            (mainWindow.DataContext as MainWindowViewModel).ContinueInfoService();
            mainWindow.SetTopMost(MainWindow.GetStatusTopMost(), false);
        }

        private void BorderPanel_OnMouseLeave(object sender, MouseEventArgs e)
        {
            LostFocusOperation();
        }

        private void LostFocusOperation()
        {
            if (BorderPanel.Width >= 320 && !MainWindow.GetStatusPinSideBar() && !PreventClose)
                BorderWidthAnimation(BorderPanel, closeSideBarStoryboard, BorderPanel.Width, 0);
        }
    }
}
