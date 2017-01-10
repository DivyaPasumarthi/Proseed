using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EProSeed.Models;

namespace EProSeed.Lib.BLL.Contracts
{
    public interface IReport
    {
        int CountInductees(int batchId);

        ReportModel GetReport(int batchId, int inducteeId);

        string TrainerName(int batchId);      
    }
}
