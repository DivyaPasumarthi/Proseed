using EProSeed.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EProSeed.Web.Models
{
    public class vmFeedbackProperty
    {
        public int? FeedBackID { get; set; }


        [Required(ErrorMessage = "Select date")]
        public string Date { get; set; }

        public int InducteeID { get; set; }

        public string InducteeName { get; set; }

        public int? TrainerID { get; set; }

        public string TrainerName { get; set; }


        public int BatchID { get; set; }

        public string BatchName { get; set; }

        public List<SelectListItem>  BatchDates { get; set; }

        public PropertyModel Property { get; set; }
    }
}