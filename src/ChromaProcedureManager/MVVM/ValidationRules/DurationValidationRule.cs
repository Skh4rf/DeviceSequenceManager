using System;
using System.Globalization;
using System.Windows.Controls;

namespace DeviceSequenceManager.MVVM
{
    internal class DurationValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try { Convert.ToInt32(value); }
            catch (Exception) { return new ValidationResult(false, "Input has to be a number"); }

            string str = value.ToString();

            if (str.Contains(",") || str.Contains(".")) { return new ValidationResult(false, "No decimal places allowed"); }

            if (Convert.ToInt32(value) < 1) { return new ValidationResult(false, "Number has to be bigger than 0"); }

            return new ValidationResult(true, "");
        }
    }
}
