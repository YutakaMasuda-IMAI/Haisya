using RenkeiDB.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RenkeiDB.Dto.ValidateRules
{
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
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string[] array = value as string[];

            var propertyInfo = validationContext.ObjectType.GetProperty(_requiredField);
            var fieldValueRequired = propertyInfo.GetValue(validationContext.ObjectInstance, null);

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
