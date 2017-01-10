using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EProSeed.Lib.BLL;
using EProSeed.Models;

namespace EProSeed.Web.Models
{
    public class vmAdmin
    {
        private IInductee inductees;
        private ITrainer trainers;
        
        public IList<InducteeModel> InducteeRepo { get { return inductees.Get(); } }

        public IList<TrainerModel> TrainerRepo { get { return trainers.GetAll(); } }

        public vmAdmin(IInductee inducteeRepo, ITrainer trainerRepo)
        {
             inductees = new Inductee();
             trainers = new Trainer();
        }
    }
}