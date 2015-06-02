using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DevelopmentInProgress.RemediationProgramme.Model;

namespace DevelopmentInProgress.RemediationProgramme.Converters
{
    /// <summary>
    /// Converts a boolean to a <see cref="Visibility"/> value.
    /// </summary>
    public class NullToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts the value to the converted type.
        /// </summary>
        /// <param name="value">The value to evaluate.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture information.</param>
        /// <returns>Visibility.Visible if true, else returns Visibility.Collapsed.</returns>
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value is Customer)
            {
                return Visibility.Visible; 
            }

            return Visibility.Collapsed;
        }

        /// <summary>
        /// Converts the value back to the converted type.
        /// </summary>
        /// <param name="value">The value to evaluate.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture information.</param>
        /// <returns>A converted type.</returns>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
