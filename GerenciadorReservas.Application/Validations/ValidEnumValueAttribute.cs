using System.ComponentModel.DataAnnotations;

namespace GerenciadorReservas.Application.Validations
{
    public class ValidEnumValueAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        public ValidEnumValueAttribute(Type enumType)
        {
            _enumType = enumType;
            ErrorMessage = $"O valor informado não é válido para o tipo {_enumType.Name}.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || !Enum.IsDefined(_enumType, value))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
