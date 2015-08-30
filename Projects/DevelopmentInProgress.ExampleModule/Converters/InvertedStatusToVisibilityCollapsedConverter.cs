using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevelopmentInProgress.DipState;

namespace DevelopmentInProgress.ExampleModule.Converters
{
    public class InvertedStatusToVisibilityCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value != null
                && ((StateStatus)value).Equals(StateStatus.Completed))
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
