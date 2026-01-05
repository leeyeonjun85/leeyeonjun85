using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataBaseTools.Models;
using Microsoft.Extensions.Logging;

namespace DataBaseTools.ViewModels
{
    public partial class MongoDbViewModel : ViewModelBase, IParameterReceiver
    {

        [ObservableProperty]
        private string _statusBar1 = "Status : Ready";
        [ObservableProperty]
        private string _statusBar2 = "Hellow world!";
        [ObservableProperty]
        private int _statusBarProgressBar = 0;

        [ObservableProperty]
        private SubData _subData = new();
        [ObservableProperty]
        private string _tbMessage = "테스트 메시지1";

        public MongoDbViewModel()
        {

        }

        [RelayCommand]
        private void BtnClose(object? obj)
        {
            Window?.Close();
        }

        [RelayCommand]
        private void BtnOk(object? obj)
        {

            Window?.Close();
        }

        [RelayCommand]
        private void BtnCancel(object? obj) => Window?.Close();


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

        //public ICommand CloseCommand => new RelayCommand<object>(_ => Window?.Close());
    }
}
