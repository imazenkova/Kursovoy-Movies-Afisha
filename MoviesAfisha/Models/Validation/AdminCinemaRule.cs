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
    public class AdminCinemaRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            cultureInfo = CultureInfo.CurrentCulture;
            if (!Regex.Match(charString, "^[A-ZА-Я][a-zа-я]+( )[A-ZА-Я]+[a-zа-я]*$").Success)
            {
                if (!Regex.Match(charString, "^[A-ZА-Я][a-zа-я]*$").Success) 
                    return new ValidationResult(false, $"Только буквы, учитывая заглавные"); 
                else
                    return ValidationResult.ValidResult;
            }
            return ValidationResult.ValidResult;
        }
    }
}
