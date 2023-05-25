using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using PandaStore.Models;
using System.Text.Json;

namespace PandaStore.Controllers
{
    [Authorize]
    public class CustomerProductController : Controller
    {
        private const string ShoppingCartSessionKey = "ShoppingCart";

        // GET: CustomerProductController
        public ActionResult Index()
        {
            var shoppingCart = GetShoppingCart();
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
                FK_ProductID = rand.Next(0, 100),
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

        protected ActionResult RemoveFromCart(CustomerProduct product)
        {
            // Ta bort produkten från kundkorgen
            var shoppingCart = GetShoppingCart();
            shoppingCart.Remove(product);
            SaveShoppingCart(shoppingCart);
            return View("Index", shoppingCart);
        }       

        private void SaveShoppingCart(List<CustomerProduct> shoppingCart)
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
