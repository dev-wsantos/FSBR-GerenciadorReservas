using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Enums;
using GerenciadorReservas.Domain.Interfaces;
using GerenciadorReservas.Infra.Data.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorReservas.Infra.Data.Data.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private ApplicationDbContext _reservaContext;

        public ReservaRepository(ApplicationDbContext reservaContext)
        {
            _reservaContext = reservaContext;
        }

        public async Task AdicionarAsync(Reserva reserva)
        {
            _reservaContext.Reservas.Add(reserva);
            await _reservaContext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Reserva reserva)
        {
            _reservaContext.Reservas.Update(reserva);
            await _reservaContext.SaveChangesAsync();
        }

        public async Task AtualizarStatusReservaAsync(int reservaId, StatusReserva status)
        {
            var reserva = await _reservaContext.Reservas.FindAsync(reservaId);
            if (reserva != null)
            {
                reserva.AtualizarStatus(status);
                _reservaContext.Reservas.Update(reserva);

                await _reservaContext.SaveChangesAsync();
            }
        }

        public async Task<Reserva?> ObterPorIdAsync(int? id)
        {
            return await _reservaContext.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Sala)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Reserva>> ObterReservasPorDataAsync(DateTime data)
        {
            return await _reservaContext.Reservas
                .Where(r =>
                    r.DataHoraInicio.Date == data.Date ||  
                    r.DataHoraFim.Date == data.Date ||    
                    r.DataHoraInicio < data && r.DataHoraFim > data 
                )
                .ToListAsync();
        }

        public async Task<List<Reserva>> ObterReservasPorSalaAsync(int salaId, DateTime data)
        {
            return await _reservaContext.Reservas
                .Include(r => r.Sala)
                .Include(r => r.Usuario)
                .Where(r =>
                    r.SalaId == salaId &&
                    (r.DataHoraInicio.Date == data.Date ||
                     r.DataHoraFim.Date == data.Date ||
                     r.DataHoraInicio < data && r.DataHoraFim > data)
                )
                .ToListAsync();
        }

        public async Task<List<Reserva>> ObterReservasPorUsuarioAsync(int usuarioId)
        {
            return await _reservaContext.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Sala)
                .Where(r => r.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<List<Reserva>> ObterTodasReservasAsync()
        {
            var reservas = await _reservaContext.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Sala)
                .AsNoTracking()
                .ToListAsync();

            reservas.ForEach(r =>
            {
                r.Usuario?.Reservas?.Clear();
                r.Sala?.Reservas?.Clear();
            });

            return reservas ?? new List<Reserva>();
        }


        public async Task<bool> VerificarConflitoReservaAsync(int salaId, int usuarioId, DateTime dataHoraInicio, DateTime dataHoraFim)
        {
            
            dataHoraInicio = dataHoraInicio.AddMilliseconds(-dataHoraInicio.Millisecond);
            dataHoraFim = dataHoraFim.AddMilliseconds(-dataHoraFim.Millisecond);

            return await _reservaContext.Reservas.AnyAsync(r =>
                r.SalaId == salaId &&
                r.Status == StatusReserva.Confirmada && 
                (
                    
                    (dataHoraInicio >= r.DataHoraInicio && dataHoraInicio < r.DataHoraFim) ||
                    (dataHoraFim > r.DataHoraInicio && dataHoraFim <= r.DataHoraFim) ||
                    (dataHoraInicio <= r.DataHoraInicio && dataHoraFim >= r.DataHoraFim)
                )
                
            );
        }

        
    }
}
