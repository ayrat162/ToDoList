﻿using System.Collections.Generic;
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
<<<<<<< HEAD
        private IUnitOfWork Database { get; set; }
        public ToDoListService(IUnitOfWork unitOfWork)
=======
        private EFUnitOfWork Database { get; set; }

        public ToDoListService(EFUnitOfWork unitOfWork)
>>>>>>> dev
        {
            Database = unitOfWork;
        }
        public ToDoListService()
        {
            Database= new EFUnitOfWork();
        }

        public int AddToDoTask(ToDoTaskDTO toDoTaskDto)
        {
            var toDoTask = Converter.Convert2Dal(toDoTaskDto);
            return Database.ToDoTasks.Create(toDoTask).Id;
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

        public void UpdateClassification(ClassificationDTO classificationDto)
        {
            var classification = Database.Classifications.Get(classificationDto.Id);
            Mapper.Map(classificationDto,classification);
            Database.Classifications.Update(classification);
            Database.Save();
        }
        public void UpdateToDoTask(ToDoTaskDTO toDoTaskDto)
        {
            var toDoTask = Database.ToDoTasks.Get(toDoTaskDto.Id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTaskDTO, ToDoTask>()).CreateMapper();
            mapper.Map(toDoTaskDto, toDoTask);
            Database.ToDoTasks.Update(toDoTask);
            Database.Save();
        }
        public void UpdatePicture(PictureDTO pictureDto)
        {
            var picture = Database.Pictures.Get(pictureDto.Id);
            Mapper.Map(pictureDto, picture);
            Database.Pictures.Update(picture);
            Database.Save();
        }

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

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}