using ToDoList.Core.Services;
using static EmailService.Sender;

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
