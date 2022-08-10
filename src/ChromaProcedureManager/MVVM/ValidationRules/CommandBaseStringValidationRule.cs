using System.Globalization;
using System.Windows.Controls;

namespace DeviceSequenceManager.MVVM
{
    internal class CommandBaseStringValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || value.ToString().Length < 1) { return new ValidationResult(false, "Field is required"); }

            return new ValidationResult(true, "");
        }
    }
}
