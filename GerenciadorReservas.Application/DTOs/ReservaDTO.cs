using GerenciadorReservas.Application.Validations;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Enums;

namespace GerenciadorReservas.Application.DTOs
{
    public class ReservaDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
       
        public int SalaId { get; set; }

        [MinHoursBeforeReservation(24)]
        [FutureDateOnly]
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }

        [ValidEnumValue(typeof(StatusReserva))]
        public StatusReserva Status { get; set; }

    }
}
