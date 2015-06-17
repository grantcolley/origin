using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace DevelopmentInProgress.RemediationProgramme.Rules
{
    public class NumericValueRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
           var regex = new Regex("[^0-9.]+");
            if (value == null
                || value.ToString().Equals(String.Empty)
                || !regex.IsMatch(value.ToString()))
            {
                return new ValidationResult(true, null);
            }

            return new ValidationResult(false, "A numeric value is required.");
        }
    }
}
