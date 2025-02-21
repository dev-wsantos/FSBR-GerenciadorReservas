
using GerenciadorReservas.Domain.Validation;

namespace GerenciadorReservas.Domain.Entities
{
    public sealed class Sala : BaseEntity
    {
        public string? Nome { get; private set; }
        public int Capacidade { get; private set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

        public Sala() { }
        public Sala(string nome, int capacidade)
        {
            ValidateDomain(nome, capacidade);
        }

        private void ValidateDomain(string nome, int capacidade)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(nome), "Nome inválido. O Nome é obrigatório.");

            DomainExceptionValidation.When(capacidade <= 0, "Capacidade da sala é inválida. A Capacidade da sala deve ser maior que 0.");


            Nome = nome;
            
            Capacidade = capacidade;
            
        }
    }
}