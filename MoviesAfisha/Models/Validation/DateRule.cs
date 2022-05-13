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
    public class DateRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;
            if (Regex.Match(charString, "[0-9]{2}/[0-9]{2}/[0-9]{4}").Success)
            {
                try
                {
                    DateTime time = DateTime.Parse(charString);
                    if (time.Date < DateTime.Now.Date.AddDays(6))
                    {
                        return new ValidationResult(false, $"\n\nВведите корректную дату.\nСеанс возможен через 7 дней от текущей даты");
                    }
                    return ValidationResult.ValidResult;
                }
                catch (Exception e)
                {
                    return new ValidationResult(false, $"Введите корректную дату");
                }
            }
            else
            {
                return new ValidationResult(false, $"dd/mm/yyyy");
            }


        }
    }
}
