using SeikyuWeb.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SeikyuWeb.Dto.ValidateRules
{
    /// <summary>
    /// 開始日が終了日よりも大きいことを検証するバリデーション属性
    /// </summary>
    public class FromDateGreaterThanToDateAttribute : ValidationAttribute
    {
        private readonly string _fromDatePropertyName;

        public FromDateGreaterThanToDateAttribute(string propertyName)
        {
            _fromDatePropertyName = propertyName;
        }

        /// <summary>
        /// バリデーションチェック
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <param name="validationContext">検証コンテキスト</param>
        /// <returns>検証結果</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;
            try
            {
                PropertyInfo fromDateProp = validationContext.ObjectInstance.GetType().GetProperty(_fromDatePropertyName);
                object fromDateValue = fromDateProp?.GetValue(validationContext.ObjectInstance, null);

                if (fromDateProp == null || fromDateValue == null)
                {
                    return validationResult;
                }

                if (!DateTime.TryParse(Convert.ToString(value), out DateTime toDate) || !DateTime.TryParse(Convert.ToString(fromDateValue), out DateTime fromDate))
                {
                    return validationResult;
                }

                if (toDate < fromDate)
                {
                    validationResult = new ValidationResult(ErrorMessage ?? string.Format(SystemConstants.Message.FromDayGreaterThanToDay, _fromDatePropertyName, validationContext.DisplayName));
                }

                return validationResult;
            }
            catch
            {
                throw;
            }
        }

    }
}
