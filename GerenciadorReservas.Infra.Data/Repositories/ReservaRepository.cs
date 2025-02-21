using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Interfaces;
using GerenciadorReservas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorReservas.Infra.Data.Repositories
{
    public class ReservaRepository : IReservaRepository
    {

        ApplicationDbContext _reservaContext;

        public ReservaRepository(ApplicationDbContext reservaContext)
        {
            _reservaContext = reservaContext;
        }

        public async Task<IEnumerable<Reserva>> GetAllAsync()
        {
            return await _reservaContext.Reservas.ToListAsync();
        }

        public async Task<Reserva> GetByIdAsync(int? id)
        {
            return await _reservaContext.Reservas.FindAsync(id);
        }

        public async Task<Reserva> CreateAsync(Reserva reserva)
        {
            _reservaContext.Add(reserva);
            await _reservaContext.SaveChangesAsync();

            return reserva;
        }

        public async Task<Reserva> UpdateAsync(Reserva reserva)
        {
            _reservaContext.Update(reserva);
            await _reservaContext.SaveChangesAsync();

            return reserva;
        }

        public async Task<Reserva> RemoveAsync(Reserva reserva)
        {
            _reservaContext.Remove(reserva);
            await _reservaContext.SaveChangesAsync();

            return reserva;
        }

      
    }
}
