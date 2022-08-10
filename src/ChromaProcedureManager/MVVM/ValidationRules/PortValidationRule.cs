using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;

namespace DeviceSequenceManager.MVVM
{
    internal class PortValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || value.ToString().Equals(String.Empty)) { return new ValidationResult(false, "Invalid IP-Address."); }

            string text = value.ToString();

            if(text.Length > 5) { return new ValidationResult(false, "Invalid Port"); }

            foreach (char c in text.ToCharArray())
            {
                if (!Char.IsDigit(c)) { return new ValidationResult(false, "Invalid Port"); }
            }

            if (Convert.ToInt32(text) > 65535) { return new ValidationResult(false, "Invalid Port"); }

            return new ValidationResult(true, "");
        }
    }
}
