using System.ComponentModel.DataAnnotations;

namespace GerenciadorReservas.Application.DTOs
{
    public class SalaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome é obrigatório")]
        [MaxLength(100)]
        public string? Nome { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Capacidade da sala é inválida. A Capacidade da sala deve ser maior que 0.")]
        public int Capacidade { get; set; }
    }
}
