using FluentAssertions;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Validation;

namespace GerenciadorReservas.Tests
{
    public class UsuarioUnitTest
    {
        [Fact(DisplayName = "Criar Usuário com Parâmetros válidos")]
        public void CriarUsuario_ComParametrosValidos_ResultadoObjetoComEstadoValido()
        {
            Action action = () => new Usuario("Wellington Santos", "wellington.bezerra.santos@outlook.com");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Criar Usuário com nome nulo")]
        public void CriarUsuario_ComNomeNulo_LancarExcecaoNomeObrigatorio()
        {
            Action action = () => new Usuario(null, "wellington.bezerra.santos@outlook.com");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage($"Nome inválido. O Nome é obrigatório.");
        }

        [Fact(DisplayName = "Criar Usuário com nome vazio")]
        public void CriarUsuario_ComNomeVazio_LancarExcecaoNomeObrigatorio()
        {
            Action action = () => new Usuario("", "wellington.bezerra.santos@outlook.com");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage($"Nome inválido. O Nome é obrigatório.");
        }

        [Fact(DisplayName = "Create Usuário com nome maior do que 5 caracteres.")]
        public void CriarUsuario_ComNomeCurto_LancarExcecaoNomeCurto()
        {
            Action action = () => new Usuario("Well", "wellington.bezerra.santos@outlook.com");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage($"Nome inválido. O Nome deve ter no mínimo 5 caracteres.");
        }
        [Fact(DisplayName = "Criar Usuário com e-mail nulo")]
        public void CriarUsuario_ComEmailNulo_LancarExcecaoEmailObrigatorio()
        {
            Action action = () => new Usuario("Wellington Santos", null);
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage($"O e-mail é obrigatório.");
        }
        [Fact(DisplayName = "Criar Usuário com e-mail vazio")]
        public void CriarUsuario_ComEmailVazio_LancarExcecaoEmailObrigatorio()
        {
            Action action = () => new Usuario("Wellington Santos", "");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage($"O e-mail é obrigatório.");
        }

        [Fact(DisplayName = "Criar Usuário com e-mail inválido")]
        public void CriarUsuario_ComEmailInvalido_LancarExcecaoEmailInvalido()
        {
            Action action = () => new Usuario("Wellington Santos", "well.gmail.com");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage($"O e-mail informado é inválido.");
        }
    }
}
