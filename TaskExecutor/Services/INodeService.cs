using TaskExecutor.Domains;
using TaskExecutor.Enums;
using TaskExecutor.Models;

namespace TaskExecutor.Services
{
    public interface INodeService
    {
        NodeResponse RegisterNode(NodeRegistrationRequest request);

        NodeResponse? GetFirstIdleNode();

        List<NodeResponse> GetAllNodes();

        void UpdateNodeStatus(Guid nodeId, NodeExecutionStatus status);

        void UpdateNodeConnection(Guid nodeId, NodeConnectionStatus connection);

        void StartTaskOnNode(NodeResponse node, Guid taskId);
    }
}
