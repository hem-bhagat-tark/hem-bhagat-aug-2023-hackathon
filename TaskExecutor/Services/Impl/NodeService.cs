using Microsoft.OpenApi.Extensions;
using RestSharp;
using System.Net;
using TaskExecutor.Domains;
using TaskExecutor.Enums;
using TaskExecutor.Events;
using TaskExecutor.Exceptions;
using TaskExecutor.Models;

namespace TaskExecutor.Services.Impl
{
    public class NodeService : INodeService
    {
        private static readonly IDictionary<Guid, NodeInformation> Nodes = new Dictionary<Guid, NodeInformation>();

        private readonly ITaskService _taskService;
        private readonly IRestService _restService;

        public NodeService(ITaskService taskService, IRestService restService)
        {
            _taskService = taskService;
            _restService = restService;
        }

        public List<NodeResponse> GetAllNodes()
        {
            var nodes = Nodes.Values.ToList();
            return MapList(nodes);
        }

        public NodeResponse? GetFirstIdleNode()
        {
            var node = Nodes.Values
                .Where(_ => _.Connection == NodeConnectionStatus.Online)
                .FirstOrDefault(_ => _.Status == NodeExecutionStatus.Idle);

            return node == null ? null : Map(node);
        }

        public NodeResponse RegisterNode(NodeRegistrationRequest request)
        {
            ValidateDuplicateAddress(request.Address);

            var node = new NodeInformation(request.Name, request.Address);
            Nodes.Add(node.Id, node);

            return Map(node);
        }

        public void UpdateNodeStatus(Guid nodeId, NodeExecutionStatus status)
        {
            if (!Nodes.ContainsKey(nodeId))
                throw new BusinessException(HttpStatusCode.NotFound, $"Node with id {nodeId} not found");

            var node = Nodes[nodeId];
            node.Status = status;
        }

        public void UpdateNodeConnection(Guid nodeId, NodeConnectionStatus connection)
        {
            if (!Nodes.ContainsKey(nodeId))
                throw new BusinessException(HttpStatusCode.NotFound, $"Node with id {nodeId} not found");

            var node = Nodes[nodeId];
            node.Connection = connection;
        }

        public void StartTaskOnNode(NodeResponse node, Guid taskId)
        {
            UpdateNodeStatus(node.Id, NodeExecutionStatus.Busy);

            Task.Run(async () =>
            {
                try
                {
                    _taskService.UpdateTaskStatus(taskId, Status.Running);

                    await _restService.PostAsync(node.Address);

                    _taskService.UpdateTaskStatus(taskId, Status.Running);
                }
                catch
                {
                    _taskService.UpdateTaskStatus(taskId, Status.Failed);
                }
                finally
                {
                    UpdateNodeStatus(node.Id, NodeExecutionStatus.Idle);
                    NodeAvailableEvent.Invoke(null, null);
                }
            });
        }

        private void ValidateDuplicateAddress(string address)
        {
            var duplicateAddress = Nodes.Values.Any(_ => _.Address == address);
            if (duplicateAddress)
                throw new BusinessException(HttpStatusCode.BadRequest, $"Node with address {address} already registered");
        }

        private List<NodeResponse> MapList(List<NodeInformation> nodeInformation)
        {
            return nodeInformation.Select(nodeInformation => Map(nodeInformation)).ToList();
        }

        private NodeResponse Map(NodeInformation nodeInformation)
        {
            return new NodeResponse
            {
                Id = nodeInformation.Id,
                Name = nodeInformation.Name,
                Address = nodeInformation.Address,
                Connection = nodeInformation.Connection.GetDisplayName(),
                Status = nodeInformation.Connection == Enums.NodeConnectionStatus.Offline ? "N/A" : nodeInformation.Status.GetDisplayName(),
                RegisteredAt = nodeInformation.RegisteredAt,
            };
        }
    }
}
