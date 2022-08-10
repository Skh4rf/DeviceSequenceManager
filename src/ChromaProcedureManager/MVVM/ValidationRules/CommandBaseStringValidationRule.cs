using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;

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
