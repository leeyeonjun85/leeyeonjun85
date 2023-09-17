using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using OoManager.Common;
using OoManager.Models;

namespace OoManager.ViewModels
{
    public partial class PageHomeViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppModel>>
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppModel _appModel = new();
        #endregion


        public PageHomeViewModel()
        {
            IsActive = true;

            
        }

        [RelayCommand]
        private void ChatSend(object obj)
        {
            AppModel.ChatHubConnection.SendAsync("SendMessage", "WPF유저", AppModel.ChatSendText);
        }


        public void Receive(ValueChangedMessage<AppModel> message)
        {
            AppModel = message.Value;
        }
    }
}
