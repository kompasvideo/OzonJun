using System.Collections.Concurrent;

namespace ParallelTasks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            int[] nums = ParallelWithTasks(new int[] {1,2,3,4,5,6,7,8 });
            foreach (int i in nums) 
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        public static int[] ParallelWithTasks(IEnumerable<int> numbers)
        {
            var result = new ConcurrentBag<int>();
            var tasks = new List<Task>();

            foreach (var number in numbers) 
            {
                tasks.Add(Task.Run(() =>
                {
                    if(IsPrime(number))
                        result.Add(number);
                }));
            }
            Task.WaitAll(tasks.ToArray());
            return result.ToArray();
        }

        public static bool IsPrime(int number) 
        { 
            return number % 2 == 0;
        }
    }
}
