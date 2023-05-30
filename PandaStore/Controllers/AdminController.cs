using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using PandaStore.Data;
using PandaStore.Models;

namespace PandaStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<PandaUser> userManager;
        private readonly PandaStoreContext context;
        public AdminController(PandaStoreContext context, UserManager<PandaUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchOrder()
        {
            return View();
        }
        public async Task<IActionResult> getOrders(string search = "")
        {
            search = search.Trim();
            search = search.ToLower();
            if (search == "")
            {
                var orderList = await context.OrderDetails
                    .Include(o => o.Orders)
                    .Include(r => r.Receipts)
                    .Include(o => o.OrderStatuses)
                    .Include(p => p.PandaUsers)
                    .ToListAsync();
                return View("SearchOrder", orderList);
            }
            else
            {
                var orderList = await context.OrderDetails
                    .Include(o => o.Orders)
                    .Include(r => r.Receipts)
                    .Include(o => o.OrderStatuses)
                    .Include(p => p.PandaUsers)
                    .Where(p => p.PandaUsers.Email.ToLower().Contains(search) ||
                        p.PandaUsers.FirstName.ToLower().Contains(search) ||
                        p.PandaUsers.LastName.ToLower().Contains(search) ||
                        p.PandaUsers.PhoneNumber.Contains(search) ||
                        search == p.PandaUsers.FirstName.TrimStart() + " " + p.PandaUsers.LastName.TrimEnd()                        
                    )
                    .ToListAsync();

                return View("SearchOrder", orderList);
            }            
        }


        public async Task<IActionResult> Details(int receiptNumber)
        {
            string userId = userManager.GetUserId(User);

            // Hämta alla ordrar för användaren inom det specificerade datumintervallet

            var receipt = await context.CustomerProducts
                .Where(r => r.ReceiptNumber == receiptNumber)
                .Include(p => p.Products)                
                .ToListAsync();

            return View(receipt);
        }

    }
}
