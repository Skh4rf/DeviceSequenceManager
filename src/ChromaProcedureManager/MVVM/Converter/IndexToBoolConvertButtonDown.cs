using System;
using System.Linq;
using System.Windows.Data;

namespace DeviceSequenceManager.MVVM
{
    internal class IndexToBoolConvertButtonDown : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            SequenceOperation operation = (SequenceOperation)value;

            if (DataContainer.Sequence.Operations.Last() == operation) { return false; }
            return true;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
