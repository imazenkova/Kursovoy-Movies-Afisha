using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MoviesAfisha.Models.Validation
{
    public class PriceRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            if (!Regex.Match(charString, @"^[0-9]\d*$").Success)
                return new ValidationResult(false, $"Только положительные числа");

            return ValidationResult.ValidResult;
        }
    }
}
