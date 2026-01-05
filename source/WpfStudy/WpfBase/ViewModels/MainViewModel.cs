using System;
using System.ComponentModel;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Logging;
using WpfBase.Models;
using WpfBase.Services;

namespace WpfBase.ViewModels
{
    public partial class MainViewModel : ViewModelBase, IRecipient<ValueChangedMessage<ToMainData>>
    {
        private readonly IViewService _viewService;
        private readonly Regex _regexIsNumeric = new Regex("[0-9]+"); //regex that matches Numeric

        [ObservableProperty]
        private string _statusBar1 = "Status : Ready";
        [ObservableProperty]
        private string _statusBar2 = "Hellow world!";
        [ObservableProperty]
        private int _statusBarProgressBar = 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmptyAndIsNumeric))]
        [NotifyCanExecuteChangedFor(nameof(ShowSubViewCommand))]
        private string _tbName;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmptyAndIsNumeric))]
        [NotifyCanExecuteChangedFor(nameof(ShowSubViewCommand))]
        private string _tbOld;

        [ObservableProperty]
        private string _tbMessage = "Sample Message";

        private bool IsNotEmptyAndIsNumeric => (TbName != string.Empty
                                             && TbOld != string.Empty
                                             && IsNumeric(TbOld)
                                                );
        private bool IsNumeric(string str) => _regexIsNumeric.IsMatch(str);


        public MainViewModel(IViewService viewService)
        {
            _viewService = viewService;

            TbName = ConfigurationManager.AppSettings["MyName"]?.ToString() ?? "이름을 입력하세요";
            TbOld = ConfigurationManager.AppSettings["MyOld"]?.ToString() ?? "0";

            // SubView에서 message받기
            //WeakReferenceMessenger.Default.RegisterAll(this); // ObservableObject 상속한 경우
            IsActive = true; // ObservableRecipient 상속한 경우
        }

        [RelayCommand(CanExecute = nameof(IsNotEmptyAndIsNumeric))]
        private void ShowSubView(object? obj)
        {
            _viewService.ShowSubView(new Models.SubData { StringData = TbName, IntData = Convert.ToInt16(TbOld) });
        }


        public void Receive(ValueChangedMessage<ToMainData> message)
        {
            TbMessage = message.Value.Message;
        }



        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            App.LOGGER!.LogInformation("프로그램이 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.LOGGER!.LogInformation("프로그램이 종료되었습니다.");
        }
    }
}
