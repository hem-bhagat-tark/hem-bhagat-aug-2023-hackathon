using TaskExecutor.Domains;
using TaskExecutor.Models;

namespace TaskExecutor.Services
{
    public interface IScheduleService
    {
        List<TaskResponse> GetTasksQueue();

        void ScheduleTask(Guid taskId);

        void ScheduleNextTaskInQueue();
    }
}
