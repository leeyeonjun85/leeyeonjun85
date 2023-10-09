using System;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Firebase.Database.Query;
using OoManager.Models;
using OoManager.Views;

namespace OoManager.ViewModels
{
    public partial class PageMembersViewModel : ViewModelBase, IRecipient<ValueChangedMessage<AppData>>
    {
        #region 바인딩 멤버
        [ObservableProperty]
        private AppData _appData = new();
        #endregion

        public PageMembersViewModel()
        {
            IsActive = true;
        }

        [RelayCommand]
        private async Task RefreshAsync(object obj)
        {
            Task<AppData> _appData = AppData.OoService!.RefreshDataAsync(AppData);
            await _appData;
            AppData = _appData.Result;
        }



        //[RelayCommand]
        //private async void Refresh(object obj)
        //{
        //    AppData.Members = new();
        //    GetFireBase(AppData);
        //}


        [RelayCommand]
        private void UpdateMember(object obj)
        {
            if (AppData.SelectedMember is not null)
            {
                ViewModelBase viewModel = (ViewModelBase)Ioc.Default.GetService(typeof(WindowMemberUpdateViewModel))!;
                Window view = (Window)Ioc.Default.GetService(typeof(WindowMemberUpdate))!;
                viewModel.SetWindow(view);
                if (viewModel is IParameterReceiver parameterReceiver)
                {
                    parameterReceiver.ReceiveParameter(AppData);
                }
                view.DataContext = viewModel;
                view.Show();
            };
        }

        [RelayCommand]
        private async Task InitMembersAsync(object obj)
        {
            Task<AppData> _appData = AppData.OoService!.InitMembers(AppData);
            await _appData;
            AppData = _appData.Result;
        }

        [RelayCommand]
        private async Task DeleteMemberAsync(object obj)
        {
            if (AppData.SelectedMember is not null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(
                    $"정말로 '{AppData.SelectedMember.Member.member_name}' 회원을 삭제하시겠습니까?{Environment.NewLine}(삭제하면 복구할 수 없습니다.)",
                    "삭제 확인",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    await AppData.FirebaseDB!
                        .Child("member")
                        .Child(AppData.SelectedMember.Key)
                        .DeleteAsync();
                    AppData.Members.Remove(AppData.SelectedMember);
                }
            };
        }

        [RelayCommand]
        private void AddMember(object obj)
        {
            ViewModelBase viewModel = (ViewModelBase)Ioc.Default.GetService(typeof(WindowMemberAddViewModel))!;
            Window view = (Window)Ioc.Default.GetService(typeof(WindowMemberAdd))!;
            viewModel.SetWindow(view);
            if (viewModel is IParameterReceiver parameterReceiver)
            {
                parameterReceiver.ReceiveParameter(AppData);
            }
            view.DataContext = viewModel;
            view.Show();
        }


        public void Receive(ValueChangedMessage<AppData> message)
        {
            AppData = message.Value;
        }
    }
}
