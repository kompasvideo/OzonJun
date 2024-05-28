namespace _06_IAsyncEnumerable
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await foreach(var item in SlowRange())
            {
                Console.WriteLine(item);
            }
        }

        static async IAsyncEnumerable<int> SlowRange()
        {
            for (int i = 0; i < 10; i++) 
            {
                await Task.Delay(i * TimeSpan.FromSeconds(0.5));
                yield return i;
            }
        }

        //public interface IAsyncEnumerable<out T>
        //{
        //    IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default);
        //}

        public interface IAsyncEnumerator<out T> : IAsyncDisposable
        {
            ValueTask<bool> MoveNextAsync();
            T Current { get; }
        }
    }
}
