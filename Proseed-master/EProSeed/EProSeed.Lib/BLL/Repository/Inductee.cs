using EProSeed.DAL;
using EProSeed.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EProSeed.Lib.BLL
{
    public class Inductee:IInductee
    {
        protected readonly ProDbContext db;
        public Inductee()
        {
            db = new ProDbContext();
        }


        public IList<InducteeModel> Get(int count=10,int skip=0)
        {
            return  db.Inductee.Take(count).OrderByDescending(I=>I.Id).Skip(skip).ToList();
        }


        

        public bool Create(InducteeModel Inductee)
        {
            db.Inductee.Add(Inductee);
            return db.SaveChanges() > 0;
        }
        public InducteeModel Get(string mailId)
        {
            return db.Inductee.Where(I => I.Email.Trim() == mailId.Trim()).Select(I => I).FirstOrDefault();
        }


        public bool Delete(int? ID)
        {
            if (ID == null)
                throw new Exception("Select valid Trainee");

            var Inductee = db.Inductee.Find(ID);
            if (Inductee != null)
            {
                db.Inductee.Remove(Inductee);
                return db.SaveChanges() > 0;
            }
            return false;
        }


        public InducteeModel Find(int? id)
        {
            if (id == null)
                throw new Exception("Select valid Trainee");
            return db.Inductee.Find(id);
        }


        public IList<InducteeModel> InducteesByBatch(int? batchId)
        {
            if (batchId == null)
                throw new Exception("Select valid Trainee");
            return db.Inductee.Where(i => i.Batch.Id == batchId).ToList();
        }


        public bool Update(InducteeModel Inductee)
        {
            if (Inductee != null)
            {
                db.Entry(Inductee).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
            return false;
        }


        public IList<InducteeModel> FindByEmp(string _EmpID)
        {
            return db.Inductee.Where(I => I.EmpId == _EmpID).ToList();
        }
        public InducteeModel FindByEmail(string _email)
        {
            try
            {
               return db.Inductee.Where(I => I.Email == _email).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
