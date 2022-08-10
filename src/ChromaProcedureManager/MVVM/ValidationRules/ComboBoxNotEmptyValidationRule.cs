using System.Globalization;
using System.Windows.Controls;

namespace DeviceSequenceManager.MVVM
{
    internal class ComboBoxNotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) { return new ValidationResult(false, "Field is required"); }

            return new ValidationResult(true, "");
        }
    }
}
