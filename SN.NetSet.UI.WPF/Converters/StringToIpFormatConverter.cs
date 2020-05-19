using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SN.NetSet.UI.WPF.Converters
{
    public class StringToIpFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strTrim = value != null? value.ToString().Trim('.') : "";
            string str = (value as string);
            if (strTrim.ToString().Length == 3 && !str.EndsWith("."))
                str.Concat(".");
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
           // throw new NotImplementedException();
        }
    }
}
