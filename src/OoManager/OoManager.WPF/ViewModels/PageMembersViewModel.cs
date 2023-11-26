using System;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using OoManager.WPF.Services;
using OoManager.WPF.Views;

namespace OoManager.WPF.ViewModels
{
    public partial class PageMembersViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppData>>
    {
        [ObservableProperty]
        private AppData _appData = App.Data;

        public PageMembersViewModel()
        {
            IsActive = true;
        }

        [RelayCommand]
        private async Task BtnRefreshClickAsync(object obj)
        {
            await Utiles.RefreshOoDbAsync();
        }


        [RelayCommand]
        private void BtnUpdateMemberClick(object obj)
        {
            if (AppData.SelectedMember is not null)
            {
                ViewModelBase viewModel = (ViewModelBase)Ioc.Default.GetService(typeof(WindowMemberUpdateViewModel))!;
                Window view = (Window)Ioc.Default.GetService(typeof(WindowMemberUpdate))!;
                viewModel.SetWindow(view);
                view.DataContext = viewModel;
                view.Show();
            };
        }

        [RelayCommand]
        private async Task BtnDeleteMemberClickAsync(object obj)
        {
            if (AppData.SelectedMember is not null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(
                    $"정말로 '{AppData.SelectedMember.name}' 회원을 삭제하시겠습니까?{Environment.NewLine}(삭제하면 복구할 수 없습니다.)",
                    "삭제 확인",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    await Utiles.DeleteMemberAsync(AppData.SelectedMember);
                    await Utiles.RefreshOoDbAsync();
                }
            };
        }

        [RelayCommand]
        private void BtnAddMemberClick(object obj)
        {
            ViewModelBase viewModel = (ViewModelBase)Ioc.Default.GetService(typeof(WindowMemberAddViewModel))!;
            Window view = (Window)Ioc.Default.GetService(typeof(WindowMemberAdd))!;
            viewModel.SetWindow(view);
            view.DataContext = viewModel;
            view.Show();
        }


        public void Receive(ValueChangedMessage<AppData> message)
        {
            
        }
    }
}
