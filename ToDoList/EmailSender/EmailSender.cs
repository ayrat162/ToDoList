using ToDoList.Core.Services;
using static EmailService.EmailService;

namespace EmailSender
{
    public class EmailSender
    {
        static void Main(string[] args)
        {
            var toDoListService = new ToDoListService();
            var tasks = toDoListService.GetOldTasks();
            foreach (var task in tasks)
            {
                SendFakeMessage(Helper.CompileEmail(task));
            }
        }
    }
}
