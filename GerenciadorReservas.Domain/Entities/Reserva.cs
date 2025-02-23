using GerenciadorReservas.Domain.Enums;
using GerenciadorReservas.Domain.Validation;

namespace GerenciadorReservas.Domain.Entities
{
    public sealed class Reserva : BaseEntity
    {
        public int UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }
        public int SalaId { get; private set; }
        public Sala? Sala { get; private set; }
        public DateTime DataHora { get; private set; }
        public StatusReserva Status { get; private set; }
        public string? TokenConfirmacao { get; private set; }
     

       
        private Reserva() { } 
       
        public Reserva(int salaId, int usuarioId, DateTime dataHora)
        {
            ValidateDomain(salaId, usuarioId, dataHora);

            UsuarioId = usuarioId;
            SalaId = salaId;
            DataHora = dataHora;
            
        }

        private static void ValidateDomain(int salaId, int usuarioId, DateTime dataHora)
        {
            DomainExceptionValidation.When(usuarioId <= 0, "Usuário inválido. O Usuário é obrigatório.");
            
            DomainExceptionValidation.When(salaId <= 0, "Sala inválida. A Sala é obrigatória.");
           
            DomainExceptionValidation.When(dataHora == DateTime.MinValue, "Data e hora inicial inválida. A Data e hora inicial é obrigatória.");

        }

        public void AtualizarStatus(StatusReserva status)
        {
            Status = status;
        }

        public void Cancelar()
        {
            if ((DataHora - DateTime.Now).TotalHours < 24)
            {
               throw new DomainExceptionValidation("Não é possível cancelar a reserva com menos de 24 horas de antecedência.");
            }

            Status = StatusReserva.Cancelada;
        }
    }
}
