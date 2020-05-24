using System;
using System.Diagnostics;
using System.Windows;

namespace SN.NetSet.UI.WPF.Views.About
{
    /// <summary>
    /// Interaction logic for AboutMe.xaml
    /// </summary>
    public partial class AboutMe : Window
    {
        public static AboutMe Current { get; private set; } 

        public AboutMe()
        {
            InitializeComponent();

            this.Loaded += AboutMe_Loaded;
            this.Closed += AboutMe_Closed;
        }

        private void AboutMe_Loaded(object sender, RoutedEventArgs e)
        {
            if (Current != null)
            {
                this.Close();  

                if (!Current.IsActive)
                    Current.Activate();

                if (!Current.IsFocused)
                    Current.Focus();
            }
            else
            {
                Current = this;  
            }
        }
        private void AboutMe_Closed(object sender, EventArgs e)
        {
            if ((Current == this) && (Current != null))
                Current = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HLinkLinkedIn_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
