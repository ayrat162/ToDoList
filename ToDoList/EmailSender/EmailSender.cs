using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailSender.Handlers;
using ToDoList.Models;
using DateTime = System.DateTime;

namespace EmailSender
{
    class EmailSender
    {
        static void Main(string[] args)
        {
            var text = "";
            Repository.Connect();
            foreach (var toDoTask in Repository.GetTasks())
            {
                text += $"<p>{DateTime.Now} : {toDoTask.Description}</p>";
            }
            EmailHandler.SendMessage(text);
        }
    }
}
