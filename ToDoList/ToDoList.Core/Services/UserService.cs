using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
<<<<<<< HEAD
using ToDoList.Core.DTO;
=======
>>>>>>> dev
using ToDoList.Core.Helpers;
using ToDoList.Core.Infrastructure;
using ToDoList.Core.Interfaces;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;
<<<<<<< HEAD
=======
using ToDoList.Models.DTO;
using ToDoList.Models.Entities;
>>>>>>> dev

namespace ToDoList.Core.Services
{
    public class UserService : IUserService
    {
        private EFUnitOfWork Database { get; set; }
        public UserService(EFUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }
<<<<<<< HEAD
        public UserService()
=======
        public UserService() 
>>>>>>> dev
        {
            Database = new EFUnitOfWork();
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = Database.UserManager.Create(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Name = userDto.Name, Email = userDto.Email };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Successfully registered", "");
            }
            else
            {
                return new OperationDetails(false, "User with this email already exists", "Email");
            }
        }
        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

<<<<<<< HEAD
        public IEnumerable<UserDTO> GetAllUsers()
        {
            var users = Database.UserManager.Users.ToList();
            return Converter.Convert2Dto(users);
        }

        public UserDTO GetUser(string id)
        {
            if (id == null) return null;
            var user = Database.UserManager.FindById(id);
            if (user == null) return null;
            return Converter.Convert2Dto(user);
=======

        public IEnumerable<UserAndRoleDTO> GetAllUsers()
        {
            var users = Database.UserManager.Users.ToList();
            var roles = Database.RoleManager.Roles.ToList();
            var usersAndRoles = users.Join(roles,
                u => u.Roles.First().RoleId,
                r => r.Id,
                (u, r) => new UserAndRoleDTO(){Id = u.Id, Name = u.ClientProfile.Name, Email = u.Email, Role = r.Name}
            );
            return usersAndRoles;
        }

        public UserAndRoleDTO GetUser(string id)
        {
            var user = Database.UserManager.Users.SingleOrDefault(u => u.Id == id);
            var roles = Database.RoleManager.Roles.ToList();
            var userAndRoleDto = new UserAndRoleDTO
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.ClientProfile.Name,
                Role = roles.SingleOrDefault(r => r.Id == user.Roles.First().RoleId).Name
            };
            return userAndRoleDto;
>>>>>>> dev
        }
        public IList<string> GetRoleForUser(string id)
        {
            if (id == null) return null;
            var roles = Database.UserManager.GetRoles(id);
            return roles;
        }

        // TODO: Implementation required
        public void UpdateUser(UserDTO userDto)
        {
            var user = Database.UserManager.FindById(userDto.Id);
            Mapper.Map(userDto, user);
            Database.UserManager.Update(user);
            Database.Save();
        }

        public void DeleteUser(string id)
        {
            var user = Database.UserManager.FindById(id);
            if (user != null)
            {
                // TODO: Implement Delete for ClientManager
                // Database.ClientManager.Delete(id);
                Database.UserManager.Delete(user);
            }
            Database.Save();
        }

<<<<<<< HEAD
=======

>>>>>>> dev
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
