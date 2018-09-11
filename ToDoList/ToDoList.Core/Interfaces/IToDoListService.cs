using System.Collections.Generic;
using ToDoList.Core.DTO;

namespace ToDoList.Core.Interfaces
{
    public interface IToDoListService
    {
        int AddToDoTask(ToDoTaskDTO toDoTaskDto);
        ToDoTaskDTO GetToDoTask(int? id);
        IEnumerable<ToDoTaskDTO> GetToDoTasks();
        void Dispose();
    }
}