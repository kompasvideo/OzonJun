using System;

namespace Thread1;
public class Program
{
    static void Main(string[] args)
    {
        Thread myThread = new Thread(Print);
        myThread.IsBackground = true;
        myThread.Start();
        myThread.Join();
        Console.WriteLine("hello");
    }

    private static void Print(object? obj)
    {
        Console.WriteLine("New Thread");
    }

}
