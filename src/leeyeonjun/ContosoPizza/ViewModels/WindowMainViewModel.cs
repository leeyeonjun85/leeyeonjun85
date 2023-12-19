using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using ContosoPizza.Models;
using ContosoPizza.Views;
using Microsoft.EntityFrameworkCore;
using System.Windows.Documents;
using System.Xml.Linq;
using System.Linq;

namespace ContosoPizza.ViewModels
{
    public partial class WindowMainViewModel : ViewModelBase, IRecipient<ValueChangedMessage<Pizza>>
    {
        [ObservableProperty]
        private ObservableCollection<Pizza> _allPizza = [];
        private List<Sauce> AllSauce { get; set; } = [];
        [ObservableProperty]
        private ObservableCollection<string> _allSauceString = [];
        private List<Topping> AllTopping { get; set; } = [];
        [ObservableProperty]
        private Pizza? _selectedPizza;
        [ObservableProperty]
        private string? _selectedPizzaName;
        [ObservableProperty]
        private string? _selectedPizzaSauceName;

        private PizzaContext _context { get; set; }
        private DbConnection? _conn { get; set; }
        private DbCommand? _cmd { get; set; }



        public WindowMainViewModel(PizzaContext context)
        {
            IsActive = true;
            _context = context;
        }

        [RelayCommand]
        private void BtnNewPizzaClick(string? newPizzaName)
        {
            if (newPizzaName is not null)
            {
                Pizza newPizza = new()
                {
                    Name = newPizzaName
                };

                Create(newPizza);
            }
        }

        [RelayCommand]
        private void BtnShowWindowSubClick(object? obj)
        {
            App.ViewService?.ShowView<WindowNewPizza, WindowNewPizzaViewModel>(_context);
        }

        [RelayCommand]
        private void SelectionChanged(WrapPanel? wrapPanel)
        {
            if (wrapPanel is not null && SelectedPizza is not null)
            {
                Pizza? selectedPizza = GetById(SelectedPizza.Id);
                if (selectedPizza is not null)
                {
                    wrapPanel.Children.Clear();
                    SelectedPizzaName = $"{selectedPizza.Name}";
                    SelectedPizzaSauceName = $"{selectedPizza.Sauce?.Name}";
                    if (selectedPizza.Toppings is not null)
                    {
                        foreach (Topping topping in AllTopping)
                        {
                            var findThing = selectedPizza.Toppings
                                            .Where(x => x.Id == topping.Id)
                                            .ToList();

                            if (findThing.Count > 0)
                            {
                                wrapPanel.Children.Add(new CheckBox()
                                {
                                    Margin = new Thickness(10, 5, 10, 5),
                                    Content = $"{topping.Name}({topping.Calories}cal)",
                                    IsChecked = true,
                                    VerticalContentAlignment = VerticalAlignment.Center
                                });
                            }
                            else
                            {
                                wrapPanel.Children.Add(new CheckBox()
                                {
                                    Margin = new Thickness(10, 5, 10, 5),
                                    Content = $"{topping.Name}({topping.Calories}cal)",
                                    IsChecked = false,
                                    VerticalContentAlignment = VerticalAlignment.Center
                                });
                            }
                        }
                    }
                }
            }
        }

        public Pizza? GetById(int id)
        {
            Pizza? pizza = _context.Pizzas
                    .Include(p => p.Toppings)
                    .Include(p => p.Sauce)
                    .AsNoTracking()
                    .SingleOrDefault(p => p.Id == id);

            return pizza;
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

            pizzaToUpdate.Toppings ??= new List<Topping>();

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
            ObservableCollection<Pizza> returnData = [];

            context.Pizzas
                    .AsNoTracking()
                    .ToList()
                    .ForEach(x => { returnData.Add(x); });

            return returnData;
        }

        public void Receive(ValueChangedMessage<Pizza> newPizza)
        {
            Create(newPizza.Value);
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

        private List<Sauce> GetAllSauce(PizzaContext context)
        {
            List<Sauce> returnData = [];

            context.Sauces
                    .AsNoTracking()
                    .ToList()
                    .ForEach(x => { returnData.Add(x); });

            return returnData;
        }
        private List<Topping> GetAllTopping(PizzaContext context)
        {
            List<Topping> returnData = [];

            context.Toppings
                    .AsNoTracking()
                    .ToList()
                    .ForEach(x => { returnData.Add(x); });

            return returnData;
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

            AllPizza = GetAllPizza(_context);
            AllSauce = GetAllSauce(_context);
            AllTopping = GetAllTopping(_context);

            AllSauce.ForEach(x => 
            { 
                if(x.Name is not null)
                {
                    AllSauceString.Add(x.Name);
                }
            });
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {

        }
    }
}
