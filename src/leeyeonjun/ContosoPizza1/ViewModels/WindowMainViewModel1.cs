using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.ViewModels
{
    public partial class WindowMainViewModel1 : ViewModelBase
    {
        [ObservableProperty]
        private string _connectionString = string.Empty;
        [ObservableProperty]
        private ObservableCollection<Sauce> _itemsSourceSauce = [];
        [ObservableProperty]
        private ObservableCollection<Topping> _itemsSourceTopping = [];
        [ObservableProperty]
        private ObservableCollection<Pizza> _itemsSourcePizza = [];

        private PizzaContext _context { get; set; }
        private DbConnection? _conn { get; set; }
        private DbCommand? _cmd { get; set; }

        public WindowMainViewModel1(PizzaContext context)
        {
            _context = context;
        }

        [RelayCommand]
        private void BtnConnectClick(object? obj)
        {
            _context.Database.EnsureCreated();
            _conn = _context.Database.GetDbConnection();
            _conn.Open();
            _cmd = _conn.CreateCommand();
            _cmd.CommandText = "PRAGMA journal_mode=Off;";
            _cmd.ExecuteNonQuery();

            Initialize(_context);

            ItemsSourceSauce = GetAllSauce(_context);
            ItemsSourceTopping = GetAllTopping(_context);
            ItemsSourcePizza = GetAllPizza(_context);
        }

        public ObservableCollection<Sauce> GetAllSauce(PizzaContext context)
        {
            ObservableCollection<Sauce> returnData = [];

            context.Sauces
                    .AsNoTracking()
                    .ToList()
                    .ForEach(x => { returnData.Add(x); });

            return returnData;
        }

        public ObservableCollection<Topping> GetAllTopping(PizzaContext context)
        {
            ObservableCollection<Topping> returnData = [];

            context.Toppings
                    .AsNoTracking()
                    .ToList()
                    .ForEach(x => { returnData.Add(x); });

            return returnData;
        }

        public ObservableCollection<Pizza> GetAllPizza(PizzaContext context)
        {
            ObservableCollection<Pizza> returnData = [];

            context.Pizzas
                    .AsNoTracking()
                    .ToList()
                    .ForEach(x => { returnData.Add(x); });

            return returnData;
        }


        public void Initialize(PizzaContext context)
        {

            if (context.Pizzas.Any()
                && context.Toppings.Any()
                && context.Sauces.Any())
            {
                return;   // DB has been seeded
            }

            var pepperoniTopping = new Topping { Name = "Pepperoni", Calories = 130 };
            var sausageTopping = new Topping { Name = "Sausage", Calories = 100 };
            var hamTopping = new Topping { Name = "Ham", Calories = 70 };
            var chickenTopping = new Topping { Name = "Chicken", Calories = 50 };
            var pineappleTopping = new Topping { Name = "Pineapple", Calories = 75 };

            var tomatoSauce = new Sauce { Name = "Tomato", IsVegan = true };
            var alfredoSauce = new Sauce { Name = "Alfredo", IsVegan = false };

            var pizzas = new Pizza[]
            {
                new Pizza
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
                new Pizza
                    {
                        Name = "Hawaiian",
                        Sauce = tomatoSauce,
                        Toppings = new List<Topping>
                            {
                                pineappleTopping,
                                hamTopping
                            }
                    },
                new Pizza
                    {
                        Name="Alfredo Chicken",
                        Sauce = alfredoSauce,
                        Toppings = new List<Topping>
                            {
                                chickenTopping
                            }
                        }
            };

            context.Pizzas.AddRange(pizzas);
            context.SaveChanges();
        }


        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            _cmd?.Dispose();
            _conn?.Dispose();
            _context?.Dispose();
        }
    }
}
