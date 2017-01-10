using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EProSeed.DAL;
using EProSeed.Models;

namespace EProSeed.Lib.BLL
{
    public class TrainerTraineeUserMapping
    {
        protected readonly ProDbContext db;

        public TrainerTraineeUserMapping()
        {
            db = new ProDbContext();
        }

        public void Create(Trainer_UserType_Map_Model map_Model)
        {
            map_Model.Map_Id = db.TrainerTraineeUserMapping.Count() > 0 ? db.TrainerTraineeUserMapping.Max(n => n.Map_Id) + 1 : 1;
            db.TrainerTraineeUserMapping.Add(map_Model);
            db.SaveChanges();
        }
    }
}
