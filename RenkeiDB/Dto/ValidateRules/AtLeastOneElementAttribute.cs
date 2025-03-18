using RenkeiDB.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.ValidateRules
{
    /// <summary>
    /// 少なくとも1つの要素が含まれていることを検証するバリデーション属性
    /// </summary>
    public class AtLeastOneElementAttribute : ValidationAttribute
    {
        protected readonly List<ValidationResult> validationResults = new List<ValidationResult>();

        /// <summary>
        /// バリデーションチェック
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns>検証結果</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as IList;
            if (list != null && list.Count > 0)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(string.Format(ErrorMessage ?? SystemConstants.Message.RequiredField, validationContext.DisplayName));
        }
    }
}
