using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.CodeAnalysis;
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
            int CartItemCount = shoppingCart.Count;
            foreach (var item in shoppingCart)
            {
                var product = context.Products.FirstOrDefault(p => p.ProductID == item.FK_ProductID);
                if (product != null)
                {
                    item.ProductName = product.ProductTitel;
                }
            }
            ViewBag.CartItemCount = CartItemCount; // Lägg till detta för att skicka antalet till vyn
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


        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity, double price)
        {
            bool existedProduct = false;
            var shoppingCart = await GetShoppingCart();
            foreach (var item in shoppingCart)
            {
                if (item.FK_ProductID == productId)
                {
                    // Uppdatera kundkorgen
                    item.Quantity += quantity;
                    existedProduct = true;
                    break;
                }
            }

            if (existedProduct == false)
            {
                // Lägg till produkten i kundkorgen
                CustomerProduct product = new CustomerProduct()
                {
                    FK_ProductID = productId,
                    Quantity = quantity,
                    Price = price,
                };
                shoppingCart.Add(product);
            }

            SaveShoppingCart(shoppingCart);

            // Uppdatera produktens lagersaldo
            await UpdateProductQuantity(productId, quantity);

            return RedirectToAction("Index");
        }

        private async Task UpdateProductQuantity(int productId, int quantity)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.ProductID == productId);
            if (product != null)
            {
                product.InventoryQuantity -= quantity;
                await context.SaveChangesAsync();
            }
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            // Ta bort produkten från kundkorgen
            var shoppingCart = await GetShoppingCart();
            var product = shoppingCart.FirstOrDefault(p => p.FK_ProductID == id);
            if (product != null)
            {
                // Öka kvantiteten för att återställa rätt antal i lagersaldot
                var increaseProduct = await context.Products
                        .FirstOrDefaultAsync(p => p.ProductID == id);
                if (product != null)
                {
                    increaseProduct.InventoryQuantity += product.Quantity;
                    await context.SaveChangesAsync();
                }

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
            double totalOrderPrice = 0;
            var addShoppingCartToDb = await GetShoppingCart();

            // adding receipt to database
            Receipt receipt = new Receipt();
            receipt.ReceiptNumber = receiptNumber;
            receipt.PaymentSucceeded = true;
            context.Receipts.Add(receipt);

            foreach (var item in addShoppingCartToDb)
            {
                item.Id = userManager.GetUserId(User);
                totalOrderPrice += item.Price * item.Quantity;
                context.CustomerProducts.Add(item);
            }
            SaveShoppingCart(addShoppingCartToDb);

            await context.SaveChangesAsync();

            await createOrder(totalOrderPrice, receiptNumber);

            _httpContextAccessor.HttpContext.Session.Clear();

            return RedirectToAction(nameof(Index));
        }

        private async Task createOrder(double totalOrderPrice, int receiptNumber)
        {
            // adding order to database
            Order order = new Order();
            order.OrderTotalPrice = totalOrderPrice;
            context.Orders.Add(order);

            await context.SaveChangesAsync();

            await createOrderStatuses(receiptNumber);
        }


        private async Task createOrderStatuses(int receiptNumber)
        {
            // adding orderStatus to database
            Random rand = new Random();

            OrderStatus orderStatus = new OrderStatus();
            orderStatus.StatusTitel = "Beställt";
            orderStatus.Shipped = rand.Next(2) == 0;
            if (orderStatus.Shipped == true) // randomize shipped
            {
                orderStatus.Delivered = rand.Next(2) == 0;
            }
            else
            {
                orderStatus.Delivered = false;
            }
            context.OrderStatuses.Add(orderStatus);

            await context.SaveChangesAsync();

            await createOrderDetails(receiptNumber);
        }
        private async Task createOrderDetails(int receiptNumber)
        {
            // adding orderDetails to database

            OrderDetail orderDetails = new OrderDetail();

            var orderId = await context.Orders.OrderByDescending(o => o.OrderID).Select(o => o.OrderID).FirstOrDefaultAsync();

            var orderstatusId = await context.OrderStatuses.OrderByDescending(o => o.OrderStatusID).Select(o => o.OrderStatusID).FirstOrDefaultAsync();

            var reciptId = await context.Receipts.Where(r => r.ReceiptNumber == receiptNumber).Select(r => r.ReceiptID).FirstOrDefaultAsync();

            orderDetails.FK_OrderID = orderId;
            orderDetails.FK_OrderStatus = orderstatusId;
            orderDetails.FK_ReceiptID = reciptId;
            orderDetails.Id = userManager.GetUserId(User);

            context.OrderDetails.Add(orderDetails);

            await context.SaveChangesAsync();
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
