using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EProSeed.Models;
namespace EProSeed.DAL
{
    public class ProDbContext : DbContext
    {
        public ProDbContext()
            : base("_dbProDev")
        {

        }

        public virtual IDbSet<BatchModel> Batch { get; set; }
        public virtual IDbSet<BatchDates> BatchDates { get; set; }
        public virtual IDbSet<TrainerModel> Tranner { get; set; }
        public virtual IDbSet<InducteeModel> Inductee { get; set; }
        public virtual IDbSet<PropertyModel> Property { get; set; }
        public virtual IDbSet<FeedbackModel> Feedback { get; set; }
        public virtual IDbSet<Trainer_UserType_Map_Model> TrainerTraineeUserMapping { get; set; }
        public virtual IDbSet<TrainersFeedbackModel> TrainersFeedback { get; set; }

        //public DbSet<TrainerFeedbackQuestionModel> TrainerFeedbackQuestion { get; set; }

        //public DbSet<TrainerFeedbackQuestionResponseMappingModel> TrainerFeedbackQuestionResponseMapping { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ProDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

    }
}
