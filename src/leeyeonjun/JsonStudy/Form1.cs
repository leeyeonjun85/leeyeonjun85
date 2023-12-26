using JsonStudy.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace JsonStudy
{
    public partial class Form1 : Form
    {
        string _vsFolder1 { get; } = AppDomain.CurrentDomain.BaseDirectory;
        string _vsFolder2 { get; } = Directory.GetCurrentDirectory();
        string _vsFolder3 { get; } = Environment.CurrentDirectory;
        string _vsFolder4 { get; } = Path.GetFullPath(@".\");

        string? _projectDirectory { get; }
        string? _jsonFileName { get; }
        string? _jsonString { get; set; }

        List<Pizza>? _pizzas { get; set; }
        JsonSerializerOptions? _jsonSerializerOption;

        public Form1()
        {
            InitializeComponent();

            _projectDirectory = Directory.GetParent(_vsFolder1)?.Parent?.Parent?.Parent?.FullName;

            if (!string.IsNullOrEmpty(_projectDirectory))
            {
                _jsonFileName = Path.Combine(_projectDirectory, "pizzas.json");
            }
        }

        private void BtnInitData_Click(object sender, EventArgs e)
        {
            _pizzas = InitPizzaData();
            dataGridView1.DataSource = _pizzas;
            label1.Text = $"Initialized Pizza : {_pizzas.Count}";
        }

        private void BtnClearData_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            _pizzas = null;
            label1.Text = $"Clear Pizza!";
        }

        private void BtnSaveJson_Click(object sender, EventArgs e)
        {
            TextEncoderSettings encoderSettings = new();
            encoderSettings.AllowRange(UnicodeRanges.All);

            _jsonSerializerOption = new()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(encoderSettings),
                AllowTrailingCommas = true
            };

            string jsonString = JsonSerializer.Serialize(_pizzas, _jsonSerializerOption);

            if (_jsonFileName is not null)
            {
                File.WriteAllText(_jsonFileName, jsonString);
            }

            label1.Text = $"Json Save : {_jsonFileName}";
        }

        private void BtnLoadData_Click(object sender, EventArgs e)
        {
            if (_jsonFileName is not null)
            {
                _jsonString = File.ReadAllText(_jsonFileName);
                _pizzas = JsonSerializer.Deserialize<List<Pizza>>(_jsonString)!;
                dataGridView1.DataSource = _pizzas;
            }

            label1.Text = $"Json Load : {_jsonFileName}";
        }


        private List<Pizza> InitPizzaData()
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
