using GerenciadorReservas.Domain.Entities;

namespace GerenciadorReservas.Domain.Interfaces
{
    public interface ISalaRepository
    {
        Task<Sala> GetByIdAsync(int? id);
        Task<IEnumerable<Sala>> GetAllAsync();
        Task<Sala>CreateAsync(Sala sala);
        Task<Sala>UpdateAsync(Sala sala);
        Task<Sala>RemoveAsync(Sala sala);
    }
}
