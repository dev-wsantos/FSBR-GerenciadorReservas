using GerenciadorReservas.Application.DTOs;

namespace GerenciadorReservas.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDTO>> GetUsuarios();
        Task<UsuarioDTO> GetUsuario(int? id);
        Task Add(UsuarioDTO usuario);
        Task Update(UsuarioDTO usuario);
        Task Remove(int? id);

    }
}
