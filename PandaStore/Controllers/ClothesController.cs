using Microsoft.AspNetCore.Mvc;
using PandaStore.Data;
using PandaStore.Models;
using PandaStore.Repository;
using System.Diagnostics;

namespace PandaStore.Controllers
{
    public class ClothesController : Controller
    {
        public IActionResult ClothesProducts()
        {
            return View();
        }
    }
}
