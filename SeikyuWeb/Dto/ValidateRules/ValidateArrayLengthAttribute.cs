using SeikyuWeb.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SeikyuWeb.Dto.ValidateRules
{
    /// <summary>
    /// 配列の長さを検証するバリデーション属性
    /// </summary>
    public class ValidateArrayLengthAttribute : ValidationAttribute
    {
        private readonly int _requiredLength;
        private readonly string _requiredField;

        public ValidateArrayLengthAttribute(int requiredLength, string requiredField)
        {
            _requiredLength = requiredLength;
            _requiredField = requiredField;
        }

        /// <summary>
        /// バリデーションチェック
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns>検証結果</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] array = value as string[];

            PropertyInfo propertyInfo = validationContext.ObjectType.GetProperty(_requiredField);
            object fieldValueRequired = propertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (fieldValueRequired != null && Convert.ToBoolean(fieldValueRequired) == true)
            {
                if (value != null && array.All(string.IsNullOrEmpty))
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? SystemConstants.Message.RequiredField, validationContext.DisplayName));
                }

                if (value != null && array.Length != _requiredLength)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? SystemConstants.Message.InValidParam, validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }
    }
}
