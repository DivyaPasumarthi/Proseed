using EProSeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Lib.BLL
{
   public interface IFeedback
    {
        bool Create(FeedbackModel Feedback);

         FeedbackModel Find(int? id);

        bool Update(FeedbackModel feedback);
        bool Delete(int? id);
        IList<FeedbackModel> GetFeedbacksForTrainee(int trainee);

        //Property update and delete

        bool UpdateProperty(PropertyModel Property,string date,int? feedbackID);
        bool DeleteProperty(int? id);
    }
}
