namespace IntegralTypes
{
    class MainApp
    {
        static void Main(string[] args)
        {
            byte a = 240;
            Console.WriteLine($"a={a}"); // a=240

            byte b = 0b1111_0000; // 2진수 리터럴
            Console.WriteLine($"b={b}"); // b=240

            byte c = 0XF0; // 16진수 리터럴
            Console.WriteLine($"c={c}"); // c=240

            uint d = 0x1234_abcd; // 16진수 리터럴
            Console.WriteLine($"d={d}"); // d=305441741
        }
    }
}
