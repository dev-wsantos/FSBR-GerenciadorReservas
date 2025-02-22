using GerenciadorReservas.Domain.Interfaces;
using GerenciadorReservas.Infra.Data.Context;

namespace GerenciadorReservas.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IUsuarioRepository UsuarioRepository { get; }
        public ISalaRepository SalaRepository { get; }

        public IReservaRepository ReservaRepository { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            IUsuarioRepository usuarioRepository,
            ISalaRepository salaRepository,
            IReservaRepository reservaRepository)
        {
            _context = context;
            UsuarioRepository = usuarioRepository;
            SalaRepository = salaRepository;
            ReservaRepository = reservaRepository;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
