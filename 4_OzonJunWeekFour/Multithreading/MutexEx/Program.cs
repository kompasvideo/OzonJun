namespace MutexEx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var mutex = new Mutex(false, "single_instance_application"))
            {
                if (!mutex.WaitOne(TimeSpan.FromSeconds(3), false)) 
                {
                    Console.WriteLine("Only one instance of the application car be running at one time");
                    return;
                }
                MainInternal();
            }            
        }

        private static void MainInternal()
        {
            Console.WriteLine("MainInternal");
        }
    }
}
