using GerenciadorReservas.Application.DTOs;

namespace GerenciadorReservas.Application.Interfaces
{
    public interface ISalaService
    {
        Task<IEnumerable<SalaDTO>> GetSalas();
        Task<SalaDTO> GetSala(int? id);
        Task Add(SalaDTO sala);
        Task Update(SalaDTO sala);
        Task Remove(int? id);
    }
}
