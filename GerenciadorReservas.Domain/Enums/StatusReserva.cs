using System.ComponentModel.DataAnnotations;

namespace GerenciadorReservas.Domain.Enums
{
    public enum StatusReserva
    {
        [Display(Name = "Confirmada")]
        Confirmada = 1,

        [Display(Name = "Cancelada")]
        Cancelada = 2
        
    }
}
