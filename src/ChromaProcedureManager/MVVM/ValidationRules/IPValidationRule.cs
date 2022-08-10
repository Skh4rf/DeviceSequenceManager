using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DeviceSequenceManager.MVVM
{
    internal class IPValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value==null || value.ToString().Equals(String.Empty)) { return new ValidationResult(false, "Invalid IP-Address"); }

            string text = value.ToString();
            
            if (text.Length > 15) { return new ValidationResult(false, "Invalid IP-Address"); }

            string[] textarray = text.Split(new char[] {'.'}, StringSplitOptions.RemoveEmptyEntries);


            if (textarray.Length == 4)
            {
                foreach (string str in textarray)
                {
                    if (str.Length > 3) { return new ValidationResult(false, "Invalid IP-Address"); }

                    foreach (char c in str.ToCharArray())
                    {
                        if (!Char.IsDigit(c)) { return new ValidationResult(false, "Invalid IP-Address"); }
                    }
                }
            }
            else
            {
                return new ValidationResult(false, "Invalid IP-Address");
            }

            return new ValidationResult(true, "");
        }
    }
}
