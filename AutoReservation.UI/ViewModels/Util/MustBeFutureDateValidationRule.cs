using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutoReservation.UI.ViewModels.Util
{
    public class MustBeFutureDateValidationRule : ValidationRule
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
            if (date < DateTime.Now)
            {
                string msg = ((string)Application.Current.TryFindResource("validation_date_too_far")).Replace("{maxDate}", (DateTime.Now).ToShortDateString());
                return new ValidationResult(false, msg);
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
