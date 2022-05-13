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
    class AddressRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            cultureInfo = CultureInfo.CurrentCulture;
            if (!Regex.Match(charString, "^[А-Я][а-я]+( )[0-9]*$").Success)
            {
                if(!Regex.Match(charString, "^[A-ZА-Я][a-zа-я]+( )[A-ZА-Я]+[a-zа-я]+( )[0-9]*$").Success)
                    return new ValidationResult(false, $"\n\n\nНазвание улицы начинается с большой буквы,\nукажите номер через пробел.");
                else
                    return ValidationResult.ValidResult;
            }
            return ValidationResult.ValidResult;
        }
    }
}
