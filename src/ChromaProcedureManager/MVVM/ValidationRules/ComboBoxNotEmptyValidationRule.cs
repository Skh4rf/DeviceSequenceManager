using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

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
