using GerenciadorReservas.Domain.Validation;
using System.Text.RegularExpressions;

namespace GerenciadorReservas.Domain.Entities
{
    public sealed class Usuario : BaseEntity
    {
        public int Id { get; private set; }
        public string? Nome { get; private set; }
        public string? Email { get; private set; }

        
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

        public Usuario(string nome, string email)
        {
            ValidateDomain(nome, email);
        }

        public void Update(string nome, string email)
        {
            ValidateDomain(nome, email);
        }

        private void ValidateDomain(string nome, string email)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(nome), "Nome inválido. O Nome é obrigatório");
            
            DomainExceptionValidation.When(nome.Length < 5, "Nome inválido. O Nome deve ter no mínimo 5 caracteres");

            ValidarEmail(email);

            Nome = nome;

            Email = email;
        }

        private void ValidarEmail(string email)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(email), "O e-mail é obrigatório.");

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            bool emailValido = Regex.IsMatch(email, emailPattern);

            DomainExceptionValidation.When(!emailValido, "O e-mail informado é inválido.");
        }
    }
}