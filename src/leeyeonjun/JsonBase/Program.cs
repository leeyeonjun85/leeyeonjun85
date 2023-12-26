using JsonBase.Models;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace JsonBase
{
    internal class Program
    {
        static string vsFolder1 = AppDomain.CurrentDomain.BaseDirectory;
        static string vsFolder2 = Directory.GetCurrentDirectory();
        static string vsFolder3 = Environment.CurrentDirectory;
        static string vsFolder4 = Path.GetFullPath(".\\");

        static string? projectDirectory = Directory.GetParent(vsFolder1)?.Parent?.Parent?.Parent?.FullName;

        private static void Main(string[] args)
        {
            List<Pizza> pizzas = InitPizzaData();

            TextEncoderSettings encoderSettings = new();
            encoderSettings.AllowRange(UnicodeRanges.All);

            JsonSerializerOptions options = new()
            { 
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(encoderSettings),
                AllowTrailingCommas = true
            };
            string jsonString = JsonSerializer.Serialize(pizzas, options);
            Console.WriteLine($"{jsonString}");

            Console.WriteLine($"========================");
            Console.WriteLine($"{vsFolder1}");
            Console.WriteLine($"{vsFolder2}");
            Console.WriteLine($"{vsFolder3}");
            Console.WriteLine($"{vsFolder4}");
            Console.WriteLine($"{projectDirectory}");

            File.WriteAllText($"{projectDirectory}{Path.DirectorySeparatorChar}pizzas.json", jsonString);
        }



        private static List<Pizza> InitPizzaData()
        {
            // Init Data
            var pepperoniTopping = new Topping { Name = "Pepperoni", Calories = 130 };
            var sausageTopping = new Topping { Name = "Sausage", Calories = 100 };
            var hamTopping = new Topping { Name = "Ham", Calories = 70 };
            var chickenTopping = new Topping { Name = "Chicken", Calories = 50 };
            var pineappleTopping = new Topping { Name = "Pineapple", Calories = 75 };

            var tomatoSauce = new Sauce { Name = "Tomato", IsVegan = true };
            var alfredoSauce = new Sauce { Name = "Alfredo", IsVegan = false };

            List<Pizza> pizzaList =
            [
                new()
                {
                    Name = "Meat Lovers",
                    Sauce = tomatoSauce,
                    Toppings = new List<Topping>
                        {
                            pepperoniTopping,
                            sausageTopping,
                            hamTopping,
                            chickenTopping
                        }
                },
                new()
                {
                    Name = "Hawaiian",
                    Sauce = tomatoSauce,
                    Toppings = new List<Topping>
                        {
                            pineappleTopping,
                            hamTopping
                        }
                },
                new()
                {
                    Name = "Alfredo Chicken",
                    Sauce = alfredoSauce,
                    Toppings = new List<Topping>
                        {
                            chickenTopping
                        }
                }
            ];

            return pizzaList;
        }
    }
}
