using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Core.DTO;

namespace ToDoList.Web.ViewModels
{
    public class AllUsersViewModel
    {
        public IEnumerable<UserDTO> UserDtos { get; set; }
        //public IEnumerable<RoleDTO> RoleDtos { get; set; }
    }
}