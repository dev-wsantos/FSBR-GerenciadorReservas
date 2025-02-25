using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Application.Interfaces;
using GerenciadorReservas.Domain.Validation;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorReservas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaService _reservaService;
        public ReservasController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpGet("ListarReservas")]
        public async Task<ActionResult<IEnumerable<ReservaDTO>>> ListarReservas()
        {
            var reservas = await _reservaService.GetReservasAsync();

            return Ok(reservas);
        }


        [HttpGet("ObterReserva/{id}")]
        public async Task<ActionResult<ReservaDTO>> ObterReserva(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var reserva = await _reservaService.GetReservaAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            return Ok(reserva);
        }


        [HttpPost("AdicionarReserva")]
        public async Task<ActionResult<ReservaDTO>> AdicionarReserva(ReservaDTO reserva)
        {

            try
            {
                await _reservaService.CriarReservaAsync(reserva);
                return Ok(new { mensagem = "Reserva criada com sucesso!" });
            }
            catch (DomainExceptionValidation ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor. Por favor, tente novamente." });
            }
        }



        [HttpPut("EditarReserva/{id}")]
        public async Task<ActionResult> EditarReserva(int id, ReservaDTO reserva)
        {
            if (id != reserva.Id)
            {
                return BadRequest();
            }
            var resultado = await _reservaService.EditarReservaAsync(id, reserva);
            if (resultado is null)
                return NotFound();

            return NoContent(); 
        }

        [HttpPatch("CancelarReserva/{id}")]
        public async Task<ActionResult> CancelarReserva(int id)
        {
            var resultado = await _reservaService.CancelarReservaAsync(id);
            if (resultado is null)
                return NotFound();
            
            return NoContent();  
        }
    }
}
