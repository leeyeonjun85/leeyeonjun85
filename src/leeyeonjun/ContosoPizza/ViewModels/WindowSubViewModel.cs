﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        private string _windowTitle = @"Edit Sauce & Toppings";
        [ObservableProperty]
        private ObservableCollection<Sauce> _itemsSourceSauce = [];
        [ObservableProperty]
        private ObservableCollection<Topping> _itemsSourceTopping = [];
        [ObservableProperty]
        private Sauce _selectedSauce = new();
        [ObservableProperty]
        private Topping _selectedTopping = new();
        private Pizza SelectedPizza { get; set; } = new();
        private int SelectedPizzaIndex { get; set; }
        private IPizzaService PizzaService { get; set; }
        

        public WindowSubViewModel(IPizzaService pizzaService)
        {
            PizzaService = pizzaService;

            ItemsSourceSauce = PizzaService.GetAllSauce();
            ItemsSourceTopping = PizzaService.GetAllTopping();
        }



        [RelayCommand]
        private void BtnOKClick(object? obj)
        {
            PizzaService.SaveChanges();

            ValueChangedMessage<int> message = new(SelectedPizzaIndex);
            WeakReferenceMessenger.Default.Send(message);

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
            PizzaService.DeleteSauceById(SelectedSauce.Id);
        }
        [RelayCommand]
        private void BtnToppingNewClick(DataGrid? dataGrid)
        {
            ItemsSourceTopping.Add(new Topping());
        }
        [RelayCommand]
        private void DeleteToppingClick(object? obj)
        {
            PizzaService.DeleteToppingById(SelectedTopping.Id);
        }


        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {

        }

        public void ReceiveParameter(object? parameter)
        {
            if (parameter is int param)
                SelectedPizzaIndex = param;
        }
    }
}
