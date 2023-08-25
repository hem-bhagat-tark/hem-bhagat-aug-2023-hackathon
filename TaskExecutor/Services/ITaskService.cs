using TaskExecutor.Enums;
using TaskExecutor.Models;

namespace TaskExecutor.Services
{
    public interface ITaskService
    {
        TaskResponse CreateTask(string name);

        TaskResponse GetTask(Guid taskId);

        List<TaskResponse> GetAllTasks();

        List<TaskResponse> GetTasksByStatus(Status taskStatus);

        void UpdateTaskStatus(Guid taskId, Status taskStatus);
    }
}
