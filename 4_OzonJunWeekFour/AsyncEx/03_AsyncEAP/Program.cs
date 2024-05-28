using System.Net;

namespace _03_AsyncEAP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var webClient = new WebClient();
            webClient.DownloadStringCompleted += (sender, eventArgs) =>
            {
                Console.WriteLine(eventArgs.Result);
            };
            webClient.DownloadStringAsync(new Uri("https://api.my-ip.io/ip"));

            Console.WriteLine("Hello, World!");
        }
    }
}
