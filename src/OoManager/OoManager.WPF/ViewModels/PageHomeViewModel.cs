using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using OoManager.WPF.Services;

namespace OoManager.WPF.ViewModels
{
    public partial class PageHomeViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppData>>
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = App.Data;
        #endregion


        public PageHomeViewModel()
        {
            IsActive = true;
        }


        [RelayCommand]
        private void BtnTest1(object obj)
        {
            Utiles.InitSQLiteDataBase();
        }


        public void Receive(ValueChangedMessage<AppData> message)
        {
            AppData = message.Value;
        }
    }
}
