using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaStore.Data;

namespace PandaStore.Controllers
{
    public class FoodController : Controller
    {
        private readonly PandaStoreContext _context;
        public FoodController(PandaStoreContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Details ()
        {
            var products = await _context.Products
                .Include(c => c.Campaigns)
                .Include(p => p.Categorys)
                .Where(p => p.FK_CategoryID == 6)
                .ToListAsync();

            return View(products);
        }
    }
}

