using TaskExecutor.Services;

namespace TaskExecutor.Events
{
    public class NodeAvailableEvent
    {
        public static event EventHandler NodeAvailable;

        public NodeAvailableEvent(EventHandler eventHandler)
        {
            NodeAvailable += eventHandler;
        }

        public static void Invoke(object sender, EventArgs e)
        {
            NodeAvailable.Invoke(sender, e);
        }
    }
}
