using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobSmart.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class SundayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext vc)
        {
            var date = ((DateTime)value);

            if ((date.DayOfWeek != DayOfWeek.Sunday) || (date != date.Date))
            {
                return new ValidationResult(string.Format(
                    "The {0} field must be set to a Sunday value.", vc.MemberName));
            }

            return ValidationResult.Success;
        }
    }
}
