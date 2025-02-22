using AutoMapper;
using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Application.Interfaces;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Interfaces;

namespace GerenciadorReservas.Application.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IMapper _mapper;

        public ReservaService(IReservaRepository reservaRepository, IMapper mapper)
        {
            _reservaRepository = reservaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservaDTO>> GetReservas()
        {
            var reservas = await _reservaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReservaDTO>>(reservas);
        }

        public async Task<ReservaDTO> GetReserva(int? id)
        {
            var reservaEntity = await _reservaRepository.GetByIdAsync(id);
            return _mapper.Map<ReservaDTO>(reservaEntity);
        }

        public async Task Add(ReservaDTO reserva)
        {
            var reservaEntity = _mapper.Map<Reserva>(reserva);
            await _reservaRepository.CreateAsync(reservaEntity);
        }

        public async Task Update(ReservaDTO reserva)
        {
            var reservaEntity = _mapper.Map<Reserva>(reserva);
            await _reservaRepository.UpdateAsync(reservaEntity);
        }

        public async Task Remove(int? id)
        {
            var reservaEntity = _reservaRepository.GetByIdAsync(id).Result;
            await _reservaRepository.RemoveAsync(reservaEntity);
        }
    }
}
