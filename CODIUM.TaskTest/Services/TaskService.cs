using CODIUM.TaskTest.Model;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODIUM.TaskTest.Services
{
    public class TaskService
    {
        /// <summary>
        /// GetTasks
        /// </summary>
        public static void GetTasks()
        {
            var results = new List<UserTask>();
            using (var radis = new RedisClient())
            {
                var radisTask = radis.As<UserTask>();
                var userTasksList = radisTask.GetAll();

                foreach (var userTasks in userTasksList)
                {
                    results.Add(new UserTask(userTasks));
                }
                if (results.Count() > 0)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("######### User Task List ######### \n");
                    Console.BackgroundColor = ConsoleColor.Black;
                    foreach (var result in results)
                    {
                        Console.WriteLine(result.Id + ":" + result.TaskName + "\n");
                    }
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("######### User Task List ######### \n");
                }

            }
        }
        /// <summary>
        /// AddTask
        /// </summary>
        /// <param name="TaskName"></param>
        public static void AddTask(string TaskName)
        {
            if (!string.IsNullOrEmpty(TaskName))
            {
                try
                {
                    var result = new List<UserTask>();
                    using (var radis = new RedisClient())
                    {
                        var radisTask = radis.As<UserTask>();
                        var test = new UserTask { Id = radisTask.GetNextSequence(), TaskName = TaskName };
                        radisTask.Store(test);
                    }
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nSuccessfully Insetred the task\n");
                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nAn Error Occured : " + ex.Message + "\n");
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter a valid Task Name\n");
            }


        }
        /// <summary>
        /// DeleteTask
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteTask(object id)
        {
            string currentTaskId = id.ToString().Trim();
            if (!string.IsNullOrEmpty(currentTaskId))
            {
                using (var radis = new RedisClient())
                {
                    try
                    {
                        var radisTask = radis.As<UserTask>();
                        if (radisTask.GetById(currentTaskId) == null)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.WriteLine("This Task Id does not exists\n");
                        }
                        else
                        {

                            radisTask.DeleteById(id);
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.WriteLine("Successfully Deleted the task\n");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("An Error Occured : " + ex.Message+ "\n");
                    }
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter a valid Task Id\n");
            }

        }
    }
}
