using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Models
{
    [Table("Inductee")]
    public class InducteeModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Employee Id is required.")]
       
        [System.Web.Mvc.Remote("IsEmpID_Unique", "Validation", AdditionalFields = "Id", ErrorMessage = "This {0} is already used.")]
        public string EmpId { get;  set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-mail id is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid E-mail format.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Phone number is required.")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Invalid phone number.")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; }

        [ForeignKey("Batch")]
        [Required(ErrorMessage ="Select batch for Trainee.")]
        public int BatchID { get; set; }

        public virtual BatchModel Batch { get; set; }

        public virtual IList<FeedbackModel> Feedback { get; set; }



    }
}
