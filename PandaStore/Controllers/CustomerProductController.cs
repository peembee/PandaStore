using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Nest;
using PandaStore.Data;
using PandaStore.Models;
using System.Text.Json;

namespace PandaStore.Controllers
{
    
    public class CustomerProductController : Controller
    {
        private const string ShoppingCartSessionKey = "ShoppingCart";

        private readonly PandaStoreContext context;

        private readonly UserManager<PandaUser> userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerProductController(PandaStoreContext context, UserManager<PandaUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }


        // GET: CustomerProductController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var shoppingCart = await GetShoppingCart();


            foreach (var item in shoppingCart)
            {
                var product = context.Products.FirstOrDefault(p => p.ProductID == item.FK_ProductID);
                if (product != null)
                {
                    item.ProductName = product.ProductTitel;
                }
            }
            return View(shoppingCart);
        }


        public async Task<List<CustomerProduct>> GetShoppingCart()
        {
            // Returnera hela kundkorgen
            var shoppingCart = HttpContext.Session.GetObject<List<CustomerProduct>>(ShoppingCartSessionKey);
            if (shoppingCart == null)
            {
                shoppingCart = new List<CustomerProduct>();
                SaveShoppingCart(shoppingCart);
            }

            return shoppingCart;
        }


        protected async Task AddToCart(CustomerProduct product)
        {
            // Lägg till produkten i kundkorgen
            var shoppingCart = await GetShoppingCart();
            shoppingCart.Add(product);
            SaveShoppingCart(shoppingCart);
        }


        public async Task<IActionResult> AddAutoProducts(CustomerProduct product)
        {
            Random rand = new Random();

            CustomerProduct tests = new CustomerProduct()
            {
                Id = rand.Next(0, 100).ToString(),
                FK_ProductID = rand.Next(1, 4),
                Quantity = rand.Next(1, 5),
                Price = rand.Next(0, 100),
            };

            await AddToCart(tests);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            // Ta bort produkten från kundkorgen
            var shoppingCart = await GetShoppingCart();
            var product = shoppingCart.FirstOrDefault(p => p.FK_ProductID == id);
            if (product != null)
            {
                shoppingCart.Remove(product);
                SaveShoppingCart(shoppingCart);
            }

            return RedirectToAction("Index");

        }

        private void SaveShoppingCart(List<CustomerProduct> shoppingCart)
        {
            HttpContext.Session.SetObject(ShoppingCartSessionKey, shoppingCart);
        }


        // Payment-handler
        [Authorize]
        public async Task<IActionResult> CheckOut()
        {
            Random rand = new Random();
            string collectReceiptNumber = "";
            bool isUnique = true;
            int receiptNumber;

            var shoppingCart = await GetShoppingCart();

            /// Getting unique receipt-number.
            do
            {
                for (int i = 0; i < 6; i++)
                {
                    int randomNumber = rand.Next(1, 10);
                    collectReceiptNumber += randomNumber.ToString();
                }

                foreach (var item in shoppingCart)
                {
                    if (collectReceiptNumber == item.ReceiptNumber.ToString())
                    {
                        isUnique = false;
                        break;
                    }
                }

            } while (isUnique == false);

            receiptNumber = Convert.ToInt32(collectReceiptNumber);


            ///insert the receipt-number in the shoppingchart
            foreach (var item in shoppingCart)
            {
                var product = context.Products.FirstOrDefault(p => p.ProductID == item.FK_ProductID);
                if (product != null)
                {
                    item.ProductName = product.ProductTitel;
                    item.ReceiptNumber = receiptNumber;
                }
            }
            SaveShoppingCart(shoppingCart);
            return View(shoppingCart);
        }
        

        public async Task<IActionResult> sendOrder(int receiptNumber)
        {
            // Hämta användarens UserId
            string userId = userManager.GetUserId(User);

            var addShoppingCartToDb = await GetShoppingCart();
   

            Receipt receipt = new Receipt();
            receipt.ReceiptNumber = receiptNumber;
            receipt.PaymentSucceeded = true;

            context.Receipts.Add(receipt); // adding receipt to database

          
            foreach (var item in addShoppingCartToDb)
            {
                item.Id = userManager.GetUserId(User);
                context.CustomerProducts.Add(item);
            }
            SaveShoppingCart(addShoppingCartToDb);

            await context.SaveChangesAsync();
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }








        ////////Not in use for the moment
        protected void UpdateCart(CustomerProduct product)
        {
            // Uppdatera kundkorgen
            // ... kod för att uppdatera kundkorgen baserat på produkten
        }
    }

    // Konfigurera session
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
