using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorReservas.Application.Validations
{
    public class FutureDateOnlyAttribute : ValidationAttribute
    {
        public FutureDateOnlyAttribute()
        {
            ErrorMessage = "Não é possível fazer uma reserva para um horário passado.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dataHora)
            {
                if (dataHora < DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }

}
