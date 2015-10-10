using System;
using System.ComponentModel.DataAnnotations;

namespace BlobSmart.Common.Generics
{
    public class DefinedEnumAttribute : ValidationAttribute
    {
        private Type type;

        public DefinedEnumAttribute(Type type)
        {
            Contract.Requires(type != null, nameof(type));
            Contract.Requires(type.IsEnum, nameof(type));

            this.type = type;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext vc)
        {
            if ((!type.IsEnum) || (!Enum.IsDefined(type, value)))
            {
                return new ValidationResult(string.Format(
                    "The {0} field must be set to a pre-defined {1} value.",
                    vc.MemberName, type.FullName));
            }

            return ValidationResult.Success;
        }
    }
}