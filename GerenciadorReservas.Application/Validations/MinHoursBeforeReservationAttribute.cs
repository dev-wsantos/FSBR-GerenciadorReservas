using System.ComponentModel.DataAnnotations;

namespace GerenciadorReservas.Application.Validations
{
    public class MinHoursBeforeReservationAttribute : ValidationAttribute
    {
        private readonly int _minHours;

        public MinHoursBeforeReservationAttribute(int minHours)
        {
            _minHours = minHours;
            ErrorMessage = $"A reserva deve ser feita com no mínimo {_minHours} horas de antecedência";
        }
        override protected ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                if ((dateTime - DateTime.Now).TotalHours < _minHours)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}
