using EProSeed.Lib.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using EProSeed.Models;

namespace EProSeed.Web.Controllers
{
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    [Authorize]
    public class ValidationController : Controller
    {

        protected readonly IBatch BatchRepo;
        protected readonly IInductee InducteeRepo;

        public ValidationController()
        {
            BatchRepo = new Batch();
            InducteeRepo = new Inductee();
        }



        public JsonResult IsBatchName_Unique(BatchModel batch)
         {
            if (batch.Id > 0)
                return Json(true, JsonRequestBehavior.AllowGet);
            return IsExist(batch.Name)
            ? Json(true, JsonRequestBehavior.AllowGet)
            : Json(false, JsonRequestBehavior.AllowGet);
        }


        public JsonResult IsEmpID_Unique(InducteeModel Inductee)
        {
            if (Inductee.Id > 0)
                return Json(true, JsonRequestBehavior.AllowGet);
            return IsEmpExist(Inductee.EmpId)
            ? Json(true, JsonRequestBehavior.AllowGet)
            : Json(false, JsonRequestBehavior.AllowGet);
        }

        public bool IsExist(string BatchName)
        {
            //True:False--- action that implement to check barcode uniqueness
           var BatchNames = BatchRepo.FindByName(BatchName);
            if (BatchNames.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }


        public bool IsEmpExist(string EmpId)
        {
            //True:False--- action that implement to check barcode uniqueness
            var _EmpId = InducteeRepo.FindByEmp(EmpId);
            if (_EmpId.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
