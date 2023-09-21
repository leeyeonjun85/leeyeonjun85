using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using OoManager.Models;

namespace OoManager.ViewModels
{
    public partial class PageHomeViewModel : ViewModelBase, IRecipient<ValueChangedMessage<object[]>>
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppModel _appModel = new();
        [ObservableProperty]
        private PageHomeModel _pageHome = new();
        #endregion


        public PageHomeViewModel()
        {
            IsActive = true;
        }


        [RelayCommand]
        private void Test1(object obj)
        {
            MessageBox.Show($"{PageHome.Text1}");
        }


        public void Receive(ValueChangedMessage<object[]> message)
        {
            AppModel = (AppModel)message.Value[0];

            PageHome = (PageHomeModel)message.Value[1];
        }
    }
}
