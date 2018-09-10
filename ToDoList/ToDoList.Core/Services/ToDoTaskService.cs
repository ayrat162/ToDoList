using System.Collections.Generic;
using AutoMapper;
using ToDoList.Core.DTO;
using ToDoList.Core.Helpers;
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
            var toDoTask = Converter.Convert2DAL(toDoTaskDto);
            Database.ToDoTasks.Create(toDoTask);
        }
        public void AddClassification(ClassificationDTO classificationDto)
        {
            var classification = Converter.Convert2DAL(classificationDto);
            Database.Classifications.Create(classification);
        }
        public void AddPicture(PictureDTO pictureDto)
        {
            var picture = Converter.Convert2DAL(pictureDto);
            Database.Pictures.Create(picture);
        }

        public IEnumerable<ToDoTaskDTO> GetToDoTasks()
        {
            return Converter.Convert2Dto(Database.ToDoTasks.GetAll());
        }
        public IEnumerable<ClassificationDTO> GetClassifications()
        {
            return Converter.Convert2Dto(Database.Classifications.GetAll());
        }
        public IEnumerable<PictureDTO> GetPictures()
        {
            return Converter.Convert2Dto(Database.Pictures.GetAll());
        }

        public ToDoTaskDTO GetToDoTask(int? id)
        {
            if (id == null)
                throw new ValidationException("No ID is found. ID is required.", "");
            var toDoTask = Database.ToDoTasks.Get(id.Value);
            if (toDoTask == null)
                throw new ValidationException("Task with this ID can't be found", "");
            return Converter.Convert2Dto(toDoTask);
        }
        public ClassificationDTO GetClassification(int? id)
        {
            if (id == null)
                throw new ValidationException("No ID is found. ID is required.", "");
            var classification = Database.Classifications.Get(id.Value);
            if (classification == null)
                throw new ValidationException("Task with this ID can't be found", "");
            return Converter.Convert2Dto(classification);
        }
        public PictureDTO GetPicture(int? id)
        {
            if (id == null)
                throw new ValidationException("No ID is found. ID is required.", "");
            var picture = Database.Pictures.Get(id.Value);
            if (picture == null)
                throw new ValidationException("Task with this ID can't be found", "");
            return Converter.Convert2Dto(picture);
        }

        public IEnumerable<ToDoTaskDTO> GetToDoTasksOf(string id)
        {
            var listOfTaskForUser = Database.ToDoTasks.Find(t => t.UserId == id);
            return Converter.Convert2Dto(listOfTaskForUser);
        }
        public IEnumerable<ClassificationDTO> GetClassificationsOf(string id)
        {
            var classifications = Database.Classifications.Find(t => t.UserId == id);
            return Converter.Convert2Dto(classifications);
        }
        public IEnumerable<PictureDTO> GetPicturesOf(string id)
        {
            var pictures = Database.Pictures.Find(t => t.UserId == id);
            return Converter.Convert2Dto(pictures);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}