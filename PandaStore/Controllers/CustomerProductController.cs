using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Nest;
using PandaStore.Data;
using PandaStore.Models;
using System.Text.Json;

namespace PandaStore.Controllers
{
    [Authorize]
    public class CustomerProductController : Controller
    {
        private const string ShoppingCartSessionKey = "ShoppingCart";

        private readonly PandaStoreContext context;
        public CustomerProductController(PandaStoreContext context)
        {
            this.context = context;
        }

        // GET: CustomerProductController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var shoppingCart = GetShoppingCart();


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
        protected List<CustomerProduct> GetShoppingCart()
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
            var shoppingCart = GetShoppingCart();
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
                Quantity = rand.Next(0, 100),
                Price = rand.Next(0, 100),
            };

            await AddToCart(tests);

            return RedirectToAction("Index");
        }

        protected void UpdateCart(CustomerProduct product)
        {
            // Uppdatera kundkorgen
            // ... kod för att uppdatera kundkorgen baserat på produkten
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            // Ta bort produkten från kundkorgen
            var shoppingCart = GetShoppingCart();
            var product = shoppingCart.FirstOrDefault(p => p.CustomerProductID == id);
            if (product != null)
            {
                shoppingCart.Remove(product);
                await SaveShoppingCart(shoppingCart);
            }

            return RedirectToAction("Index");
        }

        private async Task SaveShoppingCart(List<CustomerProduct> shoppingCart)
        {
            HttpContext.Session.SetObject(ShoppingCartSessionKey, shoppingCart);
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
