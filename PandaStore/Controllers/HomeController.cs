using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PandaStore.Data;
using PandaStore.Models;
using PandaStore.Repository;
using System.Diagnostics;


namespace PandaStore.Controllers
{
    public class HomeController : BaseController
    {
        private readonly Repository<PandaUser> _pandaRepository;
        public HomeController(PandaStoreContext context)
        {
            _pandaRepository = new Repository<PandaUser>(context);
            CustomerProduct newItem = new CustomerProduct
            {
                Id = "2",
                FK_ProductID = 3,
                Quantity = 4,
                Price = 5
            };
            shoppingCartList.Add(newItem);
        }

        public IActionResult Index()
        {
 
            return View("Index", shoppingCartList);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}