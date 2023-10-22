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
    public partial class SubViewModel : ViewModelBase, IParameterReceiver
    {
        [ObservableProperty]
        private SubData _subData = new();
        [ObservableProperty]
        private string _tbMessage = "테스트 메시지1";

        public SubViewModel()
        {
        }

        [RelayCommand]
        private void BtnOkClick(object? obj)
        {
            ToMainData toMainData = new()
            {
                Message = TbMessage,
            };

            ValueChangedMessage<ToMainData> message = new ValueChangedMessage<ToMainData>(toMainData);
            WeakReferenceMessenger.Default.Send(message);
            Window?.Close();
        }

        [RelayCommand]
        private void BtnCancelClick(object? obj) => Window?.Close();


        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            App.logger.LogInformation("SubView가 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.logger.LogInformation("SubView가 종료되었습니다.");
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
