using System;
using System.ComponentModel.DataAnnotations;

namespace BlobSmart.Common.Generics
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext vc)
        {
            var date = ((DateTime)value);

            if (date.TimeOfDay.Ticks != 0)
            {
                return new ValidationResult(string.Format(
                    "The {0} field must be set to a Date value with no TimeOfDay component.", 
                    vc.MemberName));
            }

            return ValidationResult.Success;
        }
    }
}
