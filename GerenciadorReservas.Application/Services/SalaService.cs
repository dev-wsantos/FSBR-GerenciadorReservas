using AutoMapper;
using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Application.Interfaces;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Interfaces;

namespace GerenciadorReservas.Application.Services
{
    public class SalaService : ISalaService
    {
        private readonly IUnitOfWork _unitOfWork;
      
        private readonly IMapper _mapper;

        public SalaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SalaDTO>> GetSalas()
        {
            var salaEntity = await _unitOfWork.SalaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SalaDTO>>(salaEntity);
        }

        public async Task<SalaDTO> GetSala(int? id)
        {
            var salaEntity = await _unitOfWork.SalaRepository.GetByIdAsync(id);

            return _mapper.Map<SalaDTO>(salaEntity);
        }


        public async Task Add(SalaDTO sala)
        {
            var salaEntity = _mapper.Map<Sala>(sala);
            await _unitOfWork.SalaRepository.CreateAsync(salaEntity);
        }

        public async Task Update(SalaDTO sala)
        {
            var salaEntity = _mapper.Map<Sala>(sala);
            await _unitOfWork.SalaRepository.UpdateAsync(salaEntity);
        }

        public async Task Remove(int? id)
        {
            var salaEntity = _unitOfWork.SalaRepository.GetByIdAsync(id).Result;
            await _unitOfWork.SalaRepository.RemoveAsync(salaEntity);
        }
    }
}
