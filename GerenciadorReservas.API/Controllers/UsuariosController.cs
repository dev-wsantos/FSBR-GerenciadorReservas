using GerenciadorReservas.Application.DTOs;
using GerenciadorReservas.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorReservas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("ListarUsuarios")]
        public async Task<IEnumerable<UsuarioDTO>> ListarUsuarios()
        {
            var usuarios = await _usuarioService.GetUsuarios();

            return usuarios;

        }

        [HttpGet("ObterUsuario/{id}")]
        public async Task<UsuarioDTO> ObterUsuario(int? id)
        {
            var usuario = await _usuarioService.GetUsuario(id);
            return usuario;
        }

        [HttpPost("AdicionarUsuario")]
        public async Task<ActionResult<UsuarioDTO>> AdicionarUsuario(UsuarioDTO usuario)
        {
            await _usuarioService.Add(usuario);
            return CreatedAtAction("AdicionarUsuario", new { id = usuario.Id }, usuario);
        }

        [HttpPut("AtualizarUsuario/{id}")]
        public async Task<ActionResult<UsuarioDTO>> AtualizarUsuario(int id, UsuarioDTO usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }
            await _usuarioService.Update(usuario);
            
            return Ok(usuario);
        }

        [HttpDelete("RemoverUsuario/{id}")]
        public async Task<ActionResult<UsuarioDTO>> RemoverUsuario(int id)
        {
            var usuario = await _usuarioService.GetUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }
            await _usuarioService.Remove(id);
            return usuario;
        }
    }
}
