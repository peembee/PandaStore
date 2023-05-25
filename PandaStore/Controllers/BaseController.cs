using Microsoft.AspNetCore.Mvc;
using PandaStore.Models;

namespace PandaStore.Controllers
{
    public class BaseController : Controller
    {
        private List<CustomerProduct> shoppingCart { get; set; }

        public BaseController()
        {
            shoppingCart = new List<CustomerProduct>();
            CustomerProduct test = new CustomerProduct()
            {
                Id = "1",
                FK_ProductID = 1,
                Quantity = 1,
                Price = 1,
            };

            AddToCart(test);
        }

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

    }

}
