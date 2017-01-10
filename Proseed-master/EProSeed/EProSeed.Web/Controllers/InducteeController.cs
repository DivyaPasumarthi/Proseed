using EProSeed.Lib.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EProSeed.Models;

namespace EProSeed.Web.Controllers
{
    [Authorize]
    public class InducteeController : Controller
    {
      
      

        protected readonly IInductee InducteeRepo;

        protected readonly IBatch BatchRepo;
        public InducteeController()
        {
            InducteeRepo = new Inductee();
            BatchRepo = new Batch();
        }
        // GET: /Inductee/
        public ActionResult Index()
        {
            var Inductees = InducteeRepo.Get();
            return View(Inductees);
        }


        public ActionResult Create()
        {
            var BatchList = BatchRepo.GetAll().OrderByDescending(b=>b.Id);
            ViewBag.BatchList =  new SelectList(BatchList, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Create(InducteeModel Inductee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (InducteeRepo.Create(Inductee))
                    {
                        TrainerModel trainerModel = new TrainerModel()
                        {
                            Email = Inductee.Email,
                            Name = Inductee.Name,
                            Password = System.Web.Security.Membership.GeneratePassword(8, 6)
                        };
                        int trainerid = new Trainer().Create(trainerModel);
                        var user_type_model = new Trainer_UserType_Map_Model()
                        {
                            Map_Trainer_Id = trainerid,
                            Map_UserType_Id = UserType.Trainee.GetHashCode()
                        };
                        new TrainerTraineeUserMapping().Create(user_type_model);
                        ViewData["SuccessMsg"] = "Trainee created successfully.";
                        return   Redirect("/Inductee");
                    }
                    else
                    {
                        ViewData["ErrorMsg"] = "Failed to create Trainee.";
                    }
                }
            }
            catch (Exception)
            {
                ViewData["ErrorMsg"] = "Failed to create Trainee.";
            }
            var BatchList = BatchRepo.GetAll().OrderByDescending(b => b.Id);
            ViewBag.BatchList = new SelectList(BatchList, "Id", "Name");
            return View(Inductee);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
                return HttpNotFound();
            var Inductee = InducteeRepo.Find(Id);
            if(Inductee == null)
                return HttpNotFound();

            //Get the batch list for display
            var BatchList = BatchRepo.GetAll().OrderByDescending(b => b.Id);
            ViewBag.BatchList = new SelectList(BatchList, "Id", "Name", Inductee.BatchID);
            return View(Inductee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Edit(InducteeModel Inductee)
        {
            if (ModelState.IsValid)
            {
                var res = InducteeRepo.Update(Inductee);
                if (res == true)
                {
                    ViewData["SuccessMsg"] = "Trainee updated successfully.";
                }
                else
                {
                    ViewData["ErrorMsg"] = "Failed to update inductee.";
                }
            }

            var BatchList = BatchRepo.GetAll().OrderByDescending(b => b.Id);
            ViewBag.BatchList = new SelectList(BatchList, "Id", "Name", Inductee.BatchID);
            return View(Inductee);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();

            if (InducteeRepo.Delete(id) == true)
            {
                ViewData["SuccessMsg"] = "Inductee deleted successfully.";
            }
            else
            {
                ViewData["ErrorMsg"] = "Failed to delete inductee.";
            }
            return Redirect("/Inductee");
        }

        [HttpPost]
        public ActionResult GetInducteesByBatchID(int? BatchID)
        {
            if (BatchID == null)
                return HttpNotFound();
            var Ind= InducteeRepo.InducteesByBatch(BatchID);
            return PartialView("InducteeByBatch", Ind);
        }
        
    }
}
