using Microsoft.AspNetCore.Mvc;

namespace PandaStore.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
