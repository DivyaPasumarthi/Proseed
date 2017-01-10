namespace EProSeed.DAL.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EProSeed.DAL.ProDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EProSeed.DAL.ProDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //context.TrainerFeedback.AddOrUpdate(
            //    t => t.Batch,
            //    new TrainerFeedbackModel { Batch = 1, DateCreated = DateTime.Now() },
            //    new TrainerFeedbackQuestionModel { Question = "What didn't go well?" },
            //    new TrainerFeedbackQuestionModel { Question = "What can be improved?" },
            //    new TrainerFeedbackQuestionModel { Question = "Ratings" }
            //    );
        }
    }
}
