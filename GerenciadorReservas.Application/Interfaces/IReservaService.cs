using GerenciadorReservas.Application.DTOs;

namespace GerenciadorReservas.Application.Interfaces
{
    public interface IReservaService
    {
        Task<IEnumerable<ReservaDTO>> GetReservas();
        Task<ReservaDTO> GetReserva(int? id);
        Task Add(ReservaDTO reserva);
        Task Update(ReservaDTO reserva);
        Task Remove(int? id);
    }
}
