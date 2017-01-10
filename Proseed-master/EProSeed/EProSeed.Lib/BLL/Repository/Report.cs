using EProSeed.DAL;
using EProSeed.Lib.BLL.Contracts;
using EProSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Lib.BLL.Repository
{
    public class Report : IReport
    {
        protected readonly ProDbContext db;

        public Report(ProDbContext context)
        {
            this.db = context;
        }

        public Report() : this(new ProDbContext())
        {
            
        }

        public string TrainerName(int batchId)
        {
            try
            {
                int trainerId = db.Batch.Where(B => B.Id == batchId).Select(T => T.TrainerId).SingleOrDefault().HasValue ? db.Batch.Where(B => B.Id == batchId).Select(T => T.TrainerId).SingleOrDefault().Value : 0;
                string TrainerName = db.Tranner.Where(t => t.Id == trainerId).Select(n => n.Name).SingleOrDefault();
                return TrainerName;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CountInductees(int batchId)
        {
            try
            {
                int count = db.Inductee.Where(m => m.BatchID == batchId).Count();
                return count;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ReportModel GetReport(int batchId, int indcuteeId)
        {
            List<DateTime> batchDateList = db.BatchDates.Where(d => d.BatchID == batchId && d.BatchDate <= DateTime.Now)
                                                    .OrderBy(n => n.BatchDate)
                                                    .Select(n => n.BatchDate).ToList();
            List<InducteeModel> inducteesList = db.Inductee.Where(m => m.BatchID == batchId).ToList();
            InducteeModel selectedInductee = indcuteeId > 0 ? inducteesList.Find(n => n.Id == indcuteeId) : null;
            decimal batchSum = 0.0M;
            ReportModel reportModel = new ReportModel()
            {
                TrainerName = TrainerName(batchId),
                BatchId = batchId,
                InducteeId = indcuteeId,
                NumberofInductees = inducteesList.Count,
                InducteeName = selectedInductee != null ? selectedInductee.Name : "",
                FeedBacks = new List<ReportFeedbackModel>()
            };
            foreach (InducteeModel inductee in inducteesList)
                batchSum += GetInducteeFeedbackAverage(batchDateList, selectedInductee, reportModel, inductee);
            reportModel.BatchAverage = Math.Round(batchSum / reportModel.NumberofInductees, 2);
            return reportModel;
        }

        private decimal GetInducteeFeedbackAverage(List<DateTime> batchDateList, InducteeModel selectedInductee, ReportModel reportModel, InducteeModel inductee)
        {
            var feedBacks = db.Feedback.Where(m => m.InducteeID == inductee.Id).ToList();
            decimal inudcteeSum = 0.0M;
            foreach (DateTime date in batchDateList)
            {
                decimal sum = GetFeedbackAverageByDate(feedBacks, date);
                inudcteeSum += sum;
                if (selectedInductee != null && inductee.Id == selectedInductee.Id)
                    reportModel.FeedBacks.Add(new ReportFeedbackModel()
                    {
                        Date = date,
                        Score = sum
                    });
            }
            decimal inudcteeAvg = Math.Round(inudcteeSum / batchDateList.Count, 2);
            if (selectedInductee != null && inductee.Id == selectedInductee.Id)
                reportModel.InducteeAverage = inudcteeAvg;
            return inudcteeAvg;
        }

        private decimal GetFeedbackAverageByDate(List<FeedbackModel> feedBacks, DateTime date)
        {
            var feedBack = feedBacks.FirstOrDefault(p => p.FeedbackDate.Date == date);
            Decimal sum = 0.0M;
            if (feedBack != null)
            {
                PropertyModel property = db.Property.FirstOrDefault(p => p.ID == feedBack.PropertyId);
                if (property != null)
                    sum = Math.Round((property.PassionForClientSuccessRating + property.FocusOnQualityRating + property.CommunicationRating + property.TransparencyRating + property.OwnerShipRating
                           + property.TeamPlayerRating + property.CommitmentRating + property.DisciplineRating + property.EnergyRating + property.TechnicalCompetencyRating) / 10.0M, 2);

            }
            return sum;
        }
    }
}
