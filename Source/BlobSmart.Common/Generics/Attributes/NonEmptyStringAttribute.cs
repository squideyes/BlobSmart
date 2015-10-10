using System;
using System.ComponentModel.DataAnnotations;

namespace BlobSmart.Common.Generics
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NonEmptyStringAttribute : ValidationAttribute
    {
        private bool musBeTrimmed;

        public NonEmptyStringAttribute(bool musBeTrimmed = false)
        {
            this.musBeTrimmed = musBeTrimmed;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var text = value as string;

            if (text != null)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    return new ValidationResult(string.Format(
                        "The {0} field may not be empty.",
                        context.MemberName));
                }
                else if(musBeTrimmed && (text != text.Trim()))
                {
                    return new ValidationResult(string.Format(
                        "The {0} field must be trimmed and non-empty.",
                        context.MemberName));
                }
            }

            return ValidationResult.Success;
        }
    }
}
