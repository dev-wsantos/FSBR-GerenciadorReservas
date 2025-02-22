using AutoMapper;
using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Application.Interfaces;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Interfaces;

namespace GerenciadorReservas.Application.Services
{
    public class SalaService : ISalaService
    {
        private readonly ISalaRepository _salaRepository;
        private readonly IMapper _mapper;

        public SalaService(ISalaRepository salaRepository, IMapper mapper)
        {
            _salaRepository = salaRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SalaDTO>> GetSalas()
        {
            var salaEntity = await _salaRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<SalaDTO>>(salaEntity);
        }

        public async Task<SalaDTO> GetSala(int? id)
        {
            var salaEntity = await _salaRepository.GetByIdAsync(id);

            return _mapper.Map<SalaDTO>(salaEntity);
        }


        public async Task Add(SalaDTO sala)
        {
            var salaEntity = _mapper.Map<Sala>(sala);
            await _salaRepository.CreateAsync(salaEntity);
        }

        public async Task Update(SalaDTO sala)
        {
            var salaEntity = _mapper.Map<Sala>(sala);
            await _salaRepository.UpdateAsync(salaEntity);
        }

        public async Task Remove(int? id)
        {
            var salaEntity = _salaRepository.GetByIdAsync(id).Result;
            await _salaRepository.RemoveAsync(salaEntity);
        }
    }
}
