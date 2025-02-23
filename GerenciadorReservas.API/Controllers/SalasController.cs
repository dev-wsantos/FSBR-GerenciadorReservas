using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorReservas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private readonly ISalaService _salaService;

        public SalasController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        [HttpGet("ListarSalas")]
        public async Task<IEnumerable<SalaDTO>> ListarSalas()
        {
            var salas = await _salaService.GetSalas();
            
            return salas;
        }

        [HttpGet("ObterSala/{id}")]
        public async Task<SalaDTO> ObterSala(int? id)
        {
            var sala = await _salaService.GetSala(id);

            return sala;
        }

        [HttpPost("AdicionarSala")]
        public async Task<ActionResult<SalaDTO>> AdicionarSala(SalaDTO sala)
        {
            await _salaService.Add(sala);
            return CreatedAtAction("ObterSala", new { id = sala.Id }, sala);
        }

        [HttpPut("AtualizarSala/{id}")]
        public async Task<ActionResult<SalaDTO>> AtualizarSala(int id, SalaDTO sala)
        {
            if (id != sala.Id)
            {
                return BadRequest();
            }
            await _salaService.Update(sala);
            
            return Ok(sala);
        }
        [HttpDelete("RemoverSala/{id}")]
        public async Task<ActionResult<SalaDTO>> RemoveSala(int id)
        {
            var sala = await _salaService.GetSala(id);
            if (sala == null)
            {
                return NotFound();
            }
            await _salaService.Remove(id);
            return Ok(sala);
        }
    }
}
