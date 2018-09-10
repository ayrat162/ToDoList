using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Core.DTO;
using ToDoList.DAL.Entities;

namespace ToDoList.Core.Helpers
{
    public static class Converter
    {
        public static IEnumerable<ToDoTaskDTO> Convert2Dto(IEnumerable<ToDoTask> toDoTasks)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTask, ToDoTaskDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ToDoTask>, IEnumerable<ToDoTaskDTO>>(toDoTasks);
        }
        public static ToDoTaskDTO Convert2Dto(ToDoTask toDoTask)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTask, ToDoTaskDTO>()).CreateMapper();
            return mapper.Map<ToDoTask, ToDoTaskDTO>(toDoTask);
        }
        public static IEnumerable<PictureDTO> Convert2Dto(IEnumerable<Picture> pictures)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<IEnumerable<Picture>, IEnumerable<PictureDTO>>()).CreateMapper();
            return mapper.Map<IEnumerable<Picture>, IEnumerable<PictureDTO>>(pictures);
        }
        public static PictureDTO Convert2Dto(Picture picture)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Picture, PictureDTO>()).CreateMapper();
            return mapper.Map<Picture, PictureDTO>(picture);
        }
        public static IEnumerable<ClassificationDTO> Convert2Dto(IEnumerable<Classification> classifications)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<IEnumerable<Classification>, IEnumerable<ClassificationDTO>>()).CreateMapper();
            return mapper.Map<IEnumerable<Classification>, IEnumerable<ClassificationDTO>>(classifications);
        }
        public static ClassificationDTO Convert2Dto(Classification classification)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Classification, ClassificationDTO>()).CreateMapper();
            return mapper.Map<Classification, ClassificationDTO>(classification);
        }



        public static IEnumerable<ToDoTask> Convert2DAL(IEnumerable<ToDoTaskDTO> toDoTasks)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTaskDTO, ToDoTask>()).CreateMapper();
            return mapper.Map<IEnumerable<ToDoTaskDTO>, IEnumerable<ToDoTask>>(toDoTasks);
        }
        public static ToDoTask Convert2DAL(ToDoTaskDTO toDoTask)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTaskDTO, ToDoTask>()).CreateMapper();
            return mapper.Map<ToDoTaskDTO, ToDoTask>(toDoTask);
        }
        public static IEnumerable<Picture> Convert2DAL(IEnumerable<PictureDTO> pictures)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<IEnumerable<PictureDTO>, IEnumerable<Picture>>()).CreateMapper();
            return mapper.Map<IEnumerable<PictureDTO>, IEnumerable<Picture>>(pictures);
        }
        public static Picture Convert2DAL(PictureDTO picture)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PictureDTO, Picture>()).CreateMapper();
            return mapper.Map<PictureDTO, Picture>(picture);
        }
        public static IEnumerable<Classification> Convert2DAL(IEnumerable<ClassificationDTO> classifications)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<IEnumerable<ClassificationDTO>, IEnumerable<Classification>>()).CreateMapper();
            return mapper.Map<IEnumerable<ClassificationDTO>, IEnumerable<Classification>>(classifications);
        }
        public static Classification Convert2DAL(ClassificationDTO classification)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ClassificationDTO, Classification>()).CreateMapper();
            return mapper.Map<ClassificationDTO, Classification>(classification);
        }

    }
}