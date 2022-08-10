using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace DeviceSequenceManager.MVVM
{
    internal class DeviceTypeNameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || value.ToString().Length < 3) { return new ValidationResult(false, "Invalid. Minimum 3 characters"); }

            string text = value.ToString();

            if (text.Length > 28) { return new ValidationResult(false, "Invalid. Maximum 27 characters"); }

            if (DataContainer.DeviceTypes != null && DataContainer.DeviceTypes.Any(x => x.Name.Equals(text))) { return new ValidationResult(false, "Invalid. Name already exists"); }

            return new ValidationResult(true, "");
        }
    }
}
