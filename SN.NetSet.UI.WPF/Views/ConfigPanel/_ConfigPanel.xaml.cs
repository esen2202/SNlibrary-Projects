using SN.NetSet.UI.WPF.ViewModels;
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

namespace SN.NetSet.UI.WPF.Views.ConfigPanel
{
    /// <summary>
    /// Interaction logic for _SettingsPanel.xaml
    /// </summary>
    public partial class _ConfigPanel : UserControl
    {
        private Storyboard openConfigStoryboard;

        private Storyboard closeConfigStoryboard;

        public _ConfigPanel()
        {
            InitializeComponent();
            this.DataContext = new _ConfigPanelViewModel();
            this.Loaded += _ConfigPanel_Loaded;

            BorderConfiguration.Width = 0;

            openConfigStoryboard = new Storyboard();
            closeConfigStoryboard = new Storyboard();

            openConfigStoryboard.Completed += OpenConfigStoryboard_Completed; ;
            closeConfigStoryboard.Completed += CloseConfigStoryboard_Completed; ;
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
