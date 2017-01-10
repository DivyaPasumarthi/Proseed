using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Models
{
    [Table("Feedback")]
    public class FeedbackModel
    {
        [Key]
        public int ID { get; set; }
        public DateTime FeedbackDate { get; set; }


        [ForeignKey("Inductee")]
        public int InducteeID { get; set; }

        public virtual InducteeModel Inductee { get; set; }

        [ForeignKey("Trainer")]
        public int TrainerId { get; set; }

        public virtual TrainerModel Trainer { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        
      
        public virtual  PropertyModel Property { get; set; }
    }
}
