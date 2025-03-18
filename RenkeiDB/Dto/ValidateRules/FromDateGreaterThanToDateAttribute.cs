using RenkeiDB.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace RenkeiDB.Dto.ValidateRules
{
    /// <summary>
    /// 開始日が終了日よりも後でないことを検証するバリデーション属性
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
                var fromDateValue = fromDateProp?.GetValue(validationContext.ObjectInstance, null);

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
                    string display_name = (fromDateProp.GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault() as DisplayAttribute)?.GetName();
                    display_name ??= (fromDateProp.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute)?.DisplayName;

                    validationResult = new ValidationResult(ErrorMessage ?? string.Format(SystemConstants.Message.FromDayGreaterThanToDay, display_name ?? _fromDatePropertyName, validationContext.DisplayName));
                }

                return validationResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
