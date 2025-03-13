using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Security.Cryptography.X509Certificates;

namespace TaskTracker_CLI.classes
{
    internal class TaskManager
    {
        private List<Task> tasks = new List<Task>();
        private readonly string filePath = "task.json";

        public TaskManager()
        {
            tasks = LoadTasks();
        }

        public void addTask(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Error: Task title cannot be empty.");
                return;
            }
            else
            {
                int newId = tasks.Count > 0 ? tasks.Max(t => t.id) + 1 : 1;
                var task = new Task(newId, title);
                tasks.Add(task);
                SaveTasks();
                Console.WriteLine($"Task added: {task.title} (ID: {task.id})");
            }
        }
        public void updateTask(int id,string newtitle)
        {
            if (string.IsNullOrWhiteSpace(newtitle))
            {
                Console.WriteLine("Error: New title cannot be empty.");
                return;
            }
            var task = tasks.Find(t => t.id == id);
            if (task == null)
            {
                Console.WriteLine($"Error: Task with ID {id} not found.");
                return;
            }

            //task.title = newtitle;
            else {
                Console.WriteLine($"Before update: Title={task.title}"); // Debug
                task.title = newtitle;
                Console.WriteLine($"After update: Title={task.title}");
                SaveTasks();
                Console.WriteLine($"Task updated: {task.title} (ID: {id})");
            }
            


        }

        public void deleteTask(int id)
        {
           var task = tasks.Find(t => t.id == id);

           if (task != null)
            {
                tasks.Remove(task);
            }
           else
            {
                Console.WriteLine($"Error: Task with ID {id} not found.");
                return;
            }
            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].id = i + 1;
            }
            SaveTasks();
            Console.WriteLine($"Task deleted: ID {id}");

        }
        private void SaveTasks()
        {
            try
            {
                // Serialize the list of tasks to JSON with indentation for readability
                string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                // Write the JSON string to the file
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tasks to JSON file: {ex.Message}");
            }
        }
        private List<Task> LoadTasks()
        {
            try
            {
                // Check if the file exists; if not, create an empty one
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, "[]"); // Empty JSON array
                    return new List<Task>();
                }

                // Read the JSON string from the file
                string json = File.ReadAllText(filePath);

                // Deserialize the JSON string into a List<Task>
                return JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                return new List<Task>(); // Return an empty list on failure
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tasks from JSON file: {ex.Message}");
                return new List<Task>(); // Return an empty list on failure
            }
            
        }
        public void markINProgress(int id)
        {
            var task = tasks.Find(x => x.id == id);
            if (task != null)
            {
                task.status = "in-progress";
                SaveTasks();
                Console.WriteLine($"Task marked as in-progress: {task.title} (ID: {id})");
            }
            else
            {
                Console.WriteLine($"Error: Task with ID {id} not found.");
                return;
            }

        }
        public void markDone(int id)
        {
            var task = tasks.Find(x => x.id == id);
            if (task != null)
            {
                task.status = "done";
                SaveTasks();
                Console.WriteLine($"Task marked as done: {task.title} (ID: {id})");
            }
            else
            {
                Console.WriteLine($"Error: Task with ID {id} not found.");
                return;
            }

        }
        public void listAllTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("There are no tasks to list");
                return;

            }
            else
            {
                foreach(var task in tasks)
                {
                    Console.WriteLine($"ID: {task.id}, Title: {task.title}, Status: {task.status}");
                }
            }
        }
        public void listDone()
        {
            var donetasks = tasks.Where(x => x.status == "done").ToList();
            if (donetasks.Count == 0)
            {
                Console.WriteLine("There are not tasks done.");
                return;
            }
            foreach(var task in donetasks)
            {
                Console.WriteLine($"ID: {task.id}, Title: {task.title}, Status: {task.status}");
            }
        }
        public void listInProgress()
        {
            var inProgresstasks = tasks.Where(x => x.status == "in-progress").ToList();
            if (inProgresstasks.Count == 0)
            {
                Console.WriteLine("There are not tasks in progresss.");
                return;
            }
            foreach (var task in inProgresstasks)
            {
                Console.WriteLine($"ID: {task.id}, Title: {task.title}, Status: {task.status}");
            }

        }
        public void listToDo()
        {
            var itoDotasks = tasks.Where(x => x.status == "todo").ToList();
            if (itoDotasks.Count == 0)
            {
                Console.WriteLine("There are not tasks to do.");
                return;
            }
            foreach (var task in itoDotasks)
            {
                Console.WriteLine($"ID: {task.id}, Title: {task.title}, Status: {task.status}");
            }

        }


    }
}
