using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models.DTO;
using ToDoList.Models.Entities;

namespace ToDoList.Core.Helpers
{
    public static class Converter
    {
        public static IEnumerable<ToDoTaskDTO> Convert2Dto(IEnumerable<ToDoTask> toDoTasks)
        {
            if (toDoTasks == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTask, ToDoTaskDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ToDoTask>, IEnumerable<ToDoTaskDTO>>(toDoTasks);
        }
        public static ToDoTaskDTO Convert2Dto(ToDoTask toDoTask)
        {
            if (toDoTask == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTask, ToDoTaskDTO>()).CreateMapper();
            return mapper.Map<ToDoTask, ToDoTaskDTO>(toDoTask);
        }
        public static IEnumerable<PictureDTO> Convert2Dto(IEnumerable<Picture> pictures)
        {
            if (pictures == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Picture, PictureDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Picture>, IEnumerable<PictureDTO>>(pictures);
        }
        public static PictureDTO Convert2Dto(Picture picture)
        {
            if (picture == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Picture, PictureDTO>()).CreateMapper();
            return mapper.Map<Picture, PictureDTO>(picture);
        }
        public static IEnumerable<ClassificationDTO> Convert2Dto(IEnumerable<Classification> classifications)
        {
            if (classifications == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Classification, ClassificationDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Classification>, IEnumerable<ClassificationDTO>>(classifications);
        }
        public static ClassificationDTO Convert2Dto(Classification classification)
        {
            if (classification == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Classification, ClassificationDTO>()).CreateMapper();
            return mapper.Map<Classification, ClassificationDTO>(classification);
        }
        public static UserDTO Convert2Dto(ApplicationUser user)
        {
            if (user == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, UserDTO>()).CreateMapper();
            return mapper.Map<ApplicationUser, UserDTO>(user);
        }
        public static IEnumerable<UserDTO> Convert2Dto(IEnumerable<ApplicationUser> users)
        {
            if (users == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserDTO>>(users);
        }

        public static ToDoTask Convert2Dal(ToDoTaskDTO toDoTask)
        {
            if (toDoTask == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTaskDTO, ToDoTask>()).CreateMapper();
            return mapper.Map<ToDoTaskDTO, ToDoTask>(toDoTask);
        }
        public static Picture Convert2Dal(PictureDTO picture)
        {
            if (picture == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PictureDTO, Picture>()).CreateMapper();
            return mapper.Map<PictureDTO, Picture>(picture);
        }
        public static Classification Convert2Dal(ClassificationDTO classification)
        {
            if (classification == null) return null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClassificationDTO, Classification>()).CreateMapper();
            return mapper.Map<ClassificationDTO, Classification>(classification);
        }
    }
}