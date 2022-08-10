using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DeviceSequenceManager.MVVM
{
    internal class CommandRangeMaximumValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || value.Equals(String.Empty))
            {
                return new ValidationResult(false, "You have to enter a number");
            }

            foreach (char c in value.ToString().ToCharArray())
            {
                if (!(c.Equals('-') || c.Equals('.') || c.Equals(',') || Char.IsDigit(c)))
                {
                    return new ValidationResult(false, "No letters allowed");
                }
            }

            return new ValidationResult(true, "");
        }
    }
}
