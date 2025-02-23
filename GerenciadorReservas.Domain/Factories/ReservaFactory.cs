using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Enums;
using GerenciadorReservas.Domain.Interfaces;
using GerenciadorReservas.Domain.Validation;

namespace GerenciadorReservas.Domain.Factories
{
    public class ReservaFactory : IReservaFactory
    {
        public Reserva CriarReserva(int salaId, int usuarioId, DateTime dataHoraReserva)
        {
            if (dataHoraReserva < DateTime.Now)
                throw new DomainExceptionValidation("Não é possível criar uma reserva para uma data passada.");

            return new Reserva(salaId, usuarioId, dataHoraReserva);
        }
    }
}
