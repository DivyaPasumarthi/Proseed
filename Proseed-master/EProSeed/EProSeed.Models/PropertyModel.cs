using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Models
{
    [Table("Property")]
    public  class PropertyModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Rating should be between 0 to 10.")]
        [Display(Name = "Passion For Client Success")]
        public int PassionForClientSuccessRating { get; set; }

        [StringLength(500,ErrorMessage ="Comments cannot exceed 500 characters.")]
        public string PassionForClientSuccessComment { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Rating should be between 0 to 10.")]
        [Display(Name = "Focus On Quality")]
        public int FocusOnQualityRating { get; set; }

        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        public string FocusOnQualityComment { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Rating should be between 0 to 10.")]
        [Display(Name = "Communication")]
        public int CommunicationRating { get; set; }

        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        public string CommunicationComment { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Rating should be between 0 to 10.")]
        [Display(Name = "Transparency")]
        public int TransparencyRating { get; set; }

        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        public string TransparencyComment { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Rating should be between 0 to 10.")]
        [Display(Name = "Team Player")]
        public int TeamPlayerRating { get; set; }

        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        public string TeamPlayerComment { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Rating should be between 0 to 10.")]
        [Display(Name = "Discipline")]
        public int DisciplineRating { get; set; }


        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        public string DisciplineComment { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Rating should be between 0 to 10.")]
        [Display(Name = "Energy")]
        public int EnergyRating { get; set; }


        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        public string EnergyComment { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Rating should be between 0 to 10.")]
        [Display(Name = "Commitment")]
        public int CommitmentRating { get; set; }

        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        public string CommitmentComment { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Rating should be between 0 to 10.")]
        [Display(Name = "Ownership")]
        public int OwnerShipRating { get; set; }

        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        public string OwnerShipComment { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Rating should be between 0 to 10.")]
        [Display(Name = "Technical Competency")]
        public int TechnicalCompetencyRating { get; set; }

        [Display(Name = "Technical Competency")]
        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        public string TechnicalCompetencyComment { get; set; }
    }
}
