using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using OoManager.Models;
using OoManager.Views;

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

        [RelayCommand]
        private async Task RefreshPageHomeAsync(object obj)
        {
            Task<AppData> _appData = AppData.OoService!.InitAppAsync(AppData);
            await _appData; AppData = _appData.Result;
        }


        public void Receive(ValueChangedMessage<AppData> message)
        {
            AppData = message.Value;
        }
    }
}
