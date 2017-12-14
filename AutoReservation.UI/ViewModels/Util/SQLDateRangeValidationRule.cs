using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutoReservation.UI.ViewModels.Util
{
    public class SQLDateRangeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime date;
            try
            {
                date = DateTime.Parse(value.ToString());
            }
            catch (FormatException)
            {
                return new ValidationResult(false, (string)Application.Current.TryFindResource("validation_date_not_valid"));
            }
            if (date < (DateTime)SqlDateTime.MinValue)
            {
                string msg = ((string)Application.Current.TryFindResource("validation_date_too_old")).Replace("{minDate}", ((DateTime)SqlDateTime.MinValue).ToShortDateString());
                return new ValidationResult(false, msg);
            } else if(date > (DateTime)SqlDateTime.MaxValue)
            {
                string msg = ((string)Application.Current.TryFindResource("validation_date_too_far")).Replace("{maxDate}", ((DateTime)SqlDateTime.MaxValue).ToShortDateString());
                return new ValidationResult(false,msg);
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
