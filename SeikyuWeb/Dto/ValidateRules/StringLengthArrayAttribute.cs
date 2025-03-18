using SeikyuWeb.Common;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SeikyuWeb.Dto.ValidateRules
{
    /// <summary>
    /// 配列内の文字列の長さを検証するバリデーション属性
    /// </summary>
    public class StringLengthArrayAttribute: ValidationAttribute
    {
         private readonly int _maxLength;

        public StringLengthArrayAttribute(int maxLength)
        {
            _maxLength = maxLength;
        }

        /// <summary>
        /// バリデーションチェック
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns>検証結果</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            string[] arrValue = (string[])value;
            if (arrValue.Any(v => v != null && v.Length > _maxLength))
            {
                return new ValidationResult(string.Format(ErrorMessage ?? SystemConstants.Message.InvalidNumberLength, validationContext.DisplayName));
            }     

            return ValidationResult.Success;
        }
    }
}
