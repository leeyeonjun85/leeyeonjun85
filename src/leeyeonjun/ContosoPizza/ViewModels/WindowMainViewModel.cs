using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ContosoPizza.Models;
using ContosoPizza.Views;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.ViewModels
{
    public partial class WindowMainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _connectionString = string.Empty;
        [ObservableProperty]
        private ObservableCollection<Pizza> _itemsSourcePizza = new();
        [ObservableProperty]
        private string? _pizzaName;
        [ObservableProperty]
        private string? _pizzaSauce;
        [ObservableProperty]
        private string? _pizzaToppings;

        private PizzaContext _context { get; set; }
        private DbConnection? _conn { get; set; }
        private DbCommand? _cmd { get; set; }



        public WindowMainViewModel(PizzaContext context)
        {
            _context = context;
        }

        [RelayCommand]
        private void BtnShowWindowSubClick(object? obj)
        {
            App.ViewService?.ShowView<WindowSub, WindowSubViewModel>(_context);
        }

        [RelayCommand]
        private void SelectionChanged(Pizza? selectedPizza)
        {
            if (selectedPizza is not null)
            {
                selectedPizza = GetById(selectedPizza.Id);
                if (selectedPizza is not null)
                {
                    PizzaName = $"Pizza Name : {selectedPizza.Name}";
                    PizzaSauce = $"Sauce Name : {selectedPizza.Sauce?.Name}";
                    if (selectedPizza.Toppings is not null && selectedPizza.Toppings.Count > 0)
                    {
                        PizzaToppings = $"Toppiongs{Environment.NewLine}";
                        selectedPizza.Toppings
                            .ToList()
                            .ForEach(x => { PizzaToppings += $"{x.Name}, "; });
                        PizzaToppings = PizzaToppings[..^2];
                    }
                }
            }
        }

        public Pizza? GetById(int id)
        {
            return _context.Pizzas
                .Include(p => p.Toppings)
                .Include(p => p.Sauce)
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);
        }

        public Pizza Create(Pizza newPizza)
        {
            _context.Pizzas.Add(newPizza);
            _context.SaveChanges();

            return newPizza;
        }

        public void UpdateSauce(int pizzaId, int sauceId)
        {
            var pizzaToUpdate = _context.Pizzas.Find(pizzaId);
            var sauceToUpdate = _context.Sauces.Find(sauceId);

            if (pizzaToUpdate is null || sauceToUpdate is null)
            {
                throw new InvalidOperationException("Pizza or sauce does not exist");
            }

            pizzaToUpdate.Sauce = sauceToUpdate;

            _context.SaveChanges();
        }

        public void AddTopping(int pizzaId, int toppingId)
        {
            var pizzaToUpdate = _context.Pizzas.Find(pizzaId);
            var toppingToAdd = _context.Toppings.Find(toppingId);

            if (pizzaToUpdate is null || toppingToAdd is null)
            {
                throw new InvalidOperationException("Pizza or topping does not exist");
            }

            if (pizzaToUpdate.Toppings is null)
            {
                pizzaToUpdate.Toppings = new List<Topping>();
            }

            pizzaToUpdate.Toppings.Add(toppingToAdd);

            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var pizzaToDelete = _context.Pizzas.Find(id);
            if (pizzaToDelete is not null)
            {
                _context.Pizzas.Remove(pizzaToDelete);
                _context.SaveChanges();
            }
        }

        public ObservableCollection<Pizza> GetAllPizza(PizzaContext context)
        {
            ObservableCollection<Pizza> returnData = new();

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
            _context.Database.EnsureCreated();
            _conn = _context.Database.GetDbConnection();
            _conn.Open();
            _cmd = _conn.CreateCommand();
            _cmd.CommandText = "PRAGMA journal_mode=Off;";
            _cmd.ExecuteNonQuery();

            Initialize(_context);

            ItemsSourcePizza = GetAllPizza(_context);
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {

        }
    }
}
