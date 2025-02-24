using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Interfaces;
using GerenciadorReservas.Infra.Data.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorReservas.Infra.Data.Data.Repositories
{
    public class SalaRepository : ISalaRepository
    {
        private ApplicationDbContext _salaContext;

        public SalaRepository(ApplicationDbContext salaContext)
        {
            _salaContext = salaContext;
        }

        public async Task<IEnumerable<Sala>> GetAllAsync()
        {
            return await _salaContext.Salas.ToListAsync();
        }

        public async Task<Sala> GetByIdAsync(int? id)
        {
            return await _salaContext.Salas.FindAsync(id);
        }

        public async Task<Sala> CreateAsync(Sala sala)
        {
            _salaContext.Add(sala);
            await _salaContext.SaveChangesAsync();
            
            return sala;
        }
        public async Task<Sala> UpdateAsync(Sala sala)
        {
            _salaContext.Update(sala);
            await _salaContext.SaveChangesAsync();
            return sala;
        }

        public async Task<Sala> RemoveAsync(Sala sala)
        {
            _salaContext.Remove(sala);
            await _salaContext.SaveChangesAsync();
            return sala;
        }

        
    }
}
