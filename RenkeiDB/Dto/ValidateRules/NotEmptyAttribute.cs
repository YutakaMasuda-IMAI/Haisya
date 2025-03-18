using RenkeiDB.Common;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.ValidateRules
{
    /// <summary>
    /// 値が空でないことを検証するバリデーション属性
    /// </summary>
    public class NotEmptyAttribute: ValidationAttribute
    {
        public NotEmptyAttribute()
        {
        }

        /// <summary>
        /// バリデーションチェック
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns>検証結果</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(string.Format(SystemConstants.Message.RequiredField, validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }
}
