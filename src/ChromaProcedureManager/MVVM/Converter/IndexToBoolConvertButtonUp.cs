using System;
using System.Windows.Data;

namespace DeviceSequenceManager.MVVM
{
    internal class IndexToBoolConvertButtonUp : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            int i = (int)value;
            if (i > 1) { return true; }
            return false;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
