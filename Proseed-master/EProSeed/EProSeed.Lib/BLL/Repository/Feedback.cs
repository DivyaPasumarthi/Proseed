using EProSeed.DAL;
using EProSeed.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Lib.BLL
{
    public class Feedback : IFeedback
    {
        protected readonly ProDbContext db;
        public Feedback()
        {
            db = new ProDbContext();
        }

        public bool Create(FeedbackModel Feedback)
        {
            db.Feedback.Add(Feedback);
            return db.SaveChanges() > 0;
        }




        public FeedbackModel Find(int? id)
        {
            if (id == null)
                throw new Exception("Select valid Trainee");
            return db.Feedback.Find(id);
        }


        public bool Update(FeedbackModel feedback)
        {
            if (feedback != null)
            {
                db.Entry(feedback).State = EntityState.Modified;
                return db.SaveChanges() > 0;

            }

            return false;
        }


        public bool Delete(int? id)
        {
            if (id == null)
                throw new Exception("Invalid feedback.");
            FeedbackModel feedback = db.Feedback.Find(id);
            if (feedback != null)
            {
                db.Property.Remove(feedback.Property);
                db.Feedback.Remove(feedback);
                return db.SaveChanges() > 0;
            }
            return false;
        }


        public bool UpdateProperty(PropertyModel Property, string Date, int? feedbackID)
        {
            if (Property != null)
            {

                if (Date != null)
                {
                    var feedback = Find(feedbackID);
                    var dt = Convert.ToDateTime(Date);
                    feedback.FeedbackDate = dt;
                    db.Entry(feedback).State = EntityState.Modified;
                }


                db.Entry(Property).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }

            return false;
        }



        public bool DeleteProperty(int? id)
        {
            if (id == null)
                throw new Exception("Invalid feedback.");
            var property = db.Property.Find(id);
            if (property != null)
            {
                db.Property.Remove(property);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public IList<FeedbackModel> GetFeedbacksForTrainee(int traineeID)
        {
            return db.Feedback.Where(p => p.InducteeID == traineeID).ToList();
        }
    }
}
