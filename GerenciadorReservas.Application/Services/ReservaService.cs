using AutoMapper;
using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Application.Interfaces;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Interfaces;

namespace GerenciadorReservas.Application.Services
{
    public class ReservaService : IReservaService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IReservaFactory _reservaFactory;

        public ReservaService(IUnitOfWork unitOfWork, IMapper mapper, IReservaFactory reservaFactory)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _reservaFactory = reservaFactory;
        }

        public async Task<ReservaDTO> CancelarReservaAsync(int reservaId)
        {
            var reserva = await _unitOfWork.ReservaRepository.ObterPorIdAsync(reservaId);
            if (reserva == null)
                throw new InvalidOperationException("Reserva não encontrada");

            reserva.Cancelar();
            
            await _unitOfWork.ReservaRepository.AtualizarAsync(reserva);

            return _mapper.Map<ReservaDTO>(reserva);
        }

        public async Task<ReservaDTO> CriarReservaAsync(ReservaDTO reservaDto)
        {
            var reserva = _reservaFactory.CriarReserva(reservaDto.SalaId, reservaDto.UsuarioId, reservaDto.DataHora);

            await _unitOfWork.ReservaRepository.AdicionarAsync(reserva);

            return _mapper.Map<ReservaDTO>(reserva);
        }

        public async Task<ReservaDTO> EditarReservaAsync(int? id, ReservaDTO reserva)
        {
            var reservaExistente = await _unitOfWork.ReservaRepository.ObterPorIdAsync(id);

            if (reservaExistente == null)
                throw new InvalidOperationException("Reserva não encontrada");

            var existeConflito = await _unitOfWork.ReservaRepository.VerificarConflitoReservaAsync(reserva.SalaId, reserva.DataHora);

            if (existeConflito)
                throw new InvalidOperationException("Já existe uma reserva para a sala no mesmo horário");

            var reservaEntity = _mapper.Map<Reserva>(reserva);
            await _unitOfWork.ReservaRepository.AtualizarAsync(reservaEntity);

            return _mapper.Map<ReservaDTO>(reservaEntity);
        }

        public async Task<ReservaDTO> GetReservaAsync(int? id)
        {
            var reserva = await _unitOfWork.ReservaRepository.ObterPorIdAsync(id);

            if (reserva == null)
                throw new InvalidOperationException("Reserva não encontrada");

           
            return _mapper.Map<ReservaDTO>(reserva);
        }

        public async Task<IEnumerable<ReservaDTO>> GetReservasAsync()
        {
            var reservas = await _unitOfWork.ReservaRepository.ObterTodasReservasAsync();
            return _mapper.Map<List<ReservaDTO>>(reservas);
        }
    }
}
