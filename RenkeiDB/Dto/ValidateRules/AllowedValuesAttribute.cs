using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;
using RenkeiDB.Common;

namespace RenkeiDB.Dto.ValidateRules
{
    /// <summary>
    /// 許可された値のみを検証するバリデーション属性
    /// </summary>
    public class AllowedValuesAttribute : ValidationAttribute
    {
        private readonly string[] _allowedValues;

        public AllowedValuesAttribute(params string[] allowedValues)
        {
            _allowedValues = allowedValues;
        }

        /// <summary>
        /// バリデーションチェック
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns>検証結果</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !_allowedValues.Contains(value.ToString(), StringComparer.OrdinalIgnoreCase))
            {
                return new ValidationResult(string.Format(ErrorMessage ?? SystemConstants.Message.AllowValue, validationContext.DisplayName, string.Join(", ", _allowedValues)));
            }
            return ValidationResult.Success;
        }
    }
}
