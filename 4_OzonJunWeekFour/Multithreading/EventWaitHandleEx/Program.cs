namespace EventWaitHandleEx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Func();
            Console.WriteLine("Hello, World!");
        }

        private void Func()
        {
           using var autoEvent = new AutoResetEvent(false);

            for (int i = 0; i <= 2; i++)
            {
                string threadName = $"Thread #{i}";

                Task.Run(() =>
                {
                    Console.WriteLine($"{threadName} is waiting for signal");
                    autoEvent.WaitOne();
                    Console.WriteLine($"{threadName} is released");
                });
            }
            Console.WriteLine("Press Enter to signal and release one of waiting threads");
            Console.ReadLine();
            autoEvent.Set();
        }

        
    }
}
