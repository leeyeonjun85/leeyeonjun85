using System.Diagnostics;

namespace DotNetDebugging
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int target = 10;
            int result = Fibonacci(target);
            Console.WriteLine($"The Fibonacci {target} is : {result}");

            IntegerDivide(2, 1);

            static int Fibonacci(int n)
            {
                Debug.WriteLine($"Entering {nameof(Fibonacci)} method");
                Debug.WriteLine($"We are looking for the {n}th number");

                //Console.Write("The output is : ");
                int n1 = 0;
                int n2 = 1;
                int sum;

                for (int i = 2; i <= n; i++)
                {
                    sum = n1 + n2;
                    n1 = n2;
                    n2 = sum;

                    Console.WriteLine($"Console, i : {i}, Sum : {sum}");
                    Trace.WriteLine($"Trace, i : {i}, Sum : {sum}");
                    Debug.WriteLine($"Debug, i : {i}, Sum : {sum}");

                }

                // If n2 is 5 continue, else break.
                Debug.Assert(n2 == 5, "The return value is not 5 and it should be.");

                return n == 0 ? n1 : n2;
            }

            static int IntegerDivide(int dividend, int divisor)
            {
                Debug.Assert(divisor != 0, $"{nameof(divisor)} is 0 and will cause an exception.");

                return dividend / divisor;
            }
        }

        
    }
}
