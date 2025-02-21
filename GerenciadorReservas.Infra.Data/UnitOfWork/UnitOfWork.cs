using GerenciadorReservas.Domain.Interfaces;
using GerenciadorReservas.Infra.Data.Context;

namespace GerenciadorReservas.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext? _context;
        public IUsuarioRepository UsuarioRepository => throw new NotImplementedException();

        public ISalaRepository SalaRepository => throw new NotImplementedException();

        public IReservaRepository ReservaRepository => throw new NotImplementedException();

        public async Task<int> CommitAsync()
        {
            return await _context!.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
