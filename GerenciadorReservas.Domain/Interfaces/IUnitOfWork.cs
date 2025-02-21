namespace GerenciadorReservas.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository UsuarioRepository { get; }
        ISalaRepository SalaRepository { get; }
        IReservaRepository ReservaRepository { get; }
        Task<int> CommitAsync();
    }
}
