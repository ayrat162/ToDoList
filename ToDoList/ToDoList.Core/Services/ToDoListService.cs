using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ToDoList.Core.Helpers;
using ToDoList.Core.Interfaces;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;
using ToDoList.Models.DTO;
using ToDoList.Models.Entities;

namespace ToDoList.Core.Services
{
    public class ToDoListService : IToDoListService

    {
        #region UnitOfWork definitions

        private EFUnitOfWork Database { get; set; }

        public ToDoListService(EFUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public ToDoListService()
        {
            Database = new EFUnitOfWork();
        }

        #endregion

        #region methods for add

        public int AddToDoTask(ToDoTaskDTO toDoTaskDto)
        {
            var toDoTask = Converter.Convert2Dal(toDoTaskDto);
            var newToDoTask = Database.ToDoTasks.Create(toDoTask);
            ApproveOrSendApprovalEmail(toDoTaskDto);
            return newToDoTask.Id;
        }

        public int AddClassification(ClassificationDTO classificationDto)
        {
            var classification = Converter.Convert2Dal(classificationDto);
            return Database.Classifications.Create(classification).Id;
        }

        public int AddPicture(PictureDTO pictureDto)
        {
            var picture = Converter.Convert2Dal(pictureDto);
            return Database.Pictures.Create(picture).Id;
        }

        #endregion

        #region methods for get

        public IEnumerable<ToDoTaskDTO> GetToDoTasks()
        {
            return Converter.Convert2Dto(Database.ToDoTasks.GetAll());
        }

        public IEnumerable<ClassificationDTO> GetClassifications()
        {
            return Converter.Convert2Dto(Database.Classifications.GetAll());
        }

        public ToDoTaskDTO GetToDoTask(int? id)
        {
            if (id == null) return null;
            var toDoTask = Database.ToDoTasks.Get(id.Value);
            if (toDoTask == null) return null;
            return Converter.Convert2Dto(toDoTask);
        }

        public ClassificationDTO GetClassification(int? id)
        {
            if (id == null) return null;
            var classification = Database.Classifications.Get(id.Value);
            if (classification == null) return null;
            return Converter.Convert2Dto(classification);
        }

        public PictureDTO GetPicture(int? id)
        {
            if (id == null) return null;
            var picture = Database.Pictures.Get(id.Value);
            if (picture == null) return null;
            return Converter.Convert2Dto(picture);
        }

        public IEnumerable<ToDoTaskDTO> GetToDoTasksOf(string userId)
        {
            var tasksOfUser = Database.ToDoTasks.Find(t => t.UserId == userId);
            return Converter.Convert2Dto(tasksOfUser);
        }

        public IEnumerable<ToDoTaskDTO> GetOldTasks()
        {
            var tasks = Database.ToDoTasks.Find(t => t.DueDateTime < DateTime.Now);
            var users = Database.UserManager.Users.ToList();
            var tasksDto = tasks.Join(
                users,
                t => t.UserId,
                u => u.Id,
                (t, u) => new ToDoTaskDTO()
                {
                    Description = t.Description,
                    DueDateTime = t.DueDateTime,
                    Status = t.Status,
                    UserUserName = u.ClientProfile.Name,
                    UserEmail = u.Email
                }
            );
            return tasksDto;
        }
        #endregion

        #region methods for update
        public void UpdateClassification(ClassificationDTO classificationDto)
        {
            var classification = Database.Classifications.Get(classificationDto.Id);
            Mapper.Map(classificationDto, classification);
            Database.Classifications.Update(classification);
            Database.Save();
        }

        public void UpdateToDoTask(ToDoTaskDTO toDoTaskDto)
        {
            var toDoTask = Database.ToDoTasks.Get(toDoTaskDto.Id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTaskDTO, ToDoTask>()).CreateMapper();
            mapper.Map(toDoTaskDto, toDoTask);
            toDoTask.IsApproved = false;
            Database.ToDoTasks.Update(toDoTask);
            Database.Save();
            ApproveOrSendApprovalEmail(toDoTaskDto);
        }

        public void UpdatePicture(PictureDTO pictureDto)
        {
            var picture = Database.Pictures.Get(pictureDto.Id);
            Mapper.Map(pictureDto, picture);
            Database.Pictures.Update(picture);
            Database.Save();
        }

        #endregion

        #region methods for delete
        public void DeleteToDoTask(int? id)
        {
            if (id != null)
            {
                Database.ToDoTasks.Delete(id.Value);
            }
        }

        public void DeleteClassification(int? id)
        {
            if (id != null)
            {
                Database.Classifications.Delete(id.Value);
            }
        }

        public void DeletePicture(int? id)
        {
            if (id != null)
            {
                Database.Pictures.Delete(id.Value);
            }
        }

        #endregion

        #region methods for approval
        public void ApproveTask(int id)
        {
            var ToDoTask = Database.ToDoTasks.Get(id);
            if (ToDoTask != null)
            {
                ToDoTask.IsApproved = true;
                Database.Save();
            }
        }

        public void ApproveOrSendApprovalEmail(ToDoTaskDTO toDoTaskDto)
        {
            var userService = new UserService();
            var toDoTask = Database.ToDoTasks.Get(toDoTaskDto.Id);

            if (toDoTaskDto.CreatedById == toDoTaskDto.UserId) //created by user for himself
            {
                ApproveTask(toDoTaskDto.Id);
                return;
            }

            var bosses = userService.GetBosses(toDoTaskDto.UserId).Select(b => b.Id);
            if (bosses.Contains(toDoTaskDto.CreatedById)) // created by boss of the user
            {
                ApproveTask(toDoTaskDto.Id);
                return;
            }

            if (toDoTaskDto.Id == 0
                || (toDoTask != null && !toDoTask.IsApproved))
                Helper.SendApprovalEmail(toDoTaskDto);
        }
        #endregion

        public void Dispose()
        {
            Database.Dispose();
        }


    }
}