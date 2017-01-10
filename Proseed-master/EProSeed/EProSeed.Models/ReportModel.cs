using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EProSeed.Models
{
    public class ReportModel
    {
        [Required(ErrorMessage = "Batch Name is required.")]
        public int BatchId { get; set; }

        public int InducteeId { get; set; }

        public string TrainerName { get; set; }

        public int NumberofInductees { get; set; }

        public decimal BatchAverage { get; set; }

        public decimal InducteeAverage { get; set; }

        public string InducteeName { get; set; }

        public List<ReportFeedbackModel> FeedBacks { get; set; }
    }

    public class ReportFeedbackModel
    {
        public DateTime Date { get; set; }
        public decimal Score { get; set; }
    }
}
