using Microsoft.AspNetCore.Mvc;

namespace PandaStore.Controllers
{
	public class ProductPageController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
