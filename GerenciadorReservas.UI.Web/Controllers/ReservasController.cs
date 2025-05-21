using GerenciadorReservas.UI.Web.Models;
using GerenciadorReservas.UI.Web.Services;
using GerenciadorReservas.UI.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace GerenciadorReservas.UI.Web.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ReservasService _reservasService;
        private readonly UsuarioService _usuariosService;
        private readonly SalaService _salasService;


        public ReservasController(ReservasService reservasService, UsuarioService usuarioService, SalaService salaService)
        {
            _reservasService = reservasService;
            _usuariosService = usuarioService;
            _salasService = salaService;
        }

        public async Task<IActionResult> Index()
        {
                
            var reservas = await _reservasService.ObterReservasAsync();

            foreach (var reserva in reservas)
            {
                reserva.Usuario = await _usuariosService.GetUsuarioByIdAsync(reserva.UsuarioId);
                reserva.Sala = await _salasService.GetSalaByIdAsync(reserva.SalaId);
            }

            return View(reservas);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var usuarios = await _usuariosService.GetUsuariosAsync(); 
            var salas = await _salasService.GetSalasAsync(); 

            var viewModel = new ReservaViewModel
            {
                Usuarios = usuarios.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Nome
                }).ToList(),
                Salas = salas.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Nome
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReservaViewModel reserva)
        {
            try
            {
                var success = await _reservasService.CreateReservaAsync(reserva);
             
                if (success)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            var usuarios = await _usuariosService.GetUsuariosAsync(); 
            var salas = await _salasService.GetSalasAsync(); 



            return View(reserva);

        }
    }
}
