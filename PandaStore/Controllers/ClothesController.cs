using Microsoft.AspNetCore.Mvc;
using PandaStore.Data;
using PandaStore.Models;
using PandaStore.Repository;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace PandaStore.Controllers
{
    public class ClothesController : Controller
    {
        private readonly PandaStoreContext _context;
        public ClothesController(PandaStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> ClothesProducts()
        {
            var products = await _context.Products
                .Include(c => c.Campaigns)
                .Include(p => p.Categorys)
                .Where(p => p.FK_CategoryID == 3)
                .ToListAsync();

            return View(products);
        }
    }
}
