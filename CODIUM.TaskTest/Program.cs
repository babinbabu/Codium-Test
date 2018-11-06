using CODIUM.TaskTest.Services;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODIUM.TaskTest
{
    class Program
    {
        static void Main(string[] args)
        {

            TaskService.GetTasks();
            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("Please Enter the Task Name");
                var taskName = Console.ReadLine();
                taskName = taskName.Trim();
                if (string.IsNullOrEmpty(taskName))
                {
                    break;
                }
                TaskService.AddTask(taskName);

            }
            TaskService.GetTasks();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Please Enter the Task Id to delete the Task");
            var taskId = Console.ReadLine();
            TaskService.DeleteTask(taskId);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Task List after Delete\n");
            TaskService.GetTasks();
            Console.ReadLine();
        }
    }
}
