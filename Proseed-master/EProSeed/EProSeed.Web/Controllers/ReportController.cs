using EProSeed.Lib.BLL;
using EProSeed.Lib.BLL.Contracts;
using EProSeed.Lib.BLL.Repository;
using EProSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EProSeed.Web.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        protected readonly IBatch BatchRepo;
        protected readonly IReport ReportRepo;
        protected readonly IInductee IndcuteeRepo;

        public ReportController()
        {
            BatchRepo = new Batch();
            ReportRepo = new Report();
            IndcuteeRepo = new Inductee();
        }

        public ActionResult Index()
        {
            var batchList = BatchRepo.GetAll();
            ViewBag.Batch = new SelectList(batchList, "Id", "Name");
            var inductees = new List<InducteeModel>();
            ViewBag.Inductees = new SelectList(inductees, "Id", "Name");
            return View("Report");
        }

        [HttpPost]
        public JsonResult GetIndcutees(int batchId)
        {
            var inductees = new List<InducteeModel>() { new InducteeModel() { Id = -1, Name = "All" } };
            if (batchId > 0)
                inductees.AddRange(IndcuteeRepo.InducteesByBatch(batchId));
            return Json(inductees.Select(n => new { Id = n.Id, Name = n.Name }).ToList() , JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ViewReport(int batchId, int inducteeId)
        {
            ReportModel report = ReportRepo.GetReport(batchId, inducteeId);
            return PartialView("_ReportPartial", report);
        }
    }
}
