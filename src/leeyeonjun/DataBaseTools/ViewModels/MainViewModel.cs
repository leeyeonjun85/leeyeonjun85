#pragma warning disable CA2254 // 템플릿은 정적 표현식이어야 합니다.
using System;
using System.ComponentModel;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DataBaseTools.Models;
using DataBaseTools.Views;
using DataBaseTools.ViewModels;
using Microsoft.Extensions.Logging;

namespace DataBaseTools.ViewModels
{
    public partial class MainViewModel : ViewModelBase, IRecipient<ValueChangedMessage<ToMainData>>
    {

        private readonly Regex _regexIsNumeric = IsNumeric(); //regex that matches Numeric

        [ObservableProperty]
        private AppData _appData = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmptyAndIsNumeric))]
        [NotifyCanExecuteChangedFor(nameof(BtnShowSubViewClickCommand))]
        private string _tbName;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEmptyAndIsNumeric))]
        [NotifyCanExecuteChangedFor(nameof(BtnShowSubViewClickCommand))]
        private string _tbOld;

        [ObservableProperty]
        private string _tbMessage = "Sample Message";

        private bool IsNotEmptyAndIsNumeric => (TbName != string.Empty
                                             && TbOld != string.Empty
                                             && IsNumeric(TbOld)
                                                );
        private bool IsNumeric(string str) => _regexIsNumeric.IsMatch(str);


        public MainViewModel()
        {
            App.utiles.InitApp(AppData);
            TbName = ConfigurationManager.AppSettings["MyName"]?.ToString() ?? "이름을 입력하세요";
            TbOld = ConfigurationManager.AppSettings["MyOld"]?.ToString() ?? "0";

            // SubView에서 message받기
            IsActive = true; // ObservableRecipient 상속한 경우
        }

        [RelayCommand(CanExecute = nameof(IsNotEmptyAndIsNumeric))]
        private void BtnShowSubViewClick(ViewModelBase? obj)
        {
            App.viewService.ShowView<SubView, SubViewModel>(
                new SubData() { StringData = TbName, IntData = Convert.ToInt32(TbOld), AppData = AppData }
            );
        }

        [RelayCommand]
        private void MongoDb(ViewModelBase? obj)
        {
            App.viewService.ShowView<MongoDbView, MongoDbViewModel>();
        }

        [RelayCommand]
        private void FireBase(ViewModelBase? obj)
        {
            App.viewService.ShowView<FireBaseView, FireBaseViewModel>();
        }

        [RelayCommand]
        private void SeojungriOracle(ViewModelBase? obj)
        {
            App.viewService.ShowView<SeojungriOracleView, SeojungriOracleViewModel>();
        }

        [RelayCommand]
        private void SQLite(ViewModelBase? obj)
        {
            App.viewService.ShowView<SQLiteView, SQLiteViewModel>(AppData);
        }

        [RelayCommand]
        private void Sftp(ViewModelBase? obj)
        {
            App.viewService.ShowView<SftpView, SftpViewModel>();
        }

        public void Receive(ValueChangedMessage<ToMainData> message)
        {
            TbMessage = message.Value.Message;
        }



        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            App.logger.LogInformation("프로그램이 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.logger.LogInformation("프로그램이 종료되었습니다.");
        }

        [GeneratedRegex("^[0-9]*$")]
        private static partial Regex IsNumeric();
    }
}
