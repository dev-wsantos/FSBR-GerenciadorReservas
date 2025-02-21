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
            DateTime dataHora = DateTime.Now.AddHours(25); 

            var reserva = new Reserva(usuarioId, salaId, dataHora);

            reserva.UsuarioId.Should().Be(usuarioId);
            reserva.SalaId.Should().Be(salaId);
            reserva.DataHora.Should().Be(dataHora);
            reserva.Status.Should().Be(StatusReserva.Confirmada);
        }

        [Fact]
        public void CriarReserva_ComUsuarioIdInvalido_DeveLancarExcecaoUsuarioIdInvalido()
        {
           
            int usuarioId = 0; 
            int salaId = 1;
            DateTime dataHora = DateTime.Now.AddHours(24);

            Action act = () => new Reserva(usuarioId, salaId, dataHora);

            act.Should().Throw<DomainExceptionValidation>()
               .WithMessage("Id do usuário inválido. O Id do usuário deve ser maior que 0");
        }

       
        [Fact]
        public void CancelarReserva_ComMaisDeVinteQuatroHoras_DeveCancelarReservaComSucesso()
        {
            
            int usuarioId = 1;
            int salaId = 1;
            DateTime dataHora = DateTime.Now.AddHours(25);

            var reserva = new Reserva(usuarioId, salaId, dataHora);

            reserva.Cancelar();

            reserva.Status.Should().Be(StatusReserva.Cancelada);
        }

        [Fact]
        public void CancelarReserva_ComMenosDeVinteQuatroHoras_NaoDeveCancelarReservaComMenosDeVinteQuatroHoras()
        {

            int usuarioId = 1;
            int salaId = 1;
            DateTime dataHora = DateTime.Now.AddHours(23);

            Action act = () => new Reserva(usuarioId, salaId, dataHora);

            act.Should().Throw<DomainExceptionValidation>()
               .WithMessage("Não é possível fazer uma reserva com menos de 24 horas de antecedência.");
        }
    }
}
