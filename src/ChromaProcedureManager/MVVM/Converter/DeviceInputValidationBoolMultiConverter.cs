using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace DeviceSequenceManager.MVVM
{
    internal class DeviceInputValidationBoolMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            List<bool> bools = new List<bool>();

            foreach (object o in value)
            {
                bools.Add((bool)o);
            }

            return bools.All(b => b == false);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
