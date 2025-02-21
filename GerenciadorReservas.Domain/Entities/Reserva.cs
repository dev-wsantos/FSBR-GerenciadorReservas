using GerenciadorReservas.Domain.Enums;
using GerenciadorReservas.Domain.Validation;

namespace GerenciadorReservas.Domain.Entities
{
    public sealed class Reserva : BaseEntity
    {
        public int UsuarioId { get; private set; }
        public Usuario? Usuario { get; set; }

        public int SalaId { get; private set; }
        public Sala? Sala { get; private set; }

        public DateTime DataHora { get; private set; }
        public StatusReserva Status { get; private set; }

        public Reserva(int usuarioId, int salaId, DateTime dataHora)
        {
            ValidationDomain(usuarioId, salaId, dataHora);
        }

        private void ValidationDomain(int usuarioId, int salaId, DateTime dataHora)
        {
            DomainExceptionValidation.When(usuarioId <= 0, "Id do usuário inválido. O Id do usuário deve ser maior que 0");
            
            DomainExceptionValidation.When(salaId <= 0, "Id da sala inválido. O Id da sala deve ser maior que 0");

            DomainExceptionValidation.When(dataHora < DateTime.Now, "Não é possível fazer uma reserva para um horário passado.");

            
            UsuarioId = usuarioId;
            SalaId = salaId;
            DataHora = dataHora;
            Status = StatusReserva.Confirmada;
            
        }

        public void Cancelar()
        {
            double horasRestantes = (DataHora - DateTime.Now).TotalHours;

            DomainExceptionValidation.When(horasRestantes < 24,
                "Uma reserva só pode ser cancelada com no mínimo 24 horas de antecedência.");

            Status = StatusReserva.Cancelada;
        }
    }
}
