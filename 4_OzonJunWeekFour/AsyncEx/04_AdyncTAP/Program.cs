namespace _04_AdyncTAP
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // .net framework 4
            //var httpClient = new HttpClient();
            //httpClient.GetStringAsync("https://api.my-ip.io/ip").ContinueWith(task=>Console.WriteLine(task.Result));

            // .net framework 4.5
            var httpClient45 = new HttpClient();
            var ip = await httpClient45.GetStringAsync("https://api.my-ip.io/ip");
            Console.WriteLine(ip);
            // IAsyncStateMachine


            Console.WriteLine("Hello, World!");
        }
    }
}
