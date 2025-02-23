using System.ComponentModel.DataAnnotations;

namespace GerenciadorReservas.Domain.Enums
{
    public enum StatusReserva
    {
        [Display(Name = "Aguardando Confirmação")]
        AguardandoConfirmacao = 0,

        [Display(Name = "Confirmada")]
        Confirmada = 1,

        [Display(Name = "Cancelada")]
        Cancelada = 2
        
    }
}
