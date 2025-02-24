using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorReservas.UI.Web.Models;
using GerenciadorReservas.UI.Web.Services.Interfaces;

namespace GerenciadorReservas.UI.Web.Services
{
    public class ReservasService : IReservaService
    {
        private readonly HttpClient _httpClient;

        public ReservasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ReservaViewModel>> ObterReservasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ReservaViewModel>>("https://localhost:7145/api/Reservas/ListarReservas");
        }
    }
}
