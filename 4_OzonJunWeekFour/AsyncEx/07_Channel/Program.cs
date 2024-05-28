using System.Threading.Channels;

namespace _07_Channel
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var channel = Channel.CreateUnbounded<int>();
            var producer = Task.Run(async () =>
            {
                for (int i = 0; i < 5; i++)
                {
                    await channel.Writer.WriteAsync(i);
                    await Console.Out.WriteLineAsync($"Message {i} is published");
                }
                channel.Writer.Complete();
            });

            var cobsumer = Task.Run(async () =>
            {
                await foreach (var msg in channel.Reader.ReadAllAsync())
                    Console.WriteLine($"Message {msg} is consumed");
            });

            await Task.WhenAll(producer, cobsumer);
            Console.WriteLine("Hello, World!");
        }
    }
}
