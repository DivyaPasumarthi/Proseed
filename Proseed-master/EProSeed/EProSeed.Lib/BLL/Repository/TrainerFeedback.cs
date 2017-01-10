using System.Collections.Generic;
using System.Linq;
using EProSeed.Models;
using EProSeed.DAL;
using System;
using System.Data.Entity;

namespace EProSeed.Lib.BLL.Repository
{
    public class TrainerFeedback : ITrainerFeedback
    {
        protected readonly ProDbContext db;
        protected readonly IInductee _Inductee;
        public TrainerFeedback()
        {
            db = new ProDbContext();
            _Inductee = new Inductee();
        }

        public bool Create(TrainersFeedbackModel model)
        {
            db.TrainersFeedback.Add(model);
            return db.SaveChanges() > 0;
        }

        public bool Update(TrainersFeedbackModel model)
        {
            if (model != null)
            {
                db.Entry(model).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public bool Delete(int? id)
        {
            if (id == 0)
                throw new Exception("Select valid Inductee");

            var feedback = db.TrainersFeedback.Find(id);
            if (feedback != null)
            {
                db.TrainersFeedback.Remove(feedback);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public TrainersFeedbackModel GetTrainerFeedback(int Id)
        {
            return db.TrainersFeedback.Find(Id);
        }

        public CustomTrainerFeedbackModel GetTrainerFeedbackList(int batchId)
        {
            var customTrainerFeedback = new CustomTrainerFeedbackModel();
            var feedbackList = db.TrainersFeedback.Where(f => f.BatchID == batchId).OrderByDescending(d => d.DateCreated);
            var batchDetail = db.Batch.Find(batchId);
            var traineeList = db.Tranner.ToList();
            customTrainerFeedback.BatchID = batchDetail.Id;
            customTrainerFeedback.BatchName = batchDetail.Name;
            customTrainerFeedback.BatchStartDate = batchDetail.BatchDates.OrderBy(p => p.BatchDate).Select(p => p.BatchDate).FirstOrDefault();
            customTrainerFeedback.BatchEndDate = batchDetail.BatchDates.OrderBy(p => p.BatchDate).Select(p => p.BatchDate).LastOrDefault();
            customTrainerFeedback.TrainerID = batchDetail.trainer.Id;
            customTrainerFeedback.TrainerName = batchDetail.trainer.Name;
            customTrainerFeedback.TrainerEmail = batchDetail.trainer.Email;

            var feedbackReponseList = new List<FeedbackResponse>();

            if (feedbackList.Count() > 0) //if records exist
            {
                foreach (var feedback in feedbackList)
                {
                    var feedbackReponse = new FeedbackResponse();
                    feedbackReponse.ID = feedback.ID;
                    feedbackReponse.Rating = feedback.Rating;
                    feedbackReponse.WhatWentWell = feedback.WhatWentWell;
                    feedbackReponse.DidnotGoWell = feedback.DidnotGoWell;
                    feedbackReponse.CanBeImproved = feedback.CanBeImproved;
                    feedbackReponse.TraineeID = feedback.TraineeID;
                    feedbackReponse.TraineeName = traineeList.Find(tr => tr.Id == feedback.TraineeID).Name;

                    feedbackReponseList.Add(feedbackReponse);
                }
            }
            customTrainerFeedback.FeedbackReponse = feedbackReponseList;

            return customTrainerFeedback;
        }

        public CustomTrainerFeedbackModel GetTrainerFeedbackListForTrainer(int batchId, string traineeId)
        {
            var customTrainerFeedback = new CustomTrainerFeedbackModel();

            var feedbackList = db.TrainersFeedback.Where(f => f.BatchID == batchId && f.TraineeID.ToString() == traineeId).OrderByDescending(d => d.DateCreated).ToList();
            int traineeIntId = Convert.ToInt32(traineeId);
            var user = db.Tranner.FirstOrDefault(us => us.Id == traineeIntId);

            var inducteeBatchId = _Inductee.Get(user.Email).BatchID;
            var traineesBatch = db.Batch.Where(B => B.Id == inducteeBatchId).Select(B => B).FirstOrDefault();

            customTrainerFeedback.BatchID = traineesBatch.Id;
            customTrainerFeedback.BatchName = traineesBatch.Name;
            customTrainerFeedback.BatchStartDate = traineesBatch.BatchDates.OrderBy(p => p.BatchDate).Select(p => p.BatchDate).FirstOrDefault();
            customTrainerFeedback.BatchEndDate = traineesBatch.BatchDates.OrderBy(p => p.BatchDate).Select(p => p.BatchDate).LastOrDefault();
            customTrainerFeedback.TrainerID = traineesBatch.trainer.Id;
            customTrainerFeedback.TrainerName = traineesBatch.trainer.Name;
            customTrainerFeedback.TrainerEmail = traineesBatch.trainer.Email;

            var feedbackReponseList = new List<FeedbackResponse>();

            if (feedbackList.Any()) //if records exist
            {
                foreach (var feedback in feedbackList)
                {
                    var feedbackReponse = new FeedbackResponse();
                    feedbackReponse.ID = feedback.ID;
                    feedbackReponse.Rating = feedback.Rating;
                    feedbackReponse.WhatWentWell = feedback.WhatWentWell;
                    feedbackReponse.DidnotGoWell = feedback.DidnotGoWell;
                    feedbackReponse.CanBeImproved = feedback.CanBeImproved;
                    feedbackReponse.TraineeID = feedback.TraineeID;
                    feedbackReponse.TraineeName = db.Tranner.Find(feedback.TraineeID).Name;

                    feedbackReponseList.Add(feedbackReponse);
                }
            }
            customTrainerFeedback.FeedbackReponse = feedbackReponseList;

            return customTrainerFeedback;
        }

        public IList<TrainerModel> GetTrainerList()
        {
            return db.Tranner.OrderByDescending(t => t.Name).ToList();
        }
    }
}
