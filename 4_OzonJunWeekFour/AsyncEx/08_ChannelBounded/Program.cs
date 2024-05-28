using System.Threading.Channels;

namespace _08_ChannelBounded
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var channel = Channel.CreateBounded<int>(1);
            var producer = Task.Run(async () =>
            {
                for (int i = 0; i < 5; i++) 
                { 
                    await channel.Writer.WriteAsync(i);
                    Console.WriteLine($"Message {i} is published");
                }
                channel.Writer.Complete();
            });

            var consumer = Task.Run(async () =>
            {
                await foreach (var msg in channel.Reader.ReadAllAsync())
                {
                    Console.WriteLine($"Message {msg} is consumed");
                    await Task.Delay(500);
                }
            });

            await Task.WhenAll(producer, consumer);

            Console.WriteLine("Hello, World!");
        }
    }
}
