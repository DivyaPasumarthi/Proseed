using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EProSeed.Models;

namespace EProSeed.Lib.BLL
{
    public interface IBatch
    {
        bool Create(BatchModel batch);
        IList<BatchModel> GetAll();

        bool Edit(BatchModel batch);

        BatchModel Find(int? id);

        IList<BatchModel> FindByName(string Name);

        bool Update(BatchModel batch);

        void DeleteConfirmed(int? id);

        IList<BatchModel> GetAllForTrainee(string trainee);

        BatchModel GetBatchDetailsByTraineeId(string userId);
    }


}
