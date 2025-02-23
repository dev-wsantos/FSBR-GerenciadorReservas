using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorReservas.Domain.Interfaces
{
    public interface IReservaRepository
    {
        Task<List<Reserva>> ObterTodasReservasAsync();
        Task<Reserva> ObterPorIdAsync(int? id);
        Task<List<Reserva>> ObterReservasPorSalaAsync(int salaId, DateTime data);
        Task<List<Reserva>> ObterReservasPorUsuarioAsync(int usuarioId);
        Task AtualizarStatusReservaAsync(int reservaId, StatusReserva status);
        Task<bool> VerificarConflitoReservaAsync(int salaId, DateTime dataHoraReserva);
        Task<List<Reserva>> ObterReservasPorDataAsync(DateTime data);
        Task AdicionarAsync(Reserva reserva);
        Task AtualizarAsync(Reserva reserva);

    }
}
