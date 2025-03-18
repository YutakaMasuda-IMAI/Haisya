using SeikyuWeb.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SeikyuWeb.Dto.ValidateRules
{
    public class MultiEmailAddressAttribute : ValidationAttribute
    {
        /// <summary>
        /// バリデーションチェック
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!(new RequiredAttribute().IsValid(value)))
            {
                return validationResult;
            }

            foreach (string email in value.ToString().Split(";"))
            {
                Regex regex = new(emailPattern);
                if (!regex.IsMatch(email))
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? SystemConstants.Message.InValidEmail, validationContext.DisplayName));
                }
            }

            return validationResult;
        }
    }
}
