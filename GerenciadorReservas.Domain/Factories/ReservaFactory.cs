using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Enums;
using GerenciadorReservas.Domain.Interfaces;
using GerenciadorReservas.Domain.Validation;

namespace GerenciadorReservas.Domain.Factories
{
    public class ReservaFactory : IReservaFactory
    {
        public Reserva CriarReserva(int salaId, int usuarioId, DateTime dataHoraInicio, DateTime dataHoraFim)
        {
            if (dataHoraInicio < DateTime.Now)
                throw new DomainExceptionValidation("Não é possível criar uma reserva para uma data passada.");

            if (dataHoraFim <= dataHoraInicio)
                throw new DomainExceptionValidation("A data e hora de término deve ser maior que a de início.");

            return new Reserva(salaId, usuarioId, dataHoraInicio, dataHoraFim);
        }

    }
}
