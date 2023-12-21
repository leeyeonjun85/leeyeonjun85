using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using ContosoPizza.Models;
using ContosoPizza.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ContosoPizza.ViewModels
{
    public partial class WindowSubViewModel : ViewModelBase, IParameterReceiver
    {

        [ObservableProperty]
        private ObservableCollection<Sauce> _itemsSourceSauce = new();
        [ObservableProperty]
        private ObservableCollection<Topping> _itemsSourceTopping = new();
        [ObservableProperty]
        private Sauce _selectedSauce = new();
        [ObservableProperty]
        private Topping _selectedTopping = new();
        private IPizzaService _pizzaService { get; set; }

        public WindowSubViewModel(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;

            ItemsSourceSauce = _pizzaService.GetAllSauce();
            ItemsSourceTopping = _pizzaService.GetAllTopping();
        }



        [RelayCommand]
        private void BtnOKClick(object? obj)
        {
            _pizzaService.SaveChanges();
            Window?.Close();
        }

        [RelayCommand]
        private void BtnCancelClick(object? obj) => Window?.Close();

        [RelayCommand]
        private void BtnSauceNewClick(DataGrid? dataGrid)
        {
            ItemsSourceSauce.Add(new Sauce());
        }
        [RelayCommand]
        private void DeleteSauceClick(object? obj)
        {
            _pizzaService.DeleteSauceById(SelectedSauce.Id);
        }
        [RelayCommand]
        private void BtnToppingNewClick(DataGrid? dataGrid)
        {
            ItemsSourceTopping.Add(new Topping());
        }
        [RelayCommand]
        private void DeleteToppingClick(object? obj)
        {
            _pizzaService.DeleteToppingById(SelectedTopping.Id);
        }


        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {

        }

        public void ReceiveParameter(object? parameter)
        {

        }
    }
}
