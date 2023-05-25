using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandaStore.Models;

namespace PandaStore.Controllers
{
    public class CustomerProductController : Controller
    {
        private List<CustomerProduct> shoppingCart { get; set; } = new List<CustomerProduct>();

     




        protected void AddToCart(CustomerProduct product)
        {
            // Lägg till produkten i kundkorgen
            shoppingCart.Add(product);
        }

        protected void UpdateCart(CustomerProduct product)
        {
            // Uppdatera kundkorgen
            // ... kod för att uppdatera kundkorgen baserat på produkten
        }

        protected void RemoveFromCart(CustomerProduct product)
        {
            // Ta bort produkten från kundkorgen
            shoppingCart.Remove(product);
        }

        protected List<CustomerProduct> GetShoppingCart()
        {
            // Returnera hela kundkorgen
            return shoppingCart;
        }













        // GET: CustomerProductController
        public ActionResult Index()
        {
            return View(GetShoppingCart());
        }



        public IActionResult AddAutoProducts(CustomerProduct product)
        {
            Random rand = new Random();

            CustomerProduct tests = new CustomerProduct()
            {
                Id = rand.Next(0, 100).ToString(),
                FK_ProductID = rand.Next(0, 100),
                Quantity = rand.Next(0, 100),
                Price = rand.Next(0, 100),
            };

            AddToCart(tests);

            return RedirectToAction("Index");
        }


































        // GET: CustomerProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
