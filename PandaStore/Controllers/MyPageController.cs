using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandaStore.Data;
using PandaStore.Models;

namespace PandaStore.Controllers
{
    [Authorize]
    public class MyPageController : Controller
    {
        private readonly PandaStoreContext context;

        private readonly UserManager<PandaUser> userManager;


        public MyPageController(PandaStoreContext context, UserManager<PandaUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        // GET: MyPageController
        public async Task<IActionResult> Index()
        {
            string userId = userManager.GetUserId(User);

            // Getting all users orders into a list.
            var orderList = await context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.PandaUsers)
                .Where(o => o.OrderDetails.Any(od => od.Id == userId && od.FK_OrderID == o.OrderID))
                .ToListAsync();

            return View(orderList);
        }

        // GET: MyPageController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var reciptId = await context.OrderDetails
                .Where(r => r.FK_OrderID == id)
                .Select(r => r.FK_ReceiptID).FirstOrDefaultAsync();

            var reciptNumber = await context.Receipts.Where(r => r.ReceiptID == reciptId)
                .Select(r => r.ReceiptNumber).FirstOrDefaultAsync();

            var reciptDetails = await context.CustomerProducts
                .Where(r => r.ReceiptNumber == reciptNumber).ToListAsync();

            foreach (var item in reciptDetails)
            {
                var product = context.Products.FirstOrDefault(p => p.ProductID == item.FK_ProductID);
                if (product != null)
                {
                    item.ProductName = product.ProductTitel;
                }
            }


            return View(reciptDetails);
        } 
    }
}
