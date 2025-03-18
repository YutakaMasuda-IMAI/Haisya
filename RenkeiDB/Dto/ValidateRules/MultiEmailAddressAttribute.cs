using RenkeiDB.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RenkeiDB.Dto.ValidateRules
{
    /// <summary>
    /// 複数のメールアドレスを検証するバリデーション属性
    /// </summary>
    public class MultiEmailAddressAttribute : ValidationAttribute
    {
        /// <summary>
        /// バリデーションチェック
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns>検証結果</returns>
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
                Regex regex = new Regex(emailPattern);
                bool isValidEmail = regex.IsMatch(email);

                if (!isValidEmail)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? SystemConstants.Message.InValidEmail, validationContext.DisplayName));
                }
            }

            return validationResult;
        }
    }
}
