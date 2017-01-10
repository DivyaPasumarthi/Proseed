using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EProSeed.Web.Models
{
    public class vmLogin
    {
        [Required(ErrorMessage ="Please enter the Email.")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Please enter the valid Email.")]
        [EmailAddress(ErrorMessage = "Please enter the valid Email.")]
        //[RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$", ErrorMessage = "Please enter the valid Email.")]
        public String Email { get; set; }


        [Required(ErrorMessage = "Please enter the password")]
        public String Password { get; set; }
    }
}