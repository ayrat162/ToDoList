using System.Collections.Generic;
using ToDoList.Models.DTO;

namespace ToDoList.Core.Interfaces
{
    public interface IToDoListService
    {
        int AddToDoTask(ToDoTaskDTO toDoTaskDto);
        ToDoTaskDTO GetToDoTask(int? id);
        IEnumerable<ToDoTaskDTO> GetToDoTasks();
        void Dispose();
        void UpdateToDoTask(ToDoTaskDTO toDoTaskDto);
        void DeleteToDoTask(int? id);
    }
}