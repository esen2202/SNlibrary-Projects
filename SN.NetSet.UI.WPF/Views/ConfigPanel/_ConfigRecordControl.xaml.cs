using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace SN.NetSet.UI.WPF.Views.ConfigPanel
{
    /// <summary>
    /// Interaction logic for _ConfigRecordControl.xaml
    /// </summary>
    public partial class _ConfigRecordControl : UserControl
    {
        public _ConfigRecordControl()
        {
            InitializeComponent();

            TxtIpAddress.KeyDown += delegate (object sender, KeyEventArgs e) { IpFormatDelegate(sender, e); };
            TxtSubnetMask.KeyDown += delegate (object sender, KeyEventArgs e) { IpFormatDelegate(sender, e); };
            TxtGateway.KeyDown += delegate (object sender, KeyEventArgs e) { IpFormatDelegate(sender, e); };
            TxtDnsServer1.KeyDown += delegate (object sender, KeyEventArgs e) { IpFormatDelegate(sender, e); };
            TxtDnsServer2.KeyDown += delegate (object sender, KeyEventArgs e) { IpFormatDelegate(sender, e); };
        }

        private void TxtIpAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var tb = (sender as TextBox);
            //var pointNumber = tb.Text.Count(o => o == '.');

            //var strArr = tb.Text.Split('.');
            //var strArrLast = strArr.Last();

            //var newStr = "";
            //foreach (var item in strArr)
            //{

            //    newStr += !string.IsNullOrEmpty(item) ?
            //        !string.IsNullOrEmpty(newStr) ? "." + item : item :
            //        !string.IsNullOrEmpty(newStr) ? "." : "";
            //}

            //foreach (var item in strArr)
            //{
            //    if (item.Length > 3)
            //        tb.Text = oldText;
            //}

            //oldText = tb.Text;
        }

        private static void IpFormatDelegate(object sender, KeyEventArgs e)
        {
            var tb = (sender as TextBox);
            var pointNumber = tb.Text.Count(o => o == '.');

            if (pointNumber >= 3
            && !Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key))
            && e.Key != System.Windows.Input.Key.Back
            && e.Key != System.Windows.Input.Key.Delete
            && e.Key != Key.Left && e.Key != Key.Right)
            {
                e.Handled = true;
                return;
            }

            var strArr = tb.Text.Split('.');
            var strArrLast = strArr.Last();

            if ((strArrLast.Length >= 3 || e.Key == System.Windows.Input.Key.Right && !tb.Text.EndsWith("."))
        && strArr.Count() < 4 && e.Key != System.Windows.Input.Key.Back)
            {
                tb.Text += ".";
                tb.CaretIndex = tb.Text.Length;
                e.Handled = true;
                return;
            }

            pointNumber = tb.Text.Count(o => o == '.');
        }
    }
}
