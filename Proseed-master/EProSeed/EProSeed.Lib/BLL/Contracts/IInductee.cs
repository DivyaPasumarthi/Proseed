using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EProSeed.Models;

namespace EProSeed.Lib.BLL
{
  public interface IInductee
    {
        IList<InducteeModel> Get(int count = 10, int skip = 0);
        bool Create(InducteeModel Inductee);

        bool Update(InducteeModel Inductee);

        bool Delete(int? ID);

        InducteeModel Find(int? id);

        IList<InducteeModel> InducteesByBatch(int? BatchId);
        IList<InducteeModel> FindByEmp(string _EmpID);
        InducteeModel FindByEmail(string _email);

        InducteeModel Get(string mailId);

    }
}
