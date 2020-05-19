using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SN.WPFTest.UC
{
    /// <summary>
    /// Interaction logic for IpTextBox.xaml
    /// </summary>
    public partial class IpTextBox : UserControl, INotifyPropertyChanged
    {


        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }

        public static readonly DependencyProperty AddressProperty =
            DependencyProperty.Register("Address", typeof(string), typeof(IpTextBox), new PropertyMetadata(" ", new PropertyChangedCallback(OnFirstPropertyChanged)));

        private static void OnFirstPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var split = e.NewValue.ToString().Split('.');

            var _this = d as IpTextBox;

            _this.AdrSegment1 = split.Count() > 0 ? "" : split[0];
            _this.AdrSegment2 = split.Count() > 1 ? "" : split[1];
            _this.AdrSegment3 = split.Count() > 2 ? "" : split[2];
            _this.AdrSegment4 = split.Count() > 3 ? "" : split[3];

        }

        private string AdrSegment1Property;
        public string AdrSegment1
        {
            get { return AdrSegment1Property; }
            set { AdrSegment1Property = value; OnPropertyChanged("AdrSegment1"); }
        }


        public string AdrSegment2
        {
            get { return (string)GetValue(AdrSegment2Property); }
            set { SetValue(AdrSegment2Property, value); OnPropertyChanged("AdrSegment2"); }
        }

        public static readonly DependencyProperty AdrSegment2Property =
            DependencyProperty.Register("AdrSegment2", typeof(string), typeof(IpTextBox), new PropertyMetadata(""));

        public string AdrSegment3
        {
            get { return (string)GetValue(AdrSegment3Property); }
            set { SetValue(AdrSegment3Property, value); OnPropertyChanged("AdrSegment3"); }
        }

        public static readonly DependencyProperty AdrSegment3Property =
            DependencyProperty.Register("AdrSegment3", typeof(string), typeof(IpTextBox), new PropertyMetadata(""));

        public string AdrSegment4
        {
            get { return (string)GetValue(AdrSegment4Property); }
            set { SetValue(AdrSegment4Property, value); OnPropertyChanged("AdrSegment4"); }
        }

        public static readonly DependencyProperty AdrSegment4Property =
            DependencyProperty.Register("AdrSegment4", typeof(string), typeof(IpTextBox), new PropertyMetadata(""));

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IpTextBox()
        {
            InitializeComponent();
        }
    }
}
