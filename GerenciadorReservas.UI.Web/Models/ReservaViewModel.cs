using GerenciadorReservas.Application.Validations;
using GerenciadorReservas.Domain.Entities;
using GerenciadorReservas.Domain.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorReservas.UI.Web.Models
{
    public class ReservaViewModel
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
        public DateTime? DataHoraInicio { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "A data de término é obrigatória.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data e Hora de Fim")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm}")]

        public DateTime DataHoraFim { get; set; } = DateTime.Now.AddHours(1);


       
        public StatusReserva Status { get; set; }

        
        // Listas para o DropDownList
        public List<SelectListItem> Usuarios { get; set; } = new();
        public List<SelectListItem> Salas { get; set; } = new();

        //public ReservaViewModel()
        //{
        //    DataHoraInicio = DateTime.Now;
        //    DataHoraFim = DateTime.Now.AddHours(1); 
        //}
    }
}
