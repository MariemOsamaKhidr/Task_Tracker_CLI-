using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker_CLI.classes
{
    internal class Task
    {
        public int id { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        private DateTime created_at { get; set; }
        private DateTime updated_at { get; set; }
        public Task(int id,string title,string status="todo")
        {
            this.id = id;
            this.title = title;
            this.status = status;
        }




    }
}
