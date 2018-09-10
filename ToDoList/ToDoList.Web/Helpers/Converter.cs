using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Core.DTO;
using ToDoList.Web.Models;
using ToDoList.Web.ViewModels;

namespace ToDoList.Web.Helpers
{
    public static class Converter
    {
        public static IEnumerable<ToDoTaskViewModel> Dto2ViewModel(IEnumerable<ToDoTaskDTO> IEnumToDoTaskDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTaskDTO, ToDoTaskViewModel>()).CreateMapper();
            var toDoTasks = mapper.Map<IEnumerable<ToDoTaskDTO>, IEnumerable<ToDoTaskViewModel>>(IEnumToDoTaskDto);
            return toDoTasks;
        }
        public static ToDoTaskViewModel Dto2ViewModel(ToDoTaskDTO ToDoTaskDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTaskDTO, ToDoTaskViewModel>()).CreateMapper();
            var toDoTasks = mapper.Map<ToDoTaskDTO, ToDoTaskViewModel>(ToDoTaskDto);
            return toDoTasks;
        }

        public static ToDoTaskViewModel Dto2ViewModel()
        {
            return new ToDoTaskViewModel();
        }
    }
}