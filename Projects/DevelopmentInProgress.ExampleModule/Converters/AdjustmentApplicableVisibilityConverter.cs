using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DevelopmentInProgress.ExampleModule.Converters
{
    public class AdjustmentApplicableVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value is bool
                && value.Equals(true))
            {
                return Visibility.Visible;
            }
            
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
