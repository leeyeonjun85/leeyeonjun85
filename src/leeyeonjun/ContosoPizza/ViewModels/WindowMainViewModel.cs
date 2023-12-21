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
using ContosoPizza.Services;

namespace ContosoPizza.ViewModels
{
    public partial class WindowMainViewModel : ViewModelBase, IRecipient<ValueChangedMessage<Pizza>>
    {
        [ObservableProperty]
        private ObservableCollection<Pizza> _allPizza = new();
        [ObservableProperty]
        private ObservableCollection<Sauce> _allSauce = new();
        [ObservableProperty]
        private ObservableCollection<string> _allSauceString = new();
        [ObservableProperty]
        private List<Topping> _allTopping = new();
        [ObservableProperty]
        private Pizza? _selectedPizza;
        [ObservableProperty]
        private string? _selectedPizzaName;
        
        [ObservableProperty]
        private Sauce? _selectedPizzaSauce;
        [ObservableProperty]
        private string? _selectedPizzaSauceName;
        [ObservableProperty]
        private int _selectedPizzaSauceIndex;

        private PizzaContext _context { get; set; }
        private DbConnection? _conn { get; set; }
        private DbCommand? _cmd { get; set; }

        private IPizzaService _pizzaService { get; set; }
        private IViewService _viewService { get; set; }
        
        public WindowMainViewModel(PizzaContext context, IViewService viewService, IPizzaService pizzaService)
        {
            IsActive = true;
            _context = context;
            _viewService = viewService;
            _pizzaService = pizzaService;
        }




        [RelayCommand]
        private void BtnMakePizzaClick(object? obj)
        {
            if (SelectedPizza is not null)
            {
                string messageContent = $"Name : {SelectedPizza.Name}{Environment.NewLine}";
                messageContent += $"Sauce : {SelectedPizza.Sauce?.Name}{Environment.NewLine}";
                messageContent += $"Toppings : ";
                if (SelectedPizza.Toppings is not null) 
                {
                    foreach (var topping in SelectedPizza.Toppings)
                    {
                        messageContent += $"{topping.Name}, ";
                    }
                }
                messageContent = messageContent[..^2];

                MessageBox.Show(messageContent, "Wow! Pizza~!");
            }
        }


        [RelayCommand]
        private void BtnNewPizzaClick(TextBox? newPizzaTextBox)
        {
            if (newPizzaTextBox is not null && !string.IsNullOrEmpty(newPizzaTextBox.Text))
            {
                Pizza newPizza = new()
                {
                    Name = newPizzaTextBox.Text
                };

                AddNewPizza(newPizza);
                newPizzaTextBox.Text = string.Empty;
            }
        }

        [RelayCommand]
        private void DeletePizzaClick(object? obj)
        {
            if (SelectedPizza is not null)
            {
                DeleteById(SelectedPizza.Id);
            }
        }

        [RelayCommand]
        private void BtnShowWindowSubClick(object? obj)
        {
            App.ViewService?.ShowView<WindowSub, WindowSubViewModel>(_context);
        }

        [RelayCommand]
        private void SelectionChangedSauce(ComboBox? comboBoxPizzaSauce)
        {
            if (SelectedPizza is not null && comboBoxPizzaSauce is not null)
            {
                var selectedPizzaSauce = comboBoxPizzaSauce.SelectedItem as Sauce;
                if (selectedPizzaSauce is not null)
                    UpdateSauce(SelectedPizza.Id, selectedPizzaSauce.Id);
            }
        }

        [RelayCommand]
        private void SelectionChanged(WrapPanel? wrapPanel)
        {
            if (wrapPanel is not null && SelectedPizza is not null)
            {
                Pizza? selectedPizza = GetById(this.SelectedPizza.Id);
                if (selectedPizza is not null)
                {
                    wrapPanel.Children.Clear();
                    SelectedPizzaName = $"{selectedPizza.Name}";
                    SelectedPizzaSauce = selectedPizza.Sauce;
                    if (selectedPizza.Sauce is not null)
                        SelectedPizzaSauceIndex = AllSauce.Select(x => x.Id).ToList().IndexOf(selectedPizza.Sauce.Id);
                    else
                        SelectedPizzaSauceIndex = -1;
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
                    .SingleOrDefault(p => p.Id == id);

            return pizza;
        }

        public PizzaTopping? GetPizzaToppingById(int pizzaId, int toppingId)
        {
            PizzaTopping? pizzaTopping = _context.PizzaTopping
                    //.AsNoTracking()
                    .Where(p => p.PizzaId == pizzaId)
                    .SingleOrDefault(p => p.ToppingId == toppingId);

            return pizzaTopping;
        }

        public Pizza AddNewPizza(Pizza newPizza)
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

        public void RemoveTopping(int pizzaId, int toppingId)
        {
            var pizzaToppingToRemove = GetPizzaToppingById(pizzaId, toppingId);

            if (pizzaToppingToRemove is not null)
            {
                _context.PizzaTopping.Remove(pizzaToppingToRemove);
                _context.SaveChanges();
            }

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

        public void Receive(ValueChangedMessage<Pizza> newPizza)
        {
            AddNewPizza(newPizza.Value);
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

        public ObservableCollection<Pizza> GetAllPizza(PizzaContext context)
        {
            ObservableCollection<Pizza> returnData = new();

            //context.Pizzas
            //        .AsNoTracking()
            //        .ToList()
            //        .ForEach(x => { returnData.Add(x); });
            context.Pizzas.Load();
            returnData = context.Pizzas.Local.ToObservableCollection();

            return returnData;
        }

        private ObservableCollection<Sauce> GetAllSauce(PizzaContext context)
        {
            ObservableCollection<Sauce> returnData = new();

            //context.Sauces
            //        .AsNoTracking()
            //        .ToList()
            //        .ForEach(x => { returnData.Add(x); });
            context.Sauces.Load();
            returnData = context.Sauces.Local.ToObservableCollection();

            return returnData;
        }
        private List<Topping> GetAllTopping(PizzaContext context)
        {
            List<Topping> returnData = new();

            //context.Toppings
            //        .AsNoTracking()
            //        .ToList()
            //        .ForEach(x => { returnData.Add(x); });
            context.Toppings.Load();
            returnData = context.Toppings.Local.ToList();

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

            AllPizza = _pizzaService.GetAllPizza(_context);
            AllSauce = GetAllSauce(_context);
            AllTopping = GetAllTopping(_context);

            AllSauce.ToList().ForEach(x => 
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
