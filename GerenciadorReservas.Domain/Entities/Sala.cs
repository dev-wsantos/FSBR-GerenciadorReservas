
using GerenciadorReservas.Domain.Validation;

namespace GerenciadorReservas.Domain.Entities
{
    public sealed class Sala : BaseEntity
    {
        public string? Nome { get; private set; }
        public int Capacidade { get; private set; }
        public bool IsDisponivel { get; private set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

        public Sala(string nome, int capacidade, bool disponivel)
        {
            ValidateDomain(nome, capacidade, disponivel);
        }

        private void ValidateDomain(string nome, int capacidade, bool disponivel)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(nome), "Nome inválido. O Nome é obrigatório");

            DomainExceptionValidation.When(capacidade <= 0, "Capacidade inválida. A Capacidade deve ser maior que 0");

            DomainExceptionValidation.When(!disponivel, "A sala não pode ser reservada. Ela não está disponível.");
            
            Nome = nome;
            
            Capacidade = capacidade;
            
            IsDisponivel = disponivel;
        }
    }
}