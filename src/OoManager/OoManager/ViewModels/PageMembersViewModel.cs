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
    public partial class PageMembersViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppModel>>
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppModel _appModel = new();
        #endregion


        public PageMembersViewModel()
        {
            IsActive = true;
        }

        [RelayCommand]
        private void AddMember(object obj)
        {
            var addData = new ModelMembers { name = AppModel.AddMemeberName };

            AppModel.OoDbContext.members.Add(addData);
            AppModel.OoDbContext.SaveChanges();

            AppModel.OoDbContext.members.Load();
            AppModel.Members = AppModel.OoDbContext.members.Local.ToObservableCollection();

            AppModel.OoHubConnection.SendAsync("OoMessage", OoMessageType.MemberAdd);
        }


        public void Receive(ValueChangedMessage<AppModel> message)
        {
            AppModel = message.Value;
        }
    }
}
