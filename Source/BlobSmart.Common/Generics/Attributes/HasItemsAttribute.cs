using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BlobSmart.Common.Generics
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HasItemsAttribute : ValidationAttribute
    {
        private Type itemType;
        private int minElements;

        public HasItemsAttribute(Type itemType, int minItems = 1)
        {
            Contract.Requires(itemType != null, nameof(itemType));
            Contract.Requires(minItems >= 0, nameof(itemType));

            this.itemType = itemType;
            this.minElements = minItems;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value == null)
                return ValidationResult.Success;

            var enumerable = value as IEnumerable;

            if (enumerable == null)
            {
                return new ValidationResult(string.Format(
                    "The \"{0}\" value does implement the IEnumerable interface", 
                    value.GetType()));
            }
            else
            {
                var enumerator = enumerable.GetEnumerator();

                if (!enumerator.MoveNext())
                {
                    return new ValidationResult(string.Format(
                        "The {0} field must have {1} or more items.",
                        context.MemberName, GetMinItemsDecription()));
                }
            }

            return ValidationResult.Success;
        }

        private string GetMinItemsDecription()
        {
            switch (minElements)
            {
                case 0:
                    return "zero";

                case 1:
                    return "one";

                default:
                    return minElements.ToString("N0");
            }
        }
    }
}