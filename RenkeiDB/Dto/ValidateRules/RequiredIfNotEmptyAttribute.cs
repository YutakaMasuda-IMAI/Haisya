using RenkeiDB.Common;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RenkeiDB.Dto.ValidateRules
{
    /// <summary>
    /// 依存するプロパティが空でない場合に必須とするバリデーション属性
    /// </summary>
    public class RequiredIfNotEmptyAttribute : ValidationAttribute
    {
        private readonly string _dependentName;

        public RequiredIfNotEmptyAttribute(string dependentName)
        {
            _dependentName = dependentName;
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
            var dependentValue = dependentProp?.GetValue(validationContext.ObjectInstance, null);

            ErrorMessage = string.Format(ErrorMessage ?? SystemConstants.Message.RequiredField, validationContext.DisplayName);

            if (dependentValue != null)
            {
                if (!(new RequiredAttribute().IsValid(value)))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
