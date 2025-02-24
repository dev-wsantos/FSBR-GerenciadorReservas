using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.UI.Web.Models;
using GerenciadorReservas.UI.Web.Services.Interfaces;

namespace GerenciadorReservas.UI.Web.Services
{
    public class SalaService : ISalaService
    {
        private readonly HttpClient _httpClient;

        public SalaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SalaViewModel>> GetSalasAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7145/api/Salas/ListarSalas");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<SalaViewModel>>() ?? new List<SalaViewModel>();
        }

        public async Task<Sala?> GetSalaByIdAsync(int? id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7145/api/Salas/ObterSala/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Sala>();
        }
    }

}
