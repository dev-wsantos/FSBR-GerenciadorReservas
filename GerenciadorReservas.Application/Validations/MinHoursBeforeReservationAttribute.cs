using System.ComponentModel.DataAnnotations;

namespace GerenciadorReservas.Application.Validations
{
    public class MinHoursBeforeReservationAttribute : ValidationAttribute
    {
        private readonly int _minHours;

        public MinHoursBeforeReservationAttribute(int minHours)
        {
            _minHours = minHours;
            ErrorMessage = $"A reserva deve ser feita com no mínimo {_minHours} horas de antecedência.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime reservationDateTime)
            {
                // Captura o horário atual e remove os segundos e milissegundos
                var now = DateTime.Now;
                now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);

                // Também remove os segundos e milissegundos da data de reserva
                reservationDateTime = new DateTime(reservationDateTime.Year, reservationDateTime.Month, reservationDateTime.Day,
                                                   reservationDateTime.Hour, reservationDateTime.Minute, 0);

                // Verifica se a reserva está pelo menos _minHours no futuro
                if (reservationDateTime < now.AddHours(_minHours))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }



    }
}
