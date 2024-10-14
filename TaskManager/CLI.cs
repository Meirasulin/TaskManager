using System;
using System.Collections.Generic;

namespace TaskManager
{
    public class CLI
    {
        private TaskManager taskManager;

        public CLI(TaskManager taskManager)
        {
            this.taskManager = taskManager;
        }

        public void Run()
        {
            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllTasks();
                        break;
                    case "2":
                        AddNewTask();
                        break;
                    case "3":
                        EditTask();
                        break;
                    case "4":
                        DeleteTask();
                        break;
                    case "5":
                        AssignTaskToUser();
                        break;
                    case "0":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void DisplayMenu()
        {
            Console.WriteLine("Task Management System");
            Console.WriteLine("1. Display all tasks");
            Console.WriteLine("2. Add a new task");
            Console.WriteLine("3. Edit a task");
            Console.WriteLine("4. Delete a task");
            Console.WriteLine("5. Assign a task to a user");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
        }

        private void DisplayAllTasks()
        {
            var tasks = taskManager.GetTasks();
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            foreach (var task in tasks)
            {
                    string dataDisply = $"ID: {task.Id}, Title: {task.Title}, Priority: {task.Priority}, Status: {task.Status}, Deadline: {task.Deadline.ToShortDateString()}";
                if (task.AssignedUserId != Guid.Empty)
                {
                    User user = taskManager.getUser(task.AssignedUserId);
                    Console.WriteLine(dataDisply + $" Assigned: {user.Email}");
                }
                Console.WriteLine(dataDisply);
            }
        }

        private void AddNewTask()
        {
            Console.Write("Enter task title: ");
            string title = Console.ReadLine();

            Console.Write("Enter task description: ");
            string description = Console.ReadLine();

            Console.Write("Enter task priority (Low/Medium/High): ");
            if (Enum.TryParse<Priority>(Console.ReadLine(), true, out Priority priority))
            {
                Console.Write("Enter task deadline (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime deadline))
                {
                    try
                    {
                        taskManager.AddTask(title, description, priority, deadline);
                        Console.WriteLine("Task added successfully.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format.");
                }
            }
            else
            {
                Console.WriteLine("Invalid priority.");
            }
        }

        private void EditTask()
        {
            Console.Write("Enter task ID to edit: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid taskId))
            {
                try
                {
                    taskManager.EditTask(taskId, task =>
                    {
                        Console.WriteLine("Enter new values (press Enter to skip):");

                        Console.Write("Title: ");
                        string title = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(title))
                            task.Title = title;

                        Console.Write("Description: ");
                        string description = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(description))
                            task.Description = description;

                        Console.Write("Priority (Low/Medium/High): ");
                        string priorityInput = Console.ReadLine();
                        if (Enum.TryParse<Priority>(priorityInput, true, out Priority priority))
                            task.Priority = priority;

                        Console.Write("Deadline (yyyy-MM-dd): ");
                        string deadlineInput = Console.ReadLine();
                        if (DateTime.TryParse(deadlineInput, out DateTime deadline))
                            task.Deadline = deadline;

                        Console.Write("Status (Backlog/InProgress/Completed): ");
                        string statusInput = Console.ReadLine();
                        if (Enum.TryParse<Status>(statusInput, true, out Status status))
                            task.Status = status;
                    });
                    Console.WriteLine("Task updated successfully.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid task ID.");
            }
        }

        private void DeleteTask()
        {
            Console.Write("Enter task ID to delete: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid taskId))
            {
                taskManager.DeleteTask(taskId);
                Console.WriteLine("Task deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid task ID.");
            }
        }

        private void AssignTaskToUser()
        {
            Console.Write("Enter task ID: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid taskId))
            {
                Console.Write("Enter user email: ");
                string userEmail = Console.ReadLine();
                try
                {
                    taskManager.AssignedTask(taskId, userEmail);
                    Console.WriteLine("Task assigned successfully.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid task ID.");
            }
        }
    }
}