using GerenciadorReservas.Application.Validations;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Enums;

namespace GerenciadorReservas.Application.DTOs
{
    public class ReservaDTO
    {
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int SalaId { get; set; }
        public Sala? Sala { get; set; }

        [MinHoursBeforeReservation(24)]
        [FutureDateOnly]
        public DateTime DataHora { get; set; }

        [ValidEnumValue(typeof(StatusReserva))]
        public StatusReserva Status { get; set; }
    }
}
