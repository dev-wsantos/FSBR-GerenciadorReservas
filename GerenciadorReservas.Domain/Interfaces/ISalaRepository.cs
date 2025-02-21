using GerenciadorReservas.Domain.Entities;

namespace GerenciadorReservas.Domain.Interfaces
{
    public interface ISalaRepository
    {
        Task<Sala> GetByIdAsync(int id);
        Task<IEnumerable<Sala>> GetAllAsync();
        Task AddAsync(Sala sala);
        Task UpdateAsync(Sala sala);
        Task RemoveAsync(Sala sala);
    }
}
