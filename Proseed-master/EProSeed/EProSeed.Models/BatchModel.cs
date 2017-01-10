using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Models
{
    [Table("Batch")]
    public class BatchModel
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        [System.Web.Mvc.Remote("IsBatchName_Unique", "Validation", AdditionalFields = "Id",
            ErrorMessage = "This {0} is already used.")]
        public string Name { get; set; }

        [ForeignKey("trainer")]
        public int? TrainerId { get; set; }

       
        public virtual TrainerModel trainer { get; set; }

        public virtual IList<BatchDates> BatchDates { get; set; }
    }
}
