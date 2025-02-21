using GerenciadorReservas.Domain.Validation;

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

            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(email), "Email inválido. O Email é obrigatório");

            DomainExceptionValidation.When(email.Length < 10, "Email inválido. O Email deve ter no mínimo 10 caracteres");

            Nome = nome;

            Email = email;
        }
    }
}