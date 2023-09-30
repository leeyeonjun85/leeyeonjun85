using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Firebase.Database.Query;
using OoManager.Models;
using OoManager.Views;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace OoManager.ViewModels
{
    public partial class PageLectureViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppData>>
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = new();
        #endregion

        public PageLectureViewModel()
        {
            IsActive = true;
        }

        [RelayCommand]
        private void BtnTest1(object obj)
        {
            MessageBox.Show($"{AppData.SelectedPage!.Title}");
        }


        public void Receive(ValueChangedMessage<AppData> message)
        {
            AppData = message.Value;
            AppData = AppData.OoService!.GetFireBase(AppData);
        }
    }
}
