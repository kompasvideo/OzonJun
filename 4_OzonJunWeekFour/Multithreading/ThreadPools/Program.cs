namespace ThreadPools
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int workerThreads, iocpThreads;
            ThreadPool.GetAvailableThreads(out workerThreads, out iocpThreads);
            ThreadPool.GetMinThreads(out workerThreads, out iocpThreads);
            ThreadPool.GetMaxThreads(out workerThreads, out iocpThreads);
            var threadCount = ThreadPool.ThreadCount;

            ThreadPool.SetMinThreads(workerThreads:10, completionPortThreads:100);
            ThreadPool.SetMaxThreads(workerThreads:150, completionPortThreads:150);

            ThreadPool.GetMinThreads(out workerThreads, out iocpThreads);
            ThreadPool.GetMaxThreads(out workerThreads, out iocpThreads);

            ThreadPool.QueueUserWorkItem(DelegateForQueueWorkItem);
            ThreadPool.QueueUserWorkItem(DelegateForQueueWorkItem,
                new { Message = "spme state info" });
            ThreadPool.UnsafeQueueUserWorkItem(DelegateForQueueWorkItem,
                new { Message = "spme state info" });

            Console.WriteLine("Hello, World!");
        }

        public static void DelegateForQueueWorkItem(object stateInfo)
        {
            Console.WriteLine("DelegateForQueueWorkItem" + stateInfo);           
        }
    }
}
