using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.DAL.Entities
{
    public class Picture
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }

        //[Required]
        //public ApplicationUser User { get; set; }
        //public string UserId { get; set; }
    }
}
