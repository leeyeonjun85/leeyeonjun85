using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("===============================================\n");
            Console.WriteLine("            C#으로 만든 콘솔 계산기\r\n");
            Console.WriteLine("===============================================\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                // Declare variables and set to empty.
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                // Ask the user to type the first number.
                Console.Write("첫 번째 숫자를 입력하고 Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("적절하지 않은 숫자입니다. 정수형 숫자를 입력해주세요 : ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("두 번째 숫자를 입력하고 Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("적절하지 않은 숫자입니다. 정수형 숫자를 입력해주세요 : ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("수행 할 계산을 선택해주세요 :");
                Console.WriteLine("\ta - 더하기(Add)");
                Console.WriteLine("\ts - 빼기(Subtract)");
                Console.WriteLine("\tm - 곱하기(Multiply)");
                Console.WriteLine("\td - 나누기(Divide)");
                Console.Write("선택한 계산 : ");

                string? op = Console.ReadLine();

                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op!);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("잘못 된 계산식입니다.\n");
                    }
                    else Console.WriteLine("계산 결과 : {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! 알 수 없는 문제가 발생하였습니다.\n - Details: " + e.Message);
                }

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("계산기를 종료하시려면 'n'을 입력하시고, 계속 사용하시려면 다른키를 누르고 Enter를 입력하세요.");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }
    }
}