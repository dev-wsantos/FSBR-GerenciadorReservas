using FluentAssertions;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Validation;

namespace GerenciadorReservas.Tests
{
    public class SalaUnitTest
    {
        [Fact]
        public void CriarSala_ComParametrosValidos_DeveCriarSalaComSucesso()
        {
           
            string nome = "Sala de Reuniões";
            int capacidade = 10;

            var sala = new Sala(nome, capacidade);

            sala.Nome.Should().Be(nome);
            sala.Capacidade.Should().Be(capacidade);
        }

        [Fact]
        public void CriarSala_ComNomeInvalido_DeveLancarExcecaoNomeInvalido()
        {

            string nome = ""; 
            int capacidade = 10;

            Action act = () => new Sala(nome, capacidade);

            act.Should().Throw<DomainExceptionValidation>()
               .WithMessage("Nome inválido. O Nome é obrigatório.");
        }

        [Fact]
        public void CriarSala_CapacidadeZero_DeveLancarExcecaoCapacidadeInvalida()
        {
            
            string nome = "Sala de Reuniões";
            int capacidade = 0; 

            Action act = () => new Sala(nome, capacidade);

            act.Should().Throw<DomainExceptionValidation>()
               .WithMessage("Capacidade da sala é inválida. A Capacidade da sala deve ser maior que 0.");
        }


        [Fact]
        public void CriarSala_CapacidadeNegativa_NaoDeveAceitarCapacidadeNegativa()
        {

            string nome = "Sala de Reuniões";

            int capacidade = -1; 

            Action act = () => new Sala(nome, capacidade);

            act.Should().Throw<DomainExceptionValidation>()
               .WithMessage("Capacidade da sala é inválida. A Capacidade da sala deve ser maior que 0.");
        }
    }
}
