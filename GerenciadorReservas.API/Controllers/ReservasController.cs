﻿using GerenciadorReservas.Application.DTOs;
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
            if (reserva == null)
            {
                return BadRequest();
            }
            await _reservaService.CriarReservaAsync(reserva);
            return CreatedAtAction("ObterReserva", new { id = reserva.Id }, reserva);
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
            
            return NoContent();  // Retorna 204 No Content quando a reserva for cancelada com sucesso
        }
    }
}
