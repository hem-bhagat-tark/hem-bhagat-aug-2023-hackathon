using Microsoft.OpenApi.Extensions;
using System.Net;
using TaskExecutor.Domains;
using TaskExecutor.Enums;
using TaskExecutor.Exceptions;
using TaskExecutor.Models;

namespace TaskExecutor.Services.Impl
{
    public class TaskService : ITaskService
    {
        private static readonly IDictionary<Guid, TaskInformation> Tasks = new Dictionary<Guid, TaskInformation>();

        public TaskResponse CreateTask(string name)
        {
            // NOTE: this allows user to specify tasks with same name
            var task = new TaskInformation(name);
            Tasks.Add(task.Id, task);

            return Map(task);
        }

        public List<TaskResponse> GetAllTasks()
        {
            var tasks = Tasks.Values.ToList();
            return MapList(tasks);
        }

        public List<TaskResponse> GetTasksByStatus(Status taskStatus)
        {
            var tasks = Tasks.Values.Where(_ => _.Status == taskStatus).ToList();
            return MapList(tasks);
        }

        public TaskResponse GetTask(Guid taskId)
        {
            if (!Tasks.ContainsKey(taskId))
                throw new BusinessException(HttpStatusCode.NotFound, $"Task with id {taskId} not found.");

            var task = Tasks[taskId];
            return Map(task);
        }

        public void UpdateTaskStatus(Guid taskId, Status taskStatus)
        {
            if (!Tasks.ContainsKey(taskId))
                throw new BusinessException(HttpStatusCode.NotFound, $"Task with id {taskId} not found.");

            var task = Tasks[taskId];
            task.Status = taskStatus;
        }

        private List<TaskResponse> MapList(List<TaskInformation> tasks)
        {
            return tasks.Select(task => Map(task)).ToList();
        }

        private TaskResponse Map(TaskInformation task)
        {
            // ENHANCEMENT: Make use of Automapper
            return new TaskResponse
            {
                Id = task.Id,
                Name = task.Name,
                Status = task.Status.GetDisplayName(),
                CreatedAt = task.CreatedAt,
            };
        }
    }
}
