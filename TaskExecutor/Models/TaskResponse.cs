using TaskStatus = TaskExecutor.Enums.Status;
namespace TaskExecutor.Models
{
    public class TaskResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
