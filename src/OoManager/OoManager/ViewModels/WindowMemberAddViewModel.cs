using System;
using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.Logging;
using OoManager.Common;
using OoManager.Models;
using OoManager.Services;

namespace OoManager.ViewModels
{
    public partial class WindowMemberAddViewModel : ViewModelBase
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = new();
        [ObservableProperty]
        private MemberAdd _memberAdd = new();
        #endregion


        public WindowMemberAddViewModel(
            OoDbContext ooDbContext,
            IOoService ooService)
        {
            AppData.OoDbContext = ooDbContext;
            AppData.OoService = ooService;
        }


        

        protected override void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnWindowClosing(object? sender, CancelEventArgs e)
        {
        }
    }
}
