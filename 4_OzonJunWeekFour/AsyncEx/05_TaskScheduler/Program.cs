
namespace _05_TaskScheduler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    public class YetAnotherOneTaskScheduler : TaskScheduler
    {
        protected override IEnumerable<Task>? GetScheduledTasks()
        {
            throw new NotImplementedException();
        }

        protected override void QueueTask(Task task)
        {
            throw new NotImplementedException();
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            throw new NotImplementedException();
        }
    }

    public class SynchronizationContext
    {
        public virtual void Send(SendOrPostCallback d, Object state)
        {
            d(state);
        }
        public virtual void Post(SendOrPostCallback d, Object state)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(d), state);
        }
    }

    public sealed class WindowsFormsSynchronizationContext : SynchronizationContext, IDisposable
    {
        private static Control controlToSendTo;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            controlToSendTo?.BeginInvoke(d, new object[] { state });
        }
    }
}
