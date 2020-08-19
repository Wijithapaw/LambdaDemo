using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace LambdaDemo.Main2
{
    class Program
    {
        static void Main(string[] args)
        {
            var upto = 30;

            if (args.Length > 0)
                upto = int.Parse(args[0]);

            Console.WriteLine($"Starting finding Fibs upto {upto}...");

            var startTime = DateTime.Now;

            var tasks = new List<Task<InvokeResponse>>();

            for (var i = 0; i <= upto; i++)
            {
                var task = InvokeLambda(i);
                tasks.Add(task);
            }

            Task.WhenAll(tasks).Wait();

            var fibResults = tasks.Select(t => GetFibResult(t.Result.Payload)).OrderBy(f => f.N);

            foreach(var res in fibResults)
            {
                Console.WriteLine($"Fib({res.N}): {res.Fib}");
            }

            var endTime = DateTime.Now;

            Console.WriteLine("Done!");

            var elapsedTime = endTime - startTime;

            Console.WriteLine($"Elapsed Time: {elapsedTime.Minutes} Mins {elapsedTime.Seconds} Secs");

            Console.ReadKey();
        }

        static FibResult GetFibResult(MemoryStream stream)
        {
            string payload = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            var fibResult = JsonConvert.DeserializeObject<FibResult>(payload);
            return fibResult;
        }

        static Task<InvokeResponse> InvokeLambda(int n)
        {
            var config = new AmazonLambdaConfig
            {
                RegionEndpoint = RegionEndpoint.EUWest2,
                Timeout = new TimeSpan(0, 15, 0)
            };

            var client = new AmazonLambdaClient(config);

            var invokeReq = new InvokeRequest
            {
                FunctionName = "demoFibonacci",
                InvocationType = InvocationType.RequestResponse,
                Payload = n.ToString(),
            };

            var task = client.InvokeAsync(invokeReq);

            return task;
        }
    }

    class FibResult
    {
        public int N { get; set; }
        public int Fib { get; set; }
    }
}
