using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Models
{
    [Table("TrainersFeedback")]
    public class TrainersFeedbackModel
    {
        [Key]
        public int ID { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey("Batch")]
        public int BatchID { get; set; }

        [ForeignKey("Trainee")]
        public int TraineeID { get; set; }

        public virtual BatchModel Batch { get; set; }

        public virtual InducteeModel Trainee { get; set; }

        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        [Display(Name = "What went well?")]
        [Required]
        public string WhatWentWell { get; set; }

        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        [Display(Name = "What didn't go well?")]
        [Required]
        public string DidnotGoWell { get; set; }

        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        [Display(Name = "What can be improved?")]
         [Required]
        public string CanBeImproved { get; set; }

        [Display(Name = "Rating")]
        [Required]
        public int Rating { get; set; }

    }
}
