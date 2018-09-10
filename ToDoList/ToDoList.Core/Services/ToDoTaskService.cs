﻿using System.Collections.Generic;
using AutoMapper;
using ToDoList.Core.DTO;
using ToDoList.Core.Infrastructure;
using ToDoList.Core.Interfaces;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

namespace ToDoList.Core.Services
{
    public class ToDoTaskService : IToDoTaskService
    {
        private IUnitOfWork Database { get; set; }

        public ToDoTaskService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public void AddToDoTask(ToDoTaskDTO toDoTaskDto)
        {
            Database.ToDoTasks.Create(new ToDoTask {Description = toDoTaskDto.Description, Status = toDoTaskDto.Status});
        }

        public IEnumerable<ToDoTaskDTO> GetToDoTasks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTask, ToDoTaskDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ToDoTask>, List<ToDoTaskDTO>>(Database.ToDoTasks.GetAll());
        }

        public ToDoTaskDTO GetToDoTask(int? id)
        {
            if (id == null)
                throw new ValidationException("No ID is found. ID is required.", "");
            var toDoTask = Database.ToDoTasks.Get(id.Value);
            if (toDoTask == null)
                throw new ValidationException("Task with this ID can't be found", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTask, ToDoTaskDTO>()).CreateMapper();

            return mapper.Map<ToDoTask, ToDoTaskDTO>(toDoTask);
        }

        public List<ToDoTaskDTO> GetToDoTasksFor(string id)
        {
            var listOfTaskForUser = Database.ToDoTasks.Find(t => t.UserId == id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTask, ToDoTaskDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ToDoTask>, List<ToDoTaskDTO>>(listOfTaskForUser);
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}