using System.ComponentModel.DataAnnotations;

namespace TaskExecutor.Enums
{
    public enum NodeExecutionStatus
    {
        [Display(Name = "Idle")]
        Idle = 10,

        [Display(Name = "Busy")]
        Busy
    }
}
