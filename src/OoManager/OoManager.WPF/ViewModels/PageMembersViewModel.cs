using System;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using DataBaseTools.Services;
using OoManager.Common.Models;
using OoManager.WPF.Services;
using OoManager.WPF.Views;

namespace OoManager.WPF.ViewModels
{
    public partial class PageMembersViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppData>>
    {
        [ObservableProperty]
        private AppData _appData = App.Data;

        public IViewService viewService;

        public PageMembersViewModel()
        {
            IsActive = true;
            viewService = (IViewService)Ioc.Default.GetService(typeof(IViewService))!;
        }

        [RelayCommand]
        private async Task BtnRefreshClickAsync(object obj)
        {
            await Utiles.RefreshOoDbAsync();
        }


        [RelayCommand]
        private void BtnUpdateMemberClick(ModelMember? member)
        {
            if (member is not null)
            {
                viewService.ShowView<WindowMemberUpdate, WindowMemberUpdateViewModel>(member);
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
            viewService.ShowView<WindowMemberAdd, WindowMemberAddViewModel>();
        }


        public void Receive(ValueChangedMessage<AppData> message)
        {

        }
    }
}
