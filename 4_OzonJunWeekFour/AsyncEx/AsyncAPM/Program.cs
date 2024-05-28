using System.Net;
using System.Text;

namespace AsyncAPM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var webClient = new WebClient();
            var stream = webClient.OpenRead("https://api.my-ip.io/ip");
            var buffer = new byte[100];
            IAsyncResult asyncResult = stream.BeginRead(
                buffer: buffer,
                offset: 0,
                count: buffer.Length,
                callback: result =>
                {
                    stream.EndRead(result);
                    Console.WriteLine(Encoding.Default.GetString(buffer));
                },
                state: null);
            Console.WriteLine("Hello, World!");
        }
    }
}
