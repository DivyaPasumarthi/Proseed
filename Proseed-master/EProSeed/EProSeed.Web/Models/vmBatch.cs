using EProSeed.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EProSeed.Web.Models
{
    public class vmBatch
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Select Batch Dates.")]
        public string BatchDates { get; set; }

        [Required(ErrorMessage = "Select Trainer.")]
        public int? TrainerId { get; set; }

        [ForeignKey("TrainerId")]
        public TrainerModel trainer { get; set; }

    }
}