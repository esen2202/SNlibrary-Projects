using SN.NetSet.UI.WPF.ViewModels;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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

        public int HolderMarginTop
        {
            get { return (int)GetValue(HolderMarginTopProperty); }
            set { SetValue(HolderMarginTopProperty, value); }
        }

        public static readonly DependencyProperty HolderMarginTopProperty =
            DependencyProperty.Register("HolderMarginTop", typeof(int), typeof(_MainPanel), new PropertyMetadata(150));
        public int HolderMoveMargin
        {
            get { return (int)GetValue(HolderMoveMarginProperty); }
            set
            {
                SetValue(HolderMoveMarginProperty, value);
                SetValue(HolderMarginTopProperty, value - 100);
                MainWindow.HolderMarginTop = value;
            }
        }

        public static readonly DependencyProperty HolderMoveMarginProperty =
            DependencyProperty.Register("HolderMoveMargin", typeof(int), typeof(_MainPanel), new PropertyMetadata(250));


        public _MainPanel()
        {
            InitializeComponent();
            HolderMoveMargin = MainWindow.HolderMarginTop;


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
            this.ControlButtonSet.HideExitDailog();
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


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        private void BorderFloatingMove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BorderFloatingMove.Background = Brushes.White;
            if (BorderFloatingMove.Width != 20)
            {
                BorderFloatingMove.Width = 20; BorderFloatingMove.Height = 40;
                BorderFloatingMove.Background = Brushes.Red;
            }
            else
            {
                BorderFloatingMove.Width = 4; BorderFloatingMove.Height = 15;
                BorderFloatingMove.Background = Brushes.DarkSlateBlue;
            }

        }

        private void BorderFloatingMove_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = GetMousePosition();
            if (BorderFloatingMove.Width == 20 &&
                pos.Y - 110 > 0 &&
                pos.Y + 40 < SystemParameters.WorkArea.Height)
            {

                HolderMoveMargin = (int)pos.Y - 20;
            }
        }
    }
}
