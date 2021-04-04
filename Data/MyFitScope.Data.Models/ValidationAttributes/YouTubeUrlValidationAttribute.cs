namespace MyFitScope.Data.Models.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class YouTubeUrlValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var valueAsString = value.ToString();

                if (!Regex.IsMatch(valueAsString, @"^(http(s)?:\/\/)?((w){3}.)?youtu(be|.be)?(\.com)?\/.+"))
                {
                    return new ValidationResult("Video Url must be a valid YouTube Url!");
                }
            }

            // If value == null we still have success --> YouTube Url is NOT Required!!!
            return ValidationResult.Success;
        }
    }
}
