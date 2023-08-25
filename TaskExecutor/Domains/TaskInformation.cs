using Status = TaskExecutor.Enums.Status;

namespace TaskExecutor.Domains
{
    public class TaskInformation
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public TaskInformation(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Status = Status.Pending;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
