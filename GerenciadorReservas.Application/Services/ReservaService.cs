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
        private readonly IEmailService _emailService;

        public ReservaService(IUnitOfWork unitOfWork, IMapper mapper, IReservaFactory reservaFactory, IEmailService emailService)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _reservaFactory = reservaFactory;
            _emailService = emailService;
        }

        public async Task<ReservaDTO> CancelarReservaAsync(int reservaId)
        {
            var reserva = await _unitOfWork.ReservaRepository.ObterPorIdAsync(reservaId);

            if (reserva == null)
                throw new InvalidOperationException("Reserva não encontrada");


            reserva.Cancelar();

            await _unitOfWork.ReservaRepository.AtualizarAsync(reserva);
            await _unitOfWork.CommitAsync();

            var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(reserva.UsuarioId);

            var sala = await _unitOfWork.SalaRepository.GetByIdAsync(reserva.SalaId);


            await _emailService.EnviarConfirmacaoCancelamentoAsync(
                usuario.Email!,
                usuario.Nome!,
                sala.Nome!,
                reserva.DataHoraInicio,
                reserva.DataHoraFim
            );


            return _mapper.Map<ReservaDTO>(reserva);
        }

        public async Task<ReservaDTO> CriarReservaAsync(ReservaDTO reservaDto)
        {
            var reserva = _reservaFactory.CriarReserva(reservaDto.SalaId, reservaDto.UsuarioId, reservaDto.DataHoraInicio, reservaDto.DataHoraFim);

            var existeConflito = await _unitOfWork.ReservaRepository.VerificarConflitoReservaAsync(reserva.SalaId, reservaDto.UsuarioId, reserva.DataHoraInicio, reserva.DataHoraFim);

            if (existeConflito)
                throw new InvalidOperationException("Já existe uma reserva para a sala dentro desse período");


            var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(reserva.UsuarioId);

            var sala = await _unitOfWork.SalaRepository.GetByIdAsync(reserva.SalaId);

            await _unitOfWork.ReservaRepository.AdicionarAsync(reserva);

            await _unitOfWork.CommitAsync();

          await _emailService.EnviarEmailConfirmacaoReservaAsync(
                     usuario.Email!,
                     usuario.Nome!,
                     sala.Nome!,
                     reserva.DataHoraInicio,
                     reserva.DataHoraFim
            );


            return _mapper.Map<ReservaDTO>(reserva);
        }

        public async Task<ReservaDTO> EditarReservaAsync(int? id, ReservaDTO reserva)
        {
            var reservaExistente = await _unitOfWork.ReservaRepository.ObterPorIdAsync(id);

            if (reservaExistente == null)
                throw new InvalidOperationException("Reserva não encontrada");

            var existeConflito = await _unitOfWork.ReservaRepository.VerificarConflitoReservaAsync(reserva.SalaId, reserva.UsuarioId, reserva.DataHoraInicio, reserva.DataHoraFim);

            if (existeConflito)
                throw new InvalidOperationException("Já existe uma reserva para a sala no mesmo horário");

            var reservaEntity = _mapper.Map<Reserva>(reserva);
            await _unitOfWork.ReservaRepository.AtualizarAsync(reservaEntity);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<ReservaDTO>(reservaEntity);
        }

        public async Task<ReservaDTO> GetReservaAsync(int? id)
        {
            var reserva = await _unitOfWork.ReservaRepository.ObterPorIdAsync(id);

            var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(reserva.UsuarioId);

            var sala = await _unitOfWork.SalaRepository.GetByIdAsync(reserva.SalaId);

            if (reserva == null)
                throw new InvalidOperationException("Reserva não encontrada");


            var reservaDetalhada = new ReservaDTO()
            {
                Id = reserva.Id,
                UsuarioId = reserva.UsuarioId,
                Usuario = usuario,
                SalaId = reserva.SalaId,
                Sala = sala,
                DataHoraInicio = reserva.DataHoraInicio,
                DataHoraFim = reserva.DataHoraFim,
                Status = reserva.Status
            };


            return reservaDetalhada;
        }

        public async Task<IEnumerable<ReservaDTO>> GetReservasAsync()
        {
            var reservas = await _unitOfWork.ReservaRepository.ObterTodasReservasAsync();
            return _mapper.Map<List<ReservaDTO>>(reservas);
        }
    }
}
