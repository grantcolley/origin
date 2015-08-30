using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevelopmentInProgress.DipState;

namespace DevelopmentInProgress.ExampleModule.Converters
{
    public class StatusToVisibilityHiddenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value != null
                && ((StateStatus) value).Equals(StateStatus.Uninitialised))
            {
                return Visibility.Hidden;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
