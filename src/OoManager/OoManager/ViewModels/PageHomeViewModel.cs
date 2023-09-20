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


        


        public void Receive(ValueChangedMessage<AppModel> message)
        {
            AppModel = message.Value;
        }
    }
}
