using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.UI.Web.Models;

namespace GerenciadorReservas.UI.Web.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioViewModel>> GetUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync(int? id);
    }

}
