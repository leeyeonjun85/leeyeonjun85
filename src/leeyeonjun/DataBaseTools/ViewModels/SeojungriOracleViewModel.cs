using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Logging;
using DataBaseTools.Models;
using Serilog.Context;
using DataBaseTools.Services;
using Edcore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Windows.Forms;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataBaseTools.ViewModels
{
    public partial class SeojungriOracleViewModel : ViewModelBase, IParameterReceiver
    {
        private readonly IViewService _viewService;
        private readonly SeojungriOracleContext _context;

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

        public SeojungriOracleViewModel(
            IViewService viewService,
            SeojungriOracleContext context
            )
        {
            _viewService = viewService;
            _context = context;
            
        }

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
            RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)_context.Database.GetService<IDatabaseCreator>();
            databaseCreator.CreateTables();

            App.LOGGER!.LogInformation("SubView가 시작되었습니다.");
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            _context.yeonjunTest.Add(new YeonjunTest() { Id = 3, Name = "이연준", Years = 38 });
            _context.SaveChanges();
            App.LOGGER!.LogInformation("SubView가 종료되었습니다.");
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
