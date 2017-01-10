using EProSeed.Lib.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EProSeed.Web.Models;
using EProSeed.Models;


namespace EProSeed.Web.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {

        protected readonly IFeedback FeedbackRepo;
        protected readonly ITrainer TrainerRepo;
        protected readonly IInductee InducteeRepo;

        public FeedbackController()
        {
            FeedbackRepo = new Feedback();
            InducteeRepo = new Inductee();
            TrainerRepo = new Trainer();
        }


        //
        // GET: /Feedback/of/<inducteeID>

        public ActionResult Of(int? id)
        {
            var Inductee = InducteeRepo.Find(id);
            return View(Inductee);
        }

        #region Create feedback


        public ActionResult Create(int? id)
        {
            vmFeedBack objFeedback;
            if (id == null)
                return HttpNotFound();
            objFeedback = FillFeedBackViewModel(id);

            return View(objFeedback);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Create(vmFeedBack feedback)
        {
           FillFeedBackViewModel(feedback.InducteeID);
            if (ModelState.IsValid)
            {
                FeedbackModel dbFeedback = new FeedbackModel();
                PropertyModel Property = new PropertyModel();
                if (feedback.Date != null)
                {
                    var Feedback = Convert.ToDateTime(feedback.Date);
                    dbFeedback.FeedbackDate = Feedback;
                }
                else
                {
                    dbFeedback.FeedbackDate = DateTime.Now;
                }

                dbFeedback.InducteeID = feedback.InducteeID;
                dbFeedback.TrainerId = Convert.ToInt32(feedback.TrainerID);
                dbFeedback.InducteeID = feedback.InducteeID;

                Property.CommitmentComment = feedback.Property.CommitmentComment;
                Property.CommitmentRating = feedback.Property.CommitmentRating;
                Property.CommunicationComment = feedback.Property.CommunicationComment;
                Property.CommunicationRating = feedback.Property.CommunicationRating;
                Property.DisciplineComment = feedback.Property.DisciplineComment;
                Property.DisciplineRating = feedback.Property.DisciplineRating;
                Property.EnergyComment = feedback.Property.EnergyComment;
                Property.EnergyRating = feedback.Property.EnergyRating;

                Property.FocusOnQualityComment = feedback.Property.FocusOnQualityComment;
                Property.FocusOnQualityRating = feedback.Property.FocusOnQualityRating;

                Property.OwnerShipComment = feedback.Property.OwnerShipComment;
                Property.OwnerShipRating = feedback.Property.OwnerShipRating;

                Property.PassionForClientSuccessComment = feedback.Property.PassionForClientSuccessComment;
                Property.PassionForClientSuccessRating = feedback.Property.PassionForClientSuccessRating;
                Property.TeamPlayerComment = feedback.Property.TeamPlayerComment;
                Property.TeamPlayerRating = feedback.Property.TeamPlayerRating;
                Property.TechnicalCompetencyComment = feedback.Property.TechnicalCompetencyComment;
                Property.TechnicalCompetencyRating = feedback.Property.TechnicalCompetencyRating;
                Property.TransparencyComment = feedback.Property.TransparencyComment;
                Property.TransparencyRating = feedback.Property.TransparencyRating;
                dbFeedback.Property = Property;

                if (FeedbackRepo.Create(dbFeedback) == true)
                {
                    ViewData["SuccessMsg"] = "Feedback saved successfully.";
                }
                else
                {
                    ViewData["ErrorMsg"] = "Failed to save feedback.";
                }
            }
            return Redirect("/feedback/of/" + feedback.InducteeID);
        }

        #endregion


        #region Edit Feedback

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();

            var Feedback = FeedbackRepo.Find(id);

            vmFeedbackProperty objFeedbackProperty = new vmFeedbackProperty();

            var Inductee = InducteeRepo.Find(Feedback.InducteeID);

            objFeedbackProperty.TrainerName = Inductee.Batch.trainer.Name;
            objFeedbackProperty.BatchName = Inductee.Batch.Name;
            objFeedbackProperty.InducteeID = Inductee.Id;
            objFeedbackProperty.InducteeName = Inductee.Name;
            var feedbacks = FeedbackRepo.GetFeedbacksForTrainee(Inductee.Id);
            var datesWithFeedback = feedbacks.Select(p => p.FeedbackDate).ToList();
            var allDates = Inductee.Batch.BatchDates.Select((p, i) => new { date = p.BatchDate, Text = "Day " + (i + 1) + " - " + p.BatchDate.ToString("dd MMM yyyy - ddd") }).ToList();
            objFeedbackProperty.BatchDates = allDates.Where(p => p.date == Feedback.FeedbackDate || !datesWithFeedback.Contains(p.date)).Select((p, i) => new SelectListItem
            { Value = p.date.ToString("MM/dd/yyyy"), Text = p.Text }).ToList();

            objFeedbackProperty.BatchID = Inductee.BatchID;
            objFeedbackProperty.TrainerID = Inductee.Batch.TrainerId;
            objFeedbackProperty.FeedBackID = Feedback.ID;
            objFeedbackProperty.Date = Feedback.FeedbackDate.ToString("MM/dd/yyyy");
            objFeedbackProperty.Property = Feedback.Property;


            return View(objFeedbackProperty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(true)]
        public ActionResult Edit(vmFeedbackProperty feedbackProperty)
        {

            if (ModelState.IsValid)
            {
                var Feedback = FeedbackRepo.Find(feedbackProperty.FeedBackID);

                PropertyModel Property = Feedback.Property;

                Property.CommitmentComment = feedbackProperty.Property.CommitmentComment;
                Property.CommitmentRating = feedbackProperty.Property.CommitmentRating;
                Property.CommunicationComment = feedbackProperty.Property.CommunicationComment;
                Property.CommunicationRating = feedbackProperty.Property.CommunicationRating;
                Property.DisciplineComment = feedbackProperty.Property.DisciplineComment;
                Property.DisciplineRating = feedbackProperty.Property.DisciplineRating;
                Property.EnergyComment = feedbackProperty.Property.EnergyComment;
                Property.EnergyRating = feedbackProperty.Property.EnergyRating;

                Property.FocusOnQualityComment = feedbackProperty.Property.FocusOnQualityComment;
                Property.FocusOnQualityRating = feedbackProperty.Property.FocusOnQualityRating;

                Property.OwnerShipComment = feedbackProperty.Property.OwnerShipComment;
                Property.OwnerShipRating = feedbackProperty.Property.OwnerShipRating;

                Property.PassionForClientSuccessComment = feedbackProperty.Property.PassionForClientSuccessComment;
                Property.PassionForClientSuccessRating = feedbackProperty.Property.PassionForClientSuccessRating;
                Property.TeamPlayerComment = feedbackProperty.Property.TeamPlayerComment;
                Property.TeamPlayerRating = feedbackProperty.Property.TeamPlayerRating;
                Property.TechnicalCompetencyComment = feedbackProperty.Property.TechnicalCompetencyComment;
                Property.TechnicalCompetencyRating = feedbackProperty.Property.TechnicalCompetencyRating;
                Property.TransparencyComment = feedbackProperty.Property.TransparencyComment;
                Property.TransparencyRating = feedbackProperty.Property.TransparencyRating;

                if (FeedbackRepo.UpdateProperty(Property, feedbackProperty.Date, feedbackProperty.FeedBackID) == true)
                {
                    ViewData["SuccessMsg"] = "Feedback updated successfully.";
                }
                else
                {
                    ViewData["ErrorMsg"] = "Failed to update feedback.";
                }
            }
            return Redirect("/feedback/of/" + feedbackProperty.InducteeID);
        }

        #endregion


        public ActionResult Delete(int? id, int? InducteeId)
        {
            if (id == null)
                return HttpNotFound();

         FeedbackRepo.Delete(id);
            return Redirect("/feedback/of/" + InducteeId);
        }


        private vmFeedBack FillFeedBackViewModel(int? InducteeID)
        {
            vmFeedBack objFeedback = new vmFeedBack();
            var Inductee = InducteeRepo.Find(InducteeID);
            objFeedback.TrainerName = Inductee.Batch.trainer.Name;
            objFeedback.BatchName = Inductee.Batch.Name;
            objFeedback.InducteeName = Inductee.Name;
            objFeedback.BatchID = Inductee.BatchID;
            objFeedback.TrainerID = Inductee.Batch.TrainerId;
            objFeedback.InducteeID = Inductee.Id;
            var feedbacks = FeedbackRepo.GetFeedbacksForTrainee(objFeedback.InducteeID);
            var datesWithFeedback = feedbacks.Select(p => p.FeedbackDate).ToList();
            var allDates = Inductee.Batch.BatchDates.Select((p, i) => new { date = p.BatchDate, Text = "Day " + (i + 1) + " - " + p.BatchDate.ToString("dd MMM yyyy - ddd") }).Where(p => !datesWithFeedback.Contains(p.date)).ToList();
            objFeedback.BatchDates = allDates.Select((p, i) => new SelectListItem
            { Value = p.date.ToString("MM/dd/yyyy"), Text = p.Text }).ToList();
            return objFeedback;
        }







    }
}
