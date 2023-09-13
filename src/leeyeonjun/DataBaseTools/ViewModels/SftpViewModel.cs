using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DataBaseTools.Models;
using DataBaseTools.Services;
using Edcore.Models;
using Microsoft.Extensions.Logging;
using Renci.SshNet;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Utiles;

namespace DataBaseTools.ViewModels
{
    public partial class SftpViewModel : ViewModelBase, IParameterReceiver
    {
        private readonly IViewService _viewService;

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

        [ObservableProperty]
        private string _addNameText = "";
        [ObservableProperty]
        private int _addYearsText = 0;
        [ObservableProperty]
        private ObservableCollection<TestSQLiteModel> _yeonjunTestItemsSource = new();
        [ObservableProperty]
        private TestSQLiteModel _selectedData = new();
        [ObservableProperty]
        private string _selectedDataString = "아이디 / 이름 / 나이";

        public SftpViewModel(
            IViewService viewService
            )
        {
            _viewService = viewService;

            JsonModel jsonModel = MyUtiles.GetJsonModel();
            var connectInfo = new ConnectionInfo(jsonModel.Edcore.SFTP.host,
                                        Convert.ToInt32(jsonModel.Edcore.SFTP.port),
                                        jsonModel.Edcore.SFTP.username,
                                        new PasswordAuthenticationMethod(jsonModel.Edcore.SFTP.username, jsonModel.Edcore.SFTP.password));
            var client = new SftpClient(connectInfo);

            client.KeepAliveInterval = TimeSpan.FromSeconds(60);
            client.ConnectionInfo.Timeout = TimeSpan.FromMinutes(180);
            client.OperationTimeout = TimeSpan.FromMinutes(180);

            // SFTP 서버 연결
            client.Connect();

            bool IsConnected = client.IsConnected;
        }



        //[RelayCommand]
        //private void BtnUpdate(object? obj)
        //{
        //    //TestSQLiteModel foundata = _context.yeonjunTest.Find(SelectedData.Id)!;

        //    _context.Entry(SelectedData).State = EntityState.Modified;
        //    _context.SaveChanges();
        //}

        //[RelayCommand]
        //private void BtnDelete(object? obj)
        //{
        //    _context.yeonjunTest.Remove(SelectedData);
        //    _context.SaveChanges();
        //}

        //[RelayCommand]
        //private void MouseLeftButtonUp(object? obj)
        //{
        //    if (obj is not MouseButtonEventArgs args) return;
        //    if (args!.OriginalSource is not TextBlock textBlock) return;
        //    if (textBlock.DataContext is not TestSQLiteModel yeonjunTest) return;
        //    SelectedData = yeonjunTest;
        //    SelectedDataString = $"{yeonjunTest.Name} / {yeonjunTest.Years}";
        //}

        //[RelayCommand]
        //private void BtnConnect(object? obj)
        //{
        //    //_context.Database.EnsureCreated();

        //    if (!_context.Database.GetService<IRelationalDatabaseCreator>().Exists())
        //    {
        //        RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)_context.Database.GetService<IDatabaseCreator>();
        //        databaseCreator.CreateTables();
        //        StatusBar1 = "Status : Connected";
        //        StatusBar2 = "SQLite 데이터베이스를 생성하였습니다.";
        //    }

        //    StatusBar1 = "Status : Connected"; ;
        //    StatusBar2 = "SQLite 데이터베이스에 연결되었습니다.";

        //    _context.yeonjunTest.Load();
        //    YeonjunTestItemsSource = _context.yeonjunTest.Local.ToObservableCollection();

        //}

        //[RelayCommand]
        //private void AddData(object? obj)
        //{
        //    _context.yeonjunTest.Add(new TestSQLiteModel() { Name = AddNameText, Years = AddYearsText });
        //    _context.SaveChanges();
        //}

        [RelayCommand]
        private void BtnClose(object? obj)
        {
            Window?.Close();
        }

        [RelayCommand]
        private void BtnOk(object? obj)
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
        private void BtnCancel(object? obj) => Window?.Close();

        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            App.LOGGER!.LogInformation("SQLite가 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            App.LOGGER!.LogInformation("SQLite가 종료되었습니다.");
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
