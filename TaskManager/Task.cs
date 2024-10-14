using System;
using System.Threading.Tasks;


namespace TaskManager
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum Status
    {
        InProgress,
        Completed,
        Backlog
    }
    public enum T
    {
        InProgress,
        Completed,
        Backlog
    }

    public class Task
    {
        public Guid Id { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public DateTime Deadline { get; set; }
        public Status Status { get; set; }
        public Guid AssignedUserId { get; private set; }

        public Task(string title, string description, Priority priority, DateTime deadline)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");

            if (deadline < DateTime.Now)
                throw new ArgumentException("Deadline cannot be in the past.");

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Priority = priority;
            Deadline = deadline;
            Status = Status.Backlog;
            Guid actualUserId = Guid.Empty;
        }
    }
}
