using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlobSmart.Common.Generics
{
    public class ValidationException : Exception
    {
        public ValidationException(List<ValidationResult> results) :
            base(GetMessage(results))
        {
            Results = results;
        }

        private static string GetMessage(List<ValidationResult> results)
        {
            return string.Format(
                "{0} validation errors were detected.  (See the \"Results\" collection for more details.)",
                results.Count);
        }

        public List<ValidationResult> Results { get; private set; }
    }
}