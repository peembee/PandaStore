using Microsoft.AspNetCore.Mvc;
using PandaStore.Data;
using PandaStore.Models;
using PandaStore.Repository;
using System.Diagnostics;



namespace PandaStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository<User> _pandaRepository;

        public HomeController(PandaStoreContext context)
        {
            _pandaRepository = new Repository<User>(context);
        }

        public async Task <IActionResult> Index()
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