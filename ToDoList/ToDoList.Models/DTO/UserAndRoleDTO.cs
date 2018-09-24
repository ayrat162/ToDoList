using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Models.DTO
{
    public class UserAndRoleDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string RoleId { get; set; }
        [StringLength(maximumLength:30, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
