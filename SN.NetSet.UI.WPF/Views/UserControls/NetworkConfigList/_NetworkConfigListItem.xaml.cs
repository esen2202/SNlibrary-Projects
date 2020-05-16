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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SN.NetSet.UI.WPF.Views.UserControls.NetworkConfigList
{
    /// <summary>
    /// Interaction logic for _NetworkConfigListItem.xaml
    /// </summary>
    public partial class _NetworkConfigListItem : UserControl
    {


        public Visibility DeleteQuestion
        {
            get { return (Visibility)GetValue(DeleteQuestionProperty); }
            set { SetValue(DeleteQuestionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeleteQuestion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DeleteQuestionProperty =
            DependencyProperty.Register("DeleteQuestion", typeof(Visibility), typeof(_NetworkConfigListItem ), new PropertyMetadata(Visibility.Collapsed));

        public _NetworkConfigListItem()
        {
            InitializeComponent();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteQuestion = Visibility.Visible;
        }

        private void BtnCancelDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteQuestion = Visibility.Collapsed;
        }

        public void DeleteQuestionReset()
        {
            DeleteQuestion = Visibility.Collapsed;
        }
    }
}
