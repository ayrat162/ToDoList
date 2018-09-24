using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ToDoList.Models.DTO;
using ToDoList.Models.Entities;

namespace ToDoList.Web.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Email { get; set; }

        [RegularExpression(@"^.{8,}$", ErrorMessage = "Minimum 6 characters required")]
        [Required(ErrorMessage = "Required")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Invalid")]
        public string Password { get; set; }

        public string Role { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("User's Role")]
        public string RoleId { get; set; }
        public IEnumerable<RoleDTO> Roles { get; set; }
    }
}