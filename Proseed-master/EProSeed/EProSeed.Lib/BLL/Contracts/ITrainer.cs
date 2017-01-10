using EProSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EProSeed.Lib.BLL
{
    public enum UserType
    {
        None = 0,
        Trainer = 1,
        Trainee = 2
    }
    public interface ITrainer
    {
        TrainerModel Login(string Email, string Password);
        TrainerModel Login(string Email, string Password, out UserType userType);

        string GetName(int id);

        IList<TrainerModel> GetAll();

        TrainerModel Find(int? id);
    }
}
