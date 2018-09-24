using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using ToDoList.Core.Services;
using ToDoList.Models.DTO;
using ToDoList.Web.Helpers;

namespace ToDoList.Web.Controllers.API
{
    [System.Web.Mvc.Authorize(Roles = Check.Admin)]
    public class UsersApiController : ApiController
    {
        #region services definition
        private UserService userService;
        public UsersApiController(UserService service)
        {
            userService = service;
        }
        UserService service = new UserService();
        public UsersApiController()
        {
            userService = service;
        }
        #endregion

        [Route("api/users")]
        [HttpGet]
        public IEnumerable<UserAndRoleDTO> GetUsers()
        {
            var users = userService.GetAllUsers();
            return users;
        }

        [Route("api/users/{id}")]
        [HttpDelete]
        public void DeleteTask(string id)
        {
            var useAndRoleDto = userService.GetUser(id);
            if (useAndRoleDto == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            userService.DeleteUser(id);
        }
    }
}
