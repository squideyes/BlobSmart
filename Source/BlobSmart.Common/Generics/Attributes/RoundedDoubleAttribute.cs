using System;
using System.ComponentModel.DataAnnotations;

namespace BlobSmart.Common.Generics
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class RoundedDoubleAttribute : ValidationAttribute
    {
        private int digits;

        public RoundedDoubleAttribute(int digits)
        {
            Contract.Requires((digits >= 0) && (digits <= 15), nameof(digits));

            this.digits = digits;
        }

        protected override ValidationResult IsValid(object value, ValidationContext vc)
        {
            var number = ((double)value);

            if (number != Math.Round(number, digits))
            {
                return new ValidationResult(string.Format(
                    "The {0} field must be set to double value with {1} digits of precision.", 
                    vc.MemberName, digits));
            }

            return ValidationResult.Success;
        }
    }
}
