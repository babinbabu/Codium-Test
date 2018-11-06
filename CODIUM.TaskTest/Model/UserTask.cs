using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODIUM.TaskTest.Model
{
    public class UserTask
    {
        public long Id { get; set; }
        public string TaskName { get; set; }
        public UserTask()
        {

        }
        public UserTask(UserTask userTask )
        {
            Id = userTask.Id;
            TaskName = userTask.TaskName;
        }
    }
}
