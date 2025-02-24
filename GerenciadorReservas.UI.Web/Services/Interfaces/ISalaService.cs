using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.UI.Web.Models;

namespace GerenciadorReservas.UI.Web.Services.Interfaces
{
    public interface ISalaService
    {
        Task<List<SalaViewModel>> GetSalasAsync();
        Task<Sala?> GetSalaByIdAsync(int? id);
    }

}
