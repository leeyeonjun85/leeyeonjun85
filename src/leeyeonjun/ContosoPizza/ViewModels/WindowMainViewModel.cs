using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using ContosoPizza.Models;
using ContosoPizza.Services;
using ContosoPizza.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ContosoPizza.ViewModels
{
    public partial class WindowMainViewModel : ViewModelBase, IRecipient<ValueChangedMessage<Pizza>>
    {
        [ObservableProperty]
        private ObservableCollection<Pizza> _allPizza = [];
        [ObservableProperty]
        private ObservableCollection<Sauce> _allSauce = [];
        [ObservableProperty]
        private ObservableCollection<Topping> _allTopping = [];

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


        private IPizzaService _pizzaService { get; set; }
        private IViewService _viewService { get; set; }

        public WindowMainViewModel(IViewService viewService, IPizzaService pizzaService)
        {
            IsActive = true;
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

                _pizzaService.AddNewPizza(newPizza);
                newPizzaTextBox.Text = string.Empty;
            }
        }

        [RelayCommand]
        private void DeletePizzaClick(object? obj)
        {
            if (SelectedPizza is not null)
            {
                _pizzaService.DeletePizzaById(SelectedPizza.Id);
            }
        }

        [RelayCommand]
        private void BtnShowWindowSubClick(object? obj)
        {
            _viewService.ShowView<WindowSub, WindowSubViewModel>();
        }

        [RelayCommand]
        private void SelectionChangedSauce(ComboBox? comboBoxPizzaSauce)
        {
            if (SelectedPizza is not null && comboBoxPizzaSauce is not null)
            {
                var selectedPizzaSauce = comboBoxPizzaSauce.SelectedItem as Sauce;
                if (selectedPizzaSauce is not null)
                    _pizzaService.UpdatePizzaSauce(SelectedPizza.Id, selectedPizzaSauce.Id);
            }
        }

        [RelayCommand]
        private void SelectionChangedPizza(WrapPanel? wrapPanel)
        {
            if (wrapPanel is not null && SelectedPizza is not null)
            {
                Pizza? selectedPizza = _pizzaService.GetPizzaById(this.SelectedPizza.Id);
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
                                    Margin = new Thickness(10, 3, 10, 3),
                                    Content = $"{topping.Name}({topping.Calories}cal)",
                                    IsChecked = true,
                                    VerticalContentAlignment = VerticalAlignment.Center
                                });
                            }
                            else
                            {
                                wrapPanel.Children.Add(new CheckBox()
                                {
                                    Margin = new Thickness(10, 3, 10, 3),
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

        public void Receive(ValueChangedMessage<Pizza> newPizza)
        {
            _pizzaService.AddNewPizza(newPizza.Value);
        }

        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            _pizzaService.Initialize();

            AllPizza = _pizzaService.GetAllPizza();
            AllSauce = _pizzaService.GetAllSauce();
            AllTopping = _pizzaService.GetAllTopping();
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {

        }
    }
}
