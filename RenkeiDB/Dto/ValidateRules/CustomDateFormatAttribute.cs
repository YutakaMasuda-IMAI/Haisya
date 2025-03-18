using RenkeiDB.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace RenkeiDB.Dto.ValidateRules
{
    /// <summary>
    /// カスタム日付形式を検証するバリデーション属性
    /// </summary>
    public class CustomDateFormatAttribute : ValidationAttribute
    {
        private readonly string _formats;
        public CustomDateFormatAttribute(string formats)
        {
            _formats = formats;
        }

        /// <summary>
        /// バリデーションチェック
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns>検証結果</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || value.ToString() == string.Empty)
            {
                return ValidationResult.Success;
            }

            string dateString = value.ToString();
            if (DateTime.TryParseExact(dateString, _formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(string.Format(ErrorMessage ?? SystemConstants.Message.InValidDate, validationContext.DisplayName));
        }
    }
}

