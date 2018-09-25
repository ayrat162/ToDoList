using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Models.Entities
{
    public class Picture
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }

        // TODO: User for classification can be defined
    }
}
