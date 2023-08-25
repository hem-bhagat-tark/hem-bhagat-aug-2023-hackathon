using TaskExecutor.Domains;
using TaskExecutor.Models;

namespace TaskExecutor.Services.Impl
{
    public class ScheduleService : IScheduleService
    {
        private static readonly Queue<Guid> TasksQueue = new Queue<Guid>();

        private readonly INodeService _nodeService;
        private readonly ITaskService _taskService;

        public ScheduleService(INodeService nodeService, ITaskService taskService)
        {
            _nodeService = nodeService;
            _taskService = taskService;
        }

        public List<TaskResponse> GetTasksQueue()
        {
            var taskIds = TasksQueue.ToList();
            return _taskService.GetTasksByIds(taskIds);
        }

        public void ScheduleTask(Guid taskId)
        {
            var node = _nodeService.GetFirstIdleNode();
            if(node == null)
            {
                TasksQueue.Enqueue(taskId);
            }

            _nodeService.StartTaskOnNode(node, taskId);
        }

        public void ScheduleNextTaskInQueue()
        {
            var taskId = TasksQueue.Dequeue();
            ScheduleTask(taskId);
        }
    }
}
