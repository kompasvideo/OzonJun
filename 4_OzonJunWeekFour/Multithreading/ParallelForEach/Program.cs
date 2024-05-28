using System.Collections.Concurrent;

namespace ParallelForEach
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            int[] nums = ParallelWithParallelForeach([1, 2, 3, 4, 5, 6, 7, 8]);
            foreach (int i in nums)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        public static int[] ParallelWithParallelForeach(IEnumerable<int> numbers)
        {
            var result = new ConcurrentBag<int>();
            Parallel.ForEach(
                numbers,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = Environment.ProcessorCount,
                    TaskScheduler = TaskScheduler.Default,
                    CancellationToken = CancellationToken.None
                },
                number =>
                {
                    if (IsPrime(number))
                        result.Add(number);
                });
            return result.ToArray();
        }

        public static bool IsPrime(int number)
        {
            return number % 2 == 0;
        }
    }
}
