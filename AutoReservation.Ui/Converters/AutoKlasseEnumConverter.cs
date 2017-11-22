using AutoReservation.Common.DataTransferObjects;
using System;
using System.Windows.Data;

namespace AutoReservation.Ui.Converters
{
    public class AutoKlasseEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (AutoKlasse)value;
        }
    }
}
