﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace SN.NetSet.UI.WPF.Converters
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value != null ? (string)value : "";
            return String.IsNullOrEmpty(str) ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
