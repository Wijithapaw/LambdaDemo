using LambdaDemo.Core;
using System;

namespace LambdaDemo.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var upto = 10;

            if (args.Length > 0)
                upto = int.Parse(args[0]);

            Console.WriteLine($"Starting finding Fibs upto {upto}...");

            var startTime = DateTime.Now;

            for (var i = 0; i <= upto; i++)
            {
                var fib = DemoMath.FibDelayed(i);
                Console.WriteLine($"Fib({i}): {fib}");
            }

            var endTime = DateTime.Now;

            Console.WriteLine("Done!");

            var elapsedTime = endTime - startTime;

            Console.WriteLine($"Elapsed Time: {elapsedTime.Minutes} Mins {elapsedTime.Seconds} Secs");

            Console.ReadKey();
        }
    }
}
