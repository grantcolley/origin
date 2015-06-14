using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevelopmentInProgress.DipState;

namespace DevelopmentInProgress.RemediationProgramme.Converters
{
    public class InvertedDipStateStatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value != null
                && ((DipStateStatus)value).Equals(DipStateStatus.Completed))
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
