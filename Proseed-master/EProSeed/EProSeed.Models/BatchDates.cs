using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Models
{
    [Table("BatchDate")]
    public class BatchDates
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Batch")]
        public int BatchID { get; set; }

        public DateTime BatchDate { get; set; }
        public virtual BatchModel Batch { get; set; }

    }
}
