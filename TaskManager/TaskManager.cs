using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();
        private List<User> users = new List<User>();

        public List<Task> GetTasks()
        {
            return tasks
                .OrderBy(t => t.Deadline)
                .ThenByDescending(t => t.Priority)
                .ToList();
        }
        public Task GetTask(Guid taskId)
        {
            return tasks.Find(t => t.Id == taskId);
        }
        public void AddTask(string title, string description, Priority priority, DateTime deadline)
        {
            Task newTask = new Task(title, description, priority, deadline);
            tasks.Add(newTask);
        }
        public void EditTask(Guid taskId, Action<Task> updatedTask)
        {
            Task existingTask = GetTask(taskId);
            if (existingTask != null)
            {
                updatedTask(existingTask);
            }
            else
            {
                throw new ArgumentException("Task not found", nameof(taskId));
            }
        }
        public void DeleteTask(Guid taskId)
        {
            tasks.RemoveAll(t => t.Id == taskId);
        }

        public void AssignedTask(Guid taskId, string userEmail)
        {
            Task existingTask = GetTask(taskId);
            if (existingTask != null)
            {
                User user = users.Find(u => u.Email == userEmail);
                if (user == null)
                {
                    user = new User(userEmail);
                    users.Add(user);
                    Console.WriteLine($"New user created with ID: {user.Id}");
                }
                existingTask.AssignedUserId = user.Id;
            }
        }
    }
}
