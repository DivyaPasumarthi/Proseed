using EProSeed.Lib.BLL;
using EProSeed.Lib.BLL.Repository;
using EProSeed.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EProSeed.Web.Controllers
{
    [Authorize]
    public class TrainerFeedbackController : Controller
    {
        protected readonly ITrainerFeedback TrainerFeedbackRepo;
        protected readonly IBatch BatchRepo;
        protected readonly IInductee _Inductee;
        public TrainerFeedbackController()
        {
            TrainerFeedbackRepo = new TrainerFeedback();
            BatchRepo = new Batch();
            _Inductee = new Inductee();
        }
        public TrainerFeedbackController(ITrainerFeedback trainerFeedbackRepo, IBatch batchRepo)
        {
            TrainerFeedbackRepo = trainerFeedbackRepo;
            BatchRepo = batchRepo;
        }
        // GET: /Inductee/
        public ActionResult Index()
        {
            string currentUserId = Session["UserId"] == null ? string.Empty : Session["UserId"].ToString();
            var userType = Session["UserType"] == null ? UserType.None.ToString() : Session["UserType"].ToString();
            IList<BatchModel> batchList = new List<BatchModel>();
            if (userType == UserType.Trainer.ToString())
            {
                batchList = BatchRepo.GetAll();
            }
            else if (userType == UserType.Trainee.ToString())
            {
                batchList.Add(BatchRepo.GetBatchDetailsByTraineeId(currentUserId));
            }
            return View(batchList);
        }

        public ActionResult TrainerFeedback(int id)
        {
            string currentUserId = Session["UserId"] == null ? string.Empty : Session["UserId"].ToString();
            var userType = Session["UserType"] == null ? UserType.None.ToString() : Session["UserType"].ToString();
            var trainerFeedbackList = new CustomTrainerFeedbackModel();
            string currentUserMailId = Session["UserEmailId"] == null ? string.Empty : Session["UserEmailId"].ToString();
            var batchId = _Inductee.FindByEmail(currentUserMailId);
            if (userType == UserType.Trainer.ToString())
            {
                trainerFeedbackList = TrainerFeedbackRepo.GetTrainerFeedbackList(id);
            }
            else if (userType == UserType.Trainee.ToString())
            {
                trainerFeedbackList = TrainerFeedbackRepo.GetTrainerFeedbackListForTrainer(batchId.BatchID, currentUserId);
            }

            TempData["CurrentBatchID"] = id;
            return View("TrainerFeedback", trainerFeedbackList);
        }

        public ActionResult Create()
        {
            TrainersFeedbackModel model = new TrainersFeedbackModel();
            string currentUserMailId = Session["UserEmailId"] == null ? string.Empty : Session["UserEmailId"].ToString();
            var batchId = _Inductee.FindByEmail(currentUserMailId);
            if (batchId==null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                model.BatchID = batchId.BatchID;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Create(TrainersFeedbackModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.DateCreated = DateTime.Now;
                    model.TraineeID = Session["UserId"] == null ? 0 : Convert.ToInt32(Session["UserId"].ToString());

                    if (TrainerFeedbackRepo.Create(model) == true)
                    {
                        ViewData["SuccessMsg"] = "Feedback for trainer created successfully.";
                    }
                    else
                    {
                        ViewData["ErrorMsg"] = "Failed to create feedback for trainer.";
                    }
                }
            }
            catch (Exception)
            {
                ViewData["ErrorMsg"] = "Failed to create feedback for trainer.";
            }
            return RedirectToAction("TrainerFeedback", new { id = model.BatchID });
        }


        public ActionResult Edit(int id)
        {
            var trainerFeedback = TrainerFeedbackRepo.GetTrainerFeedback(id);
            return View(trainerFeedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Edit(TrainersFeedbackModel model)
        {
            bool response = false;
            if (ModelState.IsValid)
            {
                try
                {
                    response = TrainerFeedbackRepo.Update(model);
                    if (response == true)
                    {
                        ViewData["SuccessMsg"] = "Trainer's feedback updated successfully.";
                    }
                    else
                    {
                        ViewData["ErrorMsg"] = "Failed to udpate trainer's feedback.";
                    }
                }
                catch
                {
                    //something went wrong! error
                }
            }
            return RedirectToAction("TrainerFeedback", new { id = model.BatchID });
        }

        public ActionResult Delete(int? id)
        {
            if (id == 0)
                return HttpNotFound();

            if (TrainerFeedbackRepo.Delete(id) == true)
            {
                ViewData["SuccessMsg"] = "Trainer's feedback deleted successfully.";
            }
            else
            {
                ViewData["ErrorMsg"] = "Failed to delete trainer's feedback.";
            }

            return RedirectToAction("TrainerFeedback", new { id = (int)TempData.Peek("CurrentBatchID") });
        }
    }
}
