using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Logging;
using DataBaseTools.Models;

namespace DataBaseTools.ViewModels
{
    public partial class WindowSubViewModel : ViewModelBase, IParameterReceiver
    {
        [ObservableProperty]
        private AppData _appData = App.Data;
        [ObservableProperty]
        private SubData _subData = new();

        public WindowSubViewModel()
        {
        }

        [RelayCommand]
        private void Test1(object? obj)
        {
            //AppData.StatusBar2 = "서브뷰 테스트1";
        }


        [RelayCommand]
        private void BtnOkClick(object? obj)
        {
            SubData _subData = new()
            {
                Name = SubData.Name,
                Old = SubData.Old,
                Message = SubData.Message,
            };

            ValueChangedMessage<SubData> message = new(_subData);
            WeakReferenceMessenger.Default.Send(message);
            Window?.Close();
        }

        [RelayCommand]
        private void BtnCancelClick(object? obj) => Window?.Close();


        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            App.Logger?.LogInformation("SubView가 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.Logger?.LogInformation("SubView가 종료되었습니다.");
        }

        public void ReceiveParameter(object parameter)
        {
            if (parameter is SubData subData)
            {
                SubData = subData;
            }
        }
    }
}
