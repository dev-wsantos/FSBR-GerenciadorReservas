using System.ComponentModel.DataAnnotations;

namespace GerenciadorReservas.Application.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório")]
        [MinLength(5)]
        [MaxLength(100)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O E-mail é obrigatório")]
        [MaxLength(150)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "O e-mail informado é inválido")]
        public string? Email { get; set; }
    }
}
