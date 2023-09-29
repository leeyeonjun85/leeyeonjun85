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
            await Task.Run(() =>
            {
                Dispatcher dispatchObject = System.Windows.Application.Current.Dispatcher;
                if (dispatchObject == null || dispatchObject.CheckAccess())
                {
                    AppData.OoService!.RefreshMembersAsync(AppData);
                }

                else dispatchObject.Invoke(() =>
                {
                    AppData.OoService!.RefreshMembersAsync(AppData);
                });
            });



            //IReadOnlyCollection<FirebaseObject<object>> Lectures1 = await AppData.FirebaseDB
            //        .Child("lecture")
            //        .OnceAsync<object>();

            //IReadOnlyCollection<FirebaseObject<string>> Lectures2 = await AppData.FirebaseDB
            //        .Child("lecture")
            //        .OnceAsync<string>();

            //IReadOnlyCollection<FirebaseObject<OoLectures>> Lectures3 = await AppData.FirebaseDB
            //        .Child("lecture")
            //        .OnceAsync<OoLectures>();

            //OoLectures lecture1 = new()
            //{
            //    mid = 1,
            //    o2_class_date = DateTime.Now.ToString("yyyy-MM-dd"),
            //    o2_class_homework = "테스트 숙제",
            //    o2_class_lecture = "테스트 수업",
            //    o2_class_memo = "테스트 메모",
            //    o2_class_time_in = DateTime.Now.AddMinutes(-30).ToString("hh:mm"),
            //    o2_class_time_out = DateTime.Now.ToString("hh:mm"),
            //};

            //await AppData.FirebaseDB
            //        .Child("lecture")
            //        .Child(DateTime.Now.ToString("yyyy-MM-dd"))
            //        .PostAsync(lecture1);
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
            AppData.Members = new();
            AppData.MemberData = new();

            AppData.OoService!.RefreshMembersAsync(AppData);
        }
    }
}
