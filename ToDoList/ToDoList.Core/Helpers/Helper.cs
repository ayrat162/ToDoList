using System.Linq;
using EmailService;
using ToDoList.Core.Services;
using ToDoList.Models.DTO;

namespace ToDoList.Core.Helpers
{
    public class Helper
    {
        public static void SendApprovalEmail(ToDoTaskDTO toDoTaskDto)
        {
            var userService = new UserService();
            var user = userService.GetUser(toDoTaskDto.UserId);
            var emailText = "Dear sir/madam,\n";
            emailText += "Please approve the following task\n";
            emailText += toDoTaskDto.Description + "\n";
            emailText += "that was created for " + user.Name + "\n";
            emailText += "Go to the following page to approve:\n";
            emailText += "http://localhost:52001/tasks/" + toDoTaskDto.Id;
            
            var emailTo = user.Email + "; ";
            emailTo += userService.GetUser(toDoTaskDto.UserId).Email;
            var bosses = userService.GetBosses(toDoTaskDto.UserId);
            foreach (var boss in bosses)
            {
                emailTo += boss.Email + "; ";
            }

            Sender.SendFakeMessage(text: emailText, emailTo: emailTo);
        }

        public static bool CanEditTask(string userId, int taskId)
        {
            var userService = new UserService();
            var taskService = new ToDoListService();
            var task = taskService.GetToDoTask(taskId);
            var taskUserId = task.UserId;

            if (taskUserId == userId) return true;

            var bossIds = userService.GetBosses(taskUserId).Select(b => b.Id);
            if (bossIds.Contains(userId)) return true;

            var adminIds = userService.GetAdminIds();
            if (adminIds.Contains(userId)) return true;

            return false;
        }
    }
}