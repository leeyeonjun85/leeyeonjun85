using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Logging;
using ContosoPizza.Models;

namespace ContosoPizza.ViewModels
{
    public partial class WindowSubViewModel : ViewModelBase, IParameterReceiver
    {
        private PizzaContext? _context { get; set; }
        [ObservableProperty]
        private AppData _appData = App.Data;
        [ObservableProperty]
        private SubData _subData = new();
        [ObservableProperty]
        private string _statusBar1 = "Ready";
        [ObservableProperty]
        private string _statusBar2 = "Meassage";
        [ObservableProperty]
        private bool _progressBarIsIndeterminate = false;
        [ObservableProperty]
        private int _statusBarProgressBar = 80;

        public WindowSubViewModel()
        {
        }


        [RelayCommand]
        private void BtnOkClick(object? obj)
        {
            SubData _subData = new()
            {
                Name = SubData.Name,
                Old = SubData.Old,
            };

            ValueChangedMessage<SubData> message = new(_subData);
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
            }
        }
    }
}
