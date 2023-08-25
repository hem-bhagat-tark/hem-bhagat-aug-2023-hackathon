using TaskExecutor.Enums;

namespace TaskExecutor.Domains
{
    public class NodeInformation
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public NodeConnectionStatus Connection { get; set; }

        public NodeExecutionStatus Status { get; set; }

        public DateTimeOffset RegisteredAt { get; set; }

        public NodeInformation(string name, string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            Connection = NodeConnectionStatus.Online;
            Status = NodeExecutionStatus.Idle;
            RegisteredAt = DateTimeOffset.UtcNow;
        }
    }
}
