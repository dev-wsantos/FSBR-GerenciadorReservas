using GerenciadorReservas.Application.DTOs;

namespace GerenciadorReservas.Application.Interfaces
{
    public interface IReservaService
    {
        Task<IEnumerable<ReservaDTO>> GetReservasAsync();
        Task<ReservaDTO> GetReservaAsync(int? id);
        Task<ReservaDTO> CriarReservaAsync(ReservaDTO reserva);
        Task<ReservaDTO> EditarReservaAsync(int? id, ReservaDTO reserva);
        Task<ReservaDTO> CancelarReservaAsync(int reservaId);


    }
}
