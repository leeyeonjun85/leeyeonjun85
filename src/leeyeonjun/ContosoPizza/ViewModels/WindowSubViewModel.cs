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

namespace ContosoPizza.ViewModels
{
    public partial class WindowSubViewModel : ViewModelBase, IParameterReceiver
    {
        private PizzaContext? _context { get; set; }
        [ObservableProperty]
        private AppData _appData = App.Data;

        [ObservableProperty]
        private ObservableCollection<Sauce> _itemsSourceSauce = new();

        public WindowSubViewModel()
        {
        }

        public ObservableCollection<Sauce> GetAllSauce(PizzaContext context)
        {
            ObservableCollection<Sauce> returnData = new();

            context.Sauces
                    .AsNoTracking()
                    .ToList()
                    .ForEach(x => { returnData.Add(x); });

            return returnData;
        }

        [RelayCommand]
        private void BtnOkClick(object? obj)
        {
            Pizza pizza = new()
            {
                
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
            }
        }
    }
}
