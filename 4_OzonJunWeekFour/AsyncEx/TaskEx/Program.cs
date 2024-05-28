

namespace TaskEx
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // устаревший вариант, так делать не надо
            var legasyTask = new Task(DoSomething);
            legasyTask.Start();

            // сложный метод создания тасков, имеет много разных атрибутов
            //var complexTask = Task.Factory.StartNew(DoSomethingWith, new { Name = "John" });
            var longRunningTask = Task.Factory.StartNew(DoSomething, TaskCreationOptions.LongRunning);

            var task = Task.Run(DoSomething);

            
            
            
            CancellationTokenSource cts  = new CancellationTokenSource();
            //var fromCancelled = Task.FromCanceled(cts.Token);  // Cancelled
            //var fromException = Task.FromException(new FileNotFoundException()); // Faulted
            //var fromResult = Task.FromResult("Route256"); // RunToCompletion
            //var completedTask = Task.CompletedTask; // RunToCompletion

            var tcs = new TaskCompletionSource<int>();
            var task2 = tcs.Task;
            tcs.SetResult(42);
            //tcs.SetCanceled();
            //tcs.SetException(new Exception("oops"));
            //task2.Start(); // InvalidOperationException




            // синхронное ожидание с блокировкой потока
            task.Wait();
            task.GetAwaiter().GetResult();

            // в случае выполняющейся задачи - эквивалентно вызову  Wait
            //int result = task.Result;

            //комбинированное синхронное ожидание нескольких задач
            Task.WaitAll(task, task2, legasyTask);
            Task.WaitAny(task, task2, legasyTask);

            // асинхронное ожидание
            await task;

            //комбинированное асинхронное ожидание нескольких задач
            await Task.WhenAll(task, task2, legasyTask);
            await Task.WhenAny(task, task2, legasyTask);





            var tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;
            Task.Run(() =>
            {
                ct.ThrowIfCancellationRequested();
                for (int i = 0; i < 100; i++) 
                { 
                    if (ct.IsCancellationRequested)
                    {
                        // какой-то cleanup код
                        ct.ThrowIfCancellationRequested();
                    }
                    // основной код
                }
            }, ct);
            tokenSource.CancelAfter(TimeSpan.FromSeconds(3));
            tokenSource.Cancel();




            Console.WriteLine("Hello, World!");
        }

        private static void DoSomethingWith()
        {
            Console.WriteLine("DoSomethingWith");
        }

        private static void DoSomething()
        {
            Console.WriteLine("DoSomething");
        }
    }
}
