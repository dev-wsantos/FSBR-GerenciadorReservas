using GerenciadorReservas.Domain.Entities;

namespace GerenciadorReservas.UI.Web.Models
{
    public class ReservaViewModel
    {
        public int Id { get; set; }
        public Usuario? Usuario { get; set; }
        public Sala? Sala { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }

        public int StatusReservaId { get; set; }
        public string StatusReserva
        {
            get
            {
                return StatusReservaId switch
                {
                    1 => "Confirmada",
                    2 => "Cancelada",
                    _ => "Desconhecido"
                };
            }
        }

        public string NomeUsuario => Usuario?.Nome!;
        public string NomeSala => Sala?.Nome!;
    }
}
