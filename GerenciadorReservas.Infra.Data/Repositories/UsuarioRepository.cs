using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Interfaces;
using GerenciadorReservas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorReservas.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        ApplicationDbContext _usuarioContext;

        public UsuarioRepository(ApplicationDbContext usuarioContext)
        {
            _usuarioContext = usuarioContext;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _usuarioContext.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(int? id)
        {
            return await _usuarioContext.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            _usuarioContext.Add(usuario);
            await _usuarioContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            _usuarioContext.Update(usuario);
            await _usuarioContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> RemoveAsync(Usuario usuario)
        {
            _usuarioContext.Remove(usuario);
            await _usuarioContext.SaveChangesAsync();

            return usuario;
        }
    }
  
}
