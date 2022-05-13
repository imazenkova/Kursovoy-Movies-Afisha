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
    public class LoginValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            cultureInfo = CultureInfo.CurrentCulture;
            if (char.IsDigit(charString.FirstOrDefault()))
            {
                return new ValidationResult(false, $"\n\nИмя не может начинаться с цифры");
            }
            if (!Regex.Match(charString, "^[a-zA-Z][a-zA-Z._\\d]*$").Success)
            {
                return new ValidationResult(false, $"\n\nТолько латинские буквы, цифры и . _"); //Поле может содержать только латинские буквы, цифры и . _
            }
            return ValidationResult.ValidResult;
        }
    }
}
