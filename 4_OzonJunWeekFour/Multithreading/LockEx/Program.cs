namespace LockEx
{
    internal class Program
    {
        private int _counter = 0;
        private readonly object _sync = new object();
        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine($"_counter  = {program.GetCounter()}");
            program.IncrementCounter();
            Console.WriteLine($"_counter  = {program.GetCounter()}");
            program.IncrementCounterM();
            Console.WriteLine($"_counter  = {program.GetCounter()}");
        }

        public int IncrementCounter()
        {
            lock (_sync) 
            {
                Console.WriteLine("increment");
                return _counter++;
            }
        }
        public int GetCounter()
        {
            return _counter;
        }

        public int IncrementCounterM()
        {
            object sync = _sync;
            bool lockTaken = false;
            try
            {
                Monitor.Enter(sync, ref lockTaken);
                Console.WriteLine("increment");
                return _counter++;
            }
            finally
            {
                if(lockTaken)
                {
                    Monitor.Exit(sync);
                }
            }
        }
    }


}
