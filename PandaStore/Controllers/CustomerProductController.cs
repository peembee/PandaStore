using Microsoft.AspNetCore.Mvc;
using PandaStore.Models;

namespace PandaStore.Controllers
{
    public class CustomerProductController : BaseController
    {
        public ActionResult Create(string userId, int productId, int quantity, double price)
        {
            CustomerProduct newItem = new CustomerProduct
            {
                Id = userId,
                FK_ProductID = productId,
                Quantity = quantity,
                Price = price
            };

            shoppingCartList.Clear();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DisplayShoppingCart()
        {
            return View("ShoppingCart");
        }
    }
}
