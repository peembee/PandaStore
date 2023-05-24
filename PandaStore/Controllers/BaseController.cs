using Microsoft.AspNetCore.Mvc;
using PandaStore.Models;

namespace PandaStore.Controllers
{
    public class BaseController : Controller
    {
        protected List<CustomerProduct> shoppingCartList = new List<CustomerProduct>();
    }
}
