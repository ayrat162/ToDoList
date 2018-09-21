using System.Collections.Generic;
using System.ComponentModel;
using ToDoList.Models.DTO;
using ToDoList.Models.Entities;

namespace ToDoList.Web.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        [DisplayName("User's Role")]
        public string RoleId { get; set; }
        public IEnumerable<RoleDTO> Roles { get; set; }
    }
}