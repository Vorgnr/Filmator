using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Filmator.Converters {
    class DoubleToColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var num = value as double?;
            var bc = new BrushConverter();
            if (num == null || num < 4) return bc.ConvertFrom("#c0392b");
            return bc.ConvertFrom(num < 7 ? "#f39c12" : "#27ae60");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
