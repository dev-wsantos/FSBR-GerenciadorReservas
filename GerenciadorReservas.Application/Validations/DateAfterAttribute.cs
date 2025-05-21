using System.ComponentModel.DataAnnotations;

namespace GerenciadorReservas.Application.Validations
{
    public class DateAfterAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateAfterAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currentValue = value as DateTime?;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                return new ValidationResult($"Propriedade {_comparisonProperty} não encontrada.");
            }

            var comparisonValue = property.GetValue(validationContext.ObjectInstance) as DateTime?;

            if (currentValue.HasValue && comparisonValue.HasValue)
            {
                if (currentValue <= comparisonValue)
                {
                    return new ValidationResult(
                        ErrorMessage ?? $"A data de fim deve ser maior que a data de início.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
