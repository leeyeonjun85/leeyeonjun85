using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Logging;
using ContosoPizza.Models;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;

namespace ContosoPizza.ViewModels
{
    public partial class WindowNewPizzaViewModel : ViewModelBase, IParameterReceiver
    {
        private PizzaContext? _context { get; set; }
        [ObservableProperty]
        private AppData _appData = App.Data;

        [ObservableProperty]
        private ObservableCollection<Sauce> _itemsSourceSauce = new();
        [ObservableProperty]
        private ObservableCollection<Topping> _itemsSourceTopping = new();
        [ObservableProperty]
        private string _newPizzaName = string.Empty;
        [ObservableProperty]
        private string _selectedSauceName = "Sauce : ";
        [ObservableProperty]
        private string _selectedToppingsName = "Toppings : ";
        private Sauce SelectedSauce { get; set; } = new();
        private List<Topping> SelectedToppings { get; set; } = new();

        public WindowNewPizzaViewModel()
        {
        }

        private ObservableCollection<Sauce> GetAllSauce(PizzaContext context)
        {
            ObservableCollection<Sauce> returnData = new();

            context.Sauces
                    .AsNoTracking()
                    .ToList()
                    .ForEach(x => { returnData.Add(x); });

            return returnData;
        }
        private ObservableCollection<Topping> GetAllTopping(PizzaContext context)
        {
            ObservableCollection<Topping> returnData = new();

            context.Toppings
                    .AsNoTracking()
                    .ToList()
                    .ForEach(x => { returnData.Add(x); });

            return returnData;
        }

        [RelayCommand]
        private void SelectionChangedSauce(Sauce? selectedItem)
        {
            if (selectedItem is not null)
            {
                SelectedSauceName = $"Sauce : {selectedItem.Name}";
                SelectedSauce = selectedItem;
            }
        }

        [RelayCommand]
        private void SelectionChangedTopping(DataGrid? dataGrid)
        {
            if (dataGrid is not null)
            {
                System.Collections.IList selectedItems = dataGrid.SelectedItems;
                SelectedToppingsName = "Toppings : ";
                SelectedToppings = new();
                foreach (Topping item in selectedItems)
                {
                    SelectedToppingsName += $"{item.Name}, ";
                    SelectedToppings.Add(item);
                }
                SelectedToppingsName = SelectedToppingsName[..^2];
            }
        }

        [RelayCommand]
        private void BtnSauceSaveClick(object? obj)
        {
        }
        [RelayCommand]
        private void BtnSauceDeleteClick(object? obj)
        {
        }
        [RelayCommand]
        private void BtnToppingSaveClick(object? obj)
        {
        }
        [RelayCommand]
        private void BtnToppingDeleteClick(object? obj)
        {
        }

        [RelayCommand]
        private void BtnOkClick(object? obj)
        {
            Pizza pizza = new()
            {
                Name = NewPizzaName,
                Sauce = SelectedSauce,
                Toppings = SelectedToppings
            };

            ValueChangedMessage<Pizza> message = new(pizza);
            WeakReferenceMessenger.Default.Send(message);
            Window?.Close();
        }

        [RelayCommand]
        private void BtnCancelClick(object? obj) => Window?.Close();


        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            App.Logger?.LogInformation("SubView Loaded");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.Logger?.LogInformation("SubView Closing");
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is PizzaContext context)
            {
                _context = context;
                ItemsSourceSauce = GetAllSauce(_context);
                ItemsSourceTopping = GetAllTopping(_context);
            }
        }
    }
}
