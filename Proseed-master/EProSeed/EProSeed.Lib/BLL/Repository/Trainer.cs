using EProSeed.DAL;
using EProSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Lib.BLL
{
    public class Trainer : ITrainer
    {
        protected readonly ProDbContext db;
        public Trainer()
        {
            db = new ProDbContext();
        }
        /// <summary>
        /// Avoid to use it, instead use another version.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public TrainerModel Login(string email, string password)
        {
            var User = db.Tranner.Where(t => t.Email == email && t.Password == password).SingleOrDefault();
            return User;
        }

        public TrainerModel Login(string email, string password, out UserType userType)
        {
            userType = UserType.None;
            var User = db.Tranner.Where(t => t.Email == email && t.Password == password).SingleOrDefault();

            if (User != null)
            {
                userType = (UserType)db.TrainerTraineeUserMapping.Where(M => User.Id == M.Map_Trainer_Id).
                    Select(M => M.Map_UserType_Id).SingleOrDefault();
            }
            return User;
        }

        public int Create(TrainerModel trainer)
        {
            db.Tranner.Add(trainer);
            db.SaveChanges();
            return trainer.Id;
        }

        public string GetName(int id)
        {
            return db.Tranner.Where(t => t.Id == id).SingleOrDefault().Name;

        }

        public TrainerModel Find(int? id)
        {
            return db.Tranner.Where(t => t.Id == id).SingleOrDefault();
        }

        public IList<TrainerModel> GetAll()
        {
            int user_type = UserType.Trainer.GetHashCode();
            return db.Tranner.Where(n => db.TrainerTraineeUserMapping.Any(k =>
                k.Map_Trainer_Id == n.Id
                && k.Map_UserType_Id == user_type)).ToList();
        }

    }
}
