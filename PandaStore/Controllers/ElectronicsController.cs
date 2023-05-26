using Microsoft.AspNetCore.Mvc;

namespace PandaStore.Controllers
{
    public class ElectronicsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
