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
    public class TimeRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            if (Regex.Match(charString, "[0-9]{2}:[0-9]{2}").Success)
            {
                try
                {
                    DateTime time = DateTime.Parse(charString);
                    return ValidationResult.ValidResult;
                }
                catch (Exception e)
                {
                    return new ValidationResult(false, $"Введите корректное время");
                }
            }
            else
            {
                return new ValidationResult(false, $"hh:mm");
            }
        }
    }
}
