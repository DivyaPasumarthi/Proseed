using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EProSeed.Models;
using Microsoft.Azure.ActiveDirectory.GraphClient;

namespace EProSeed.Web.Models
{
    public class vmDashBoard
    {
        public IList<BatchModel> BatchList { get; set; }

        public IList<InducteeModel> InducteeList { get; set; }

        public IUser User { get; set; }

    }
}