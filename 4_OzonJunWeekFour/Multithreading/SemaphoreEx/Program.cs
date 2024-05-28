namespace SemaphoreEx
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            for (int i = 1; i <= 5; i++) 
            {
                int carNumber = i;
                Task.Run(() => Parking.Enter(carNumber));
            }
            Console.ReadLine();
        }
    }

    public static class Parking
    {
        private static Semaphore _semaphore = new Semaphore(3, 3);
        public static void Enter(int carNumber)
        {
            Console.WriteLine($"Car {carNumber} wants to park");
            _semaphore.WaitOne();
            Console.WriteLine($"Car {carNumber} is park");
            Thread.Sleep( TimeSpan.FromSeconds(3));
            Console.WriteLine($"Car {carNumber} left park");
            _semaphore.Release();
        }
    }
}
