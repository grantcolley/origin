using System;
using System.Globalization;
using System.Windows.Data;

namespace DevelopmentInProgress.ExampleModule.Converters
{
    public class EmptyStringToNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null
                && value.ToString().Equals(String.Empty))
            {
                return null;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null
                && value.ToString().Equals(String.Empty))
            {
                return null;
            }

            return value;
        }
    }
}
