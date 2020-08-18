using System;
using System.Threading.Tasks;

namespace LambdaDemo.Core
{
    public static class DemoMath
    {
        public static int FibDelayed(int n)
        {
            Task.Delay(2 * 1000).Wait();
            return Fib(n);
        }

        public static int Fib(int n)
        {
            if (n == 0)
                return 0;

            if (n == 1)
                return 1;

            return Fib(n - 1) + Fib(n - 2);
        }
    }
}
