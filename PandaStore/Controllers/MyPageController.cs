using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PandaStore.Data;
using PandaStore.Models;

namespace PandaStore.Controllers
{
    public class MyPageController : Controller
    {
        private readonly PandaStoreContext context;

        private readonly UserManager<PandaUser> userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyPageController(PandaStoreContext context, UserManager<PandaUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }





        // GET: MyPageController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MyPageController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MyPageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyPageController/Create
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

        // GET: MyPageController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MyPageController/Edit/5
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

        // GET: MyPageController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MyPageController/Delete/5
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
