using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.Core.Infrastructure;
using ToDoList.Models.DTO;

namespace ToDoList.Core.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}
