using System.ComponentModel.DataAnnotations;

namespace TaskExecutor.Enums
{
    public enum NodeConnectionStatus
    {
        [Display(Name = "Offline")]
        Offline = 10,

        [Display(Name = "Online")]
        Online = 20
    }
}
