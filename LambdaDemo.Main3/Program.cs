using Amazon.SQS;
using Amazon.SQS.Model;
using System;

namespace LambdaDemo.Main3
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
                SendSqsMessage(i);
            }

            var endTime = DateTime.Now;

            Console.WriteLine("Done!");

            var elapsedTime = endTime - startTime;

            Console.WriteLine($"Elapsed Time: {elapsedTime.Minutes} Mins {elapsedTime.Seconds} Secs");

            Console.ReadKey();
        }

        static void SendSqsMessage(int n)
        {
            var config = new AmazonSQSConfig();

            config.ServiceURL = "https://sqs.eu-west-2.amazonaws.com";

            var client = new AmazonSQSClient(config);

            var queueUrl = "https://sqs.eu-west-2.amazonaws.com/107528682398/demoFibonacci";
            var msgReq = new SendMessageRequest(queueUrl, n.ToString());

            client.SendMessageAsync(msgReq);
        }
    }
}
