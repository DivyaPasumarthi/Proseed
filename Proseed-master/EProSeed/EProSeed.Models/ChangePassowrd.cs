using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Models
{
    public class ChangePassowrd
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string CurrentPassowrd { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassowrd { get; set; }
    }
}
