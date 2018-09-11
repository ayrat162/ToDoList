using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ToDoList.Core.DTO;
using ToDoList.Web.Models;
using ToDoList.Web.ViewModels;

namespace ToDoList.Web.Helpers
{
    public static class Converter
    {
        public static IEnumerable<TaskViewModel> Dto2ViewModel(IEnumerable<ToDoTaskDTO> IEnumToDoTaskDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTaskDTO, TaskViewModel>()).CreateMapper();
            var toDoTasks = mapper.Map<IEnumerable<ToDoTaskDTO>, IEnumerable<TaskViewModel>>(IEnumToDoTaskDto);
            return toDoTasks;
        }
        public static TaskViewModel Dto2ViewModel(ToDoTaskDTO ToDoTaskDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTaskDTO, TaskViewModel>()).CreateMapper();
            var toDoTasks = mapper.Map<ToDoTaskDTO, TaskViewModel>(ToDoTaskDto);
            return toDoTasks;
        }

        public static TaskViewModel Dto2ViewModel()
        {
            return new TaskViewModel();
        }



        public static byte[] File2Picture(HttpPostedFileBase uploadImage)
        {
            byte[] imageData;
            using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
            return imageData;
        }
    }
}