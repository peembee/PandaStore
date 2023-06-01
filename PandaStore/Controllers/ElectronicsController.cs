using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using Nest;
using PandaStore.Data;

namespace PandaStore.Controllers
{
    public class ElectronicsController : Controller
    {
        private readonly PandaStoreContext _context;
        public ElectronicsController(PandaStoreContext context)
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
                .Where(p => p.FK_CategoryID == 1)
                .ToListAsync();

            foreach (var productItem in products) 
            {
                var campaign = await _context.Campaigns.ToListAsync();
                foreach(var campaignItem in campaign)
                { 
                    if(productItem.ProductID == campaignItem.FK_ProductID) 
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
                .Where(p => p.FK_CategoryID == 1)
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
            //var product = _context.Products.Find(id);

            //if (product == null)
            //{
            //    return NotFound();
            //}

            //return View(product);
        }
    }
}
