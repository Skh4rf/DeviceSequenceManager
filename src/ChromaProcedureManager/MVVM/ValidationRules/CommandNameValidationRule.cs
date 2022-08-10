using System.Globalization;
using System.Windows.Controls;

namespace DeviceSequenceManager.MVVM
{
    internal class CommandNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || value.ToString().Length < 2) { return new ValidationResult(false, "Invalid. Minimum 2 characters."); }

            string text = value.ToString();

            if (text.Length > 20) { return new ValidationResult(false, "Invalid. Maximum 20 characters."); }

            return new ValidationResult(true, "");
        }
    }
}
