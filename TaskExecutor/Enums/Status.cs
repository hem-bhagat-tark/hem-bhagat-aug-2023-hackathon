using System.ComponentModel.DataAnnotations;

namespace TaskExecutor.Enums
{
    public enum Status
    {
        [Display(Name = "Pending")]
        Pending = 10,

        [Display(Name = "Running")]
        Running = 20,

        [Display(Name = "Completed")]
        Completed = 30,

        [Display(Name = "Failed")]
        Failed = 40,

        [Display(Name = "Aborted")]
        Aborted = 50,
    }
}
