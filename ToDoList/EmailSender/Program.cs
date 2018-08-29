using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace EmailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");
            Repository.Connect();
            Console.WriteLine("Connected");
            int i = 0;
            foreach (var toDoTask in Repository.GetTasks())
            {
                Console.WriteLine(i++);
                Console.WriteLine(toDoTask.Description);
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
