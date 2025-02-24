using GerenciadorReservas.UI.Web.Models;

namespace GerenciadorReservas.UI.Web.Services.Interfaces
{
    public interface IReservaService
    {
        Task<List<ReservaViewModel>> ObterReservasAsync();
    }
}
