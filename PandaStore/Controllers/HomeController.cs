using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PandaStore.Data;
using PandaStore.Models;
using PandaStore.Repository;
using System.Diagnostics;


namespace PandaStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository<PandaUser> _pandaRepository;
        public HomeController(PandaStoreContext context)
        {
            _pandaRepository = new Repository<PandaUser>(context);
        }

        public IActionResult Index()
        {
 
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}