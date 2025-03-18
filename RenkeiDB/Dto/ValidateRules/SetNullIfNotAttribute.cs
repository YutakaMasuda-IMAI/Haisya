using RenkeiDB.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RenkeiDB.Dto.ValidateRules
{
    /// <summary>
    /// 依存するプロパティが特定の値でない場合に値をnullに設定するバリデーション属性
    /// </summary>
    public class SetNullIfNotAttribute : ValidationAttribute
    {
        private readonly string _dependent_name;
        private readonly object _if_not_value;

        public SetNullIfNotAttribute(string dependent_name, object if_not_value)
        {
            _dependent_name = dependent_name;
            _if_not_value = if_not_value;
        }

        /// <summary>
        /// バリデーションチェック
        /// </summary>
        /// <param name="value">検証する値</param>
        /// <param name="context">検証コンテキスト</param>
        /// <returns>検証結果</returns>
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (_dependent_name == null)
            {
                return ValidationResult.Success;
            }

            var dependent_prop = context.ObjectInstance.GetType().GetProperty(_dependent_name);
            var dependent_value = dependent_prop?.GetValue(context.ObjectInstance, null);

            ErrorMessage = string.Format(SystemConstants.Message.MustSetNull, context.DisplayName);

            if (dependent_value == null)
            {
                var display_name = (dependent_prop?.GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault() as DisplayAttribute)?.GetName();
                display_name ??= (dependent_prop.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute)?.DisplayName;
                ErrorMessage = string.Format(SystemConstants.Message.MissingParams, display_name ?? _dependent_name, context.DisplayName);
                return new ValidationResult(ErrorMessage);
            }

            if (dependent_value.Equals(_if_not_value))
            {
                return ValidationResult.Success;
            }

            return value != null ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
        }
    }
}
