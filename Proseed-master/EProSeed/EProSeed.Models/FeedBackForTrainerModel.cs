using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Models
{
    [Table("FeedbackForTrainer")]
    public class FeedBackForTrainerModel
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Tranner")]
        public int TrainerID { get; set; }

        [ForeignKey("Inductee")]
        public int TraineeID { get; set; }

        [ForeignKey("TrainerFeedbackQuestion")]
        public  int QuestionID { get; set; }

        [StringLength(500,ErrorMessage ="At most 500 characters allowed.")]
        public string Response { get; set; }

        public virtual TrainerModel Trainer { get; set; }

        public virtual InducteeModel Inductee { get; set; }

        public virtual TrainerFeedbackQuestionModel TrainerFeedbackQuestion { get; set; }
    }
}
