using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Models
{
    /*
     * 
     * 
     * */
        public class CustomTrainerFeedbackModel
        {
        public int BatchID { get; set; }

        public string BatchName { get; set; }

        public DateTime BatchStartDate { get; set; }
        public DateTime BatchEndDate { get; set; }

        public int TrainerID { get; set; }

        public string TrainerName { get; set; }

        public string TrainerEmail { get; set; }

        public List<FeedbackResponse> FeedbackReponse { get; set; }

        }

        public class FeedbackResponse
        {
            public int ID { get; set; }

            public string WhatWentWell { get; set; }

            public string DidnotGoWell { get; set; }

            public string CanBeImproved { get; set; }

            [Required]
            public int Rating { get; set; }

            public int TraineeID { get; set; }

            public string TraineeName { get; set; }
        }
}
