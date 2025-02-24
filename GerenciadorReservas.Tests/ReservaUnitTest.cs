using FluentAssertions;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Enums;
using GerenciadorReservas.Domain.Interfaces;
using GerenciadorReservas.Domain.Validation;

namespace GerenciadorReservas.Tests
{
    public class ReservaUnitTest
    {
        [Fact]
        public void CriarReserva_ComParametrosValidos_DeveCriarReservaComSucesso()
        {
            int usuarioId = 1;
            int salaId = 1;
            DateTime dataHoraInicio = DateTime.Now.AddHours(25);
            DateTime dataHoraFim = dataHoraInicio.AddHours(2);
            

            var reserva = new Reserva(salaId, usuarioId, dataHoraInicio, dataHoraFim, StatusReserva.Confirmada);

            reserva.UsuarioId.Should().Be(usuarioId);
            reserva.SalaId.Should().Be(salaId);
            reserva.DataHoraInicio.Should().Be(dataHoraInicio);
            reserva.DataHoraFim.Should().Be(dataHoraFim);
            reserva.Status.Should().Be(StatusReserva.Confirmada);
        }

        [Fact]
        public void CriarReserva_ComUsuarioIdInvalido_DeveLancarExcecaoUsuarioIdInvalido()
        {
            int usuarioId = 0;
            int salaId = 1;
            DateTime dataHoraInicio = DateTime.Now.AddHours(24);
            DateTime dataHoraFim = dataHoraInicio.AddHours(2);

            Action act = () => new Reserva(salaId, usuarioId, dataHoraInicio, dataHoraFim, StatusReserva.Confirmada);

            act.Should().Throw<DomainExceptionValidation>()
               .WithMessage("Usuário inválido. O Usuário é obrigatório.");
        }

        [Fact]
        public void CancelarReserva_ComMaisDeVinteQuatroHoras_DeveCancelarReservaComSucesso()
        {
            int usuarioId = 1;
            int salaId = 1;
            DateTime dataHoraInicio = DateTime.Now.AddHours(25);
            DateTime dataHoraFim = dataHoraInicio.AddHours(2);

            var reserva = new Reserva(salaId, usuarioId, dataHoraInicio, dataHoraFim, StatusReserva.Confirmada);

            reserva.Cancelar();

            reserva.Status.Should().Be(StatusReserva.Cancelada);
        }

        [Fact]
        public void CancelarReserva_ComMenosDeVinteQuatroHoras_DeveLancarExcecao()
        {
            int usuarioId = 1;
            int salaId = 1;
            DateTime dataHoraInicio = DateTime.Now.AddHours(23); // Menos de 24h de antecedência
            DateTime dataHoraFim = dataHoraInicio.AddHours(2);

            var reserva = new Reserva(salaId, usuarioId, dataHoraInicio, dataHoraFim, StatusReserva.Confirmada);

            Action act = () => reserva.Cancelar();

            act.Should().Throw<DomainExceptionValidation>()
               .WithMessage("Não é possível cancelar a reserva com menos de 24 horas de antecedência.");
        }

    }
}
