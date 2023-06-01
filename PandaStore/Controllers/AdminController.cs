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


        public async Task<IActionResult> AddAdmin()
        {
            return View();
        }

        public async Task<IActionResult> getAdmins(string search = "")
        {
            string userId = userManager.GetUserId(User);
            search = search.Trim();
            search = search.ToLower();
            if (search == "")
            {
                var orderList = await context.PandaUsers.ToListAsync();
                return View("AddAdmin", orderList);
            }
            else
            {
                var pandaUsers = await context.PandaUsers
                    .Where(p => p.Email.ToLower().Contains(search) ||
                        p.FirstName.ToLower().Contains(search) ||
                        p.LastName.ToLower().Contains(search) ||
                        p.PhoneNumber.Contains(search) ||
                        search == p.FirstName.TrimStart() + " " + p.LastName.TrimEnd()
                    )
                    .ToListAsync();

                return View("AddAdmin", pandaUsers);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetAdmin(string id, bool isAdmin)
        {
            var user = await context.PandaUsers.FirstOrDefaultAsync(p => p.Id == id);

            if (user != null)
            {
                user.isAdmin = isAdmin;
                await context.SaveChangesAsync();
            }
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
