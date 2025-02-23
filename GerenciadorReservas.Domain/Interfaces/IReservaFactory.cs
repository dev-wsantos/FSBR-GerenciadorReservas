using GerenciadorReservas.Domain.Entities;

namespace GerenciadorReservas.Domain.Interfaces
{
    public interface IReservaFactory
    {
        Reserva CriarReserva(int salaId, int usuarioId, DateTime dataHoraInicio, DateTime dataHoraFim);
    }
}
