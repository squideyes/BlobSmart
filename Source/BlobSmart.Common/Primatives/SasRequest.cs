using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlobSmart.Common
{
    [CustomValidation(typeof(SasRequest), "FinalCheck")]
    public class SasRequest
    {
        [Required]
        public ReadOrWrite ReadOrWrite { get; set; }

        [Required]
        [Range(1, 240)]
        public int Minutes { get; set; }

        [Required]
        public List<string> Guids { get; set; }

        public static ValidationResult FinalCheck(
            SasRequest request, ValidationContext cpntext)
        {
            const string BADGUIDS =
                "The Guids list must contain 1 to 100 non-empty GUID values.";

            if (request.Guids == null)
                return new ValidationResult(BADGUIDS);

            if ((request.Guids.Count == 0) || (request.Guids.Count > 100))
                return new ValidationResult(BADGUIDS);

            if (request.Guids.All(guid => guid.IsGuid()))
                return new ValidationResult(BADGUIDS);

            return ValidationResult.Success;
        }
    }
}
