using GerenciadorReservas.UI.Web.Models;
using GerenciadorReservas.UI.Web.Services;
using GerenciadorReservas.UI.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            //var reservas = await _reservasService.ObterReservasAsync();
            //return View(reservas);

    
            var reservas = await _reservasService.ObterReservasAsync();

            // Para cada reserva, preenche o objeto Usuario e Sala
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
            var usuarios = await _usuariosService.GetUsuariosAsync(); // Exemplo de chamada
            var salas = await _salasService.GetSalasAsync(); // Exemplo de chamada

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

            //if (!ModelState.IsValid)
            //{
            //    // Exibe os erros de validação para depuração
            //    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //    {
            //        Console.WriteLine(error.ErrorMessage); // Ou use um logger
            //    }

            //    return View(reserva);
            //}



            if (ModelState.IsValid)
            {
                await _reservasService.CreateReservaAsync(reserva);
                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }
    }
}
