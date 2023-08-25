using TaskExecutor.Enums;

namespace TaskExecutor.Models
{
    public class NodeResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Connection { get; set; }

        public string Status { get; set; }

        public DateTimeOffset RegisteredAt { get; set; }
    }
}
