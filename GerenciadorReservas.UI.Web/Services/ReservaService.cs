using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorReservas.UI.Web.Models;
using GerenciadorReservas.UI.Web.Services.Interfaces;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Domain.Enums;
using System.Text;
using System.Text.Json;
using System.Net.WebSockets;
using GerenciadorReservas.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel;

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


        public async Task<bool> CreateReservaAsync(ReservaViewModel model)
        {
            var content = PrepareRequestContent(model);
            var response = await SendRequestAsync(content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            await HandleErrorResponseAsync(response);

            return false;
        }

        private StringContent PrepareRequestContent(ReservaViewModel model)
        {
            var dtoDict = new Dictionary<string, object>
            {
                { "UsuarioId", model.UsuarioId },
                { "SalaId", model.SalaId },
                { "DataHoraInicio", model.DataHoraInicio },
                { "DataHoraFim", model.DataHoraFim },
                { "Status", model.Status }
            };

            

            var json = JsonSerializer.Serialize(dtoDict);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private async Task<HttpResponseMessage> SendRequestAsync(StringContent content)
        {
            return await _httpClient.PostAsync("http://localhost:5107/api/Reservas/AdicionarReserva", content);
        }

        private async Task HandleErrorResponseAsync(HttpResponseMessage response)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorDetails = JsonSerializer.Deserialize<ErrorDetails>(errorResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });

                var errorMessages = ExtractErrorMessages(errorDetails);

                if (errorMessages.Any())
                {
                    throw new ValidationException(string.Join("\n", errorMessages));
                }

                throw new ValidationException("Erro desconhecido.");
            }

            throw new Exception("Erro interno no servidor. Por favor, tente novamente.");
        }

        private List<string> ExtractErrorMessages(ErrorDetails errorDetails)
        {
            return errorDetails?.Errors?.Values
                .SelectMany(x => x)
                .ToList() ?? new List<string>();
        }
    }
}
