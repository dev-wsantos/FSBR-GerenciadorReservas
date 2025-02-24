using Moq;
using Xunit;
using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Application.Services;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorReservas.Application.Interfaces;
using GerenciadorReservas.Domain.Enums;

namespace GerenciadorReservas.Tests
{
    public class ReservaServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IReservaFactory> _mockReservaFactory;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly ReservaService _reservaService;

        public ReservaServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockReservaFactory = new Mock<IReservaFactory>();
            _mockEmailService = new Mock<IEmailService>();
            _reservaService = new ReservaService(
                _mockUnitOfWork.Object,
                _mockMapper.Object,
                _mockReservaFactory.Object,
                _mockEmailService.Object);
        }

        [Fact]
        public async Task CriarReservaAsync_DeveCriarReservaComSucesso()
        {
            // Arrange
            var reservaDto = new ReservaDTO
            {
                SalaId = 1,
                UsuarioId = 1,
                DataHoraInicio = DateTime.Now.AddHours(1),
                DataHoraFim = DateTime.Now.AddHours(2)
            };

            var reserva = new Reserva
            {
                SalaId = reservaDto.SalaId,
                UsuarioId = reservaDto.UsuarioId,
                DataHoraInicio = reservaDto.DataHoraInicio,
                DataHoraFim = reservaDto.DataHoraFim
            };

            _mockReservaFactory.Setup(x => x.CriarReserva(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(reserva);

            _mockUnitOfWork.Setup(x => x.ReservaRepository.VerificarConflitoReservaAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(false);

            _mockUnitOfWork.Setup(x => x.ReservaRepository.AdicionarAsync(It.IsAny<Reserva>()));
            _mockUnitOfWork.Setup(x => x.CommitAsync());

            _mockMapper.Setup(x => x.Map<ReservaDTO>(It.IsAny<Reserva>())).Returns(reservaDto);

            // Act
            var resultado = await _reservaService.CriarReservaAsync(reservaDto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(reservaDto.SalaId, resultado.SalaId);
            Assert.Equal(reservaDto.UsuarioId, resultado.UsuarioId);
            Assert.Equal(reservaDto.DataHoraInicio, resultado.DataHoraInicio);
            Assert.Equal(reservaDto.DataHoraFim, resultado.DataHoraFim);
            _mockEmailService.Verify(x => x.EnviarEmailConfirmacaoReservaAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task CancelarReservaAsync_DeveCancelarReservaComSucesso()
        {
            // Arrange
            var reservaId = 1;
            var reserva = new Reserva
            {
                Id = reservaId,
                SalaId = 1,
                UsuarioId = 1,
                DataHoraInicio = DateTime.Now.AddHours(1),
                DataHoraFim = DateTime.Now.AddHours(2)
            };

            var usuario = new Usuario { Email = "usuario@test.com", Nome = "Usuario Teste" };
            var sala = new Sala { Nome = "Sala Teste" };

            _mockUnitOfWork.Setup(x => x.ReservaRepository.ObterPorIdAsync(reservaId)).ReturnsAsync(reserva);
            _mockUnitOfWork.Setup(x => x.ReservaRepository.AtualizarAsync(It.IsAny<Reserva>()));
            _mockUnitOfWork.Setup(x => x.CommitAsync());
            _mockUnitOfWork.Setup(x => x.UsuarioRepository.GetByIdAsync(reserva.UsuarioId)).ReturnsAsync(usuario);
            _mockUnitOfWork.Setup(x => x.SalaRepository.GetByIdAsync(reserva.SalaId)).ReturnsAsync(sala);

            _mockMapper.Setup(x => x.Map<ReservaDTO>(It.IsAny<Reserva>())).Returns(new ReservaDTO());

            // Act
            var resultado = await _reservaService.CancelarReservaAsync(reservaId);

            // Assert
            Assert.NotNull(resultado);
            _mockEmailService.Verify(x => x.EnviarConfirmacaoCancelamentoAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task GetReservaAsync_DeveRetornarReservaComSucesso()
        {
            // Arrange
            var reservaId = 1;
            var reserva = new Reserva
            (
                1,
                1,
                DateTime.Now.AddHours(1),
                DateTime.Now.AddHours(2),
                StatusReserva.Confirmada
            );

            _mockUnitOfWork.Setup(x => x.ReservaRepository.ObterPorIdAsync(reservaId)).ReturnsAsync(reserva);
            _mockMapper.Setup(x => x.Map<ReservaDTO>(It.IsAny<Reserva>())).Returns(new ReservaDTO());

            // Act
            var resultado = await _reservaService.GetReservaAsync(reservaId);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(reservaId, resultado.Id);
        }

        [Fact]
        public async Task GetReservasAsync_DeveRetornarListaDeReservas()
        {
            // Arrange
            var reservas = new List<Reserva>
            {
                new Reserva { Id = 1, SalaId = 1, UsuarioId = 1, DataHoraInicio = DateTime.Now.AddHours(1), DataHoraFim = DateTime.Now.AddHours(2) }
            };

            _mockUnitOfWork.Setup(x => x.ReservaRepository.ObterTodasReservasAsync()).ReturnsAsync(reservas);
            _mockMapper.Setup(x => x.Map<List<ReservaDTO>>(It.IsAny<List<Reserva>>())).Returns(new List<ReservaDTO>());

            // Act
            var resultado = await _reservaService.GetReservasAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Single(resultado);
        }
    }
}
