using Microsoft.AspNetCore.Mvc;

namespace projetoExemplo.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
