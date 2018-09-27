using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Owin.Security.DataProtection;
using ToDoList.Core.Helpers;
using ToDoList.Core.Infrastructure;
using ToDoList.Core.Interfaces;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;
using ToDoList.Models.DTO;
using ToDoList.Models.Entities;
using Microsoft.AspNet.Identity.Owin;
using ToDoList.Models;


namespace ToDoList.Core.Services
{
    public class UserService : IUserService
    {
        #region UnitOfWork definitions
        private EFUnitOfWork Database { get; set; }
        public UserService(EFUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }
        public UserService() 
        {
            Database = new EFUnitOfWork();
        }
        #endregion

        #region methods for Create
        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = Database.UserManager.Create(user, userDto.Password);
                if (result.Errors.Any())
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
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

        public OperationDetails Create(UserAndRoleDTO userAndRoleDto)
        {
            var user = Database.UserManager.FindByEmail(userAndRoleDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userAndRoleDto.Email, UserName = userAndRoleDto.Email };
                var result = Database.UserManager.Create(user, userAndRoleDto.Password);
                if (result.Errors.Any())
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                Database.UserManager.AddToRole(user.Id, userAndRoleDto.Role);
                var clientProfile = new ClientProfile { Id = user.Id, Name = userAndRoleDto.Name, Email = userAndRoleDto.Email };
                Database.ClientManager.Create(clientProfile);
                Database.Save();
                return new OperationDetails(true, "Successfully registered", "");
            }
            else
            {
                return new OperationDetails(false, "User with this email already exists", "Email");
            }
        }

        #endregion

        #region methods for Get

        public IEnumerable<UserAndRoleDTO> GetAllUsers()
        {
            var users = Database.UserManager.Users.ToList();
            var roles = Database.RoleManager.Roles.ToList();
            var usersAndRoles = users.Join(roles,
                u => u.Roles.First().RoleId,
                r => r.Id,
                (u, r) => new UserAndRoleDTO(){
                    Id = u.Id,
                    Name = u.ClientProfile.Name,
                    Email = u.Email,
                    Role = r.Name,
                    RoleId = r.Id
                }
            );
            return usersAndRoles;
        }

        public UserAndRoleDTO GetUser(string id)
        {
            var user = Database.UserManager.Users.Single(u => u.Id == id);
            if (user == null) return null;
            var roles = Database.RoleManager.Roles.ToList();
            var userAndRoleDto = new UserAndRoleDTO
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.ClientProfile.Name,
                Role = roles.SingleOrDefault(r => r.Id == user.Roles.First().RoleId).Name,
                RoleId = user.Roles.First().RoleId
            };
            return userAndRoleDto;
        }

        public IEnumerable<UserDTO> GetBosses(string userId)
        {
            var user = Database.UserManager.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null) return null;
            var relationshipsOfUser = Database.Relationships.Find(r => r.ChildId == userId);
            var bosses = relationshipsOfUser.Select(r => r.Boss);
            return Converter.Convert2Dto(bosses);
        }

        public IEnumerable<UserDTO> GetChildren(string userId)
        {
            var user = Database.UserManager.Users.SingleOrDefault(u => u.Id == userId);
            if (user == null) return null;
            var relationshipsOfUser = Database.Relationships.Find(r => r.BossId == userId);
            var children = relationshipsOfUser.Select(r => r.Child);
            return Converter.Convert2Dto(children);
        }

        public IEnumerable<RoleDTO> GetRoles()
        {
            var roles = Database.RoleManager.Roles;
            return Converter.Convert2Dto(roles);
        }

        public IEnumerable<string> GetAdminIds()
        {
            var adminUsers = Database.RoleManager.Roles.Single(r => r.Name == RoleNames.Admin).Users;
            var adminUserIds = adminUsers.Select(u => u.UserId);
            return adminUserIds;
        }

        public string GetRoleForUser(string id)
        {
            if (id == null) return null;
            var role = Database.UserManager.GetRoles(id).First();
            return role;
        }

        #endregion

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
        
        public void UpdateUser(UserAndRoleDTO userDto)
        {
            var user = Database.UserManager.FindById(userDto.Id);
            var roles = Database.RoleManager.Roles.ToList();
            var currentRoles = roles.SingleOrDefault(r => r.Id == user.Roles.SingleOrDefault().RoleId).Name;
            var newRoleName = roles.SingleOrDefault(r => r.Id == userDto.RoleId).Name;
            user.Email = userDto.Email;
            user.UserName = userDto.Name;
            user.ClientProfile.Email = userDto.Email;
            user.ClientProfile.Name = userDto.Name;
            Database.UserManager.RemoveFromRoles(userDto.Id, currentRoles);
            Database.Save();
            Database.UserManager.AddToRole(userDto.Id, newRoleName);
            Database.Save();
        }

        public void DeleteUser(string id)
        {
            var user = Database.UserManager.FindById(id);
            if (user != null)
            {
                Database.UserManager.Delete(user);
            }
            Database.Save();
        }

        public void ResetPassword(string userId)
        {
            var user = Database.UserManager.FindById(userId);
            var provider = new DpapiDataProtectionProvider("ToDoList");
            Database.UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
                provider.Create("ResetPasswordPurpose"));
            var token = Database.UserManager.GeneratePasswordResetToken(userId);
            if (user != null)
            {
                Database.UserManager.ResetPassword(userId: userId, token: token, newPassword: "qwerty");
            }
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
