using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using LambdaDemo.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LambdaDemo.Lambda
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public dynamic FunctionHandler(int n, ILambdaContext context)
        {
            var fib = DemoMath.FibDelayed(n);
            context.Logger.LogLine($"Fib({n}): {fib}");
            return new { N = n, Fib = fib };
        }
    }
}
