using GerenciadorReservas.Domain.Entities;

namespace GerenciadorReservas.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int? id);
        Task <Usuario> CreateAsync(Usuario usuario);
        Task <Usuario> UpdateAsync(Usuario usuario);
        Task <Usuario> RemoveAsync(Usuario usuario);
    }
}
