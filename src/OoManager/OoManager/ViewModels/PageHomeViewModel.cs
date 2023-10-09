using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using OoManager.Models;

namespace OoManager.ViewModels
{
    public partial class PageHomeViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppData>>
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = new();
        #endregion


        public PageHomeViewModel()
        {
            IsActive = true;
        }

        public void Receive(ValueChangedMessage<AppData> message)
        {
            AppData = message.Value;
        }
    }
}
