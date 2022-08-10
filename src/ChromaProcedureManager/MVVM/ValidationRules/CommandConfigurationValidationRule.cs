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
    internal class CommandConfigurationValidationRule : ValidationRule
    {
        public int DecimalPlaces { get; set; }
        public double RangeMinimum { get; set; }
        public double RangeMaximum { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || value.ToString().Equals(String.Empty)) { return new ValidationResult(true, ""); }
            
            try
            {
                double input = Convert.ToDouble(value);
                if (!(RangeMinimum == 0 && RangeMaximum == 0))
                {
                    if (input > RangeMaximum || input < RangeMinimum)
                    {
                        return new ValidationResult(false, $"Input has to be between {RangeMinimum} and {RangeMaximum}");
                    }
                }

                string str = value.ToString();

                if (str.Contains(",") || str.Contains("."))
                {
                    string[] strarray = str.Split(new char[] { ',', '.' });

                    if (strarray[1].Length > DecimalPlaces)
                    {
                        return new ValidationResult(false, $"Only {DecimalPlaces} decimal place(s) allowed");
                    }
                }
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Input is not a valid number");
            }

            return new ValidationResult(true, "");
        }
    }
}
