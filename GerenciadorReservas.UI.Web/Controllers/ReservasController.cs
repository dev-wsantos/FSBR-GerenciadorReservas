using GerenciadorReservas.UI.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorReservas.UI.Web.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ReservasService _reservasService;

        public ReservasController(ReservasService reservasService)
        {
            _reservasService = reservasService;
        }

        public async Task<IActionResult> Index()
        {
            var reservas = await _reservasService.ObterReservasAsync();
            return View(reservas);
        }
    }
}
