using SeikyuWeb.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SeikyuWeb.Dto.ValidateRules
{
    /// <summary>
    /// カスタム日付形式を検証するバリデーション属性
    /// </summary>
    public class CustomDateFormatAttribute : ValidationAttribute
    {
        private readonly string _formats;
        private readonly string _key;
        private readonly string _message;
        public CustomDateFormatAttribute(string formats, string key = null)
        {
            _formats = formats;
            _key = key;
            _message = string.Format(SystemConstants.Message.InValidDate, _key);
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

            if (!String.IsNullOrEmpty(_key))
            {
                return new ValidationResult(_message);
            }

            return new ValidationResult(string.Format(ErrorMessage ?? SystemConstants.Message.InValidDate, validationContext.DisplayName));
        }
    }
}

