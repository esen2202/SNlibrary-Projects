using System;
using System.Globalization;
using System.Windows.Data;

namespace SN.NetSet.UI.WPF.Converters
{
    public class BitToMBConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //System.Convert.ToDouble(value);
            //double dvalue = double.Parse(value.ToString(), System.Globalization.NumberStyles.AllowExponent | System.Globalization.NumberStyles.AllowDecimalPoint);
            return (int)(System.Convert.ToDouble(value) * 0.000001);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
