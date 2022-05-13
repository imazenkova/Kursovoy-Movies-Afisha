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
    public class NameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            if (!Regex.Match(charString, "^[^)~`!@#$%^&*()-+=?:;№\"\'][^)~`!@#$%^&*()-+=?:;№\"\']+$").Success)
                return new ValidationResult(false, $"\n\nНекорректные данные");

            return ValidationResult.ValidResult;
        }
    }
}
