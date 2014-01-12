using System;
using System.Windows;
using System.Windows.Data;

namespace Filmator.Converters {
    class DoubleToRateConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            double num;
            var strvalue = value.ToString();
            if (!double.TryParse(strvalue, out num))
                return 0;
            if (num > 10) {
                return 10;
            }
            return num < 0 ? 0 : num;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value.ToString();
        }
    }
}
