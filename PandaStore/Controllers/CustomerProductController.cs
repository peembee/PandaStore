using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PandaStore.Controllers
{
    public class CustomerProductController : BaseController
    {
        // GET: CustomerProductController
        public ActionResult Index()
        {
            return View(GetShoppingCart());
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
