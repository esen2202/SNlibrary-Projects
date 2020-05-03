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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SN.NetSet.UI.WPF.Views.MainPanel
{
    /// <summary>
    /// Interaction logic for _MainPanel.xaml
    /// </summary>
    public partial class _MainPanel : UserControl
    {
        private Storyboard openSideBarStoryboard;
        private Storyboard closeSideBarStoryboard;

        private bool pinnedSideBar;

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

            openSideBarStoryboard = new Storyboard();
            closeSideBarStoryboard = new Storyboard();

            openSideBarStoryboard.Completed += OpenStoryboard_Completed;
            closeSideBarStoryboard.Completed += CloseStoryboard_Completed;
        }

        private void CloseStoryboard_Completed(object sender, EventArgs e)
        {
            BorderFloating.Visibility = Visibility.Visible;
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

        private void BorderPanel_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (BorderPanel.Width >= 320)
                BorderWidthAnimation(BorderPanel, closeSideBarStoryboard, BorderPanel.Width, 0);
        }
        private void BorderFloating_OnMouseEnter(object sender, MouseEventArgs e)
        {
            closeSideBarStoryboard.Stop();
            BorderFloating.Visibility = Visibility.Collapsed;
            BorderWidthAnimation(BorderPanel, openSideBarStoryboard, BorderPanel.Width, 320);
        }
    }
}
