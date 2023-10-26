using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace DataBaseTools.ViewModels
{
    public partial class PageSQLIteViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppData>>
    {
        [ObservableProperty]
        private AppData _appData = new();


        public PageSQLIteViewModel()
        {
            IsActive = true;
        }

        public void Receive(ValueChangedMessage<AppData> message)
        {
            AppData = message.Value;
        }
    }
}
