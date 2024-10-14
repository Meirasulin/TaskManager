using System;

namespace TaskManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();
            CLI cli = new CLI(taskManager);
            cli.Run();
        }
    }
}