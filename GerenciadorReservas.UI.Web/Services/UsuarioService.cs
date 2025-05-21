using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.UI.Web.Models;
using GerenciadorReservas.UI.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GerenciadorReservas.UI.Web.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _httpClient;

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UsuarioViewModel>> GetUsuariosAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7145/api/Usuarios/ListarUsuarios");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<UsuarioViewModel>>() ?? new List<UsuarioViewModel>();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int? id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7145/api/Usuarios/ObterUsuario/{id}");
            response.EnsureSuccessStatusCode();
            var usuario = await response.Content.ReadFromJsonAsync<Usuario>();
            if (usuario == null)
            {
                throw new InvalidOperationException("Usuario não encontrado.");
            }
            return usuario;
        }
    }

}
