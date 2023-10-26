using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace DataBaseTools.ViewModels
{
    public partial class PageHomeViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppData>>
    {
        [ObservableProperty]
        private AppData _appData = new();


        public PageHomeViewModel()
        {
            IsActive = true;
        }


        [RelayCommand]
        private void BtnTest1(ViewModelBase? obj)
        {
            AppData.String1 = "test_home";
        }



        public void Receive(ValueChangedMessage<AppData> message)
        {
            AppData = message.Value;
        }
    }
}
