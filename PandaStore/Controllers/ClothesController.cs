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
            foreach (var productItem in products)
            {
                var campaign = await _context.Campaigns.ToListAsync();
                foreach (var campaignItem in campaign)
                {
                    if (productItem.ProductID == campaignItem.FK_ProductID)
                    {
                        if (campaignItem.StartDate.Date <= DateTime.Now.Date && campaignItem.EndDate.Date >= DateTime.Now.Date)
                        {
                            productItem.OriginalPrice = productItem.Price;
                            productItem.Price = productItem.Price - (productItem.Price * (campaignItem.Discount / 100));
                        }
                    }
                }
            }
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> ProductDetails(int id)
        {

            var product = await _context.Products
                .Include(c => c.Campaigns)
                .Include(p => p.Categorys)
                .Where(p => p.FK_CategoryID == 3)
                .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            var campaign = await _context.Campaigns.ToListAsync();
            foreach (var campaignItem in campaign)
            {
                if (product.ProductID == campaignItem.FK_ProductID)
                {
                    if (campaignItem.StartDate.Date <= DateTime.Now.Date && campaignItem.EndDate.Date >= DateTime.Now.Date)
                    {
                        product.OriginalPrice = product.Price;
                        product.Price = product.Price - (product.Price * (campaignItem.Discount / 100));
                    }
                }
            }
            return View(product);
        }
    }
}
