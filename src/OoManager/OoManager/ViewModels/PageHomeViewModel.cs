using System.Text;
using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using OoManager.Common;
using OoManager.Models;
using Firebase.Database;
using Utiles;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

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

        [RelayCommand]
        private async void GetFireBase(object obj)
        {
            Task<IReadOnlyCollection<FirebaseObject<OoMembers>>> members1 =  GetMembersAsync(obj);
            await members1;
            IReadOnlyCollection<FirebaseObject<OoMembers>> members = members1.Result;
            
            foreach (var member in members)
            {
                AppModel.Members.Add(member.Object);
            }


            //var members3 = members2.ToObservable();
            //AppModel.Members = (ObservableCollection<OoMembers>)members3.ToList();


            //MessageBox.Show(members2.ToString());
        }

        public async Task<IReadOnlyCollection<FirebaseObject<OoMembers>>> GetMembersAsync(object obj)
        {
            string FirebaseDatabaseUrl = "https://leeyeonjundb-default-rtdb.asia-southeast1.firebasedatabase.app";
            JsonModel jsonModel = MyUtiles.GetJsonModel();
            AppModel.FirebaseClient = new FirebaseClient(FirebaseDatabaseUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(jsonModel.OoManager.FireBaseAuth) });

            IReadOnlyCollection<FirebaseObject<OoMembers>> Members = await AppModel.FirebaseClient
                    .Child("o2study")
                    .Child("member")
                    .OnceAsync<OoMembers>();

            return Members;
        }


        public void Receive(ValueChangedMessage<AppModel> message)
        {
            AppModel = message.Value;
        }
    }
}
