using SeikyuWeb.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SeikyuWeb.Dto.ValidateRules
{
    /// <summary>
    /// 依存プロパティが特定の値の場合に必須とするバリデーション属性
    /// </summary>
    public class RequiredIfAttribute : ValidationAttribute
    {
        private readonly string _dependentName;
        private readonly object _expectedValue;

        public RequiredIfAttribute(string dependentName, object value)
        {
            _dependentName = dependentName;
            _expectedValue = value;
        }

        /// <summary>
        /// バリデーションチェック
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns>検証結果</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (_dependentName == null)
            {
                return ValidationResult.Success;
            }
            PropertyInfo dependentProp = validationContext.ObjectInstance.GetType().GetProperty(_dependentName);
            object dependentValue = dependentProp?.GetValue(validationContext.ObjectInstance, null);
            ErrorMessage = string.Format(SystemConstants.Message.RequiredField, validationContext.DisplayName);

            if (dependentValue == null)
            {
                ErrorMessage = string.Format(SystemConstants.Message.MissingParams, _dependentName, validationContext.DisplayName);
                return (_expectedValue == null && value == null) ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
            }

            if (dependentValue.Equals(_expectedValue))
            {
                PropertyInfo curentProp = validationContext.ObjectInstance.GetType().GetProperty(validationContext.DisplayName);

                // 配列に少なくとも1つは含まれているかチェック
                if (curentProp.PropertyType.IsArray)
                {
                    if (value == null || (value as string[]).All(string.IsNullOrEmpty))
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }

                if (!(new RequiredAttribute().IsValid(value)))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
