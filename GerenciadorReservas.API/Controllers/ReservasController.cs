using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Application.Interfaces;
using GerenciadorReservas.Application.Services;
using Microsoft.AspNetCore.Http;
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
            var reservas = await _reservaService.GetReservas();
            return Ok(reservas);
        }
        [HttpGet("ObterReserva/{id}")]
        public async Task<ActionResult<ReservaDTO>> ObterReserva(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var reserva = await _reservaService.GetReserva(id);
            if (reserva == null)
            {
                return NotFound();
            }
            return Ok(reserva);
        }
        [HttpPost("AdicionarReserva")]
        public async Task<ActionResult<ReservaDTO>> AdicionarReserva(ReservaDTO reserva)
        {
            if (reserva == null)
            {
                return BadRequest();
            }
            await _reservaService.Add(reserva);
            return CreatedAtAction("GetReserva", new { id = reserva.Id }, reserva);
        }
        [HttpPut("AtualizarReserva/{id}")]
        public async Task<ActionResult<ReservaDTO>> AtualizarUsuario(int id, ReservaDTO reserva)
        {
            if (id != reserva.Id)
            {
                return BadRequest();
            }
            await _reservaService.Update(reserva);

            return Ok(reserva);
        }
        [HttpDelete("RemoverReserva/{id}")]
        public async Task<ActionResult<ReservaDTO>> RemoverReserva(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var reserva = await _reservaService.GetReserva(id);
            if (reserva == null)
            {
                return NotFound();
            }
            await _reservaService.Remove(id);
            return Ok(reserva);
        }
    }
}
