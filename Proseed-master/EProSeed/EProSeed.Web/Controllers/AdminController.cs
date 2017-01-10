using System.Web.Mvc;
using EProSeed.Lib.BLL;
using EProSeed.Models;
using EProSeed.Web.Models;

namespace EProSeed.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        protected readonly IInductee inducteeRepo;
        protected readonly ITrainer trainerRepo;

        public AdminController()
        {
            inducteeRepo = new Inductee();
            trainerRepo = new Trainer();
        }

        public AdminController(IInductee inducteeRepo, ITrainer trainerRepo)
        {
            this.inducteeRepo = inducteeRepo;
            this.trainerRepo = trainerRepo;
        }

        //
        // GET: /Admin/
        public ActionResult Index()
        {
            vmAdmin adminViewModel = new vmAdmin(inducteeRepo, trainerRepo);
            return View(adminViewModel);
        }
              
        //
        // GET: /Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
