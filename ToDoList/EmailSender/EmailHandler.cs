using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models.DTO;

namespace EmailSender
{
    public static class Helper
    {
        public static string CompileEmail(ToDoTaskDTO task)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Email: " + task.UserEmail);
            sb.AppendLine("Description: " + task.Description);
            sb.AppendLine("User Name " + task.UserName);
            sb.AppendLine("Due DateTime: " + task.DueDateTime);
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
