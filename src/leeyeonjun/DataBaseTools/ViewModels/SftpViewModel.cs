﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DataBaseTools.Models;
using DataBaseTools.Services;
using Edcore.Models;
using Microsoft.Extensions.Logging;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Utiles;
using static System.Net.WebRequestMethods;

namespace DataBaseTools.ViewModels
{
    public partial class SftpViewModel : ViewModelBase, IParameterReceiver
    {
        private readonly IViewService _viewService;
        private SftpClient? client;

        [ObservableProperty]
        private string _statusBar1 = "Status : Ready";
        [ObservableProperty]
        private string _statusBar2 = "Hellow world!";
        [ObservableProperty]
        private int _statusBarProgressBar = 0;
        [ObservableProperty]
        private bool _isConnected;

        [ObservableProperty]
        private SubData _subData = new();
        [ObservableProperty]
        private string _tbMessage = "테스트 메시지1";

        [ObservableProperty]
        private string _addNameText = "";
        [ObservableProperty]
        private int _addYearsText = 0;
        [ObservableProperty]
        private ObservableCollection<SftpModel> _yeonjunTestItemsSource = new();
        [ObservableProperty]
        private TestSQLiteModel _selectedData = new();
        [ObservableProperty]
        private string _selectedDataString = "아이디 / 이름 / 나이";

        public SftpViewModel(
            IViewService viewService
            )
        {
            _viewService = viewService;
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

        [RelayCommand]
        private void BtnConnect(object? obj)
        {
            try
            {
                JsonModel jsonModel = MyUtiles.GetJsonModel();
                var connectInfo = new ConnectionInfo(jsonModel.Edcore.SFTP.host,
                    Convert.ToInt32(jsonModel.Edcore.SFTP.port),
                    jsonModel.Edcore.SFTP.username,
                    new PasswordAuthenticationMethod(jsonModel.Edcore.SFTP.username, jsonModel.Edcore.SFTP.password));
                client = new SftpClient(connectInfo);

                client.KeepAliveInterval = TimeSpan.FromSeconds(60);
                client.ConnectionInfo.Timeout = TimeSpan.FromMinutes(180);
                client.OperationTimeout = TimeSpan.FromMinutes(180);

                client.Connect();

                IsConnected = client.IsConnected;
                if (IsConnected)
                {
                    GetListDirectory();
                    StatusBar1 = "Status : Connected"; ;
                    StatusBar2 = "SFTP 서버에 연결되었습니다.";
                }
                else { MessageBox.Show($"연결에 문제가 있습니다. {client.IsConnected}"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                throw;
            }
        }

        private void GetListDirectory()
        {
            if ( client is not null)
            {
                client.ChangeDirectory("/D:/SFTP");
                foreach (SftpFile f in client.ListDirectory("."))
                {

                    YeonjunTestItemsSource.Add(new SftpModel() { Name = f.Name, IsDirectory = f.IsDirectory });
                }
            }
            
        }

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
            client.Disconnect();
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
