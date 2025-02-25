using GerenciadorReservas.Application.Validations;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorReservas.Application.DTOs
{
    public class ReservaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecione um usuário.")]
        [Display(Name = "Nome do Usuário")]
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        [Required(ErrorMessage = "Selecione uma sala.")]
        [Display(Name = "Nome da Sala")]
        public int SalaId { get; set; }
        public Sala? Sala { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data e Hora de Início")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm}")]
        [MinHoursBeforeReservation(24)]
        public DateTime DataHoraInicio { get; set; }

        [Required(ErrorMessage = "A data de término é obrigatória.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data e Hora de Fim")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm}")]
        public DateTime DataHoraFim { get; set; }

        public StatusReserva Status { get; set; }

    }
}
