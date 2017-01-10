using System;
using System.Collections.Generic;
using System.Linq;
using EProSeed.DAL;
using EProSeed.Models;
using System.Data.Entity;

namespace EProSeed.Lib.BLL
{
    public class Batch : IBatch
    {
        protected readonly ProDbContext db;
        protected readonly IInductee _Inductee;
        public Batch()
        {
            db = new ProDbContext();
            _Inductee = new Inductee();
        }

        public IList<BatchModel> GetAll()
        {
            return db.Batch.ToList();
        }

        public IList<BatchModel> GetAllForTrainee(string traineeId)
        {
            try
            {
                var inductee = db.Inductee.FirstOrDefault(i => i.Id.ToString() == traineeId);
                return db.Batch.Where(b => b.Id == inductee.BatchID).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public BatchModel GetBatchDetailsByTraineeId(string traineeId)
        {
            int traineeIntId = Convert.ToInt32(traineeId);
            var user = db.Tranner.FirstOrDefault(us => us.Id == traineeIntId);

            var inducteeBatchId = _Inductee.Get(user.Email).BatchID;
            var traineesBatch = db.Batch.Where(B => B.Id == inducteeBatchId).Select(B => B).FirstOrDefault();
            var batchDetails = (from inductee in db.Inductee
                                join trainee in db.Tranner on inductee.Email equals trainee.Email
                                join batch in db.Batch on inductee.BatchID equals batch.Id
                                select batch).FirstOrDefault();
            batchDetails.trainer = traineesBatch.trainer;

            return batchDetails;
        }

        public bool Create(BatchModel _batch)
        {
            var batch = db.Batch.SingleOrDefault(b => b.Name == _batch.Name.Trim());
            if (batch != null)
                return false;
            db.Batch.Add(_batch);
            return db.SaveChanges() > 0;


        }

        public bool Edit(BatchModel batch)
        {
            if (batch != null)
            {
                db.Entry(batch).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
            return false;
        }


        public BatchModel Find(int? id)
        {
            if (id == null)
                throw new Exception("Select valid Trainee");
            return db.Batch.Find(id);
        }


        public bool Update(BatchModel batch)
        {
            if (batch != null)
            {
                db.Entry(batch).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                    return true;

            }

            return false;
        }

        public void DeleteConfirmed(int? id)
        {
            if (id == null)
                throw new Exception("Select valid batch");
            BatchModel batch = db.Batch.Find(id);
            if (batch != null)
            {
                db.Batch.Remove(batch);
                db.SaveChanges();
            }
        }


        public IList<BatchModel> FindByName(string Name)
        {
            return db.Batch.Where(b => b.Name == Name).ToList();
        }

    }
}
