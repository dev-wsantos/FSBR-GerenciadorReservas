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
            try
            {
               return new Reserva(salaId, usuarioId, dataHoraInicio, dataHoraFim, StatusReserva.Confirmada);
            }
            catch (DomainExceptionValidation ex)
            {

                throw new ReservaException($"Erro ao criar a reserva: {ex.Message}");
            }            
        }

    }
}
