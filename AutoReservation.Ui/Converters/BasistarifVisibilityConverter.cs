using AutoReservation.Common.DataTransferObjects;
using System;
using System.Windows;
using System.Windows.Data;

namespace AutoReservation.Ui.Converters
{
    public class BasistarifVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((AutoKlasse)value == AutoKlasse.Luxusklasse)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
