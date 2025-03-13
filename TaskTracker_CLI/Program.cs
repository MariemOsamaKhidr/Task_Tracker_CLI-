using TaskTracker_CLI.classes;

namespace TaskTracker_CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var task = new TaskManager();
            if (args.Length == 0)
            {
                PrintUsage();
                return;
            }
            var command = args[0].ToLower();
            try
            {
                switch (command)
                {
                    case "add":
                        if (args.Length < 2)
                        {
                            Console.WriteLine("Error: Please provide a task title.");
                            return;
                        }
                        task.addTask(string.Join(" ", args[1..]));
                        break;
                    case "update":
                        if (args.Length < 3 || !int.TryParse(args[1], out int update_id))
                        {
                            Console.WriteLine("Error: Please provide a valid task ID and new title.");
                            return;
                        }
                        task.updateTask(update_id, string.Join(" ", args[2..]));
                        break;
                    case "delete":
                        if (args.Length < 2 || !int.TryParse(args[1], out int delete_id))
                        {
                            Console.WriteLine("Error: Please provide a valid task ID.");
                            return;
                        }
                        task.deleteTask(delete_id);
                        break;
                    case "mark-in-progress":
                        if (args.Length < 2 || !int.TryParse(args[1], out int progress_id))
                        {
                            Console.WriteLine("Error: Please provide a valid task ID.");
                            return;
                        }
                        task.markINProgress(progress_id);
                        break;
                    case "mark-done":

                        if (args.Length < 2 || !int.TryParse(args[1], out int done_id))
                        {
                            Console.WriteLine("Error: Please provide a valid task ID.");
                            return;
                        }
                        task.markDone(done_id);
                        break;
                    case "list":
                        if (args.Length == 1)
                        {
                            task.listAllTasks();
                            return;
                        }
                        else
                        {
                            switch (args[1].ToLower())
                            {
                                case "done":
                                    task.listDone();
                                    break;
                                case "in-progress":
                                    task.listInProgress();
                                    break;
                                case "not-done":
                                    task.listToDo();
                                    task.listInProgress();
                                    break;
                                default:
                                    Console.WriteLine("Error: Invalid list filter. Use 'done', 'not-done', or 'in-progress'.");
                                    break;
                            }
                        }
                        break;
                    default:
                        PrintUsage();
                        break;
                }
            }

            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
            }




            }
           
        
        static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  add <title>                     Add a new task");
            Console.WriteLine("  update <id> <new-title>         Update a task's title");
            Console.WriteLine("  delete <id>                     Delete a task");
            Console.WriteLine("  mark-in-progress <id>           Mark a task as in progress");
            Console.WriteLine("  mark-done <id>                  Mark a task as done");
            Console.WriteLine("  list                            List all tasks");
            Console.WriteLine("  list done                       List all done tasks");
            Console.WriteLine("  list not-done                   List all tasks that are not done");
            Console.WriteLine("  list in-progress                List all tasks that are in progress");
        }
    }

}
