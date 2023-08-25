using Microsoft.AspNetCore.Mvc;
using TaskExecutor.Enums;
using TaskExecutor.Services;

namespace TaskExecutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly ITaskService _taskPersister;
        private readonly IScheduleService _scheduleService;

        public TasksController(ITaskService taskPersister, IScheduleService scheduleService)
        {
            _taskPersister = taskPersister;
            _scheduleService = scheduleService;
        }

        [HttpGet]
        [Route("{taskId}")]
        public IActionResult GetTask([FromRoute] Guid taskId)
        {
            var task = _taskPersister.GetTask(taskId);
            return Ok(task);
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var tasks = _taskPersister.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet]
        [Route("status/{taskStatus}")]
        public IActionResult GetTasksByStatus([FromRoute] Status taskStatus)
        {
            var tasks = _taskPersister.GetTasksByStatus(taskStatus);
            return Ok(tasks);
        }

        [HttpGet]
        [Route("/queue")]
        public IActionResult GetTasksQueue()
        {
            var tasks = _scheduleService.GetTasksQueue();
            return Ok(tasks);
        }

        [HttpPost]
        [Route("{taskName}")]
        public IActionResult SubmitTask([FromRoute] string taskName)
        {
            // create a task
            var task = _taskPersister.CreateTask(taskName);

            // send the task to scheduler
            _scheduleService.ScheduleTask(task.Id);

            // return the created task data
            return Ok(task);
        }
    }
}
