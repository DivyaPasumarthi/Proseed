using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProSeed.Models
{
    [Table("UserMap")]
   public class Trainer_UserType_Map_Model
    {
        [Key]
        public int Map_Id { get; set; }


        public int Map_UserType_Id { get; set; }


        public int Map_Trainer_Id { get; set; }
    }   
}
