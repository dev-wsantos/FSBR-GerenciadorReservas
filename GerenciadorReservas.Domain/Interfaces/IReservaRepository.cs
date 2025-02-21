using GerenciadorReservas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorReservas.Domain.Interfaces
{
    public interface IReservaRepository
    {
        Task<IEnumerable<Reserva>> GetAllAsync();
        Task<Reserva> GetByIdAsync(int id);
        Task AddAsync(Reserva reserva);
        Task UpdateAsync(Reserva reserva);
        Task RemoveAsync(Reserva reserva);
    }
}
